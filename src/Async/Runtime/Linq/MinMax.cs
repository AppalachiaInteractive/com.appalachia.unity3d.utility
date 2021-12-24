using System;
using System.Threading;
using Appalachia.Utility.Async.Internal;

namespace Appalachia.Utility.Async.Linq
{
    public static partial class AppaTaskAsyncEnumerable
    {
        public static AppaTask<int> MinAsync(
            this IAppaTaskAsyncEnumerable<int> source,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Min.MinAsync(source, cancellationToken);
        }

        public static AppaTask<int> MinAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, int> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Min.MinAsync(source, selector, cancellationToken);
        }

        public static AppaTask<int> MinAwaitAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<int>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Min.MinAwaitAsync(source, selector, cancellationToken);
        }

        public static AppaTask<int> MinAwaitWithCancellationAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<int>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Min.MinAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static AppaTask<long> MinAsync(
            this IAppaTaskAsyncEnumerable<long> source,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Min.MinAsync(source, cancellationToken);
        }

        public static AppaTask<long> MinAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, long> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Min.MinAsync(source, selector, cancellationToken);
        }

        public static AppaTask<long> MinAwaitAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<long>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Min.MinAwaitAsync(source, selector, cancellationToken);
        }

        public static AppaTask<long> MinAwaitWithCancellationAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<long>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Min.MinAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static AppaTask<float> MinAsync(
            this IAppaTaskAsyncEnumerable<float> source,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Min.MinAsync(source, cancellationToken);
        }

        public static AppaTask<float> MinAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, float> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Min.MinAsync(source, selector, cancellationToken);
        }

        public static AppaTask<float> MinAwaitAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<float>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Min.MinAwaitAsync(source, selector, cancellationToken);
        }

        public static AppaTask<float> MinAwaitWithCancellationAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<float>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Min.MinAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static AppaTask<double> MinAsync(
            this IAppaTaskAsyncEnumerable<double> source,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Min.MinAsync(source, cancellationToken);
        }

        public static AppaTask<double> MinAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, double> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Min.MinAsync(source, selector, cancellationToken);
        }

        public static AppaTask<double> MinAwaitAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<double>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Min.MinAwaitAsync(source, selector, cancellationToken);
        }

        public static AppaTask<double> MinAwaitWithCancellationAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<double>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Min.MinAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static AppaTask<decimal> MinAsync(
            this IAppaTaskAsyncEnumerable<decimal> source,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Min.MinAsync(source, cancellationToken);
        }

        public static AppaTask<decimal> MinAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, decimal> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Min.MinAsync(source, selector, cancellationToken);
        }

        public static AppaTask<decimal> MinAwaitAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<decimal>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Min.MinAwaitAsync(source, selector, cancellationToken);
        }

        public static AppaTask<decimal> MinAwaitWithCancellationAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<decimal>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Min.MinAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static AppaTask<int?> MinAsync(
            this IAppaTaskAsyncEnumerable<int?> source,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Min.MinAsync(source, cancellationToken);
        }

        public static AppaTask<int?> MinAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, int?> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Min.MinAsync(source, selector, cancellationToken);
        }

        public static AppaTask<int?> MinAwaitAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<int?>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Min.MinAwaitAsync(source, selector, cancellationToken);
        }

        public static AppaTask<int?> MinAwaitWithCancellationAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<int?>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Min.MinAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static AppaTask<long?> MinAsync(
            this IAppaTaskAsyncEnumerable<long?> source,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Min.MinAsync(source, cancellationToken);
        }

        public static AppaTask<long?> MinAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, long?> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Min.MinAsync(source, selector, cancellationToken);
        }

        public static AppaTask<long?> MinAwaitAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<long?>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Min.MinAwaitAsync(source, selector, cancellationToken);
        }

        public static AppaTask<long?> MinAwaitWithCancellationAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<long?>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Min.MinAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static AppaTask<float?> MinAsync(
            this IAppaTaskAsyncEnumerable<float?> source,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Min.MinAsync(source, cancellationToken);
        }

        public static AppaTask<float?> MinAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, float?> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Min.MinAsync(source, selector, cancellationToken);
        }

        public static AppaTask<float?> MinAwaitAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<float?>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Min.MinAwaitAsync(source, selector, cancellationToken);
        }

        public static AppaTask<float?> MinAwaitWithCancellationAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<float?>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Min.MinAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static AppaTask<double?> MinAsync(
            this IAppaTaskAsyncEnumerable<double?> source,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Min.MinAsync(source, cancellationToken);
        }

        public static AppaTask<double?> MinAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, double?> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Min.MinAsync(source, selector, cancellationToken);
        }

        public static AppaTask<double?> MinAwaitAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<double?>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Min.MinAwaitAsync(source, selector, cancellationToken);
        }

        public static AppaTask<double?> MinAwaitWithCancellationAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<double?>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Min.MinAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static AppaTask<decimal?> MinAsync(
            this IAppaTaskAsyncEnumerable<decimal?> source,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Min.MinAsync(source, cancellationToken);
        }

        public static AppaTask<decimal?> MinAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, decimal?> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Min.MinAsync(source, selector, cancellationToken);
        }

        public static AppaTask<decimal?> MinAwaitAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<decimal?>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Min.MinAwaitAsync(source, selector, cancellationToken);
        }

        public static AppaTask<decimal?> MinAwaitWithCancellationAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<decimal?>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Min.MinAwaitWithCancellationAsync(source, selector, cancellationToken);
        }
    }

    internal static partial class Min
    {
        public static async AppaTask<int> MinAsync(
            IAppaTaskAsyncEnumerable<int> source,
            CancellationToken cancellationToken)
        {
            int value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = e.Current;

                    goto NEXT_LOOP;
                }

                throw Error.NoElements();

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = e.Current;
                    if (value > x)
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

        public static async AppaTask<int> MinAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, int> selector,
            CancellationToken cancellationToken)
        {
            int value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = selector(e.Current);

                    goto NEXT_LOOP;
                }

                throw Error.NoElements();

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = selector(e.Current);
                    if (value > x)
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

        public static async AppaTask<int> MinAwaitAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<int>> selector,
            CancellationToken cancellationToken)
        {
            int value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current);

                    goto NEXT_LOOP;
                }

                throw Error.NoElements();

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current);
                    if (value > x)
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

        public static async AppaTask<int> MinAwaitWithCancellationAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<int>> selector,
            CancellationToken cancellationToken)
        {
            int value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current, cancellationToken);

                    goto NEXT_LOOP;
                }

                throw Error.NoElements();

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current, cancellationToken);
                    if (value > x)
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

        public static async AppaTask<long> MinAsync(
            IAppaTaskAsyncEnumerable<long> source,
            CancellationToken cancellationToken)
        {
            long value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = e.Current;

                    goto NEXT_LOOP;
                }

                throw Error.NoElements();

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = e.Current;
                    if (value > x)
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

        public static async AppaTask<long> MinAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, long> selector,
            CancellationToken cancellationToken)
        {
            long value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = selector(e.Current);

                    goto NEXT_LOOP;
                }

                throw Error.NoElements();

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = selector(e.Current);
                    if (value > x)
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

        public static async AppaTask<long> MinAwaitAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<long>> selector,
            CancellationToken cancellationToken)
        {
            long value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current);

                    goto NEXT_LOOP;
                }

                throw Error.NoElements();

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current);
                    if (value > x)
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

        public static async AppaTask<long> MinAwaitWithCancellationAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<long>> selector,
            CancellationToken cancellationToken)
        {
            long value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current, cancellationToken);

                    goto NEXT_LOOP;
                }

                throw Error.NoElements();

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current, cancellationToken);
                    if (value > x)
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

        public static async AppaTask<float> MinAsync(
            IAppaTaskAsyncEnumerable<float> source,
            CancellationToken cancellationToken)
        {
            float value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = e.Current;

                    goto NEXT_LOOP;
                }

                throw Error.NoElements();

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = e.Current;
                    if (value > x)
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

        public static async AppaTask<float> MinAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, float> selector,
            CancellationToken cancellationToken)
        {
            float value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = selector(e.Current);

                    goto NEXT_LOOP;
                }

                throw Error.NoElements();

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = selector(e.Current);
                    if (value > x)
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

        public static async AppaTask<float> MinAwaitAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<float>> selector,
            CancellationToken cancellationToken)
        {
            float value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current);

                    goto NEXT_LOOP;
                }

                throw Error.NoElements();

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current);
                    if (value > x)
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

        public static async AppaTask<float> MinAwaitWithCancellationAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<float>> selector,
            CancellationToken cancellationToken)
        {
            float value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current, cancellationToken);

                    goto NEXT_LOOP;
                }

                throw Error.NoElements();

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current, cancellationToken);
                    if (value > x)
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

        public static async AppaTask<double> MinAsync(
            IAppaTaskAsyncEnumerable<double> source,
            CancellationToken cancellationToken)
        {
            double value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = e.Current;

                    goto NEXT_LOOP;
                }

                throw Error.NoElements();

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = e.Current;
                    if (value > x)
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

        public static async AppaTask<double> MinAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, double> selector,
            CancellationToken cancellationToken)
        {
            double value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = selector(e.Current);

                    goto NEXT_LOOP;
                }

                throw Error.NoElements();

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = selector(e.Current);
                    if (value > x)
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

        public static async AppaTask<double> MinAwaitAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<double>> selector,
            CancellationToken cancellationToken)
        {
            double value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current);

                    goto NEXT_LOOP;
                }

                throw Error.NoElements();

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current);
                    if (value > x)
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

        public static async AppaTask<double> MinAwaitWithCancellationAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<double>> selector,
            CancellationToken cancellationToken)
        {
            double value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current, cancellationToken);

                    goto NEXT_LOOP;
                }

                throw Error.NoElements();

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current, cancellationToken);
                    if (value > x)
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

        public static async AppaTask<decimal> MinAsync(
            IAppaTaskAsyncEnumerable<decimal> source,
            CancellationToken cancellationToken)
        {
            decimal value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = e.Current;

                    goto NEXT_LOOP;
                }

                throw Error.NoElements();

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = e.Current;
                    if (value > x)
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

        public static async AppaTask<decimal> MinAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, decimal> selector,
            CancellationToken cancellationToken)
        {
            decimal value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = selector(e.Current);

                    goto NEXT_LOOP;
                }

                throw Error.NoElements();

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = selector(e.Current);
                    if (value > x)
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

        public static async AppaTask<decimal> MinAwaitAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<decimal>> selector,
            CancellationToken cancellationToken)
        {
            decimal value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current);

                    goto NEXT_LOOP;
                }

                throw Error.NoElements();

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current);
                    if (value > x)
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

        public static async AppaTask<decimal> MinAwaitWithCancellationAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<decimal>> selector,
            CancellationToken cancellationToken)
        {
            decimal value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current, cancellationToken);

                    goto NEXT_LOOP;
                }

                throw Error.NoElements();

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current, cancellationToken);
                    if (value > x)
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

        public static async AppaTask<int?> MinAsync(
            IAppaTaskAsyncEnumerable<int?> source,
            CancellationToken cancellationToken)
        {
            int? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = e.Current;
                    if (value == null)
                    {
                        continue;
                    }

                    goto NEXT_LOOP;
                }

                return default;

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = e.Current;
                    if (x == null)
                    {
                        continue;
                    }

                    if (value > x)
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

        public static async AppaTask<int?> MinAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, int?> selector,
            CancellationToken cancellationToken)
        {
            int? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = selector(e.Current);
                    if (value == null)
                    {
                        continue;
                    }

                    goto NEXT_LOOP;
                }

                return default;

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = selector(e.Current);
                    if (x == null)
                    {
                        continue;
                    }

                    if (value > x)
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

        public static async AppaTask<int?> MinAwaitAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<int?>> selector,
            CancellationToken cancellationToken)
        {
            int? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current);
                    if (value == null)
                    {
                        continue;
                    }

                    goto NEXT_LOOP;
                }

                return default;

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current);
                    if (x == null)
                    {
                        continue;
                    }

                    if (value > x)
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

        public static async AppaTask<int?> MinAwaitWithCancellationAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<int?>> selector,
            CancellationToken cancellationToken)
        {
            int? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current, cancellationToken);
                    if (value == null)
                    {
                        continue;
                    }

                    goto NEXT_LOOP;
                }

                return default;

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current, cancellationToken);
                    if (x == null)
                    {
                        continue;
                    }

                    if (value > x)
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

        public static async AppaTask<long?> MinAsync(
            IAppaTaskAsyncEnumerable<long?> source,
            CancellationToken cancellationToken)
        {
            long? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = e.Current;
                    if (value == null)
                    {
                        continue;
                    }

                    goto NEXT_LOOP;
                }

                return default;

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = e.Current;
                    if (x == null)
                    {
                        continue;
                    }

                    if (value > x)
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

        public static async AppaTask<long?> MinAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, long?> selector,
            CancellationToken cancellationToken)
        {
            long? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = selector(e.Current);
                    if (value == null)
                    {
                        continue;
                    }

                    goto NEXT_LOOP;
                }

                return default;

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = selector(e.Current);
                    if (x == null)
                    {
                        continue;
                    }

                    if (value > x)
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

        public static async AppaTask<long?> MinAwaitAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<long?>> selector,
            CancellationToken cancellationToken)
        {
            long? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current);
                    if (value == null)
                    {
                        continue;
                    }

                    goto NEXT_LOOP;
                }

                return default;

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current);
                    if (x == null)
                    {
                        continue;
                    }

                    if (value > x)
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

        public static async AppaTask<long?> MinAwaitWithCancellationAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<long?>> selector,
            CancellationToken cancellationToken)
        {
            long? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current, cancellationToken);
                    if (value == null)
                    {
                        continue;
                    }

                    goto NEXT_LOOP;
                }

                return default;

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current, cancellationToken);
                    if (x == null)
                    {
                        continue;
                    }

                    if (value > x)
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

        public static async AppaTask<float?> MinAsync(
            IAppaTaskAsyncEnumerable<float?> source,
            CancellationToken cancellationToken)
        {
            float? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = e.Current;
                    if (value == null)
                    {
                        continue;
                    }

                    goto NEXT_LOOP;
                }

                return default;

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = e.Current;
                    if (x == null)
                    {
                        continue;
                    }

                    if (value > x)
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

        public static async AppaTask<float?> MinAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, float?> selector,
            CancellationToken cancellationToken)
        {
            float? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = selector(e.Current);
                    if (value == null)
                    {
                        continue;
                    }

                    goto NEXT_LOOP;
                }

                return default;

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = selector(e.Current);
                    if (x == null)
                    {
                        continue;
                    }

                    if (value > x)
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

        public static async AppaTask<float?> MinAwaitAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<float?>> selector,
            CancellationToken cancellationToken)
        {
            float? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current);
                    if (value == null)
                    {
                        continue;
                    }

                    goto NEXT_LOOP;
                }

                return default;

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current);
                    if (x == null)
                    {
                        continue;
                    }

                    if (value > x)
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

        public static async AppaTask<float?> MinAwaitWithCancellationAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<float?>> selector,
            CancellationToken cancellationToken)
        {
            float? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current, cancellationToken);
                    if (value == null)
                    {
                        continue;
                    }

                    goto NEXT_LOOP;
                }

                return default;

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current, cancellationToken);
                    if (x == null)
                    {
                        continue;
                    }

                    if (value > x)
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

        public static async AppaTask<double?> MinAsync(
            IAppaTaskAsyncEnumerable<double?> source,
            CancellationToken cancellationToken)
        {
            double? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = e.Current;
                    if (value == null)
                    {
                        continue;
                    }

                    goto NEXT_LOOP;
                }

                return default;

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = e.Current;
                    if (x == null)
                    {
                        continue;
                    }

                    if (value > x)
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

        public static async AppaTask<double?> MinAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, double?> selector,
            CancellationToken cancellationToken)
        {
            double? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = selector(e.Current);
                    if (value == null)
                    {
                        continue;
                    }

                    goto NEXT_LOOP;
                }

                return default;

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = selector(e.Current);
                    if (x == null)
                    {
                        continue;
                    }

                    if (value > x)
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

        public static async AppaTask<double?> MinAwaitAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<double?>> selector,
            CancellationToken cancellationToken)
        {
            double? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current);
                    if (value == null)
                    {
                        continue;
                    }

                    goto NEXT_LOOP;
                }

                return default;

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current);
                    if (x == null)
                    {
                        continue;
                    }

                    if (value > x)
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

        public static async AppaTask<double?> MinAwaitWithCancellationAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<double?>> selector,
            CancellationToken cancellationToken)
        {
            double? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current, cancellationToken);
                    if (value == null)
                    {
                        continue;
                    }

                    goto NEXT_LOOP;
                }

                return default;

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current, cancellationToken);
                    if (x == null)
                    {
                        continue;
                    }

                    if (value > x)
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

        public static async AppaTask<decimal?> MinAsync(
            IAppaTaskAsyncEnumerable<decimal?> source,
            CancellationToken cancellationToken)
        {
            decimal? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = e.Current;
                    if (value == null)
                    {
                        continue;
                    }

                    goto NEXT_LOOP;
                }

                return default;

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = e.Current;
                    if (x == null)
                    {
                        continue;
                    }

                    if (value > x)
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

        public static async AppaTask<decimal?> MinAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, decimal?> selector,
            CancellationToken cancellationToken)
        {
            decimal? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = selector(e.Current);
                    if (value == null)
                    {
                        continue;
                    }

                    goto NEXT_LOOP;
                }

                return default;

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = selector(e.Current);
                    if (x == null)
                    {
                        continue;
                    }

                    if (value > x)
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

        public static async AppaTask<decimal?> MinAwaitAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<decimal?>> selector,
            CancellationToken cancellationToken)
        {
            decimal? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current);
                    if (value == null)
                    {
                        continue;
                    }

                    goto NEXT_LOOP;
                }

                return default;

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current);
                    if (x == null)
                    {
                        continue;
                    }

                    if (value > x)
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

        public static async AppaTask<decimal?> MinAwaitWithCancellationAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<decimal?>> selector,
            CancellationToken cancellationToken)
        {
            decimal? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current, cancellationToken);
                    if (value == null)
                    {
                        continue;
                    }

                    goto NEXT_LOOP;
                }

                return default;

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current, cancellationToken);
                    if (x == null)
                    {
                        continue;
                    }

                    if (value > x)
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

    public static partial class AppaTaskAsyncEnumerable
    {
        public static AppaTask<int> MaxAsync(
            this IAppaTaskAsyncEnumerable<int> source,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Max.MaxAsync(source, cancellationToken);
        }

        public static AppaTask<int> MaxAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, int> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Max.MaxAsync(source, selector, cancellationToken);
        }

        public static AppaTask<int> MaxAwaitAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<int>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Max.MaxAwaitAsync(source, selector, cancellationToken);
        }

        public static AppaTask<int> MaxAwaitWithCancellationAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<int>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Max.MaxAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static AppaTask<long> MaxAsync(
            this IAppaTaskAsyncEnumerable<long> source,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Max.MaxAsync(source, cancellationToken);
        }

        public static AppaTask<long> MaxAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, long> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Max.MaxAsync(source, selector, cancellationToken);
        }

        public static AppaTask<long> MaxAwaitAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<long>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Max.MaxAwaitAsync(source, selector, cancellationToken);
        }

        public static AppaTask<long> MaxAwaitWithCancellationAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<long>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Max.MaxAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static AppaTask<float> MaxAsync(
            this IAppaTaskAsyncEnumerable<float> source,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Max.MaxAsync(source, cancellationToken);
        }

        public static AppaTask<float> MaxAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, float> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Max.MaxAsync(source, selector, cancellationToken);
        }

        public static AppaTask<float> MaxAwaitAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<float>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Max.MaxAwaitAsync(source, selector, cancellationToken);
        }

        public static AppaTask<float> MaxAwaitWithCancellationAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<float>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Max.MaxAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static AppaTask<double> MaxAsync(
            this IAppaTaskAsyncEnumerable<double> source,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Max.MaxAsync(source, cancellationToken);
        }

        public static AppaTask<double> MaxAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, double> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Max.MaxAsync(source, selector, cancellationToken);
        }

        public static AppaTask<double> MaxAwaitAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<double>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Max.MaxAwaitAsync(source, selector, cancellationToken);
        }

        public static AppaTask<double> MaxAwaitWithCancellationAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<double>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Max.MaxAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static AppaTask<decimal> MaxAsync(
            this IAppaTaskAsyncEnumerable<decimal> source,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Max.MaxAsync(source, cancellationToken);
        }

        public static AppaTask<decimal> MaxAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, decimal> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Max.MaxAsync(source, selector, cancellationToken);
        }

        public static AppaTask<decimal> MaxAwaitAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<decimal>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Max.MaxAwaitAsync(source, selector, cancellationToken);
        }

        public static AppaTask<decimal> MaxAwaitWithCancellationAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<decimal>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Max.MaxAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static AppaTask<int?> MaxAsync(
            this IAppaTaskAsyncEnumerable<int?> source,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Max.MaxAsync(source, cancellationToken);
        }

        public static AppaTask<int?> MaxAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, int?> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Max.MaxAsync(source, selector, cancellationToken);
        }

        public static AppaTask<int?> MaxAwaitAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<int?>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Max.MaxAwaitAsync(source, selector, cancellationToken);
        }

        public static AppaTask<int?> MaxAwaitWithCancellationAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<int?>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Max.MaxAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static AppaTask<long?> MaxAsync(
            this IAppaTaskAsyncEnumerable<long?> source,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Max.MaxAsync(source, cancellationToken);
        }

        public static AppaTask<long?> MaxAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, long?> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Max.MaxAsync(source, selector, cancellationToken);
        }

        public static AppaTask<long?> MaxAwaitAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<long?>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Max.MaxAwaitAsync(source, selector, cancellationToken);
        }

        public static AppaTask<long?> MaxAwaitWithCancellationAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<long?>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Max.MaxAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static AppaTask<float?> MaxAsync(
            this IAppaTaskAsyncEnumerable<float?> source,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Max.MaxAsync(source, cancellationToken);
        }

        public static AppaTask<float?> MaxAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, float?> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Max.MaxAsync(source, selector, cancellationToken);
        }

        public static AppaTask<float?> MaxAwaitAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<float?>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Max.MaxAwaitAsync(source, selector, cancellationToken);
        }

        public static AppaTask<float?> MaxAwaitWithCancellationAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<float?>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Max.MaxAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static AppaTask<double?> MaxAsync(
            this IAppaTaskAsyncEnumerable<double?> source,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Max.MaxAsync(source, cancellationToken);
        }

        public static AppaTask<double?> MaxAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, double?> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Max.MaxAsync(source, selector, cancellationToken);
        }

        public static AppaTask<double?> MaxAwaitAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<double?>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Max.MaxAwaitAsync(source, selector, cancellationToken);
        }

        public static AppaTask<double?> MaxAwaitWithCancellationAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<double?>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Max.MaxAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static AppaTask<decimal?> MaxAsync(
            this IAppaTaskAsyncEnumerable<decimal?> source,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Max.MaxAsync(source, cancellationToken);
        }

        public static AppaTask<decimal?> MaxAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, decimal?> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Max.MaxAsync(source, selector, cancellationToken);
        }

        public static AppaTask<decimal?> MaxAwaitAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<decimal?>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Max.MaxAwaitAsync(source, selector, cancellationToken);
        }

        public static AppaTask<decimal?> MaxAwaitWithCancellationAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<decimal?>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Max.MaxAwaitWithCancellationAsync(source, selector, cancellationToken);
        }
    }

    internal static partial class Max
    {
        public static async AppaTask<int> MaxAsync(
            IAppaTaskAsyncEnumerable<int> source,
            CancellationToken cancellationToken)
        {
            int value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = e.Current;

                    goto NEXT_LOOP;
                }

                throw Error.NoElements();

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = e.Current;
                    if (value < x)
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

        public static async AppaTask<int> MaxAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, int> selector,
            CancellationToken cancellationToken)
        {
            int value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = selector(e.Current);

                    goto NEXT_LOOP;
                }

                throw Error.NoElements();

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = selector(e.Current);
                    if (value < x)
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

        public static async AppaTask<int> MaxAwaitAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<int>> selector,
            CancellationToken cancellationToken)
        {
            int value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current);

                    goto NEXT_LOOP;
                }

                throw Error.NoElements();

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current);
                    if (value < x)
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

        public static async AppaTask<int> MaxAwaitWithCancellationAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<int>> selector,
            CancellationToken cancellationToken)
        {
            int value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current, cancellationToken);

                    goto NEXT_LOOP;
                }

                throw Error.NoElements();

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current, cancellationToken);
                    if (value < x)
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

        public static async AppaTask<long> MaxAsync(
            IAppaTaskAsyncEnumerable<long> source,
            CancellationToken cancellationToken)
        {
            long value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = e.Current;

                    goto NEXT_LOOP;
                }

                throw Error.NoElements();

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = e.Current;
                    if (value < x)
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

        public static async AppaTask<long> MaxAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, long> selector,
            CancellationToken cancellationToken)
        {
            long value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = selector(e.Current);

                    goto NEXT_LOOP;
                }

                throw Error.NoElements();

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = selector(e.Current);
                    if (value < x)
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

        public static async AppaTask<long> MaxAwaitAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<long>> selector,
            CancellationToken cancellationToken)
        {
            long value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current);

                    goto NEXT_LOOP;
                }

                throw Error.NoElements();

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current);
                    if (value < x)
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

        public static async AppaTask<long> MaxAwaitWithCancellationAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<long>> selector,
            CancellationToken cancellationToken)
        {
            long value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current, cancellationToken);

                    goto NEXT_LOOP;
                }

                throw Error.NoElements();

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current, cancellationToken);
                    if (value < x)
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

        public static async AppaTask<float> MaxAsync(
            IAppaTaskAsyncEnumerable<float> source,
            CancellationToken cancellationToken)
        {
            float value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = e.Current;

                    goto NEXT_LOOP;
                }

                throw Error.NoElements();

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = e.Current;
                    if (value < x)
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

        public static async AppaTask<float> MaxAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, float> selector,
            CancellationToken cancellationToken)
        {
            float value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = selector(e.Current);

                    goto NEXT_LOOP;
                }

                throw Error.NoElements();

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = selector(e.Current);
                    if (value < x)
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

        public static async AppaTask<float> MaxAwaitAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<float>> selector,
            CancellationToken cancellationToken)
        {
            float value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current);

                    goto NEXT_LOOP;
                }

                throw Error.NoElements();

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current);
                    if (value < x)
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

        public static async AppaTask<float> MaxAwaitWithCancellationAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<float>> selector,
            CancellationToken cancellationToken)
        {
            float value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current, cancellationToken);

                    goto NEXT_LOOP;
                }

                throw Error.NoElements();

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current, cancellationToken);
                    if (value < x)
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

        public static async AppaTask<double> MaxAsync(
            IAppaTaskAsyncEnumerable<double> source,
            CancellationToken cancellationToken)
        {
            double value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = e.Current;

                    goto NEXT_LOOP;
                }

                throw Error.NoElements();

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = e.Current;
                    if (value < x)
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

        public static async AppaTask<double> MaxAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, double> selector,
            CancellationToken cancellationToken)
        {
            double value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = selector(e.Current);

                    goto NEXT_LOOP;
                }

                throw Error.NoElements();

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = selector(e.Current);
                    if (value < x)
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

        public static async AppaTask<double> MaxAwaitAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<double>> selector,
            CancellationToken cancellationToken)
        {
            double value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current);

                    goto NEXT_LOOP;
                }

                throw Error.NoElements();

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current);
                    if (value < x)
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

        public static async AppaTask<double> MaxAwaitWithCancellationAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<double>> selector,
            CancellationToken cancellationToken)
        {
            double value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current, cancellationToken);

                    goto NEXT_LOOP;
                }

                throw Error.NoElements();

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current, cancellationToken);
                    if (value < x)
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

        public static async AppaTask<decimal> MaxAsync(
            IAppaTaskAsyncEnumerable<decimal> source,
            CancellationToken cancellationToken)
        {
            decimal value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = e.Current;

                    goto NEXT_LOOP;
                }

                throw Error.NoElements();

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = e.Current;
                    if (value < x)
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

        public static async AppaTask<decimal> MaxAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, decimal> selector,
            CancellationToken cancellationToken)
        {
            decimal value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = selector(e.Current);

                    goto NEXT_LOOP;
                }

                throw Error.NoElements();

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = selector(e.Current);
                    if (value < x)
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

        public static async AppaTask<decimal> MaxAwaitAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<decimal>> selector,
            CancellationToken cancellationToken)
        {
            decimal value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current);

                    goto NEXT_LOOP;
                }

                throw Error.NoElements();

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current);
                    if (value < x)
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

        public static async AppaTask<decimal> MaxAwaitWithCancellationAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<decimal>> selector,
            CancellationToken cancellationToken)
        {
            decimal value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current, cancellationToken);

                    goto NEXT_LOOP;
                }

                throw Error.NoElements();

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current, cancellationToken);
                    if (value < x)
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

        public static async AppaTask<int?> MaxAsync(
            IAppaTaskAsyncEnumerable<int?> source,
            CancellationToken cancellationToken)
        {
            int? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = e.Current;
                    if (value == null)
                    {
                        continue;
                    }

                    goto NEXT_LOOP;
                }

                return default;

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = e.Current;
                    if (x == null)
                    {
                        continue;
                    }

                    if (value < x)
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

        public static async AppaTask<int?> MaxAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, int?> selector,
            CancellationToken cancellationToken)
        {
            int? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = selector(e.Current);
                    if (value == null)
                    {
                        continue;
                    }

                    goto NEXT_LOOP;
                }

                return default;

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = selector(e.Current);
                    if (x == null)
                    {
                        continue;
                    }

                    if (value < x)
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

        public static async AppaTask<int?> MaxAwaitAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<int?>> selector,
            CancellationToken cancellationToken)
        {
            int? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current);
                    if (value == null)
                    {
                        continue;
                    }

                    goto NEXT_LOOP;
                }

                return default;

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current);
                    if (x == null)
                    {
                        continue;
                    }

                    if (value < x)
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

        public static async AppaTask<int?> MaxAwaitWithCancellationAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<int?>> selector,
            CancellationToken cancellationToken)
        {
            int? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current, cancellationToken);
                    if (value == null)
                    {
                        continue;
                    }

                    goto NEXT_LOOP;
                }

                return default;

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current, cancellationToken);
                    if (x == null)
                    {
                        continue;
                    }

                    if (value < x)
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

        public static async AppaTask<long?> MaxAsync(
            IAppaTaskAsyncEnumerable<long?> source,
            CancellationToken cancellationToken)
        {
            long? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = e.Current;
                    if (value == null)
                    {
                        continue;
                    }

                    goto NEXT_LOOP;
                }

                return default;

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = e.Current;
                    if (x == null)
                    {
                        continue;
                    }

                    if (value < x)
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

        public static async AppaTask<long?> MaxAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, long?> selector,
            CancellationToken cancellationToken)
        {
            long? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = selector(e.Current);
                    if (value == null)
                    {
                        continue;
                    }

                    goto NEXT_LOOP;
                }

                return default;

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = selector(e.Current);
                    if (x == null)
                    {
                        continue;
                    }

                    if (value < x)
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

        public static async AppaTask<long?> MaxAwaitAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<long?>> selector,
            CancellationToken cancellationToken)
        {
            long? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current);
                    if (value == null)
                    {
                        continue;
                    }

                    goto NEXT_LOOP;
                }

                return default;

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current);
                    if (x == null)
                    {
                        continue;
                    }

                    if (value < x)
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

        public static async AppaTask<long?> MaxAwaitWithCancellationAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<long?>> selector,
            CancellationToken cancellationToken)
        {
            long? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current, cancellationToken);
                    if (value == null)
                    {
                        continue;
                    }

                    goto NEXT_LOOP;
                }

                return default;

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current, cancellationToken);
                    if (x == null)
                    {
                        continue;
                    }

                    if (value < x)
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

        public static async AppaTask<float?> MaxAsync(
            IAppaTaskAsyncEnumerable<float?> source,
            CancellationToken cancellationToken)
        {
            float? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = e.Current;
                    if (value == null)
                    {
                        continue;
                    }

                    goto NEXT_LOOP;
                }

                return default;

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = e.Current;
                    if (x == null)
                    {
                        continue;
                    }

                    if (value < x)
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

        public static async AppaTask<float?> MaxAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, float?> selector,
            CancellationToken cancellationToken)
        {
            float? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = selector(e.Current);
                    if (value == null)
                    {
                        continue;
                    }

                    goto NEXT_LOOP;
                }

                return default;

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = selector(e.Current);
                    if (x == null)
                    {
                        continue;
                    }

                    if (value < x)
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

        public static async AppaTask<float?> MaxAwaitAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<float?>> selector,
            CancellationToken cancellationToken)
        {
            float? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current);
                    if (value == null)
                    {
                        continue;
                    }

                    goto NEXT_LOOP;
                }

                return default;

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current);
                    if (x == null)
                    {
                        continue;
                    }

                    if (value < x)
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

        public static async AppaTask<float?> MaxAwaitWithCancellationAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<float?>> selector,
            CancellationToken cancellationToken)
        {
            float? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current, cancellationToken);
                    if (value == null)
                    {
                        continue;
                    }

                    goto NEXT_LOOP;
                }

                return default;

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current, cancellationToken);
                    if (x == null)
                    {
                        continue;
                    }

                    if (value < x)
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

        public static async AppaTask<double?> MaxAsync(
            IAppaTaskAsyncEnumerable<double?> source,
            CancellationToken cancellationToken)
        {
            double? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = e.Current;
                    if (value == null)
                    {
                        continue;
                    }

                    goto NEXT_LOOP;
                }

                return default;

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = e.Current;
                    if (x == null)
                    {
                        continue;
                    }

                    if (value < x)
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

        public static async AppaTask<double?> MaxAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, double?> selector,
            CancellationToken cancellationToken)
        {
            double? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = selector(e.Current);
                    if (value == null)
                    {
                        continue;
                    }

                    goto NEXT_LOOP;
                }

                return default;

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = selector(e.Current);
                    if (x == null)
                    {
                        continue;
                    }

                    if (value < x)
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

        public static async AppaTask<double?> MaxAwaitAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<double?>> selector,
            CancellationToken cancellationToken)
        {
            double? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current);
                    if (value == null)
                    {
                        continue;
                    }

                    goto NEXT_LOOP;
                }

                return default;

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current);
                    if (x == null)
                    {
                        continue;
                    }

                    if (value < x)
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

        public static async AppaTask<double?> MaxAwaitWithCancellationAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<double?>> selector,
            CancellationToken cancellationToken)
        {
            double? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current, cancellationToken);
                    if (value == null)
                    {
                        continue;
                    }

                    goto NEXT_LOOP;
                }

                return default;

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current, cancellationToken);
                    if (x == null)
                    {
                        continue;
                    }

                    if (value < x)
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

        public static async AppaTask<decimal?> MaxAsync(
            IAppaTaskAsyncEnumerable<decimal?> source,
            CancellationToken cancellationToken)
        {
            decimal? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = e.Current;
                    if (value == null)
                    {
                        continue;
                    }

                    goto NEXT_LOOP;
                }

                return default;

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = e.Current;
                    if (x == null)
                    {
                        continue;
                    }

                    if (value < x)
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

        public static async AppaTask<decimal?> MaxAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, decimal?> selector,
            CancellationToken cancellationToken)
        {
            decimal? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = selector(e.Current);
                    if (value == null)
                    {
                        continue;
                    }

                    goto NEXT_LOOP;
                }

                return default;

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = selector(e.Current);
                    if (x == null)
                    {
                        continue;
                    }

                    if (value < x)
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

        public static async AppaTask<decimal?> MaxAwaitAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<decimal?>> selector,
            CancellationToken cancellationToken)
        {
            decimal? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current);
                    if (value == null)
                    {
                        continue;
                    }

                    goto NEXT_LOOP;
                }

                return default;

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current);
                    if (x == null)
                    {
                        continue;
                    }

                    if (value < x)
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

        public static async AppaTask<decimal?> MaxAwaitWithCancellationAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<decimal?>> selector,
            CancellationToken cancellationToken)
        {
            decimal? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current, cancellationToken);
                    if (value == null)
                    {
                        continue;
                    }

                    goto NEXT_LOOP;
                }

                return default;

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current, cancellationToken);
                    if (x == null)
                    {
                        continue;
                    }

                    if (value < x)
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
