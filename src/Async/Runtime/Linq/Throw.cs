using System;
using System.Threading;

namespace Appalachia.Utility.Async.Linq
{
    public static partial class AppaTaskAsyncEnumerable
    {
        public static IAppaTaskAsyncEnumerable<TValue> Throw<TValue>(Exception exception)
        {
            return new Throw<TValue>(exception);
        }
    }

    internal class Throw<TValue> : IAppaTaskAsyncEnumerable<TValue>
    {
        private readonly Exception exception;

        public Throw(Exception exception)
        {
            this.exception = exception;
        }

        public IAppaTaskAsyncEnumerator<TValue> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            return new _Throw(exception, cancellationToken);
        }

        private class _Throw : IAppaTaskAsyncEnumerator<TValue>
        {
            private readonly Exception exception;
            private CancellationToken cancellationToken;

            public _Throw(Exception exception, CancellationToken cancellationToken)
            {
                this.exception = exception;
                this.cancellationToken = cancellationToken;
            }

            public TValue Current => default;

            public AppaTask<bool> MoveNextAsync()
            {
                cancellationToken.ThrowIfCancellationRequested();
                return AppaTask.FromException<bool>(exception);
            }

            public AppaTask DisposeAsync()
            {
                return default;
            }
        }
    }
}
