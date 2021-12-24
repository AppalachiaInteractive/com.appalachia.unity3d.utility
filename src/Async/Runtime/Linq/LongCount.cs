using System;
using System.Threading;
using Appalachia.Utility.Async.Internal;

namespace Appalachia.Utility.Async.Linq
{
    public static partial class AppaTaskAsyncEnumerable
    {
        public static AppaTask<long> LongCountAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return LongCount.LongCountAsync(source, cancellationToken);
        }

        public static AppaTask<long> LongCountAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, bool> predicate,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source,    nameof(source));
            Error.ThrowArgumentNullException(predicate, nameof(predicate));

            return LongCount.LongCountAsync(source, predicate, cancellationToken);
        }

        public static AppaTask<long> LongCountAwaitAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source,    nameof(source));
            Error.ThrowArgumentNullException(predicate, nameof(predicate));

            return LongCount.LongCountAwaitAsync(source, predicate, cancellationToken);
        }

        public static AppaTask<long> LongCountAwaitWithCancellationAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source,    nameof(source));
            Error.ThrowArgumentNullException(predicate, nameof(predicate));

            return LongCount.LongCountAwaitWithCancellationAsync(source, predicate, cancellationToken);
        }
    }

    internal static class LongCount
    {
        internal static async AppaTask<long> LongCountAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            CancellationToken cancellationToken)
        {
            long count = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    checked
                    {
                        count++;
                    }
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return count;
        }

        internal static async AppaTask<long> LongCountAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, bool> predicate,
            CancellationToken cancellationToken)
        {
            long count = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    if (predicate(e.Current))
                    {
                        checked
                        {
                            count++;
                        }
                    }
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return count;
        }

        internal static async AppaTask<long> LongCountAwaitAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<bool>> predicate,
            CancellationToken cancellationToken)
        {
            long count = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    if (await predicate(e.Current))
                    {
                        checked
                        {
                            count++;
                        }
                    }
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return count;
        }

        internal static async AppaTask<long> LongCountAwaitWithCancellationAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<bool>> predicate,
            CancellationToken cancellationToken)
        {
            long count = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    if (await predicate(e.Current, cancellationToken))
                    {
                        checked
                        {
                            count++;
                        }
                    }
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return count;
        }
    }
}
