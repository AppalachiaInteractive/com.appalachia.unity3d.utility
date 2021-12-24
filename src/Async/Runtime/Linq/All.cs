using System;
using System.Threading;
using Appalachia.Utility.Async.Internal;

namespace Appalachia.Utility.Async.Linq
{
    public static partial class AppaTaskAsyncEnumerable
    {
        public static AppaTask<bool> AllAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, bool> predicate,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source,    nameof(source));
            Error.ThrowArgumentNullException(predicate, nameof(predicate));

            return All.AllAsync(source, predicate, cancellationToken);
        }

        public static AppaTask<bool> AllAwaitAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source,    nameof(source));
            Error.ThrowArgumentNullException(predicate, nameof(predicate));

            return All.AllAwaitAsync(source, predicate, cancellationToken);
        }

        public static AppaTask<bool> AllAwaitWithCancellationAsync<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source,    nameof(source));
            Error.ThrowArgumentNullException(predicate, nameof(predicate));

            return All.AllAwaitWithCancellationAsync(source, predicate, cancellationToken);
        }
    }

    internal static class All
    {
        internal static async AppaTask<bool> AllAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, bool> predicate,
            CancellationToken cancellationToken)
        {
            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    if (!predicate(e.Current))
                    {
                        return false;
                    }
                }

                return true;
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }
        }

        internal static async AppaTask<bool> AllAwaitAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<bool>> predicate,
            CancellationToken cancellationToken)
        {
            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    if (!await predicate(e.Current))
                    {
                        return false;
                    }
                }

                return true;
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }
        }

        internal static async AppaTask<bool> AllAwaitWithCancellationAsync<TSource>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<bool>> predicate,
            CancellationToken cancellationToken)
        {
            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    if (!await predicate(e.Current, cancellationToken))
                    {
                        return false;
                    }
                }

                return true;
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
