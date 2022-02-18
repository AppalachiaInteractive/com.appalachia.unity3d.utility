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
        public Cast(IAppaTaskAsyncEnumerable<object> source)
        {
            this.source = source;
        }

        #region Fields and Autoproperties

        private readonly IAppaTaskAsyncEnumerable<object> source;

        #endregion

        #region IAppaTaskAsyncEnumerable<TResult> Members

        public IAppaTaskAsyncEnumerator<TResult> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            return new _Cast(source, cancellationToken);
        }

        #endregion

        #region Nested type: _Cast

        private class _Cast : AsyncEnumeratorBase<object, TResult>
        {
            public _Cast(IAppaTaskAsyncEnumerable<object> source, CancellationToken cancellationToken) : base(
                source,
                cancellationToken
            )
            {
            }

            /// <inheritdoc />
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

        #endregion
    }
}
