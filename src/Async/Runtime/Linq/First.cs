using System;
using System.Threading;
using Appalachia.Utility.Async.Internal;

namespace Appalachia.Utility.Async.Linq
{
    public static partial class AppaTaskAsyncEnumerable
    {
        public static AppaTask<TSource> FirstAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return First.FirstAsync(source, cancellationToken, false);
        }

        public static AppaTask<TSource> FirstAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, bool> predicate,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source,    nameof(source));
            Error.ThrowArgumentNullException(predicate, nameof(predicate));

            return First.FirstAsync(source, predicate, cancellationToken, false);
        }

        public static AppaTask<TSource> FirstAwaitAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source,    nameof(source));
            Error.ThrowArgumentNullException(predicate, nameof(predicate));

            return First.FirstAwaitAsync(source, predicate, cancellationToken, false);
        }

        public static AppaTask<TSource> FirstAwaitWithCancellationAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source,    nameof(source));
            Error.ThrowArgumentNullException(predicate, nameof(predicate));

            return First.FirstAwaitWithCancellationAsync(source, predicate, cancellationToken, false);
        }

        public static AppaTask<TSource> FirstOrDefaultAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return First.FirstAsync(source, cancellationToken, true);
        }

        public static AppaTask<TSource> FirstOrDefaultAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, bool> predicate,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source,    nameof(source));
            Error.ThrowArgumentNullException(predicate, nameof(predicate));

            return First.FirstAsync(source, predicate, cancellationToken, true);
        }

        public static AppaTask<TSource> FirstOrDefaultAwaitAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source,    nameof(source));
            Error.ThrowArgumentNullException(predicate, nameof(predicate));

            return First.FirstAwaitAsync(source, predicate, cancellationToken, true);
        }

        public static AppaTask<TSource> FirstOrDefaultAwaitWithCancellationAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source,    nameof(source));
            Error.ThrowArgumentNullException(predicate, nameof(predicate));

            return First.FirstAwaitWithCancellationAsync(source, predicate, cancellationToken, true);
        }
    }

    internal static class First
    {
        public static async AppaTask<TSource> FirstAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            CancellationToken cancellationToken,
            bool defaultIfEmpty)
        {
            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                if (await e.MoveNextAsync())
                {
                    return e.Current;
                }
                else
                {
                    if (defaultIfEmpty)
                    {
                        return default;
                    }
                    else
                    {
                        throw Error.NoElements();
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
        }

        public static async AppaTask<TSource> FirstAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, bool> predicate,
            CancellationToken cancellationToken,
            bool defaultIfEmpty)
        {
            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    var v = e.Current;
                    if (predicate(v))
                    {
                        return v;
                    }
                }

                if (defaultIfEmpty)
                {
                    return default;
                }
                else
                {
                    throw Error.NoElements();
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

        public static async AppaTask<TSource> FirstAwaitAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<bool>> predicate,
            CancellationToken cancellationToken,
            bool defaultIfEmpty)
        {
            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    var v = e.Current;
                    if (await predicate(v))
                    {
                        return v;
                    }
                }

                if (defaultIfEmpty)
                {
                    return default;
                }
                else
                {
                    throw Error.NoElements();
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

        public static async AppaTask<TSource> FirstAwaitWithCancellationAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<bool>> predicate,
            CancellationToken cancellationToken,
            bool defaultIfEmpty)
        {
            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    var v = e.Current;
                    if (await predicate(v, cancellationToken))
                    {
                        return v;
                    }
                }

                if (defaultIfEmpty)
                {
                    return default;
                }
                else
                {
                    throw Error.NoElements();
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
