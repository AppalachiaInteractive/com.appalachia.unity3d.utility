using System.Collections.Generic;
using System.Threading;
using Appalachia.Utility.Async.Internal;

namespace Appalachia.Utility.Async.Linq
{
    public static partial class AppaTaskAsyncEnumerable
    {
        public static AppaTask<bool> ContainsAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            TSource value,
            CancellationToken cancellationToken = default)
        {
            return ContainsAsync(source, value, EqualityComparer<TSource>.Default, cancellationToken);
        }

        public static AppaTask<bool> ContainsAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            TSource value,
            IEqualityComparer<TSource> comparer,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source,   nameof(source));
            Error.ThrowArgumentNullException(comparer, nameof(comparer));

            return Contains.ContainsAsync(source, value, comparer, cancellationToken);
        }
    }

    internal static class Contains
    {
        internal static async AppaTask<bool> ContainsAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            TSource value,
            IEqualityComparer<TSource> comparer,
            CancellationToken cancellationToken)
        {
            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    if (comparer.Equals(value, e.Current))
                    {
                        return true;
                    }
                }

                return false;
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }
        }
    }
}
