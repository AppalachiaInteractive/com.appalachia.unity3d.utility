using System.Threading;

namespace Appalachia.Utility.Async.Linq
{
    public static partial class AppaTaskAsyncEnumerable
    {
        public static IAppaTaskAsyncEnumerable<TValue> Return<TValue>(TValue value)
        {
            return new Return<TValue>(value);
        }
    }

    internal class Return<TValue> : IAppaTaskAsyncEnumerable<TValue>
    {
        private readonly TValue value;

        public Return(TValue value)
        {
            this.value = value;
        }

        public IAppaTaskAsyncEnumerator<TValue> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            return new _Return(value, cancellationToken);
        }

        private class _Return : IAppaTaskAsyncEnumerator<TValue>
        {
            private readonly TValue value;
            private CancellationToken cancellationToken;

            private bool called;

            public _Return(TValue value, CancellationToken cancellationToken)
            {
                this.value = value;
                this.cancellationToken = cancellationToken;
                called = false;
            }

            public TValue Current => value;

            public AppaTask<bool> MoveNextAsync()
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (!called)
                {
                    called = true;
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
