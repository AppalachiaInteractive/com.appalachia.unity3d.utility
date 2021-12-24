using System.Threading;
using Appalachia.Utility.Async.Internal;

namespace Appalachia.Utility.Async.Linq
{
    public static partial class AppaTaskAsyncEnumerable
    {
        public static IAppaTaskAsyncEnumerable<TResult> Cast<TResult>(
            this IAppaTaskAsyncEnumerable<object> source)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return new Cast<TResult>(source);
        }
    }

    internal sealed class Cast<TResult> : IAppaTaskAsyncEnumerable<TResult>
    {
        private readonly IAppaTaskAsyncEnumerable<object> source;

        public Cast(IAppaTaskAsyncEnumerable<object> source)
        {
            this.source = source;
        }

        public IAppaTaskAsyncEnumerator<TResult> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            return new _Cast(source, cancellationToken);
        }

        private class _Cast : AsyncEnumeratorBase<object, TResult>
        {
            public _Cast(IAppaTaskAsyncEnumerable<object> source, CancellationToken cancellationToken) : base(
                source,
                cancellationToken
            )
            {
            }

            protected override bool TryMoveNextCore(bool sourceHasCurrent, out bool result)
            {
                if (sourceHasCurrent)
                {
                    Current = (TResult)SourceCurrent;
                    result = true;
                    return true;
                }

                result = false;
                return true;
            }
        }
    }
}
