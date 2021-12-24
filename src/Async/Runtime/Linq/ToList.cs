using System.Collections.Generic;
using System.Threading;
using Appalachia.Utility.Async.Internal;

namespace Appalachia.Utility.Async.Linq
{
    public static partial class AppaTaskAsyncEnumerable
    {
        public static AppaTask<List<TSource>> ToListAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return ToList.ToListAsync(source, cancellationToken);
        }
    }

    internal static class ToList
    {
        internal static async AppaTask<List<TSource>> ToListAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            CancellationToken cancellationToken)
        {
            var list = new List<TSource>();

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    list.Add(e.Current);
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return list;
        }
    }
}
