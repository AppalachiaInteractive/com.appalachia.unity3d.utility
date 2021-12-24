using System;
using System.Threading;
using Appalachia.Utility.Async.Internal;

namespace Appalachia.Utility.Async.Linq
{
    public static partial class AppaTaskAsyncEnumerable
    {
        public static AppaTask<TSource> SingleAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return SingleOperator.SingleAsync(source, cancellationToken, false);
        }

        public static AppaTask<TSource> SingleAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, bool> predicate,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source,    nameof(source));
            Error.ThrowArgumentNullException(predicate, nameof(predicate));

            return SingleOperator.SingleAsync(source, predicate, cancellationToken, false);
        }

        public static AppaTask<TSource> SingleAwaitAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source,    nameof(source));
            Error.ThrowArgumentNullException(predicate, nameof(predicate));

            return SingleOperator.SingleAwaitAsync(source, predicate, cancellationToken, false);
        }

        public static AppaTask<TSource> SingleAwaitWithCancellationAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source,    nameof(source));
            Error.ThrowArgumentNullException(predicate, nameof(predicate));

            return SingleOperator.SingleAwaitWithCancellationAsync(
                source,
                predicate,
                cancellationToken,
                false
            );
        }

        public static AppaTask<TSource> SingleOrDefaultAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return SingleOperator.SingleAsync(source, cancellationToken, true);
        }

        public static AppaTask<TSource> SingleOrDefaultAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, bool> predicate,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source,    nameof(source));
            Error.ThrowArgumentNullException(predicate, nameof(predicate));

            return SingleOperator.SingleAsync(source, predicate, cancellationToken, true);
        }

        public static AppaTask<TSource> SingleOrDefaultAwaitAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source,    nameof(source));
            Error.ThrowArgumentNullException(predicate, nameof(predicate));

            return SingleOperator.SingleAwaitAsync(source, predicate, cancellationToken, true);
        }

        public static AppaTask<TSource> SingleOrDefaultAwaitWithCancellationAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source,    nameof(source));
            Error.ThrowArgumentNullException(predicate, nameof(predicate));

            return SingleOperator.SingleAwaitWithCancellationAsync(
                source,
                predicate,
                cancellationToken,
                true
            );
        }
    }

    internal static class SingleOperator
    {
        public static async AppaTask<TSource> SingleAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            CancellationToken cancellationToken,
            bool defaultIfEmpty)
        {
            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                if (await e.MoveNextAsync())
                {
                    var v = e.Current;
                    if (!await e.MoveNextAsync())
                    {
                        return v;
                    }

                    throw Error.MoreThanOneElement();
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

        public static async AppaTask<TSource> SingleAsync<TSource>(
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
                        if (found)
                        {
                            throw Error.MoreThanOneElement();
                        }
                        else
                        {
                            found = true;
                            value = v;
                        }
                    }
                }

                if (found || defaultIfEmpty)
                {
                    return value;
                }

                throw Error.NoElements();
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }
        }

        public static async AppaTask<TSource> SingleAwaitAsync<TSource>(
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
                        if (found)
                        {
                            throw Error.MoreThanOneElement();
                        }
                        else
                        {
                            found = true;
                            value = v;
                        }
                    }
                }

                if (found || defaultIfEmpty)
                {
                    return value;
                }

                throw Error.NoElements();
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }
        }

        public static async AppaTask<TSource> SingleAwaitWithCancellationAsync<TSource>(
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
                        if (found)
                        {
                            throw Error.MoreThanOneElement();
                        }
                        else
                        {
                            found = true;
                            value = v;
                        }
                    }
                }

                if (found || defaultIfEmpty)
                {
                    return value;
                }

                throw Error.NoElements();
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
