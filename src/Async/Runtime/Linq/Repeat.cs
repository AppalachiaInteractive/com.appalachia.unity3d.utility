using System.Threading;
using Appalachia.Utility.Async.Internal;

namespace Appalachia.Utility.Async.Linq
{
    public static partial class AppaTaskAsyncEnumerable
    {
        public static IAppaTaskAsyncEnumerable<TElement> Repeat<TElement>(TElement element, int count)
        {
            if (count < 0)
            {
                throw Error.ArgumentOutOfRange(nameof(count));
            }

            return new Repeat<TElement>(element, count);
        }
    }

    internal class Repeat<TElement> : IAppaTaskAsyncEnumerable<TElement>
    {
        private readonly TElement element;
        private readonly int count;

        public Repeat(TElement element, int count)
        {
            this.element = element;
            this.count = count;
        }

        public IAppaTaskAsyncEnumerator<TElement> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            return new _Repeat(element, count, cancellationToken);
        }

        private class _Repeat : IAppaTaskAsyncEnumerator<TElement>
        {
            private readonly TElement element;
            private readonly int count;
            private int remaining;
            private CancellationToken cancellationToken;

            public _Repeat(TElement element, int count, CancellationToken cancellationToken)
            {
                this.element = element;
                this.count = count;
                this.cancellationToken = cancellationToken;

                remaining = count;
            }

            public TElement Current => element;

            public AppaTask<bool> MoveNextAsync()
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (remaining-- != 0)
                {
                    return CompletedTasks.True;
                }

                return CompletedTasks.False;
            }

            public AppaTask DisposeAsync()
            {
                return default;
            }
        }
    }
}
