﻿using System;
using System.Threading;
using Appalachia.Utility.Async.Internal;

namespace Appalachia.Utility.Async.Linq
{
    public static partial class AppaTaskAsyncEnumerable
    {
        public static IAppaTaskAsyncEnumerable<TSource> SkipUntilCanceled<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            CancellationToken cancellationToken)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return new SkipUntilCanceled<TSource>(source, cancellationToken);
        }
    }

    internal sealed class SkipUntilCanceled<TSource> : IAppaTaskAsyncEnumerable<TSource>
    {
        private readonly IAppaTaskAsyncEnumerable<TSource> source;
        private readonly CancellationToken cancellationToken;

        public SkipUntilCanceled(
            IAppaTaskAsyncEnumerable<TSource> source,
            CancellationToken cancellationToken)
        {
            this.source = source;
            this.cancellationToken = cancellationToken;
        }

        public IAppaTaskAsyncEnumerator<TSource> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            return new _SkipUntilCanceled(source, this.cancellationToken, cancellationToken);
        }

        private sealed class _SkipUntilCanceled : MoveNextSource, IAppaTaskAsyncEnumerator<TSource>
        {
            private static readonly Action<object> CancelDelegate1 = OnCanceled1;
            private static readonly Action<object> CancelDelegate2 = OnCanceled2;
            private static readonly Action<object> MoveNextCoreDelegate = MoveNextCore;

            private readonly IAppaTaskAsyncEnumerable<TSource> source;
            private CancellationToken cancellationToken1;
            private CancellationToken cancellationToken2;
            private CancellationTokenRegistration cancellationTokenRegistration1;
            private CancellationTokenRegistration cancellationTokenRegistration2;

            private int isCanceled;
            private IAppaTaskAsyncEnumerator<TSource> enumerator;
            private AppaTask<bool>.Awaiter awaiter;
            private bool continueNext;

            public _SkipUntilCanceled(
                IAppaTaskAsyncEnumerable<TSource> source,
                CancellationToken cancellationToken1,
                CancellationToken cancellationToken2)
            {
                this.source = source;
                this.cancellationToken1 = cancellationToken1;
                this.cancellationToken2 = cancellationToken2;
                if (cancellationToken1.CanBeCanceled)
                {
                    cancellationTokenRegistration1 =
                        cancellationToken1.RegisterWithoutCaptureExecutionContext(CancelDelegate1, this);
                }

                if ((cancellationToken1 != cancellationToken2) && cancellationToken2.CanBeCanceled)
                {
                    cancellationTokenRegistration2 =
                        cancellationToken2.RegisterWithoutCaptureExecutionContext(CancelDelegate2, this);
                }

                TaskTracker.TrackActiveTask(this, 3);
            }

            public TSource Current { get; private set; }

            public AppaTask<bool> MoveNextAsync()
            {
                if (enumerator == null)
                {
                    if (cancellationToken1.IsCancellationRequested)
                    {
                        isCanceled = 1;
                    }

                    if (cancellationToken2.IsCancellationRequested)
                    {
                        isCanceled = 1;
                    }

                    enumerator =
                        source.GetAsyncEnumerator(
                            cancellationToken2
                        ); // use only AsyncEnumerator provided token.
                }

                completionSource.Reset();

                if (isCanceled != 0)
                {
                    SourceMoveNext();
                }

                return new AppaTask<bool>(this, completionSource.Version);
            }

            private void SourceMoveNext()
            {
                try
                {
                    LOOP:
                    awaiter = enumerator.MoveNextAsync().GetAwaiter();
                    if (awaiter.IsCompleted)
                    {
                        continueNext = true;
                        MoveNextCore(this);
                        if (continueNext)
                        {
                            continueNext = false;
                            goto LOOP;
                        }
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(MoveNextCoreDelegate, this);
                    }
                }
                catch (Exception ex)
                {
                    completionSource.TrySetException(ex);
                }
            }

            private static void MoveNextCore(object state)
            {
                var self = (_SkipUntilCanceled)state;

                if (self.TryGetResult(self.awaiter, out var result))
                {
                    if (result)
                    {
                        self.Current = self.enumerator.Current;
                        self.completionSource.TrySetResult(true);
                        if (self.continueNext)
                        {
                            self.SourceMoveNext();
                        }
                    }
                    else
                    {
                        self.completionSource.TrySetResult(false);
                    }
                }
            }

            private static void OnCanceled1(object state)
            {
                var self = (_SkipUntilCanceled)state;
                if (self.isCanceled == 0)
                {
                    if (Interlocked.Increment(ref self.isCanceled) == 1)
                    {
                        self.cancellationTokenRegistration2.Dispose();
                        self.SourceMoveNext();
                    }
                }
            }

            private static void OnCanceled2(object state)
            {
                var self = (_SkipUntilCanceled)state;
                if (self.isCanceled == 0)
                {
                    if (Interlocked.Increment(ref self.isCanceled) == 1)
                    {
                        self.cancellationTokenRegistration2.Dispose();
                        self.SourceMoveNext();
                    }
                }
            }

            public AppaTask DisposeAsync()
            {
                TaskTracker.RemoveTracking(this);
                cancellationTokenRegistration1.Dispose();
                cancellationTokenRegistration2.Dispose();
                if (enumerator != null)
                {
                    return enumerator.DisposeAsync();
                }

                return default;
            }
        }
    }
}
