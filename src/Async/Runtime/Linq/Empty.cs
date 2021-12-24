using System.Threading;

namespace Appalachia.Utility.Async.Linq
{
    public static partial class AppaTaskAsyncEnumerable
    {
        public static IAppaTaskAsyncEnumerable<T> Empty<T>()
        {
            return Linq.Empty<T>.Instance;
        }
    }

    internal class Empty<T> : IAppaTaskAsyncEnumerable<T>
    {
        public static readonly IAppaTaskAsyncEnumerable<T> Instance = new Empty<T>();

        private Empty()
        {
        }

        public IAppaTaskAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return _Empty.Instance;
        }

        private class _Empty : IAppaTaskAsyncEnumerator<T>
        {
            public static readonly IAppaTaskAsyncEnumerator<T> Instance = new _Empty();

            private _Empty()
            {
            }

            public T Current => default;

            public AppaTask<bool> MoveNextAsync()
            {
                return CompletedTasks.False;
            }

            public AppaTask DisposeAsync()
            {
                return default;
            }
        }
    }
}
