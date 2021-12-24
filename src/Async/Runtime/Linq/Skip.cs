using System.Threading;
using Appalachia.Utility.Async.Internal;

namespace Appalachia.Utility.Async.Linq
{
    public static partial class AppaTaskAsyncEnumerable
    {
        public static IAppaTaskAsyncEnumerable<TSource> Skip<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            int count)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return new Skip<TSource>(source, count);
        }
    }

    internal sealed class Skip<TSource> : IAppaTaskAsyncEnumerable<TSource>
    {
        private readonly IAppaTaskAsyncEnumerable<TSource> source;
        private readonly int count;

        public Skip(IAppaTaskAsyncEnumerable<TSource> source, int count)
        {
            this.source = source;
            this.count = count;
        }

        public IAppaTaskAsyncEnumerator<TSource> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            return new _Skip(source, count, cancellationToken);
        }

        private sealed class _Skip : AsyncEnumeratorBase<TSource, TSource>
        {
            private readonly int count;

            private int index;

            public _Skip(
                IAppaTaskAsyncEnumerable<TSource> source,
                int count,
                CancellationToken cancellationToken) : base(source, cancellationToken)
            {
                this.count = count;
            }

            protected override bool TryMoveNextCore(bool sourceHasCurrent, out bool result)
            {
                if (sourceHasCurrent)
                {
                    if (count <= checked(index++))
                    {
                        Current = SourceCurrent;
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
    }
}
