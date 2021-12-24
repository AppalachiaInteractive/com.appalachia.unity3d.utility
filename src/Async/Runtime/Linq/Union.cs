using System.Collections.Generic;
using Appalachia.Utility.Async.Internal;

namespace Appalachia.Utility.Async.Linq
{
    public static partial class AppaTaskAsyncEnumerable
    {
        public static IAppaTaskAsyncEnumerable<TSource> Union<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> first,
            IAppaTaskAsyncEnumerable<TSource> second)
        {
            Error.ThrowArgumentNullException(first,  nameof(first));
            Error.ThrowArgumentNullException(second, nameof(second));

            return Union(first, second, EqualityComparer<TSource>.Default);
        }

        public static IAppaTaskAsyncEnumerable<TSource> Union<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> first,
            IAppaTaskAsyncEnumerable<TSource> second,
            IEqualityComparer<TSource> comparer)
        {
            Error.ThrowArgumentNullException(first,    nameof(first));
            Error.ThrowArgumentNullException(second,   nameof(second));
            Error.ThrowArgumentNullException(comparer, nameof(comparer));

            // improv without combinate?
            return first.Concat(second).Distinct(comparer);
        }
    }
}
