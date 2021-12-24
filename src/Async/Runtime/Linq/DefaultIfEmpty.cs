using System;
using System.Threading;
using Appalachia.Utility.Async.Internal;

namespace Appalachia.Utility.Async.Linq
{
    public static partial class AppaTaskAsyncEnumerable
    {
        public static IAppaTaskAsyncEnumerable<TSource> DefaultIfEmpty<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return new DefaultIfEmpty<TSource>(source, default);
        }

        public static IAppaTaskAsyncEnumerable<TSource> DefaultIfEmpty<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            TSource defaultValue)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return new DefaultIfEmpty<TSource>(source, defaultValue);
        }
    }

    internal sealed class DefaultIfEmpty<TSource> : IAppaTaskAsyncEnumerable<TSource>
    {
        private readonly IAppaTaskAsyncEnumerable<TSource> source;
        private readonly TSource defaultValue;

        public DefaultIfEmpty(IAppaTaskAsyncEnumerable<TSource> source, TSource defaultValue)
        {
            this.source = source;
            this.defaultValue = defaultValue;
        }

        public IAppaTaskAsyncEnumerator<TSource> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            return new _DefaultIfEmpty(source, defaultValue, cancellationToken);
        }

        private sealed class _DefaultIfEmpty : MoveNextSource, IAppaTaskAsyncEnumerator<TSource>
        {
            private enum IteratingState : byte
            {
                Empty,
                Iterating,
                Completed
            }

            private static readonly Action<object> MoveNextCoreDelegate = MoveNextCore;

            private readonly IAppaTaskAsyncEnumerable<TSource> source;
            private readonly TSource defaultValue;
            private CancellationToken cancellationToken;

            private IteratingState iteratingState;
            private IAppaTaskAsyncEnumerator<TSource> enumerator;
            private AppaTask<bool>.Awaiter awaiter;

            public _DefaultIfEmpty(
                IAppaTaskAsyncEnumerable<TSource> source,
                TSource defaultValue,
                CancellationToken cancellationToken)
            {
                this.source = source;
                this.defaultValue = defaultValue;
                this.cancellationToken = cancellationToken;

                iteratingState = IteratingState.Empty;
                TaskTracker.TrackActiveTask(this, 3);
            }

            public TSource Current { get; private set; }

            public AppaTask<bool> MoveNextAsync()
            {
                cancellationToken.ThrowIfCancellationRequested();
                completionSource.Reset();

                if (iteratingState == IteratingState.Completed)
                {
                    return CompletedTasks.False;
                }

                if (enumerator == null)
                {
                    enumerator = source.GetAsyncEnumerator(cancellationToken);
                }

                awaiter = enumerator.MoveNextAsync().GetAwaiter();

                if (awaiter.IsCompleted)
                {
                    MoveNextCore(this);
                }
                else
                {
                    awaiter.SourceOnCompleted(MoveNextCoreDelegate, this);
                }

                return new AppaTask<bool>(this, completionSource.Version);
            }

            private static void MoveNextCore(object state)
            {
                var self = (_DefaultIfEmpty)state;

                if (self.TryGetResult(self.awaiter, out var result))
                {
                    if (result)
                    {
                        self.iteratingState = IteratingState.Iterating;
                        self.Current = self.enumerator.Current;
                        self.completionSource.TrySetResult(true);
                    }
                    else
                    {
                        if (self.iteratingState == IteratingState.Empty)
                        {
                            self.iteratingState = IteratingState.Completed;

                            self.Current = self.defaultValue;
                            self.completionSource.TrySetResult(true);
                        }
                        else
                        {
                            self.completionSource.TrySetResult(false);
                        }
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
