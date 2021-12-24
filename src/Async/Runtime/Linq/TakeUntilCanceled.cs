using System;
using System.Threading;
using Appalachia.Utility.Async.Internal;

namespace Appalachia.Utility.Async.Linq
{
    public static partial class AppaTaskAsyncEnumerable
    {
        public static IAppaTaskAsyncEnumerable<TSource> TakeUntilCanceled<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            CancellationToken cancellationToken)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return new TakeUntilCanceled<TSource>(source, cancellationToken);
        }
    }

    internal sealed class TakeUntilCanceled<TSource> : IAppaTaskAsyncEnumerable<TSource>
    {
        private readonly IAppaTaskAsyncEnumerable<TSource> source;
        private readonly CancellationToken cancellationToken;

        public TakeUntilCanceled(
            IAppaTaskAsyncEnumerable<TSource> source,
            CancellationToken cancellationToken)
        {
            this.source = source;
            this.cancellationToken = cancellationToken;
        }

        public IAppaTaskAsyncEnumerator<TSource> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            return new _TakeUntilCanceled(source, this.cancellationToken, cancellationToken);
        }

        private sealed class _TakeUntilCanceled : MoveNextSource, IAppaTaskAsyncEnumerator<TSource>
        {
            private static readonly Action<object> CancelDelegate1 = OnCanceled1;
            private static readonly Action<object> CancelDelegate2 = OnCanceled2;
            private static readonly Action<object> MoveNextCoreDelegate = MoveNextCore;

            private readonly IAppaTaskAsyncEnumerable<TSource> source;
            private CancellationToken cancellationToken1;
            private CancellationToken cancellationToken2;
            private CancellationTokenRegistration cancellationTokenRegistration1;
            private CancellationTokenRegistration cancellationTokenRegistration2;

            private bool isCanceled;
            private IAppaTaskAsyncEnumerator<TSource> enumerator;
            private AppaTask<bool>.Awaiter awaiter;

            public _TakeUntilCanceled(
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
                if (cancellationToken1.IsCancellationRequested)
                {
                    isCanceled = true;
                }

                if (cancellationToken2.IsCancellationRequested)
                {
                    isCanceled = true;
                }

                if (enumerator == null)
                {
                    enumerator =
                        source.GetAsyncEnumerator(
                            cancellationToken2
                        ); // use only AsyncEnumerator provided token.
                }

                if (isCanceled)
                {
                    return CompletedTasks.False;
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
                var self = (_TakeUntilCanceled)state;

                if (self.TryGetResult(self.awaiter, out var result))
                {
                    if (result)
                    {
                        if (self.isCanceled)
                        {
                            self.completionSource.TrySetResult(false);
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

            private static void OnCanceled1(object state)
            {
                var self = (_TakeUntilCanceled)state;
                if (!self.isCanceled)
                {
                    self.cancellationTokenRegistration2.Dispose();
                    self.completionSource.TrySetResult(false);
                }
            }

            private static void OnCanceled2(object state)
            {
                var self = (_TakeUntilCanceled)state;
                if (!self.isCanceled)
                {
                    self.cancellationTokenRegistration1.Dispose();
                    self.completionSource.TrySetResult(false);
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
