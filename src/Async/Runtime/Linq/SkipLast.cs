using System;
using System.Collections.Generic;
using System.Threading;
using Appalachia.Utility.Async.Internal;

namespace Appalachia.Utility.Async.Linq
{
    public static partial class AppaTaskAsyncEnumerable
    {
        public static IAppaTaskAsyncEnumerable<TSource> SkipLast<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            int count)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            // non skip.
            if (count <= 0)
            {
                return source;
            }

            return new SkipLast<TSource>(source, count);
        }
    }

    internal sealed class SkipLast<TSource> : IAppaTaskAsyncEnumerable<TSource>
    {
        private readonly IAppaTaskAsyncEnumerable<TSource> source;
        private readonly int count;

        public SkipLast(IAppaTaskAsyncEnumerable<TSource> source, int count)
        {
            this.source = source;
            this.count = count;
        }

        public IAppaTaskAsyncEnumerator<TSource> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            return new _SkipLast(source, count, cancellationToken);
        }

        private sealed class _SkipLast : MoveNextSource, IAppaTaskAsyncEnumerator<TSource>
        {
            private static readonly Action<object> MoveNextCoreDelegate = MoveNextCore;

            private readonly IAppaTaskAsyncEnumerable<TSource> source;
            private readonly int count;
            private CancellationToken cancellationToken;

            private IAppaTaskAsyncEnumerator<TSource> enumerator;
            private AppaTask<bool>.Awaiter awaiter;
            private Queue<TSource> queue;

            private bool continueNext;

            public _SkipLast(
                IAppaTaskAsyncEnumerable<TSource> source,
                int count,
                CancellationToken cancellationToken)
            {
                this.source = source;
                this.count = count;
                this.cancellationToken = cancellationToken;
                TaskTracker.TrackActiveTask(this, 3);
            }

            public TSource Current { get; private set; }

            public AppaTask<bool> MoveNextAsync()
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (enumerator == null)
                {
                    enumerator = source.GetAsyncEnumerator(cancellationToken);
                    queue = new Queue<TSource>();
                }

                completionSource.Reset();
                SourceMoveNext();
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
                            goto LOOP; // avoid recursive
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
                var self = (_SkipLast)state;

                if (self.TryGetResult(self.awaiter, out var result))
                {
                    if (result)
                    {
                        if (self.queue.Count == self.count)
                        {
                            self.continueNext = false;

                            var deq = self.queue.Dequeue();
                            self.Current = deq;
                            self.queue.Enqueue(self.enumerator.Current);

                            self.completionSource.TrySetResult(true);
                        }
                        else
                        {
                            self.queue.Enqueue(self.enumerator.Current);

                            if (!self.continueNext)
                            {
                                self.SourceMoveNext();
                            }
                        }
                    }
                    else
                    {
                        self.continueNext = false;
                        self.completionSource.TrySetResult(false);
                    }
                }
                else
                {
                    self.continueNext = false;
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
