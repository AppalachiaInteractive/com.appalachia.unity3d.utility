using System.Threading;

namespace Appalachia.Utility.Async.Linq
{
    public static partial class AppaTaskAsyncEnumerable
    {
        public static IAppaTaskAsyncEnumerable<T> Never<T>()
        {
            return Linq.Never<T>.Instance;
        }
    }

    internal class Never<T> : IAppaTaskAsyncEnumerable<T>
    {
        public static readonly IAppaTaskAsyncEnumerable<T> Instance = new Never<T>();

        private Never()
        {
        }

        public IAppaTaskAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _Never(cancellationToken);
        }

        private class _Never : IAppaTaskAsyncEnumerator<T>
        {
            private CancellationToken cancellationToken;

            public _Never(CancellationToken cancellationToken)
            {
                this.cancellationToken = cancellationToken;
            }

            public T Current => default;

            public AppaTask<bool> MoveNextAsync()
            {
                var tcs = new AppaTaskCompletionSource<bool>();

                cancellationToken.Register(
                    state =>
                    {
                        var task = (AppaTaskCompletionSource<bool>)state;
                        task.TrySetCanceled(cancellationToken);
                    },
                    tcs
                );

                return tcs.Task;
            }

            public AppaTask DisposeAsync()
            {
                return default;
            }
        }
    }
}
