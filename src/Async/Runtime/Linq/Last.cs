using System;
using System.Threading;
using Appalachia.Utility.Async.Internal;

namespace Appalachia.Utility.Async.Linq
{
    public static partial class AppaTaskAsyncEnumerable
    {
        public static AppaTask<TSource> LastAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Last.LastAsync(source, cancellationToken, false);
        }

        public static AppaTask<TSource> LastAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, bool> predicate,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source,    nameof(source));
            Error.ThrowArgumentNullException(predicate, nameof(predicate));

            return Last.LastAsync(source, predicate, cancellationToken, false);
        }

        public static AppaTask<TSource> LastAwaitAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source,    nameof(source));
            Error.ThrowArgumentNullException(predicate, nameof(predicate));

            return Last.LastAwaitAsync(source, predicate, cancellationToken, false);
        }

        public static AppaTask<TSource> LastAwaitWithCancellationAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source,    nameof(source));
            Error.ThrowArgumentNullException(predicate, nameof(predicate));

            return Last.LastAwaitWithCancellationAsync(source, predicate, cancellationToken, false);
        }

        public static AppaTask<TSource> LastOrDefaultAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Last.LastAsync(source, cancellationToken, true);
        }

        public static AppaTask<TSource> LastOrDefaultAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, bool> predicate,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source,    nameof(source));
            Error.ThrowArgumentNullException(predicate, nameof(predicate));

            return Last.LastAsync(source, predicate, cancellationToken, true);
        }

        public static AppaTask<TSource> LastOrDefaultAwaitAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source,    nameof(source));
            Error.ThrowArgumentNullException(predicate, nameof(predicate));

            return Last.LastAwaitAsync(source, predicate, cancellationToken, true);
        }

        public static AppaTask<TSource> LastOrDefaultAwaitWithCancellationAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source,    nameof(source));
            Error.ThrowArgumentNullException(predicate, nameof(predicate));

            return Last.LastAwaitWithCancellationAsync(source, predicate, cancellationToken, true);
        }
    }

    internal static class Last
    {
        public static async AppaTask<TSource> LastAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            CancellationToken cancellationToken,
            bool defaultIfEmpty)
        {
            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                TSource value = default;
                if (await e.MoveNextAsync())
                {
                    value = e.Current;
                }
                else
                {
                    if (defaultIfEmpty)
                    {
                        return value;
                    }
                    else
                    {
                        throw Error.NoElements();
                    }
                }

                while (await e.MoveNextAsync())
                {
                    value = e.Current;
                }

                return value;
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }
        }

        public static async AppaTask<TSource> LastAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, bool> predicate,
            CancellationToken cancellationToken,
            bool defaultIfEmpty)
        {
            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                TSource value = default;

                var found = false;
                while (await e.MoveNextAsync())
                {
                    var v = e.Current;
                    if (predicate(v))
                    {
                        found = true;
                        value = v;
                    }
                }

                if (defaultIfEmpty)
                {
                    return value;
                }
                else
                {
                    if (found)
                    {
                        return value;
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

        public static async AppaTask<TSource> LastAwaitAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<bool>> predicate,
            CancellationToken cancellationToken,
            bool defaultIfEmpty)
        {
            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                TSource value = default;

                var found = false;
                while (await e.MoveNextAsync())
                {
                    var v = e.Current;
                    if (await predicate(v))
                    {
                        found = true;
                        value = v;
                    }
                }

                if (defaultIfEmpty)
                {
                    return value;
                }
                else
                {
                    if (found)
                    {
                        return value;
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

        public static async AppaTask<TSource> LastAwaitWithCancellationAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<bool>> predicate,
            CancellationToken cancellationToken,
            bool defaultIfEmpty)
        {
            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                TSource value = default;

                var found = false;
                while (await e.MoveNextAsync())
                {
                    var v = e.Current;
                    if (await predicate(v, cancellationToken))
                    {
                        found = true;
                        value = v;
                    }
                }

                if (defaultIfEmpty)
                {
                    return value;
                }
                else
                {
                    if (found)
                    {
                        return value;
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
    }
}
