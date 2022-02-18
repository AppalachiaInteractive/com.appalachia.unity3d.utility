using System;
using System.Collections.Generic;
using System.Threading;
using Appalachia.Utility.Async.Internal;

namespace Appalachia.Utility.Async.Linq
{
    public static partial class AppaTaskAsyncEnumerable
    {
        #region OrderBy_OrderByDescending

        public static IAppaTaskOrderedAsyncEnumerable<TSource> OrderBy<TSource, TKey>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, TKey> keySelector)
        {
            Error.ThrowArgumentNullException(source,      nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));

            return new OrderedAsyncEnumerable<TSource, TKey>(
                source,
                keySelector,
                Comparer<TKey>.Default,
                false,
                null
            );
        }

        public static IAppaTaskOrderedAsyncEnumerable<TSource> OrderBy<TSource, TKey>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            IComparer<TKey> comparer)
        {
            Error.ThrowArgumentNullException(source,      nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));
            Error.ThrowArgumentNullException(comparer,    nameof(comparer));

            return new OrderedAsyncEnumerable<TSource, TKey>(source, keySelector, comparer, false, null);
        }

        public static IAppaTaskOrderedAsyncEnumerable<TSource> OrderByAwait<TSource, TKey>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<TKey>> keySelector)
        {
            Error.ThrowArgumentNullException(source,      nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));

            return new OrderedAsyncEnumerableAwait<TSource, TKey>(
                source,
                keySelector,
                Comparer<TKey>.Default,
                false,
                null
            );
        }

        public static IAppaTaskOrderedAsyncEnumerable<TSource> OrderByAwait<TSource, TKey>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<TKey>> keySelector,
            IComparer<TKey> comparer)
        {
            Error.ThrowArgumentNullException(source,      nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));
            Error.ThrowArgumentNullException(comparer,    nameof(comparer));

            return new OrderedAsyncEnumerableAwait<TSource, TKey>(source, keySelector, comparer, false, null);
        }

        public static IAppaTaskOrderedAsyncEnumerable<TSource> OrderByAwaitWithCancellation<TSource, TKey>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<TKey>> keySelector)
        {
            Error.ThrowArgumentNullException(source,      nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));

            return new OrderedAsyncEnumerableAwaitWithCancellation<TSource, TKey>(
                source,
                keySelector,
                Comparer<TKey>.Default,
                false,
                null
            );
        }

        public static IAppaTaskOrderedAsyncEnumerable<TSource> OrderByAwaitWithCancellation<TSource, TKey>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<TKey>> keySelector,
            IComparer<TKey> comparer)
        {
            Error.ThrowArgumentNullException(source,      nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));
            Error.ThrowArgumentNullException(comparer,    nameof(comparer));

            return new OrderedAsyncEnumerableAwaitWithCancellation<TSource, TKey>(
                source,
                keySelector,
                comparer,
                false,
                null
            );
        }

        public static IAppaTaskOrderedAsyncEnumerable<TSource> OrderByDescending<TSource, TKey>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, TKey> keySelector)
        {
            Error.ThrowArgumentNullException(source,      nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));

            return new OrderedAsyncEnumerable<TSource, TKey>(
                source,
                keySelector,
                Comparer<TKey>.Default,
                true,
                null
            );
        }

        public static IAppaTaskOrderedAsyncEnumerable<TSource> OrderByDescending<TSource, TKey>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            IComparer<TKey> comparer)
        {
            Error.ThrowArgumentNullException(source,      nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));
            Error.ThrowArgumentNullException(comparer,    nameof(comparer));

            return new OrderedAsyncEnumerable<TSource, TKey>(source, keySelector, comparer, true, null);
        }

        public static IAppaTaskOrderedAsyncEnumerable<TSource> OrderByDescendingAwait<TSource, TKey>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<TKey>> keySelector)
        {
            Error.ThrowArgumentNullException(source,      nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));

            return new OrderedAsyncEnumerableAwait<TSource, TKey>(
                source,
                keySelector,
                Comparer<TKey>.Default,
                true,
                null
            );
        }

        public static IAppaTaskOrderedAsyncEnumerable<TSource> OrderByDescendingAwait<TSource, TKey>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<TKey>> keySelector,
            IComparer<TKey> comparer)
        {
            Error.ThrowArgumentNullException(source,      nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));
            Error.ThrowArgumentNullException(comparer,    nameof(comparer));

            return new OrderedAsyncEnumerableAwait<TSource, TKey>(source, keySelector, comparer, true, null);
        }

        public static IAppaTaskOrderedAsyncEnumerable<TSource>
            OrderByDescendingAwaitWithCancellation<TSource, TKey>(
                this IAppaTaskAsyncEnumerable<TSource> source,
                Func<TSource, CancellationToken, AppaTask<TKey>> keySelector)
        {
            Error.ThrowArgumentNullException(source,      nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));

            return new OrderedAsyncEnumerableAwaitWithCancellation<TSource, TKey>(
                source,
                keySelector,
                Comparer<TKey>.Default,
                true,
                null
            );
        }

        public static IAppaTaskOrderedAsyncEnumerable<TSource>
            OrderByDescendingAwaitWithCancellation<TSource, TKey>(
                this IAppaTaskAsyncEnumerable<TSource> source,
                Func<TSource, CancellationToken, AppaTask<TKey>> keySelector,
                IComparer<TKey> comparer)
        {
            Error.ThrowArgumentNullException(source,      nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));
            Error.ThrowArgumentNullException(comparer,    nameof(comparer));

            return new OrderedAsyncEnumerableAwaitWithCancellation<TSource, TKey>(
                source,
                keySelector,
                comparer,
                true,
                null
            );
        }

        #endregion

        #region ThenBy_ThenByDescending

        public static IAppaTaskOrderedAsyncEnumerable<TSource> ThenBy<TSource, TKey>(
            this IAppaTaskOrderedAsyncEnumerable<TSource> source,
            Func<TSource, TKey> keySelector)
        {
            Error.ThrowArgumentNullException(source,      nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));

            return source.CreateOrderedEnumerable(keySelector, Comparer<TKey>.Default, false);
        }

        public static IAppaTaskOrderedAsyncEnumerable<TSource> ThenBy<TSource, TKey>(
            this IAppaTaskOrderedAsyncEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            IComparer<TKey> comparer)
        {
            Error.ThrowArgumentNullException(source,      nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));
            Error.ThrowArgumentNullException(comparer,    nameof(comparer));

            return source.CreateOrderedEnumerable(keySelector, comparer, false);
        }

        public static IAppaTaskOrderedAsyncEnumerable<TSource> ThenByAwait<TSource, TKey>(
            this IAppaTaskOrderedAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<TKey>> keySelector)
        {
            Error.ThrowArgumentNullException(source,      nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));

            return source.CreateOrderedEnumerable(keySelector, Comparer<TKey>.Default, false);
        }

        public static IAppaTaskOrderedAsyncEnumerable<TSource> ThenByAwait<TSource, TKey>(
            this IAppaTaskOrderedAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<TKey>> keySelector,
            IComparer<TKey> comparer)
        {
            Error.ThrowArgumentNullException(source,      nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));
            Error.ThrowArgumentNullException(comparer,    nameof(comparer));

            return source.CreateOrderedEnumerable(keySelector, comparer, false);
        }

        public static IAppaTaskOrderedAsyncEnumerable<TSource> ThenByAwaitWithCancellation<TSource, TKey>(
            this IAppaTaskOrderedAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<TKey>> keySelector)
        {
            Error.ThrowArgumentNullException(source,      nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));

            return source.CreateOrderedEnumerable(keySelector, Comparer<TKey>.Default, false);
        }

        public static IAppaTaskOrderedAsyncEnumerable<TSource> ThenByAwaitWithCancellation<TSource, TKey>(
            this IAppaTaskOrderedAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<TKey>> keySelector,
            IComparer<TKey> comparer)
        {
            Error.ThrowArgumentNullException(source,      nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));
            Error.ThrowArgumentNullException(comparer,    nameof(comparer));

            return source.CreateOrderedEnumerable(keySelector, comparer, false);
        }

        public static IAppaTaskOrderedAsyncEnumerable<TSource> ThenByDescending<TSource, TKey>(
            this IAppaTaskOrderedAsyncEnumerable<TSource> source,
            Func<TSource, TKey> keySelector)
        {
            Error.ThrowArgumentNullException(source,      nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));

            return source.CreateOrderedEnumerable(keySelector, Comparer<TKey>.Default, true);
        }

        public static IAppaTaskOrderedAsyncEnumerable<TSource> ThenByDescending<TSource, TKey>(
            this IAppaTaskOrderedAsyncEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            IComparer<TKey> comparer)
        {
            Error.ThrowArgumentNullException(source,      nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));
            Error.ThrowArgumentNullException(comparer,    nameof(comparer));

            return source.CreateOrderedEnumerable(keySelector, comparer, true);
        }

        public static IAppaTaskOrderedAsyncEnumerable<TSource> ThenByDescendingAwait<TSource, TKey>(
            this IAppaTaskOrderedAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<TKey>> keySelector)
        {
            Error.ThrowArgumentNullException(source,      nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));

            return source.CreateOrderedEnumerable(keySelector, Comparer<TKey>.Default, true);
        }

        public static IAppaTaskOrderedAsyncEnumerable<TSource> ThenByDescendingAwait<TSource, TKey>(
            this IAppaTaskOrderedAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<TKey>> keySelector,
            IComparer<TKey> comparer)
        {
            Error.ThrowArgumentNullException(source,      nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));
            Error.ThrowArgumentNullException(comparer,    nameof(comparer));

            return source.CreateOrderedEnumerable(keySelector, comparer, true);
        }

        public static IAppaTaskOrderedAsyncEnumerable<TSource>
            ThenByDescendingAwaitWithCancellation<TSource, TKey>(
                this IAppaTaskOrderedAsyncEnumerable<TSource> source,
                Func<TSource, CancellationToken, AppaTask<TKey>> keySelector)
        {
            Error.ThrowArgumentNullException(source,      nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));

            return source.CreateOrderedEnumerable(keySelector, Comparer<TKey>.Default, true);
        }

        public static IAppaTaskOrderedAsyncEnumerable<TSource>
            ThenByDescendingAwaitWithCancellation<TSource, TKey>(
                this IAppaTaskOrderedAsyncEnumerable<TSource> source,
                Func<TSource, CancellationToken, AppaTask<TKey>> keySelector,
                IComparer<TKey> comparer)
        {
            Error.ThrowArgumentNullException(source,      nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));
            Error.ThrowArgumentNullException(comparer,    nameof(comparer));

            return source.CreateOrderedEnumerable(keySelector, comparer, true);
        }

        #endregion
    }

    internal abstract class AsyncEnumerableSorter<TElement>
    {
        internal abstract int CompareKeys(int index1, int index2);
        internal abstract AppaTask ComputeKeysAsync(TElement[] elements, int count);

        internal async AppaTask<int[]> SortAsync(TElement[] elements, int count)
        {
            await ComputeKeysAsync(elements, count);

            var map = new int[count];
            for (var i = 0; i < count; i++)
            {
                map[i] = i;
            }

            QuickSort(map, 0, count - 1);
            return map;
        }

        private void QuickSort(int[] map, int left, int right)
        {
            do
            {
                var i = left;
                var j = right;
                var x = map[i + ((j - i) >> 1)];
                do
                {
                    while ((i < map.Length) && (CompareKeys(x, map[i]) > 0))
                    {
                        i++;
                    }

                    while ((j >= 0) && (CompareKeys(x, map[j]) < 0))
                    {
                        j--;
                    }

                    if (i > j)
                    {
                        break;
                    }

                    if (i < j)
                    {
                        var temp = map[i];
                        map[i] = map[j];
                        map[j] = temp;
                    }

                    i++;
                    j--;
                } while (i <= j);

                if ((j - left) <= (right - i))
                {
                    if (left < j)
                    {
                        QuickSort(map, left, j);
                    }

                    left = i;
                }
                else
                {
                    if (i < right)
                    {
                        QuickSort(map, i, right);
                    }

                    right = j;
                }
            } while (left < right);
        }
    }

    internal class SyncSelectorAsyncEnumerableSorter<TElement, TKey> : AsyncEnumerableSorter<TElement>
    {
        internal SyncSelectorAsyncEnumerableSorter(
            Func<TElement, TKey> keySelector,
            IComparer<TKey> comparer,
            bool descending,
            AsyncEnumerableSorter<TElement> next)
        {
            this.keySelector = keySelector;
            this.comparer = comparer;
            this.descending = descending;
            this.next = next;
        }

        #region Fields and Autoproperties

        private readonly AsyncEnumerableSorter<TElement> next;
        private readonly bool descending;
        private readonly Func<TElement, TKey> keySelector;
        private readonly IComparer<TKey> comparer;
        private TKey[] keys;

        #endregion

        /// <inheritdoc />
        internal override int CompareKeys(int index1, int index2)
        {
            var c = comparer.Compare(keys[index1], keys[index2]);
            if (c == 0)
            {
                if (next == null)
                {
                    return index1 - index2;
                }

                return next.CompareKeys(index1, index2);
            }

            return descending ? -c : c;
        }

        /// <inheritdoc />
        internal override async AppaTask ComputeKeysAsync(TElement[] elements, int count)
        {
            keys = new TKey[count];
            for (var i = 0; i < count; i++)
            {
                keys[i] = keySelector(elements[i]);
            }

            if (next != null)
            {
                await next.ComputeKeysAsync(elements, count);
            }
        }
    }

    internal class AsyncSelectorEnumerableSorter<TElement, TKey> : AsyncEnumerableSorter<TElement>
    {
        internal AsyncSelectorEnumerableSorter(
            Func<TElement, AppaTask<TKey>> keySelector,
            IComparer<TKey> comparer,
            bool descending,
            AsyncEnumerableSorter<TElement> next)
        {
            this.keySelector = keySelector;
            this.comparer = comparer;
            this.descending = descending;
            this.next = next;
        }

        #region Fields and Autoproperties

        private readonly AsyncEnumerableSorter<TElement> next;
        private readonly bool descending;
        private readonly Func<TElement, AppaTask<TKey>> keySelector;
        private readonly IComparer<TKey> comparer;
        private TKey[] keys;

        #endregion

        /// <inheritdoc />
        internal override int CompareKeys(int index1, int index2)
        {
            var c = comparer.Compare(keys[index1], keys[index2]);
            if (c == 0)
            {
                if (next == null)
                {
                    return index1 - index2;
                }

                return next.CompareKeys(index1, index2);
            }

            return descending ? -c : c;
        }

        /// <inheritdoc />
        internal override async AppaTask ComputeKeysAsync(TElement[] elements, int count)
        {
            keys = new TKey[count];
            for (var i = 0; i < count; i++)
            {
                keys[i] = await keySelector(elements[i]);
            }

            if (next != null)
            {
                await next.ComputeKeysAsync(elements, count);
            }
        }
    }

    internal class
        AsyncSelectorWithCancellationEnumerableSorter<TElement, TKey> : AsyncEnumerableSorter<TElement>
    {
        internal AsyncSelectorWithCancellationEnumerableSorter(
            Func<TElement, CancellationToken, AppaTask<TKey>> keySelector,
            IComparer<TKey> comparer,
            bool descending,
            AsyncEnumerableSorter<TElement> next,
            CancellationToken cancellationToken)
        {
            this.keySelector = keySelector;
            this.comparer = comparer;
            this.descending = descending;
            this.next = next;
            this.cancellationToken = cancellationToken;
        }

        #region Fields and Autoproperties

        private readonly AsyncEnumerableSorter<TElement> next;
        private readonly bool descending;
        private readonly Func<TElement, CancellationToken, AppaTask<TKey>> keySelector;
        private readonly IComparer<TKey> comparer;
        private CancellationToken cancellationToken;
        private TKey[] keys;

        #endregion

        /// <inheritdoc />
        internal override int CompareKeys(int index1, int index2)
        {
            var c = comparer.Compare(keys[index1], keys[index2]);
            if (c == 0)
            {
                if (next == null)
                {
                    return index1 - index2;
                }

                return next.CompareKeys(index1, index2);
            }

            return descending ? -c : c;
        }

        /// <inheritdoc />
        internal override async AppaTask ComputeKeysAsync(TElement[] elements, int count)
        {
            keys = new TKey[count];
            for (var i = 0; i < count; i++)
            {
                keys[i] = await keySelector(elements[i], cancellationToken);
            }

            if (next != null)
            {
                await next.ComputeKeysAsync(elements, count);
            }
        }
    }

    internal abstract class OrderedAsyncEnumerable<TElement> : IAppaTaskOrderedAsyncEnumerable<TElement>
    {
        public OrderedAsyncEnumerable(IAppaTaskAsyncEnumerable<TElement> source)
        {
            this.source = source;
        }

        #region Fields and Autoproperties

        protected readonly IAppaTaskAsyncEnumerable<TElement> source;

        #endregion

        internal abstract AsyncEnumerableSorter<TElement> GetAsyncEnumerableSorter(
            AsyncEnumerableSorter<TElement> next,
            CancellationToken cancellationToken);

        #region IAppaTaskOrderedAsyncEnumerable<TElement> Members

        public IAppaTaskOrderedAsyncEnumerable<TElement> CreateOrderedEnumerable<TKey>(
            Func<TElement, TKey> keySelector,
            IComparer<TKey> comparer,
            bool descending)
        {
            return new OrderedAsyncEnumerable<TElement, TKey>(
                source,
                keySelector,
                comparer,
                descending,
                this
            );
        }

        public IAppaTaskOrderedAsyncEnumerable<TElement> CreateOrderedEnumerable<TKey>(
            Func<TElement, AppaTask<TKey>> keySelector,
            IComparer<TKey> comparer,
            bool descending)
        {
            return new OrderedAsyncEnumerableAwait<TElement, TKey>(
                source,
                keySelector,
                comparer,
                descending,
                this
            );
        }

        public IAppaTaskOrderedAsyncEnumerable<TElement> CreateOrderedEnumerable<TKey>(
            Func<TElement, CancellationToken, AppaTask<TKey>> keySelector,
            IComparer<TKey> comparer,
            bool descending)
        {
            return new OrderedAsyncEnumerableAwaitWithCancellation<TElement, TKey>(
                source,
                keySelector,
                comparer,
                descending,
                this
            );
        }

        public IAppaTaskAsyncEnumerator<TElement> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            return new _OrderedAsyncEnumerator(this, cancellationToken);
        }

        #endregion

        #region Nested type: _OrderedAsyncEnumerator

        private class _OrderedAsyncEnumerator : MoveNextSource, IAppaTaskAsyncEnumerator<TElement>
        {
            public _OrderedAsyncEnumerator(
                OrderedAsyncEnumerable<TElement> parent,
                CancellationToken cancellationToken)
            {
                this.parent = parent;
                this.cancellationToken = cancellationToken;
                TaskTracker.TrackActiveTask(this, 3);
            }

            #region Fields and Autoproperties

            protected readonly OrderedAsyncEnumerable<TElement> parent;
            private CancellationToken cancellationToken;
            private int index;
            private int[] map;
            private TElement[] buffer;

            #endregion

            private async AppaTaskVoid CreateSortSource()
            {
                try
                {
                    buffer = await parent.source.ToArrayAsync();
                    if (buffer.Length == 0)
                    {
                        completionSource.TrySetResult(false);
                        return;
                    }

                    var sorter = parent.GetAsyncEnumerableSorter(null, cancellationToken);
                    map = await sorter.SortAsync(buffer, buffer.Length);
                    sorter = null;

                    // set first value
                    Current = buffer[map[index++]];
                }
                catch (Exception ex)
                {
                    completionSource.TrySetException(ex);
                    return;
                }

                completionSource.TrySetResult(true);
            }

            #region IAppaTaskAsyncEnumerator<TElement> Members

            public TElement Current { get; private set; }

            public AppaTask<bool> MoveNextAsync()
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (map == null)
                {
                    completionSource.Reset();
                    CreateSortSource().Forget();
                    return new AppaTask<bool>(this, completionSource.Version);
                }

                if (index < buffer.Length)
                {
                    Current = buffer[map[index++]];
                    return CompletedTasks.True;
                }

                return CompletedTasks.False;
            }

            public AppaTask DisposeAsync()
            {
                TaskTracker.RemoveTracking(this);
                return default;
            }

            #endregion
        }

        #endregion
    }

    internal class OrderedAsyncEnumerable<TElement, TKey> : OrderedAsyncEnumerable<TElement>
    {
        public OrderedAsyncEnumerable(
            IAppaTaskAsyncEnumerable<TElement> source,
            Func<TElement, TKey> keySelector,
            IComparer<TKey> comparer,
            bool descending,
            OrderedAsyncEnumerable<TElement> parent) : base(source)
        {
            this.keySelector = keySelector;
            this.comparer = comparer;
            this.descending = descending;
            this.parent = parent;
        }

        #region Fields and Autoproperties

        private readonly bool descending;
        private readonly Func<TElement, TKey> keySelector;
        private readonly IComparer<TKey> comparer;
        private readonly OrderedAsyncEnumerable<TElement> parent;

        #endregion

        /// <inheritdoc />
        internal override AsyncEnumerableSorter<TElement> GetAsyncEnumerableSorter(
            AsyncEnumerableSorter<TElement> next,
            CancellationToken cancellationToken)
        {
            AsyncEnumerableSorter<TElement> sorter =
                new SyncSelectorAsyncEnumerableSorter<TElement, TKey>(
                    keySelector,
                    comparer,
                    descending,
                    next
                );
            if (parent != null)
            {
                sorter = parent.GetAsyncEnumerableSorter(sorter, cancellationToken);
            }

            return sorter;
        }
    }

    internal class OrderedAsyncEnumerableAwait<TElement, TKey> : OrderedAsyncEnumerable<TElement>
    {
        public OrderedAsyncEnumerableAwait(
            IAppaTaskAsyncEnumerable<TElement> source,
            Func<TElement, AppaTask<TKey>> keySelector,
            IComparer<TKey> comparer,
            bool descending,
            OrderedAsyncEnumerable<TElement> parent) : base(source)
        {
            this.keySelector = keySelector;
            this.comparer = comparer;
            this.descending = descending;
            this.parent = parent;
        }

        #region Fields and Autoproperties

        private readonly bool descending;
        private readonly Func<TElement, AppaTask<TKey>> keySelector;
        private readonly IComparer<TKey> comparer;
        private readonly OrderedAsyncEnumerable<TElement> parent;

        #endregion

        /// <inheritdoc />
        internal override AsyncEnumerableSorter<TElement> GetAsyncEnumerableSorter(
            AsyncEnumerableSorter<TElement> next,
            CancellationToken cancellationToken)
        {
            AsyncEnumerableSorter<TElement> sorter =
                new AsyncSelectorEnumerableSorter<TElement, TKey>(keySelector, comparer, descending, next);
            if (parent != null)
            {
                sorter = parent.GetAsyncEnumerableSorter(sorter, cancellationToken);
            }

            return sorter;
        }
    }

    internal class
        OrderedAsyncEnumerableAwaitWithCancellation<TElement, TKey> : OrderedAsyncEnumerable<TElement>
    {
        public OrderedAsyncEnumerableAwaitWithCancellation(
            IAppaTaskAsyncEnumerable<TElement> source,
            Func<TElement, CancellationToken, AppaTask<TKey>> keySelector,
            IComparer<TKey> comparer,
            bool descending,
            OrderedAsyncEnumerable<TElement> parent) : base(source)
        {
            this.keySelector = keySelector;
            this.comparer = comparer;
            this.descending = descending;
            this.parent = parent;
        }

        #region Fields and Autoproperties

        private readonly bool descending;
        private readonly Func<TElement, CancellationToken, AppaTask<TKey>> keySelector;
        private readonly IComparer<TKey> comparer;
        private readonly OrderedAsyncEnumerable<TElement> parent;

        #endregion

        /// <inheritdoc />
        internal override AsyncEnumerableSorter<TElement> GetAsyncEnumerableSorter(
            AsyncEnumerableSorter<TElement> next,
            CancellationToken cancellationToken)
        {
            AsyncEnumerableSorter<TElement> sorter =
                new AsyncSelectorWithCancellationEnumerableSorter<TElement, TKey>(
                    keySelector,
                    comparer,
                    descending,
                    next,
                    cancellationToken
                );
            if (parent != null)
            {
                sorter = parent.GetAsyncEnumerableSorter(sorter, cancellationToken);
            }

            return sorter;
        }
    }
}
