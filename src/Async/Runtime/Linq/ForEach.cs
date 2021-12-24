using System;
using System.Threading;
using Appalachia.Utility.Async.Internal;

namespace Appalachia.Utility.Async.Linq
{
    public static partial class AppaTaskAsyncEnumerable
    {
        public static AppaTask ForEachAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Action<TSource> action,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(action, nameof(action));

            return ForEach.ForEachAsync(source, action, cancellationToken);
        }

        public static AppaTask ForEachAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Action<TSource, int> action,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(action, nameof(action));

            return ForEach.ForEachAsync(source, action, cancellationToken);
        }

        /// <summary>Obsolete(Error), Use Use ForEachAwaitAsync instead.</summary>
        [Obsolete("Use ForEachAwaitAsync instead.", true)]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public static AppaTask ForEachAsync<T>(
            this IAppaTaskAsyncEnumerable<T> source,
            Func<T, AppaTask> action,
            CancellationToken cancellationToken = default)
        {
            throw new NotSupportedException("Use ForEachAwaitAsync instead.");
        }

        /// <summary>Obsolete(Error), Use Use ForEachAwaitAsync instead.</summary>
        [Obsolete("Use ForEachAwaitAsync instead.", true)]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public static AppaTask ForEachAsync<T>(
            this IAppaTaskAsyncEnumerable<T> source,
            Func<T, int, AppaTask> action,
            CancellationToken cancellationToken = default)
        {
            throw new NotSupportedException("Use ForEachAwaitAsync instead.");
        }

        public static AppaTask ForEachAwaitAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask> action,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(action, nameof(action));

            return ForEach.ForEachAwaitAsync(source, action, cancellationToken);
        }

        public static AppaTask ForEachAwaitAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, int, AppaTask> action,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(action, nameof(action));

            return ForEach.ForEachAwaitAsync(source, action, cancellationToken);
        }

        public static AppaTask ForEachAwaitWithCancellationAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask> action,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(action, nameof(action));

            return ForEach.ForEachAwaitWithCancellationAsync(source, action, cancellationToken);
        }

        public static AppaTask ForEachAwaitWithCancellationAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, int, CancellationToken, AppaTask> action,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(action, nameof(action));

            return ForEach.ForEachAwaitWithCancellationAsync(source, action, cancellationToken);
        }
    }

    internal static class ForEach
    {
        public static async AppaTask ForEachAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Action<TSource> action,
            CancellationToken cancellationToken)
        {
            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    action(e.Current);
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

        public static async AppaTask ForEachAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Action<TSource, int> action,
            CancellationToken cancellationToken)
        {
            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                var index = 0;
                while (await e.MoveNextAsync())
                {
                    action(e.Current, checked(index++));
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

        public static async AppaTask ForEachAwaitAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask> action,
            CancellationToken cancellationToken)
        {
            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    await action(e.Current);
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

        public static async AppaTask ForEachAwaitAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, int, AppaTask> action,
            CancellationToken cancellationToken)
        {
            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                var index = 0;
                while (await e.MoveNextAsync())
                {
                    await action(e.Current, checked(index++));
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

        public static async AppaTask ForEachAwaitWithCancellationAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask> action,
            CancellationToken cancellationToken)
        {
            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    await action(e.Current, cancellationToken);
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

        public static async AppaTask ForEachAwaitWithCancellationAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, int, CancellationToken, AppaTask> action,
            CancellationToken cancellationToken)
        {
            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                var index = 0;
                while (await e.MoveNextAsync())
                {
                    await action(e.Current, checked(index++), cancellationToken);
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
