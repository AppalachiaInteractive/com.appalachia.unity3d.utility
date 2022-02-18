using System.Threading;
using Appalachia.Utility.Async.Internal;

namespace Appalachia.Utility.Async.Linq
{
    public static partial class AppaTaskAsyncEnumerable
    {
        public static IAppaTaskAsyncEnumerable<TResult> OfType<TResult>(
            this IAppaTaskAsyncEnumerable<object> source)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return new OfType<TResult>(source);
        }
    }

    internal sealed class OfType<TResult> : IAppaTaskAsyncEnumerable<TResult>
    {
        public OfType(IAppaTaskAsyncEnumerable<object> source)
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
            return new _OfType(source, cancellationToken);
        }

        #endregion

        #region Nested type: _OfType

        private class _OfType : AsyncEnumeratorBase<object, TResult>
        {
            public _OfType(IAppaTaskAsyncEnumerable<object> source, CancellationToken cancellationToken)
                : base(source, cancellationToken)
            {
            }

            /// <inheritdoc />
            protected override bool TryMoveNextCore(bool sourceHasCurrent, out bool result)
            {
                if (sourceHasCurrent)
                {
                    if (SourceCurrent is TResult castCurent)
                    {
                        Current = castCurent;
                        result = true;
                        return true;
                    }

                    result = default;
                    return false;
                }

                result = false;
                return true;
            }
        }

        #endregion
    }
}
