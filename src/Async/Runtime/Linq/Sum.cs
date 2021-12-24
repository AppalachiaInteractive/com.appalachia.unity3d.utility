using System;
using System.Threading;
using Appalachia.Utility.Async.Internal;

namespace Appalachia.Utility.Async.Linq
{
    public static partial class AppaTaskAsyncEnumerable
    {
        public static AppaTask<int> SumAsync(
            this IAppaTaskAsyncEnumerable<int> source,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Sum.SumAsync(source, cancellationToken);
        }

        public static AppaTask<int> SumAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, int> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Sum.SumAsync(source, selector, cancellationToken);
        }

        public static AppaTask<int> SumAwaitAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<int>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Sum.SumAwaitAsync(source, selector, cancellationToken);
        }

        public static AppaTask<int> SumAwaitWithCancellationAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<int>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Sum.SumAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static AppaTask<long> SumAsync(
            this IAppaTaskAsyncEnumerable<long> source,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Sum.SumAsync(source, cancellationToken);
        }

        public static AppaTask<long> SumAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, long> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Sum.SumAsync(source, selector, cancellationToken);
        }

        public static AppaTask<long> SumAwaitAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<long>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Sum.SumAwaitAsync(source, selector, cancellationToken);
        }

        public static AppaTask<long> SumAwaitWithCancellationAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<long>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Sum.SumAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static AppaTask<float> SumAsync(
            this IAppaTaskAsyncEnumerable<float> source,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Sum.SumAsync(source, cancellationToken);
        }

        public static AppaTask<float> SumAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, float> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Sum.SumAsync(source, selector, cancellationToken);
        }

        public static AppaTask<float> SumAwaitAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<float>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Sum.SumAwaitAsync(source, selector, cancellationToken);
        }

        public static AppaTask<float> SumAwaitWithCancellationAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<float>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Sum.SumAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static AppaTask<double> SumAsync(
            this IAppaTaskAsyncEnumerable<double> source,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Sum.SumAsync(source, cancellationToken);
        }

        public static AppaTask<double> SumAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, double> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Sum.SumAsync(source, selector, cancellationToken);
        }

        public static AppaTask<double> SumAwaitAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<double>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Sum.SumAwaitAsync(source, selector, cancellationToken);
        }

        public static AppaTask<double> SumAwaitWithCancellationAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<double>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Sum.SumAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static AppaTask<decimal> SumAsync(
            this IAppaTaskAsyncEnumerable<decimal> source,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Sum.SumAsync(source, cancellationToken);
        }

        public static AppaTask<decimal> SumAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, decimal> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Sum.SumAsync(source, selector, cancellationToken);
        }

        public static AppaTask<decimal> SumAwaitAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<decimal>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Sum.SumAwaitAsync(source, selector, cancellationToken);
        }

        public static AppaTask<decimal> SumAwaitWithCancellationAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<decimal>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Sum.SumAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static AppaTask<int?> SumAsync(
            this IAppaTaskAsyncEnumerable<int?> source,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Sum.SumAsync(source, cancellationToken);
        }

        public static AppaTask<int?> SumAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, int?> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Sum.SumAsync(source, selector, cancellationToken);
        }

        public static AppaTask<int?> SumAwaitAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<int?>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Sum.SumAwaitAsync(source, selector, cancellationToken);
        }

        public static AppaTask<int?> SumAwaitWithCancellationAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<int?>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Sum.SumAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static AppaTask<long?> SumAsync(
            this IAppaTaskAsyncEnumerable<long?> source,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Sum.SumAsync(source, cancellationToken);
        }

        public static AppaTask<long?> SumAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, long?> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Sum.SumAsync(source, selector, cancellationToken);
        }

        public static AppaTask<long?> SumAwaitAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<long?>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Sum.SumAwaitAsync(source, selector, cancellationToken);
        }

        public static AppaTask<long?> SumAwaitWithCancellationAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<long?>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Sum.SumAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static AppaTask<float?> SumAsync(
            this IAppaTaskAsyncEnumerable<float?> source,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Sum.SumAsync(source, cancellationToken);
        }

        public static AppaTask<float?> SumAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, float?> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Sum.SumAsync(source, selector, cancellationToken);
        }

        public static AppaTask<float?> SumAwaitAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<float?>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Sum.SumAwaitAsync(source, selector, cancellationToken);
        }

        public static AppaTask<float?> SumAwaitWithCancellationAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<float?>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Sum.SumAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static AppaTask<double?> SumAsync(
            this IAppaTaskAsyncEnumerable<double?> source,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Sum.SumAsync(source, cancellationToken);
        }

        public static AppaTask<double?> SumAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, double?> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Sum.SumAsync(source, selector, cancellationToken);
        }

        public static AppaTask<double?> SumAwaitAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<double?>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Sum.SumAwaitAsync(source, selector, cancellationToken);
        }

        public static AppaTask<double?> SumAwaitWithCancellationAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<double?>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Sum.SumAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static AppaTask<decimal?> SumAsync(
            this IAppaTaskAsyncEnumerable<decimal?> source,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Sum.SumAsync(source, cancellationToken);
        }

        public static AppaTask<decimal?> SumAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, decimal?> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Sum.SumAsync(source, selector, cancellationToken);
        }

        public static AppaTask<decimal?> SumAwaitAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<decimal?>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Sum.SumAwaitAsync(source, selector, cancellationToken);
        }

        public static AppaTask<decimal?> SumAwaitWithCancellationAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<decimal?>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Sum.SumAwaitWithCancellationAsync(source, selector, cancellationToken);
        }
    }

    internal static class Sum
    {
        public static async AppaTask<int> SumAsync(
            IAppaTaskAsyncEnumerable<int> source,
            CancellationToken cancellationToken)
        {
            int sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += e.Current;
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async AppaTask<int> SumAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, int> selector,
            CancellationToken cancellationToken)
        {
            int sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += selector(e.Current);
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async AppaTask<int> SumAwaitAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<int>> selector,
            CancellationToken cancellationToken)
        {
            int sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += await selector(e.Current);
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async AppaTask<int> SumAwaitWithCancellationAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<int>> selector,
            CancellationToken cancellationToken)
        {
            int sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += await selector(e.Current, cancellationToken);
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async AppaTask<long> SumAsync(
            IAppaTaskAsyncEnumerable<long> source,
            CancellationToken cancellationToken)
        {
            long sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += e.Current;
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async AppaTask<long> SumAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, long> selector,
            CancellationToken cancellationToken)
        {
            long sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += selector(e.Current);
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async AppaTask<long> SumAwaitAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<long>> selector,
            CancellationToken cancellationToken)
        {
            long sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += await selector(e.Current);
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async AppaTask<long> SumAwaitWithCancellationAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<long>> selector,
            CancellationToken cancellationToken)
        {
            long sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += await selector(e.Current, cancellationToken);
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async AppaTask<float> SumAsync(
            IAppaTaskAsyncEnumerable<float> source,
            CancellationToken cancellationToken)
        {
            float sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += e.Current;
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async AppaTask<float> SumAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, float> selector,
            CancellationToken cancellationToken)
        {
            float sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += selector(e.Current);
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async AppaTask<float> SumAwaitAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<float>> selector,
            CancellationToken cancellationToken)
        {
            float sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += await selector(e.Current);
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async AppaTask<float> SumAwaitWithCancellationAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<float>> selector,
            CancellationToken cancellationToken)
        {
            float sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += await selector(e.Current, cancellationToken);
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async AppaTask<double> SumAsync(
            IAppaTaskAsyncEnumerable<double> source,
            CancellationToken cancellationToken)
        {
            double sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += e.Current;
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async AppaTask<double> SumAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, double> selector,
            CancellationToken cancellationToken)
        {
            double sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += selector(e.Current);
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async AppaTask<double> SumAwaitAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<double>> selector,
            CancellationToken cancellationToken)
        {
            double sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += await selector(e.Current);
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async AppaTask<double> SumAwaitWithCancellationAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<double>> selector,
            CancellationToken cancellationToken)
        {
            double sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += await selector(e.Current, cancellationToken);
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async AppaTask<decimal> SumAsync(
            IAppaTaskAsyncEnumerable<decimal> source,
            CancellationToken cancellationToken)
        {
            decimal sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += e.Current;
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async AppaTask<decimal> SumAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, decimal> selector,
            CancellationToken cancellationToken)
        {
            decimal sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += selector(e.Current);
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async AppaTask<decimal> SumAwaitAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<decimal>> selector,
            CancellationToken cancellationToken)
        {
            decimal sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += await selector(e.Current);
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async AppaTask<decimal> SumAwaitWithCancellationAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<decimal>> selector,
            CancellationToken cancellationToken)
        {
            decimal sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += await selector(e.Current, cancellationToken);
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async AppaTask<int?> SumAsync(
            IAppaTaskAsyncEnumerable<int?> source,
            CancellationToken cancellationToken)
        {
            int? sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += e.Current.GetValueOrDefault();
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async AppaTask<int?> SumAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, int?> selector,
            CancellationToken cancellationToken)
        {
            int? sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += selector(e.Current).GetValueOrDefault();
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async AppaTask<int?> SumAwaitAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<int?>> selector,
            CancellationToken cancellationToken)
        {
            int? sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += (await selector(e.Current)).GetValueOrDefault();
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async AppaTask<int?> SumAwaitWithCancellationAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<int?>> selector,
            CancellationToken cancellationToken)
        {
            int? sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += (await selector(e.Current, cancellationToken)).GetValueOrDefault();
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async AppaTask<long?> SumAsync(
            IAppaTaskAsyncEnumerable<long?> source,
            CancellationToken cancellationToken)
        {
            long? sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += e.Current.GetValueOrDefault();
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async AppaTask<long?> SumAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, long?> selector,
            CancellationToken cancellationToken)
        {
            long? sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += selector(e.Current).GetValueOrDefault();
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async AppaTask<long?> SumAwaitAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<long?>> selector,
            CancellationToken cancellationToken)
        {
            long? sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += (await selector(e.Current)).GetValueOrDefault();
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async AppaTask<long?> SumAwaitWithCancellationAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<long?>> selector,
            CancellationToken cancellationToken)
        {
            long? sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += (await selector(e.Current, cancellationToken)).GetValueOrDefault();
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async AppaTask<float?> SumAsync(
            IAppaTaskAsyncEnumerable<float?> source,
            CancellationToken cancellationToken)
        {
            float? sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += e.Current.GetValueOrDefault();
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async AppaTask<float?> SumAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, float?> selector,
            CancellationToken cancellationToken)
        {
            float? sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += selector(e.Current).GetValueOrDefault();
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async AppaTask<float?> SumAwaitAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<float?>> selector,
            CancellationToken cancellationToken)
        {
            float? sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += (await selector(e.Current)).GetValueOrDefault();
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async AppaTask<float?> SumAwaitWithCancellationAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<float?>> selector,
            CancellationToken cancellationToken)
        {
            float? sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += (await selector(e.Current, cancellationToken)).GetValueOrDefault();
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async AppaTask<double?> SumAsync(
            IAppaTaskAsyncEnumerable<double?> source,
            CancellationToken cancellationToken)
        {
            double? sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += e.Current.GetValueOrDefault();
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async AppaTask<double?> SumAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, double?> selector,
            CancellationToken cancellationToken)
        {
            double? sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += selector(e.Current).GetValueOrDefault();
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async AppaTask<double?> SumAwaitAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<double?>> selector,
            CancellationToken cancellationToken)
        {
            double? sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += (await selector(e.Current)).GetValueOrDefault();
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async AppaTask<double?> SumAwaitWithCancellationAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<double?>> selector,
            CancellationToken cancellationToken)
        {
            double? sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += (await selector(e.Current, cancellationToken)).GetValueOrDefault();
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async AppaTask<decimal?> SumAsync(
            IAppaTaskAsyncEnumerable<decimal?> source,
            CancellationToken cancellationToken)
        {
            decimal? sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += e.Current.GetValueOrDefault();
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async AppaTask<decimal?> SumAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, decimal?> selector,
            CancellationToken cancellationToken)
        {
            decimal? sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += selector(e.Current).GetValueOrDefault();
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async AppaTask<decimal?> SumAwaitAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<decimal?>> selector,
            CancellationToken cancellationToken)
        {
            decimal? sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += (await selector(e.Current)).GetValueOrDefault();
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async AppaTask<decimal?> SumAwaitWithCancellationAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<decimal?>> selector,
            CancellationToken cancellationToken)
        {
            decimal? sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += (await selector(e.Current, cancellationToken)).GetValueOrDefault();
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }
    }
}
