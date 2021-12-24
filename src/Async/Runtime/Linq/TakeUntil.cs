﻿using System;
using System.Threading;
using Appalachia.Utility.Async.Internal;

namespace Appalachia.Utility.Async.Linq
{
    public static partial class AppaTaskAsyncEnumerable
    {
        public static IAppaTaskAsyncEnumerable<TSource> TakeUntil<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            AppaTask other)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return new TakeUntil<TSource>(source, other, null);
        }

        public static IAppaTaskAsyncEnumerable<TSource> TakeUntil<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<CancellationToken, AppaTask> other)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(other));

            return new TakeUntil<TSource>(source, default, other);
        }
    }

    internal sealed class TakeUntil<TSource> : IAppaTaskAsyncEnumerable<TSource>
    {
        private readonly IAppaTaskAsyncEnumerable<TSource> source;
        private readonly AppaTask other;
        private readonly Func<CancellationToken, AppaTask> other2;

        public TakeUntil(
            IAppaTaskAsyncEnumerable<TSource> source,
            AppaTask other,
            Func<CancellationToken, AppaTask> other2)
        {
            this.source = source;
            this.other = other;
            this.other2 = other2;
        }

        public IAppaTaskAsyncEnumerator<TSource> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            if (other2 != null)
            {
                return new _TakeUntil(source, other2(cancellationToken), cancellationToken);
            }

            return new _TakeUntil(source, other, cancellationToken);
        }

        private sealed class _TakeUntil : MoveNextSource, IAppaTaskAsyncEnumerator<TSource>
        {
            private static readonly Action<object> CancelDelegate1 = OnCanceled1;
            private static readonly Action<object> MoveNextCoreDelegate = MoveNextCore;

            private readonly IAppaTaskAsyncEnumerable<TSource> source;
            private CancellationToken cancellationToken1;
            private CancellationTokenRegistration cancellationTokenRegistration1;

            private bool completed;
            private Exception exception;
            private IAppaTaskAsyncEnumerator<TSource> enumerator;
            private AppaTask<bool>.Awaiter awaiter;

            public _TakeUntil(
                IAppaTaskAsyncEnumerable<TSource> source,
                AppaTask other,
                CancellationToken cancellationToken1)
            {
                this.source = source;
                this.cancellationToken1 = cancellationToken1;

                if (cancellationToken1.CanBeCanceled)
                {
                    cancellationTokenRegistration1 =
                        cancellationToken1.RegisterWithoutCaptureExecutionContext(CancelDelegate1, this);
                }

                TaskTracker.TrackActiveTask(this, 3);

                RunOther(other).Forget();
            }

            public TSource Current { get; private set; }

            public AppaTask<bool> MoveNextAsync()
            {
                if (completed)
                {
                    return CompletedTasks.False;
                }

                if (exception != null)
                {
                    return AppaTask.FromException<bool>(exception);
                }

                if (cancellationToken1.IsCancellationRequested)
                {
                    return AppaTask.FromCanceled<bool>(cancellationToken1);
                }

                if (enumerator == null)
                {
                    enumerator = source.GetAsyncEnumerator(cancellationToken1);
                }

                completionSource.Reset();
                SourceMoveNext();
                return new AppaTask<bool>(this, completionSource.Version);
            }

            private void SourceMoveNext()
            {
                try
                {
                    awaiter = enumerator.MoveNextAsync().GetAwaiter();
                    if (awaiter.IsCompleted)
                    {
                        MoveNextCore(this);
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
                var self = (_TakeUntil)state;

                if (self.TryGetResult(self.awaiter, out var result))
                {
                    if (result)
                    {
                        if (self.exception != null)
                        {
                            self.completionSource.TrySetException(self.exception);
                        }
                        else if (self.cancellationToken1.IsCancellationRequested)
                        {
                            self.completionSource.TrySetCanceled(self.cancellationToken1);
                        }
                        else
                        {
                            self.Current = self.enumerator.Current;
                            self.completionSource.TrySetResult(true);
                        }
                    }
                    else
                    {
                        self.completionSource.TrySetResult(false);
                    }
                }
            }

            private async AppaTaskVoid RunOther(AppaTask other)
            {
                try
                {
                    await other;
                    completed = true;
                    completionSource.TrySetResult(false);
                }
                catch (Exception ex)
                {
                    exception = ex;
                    completionSource.TrySetException(ex);
                }
            }

            private static void OnCanceled1(object state)
            {
                var self = (_TakeUntil)state;
                self.completionSource.TrySetCanceled(self.cancellationToken1);
            }

            public AppaTask DisposeAsync()
            {
                TaskTracker.RemoveTracking(this);
                cancellationTokenRegistration1.Dispose();
                if (enumerator != null)
                {
                    return enumerator.DisposeAsync();
                }

                return default;
            }
        }
    }
}
