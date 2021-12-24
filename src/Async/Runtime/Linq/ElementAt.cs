using System.Threading;
using Appalachia.Utility.Async.Internal;

namespace Appalachia.Utility.Async.Linq
{
    public static partial class AppaTaskAsyncEnumerable
    {
        public static AppaTask<TSource> ElementAtAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            int index,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return ElementAt.ElementAtAsync(source, index, cancellationToken, false);
        }

        public static AppaTask<TSource> ElementAtOrDefaultAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            int index,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return ElementAt.ElementAtAsync(source, index, cancellationToken, true);
        }
    }

    internal static class ElementAt
    {
        public static async AppaTask<TSource> ElementAtAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            int index,
            CancellationToken cancellationToken,
            bool defaultIfEmpty)
        {
            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                var i = 0;
                while (await e.MoveNextAsync())
                {
                    if (i++ == index)
                    {
                        return e.Current;
                    }
                }

                if (defaultIfEmpty)
                {
                    return default;
                }
                else
                {
                    throw Error.ArgumentOutOfRange(nameof(index));
                }
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
