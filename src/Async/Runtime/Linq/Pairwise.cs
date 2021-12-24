using System;
using System.Threading;
using Appalachia.Utility.Async.Internal;

namespace Appalachia.Utility.Async.Linq
{
    public static partial class AppaTaskAsyncEnumerable
    {
        public static IAppaTaskAsyncEnumerable<(TSource, TSource)> Pairwise<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return new Pairwise<TSource>(source);
        }
    }

    internal sealed class Pairwise<TSource> : IAppaTaskAsyncEnumerable<(TSource, TSource)>
    {
        private readonly IAppaTaskAsyncEnumerable<TSource> source;

        public Pairwise(IAppaTaskAsyncEnumerable<TSource> source)
        {
            this.source = source;
        }

        public IAppaTaskAsyncEnumerator<(TSource, TSource)> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            return new _Pairwise(source, cancellationToken);
        }

        private sealed class _Pairwise : MoveNextSource, IAppaTaskAsyncEnumerator<(TSource, TSource)>
        {
            private static readonly Action<object> MoveNextCoreDelegate = MoveNextCore;

            private readonly IAppaTaskAsyncEnumerable<TSource> source;
            private CancellationToken cancellationToken;

            private IAppaTaskAsyncEnumerator<TSource> enumerator;
            private AppaTask<bool>.Awaiter awaiter;

            private TSource prev;
            private bool isFirst;

            public _Pairwise(IAppaTaskAsyncEnumerable<TSource> source, CancellationToken cancellationToken)
            {
                this.source = source;
                this.cancellationToken = cancellationToken;
                TaskTracker.TrackActiveTask(this, 3);
            }

            public (TSource, TSource) Current { get; private set; }

            public AppaTask<bool> MoveNextAsync()
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (enumerator == null)
                {
                    isFirst = true;
                    enumerator = source.GetAsyncEnumerator(cancellationToken);
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
                var self = (_Pairwise)state;

                if (self.TryGetResult(self.awaiter, out var result))
                {
                    if (result)
                    {
                        if (self.isFirst)
                        {
                            self.isFirst = false;
                            self.prev = self.enumerator.Current;
                            self.SourceMoveNext(); // run again. okay to use recursive(only one more).
                        }
                        else
                        {
                            var p = self.prev;
                            self.prev = self.enumerator.Current;
                            self.Current = (p, self.prev);
                            self.completionSource.TrySetResult(true);
                        }
                    }
                    else
                    {
                        self.completionSource.TrySetResult(false);
                    }
                }
            }

            public AppaTask DisposeAsync()
            {
                TaskTracker.RemoveTracking(this);
                if (enumerator != null)
                {
                    return enumerator.DisposeAsync();
                }

                return default;
            }
        }
    }
}
