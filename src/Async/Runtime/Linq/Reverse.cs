using System.Threading;
using Appalachia.Utility.Async.Internal;

namespace Appalachia.Utility.Async.Linq
{
    public static partial class AppaTaskAsyncEnumerable
    {
        public static IAppaTaskAsyncEnumerable<TSource> Reverse<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            return new Reverse<TSource>(source);
        }
    }

    internal sealed class Reverse<TSource> : IAppaTaskAsyncEnumerable<TSource>
    {
        private readonly IAppaTaskAsyncEnumerable<TSource> source;

        public Reverse(IAppaTaskAsyncEnumerable<TSource> source)
        {
            this.source = source;
        }

        public IAppaTaskAsyncEnumerator<TSource> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            return new _Reverse(source, cancellationToken);
        }

        private sealed class _Reverse : MoveNextSource, IAppaTaskAsyncEnumerator<TSource>
        {
            private readonly IAppaTaskAsyncEnumerable<TSource> source;
            private CancellationToken cancellationToken;

            private TSource[] array;
            private int index;

            public _Reverse(IAppaTaskAsyncEnumerable<TSource> source, CancellationToken cancellationToken)
            {
                this.source = source;
                this.cancellationToken = cancellationToken;
                TaskTracker.TrackActiveTask(this, 3);
            }

            public TSource Current { get; private set; }

            // after consumed array, don't use await so allow async(not require AppaTaskCompletionSourceCore).
            public async AppaTask<bool> MoveNextAsync()
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (array == null)
                {
                    array = await source.ToArrayAsync(cancellationToken);
                    index = array.Length - 1;
                }

                if (index != -1)
                {
                    Current = array[index];
                    --index;
                    return true;
                }

                return false;
            }

            public AppaTask DisposeAsync()
            {
                TaskTracker.RemoveTracking(this);
                return default;
            }
        }
    }
}
