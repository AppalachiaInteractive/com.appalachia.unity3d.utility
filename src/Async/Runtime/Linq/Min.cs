using System;
using System.Collections.Generic;
using System.Threading;
using Appalachia.Utility.Async.Internal;

namespace Appalachia.Utility.Async.Linq
{
    public static partial class AppaTaskAsyncEnumerable
    {
        public static AppaTask<TSource> MinAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Min.MinAsync(source, cancellationToken);
        }

        public static AppaTask<TResult> MinAsync<TSource, TResult>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, TResult> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Min.MinAsync(source, selector, cancellationToken);
        }

        public static AppaTask<TResult> MinAwaitAsync<TSource, TResult>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<TResult>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Min.MinAwaitAsync(source, selector, cancellationToken);
        }

        public static AppaTask<TResult> MinAwaitWithCancellationAsync<TSource, TResult>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<TResult>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Min.MinAwaitWithCancellationAsync(source, selector, cancellationToken);
        }
    }

    internal static partial class Min
    {
        public static async AppaTask<TSource> MinAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            CancellationToken cancellationToken)
        {
            TSource value = default;
            var comparer = Comparer<TSource>.Default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = e.Current;

                    goto NEXT_LOOP;
                }

                return value;

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = e.Current;
                    if (comparer.Compare(value, x) > 0)
                    {
                        value = x;
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

            return value;
        }

        public static async AppaTask<TResult> MinAsync<TSource, TResult>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, TResult> selector,
            CancellationToken cancellationToken)
        {
            TResult value = default;
            var comparer = Comparer<TResult>.Default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = selector(e.Current);

                    goto NEXT_LOOP;
                }

                return value;

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = selector(e.Current);
                    if (comparer.Compare(value, x) > 0)
                    {
                        value = x;
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

            return value;
        }

        public static async AppaTask<TResult> MinAwaitAsync<TSource, TResult>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<TResult>> selector,
            CancellationToken cancellationToken)
        {
            TResult value = default;
            var comparer = Comparer<TResult>.Default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current);

                    goto NEXT_LOOP;
                }

                return value;

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current);
                    if (comparer.Compare(value, x) > 0)
                    {
                        value = x;
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

            return value;
        }

        public static async AppaTask<TResult> MinAwaitWithCancellationAsync<TSource, TResult>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<TResult>> selector,
            CancellationToken cancellationToken)
        {
            TResult value = default;
            var comparer = Comparer<TResult>.Default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current, cancellationToken);

                    goto NEXT_LOOP;
                }

                return value;

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current, cancellationToken);
                    if (comparer.Compare(value, x) > 0)
                    {
                        value = x;
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

            return value;
        }
    }
}
