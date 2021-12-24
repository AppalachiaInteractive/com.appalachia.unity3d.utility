using System.Collections.Generic;
using System.Threading;
using Appalachia.Utility.Async.Internal;

namespace Appalachia.Utility.Async.Linq
{
    public static partial class AppaTaskAsyncEnumerable
    {
        public static AppaTask<HashSet<TSource>> ToHashSetAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return ToHashSet.ToHashSetAsync(source, EqualityComparer<TSource>.Default, cancellationToken);
        }

        public static AppaTask<HashSet<TSource>> ToHashSetAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            IEqualityComparer<TSource> comparer,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source,   nameof(source));
            Error.ThrowArgumentNullException(comparer, nameof(comparer));

            return ToHashSet.ToHashSetAsync(source, comparer, cancellationToken);
        }
    }

    internal static class ToHashSet
    {
        internal static async AppaTask<HashSet<TSource>> ToHashSetAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            IEqualityComparer<TSource> comparer,
            CancellationToken cancellationToken)
        {
            var set = new HashSet<TSource>(comparer);

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    set.Add(e.Current);
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return set;
        }
    }
}
