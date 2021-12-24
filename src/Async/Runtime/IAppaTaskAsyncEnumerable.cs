using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;

namespace Appalachia.Utility.Async
{
    public interface IAppaTaskAsyncEnumerable<out T>
    {
        IAppaTaskAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default);
    }

    public interface IAppaTaskAsyncEnumerator<out T> : IAppaTaskAsyncDisposable
    {
        T Current { get; }
        AppaTask<bool> MoveNextAsync();
    }

    public interface IAppaTaskAsyncDisposable
    {
        AppaTask DisposeAsync();
    }

    public interface IAppaTaskOrderedAsyncEnumerable<TElement> : IAppaTaskAsyncEnumerable<TElement>
    {
        IAppaTaskOrderedAsyncEnumerable<TElement> CreateOrderedEnumerable<TKey>(
            Func<TElement, TKey> keySelector,
            IComparer<TKey> comparer,
            bool descending);

        IAppaTaskOrderedAsyncEnumerable<TElement> CreateOrderedEnumerable<TKey>(
            Func<TElement, AppaTask<TKey>> keySelector,
            IComparer<TKey> comparer,
            bool descending);

        IAppaTaskOrderedAsyncEnumerable<TElement> CreateOrderedEnumerable<TKey>(
            Func<TElement, CancellationToken, AppaTask<TKey>> keySelector,
            IComparer<TKey> comparer,
            bool descending);
    }

    public interface IConnectableAppaTaskAsyncEnumerable<out T> : IAppaTaskAsyncEnumerable<T>
    {
        IDisposable Connect();
    }

    // don't use AsyncGrouping.
    //public interface IAppaTaskAsyncGrouping<out TKey, out TElement> : IAppaTaskAsyncEnumerable<TElement>
    //{
    //    TKey Key { get; }
    //}

    public static class AppaTaskAsyncEnumerableExtensions
    {
        public static AppaTaskCancelableAsyncEnumerable<T> WithCancellation<T>(
            this IAppaTaskAsyncEnumerable<T> source,
            CancellationToken cancellationToken)
        {
            return new AppaTaskCancelableAsyncEnumerable<T>(source, cancellationToken);
        }
    }

    [StructLayout(LayoutKind.Auto)]
    public readonly struct AppaTaskCancelableAsyncEnumerable<T>
    {
        private readonly IAppaTaskAsyncEnumerable<T> enumerable;
        private readonly CancellationToken cancellationToken;

        internal AppaTaskCancelableAsyncEnumerable(
            IAppaTaskAsyncEnumerable<T> enumerable,
            CancellationToken cancellationToken)
        {
            this.enumerable = enumerable;
            this.cancellationToken = cancellationToken;
        }

        public Enumerator GetAsyncEnumerator()
        {
            return new Enumerator(enumerable.GetAsyncEnumerator(cancellationToken));
        }

        [StructLayout(LayoutKind.Auto)]
        public readonly struct Enumerator
        {
            private readonly IAppaTaskAsyncEnumerator<T> enumerator;

            internal Enumerator(IAppaTaskAsyncEnumerator<T> enumerator)
            {
                this.enumerator = enumerator;
            }

            public T Current => enumerator.Current;

            public AppaTask<bool> MoveNextAsync()
            {
                return enumerator.MoveNextAsync();
            }

            public AppaTask DisposeAsync()
            {
                return enumerator.DisposeAsync();
            }
        }
    }
}
