using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Appalachia.Utility.Async.Internal;

namespace Appalachia.Utility.Async.Linq
{
    public static partial class AppaTaskAsyncEnumerable
    {
        public static AppaTask<ILookup<TKey, TSource>> ToLookupAsync<TSource, TKey>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source,      nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));

            return ToLookup.ToLookupAsync(
                source,
                keySelector,
                EqualityComparer<TKey>.Default,
                cancellationToken
            );
        }

        public static AppaTask<ILookup<TKey, TSource>> ToLookupAsync<TSource, TKey>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            IEqualityComparer<TKey> comparer,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source,      nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));
            Error.ThrowArgumentNullException(comparer,    nameof(comparer));

            return ToLookup.ToLookupAsync(source, keySelector, comparer, cancellationToken);
        }

        public static AppaTask<ILookup<TKey, TElement>> ToLookupAsync<TSource, TKey, TElement>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            Func<TSource, TElement> elementSelector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source,          nameof(source));
            Error.ThrowArgumentNullException(keySelector,     nameof(keySelector));
            Error.ThrowArgumentNullException(elementSelector, nameof(elementSelector));

            return ToLookup.ToLookupAsync(
                source,
                keySelector,
                elementSelector,
                EqualityComparer<TKey>.Default,
                cancellationToken
            );
        }

        public static AppaTask<ILookup<TKey, TElement>> ToLookupAsync<TSource, TKey, TElement>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            Func<TSource, TElement> elementSelector,
            IEqualityComparer<TKey> comparer,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source,          nameof(source));
            Error.ThrowArgumentNullException(keySelector,     nameof(keySelector));
            Error.ThrowArgumentNullException(elementSelector, nameof(elementSelector));
            Error.ThrowArgumentNullException(comparer,        nameof(comparer));

            return ToLookup.ToLookupAsync(source, keySelector, elementSelector, comparer, cancellationToken);
        }

        public static AppaTask<ILookup<TKey, TSource>> ToLookupAwaitAsync<TSource, TKey>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<TKey>> keySelector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source,      nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));

            return ToLookup.ToLookupAwaitAsync(
                source,
                keySelector,
                EqualityComparer<TKey>.Default,
                cancellationToken
            );
        }

        public static AppaTask<ILookup<TKey, TSource>> ToLookupAwaitAsync<TSource, TKey>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<TKey>> keySelector,
            IEqualityComparer<TKey> comparer,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source,      nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));
            Error.ThrowArgumentNullException(comparer,    nameof(comparer));

            return ToLookup.ToLookupAwaitAsync(source, keySelector, comparer, cancellationToken);
        }

        public static AppaTask<ILookup<TKey, TElement>> ToLookupAwaitAsync<TSource, TKey, TElement>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<TKey>> keySelector,
            Func<TSource, AppaTask<TElement>> elementSelector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source,          nameof(source));
            Error.ThrowArgumentNullException(keySelector,     nameof(keySelector));
            Error.ThrowArgumentNullException(elementSelector, nameof(elementSelector));

            return ToLookup.ToLookupAwaitAsync(
                source,
                keySelector,
                elementSelector,
                EqualityComparer<TKey>.Default,
                cancellationToken
            );
        }

        public static AppaTask<ILookup<TKey, TElement>> ToLookupAwaitAsync<TSource, TKey, TElement>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<TKey>> keySelector,
            Func<TSource, AppaTask<TElement>> elementSelector,
            IEqualityComparer<TKey> comparer,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source,          nameof(source));
            Error.ThrowArgumentNullException(keySelector,     nameof(keySelector));
            Error.ThrowArgumentNullException(elementSelector, nameof(elementSelector));
            Error.ThrowArgumentNullException(comparer,        nameof(comparer));

            return ToLookup.ToLookupAwaitAsync(
                source,
                keySelector,
                elementSelector,
                comparer,
                cancellationToken
            );
        }

        public static AppaTask<ILookup<TKey, TSource>> ToLookupAwaitWithCancellationAsync<TSource, TKey>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<TKey>> keySelector,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source,      nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));

            return ToLookup.ToLookupAwaitWithCancellationAsync(
                source,
                keySelector,
                EqualityComparer<TKey>.Default,
                cancellationToken
            );
        }

        public static AppaTask<ILookup<TKey, TSource>> ToLookupAwaitWithCancellationAsync<TSource, TKey>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<TKey>> keySelector,
            IEqualityComparer<TKey> comparer,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source,      nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));
            Error.ThrowArgumentNullException(comparer,    nameof(comparer));

            return ToLookup.ToLookupAwaitWithCancellationAsync(
                source,
                keySelector,
                comparer,
                cancellationToken
            );
        }

        public static AppaTask<ILookup<TKey, TElement>>
            ToLookupAwaitWithCancellationAsync<TSource, TKey, TElement>(
                this IAppaTaskAsyncEnumerable<TSource> source,
                Func<TSource, CancellationToken, AppaTask<TKey>> keySelector,
                Func<TSource, CancellationToken, AppaTask<TElement>> elementSelector,
                CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source,          nameof(source));
            Error.ThrowArgumentNullException(keySelector,     nameof(keySelector));
            Error.ThrowArgumentNullException(elementSelector, nameof(elementSelector));

            return ToLookup.ToLookupAwaitWithCancellationAsync(
                source,
                keySelector,
                elementSelector,
                EqualityComparer<TKey>.Default,
                cancellationToken
            );
        }

        public static AppaTask<ILookup<TKey, TElement>>
            ToLookupAwaitWithCancellationAsync<TSource, TKey, TElement>(
                this IAppaTaskAsyncEnumerable<TSource> source,
                Func<TSource, CancellationToken, AppaTask<TKey>> keySelector,
                Func<TSource, CancellationToken, AppaTask<TElement>> elementSelector,
                IEqualityComparer<TKey> comparer,
                CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source,          nameof(source));
            Error.ThrowArgumentNullException(keySelector,     nameof(keySelector));
            Error.ThrowArgumentNullException(elementSelector, nameof(elementSelector));
            Error.ThrowArgumentNullException(comparer,        nameof(comparer));

            return ToLookup.ToLookupAwaitWithCancellationAsync(
                source,
                keySelector,
                elementSelector,
                comparer,
                cancellationToken
            );
        }
    }

    internal static class ToLookup
    {
        internal static async AppaTask<ILookup<TKey, TSource>> ToLookupAsync<TSource, TKey>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            IEqualityComparer<TKey> comparer,
            CancellationToken cancellationToken)
        {
            var pool = ArrayPool<TSource>.Shared;
            var array = pool.Rent(16);

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                var i = 0;
                while (await e.MoveNextAsync())
                {
                    ArrayPoolUtil.EnsureCapacity(ref array, i, pool);
                    array[i++] = e.Current;
                }

                if (i == 0)
                {
                    return Lookup<TKey, TSource>.CreateEmpty();
                }
                else
                {
                    return Lookup<TKey, TSource>.Create(
                        new ArraySegment<TSource>(array, 0, i),
                        keySelector,
                        comparer
                    );
                }
            }
            finally
            {
                pool.Return(array, !RuntimeHelpersAbstraction.IsWellKnownNoReferenceContainsType<TSource>());

                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }
        }

        internal static async AppaTask<ILookup<TKey, TElement>> ToLookupAsync<TSource, TKey, TElement>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            Func<TSource, TElement> elementSelector,
            IEqualityComparer<TKey> comparer,
            CancellationToken cancellationToken)
        {
            var pool = ArrayPool<TSource>.Shared;
            var array = pool.Rent(16);

            IAppaTaskAsyncEnumerator<TSource> e = default;
            try
            {
                e = source.GetAsyncEnumerator(cancellationToken);
                var i = 0;
                while (await e.MoveNextAsync())
                {
                    ArrayPoolUtil.EnsureCapacity(ref array, i, pool);
                    array[i++] = e.Current;
                }

                if (i == 0)
                {
                    return Lookup<TKey, TElement>.CreateEmpty();
                }
                else
                {
                    return Lookup<TKey, TElement>.Create(
                        new ArraySegment<TSource>(array, 0, i),
                        keySelector,
                        elementSelector,
                        comparer
                    );
                }
            }
            finally
            {
                pool.Return(array, !RuntimeHelpersAbstraction.IsWellKnownNoReferenceContainsType<TSource>());

                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }
        }

        // with await

        internal static async AppaTask<ILookup<TKey, TSource>> ToLookupAwaitAsync<TSource, TKey>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<TKey>> keySelector,
            IEqualityComparer<TKey> comparer,
            CancellationToken cancellationToken)
        {
            var pool = ArrayPool<TSource>.Shared;
            var array = pool.Rent(16);

            IAppaTaskAsyncEnumerator<TSource> e = default;
            try
            {
                e = source.GetAsyncEnumerator(cancellationToken);
                var i = 0;
                while (await e.MoveNextAsync())
                {
                    ArrayPoolUtil.EnsureCapacity(ref array, i, pool);
                    array[i++] = e.Current;
                }

                if (i == 0)
                {
                    return Lookup<TKey, TSource>.CreateEmpty();
                }
                else
                {
                    return await Lookup<TKey, TSource>.CreateAsync(
                        new ArraySegment<TSource>(array, 0, i),
                        keySelector,
                        comparer
                    );
                }
            }
            finally
            {
                pool.Return(array, !RuntimeHelpersAbstraction.IsWellKnownNoReferenceContainsType<TSource>());

                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }
        }

        internal static async AppaTask<ILookup<TKey, TElement>> ToLookupAwaitAsync<TSource, TKey, TElement>(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<TKey>> keySelector,
            Func<TSource, AppaTask<TElement>> elementSelector,
            IEqualityComparer<TKey> comparer,
            CancellationToken cancellationToken)
        {
            var pool = ArrayPool<TSource>.Shared;
            var array = pool.Rent(16);

            IAppaTaskAsyncEnumerator<TSource> e = default;
            try
            {
                e = source.GetAsyncEnumerator(cancellationToken);
                var i = 0;
                while (await e.MoveNextAsync())
                {
                    ArrayPoolUtil.EnsureCapacity(ref array, i, pool);
                    array[i++] = e.Current;
                }

                if (i == 0)
                {
                    return Lookup<TKey, TElement>.CreateEmpty();
                }
                else
                {
                    return await Lookup<TKey, TElement>.CreateAsync(
                        new ArraySegment<TSource>(array, 0, i),
                        keySelector,
                        elementSelector,
                        comparer
                    );
                }
            }
            finally
            {
                pool.Return(array, !RuntimeHelpersAbstraction.IsWellKnownNoReferenceContainsType<TSource>());

                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }
        }

        // with cancellation

        internal static async AppaTask<ILookup<TKey, TSource>>
            ToLookupAwaitWithCancellationAsync<TSource, TKey>(
                IAppaTaskAsyncEnumerable<TSource> source,
                Func<TSource, CancellationToken, AppaTask<TKey>> keySelector,
                IEqualityComparer<TKey> comparer,
                CancellationToken cancellationToken)
        {
            var pool = ArrayPool<TSource>.Shared;
            var array = pool.Rent(16);

            IAppaTaskAsyncEnumerator<TSource> e = default;
            try
            {
                e = source.GetAsyncEnumerator(cancellationToken);
                var i = 0;
                while (await e.MoveNextAsync())
                {
                    ArrayPoolUtil.EnsureCapacity(ref array, i, pool);
                    array[i++] = e.Current;
                }

                if (i == 0)
                {
                    return Lookup<TKey, TSource>.CreateEmpty();
                }
                else
                {
                    return await Lookup<TKey, TSource>.CreateAsync(
                        new ArraySegment<TSource>(array, 0, i),
                        keySelector,
                        comparer,
                        cancellationToken
                    );
                }
            }
            finally
            {
                pool.Return(array, !RuntimeHelpersAbstraction.IsWellKnownNoReferenceContainsType<TSource>());

                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }
        }

        internal static async AppaTask<ILookup<TKey, TElement>>
            ToLookupAwaitWithCancellationAsync<TSource, TKey, TElement>(
                IAppaTaskAsyncEnumerable<TSource> source,
                Func<TSource, CancellationToken, AppaTask<TKey>> keySelector,
                Func<TSource, CancellationToken, AppaTask<TElement>> elementSelector,
                IEqualityComparer<TKey> comparer,
                CancellationToken cancellationToken)
        {
            var pool = ArrayPool<TSource>.Shared;
            var array = pool.Rent(16);

            IAppaTaskAsyncEnumerator<TSource> e = default;
            try
            {
                e = source.GetAsyncEnumerator(cancellationToken);
                var i = 0;
                while (await e.MoveNextAsync())
                {
                    ArrayPoolUtil.EnsureCapacity(ref array, i, pool);
                    array[i++] = e.Current;
                }

                if (i == 0)
                {
                    return Lookup<TKey, TElement>.CreateEmpty();
                }
                else
                {
                    return await Lookup<TKey, TElement>.CreateAsync(
                        new ArraySegment<TSource>(array, 0, i),
                        keySelector,
                        elementSelector,
                        comparer,
                        cancellationToken
                    );
                }
            }
            finally
            {
                pool.Return(array, !RuntimeHelpersAbstraction.IsWellKnownNoReferenceContainsType<TSource>());

                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }
        }

        #region Nested type: Grouping

        private class
            Grouping<TKey, TElement> : IGrouping<TKey, TElement> // , IAppaTaskAsyncGrouping<TKey, TElement>
        {
            public Grouping(TKey key)
            {
                Key = key;
                elements = new List<TElement>();
            }

            #region Fields and Autoproperties

            private readonly List<TElement> elements;

            #endregion

            /// <inheritdoc />
            public override string ToString()
            {
                return "Key: " + Key + ", Count: " + elements.Count;
            }

            public void Add(TElement value)
            {
                elements.Add(value);
            }

            public IAppaTaskAsyncEnumerator<TElement> GetAsyncEnumerator(
                CancellationToken cancellationToken = default)
            {
                return this.ToAppaTaskAsyncEnumerable().GetAsyncEnumerator(cancellationToken);
            }

            #region IGrouping<TKey,TElement> Members

            public TKey Key { get; private set; }

            public IEnumerator<TElement> GetEnumerator()
            {
                return elements.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return elements.GetEnumerator();
            }

            #endregion
        }

        #endregion

        #region Nested type: Lookup

        // Lookup

        private class Lookup<TKey, TElement> : ILookup<TKey, TElement>
        {
            #region Constants and Static Readonly

            private static readonly Lookup<TKey, TElement> empty =
                new Lookup<TKey, TElement>(new Dictionary<TKey, Grouping<TKey, TElement>>());

            #endregion

            private Lookup(Dictionary<TKey, Grouping<TKey, TElement>> dict)
            {
                this.dict = dict;
            }

            #region Fields and Autoproperties

            // original lookup keeps order but this impl does not(dictionary not guarantee)
            private readonly Dictionary<TKey, Grouping<TKey, TElement>> dict;

            #endregion

            public static Lookup<TKey, TElement> Create(
                ArraySegment<TElement> source,
                Func<TElement, TKey> keySelector,
                IEqualityComparer<TKey> comparer)
            {
                var dict = new Dictionary<TKey, Grouping<TKey, TElement>>(comparer);

                var arr = source.Array;
                var c = source.Count;
                for (var i = source.Offset; i < c; i++)
                {
                    var key = keySelector(arr[i]);

                    if (!dict.TryGetValue(key, out var list))
                    {
                        list = new Grouping<TKey, TElement>(key);
                        dict[key] = list;
                    }

                    list.Add(arr[i]);
                }

                return new Lookup<TKey, TElement>(dict);
            }

            public static Lookup<TKey, TElement> Create<TSource>(
                ArraySegment<TSource> source,
                Func<TSource, TKey> keySelector,
                Func<TSource, TElement> elementSelector,
                IEqualityComparer<TKey> comparer)
            {
                var dict = new Dictionary<TKey, Grouping<TKey, TElement>>(comparer);

                var arr = source.Array;
                var c = source.Count;
                for (var i = source.Offset; i < c; i++)
                {
                    var key = keySelector(arr[i]);
                    var elem = elementSelector(arr[i]);

                    if (!dict.TryGetValue(key, out var list))
                    {
                        list = new Grouping<TKey, TElement>(key);
                        dict[key] = list;
                    }

                    list.Add(elem);
                }

                return new Lookup<TKey, TElement>(dict);
            }

            public static async AppaTask<Lookup<TKey, TElement>> CreateAsync(
                ArraySegment<TElement> source,
                Func<TElement, AppaTask<TKey>> keySelector,
                IEqualityComparer<TKey> comparer)
            {
                var dict = new Dictionary<TKey, Grouping<TKey, TElement>>(comparer);

                var arr = source.Array;
                var c = source.Count;
                for (var i = source.Offset; i < c; i++)
                {
                    var key = await keySelector(arr[i]);

                    if (!dict.TryGetValue(key, out var list))
                    {
                        list = new Grouping<TKey, TElement>(key);
                        dict[key] = list;
                    }

                    list.Add(arr[i]);
                }

                return new Lookup<TKey, TElement>(dict);
            }

            public static async AppaTask<Lookup<TKey, TElement>> CreateAsync<TSource>(
                ArraySegment<TSource> source,
                Func<TSource, AppaTask<TKey>> keySelector,
                Func<TSource, AppaTask<TElement>> elementSelector,
                IEqualityComparer<TKey> comparer)
            {
                var dict = new Dictionary<TKey, Grouping<TKey, TElement>>(comparer);

                var arr = source.Array;
                var c = source.Count;
                for (var i = source.Offset; i < c; i++)
                {
                    var key = await keySelector(arr[i]);
                    var elem = await elementSelector(arr[i]);

                    if (!dict.TryGetValue(key, out var list))
                    {
                        list = new Grouping<TKey, TElement>(key);
                        dict[key] = list;
                    }

                    list.Add(elem);
                }

                return new Lookup<TKey, TElement>(dict);
            }

            public static async AppaTask<Lookup<TKey, TElement>> CreateAsync(
                ArraySegment<TElement> source,
                Func<TElement, CancellationToken, AppaTask<TKey>> keySelector,
                IEqualityComparer<TKey> comparer,
                CancellationToken cancellationToken)
            {
                var dict = new Dictionary<TKey, Grouping<TKey, TElement>>(comparer);

                var arr = source.Array;
                var c = source.Count;
                for (var i = source.Offset; i < c; i++)
                {
                    var key = await keySelector(arr[i], cancellationToken);

                    if (!dict.TryGetValue(key, out var list))
                    {
                        list = new Grouping<TKey, TElement>(key);
                        dict[key] = list;
                    }

                    list.Add(arr[i]);
                }

                return new Lookup<TKey, TElement>(dict);
            }

            public static async AppaTask<Lookup<TKey, TElement>> CreateAsync<TSource>(
                ArraySegment<TSource> source,
                Func<TSource, CancellationToken, AppaTask<TKey>> keySelector,
                Func<TSource, CancellationToken, AppaTask<TElement>> elementSelector,
                IEqualityComparer<TKey> comparer,
                CancellationToken cancellationToken)
            {
                var dict = new Dictionary<TKey, Grouping<TKey, TElement>>(comparer);

                var arr = source.Array;
                var c = source.Count;
                for (var i = source.Offset; i < c; i++)
                {
                    var key = await keySelector(arr[i], cancellationToken);
                    var elem = await elementSelector(arr[i], cancellationToken);

                    if (!dict.TryGetValue(key, out var list))
                    {
                        list = new Grouping<TKey, TElement>(key);
                        dict[key] = list;
                    }

                    list.Add(elem);
                }

                return new Lookup<TKey, TElement>(dict);
            }

            public static Lookup<TKey, TElement> CreateEmpty()
            {
                return empty;
            }

            #region ILookup<TKey,TElement> Members

            public IEnumerable<TElement> this[TKey key] =>
                dict.TryGetValue(key, out var g) ? g : Enumerable.Empty<TElement>();

            public int Count => dict.Count;

            public bool Contains(TKey key)
            {
                return dict.ContainsKey(key);
            }

            public IEnumerator<IGrouping<TKey, TElement>> GetEnumerator()
            {
                return dict.Values.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return dict.Values.GetEnumerator();
            }

            #endregion
        }

        #endregion
    }
}
