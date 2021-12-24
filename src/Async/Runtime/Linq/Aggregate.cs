using System;
using System.Threading;
using Appalachia.Utility.Async.Internal;

namespace Appalachia.Utility.Async.Linq
{
    public static partial class AppaTaskAsyncEnumerable
    {
        public static AppaTask<TSource> AggregateAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, TSource, TSource> accumulator,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source,      nameof(source));
            Error.ThrowArgumentNullException(accumulator, nameof(accumulator));

            return Aggregate.AggregateAsync(source, accumulator, cancellationToken);
        }

        public static AppaTask<TAccumulate> AggregateAsync<TSource, TAccumulate>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            TAccumulate seed,
            Func<TAccumulate, TSource, TAccumulate> accumulator,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source,      nameof(source));
            Error.ThrowArgumentNullException(accumulator, nameof(accumulator));

            return Aggregate.AggregateAsync(source, seed, accumulator, cancellationToken);
        }

        public static AppaTask<TResult> AggregateAsync<TSource, TAccumulate, TResult>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            TAccumulate seed,
            Func<TAccumulate, TSource, TAccumulate> accumulator,
            Func<TAccumulate, TResult> resultSelector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source,      nameof(source));
            Error.ThrowArgumentNullException(accumulator, nameof(accumulator));
            Error.ThrowArgumentNullException(accumulator, nameof(resultSelector));

            return Aggregate.AggregateAsync(source, seed, accumulator, resultSelector, cancellationToken);
        }

        public static AppaTask<TSource> AggregateAwaitAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, TSource, AppaTask<TSource>> accumulator,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source,      nameof(source));
            Error.ThrowArgumentNullException(accumulator, nameof(accumulator));

            return Aggregate.AggregateAwaitAsync(source, accumulator, cancellationToken);
        }

        public static AppaTask<TAccumulate> AggregateAwaitAsync<TSource, TAccumulate>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            TAccumulate seed,
            Func<TAccumulate, TSource, AppaTask<TAccumulate>> accumulator,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source,      nameof(source));
            Error.ThrowArgumentNullException(accumulator, nameof(accumulator));

            return Aggregate.AggregateAwaitAsync(source, seed, accumulator, cancellationToken);
        }

        public static AppaTask<TResult> AggregateAwaitAsync<TSource, TAccumulate, TResult>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            TAccumulate seed,
            Func<TAccumulate, TSource, AppaTask<TAccumulate>> accumulator,
            Func<TAccumulate, AppaTask<TResult>> resultSelector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source,      nameof(source));
            Error.ThrowArgumentNullException(accumulator, nameof(accumulator));
            Error.ThrowArgumentNullException(accumulator, nameof(resultSelector));

            return Aggregate.AggregateAwaitAsync(
                source,
                seed,
                accumulator,
                resultSelector,
                cancellationToken
            );
        }

        public static AppaTask<TSource> AggregateAwaitWithCancellationAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, TSource, CancellationToken, AppaTask<TSource>> accumulator,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source,      nameof(source));
            Error.ThrowArgumentNullException(accumulator, nameof(accumulator));

            return Aggregate.AggregateAwaitWithCancellationAsync(source, accumulator, cancellationToken);
        }

        public static AppaTask<TAccumulate> AggregateAwaitWithCancellationAsync<TSource, TAccumulate>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            TAccumulate seed,
            Func<TAccumulate, TSource, CancellationToken, AppaTask<TAccumulate>> accumulator,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source,      nameof(source));
            Error.ThrowArgumentNullException(accumulator, nameof(accumulator));

            return Aggregate.AggregateAwaitWithCancellationAsync(
                source,
                seed,
                accumulator,
                cancellationToken
            );
        }

        public static AppaTask<TResult> AggregateAwaitWithCancellationAsync<TSource, TAccumulate, TResult>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            TAccumulate seed,
            Func<TAccumulate, TSource, CancellationToken, AppaTask<TAccumulate>> accumulator,
            Func<TAccumulate, CancellationToken, AppaTask<TResult>> resultSelector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source,      nameof(source));
            Error.ThrowArgumentNullException(accumulator, nameof(accumulator));
            Error.ThrowArgumentNullException(accumulator, nameof(resultSelector));

            return Aggregate.AggregateAwaitWithCancellationAsync(
                source,
                seed,
                accumulator,
                resultSelector,
                cancellationToken
            );
        }
    }

    internal static class Aggregate
    {
        internal static async AppaTask<TSource> AggregateAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, TSource, TSource> accumulator,
            CancellationToken cancellationToken)
        {
            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                TSource value;
                if (await e.MoveNextAsync())
                {
                    value = e.Current;
                }
                else
                {
                    throw Error.NoElements();
                }

                while (await e.MoveNextAsync())
                {
                    value = accumulator(value, e.Current);
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

        internal static async AppaTask<TAccumulate> AggregateAsync<TSource, TAccumulate>(
            IAppaTaskAsyncEnumerable<TSource> source,
            TAccumulate seed,
            Func<TAccumulate, TSource, TAccumulate> accumulator,
            CancellationToken cancellationToken)
        {
            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                var value = seed;
                while (await e.MoveNextAsync())
                {
                    value = accumulator(value, e.Current);
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

        internal static async AppaTask<TResult> AggregateAsync<TSource, TAccumulate, TResult>(
            IAppaTaskAsyncEnumerable<TSource> source,
            TAccumulate seed,
            Func<TAccumulate, TSource, TAccumulate> accumulator,
            Func<TAccumulate, TResult> resultSelector,
            CancellationToken cancellationToken)
        {
            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                var value = seed;
                while (await e.MoveNextAsync())
                {
                    value = accumulator(value, e.Current);
                }

                return resultSelector(value);
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }
        }

        // with async

        internal static async AppaTask<TSource> AggregateAwaitAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, TSource, AppaTask<TSource>> accumulator,
            CancellationToken cancellationToken)
        {
            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                TSource value;
                if (await e.MoveNextAsync())
                {
                    value = e.Current;
                }
                else
                {
                    throw Error.NoElements();
                }

                while (await e.MoveNextAsync())
                {
                    value = await accumulator(value, e.Current);
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

        internal static async AppaTask<TAccumulate> AggregateAwaitAsync<TSource, TAccumulate>(
            IAppaTaskAsyncEnumerable<TSource> source,
            TAccumulate seed,
            Func<TAccumulate, TSource, AppaTask<TAccumulate>> accumulator,
            CancellationToken cancellationToken)
        {
            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                var value = seed;
                while (await e.MoveNextAsync())
                {
                    value = await accumulator(value, e.Current);
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

        internal static async AppaTask<TResult> AggregateAwaitAsync<TSource, TAccumulate, TResult>(
            IAppaTaskAsyncEnumerable<TSource> source,
            TAccumulate seed,
            Func<TAccumulate, TSource, AppaTask<TAccumulate>> accumulator,
            Func<TAccumulate, AppaTask<TResult>> resultSelector,
            CancellationToken cancellationToken)
        {
            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                var value = seed;
                while (await e.MoveNextAsync())
                {
                    value = await accumulator(value, e.Current);
                }

                return await resultSelector(value);
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }
        }

        // with cancellation

        internal static async AppaTask<TSource> AggregateAwaitWithCancellationAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, TSource, CancellationToken, AppaTask<TSource>> accumulator,
            CancellationToken cancellationToken)
        {
            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                TSource value;
                if (await e.MoveNextAsync())
                {
                    value = e.Current;
                }
                else
                {
                    throw Error.NoElements();
                }

                while (await e.MoveNextAsync())
                {
                    value = await accumulator(value, e.Current, cancellationToken);
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

        internal static async AppaTask<TAccumulate> AggregateAwaitWithCancellationAsync<TSource, TAccumulate>(
            IAppaTaskAsyncEnumerable<TSource> source,
            TAccumulate seed,
            Func<TAccumulate, TSource, CancellationToken, AppaTask<TAccumulate>> accumulator,
            CancellationToken cancellationToken)
        {
            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                var value = seed;
                while (await e.MoveNextAsync())
                {
                    value = await accumulator(value, e.Current, cancellationToken);
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

        internal static async AppaTask<TResult>
            AggregateAwaitWithCancellationAsync<TSource, TAccumulate, TResult>(
                IAppaTaskAsyncEnumerable<TSource> source,
                TAccumulate seed,
                Func<TAccumulate, TSource, CancellationToken, AppaTask<TAccumulate>> accumulator,
                Func<TAccumulate, CancellationToken, AppaTask<TResult>> resultSelector,
                CancellationToken cancellationToken)
        {
            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                var value = seed;
                while (await e.MoveNextAsync())
                {
                    value = await accumulator(value, e.Current, cancellationToken);
                }

                return await resultSelector(value, cancellationToken);
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
