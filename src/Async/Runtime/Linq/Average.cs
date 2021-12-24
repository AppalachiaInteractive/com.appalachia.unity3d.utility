using System;
using System.Threading;
using Appalachia.Utility.Async.Internal;

namespace Appalachia.Utility.Async.Linq
{
    public static partial class AppaTaskAsyncEnumerable
    {
        public static AppaTask<double> AverageAsync(
            this IAppaTaskAsyncEnumerable<int> source,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Average.AverageAsync(source, cancellationToken);
        }

        public static AppaTask<double> AverageAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, int> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Average.AverageAsync(source, selector, cancellationToken);
        }

        public static AppaTask<double> AverageAwaitAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<int>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Average.AverageAwaitAsync(source, selector, cancellationToken);
        }

        public static AppaTask<double> AverageAwaitWithCancellationAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<int>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Average.AverageAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static AppaTask<double> AverageAsync(
            this IAppaTaskAsyncEnumerable<long> source,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Average.AverageAsync(source, cancellationToken);
        }

        public static AppaTask<double> AverageAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, long> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Average.AverageAsync(source, selector, cancellationToken);
        }

        public static AppaTask<double> AverageAwaitAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<long>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Average.AverageAwaitAsync(source, selector, cancellationToken);
        }

        public static AppaTask<double> AverageAwaitWithCancellationAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<long>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Average.AverageAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static AppaTask<float> AverageAsync(
            this IAppaTaskAsyncEnumerable<float> source,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Average.AverageAsync(source, cancellationToken);
        }

        public static AppaTask<float> AverageAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, float> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Average.AverageAsync(source, selector, cancellationToken);
        }

        public static AppaTask<float> AverageAwaitAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<float>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Average.AverageAwaitAsync(source, selector, cancellationToken);
        }

        public static AppaTask<float> AverageAwaitWithCancellationAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<float>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Average.AverageAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static AppaTask<double> AverageAsync(
            this IAppaTaskAsyncEnumerable<double> source,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Average.AverageAsync(source, cancellationToken);
        }

        public static AppaTask<double> AverageAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, double> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Average.AverageAsync(source, selector, cancellationToken);
        }

        public static AppaTask<double> AverageAwaitAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<double>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Average.AverageAwaitAsync(source, selector, cancellationToken);
        }

        public static AppaTask<double> AverageAwaitWithCancellationAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<double>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Average.AverageAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static AppaTask<decimal> AverageAsync(
            this IAppaTaskAsyncEnumerable<decimal> source,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Average.AverageAsync(source, cancellationToken);
        }

        public static AppaTask<decimal> AverageAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, decimal> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Average.AverageAsync(source, selector, cancellationToken);
        }

        public static AppaTask<decimal> AverageAwaitAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<decimal>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Average.AverageAwaitAsync(source, selector, cancellationToken);
        }

        public static AppaTask<decimal> AverageAwaitWithCancellationAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<decimal>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Average.AverageAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static AppaTask<double?> AverageAsync(
            this IAppaTaskAsyncEnumerable<int?> source,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Average.AverageAsync(source, cancellationToken);
        }

        public static AppaTask<double?> AverageAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, int?> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Average.AverageAsync(source, selector, cancellationToken);
        }

        public static AppaTask<double?> AverageAwaitAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<int?>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Average.AverageAwaitAsync(source, selector, cancellationToken);
        }

        public static AppaTask<double?> AverageAwaitWithCancellationAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<int?>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Average.AverageAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static AppaTask<double?> AverageAsync(
            this IAppaTaskAsyncEnumerable<long?> source,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Average.AverageAsync(source, cancellationToken);
        }

        public static AppaTask<double?> AverageAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, long?> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Average.AverageAsync(source, selector, cancellationToken);
        }

        public static AppaTask<double?> AverageAwaitAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<long?>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Average.AverageAwaitAsync(source, selector, cancellationToken);
        }

        public static AppaTask<double?> AverageAwaitWithCancellationAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<long?>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Average.AverageAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static AppaTask<float?> AverageAsync(
            this IAppaTaskAsyncEnumerable<float?> source,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Average.AverageAsync(source, cancellationToken);
        }

        public static AppaTask<float?> AverageAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, float?> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Average.AverageAsync(source, selector, cancellationToken);
        }

        public static AppaTask<float?> AverageAwaitAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<float?>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Average.AverageAwaitAsync(source, selector, cancellationToken);
        }

        public static AppaTask<float?> AverageAwaitWithCancellationAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<float?>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Average.AverageAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static AppaTask<double?> AverageAsync(
            this IAppaTaskAsyncEnumerable<double?> source,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Average.AverageAsync(source, cancellationToken);
        }

        public static AppaTask<double?> AverageAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, double?> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Average.AverageAsync(source, selector, cancellationToken);
        }

        public static AppaTask<double?> AverageAwaitAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<double?>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Average.AverageAwaitAsync(source, selector, cancellationToken);
        }

        public static AppaTask<double?> AverageAwaitWithCancellationAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<double?>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Average.AverageAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static AppaTask<decimal?> AverageAsync(
            this IAppaTaskAsyncEnumerable<decimal?> source,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Average.AverageAsync(source, cancellationToken);
        }

        public static AppaTask<decimal?> AverageAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, decimal?> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Average.AverageAsync(source, selector, cancellationToken);
        }

        public static AppaTask<decimal?> AverageAwaitAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<decimal?>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Average.AverageAwaitAsync(source, selector, cancellationToken);
        }

        public static AppaTask<decimal?> AverageAwaitWithCancellationAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<decimal?>> selector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Average.AverageAwaitWithCancellationAsync(source, selector, cancellationToken);
        }
    }

    internal static class Average
    {
        public static async AppaTask<double> AverageAsync(
            IAppaTaskAsyncEnumerable<int> source,
            CancellationToken cancellationToken)
        {
            long count = 0;
            var sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    checked
                    {
                        sum += e.Current;
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

            return (double)sum / count;
        }

        public static async AppaTask<double> AverageAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, int> selector,
            CancellationToken cancellationToken)
        {
            long count = 0;
            var sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    checked
                    {
                        sum += selector(e.Current);
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

            return (double)sum / count;
        }

        public static async AppaTask<double> AverageAwaitAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<int>> selector,
            CancellationToken cancellationToken)
        {
            long count = 0;
            var sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    checked
                    {
                        sum += await selector(e.Current);
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

            return (double)sum / count;
        }

        public static async AppaTask<double> AverageAwaitWithCancellationAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<int>> selector,
            CancellationToken cancellationToken)
        {
            long count = 0;
            var sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    checked
                    {
                        sum += await selector(e.Current, cancellationToken);
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

            return (double)sum / count;
        }

        public static async AppaTask<double> AverageAsync(
            IAppaTaskAsyncEnumerable<long> source,
            CancellationToken cancellationToken)
        {
            long count = 0;
            long sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    checked
                    {
                        sum += e.Current;
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

            return (double)sum / count;
        }

        public static async AppaTask<double> AverageAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, long> selector,
            CancellationToken cancellationToken)
        {
            long count = 0;
            long sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    checked
                    {
                        sum += selector(e.Current);
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

            return (double)sum / count;
        }

        public static async AppaTask<double> AverageAwaitAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<long>> selector,
            CancellationToken cancellationToken)
        {
            long count = 0;
            long sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    checked
                    {
                        sum += await selector(e.Current);
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

            return (double)sum / count;
        }

        public static async AppaTask<double> AverageAwaitWithCancellationAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<long>> selector,
            CancellationToken cancellationToken)
        {
            long count = 0;
            long sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    checked
                    {
                        sum += await selector(e.Current, cancellationToken);
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

            return (double)sum / count;
        }

        public static async AppaTask<float> AverageAsync(
            IAppaTaskAsyncEnumerable<float> source,
            CancellationToken cancellationToken)
        {
            long count = 0;
            float sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    checked
                    {
                        sum += e.Current;
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

            return sum / count;
        }

        public static async AppaTask<float> AverageAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, float> selector,
            CancellationToken cancellationToken)
        {
            long count = 0;
            float sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    checked
                    {
                        sum += selector(e.Current);
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

            return sum / count;
        }

        public static async AppaTask<float> AverageAwaitAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<float>> selector,
            CancellationToken cancellationToken)
        {
            long count = 0;
            float sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    checked
                    {
                        sum += await selector(e.Current);
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

            return sum / count;
        }

        public static async AppaTask<float> AverageAwaitWithCancellationAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<float>> selector,
            CancellationToken cancellationToken)
        {
            long count = 0;
            float sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    checked
                    {
                        sum += await selector(e.Current, cancellationToken);
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

            return sum / count;
        }

        public static async AppaTask<double> AverageAsync(
            IAppaTaskAsyncEnumerable<double> source,
            CancellationToken cancellationToken)
        {
            long count = 0;
            double sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    checked
                    {
                        sum += e.Current;
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

            return sum / count;
        }

        public static async AppaTask<double> AverageAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, double> selector,
            CancellationToken cancellationToken)
        {
            long count = 0;
            double sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    checked
                    {
                        sum += selector(e.Current);
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

            return sum / count;
        }

        public static async AppaTask<double> AverageAwaitAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<double>> selector,
            CancellationToken cancellationToken)
        {
            long count = 0;
            double sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    checked
                    {
                        sum += await selector(e.Current);
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

            return sum / count;
        }

        public static async AppaTask<double> AverageAwaitWithCancellationAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<double>> selector,
            CancellationToken cancellationToken)
        {
            long count = 0;
            double sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    checked
                    {
                        sum += await selector(e.Current, cancellationToken);
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

            return sum / count;
        }

        public static async AppaTask<decimal> AverageAsync(
            IAppaTaskAsyncEnumerable<decimal> source,
            CancellationToken cancellationToken)
        {
            long count = 0;
            decimal sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    checked
                    {
                        sum += e.Current;
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

            return sum / count;
        }

        public static async AppaTask<decimal> AverageAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, decimal> selector,
            CancellationToken cancellationToken)
        {
            long count = 0;
            decimal sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    checked
                    {
                        sum += selector(e.Current);
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

            return sum / count;
        }

        public static async AppaTask<decimal> AverageAwaitAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<decimal>> selector,
            CancellationToken cancellationToken)
        {
            long count = 0;
            decimal sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    checked
                    {
                        sum += await selector(e.Current);
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

            return sum / count;
        }

        public static async AppaTask<decimal> AverageAwaitWithCancellationAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<decimal>> selector,
            CancellationToken cancellationToken)
        {
            long count = 0;
            decimal sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    checked
                    {
                        sum += await selector(e.Current, cancellationToken);
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

            return sum / count;
        }

        public static async AppaTask<double?> AverageAsync(
            IAppaTaskAsyncEnumerable<int?> source,
            CancellationToken cancellationToken)
        {
            long count = 0;
            int? sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    var v = e.Current;
                    if (v.HasValue)
                    {
                        checked
                        {
                            sum += v.Value;
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

            return (double)sum / count;
        }

        public static async AppaTask<double?> AverageAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, int?> selector,
            CancellationToken cancellationToken)
        {
            long count = 0;
            int? sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    var v = selector(e.Current);
                    if (v.HasValue)
                    {
                        checked
                        {
                            sum += v.Value;
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

            return (double)sum / count;
        }

        public static async AppaTask<double?> AverageAwaitAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<int?>> selector,
            CancellationToken cancellationToken)
        {
            long count = 0;
            int? sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    var v = await selector(e.Current);
                    if (v.HasValue)
                    {
                        checked
                        {
                            sum += v.Value;
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

            return (double)sum / count;
        }

        public static async AppaTask<double?> AverageAwaitWithCancellationAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<int?>> selector,
            CancellationToken cancellationToken)
        {
            long count = 0;
            int? sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    var v = await selector(e.Current, cancellationToken);
                    if (v.HasValue)
                    {
                        checked
                        {
                            sum += v.Value;
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

            return (double)sum / count;
        }

        public static async AppaTask<double?> AverageAsync(
            IAppaTaskAsyncEnumerable<long?> source,
            CancellationToken cancellationToken)
        {
            long count = 0;
            long? sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    var v = e.Current;
                    if (v.HasValue)
                    {
                        checked
                        {
                            sum += v.Value;
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

            return (double)sum / count;
        }

        public static async AppaTask<double?> AverageAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, long?> selector,
            CancellationToken cancellationToken)
        {
            long count = 0;
            long? sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    var v = selector(e.Current);
                    if (v.HasValue)
                    {
                        checked
                        {
                            sum += v.Value;
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

            return (double)sum / count;
        }

        public static async AppaTask<double?> AverageAwaitAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<long?>> selector,
            CancellationToken cancellationToken)
        {
            long count = 0;
            long? sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    var v = await selector(e.Current);
                    if (v.HasValue)
                    {
                        checked
                        {
                            sum += v.Value;
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

            return (double)sum / count;
        }

        public static async AppaTask<double?> AverageAwaitWithCancellationAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<long?>> selector,
            CancellationToken cancellationToken)
        {
            long count = 0;
            long? sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    var v = await selector(e.Current, cancellationToken);
                    if (v.HasValue)
                    {
                        checked
                        {
                            sum += v.Value;
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

            return (double)sum / count;
        }

        public static async AppaTask<float?> AverageAsync(
            IAppaTaskAsyncEnumerable<float?> source,
            CancellationToken cancellationToken)
        {
            long count = 0;
            float? sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    var v = e.Current;
                    if (v.HasValue)
                    {
                        checked
                        {
                            sum += v.Value;
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

            return (float)(sum / count);
        }

        public static async AppaTask<float?> AverageAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, float?> selector,
            CancellationToken cancellationToken)
        {
            long count = 0;
            float? sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    var v = selector(e.Current);
                    if (v.HasValue)
                    {
                        checked
                        {
                            sum += v.Value;
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

            return (float)(sum / count);
        }

        public static async AppaTask<float?> AverageAwaitAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<float?>> selector,
            CancellationToken cancellationToken)
        {
            long count = 0;
            float? sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    var v = await selector(e.Current);
                    if (v.HasValue)
                    {
                        checked
                        {
                            sum += v.Value;
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

            return (float)(sum / count);
        }

        public static async AppaTask<float?> AverageAwaitWithCancellationAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<float?>> selector,
            CancellationToken cancellationToken)
        {
            long count = 0;
            float? sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    var v = await selector(e.Current, cancellationToken);
                    if (v.HasValue)
                    {
                        checked
                        {
                            sum += v.Value;
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

            return (float)(sum / count);
        }

        public static async AppaTask<double?> AverageAsync(
            IAppaTaskAsyncEnumerable<double?> source,
            CancellationToken cancellationToken)
        {
            long count = 0;
            double? sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    var v = e.Current;
                    if (v.HasValue)
                    {
                        checked
                        {
                            sum += v.Value;
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

            return sum / count;
        }

        public static async AppaTask<double?> AverageAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, double?> selector,
            CancellationToken cancellationToken)
        {
            long count = 0;
            double? sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    var v = selector(e.Current);
                    if (v.HasValue)
                    {
                        checked
                        {
                            sum += v.Value;
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

            return sum / count;
        }

        public static async AppaTask<double?> AverageAwaitAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<double?>> selector,
            CancellationToken cancellationToken)
        {
            long count = 0;
            double? sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    var v = await selector(e.Current);
                    if (v.HasValue)
                    {
                        checked
                        {
                            sum += v.Value;
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

            return sum / count;
        }

        public static async AppaTask<double?> AverageAwaitWithCancellationAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<double?>> selector,
            CancellationToken cancellationToken)
        {
            long count = 0;
            double? sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    var v = await selector(e.Current, cancellationToken);
                    if (v.HasValue)
                    {
                        checked
                        {
                            sum += v.Value;
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

            return sum / count;
        }

        public static async AppaTask<decimal?> AverageAsync(
            IAppaTaskAsyncEnumerable<decimal?> source,
            CancellationToken cancellationToken)
        {
            long count = 0;
            decimal? sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    var v = e.Current;
                    if (v.HasValue)
                    {
                        checked
                        {
                            sum += v.Value;
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

            return sum / count;
        }

        public static async AppaTask<decimal?> AverageAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, decimal?> selector,
            CancellationToken cancellationToken)
        {
            long count = 0;
            decimal? sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    var v = selector(e.Current);
                    if (v.HasValue)
                    {
                        checked
                        {
                            sum += v.Value;
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

            return sum / count;
        }

        public static async AppaTask<decimal?> AverageAwaitAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<decimal?>> selector,
            CancellationToken cancellationToken)
        {
            long count = 0;
            decimal? sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    var v = await selector(e.Current);
                    if (v.HasValue)
                    {
                        checked
                        {
                            sum += v.Value;
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

            return sum / count;
        }

        public static async AppaTask<decimal?> AverageAwaitWithCancellationAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<decimal?>> selector,
            CancellationToken cancellationToken)
        {
            long count = 0;
            decimal? sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    var v = await selector(e.Current, cancellationToken);
                    if (v.HasValue)
                    {
                        checked
                        {
                            sum += v.Value;
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

            return sum / count;
        }
    }
}
