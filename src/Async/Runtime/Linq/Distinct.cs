using System;
using System.Collections.Generic;
using System.Threading;
using Appalachia.Utility.Async.Internal;

namespace Appalachia.Utility.Async.Linq
{
    public static partial class AppaTaskAsyncEnumerable
    {
        public static IAppaTaskAsyncEnumerable<TSource> Distinct<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source)
        {
            return Distinct(source, EqualityComparer<TSource>.Default);
        }

        public static IAppaTaskAsyncEnumerable<TSource> Distinct<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            IEqualityComparer<TSource> comparer)
        {
            Error.ThrowArgumentNullException(source,   nameof(source));
            Error.ThrowArgumentNullException(comparer, nameof(comparer));

            return new Distinct<TSource>(source, comparer);
        }

        public static IAppaTaskAsyncEnumerable<TSource> Distinct<TSource, TKey>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, TKey> keySelector)
        {
            return Distinct(source, keySelector, EqualityComparer<TKey>.Default);
        }

        public static IAppaTaskAsyncEnumerable<TSource> Distinct<TSource, TKey>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            IEqualityComparer<TKey> comparer)
        {
            Error.ThrowArgumentNullException(source,      nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));
            Error.ThrowArgumentNullException(comparer,    nameof(comparer));

            return new Distinct<TSource, TKey>(source, keySelector, comparer);
        }

        public static IAppaTaskAsyncEnumerable<TSource> DistinctAwait<TSource, TKey>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<TKey>> keySelector)
        {
            return DistinctAwait(source, keySelector, EqualityComparer<TKey>.Default);
        }

        public static IAppaTaskAsyncEnumerable<TSource> DistinctAwait<TSource, TKey>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<TKey>> keySelector,
            IEqualityComparer<TKey> comparer)
        {
            Error.ThrowArgumentNullException(source,      nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));
            Error.ThrowArgumentNullException(comparer,    nameof(comparer));

            return new DistinctAwait<TSource, TKey>(source, keySelector, comparer);
        }

        public static IAppaTaskAsyncEnumerable<TSource> DistinctAwaitWithCancellation<TSource, TKey>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<TKey>> keySelector)
        {
            return DistinctAwaitWithCancellation(source, keySelector, EqualityComparer<TKey>.Default);
        }

        public static IAppaTaskAsyncEnumerable<TSource> DistinctAwaitWithCancellation<TSource, TKey>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<TKey>> keySelector,
            IEqualityComparer<TKey> comparer)
        {
            Error.ThrowArgumentNullException(source,      nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));
            Error.ThrowArgumentNullException(comparer,    nameof(comparer));

            return new DistinctAwaitWithCancellation<TSource, TKey>(source, keySelector, comparer);
        }
    }

    internal sealed class Distinct<TSource> : IAppaTaskAsyncEnumerable<TSource>
    {
        private readonly IAppaTaskAsyncEnumerable<TSource> source;
        private readonly IEqualityComparer<TSource> comparer;

        public Distinct(IAppaTaskAsyncEnumerable<TSource> source, IEqualityComparer<TSource> comparer)
        {
            this.source = source;
            this.comparer = comparer;
        }

        public IAppaTaskAsyncEnumerator<TSource> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            return new _Distinct(source, comparer, cancellationToken);
        }

        private class _Distinct : AsyncEnumeratorBase<TSource, TSource>
        {
            private readonly HashSet<TSource> set;

            public _Distinct(
                IAppaTaskAsyncEnumerable<TSource> source,
                IEqualityComparer<TSource> comparer,
                CancellationToken cancellationToken) : base(source, cancellationToken)
            {
                set = new HashSet<TSource>(comparer);
            }

            protected override bool TryMoveNextCore(bool sourceHasCurrent, out bool result)
            {
                if (sourceHasCurrent)
                {
                    var v = SourceCurrent;
                    if (set.Add(v))
                    {
                        Current = v;
                        result = true;
                        return true;
                    }

                    result = default;
                    return false;
                }

                result = false;
                return true;
            }
        }
    }

    internal sealed class Distinct<TSource, TKey> : IAppaTaskAsyncEnumerable<TSource>
    {
        private readonly IAppaTaskAsyncEnumerable<TSource> source;
        private readonly Func<TSource, TKey> keySelector;
        private readonly IEqualityComparer<TKey> comparer;

        public Distinct(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            IEqualityComparer<TKey> comparer)
        {
            this.source = source;
            this.keySelector = keySelector;
            this.comparer = comparer;
        }

        public IAppaTaskAsyncEnumerator<TSource> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            return new _Distinct(source, keySelector, comparer, cancellationToken);
        }

        private class _Distinct : AsyncEnumeratorBase<TSource, TSource>
        {
            private readonly HashSet<TKey> set;
            private readonly Func<TSource, TKey> keySelector;

            public _Distinct(
                IAppaTaskAsyncEnumerable<TSource> source,
                Func<TSource, TKey> keySelector,
                IEqualityComparer<TKey> comparer,
                CancellationToken cancellationToken) : base(source, cancellationToken)
            {
                set = new HashSet<TKey>(comparer);
                this.keySelector = keySelector;
            }

            protected override bool TryMoveNextCore(bool sourceHasCurrent, out bool result)
            {
                if (sourceHasCurrent)
                {
                    var v = SourceCurrent;
                    if (set.Add(keySelector(v)))
                    {
                        Current = v;
                        result = true;
                        return true;
                    }

                    result = default;
                    return false;
                }

                result = false;
                return true;
            }
        }
    }

    internal sealed class DistinctAwait<TSource, TKey> : IAppaTaskAsyncEnumerable<TSource>
    {
        private readonly IAppaTaskAsyncEnumerable<TSource> source;
        private readonly Func<TSource, AppaTask<TKey>> keySelector;
        private readonly IEqualityComparer<TKey> comparer;

        public DistinctAwait(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<TKey>> keySelector,
            IEqualityComparer<TKey> comparer)
        {
            this.source = source;
            this.keySelector = keySelector;
            this.comparer = comparer;
        }

        public IAppaTaskAsyncEnumerator<TSource> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            return new _DistinctAwait(source, keySelector, comparer, cancellationToken);
        }

        private class _DistinctAwait : AsyncEnumeratorAwaitSelectorBase<TSource, TSource, TKey>
        {
            private readonly HashSet<TKey> set;
            private readonly Func<TSource, AppaTask<TKey>> keySelector;

            public _DistinctAwait(
                IAppaTaskAsyncEnumerable<TSource> source,
                Func<TSource, AppaTask<TKey>> keySelector,
                IEqualityComparer<TKey> comparer,
                CancellationToken cancellationToken) : base(source, cancellationToken)
            {
                set = new HashSet<TKey>(comparer);
                this.keySelector = keySelector;
            }

            protected override AppaTask<TKey> TransformAsync(TSource sourceCurrent)
            {
                return keySelector(sourceCurrent);
            }

            protected override bool TrySetCurrentCore(TKey awaitResult, out bool terminateIteration)
            {
                if (set.Add(awaitResult))
                {
                    Current = SourceCurrent;
                    terminateIteration = false;
                    return true;
                }

                terminateIteration = false;
                return false;
            }
        }
    }

    internal sealed class DistinctAwaitWithCancellation<TSource, TKey> : IAppaTaskAsyncEnumerable<TSource>
    {
        private readonly IAppaTaskAsyncEnumerable<TSource> source;
        private readonly Func<TSource, CancellationToken, AppaTask<TKey>> keySelector;
        private readonly IEqualityComparer<TKey> comparer;

        public DistinctAwaitWithCancellation(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<TKey>> keySelector,
            IEqualityComparer<TKey> comparer)
        {
            this.source = source;
            this.keySelector = keySelector;
            this.comparer = comparer;
        }

        public IAppaTaskAsyncEnumerator<TSource> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            return new _DistinctAwaitWithCancellation(source, keySelector, comparer, cancellationToken);
        }

        private class
            _DistinctAwaitWithCancellation : AsyncEnumeratorAwaitSelectorBase<TSource, TSource, TKey>
        {
            private readonly HashSet<TKey> set;
            private readonly Func<TSource, CancellationToken, AppaTask<TKey>> keySelector;

            public _DistinctAwaitWithCancellation(
                IAppaTaskAsyncEnumerable<TSource> source,
                Func<TSource, CancellationToken, AppaTask<TKey>> keySelector,
                IEqualityComparer<TKey> comparer,
                CancellationToken cancellationToken) : base(source, cancellationToken)
            {
                set = new HashSet<TKey>(comparer);
                this.keySelector = keySelector;
            }

            protected override AppaTask<TKey> TransformAsync(TSource sourceCurrent)
            {
                return keySelector(sourceCurrent, cancellationToken);
            }

            protected override bool TrySetCurrentCore(TKey awaitResult, out bool terminateIteration)
            {
                if (set.Add(awaitResult))
                {
                    Current = SourceCurrent;
                    terminateIteration = false;
                    return true;
                }

                terminateIteration = false;
                return false;
            }
        }
    }
}
