using System;
using System.Threading;
using Appalachia.Utility.Async.Internal;

namespace Appalachia.Utility.Async.Linq
{
    public static partial class AppaTaskAsyncEnumerable
    {
        public static AppaTask<TSource[]> ToArrayAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return ToArray.ToArrayAsync(source, cancellationToken);
        }
    }

    internal static class ToArray
    {
        internal static async AppaTask<TSource[]> ToArrayAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            CancellationToken cancellationToken)
        {
            var pool = ArrayPool<TSource>.Shared;
            var array = pool.Rent(16);

            TSource[] result = default;
            IAppaTaskAsyncEnumerator<TSource> e = default;
            try
            {
                e = source.GetAsyncEnumerator(cancellationToken);
                var i = 0;
                while (await e.MoveNextAsync())
                {
                    ArrayPoolUtil.EnsureCapacity(ref array, i, pool);
                    array[i++] = e.Current;
                }

                if (i == 0)
                {
                    result = Array.Empty<TSource>();
                }
                else
                {
                    result = new TSource[i];
                    Array.Copy(array, result, i);
                }
            }
            finally
            {
                pool.Return(array, !RuntimeHelpersAbstraction.IsWellKnownNoReferenceContainsType<TSource>());

                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return result;
        }
    }
}
