using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Appalachia.Utility.Async.Internal;

namespace Appalachia.Utility.Async.Linq
{
    public static partial class AppaTaskAsyncEnumerable
    {
        // Ix-Async returns IGrouping but it is competely waste, use standard IGrouping.

        public static IAppaTaskAsyncEnumerable<IGrouping<TKey, TSource>> GroupBy<TSource, TKey>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, TKey> keySelector)
        {
            Error.ThrowArgumentNullException(source,      nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));
            return new GroupBy<TSource, TKey, TSource>(
                source,
                keySelector,
                x => x,
                EqualityComparer<TKey>.Default
            );
        }

        public static IAppaTaskAsyncEnumerable<IGrouping<TKey, TSource>> GroupBy<TSource, TKey>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            IEqualityComparer<TKey> comparer)
        {
            Error.ThrowArgumentNullException(source,      nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));
            Error.ThrowArgumentNullException(comparer,    nameof(comparer));
            return new GroupBy<TSource, TKey, TSource>(source, keySelector, x => x, comparer);
        }

        public static IAppaTaskAsyncEnumerable<IGrouping<TKey, TElement>> GroupBy<TSource, TKey, TElement>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            Func<TSource, TElement> elementSelector)
        {
            Error.ThrowArgumentNullException(source,          nameof(source));
            Error.ThrowArgumentNullException(keySelector,     nameof(keySelector));
            Error.ThrowArgumentNullException(elementSelector, nameof(elementSelector));
            return new GroupBy<TSource, TKey, TElement>(
                source,
                keySelector,
                elementSelector,
                EqualityComparer<TKey>.Default
            );
        }

        public static IAppaTaskAsyncEnumerable<IGrouping<TKey, TElement>> GroupBy<TSource, TKey, TElement>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            Func<TSource, TElement> elementSelector,
            IEqualityComparer<TKey> comparer)
        {
            Error.ThrowArgumentNullException(source,          nameof(source));
            Error.ThrowArgumentNullException(keySelector,     nameof(keySelector));
            Error.ThrowArgumentNullException(elementSelector, nameof(elementSelector));
            Error.ThrowArgumentNullException(comparer,        nameof(comparer));
            return new GroupBy<TSource, TKey, TElement>(source, keySelector, elementSelector, comparer);
        }

        public static IAppaTaskAsyncEnumerable<TResult> GroupBy<TSource, TKey, TResult>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            Func<TKey, IEnumerable<TSource>, TResult> resultSelector)
        {
            Error.ThrowArgumentNullException(source,         nameof(source));
            Error.ThrowArgumentNullException(keySelector,    nameof(keySelector));
            Error.ThrowArgumentNullException(resultSelector, nameof(resultSelector));
            return new GroupBy<TSource, TKey, TSource, TResult>(
                source,
                keySelector,
                x => x,
                resultSelector,
                EqualityComparer<TKey>.Default
            );
        }

        public static IAppaTaskAsyncEnumerable<TResult> GroupBy<TSource, TKey, TResult>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            Func<TKey, IEnumerable<TSource>, TResult> resultSelector,
            IEqualityComparer<TKey> comparer)
        {
            Error.ThrowArgumentNullException(source,         nameof(source));
            Error.ThrowArgumentNullException(keySelector,    nameof(keySelector));
            Error.ThrowArgumentNullException(resultSelector, nameof(resultSelector));
            Error.ThrowArgumentNullException(comparer,       nameof(comparer));
            return new GroupBy<TSource, TKey, TSource, TResult>(
                source,
                keySelector,
                x => x,
                resultSelector,
                comparer
            );
        }

        public static IAppaTaskAsyncEnumerable<TResult> GroupBy<TSource, TKey, TElement, TResult>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            Func<TSource, TElement> elementSelector,
            Func<TKey, IEnumerable<TElement>, TResult> resultSelector)
        {
            Error.ThrowArgumentNullException(source,          nameof(source));
            Error.ThrowArgumentNullException(keySelector,     nameof(keySelector));
            Error.ThrowArgumentNullException(elementSelector, nameof(elementSelector));
            Error.ThrowArgumentNullException(resultSelector,  nameof(resultSelector));
            return new GroupBy<TSource, TKey, TElement, TResult>(
                source,
                keySelector,
                elementSelector,
                resultSelector,
                EqualityComparer<TKey>.Default
            );
        }

        public static IAppaTaskAsyncEnumerable<TResult> GroupBy<TSource, TKey, TElement, TResult>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            Func<TSource, TElement> elementSelector,
            Func<TKey, IEnumerable<TElement>, TResult> resultSelector,
            IEqualityComparer<TKey> comparer)
        {
            Error.ThrowArgumentNullException(source,          nameof(source));
            Error.ThrowArgumentNullException(keySelector,     nameof(keySelector));
            Error.ThrowArgumentNullException(elementSelector, nameof(elementSelector));
            Error.ThrowArgumentNullException(resultSelector,  nameof(resultSelector));
            Error.ThrowArgumentNullException(comparer,        nameof(comparer));
            return new GroupBy<TSource, TKey, TElement, TResult>(
                source,
                keySelector,
                elementSelector,
                resultSelector,
                comparer
            );
        }

        // await

        public static IAppaTaskAsyncEnumerable<IGrouping<TKey, TSource>> GroupByAwait<TSource, TKey>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<TKey>> keySelector)
        {
            Error.ThrowArgumentNullException(source,      nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));
            return new GroupByAwait<TSource, TKey, TSource>(
                source,
                keySelector,
                x => AppaTask.FromResult(x),
                EqualityComparer<TKey>.Default
            );
        }

        public static IAppaTaskAsyncEnumerable<IGrouping<TKey, TSource>> GroupByAwait<TSource, TKey>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<TKey>> keySelector,
            IEqualityComparer<TKey> comparer)
        {
            Error.ThrowArgumentNullException(source,      nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));
            Error.ThrowArgumentNullException(comparer,    nameof(comparer));
            return new GroupByAwait<TSource, TKey, TSource>(
                source,
                keySelector,
                x => AppaTask.FromResult(x),
                comparer
            );
        }

        public static IAppaTaskAsyncEnumerable<IGrouping<TKey, TElement>>
            GroupByAwait<TSource, TKey, TElement>(
                this IAppaTaskAsyncEnumerable<TSource> source,
                Func<TSource, AppaTask<TKey>> keySelector,
                Func<TSource, AppaTask<TElement>> elementSelector)
        {
            Error.ThrowArgumentNullException(source,          nameof(source));
            Error.ThrowArgumentNullException(keySelector,     nameof(keySelector));
            Error.ThrowArgumentNullException(elementSelector, nameof(elementSelector));
            return new GroupByAwait<TSource, TKey, TElement>(
                source,
                keySelector,
                elementSelector,
                EqualityComparer<TKey>.Default
            );
        }

        public static IAppaTaskAsyncEnumerable<IGrouping<TKey, TElement>>
            GroupByAwait<TSource, TKey, TElement>(
                this IAppaTaskAsyncEnumerable<TSource> source,
                Func<TSource, AppaTask<TKey>> keySelector,
                Func<TSource, AppaTask<TElement>> elementSelector,
                IEqualityComparer<TKey> comparer)
        {
            Error.ThrowArgumentNullException(source,          nameof(source));
            Error.ThrowArgumentNullException(keySelector,     nameof(keySelector));
            Error.ThrowArgumentNullException(elementSelector, nameof(elementSelector));
            Error.ThrowArgumentNullException(comparer,        nameof(comparer));
            return new GroupByAwait<TSource, TKey, TElement>(source, keySelector, elementSelector, comparer);
        }

        public static IAppaTaskAsyncEnumerable<TResult> GroupByAwait<TSource, TKey, TResult>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<TKey>> keySelector,
            Func<TKey, IEnumerable<TSource>, AppaTask<TResult>> resultSelector)
        {
            Error.ThrowArgumentNullException(source,         nameof(source));
            Error.ThrowArgumentNullException(keySelector,    nameof(keySelector));
            Error.ThrowArgumentNullException(resultSelector, nameof(resultSelector));
            return new GroupByAwait<TSource, TKey, TSource, TResult>(
                source,
                keySelector,
                x => AppaTask.FromResult(x),
                resultSelector,
                EqualityComparer<TKey>.Default
            );
        }

        public static IAppaTaskAsyncEnumerable<TResult> GroupByAwait<TSource, TKey, TElement, TResult>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<TKey>> keySelector,
            Func<TSource, AppaTask<TElement>> elementSelector,
            Func<TKey, IEnumerable<TElement>, AppaTask<TResult>> resultSelector)
        {
            Error.ThrowArgumentNullException(source,          nameof(source));
            Error.ThrowArgumentNullException(keySelector,     nameof(keySelector));
            Error.ThrowArgumentNullException(elementSelector, nameof(elementSelector));
            Error.ThrowArgumentNullException(resultSelector,  nameof(resultSelector));
            return new GroupByAwait<TSource, TKey, TElement, TResult>(
                source,
                keySelector,
                elementSelector,
                resultSelector,
                EqualityComparer<TKey>.Default
            );
        }

        public static IAppaTaskAsyncEnumerable<TResult> GroupByAwait<TSource, TKey, TResult>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<TKey>> keySelector,
            Func<TKey, IEnumerable<TSource>, AppaTask<TResult>> resultSelector,
            IEqualityComparer<TKey> comparer)
        {
            Error.ThrowArgumentNullException(source,         nameof(source));
            Error.ThrowArgumentNullException(keySelector,    nameof(keySelector));
            Error.ThrowArgumentNullException(resultSelector, nameof(resultSelector));
            Error.ThrowArgumentNullException(comparer,       nameof(comparer));
            return new GroupByAwait<TSource, TKey, TSource, TResult>(
                source,
                keySelector,
                x => AppaTask.FromResult(x),
                resultSelector,
                comparer
            );
        }

        public static IAppaTaskAsyncEnumerable<TResult> GroupByAwait<TSource, TKey, TElement, TResult>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<TKey>> keySelector,
            Func<TSource, AppaTask<TElement>> elementSelector,
            Func<TKey, IEnumerable<TElement>, AppaTask<TResult>> resultSelector,
            IEqualityComparer<TKey> comparer)
        {
            Error.ThrowArgumentNullException(source,          nameof(source));
            Error.ThrowArgumentNullException(keySelector,     nameof(keySelector));
            Error.ThrowArgumentNullException(elementSelector, nameof(elementSelector));
            Error.ThrowArgumentNullException(resultSelector,  nameof(resultSelector));
            Error.ThrowArgumentNullException(comparer,        nameof(comparer));
            return new GroupByAwait<TSource, TKey, TElement, TResult>(
                source,
                keySelector,
                elementSelector,
                resultSelector,
                comparer
            );
        }

        // with ct

        public static IAppaTaskAsyncEnumerable<IGrouping<TKey, TSource>>
            GroupByAwaitWithCancellation<TSource, TKey>(
                this IAppaTaskAsyncEnumerable<TSource> source,
                Func<TSource, CancellationToken, AppaTask<TKey>> keySelector)
        {
            Error.ThrowArgumentNullException(source,      nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));
            return new GroupByAwaitWithCancellation<TSource, TKey, TSource>(
                source,
                keySelector,
                (x, _) => AppaTask.FromResult(x),
                EqualityComparer<TKey>.Default
            );
        }

        public static IAppaTaskAsyncEnumerable<IGrouping<TKey, TSource>>
            GroupByAwaitWithCancellation<TSource, TKey>(
                this IAppaTaskAsyncEnumerable<TSource> source,
                Func<TSource, CancellationToken, AppaTask<TKey>> keySelector,
                IEqualityComparer<TKey> comparer)
        {
            Error.ThrowArgumentNullException(source,      nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));
            Error.ThrowArgumentNullException(comparer,    nameof(comparer));
            return new GroupByAwaitWithCancellation<TSource, TKey, TSource>(
                source,
                keySelector,
                (x, _) => AppaTask.FromResult(x),
                comparer
            );
        }

        public static IAppaTaskAsyncEnumerable<IGrouping<TKey, TElement>>
            GroupByAwaitWithCancellation<TSource, TKey, TElement>(
                this IAppaTaskAsyncEnumerable<TSource> source,
                Func<TSource, CancellationToken, AppaTask<TKey>> keySelector,
                Func<TSource, CancellationToken, AppaTask<TElement>> elementSelector)
        {
            Error.ThrowArgumentNullException(source,          nameof(source));
            Error.ThrowArgumentNullException(keySelector,     nameof(keySelector));
            Error.ThrowArgumentNullException(elementSelector, nameof(elementSelector));
            return new GroupByAwaitWithCancellation<TSource, TKey, TElement>(
                source,
                keySelector,
                elementSelector,
                EqualityComparer<TKey>.Default
            );
        }

        public static IAppaTaskAsyncEnumerable<IGrouping<TKey, TElement>>
            GroupByAwaitWithCancellation<TSource, TKey, TElement>(
                this IAppaTaskAsyncEnumerable<TSource> source,
                Func<TSource, CancellationToken, AppaTask<TKey>> keySelector,
                Func<TSource, CancellationToken, AppaTask<TElement>> elementSelector,
                IEqualityComparer<TKey> comparer)
        {
            Error.ThrowArgumentNullException(source,          nameof(source));
            Error.ThrowArgumentNullException(keySelector,     nameof(keySelector));
            Error.ThrowArgumentNullException(elementSelector, nameof(elementSelector));
            Error.ThrowArgumentNullException(comparer,        nameof(comparer));
            return new GroupByAwaitWithCancellation<TSource, TKey, TElement>(
                source,
                keySelector,
                elementSelector,
                comparer
            );
        }

        public static IAppaTaskAsyncEnumerable<TResult> GroupByAwaitWithCancellation<TSource, TKey, TResult>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<TKey>> keySelector,
            Func<TKey, IEnumerable<TSource>, CancellationToken, AppaTask<TResult>> resultSelector)
        {
            Error.ThrowArgumentNullException(source,         nameof(source));
            Error.ThrowArgumentNullException(keySelector,    nameof(keySelector));
            Error.ThrowArgumentNullException(resultSelector, nameof(resultSelector));
            return new GroupByAwaitWithCancellation<TSource, TKey, TSource, TResult>(
                source,
                keySelector,
                (x, _) => AppaTask.FromResult(x),
                resultSelector,
                EqualityComparer<TKey>.Default
            );
        }

        public static IAppaTaskAsyncEnumerable<TResult>
            GroupByAwaitWithCancellation<TSource, TKey, TElement, TResult>(
                this IAppaTaskAsyncEnumerable<TSource> source,
                Func<TSource, CancellationToken, AppaTask<TKey>> keySelector,
                Func<TSource, CancellationToken, AppaTask<TElement>> elementSelector,
                Func<TKey, IEnumerable<TElement>, CancellationToken, AppaTask<TResult>> resultSelector)
        {
            Error.ThrowArgumentNullException(source,          nameof(source));
            Error.ThrowArgumentNullException(keySelector,     nameof(keySelector));
            Error.ThrowArgumentNullException(elementSelector, nameof(elementSelector));
            Error.ThrowArgumentNullException(resultSelector,  nameof(resultSelector));
            return new GroupByAwaitWithCancellation<TSource, TKey, TElement, TResult>(
                source,
                keySelector,
                elementSelector,
                resultSelector,
                EqualityComparer<TKey>.Default
            );
        }

        public static IAppaTaskAsyncEnumerable<TResult> GroupByAwaitWithCancellation<TSource, TKey, TResult>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<TKey>> keySelector,
            Func<TKey, IEnumerable<TSource>, CancellationToken, AppaTask<TResult>> resultSelector,
            IEqualityComparer<TKey> comparer)
        {
            Error.ThrowArgumentNullException(source,         nameof(source));
            Error.ThrowArgumentNullException(keySelector,    nameof(keySelector));
            Error.ThrowArgumentNullException(resultSelector, nameof(resultSelector));
            Error.ThrowArgumentNullException(comparer,       nameof(comparer));
            return new GroupByAwaitWithCancellation<TSource, TKey, TSource, TResult>(
                source,
                keySelector,
                (x, _) => AppaTask.FromResult(x),
                resultSelector,
                comparer
            );
        }

        public static IAppaTaskAsyncEnumerable<TResult>
            GroupByAwaitWithCancellation<TSource, TKey, TElement, TResult>(
                this IAppaTaskAsyncEnumerable<TSource> source,
                Func<TSource, CancellationToken, AppaTask<TKey>> keySelector,
                Func<TSource, CancellationToken, AppaTask<TElement>> elementSelector,
                Func<TKey, IEnumerable<TElement>, CancellationToken, AppaTask<TResult>> resultSelector,
                IEqualityComparer<TKey> comparer)
        {
            Error.ThrowArgumentNullException(source,          nameof(source));
            Error.ThrowArgumentNullException(keySelector,     nameof(keySelector));
            Error.ThrowArgumentNullException(elementSelector, nameof(elementSelector));
            Error.ThrowArgumentNullException(resultSelector,  nameof(resultSelector));
            Error.ThrowArgumentNullException(comparer,        nameof(comparer));
            return new GroupByAwaitWithCancellation<TSource, TKey, TElement, TResult>(
                source,
                keySelector,
                elementSelector,
                resultSelector,
                comparer
            );
        }
    }

    internal sealed class
        GroupBy<TSource, TKey, TElement> : IAppaTaskAsyncEnumerable<IGrouping<TKey, TElement>>
    {
        private readonly IAppaTaskAsyncEnumerable<TSource> source;
        private readonly Func<TSource, TKey> keySelector;
        private readonly Func<TSource, TElement> elementSelector;
        private readonly IEqualityComparer<TKey> comparer;

        public GroupBy(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            Func<TSource, TElement> elementSelector,
            IEqualityComparer<TKey> comparer)
        {
            this.source = source;
            this.keySelector = keySelector;
            this.elementSelector = elementSelector;
            this.comparer = comparer;
        }

        public IAppaTaskAsyncEnumerator<IGrouping<TKey, TElement>> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            return new _GroupBy(source, keySelector, elementSelector, comparer, cancellationToken);
        }

        private sealed class _GroupBy : MoveNextSource, IAppaTaskAsyncEnumerator<IGrouping<TKey, TElement>>
        {
            private readonly IAppaTaskAsyncEnumerable<TSource> source;
            private readonly Func<TSource, TKey> keySelector;
            private readonly Func<TSource, TElement> elementSelector;
            private readonly IEqualityComparer<TKey> comparer;
            private CancellationToken cancellationToken;

            private IEnumerator<IGrouping<TKey, TElement>> groupEnumerator;

            public _GroupBy(
                IAppaTaskAsyncEnumerable<TSource> source,
                Func<TSource, TKey> keySelector,
                Func<TSource, TElement> elementSelector,
                IEqualityComparer<TKey> comparer,
                CancellationToken cancellationToken)
            {
                this.source = source;
                this.keySelector = keySelector;
                this.elementSelector = elementSelector;
                this.comparer = comparer;
                this.cancellationToken = cancellationToken;
                TaskTracker.TrackActiveTask(this, 3);
            }

            public IGrouping<TKey, TElement> Current { get; private set; }

            public AppaTask<bool> MoveNextAsync()
            {
                cancellationToken.ThrowIfCancellationRequested();
                completionSource.Reset();

                if (groupEnumerator == null)
                {
                    CreateLookup().Forget();
                }
                else
                {
                    SourceMoveNext();
                }

                return new AppaTask<bool>(this, completionSource.Version);
            }

            private async AppaTaskVoid CreateLookup()
            {
                try
                {
                    var lookup = await source.ToLookupAsync(
                        keySelector,
                        elementSelector,
                        comparer,
                        cancellationToken
                    );
                    groupEnumerator = lookup.GetEnumerator();
                }
                catch (Exception ex)
                {
                    completionSource.TrySetException(ex);
                    return;
                }

                SourceMoveNext();
            }

            private void SourceMoveNext()
            {
                try
                {
                    if (groupEnumerator.MoveNext())
                    {
                        Current = groupEnumerator.Current;
                        completionSource.TrySetResult(true);
                    }
                    else
                    {
                        completionSource.TrySetResult(false);
                    }
                }
                catch (Exception ex)
                {
                    completionSource.TrySetException(ex);
                }
            }

            public AppaTask DisposeAsync()
            {
                TaskTracker.RemoveTracking(this);
                if (groupEnumerator != null)
                {
                    groupEnumerator.Dispose();
                }

                return default;
            }
        }
    }

    internal sealed class GroupBy<TSource, TKey, TElement, TResult> : IAppaTaskAsyncEnumerable<TResult>
    {
        private readonly IAppaTaskAsyncEnumerable<TSource> source;
        private readonly Func<TSource, TKey> keySelector;
        private readonly Func<TSource, TElement> elementSelector;
        private readonly Func<TKey, IEnumerable<TElement>, TResult> resultSelector;
        private readonly IEqualityComparer<TKey> comparer;

        public GroupBy(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            Func<TSource, TElement> elementSelector,
            Func<TKey, IEnumerable<TElement>, TResult> resultSelector,
            IEqualityComparer<TKey> comparer)
        {
            this.source = source;
            this.keySelector = keySelector;
            this.elementSelector = elementSelector;
            this.resultSelector = resultSelector;
            this.comparer = comparer;
        }

        public IAppaTaskAsyncEnumerator<TResult> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            return new _GroupBy(
                source,
                keySelector,
                elementSelector,
                resultSelector,
                comparer,
                cancellationToken
            );
        }

        private sealed class _GroupBy : MoveNextSource, IAppaTaskAsyncEnumerator<TResult>
        {
            private readonly IAppaTaskAsyncEnumerable<TSource> source;
            private readonly Func<TSource, TKey> keySelector;
            private readonly Func<TSource, TElement> elementSelector;
            private readonly Func<TKey, IEnumerable<TElement>, TResult> resultSelector;
            private readonly IEqualityComparer<TKey> comparer;
            private CancellationToken cancellationToken;

            private IEnumerator<IGrouping<TKey, TElement>> groupEnumerator;

            public _GroupBy(
                IAppaTaskAsyncEnumerable<TSource> source,
                Func<TSource, TKey> keySelector,
                Func<TSource, TElement> elementSelector,
                Func<TKey, IEnumerable<TElement>, TResult> resultSelector,
                IEqualityComparer<TKey> comparer,
                CancellationToken cancellationToken)
            {
                this.source = source;
                this.keySelector = keySelector;
                this.elementSelector = elementSelector;
                this.resultSelector = resultSelector;
                this.comparer = comparer;
                this.cancellationToken = cancellationToken;
                TaskTracker.TrackActiveTask(this, 3);
            }

            public TResult Current { get; private set; }

            public AppaTask<bool> MoveNextAsync()
            {
                cancellationToken.ThrowIfCancellationRequested();
                completionSource.Reset();

                if (groupEnumerator == null)
                {
                    CreateLookup().Forget();
                }
                else
                {
                    SourceMoveNext();
                }

                return new AppaTask<bool>(this, completionSource.Version);
            }

            private async AppaTaskVoid CreateLookup()
            {
                try
                {
                    var lookup = await source.ToLookupAsync(
                        keySelector,
                        elementSelector,
                        comparer,
                        cancellationToken
                    );
                    groupEnumerator = lookup.GetEnumerator();
                }
                catch (Exception ex)
                {
                    completionSource.TrySetException(ex);
                    return;
                }

                SourceMoveNext();
            }

            private void SourceMoveNext()
            {
                try
                {
                    if (groupEnumerator.MoveNext())
                    {
                        var current = groupEnumerator.Current;
                        Current = resultSelector(current.Key, current);
                        completionSource.TrySetResult(true);
                    }
                    else
                    {
                        completionSource.TrySetResult(false);
                    }
                }
                catch (Exception ex)
                {
                    completionSource.TrySetException(ex);
                }
            }

            public AppaTask DisposeAsync()
            {
                TaskTracker.RemoveTracking(this);
                if (groupEnumerator != null)
                {
                    groupEnumerator.Dispose();
                }

                return default;
            }
        }
    }

    internal sealed class
        GroupByAwait<TSource, TKey, TElement> : IAppaTaskAsyncEnumerable<IGrouping<TKey, TElement>>
    {
        private readonly IAppaTaskAsyncEnumerable<TSource> source;
        private readonly Func<TSource, AppaTask<TKey>> keySelector;
        private readonly Func<TSource, AppaTask<TElement>> elementSelector;
        private readonly IEqualityComparer<TKey> comparer;

        public GroupByAwait(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<TKey>> keySelector,
            Func<TSource, AppaTask<TElement>> elementSelector,
            IEqualityComparer<TKey> comparer)
        {
            this.source = source;
            this.keySelector = keySelector;
            this.elementSelector = elementSelector;
            this.comparer = comparer;
        }

        public IAppaTaskAsyncEnumerator<IGrouping<TKey, TElement>> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            return new _GroupByAwait(source, keySelector, elementSelector, comparer, cancellationToken);
        }

        private sealed class _GroupByAwait : MoveNextSource,
                                             IAppaTaskAsyncEnumerator<IGrouping<TKey, TElement>>
        {
            private readonly IAppaTaskAsyncEnumerable<TSource> source;
            private readonly Func<TSource, AppaTask<TKey>> keySelector;
            private readonly Func<TSource, AppaTask<TElement>> elementSelector;
            private readonly IEqualityComparer<TKey> comparer;
            private CancellationToken cancellationToken;

            private IEnumerator<IGrouping<TKey, TElement>> groupEnumerator;

            public _GroupByAwait(
                IAppaTaskAsyncEnumerable<TSource> source,
                Func<TSource, AppaTask<TKey>> keySelector,
                Func<TSource, AppaTask<TElement>> elementSelector,
                IEqualityComparer<TKey> comparer,
                CancellationToken cancellationToken)
            {
                this.source = source;
                this.keySelector = keySelector;
                this.elementSelector = elementSelector;
                this.comparer = comparer;
                this.cancellationToken = cancellationToken;
                TaskTracker.TrackActiveTask(this, 3);
            }

            public IGrouping<TKey, TElement> Current { get; private set; }

            public AppaTask<bool> MoveNextAsync()
            {
                cancellationToken.ThrowIfCancellationRequested();
                completionSource.Reset();

                if (groupEnumerator == null)
                {
                    CreateLookup().Forget();
                }
                else
                {
                    SourceMoveNext();
                }

                return new AppaTask<bool>(this, completionSource.Version);
            }

            private async AppaTaskVoid CreateLookup()
            {
                try
                {
                    var lookup = await source.ToLookupAwaitAsync(
                        keySelector,
                        elementSelector,
                        comparer,
                        cancellationToken
                    );
                    groupEnumerator = lookup.GetEnumerator();
                }
                catch (Exception ex)
                {
                    completionSource.TrySetException(ex);
                    return;
                }

                SourceMoveNext();
            }

            private void SourceMoveNext()
            {
                try
                {
                    if (groupEnumerator.MoveNext())
                    {
                        Current = groupEnumerator.Current;
                        completionSource.TrySetResult(true);
                    }
                    else
                    {
                        completionSource.TrySetResult(false);
                    }
                }
                catch (Exception ex)
                {
                    completionSource.TrySetException(ex);
                }
            }

            public AppaTask DisposeAsync()
            {
                TaskTracker.RemoveTracking(this);
                if (groupEnumerator != null)
                {
                    groupEnumerator.Dispose();
                }

                return default;
            }
        }
    }

    internal sealed class GroupByAwait<TSource, TKey, TElement, TResult> : IAppaTaskAsyncEnumerable<TResult>
    {
        private readonly IAppaTaskAsyncEnumerable<TSource> source;
        private readonly Func<TSource, AppaTask<TKey>> keySelector;
        private readonly Func<TSource, AppaTask<TElement>> elementSelector;
        private readonly Func<TKey, IEnumerable<TElement>, AppaTask<TResult>> resultSelector;
        private readonly IEqualityComparer<TKey> comparer;

        public GroupByAwait(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<TKey>> keySelector,
            Func<TSource, AppaTask<TElement>> elementSelector,
            Func<TKey, IEnumerable<TElement>, AppaTask<TResult>> resultSelector,
            IEqualityComparer<TKey> comparer)
        {
            this.source = source;
            this.keySelector = keySelector;
            this.elementSelector = elementSelector;
            this.resultSelector = resultSelector;
            this.comparer = comparer;
        }

        public IAppaTaskAsyncEnumerator<TResult> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            return new _GroupByAwait(
                source,
                keySelector,
                elementSelector,
                resultSelector,
                comparer,
                cancellationToken
            );
        }

        private sealed class _GroupByAwait : MoveNextSource, IAppaTaskAsyncEnumerator<TResult>
        {
            private static readonly Action<object> ResultSelectCoreDelegate = ResultSelectCore;

            private readonly IAppaTaskAsyncEnumerable<TSource> source;
            private readonly Func<TSource, AppaTask<TKey>> keySelector;
            private readonly Func<TSource, AppaTask<TElement>> elementSelector;
            private readonly Func<TKey, IEnumerable<TElement>, AppaTask<TResult>> resultSelector;
            private readonly IEqualityComparer<TKey> comparer;
            private CancellationToken cancellationToken;

            private IEnumerator<IGrouping<TKey, TElement>> groupEnumerator;
            private AppaTask<TResult>.Awaiter awaiter;

            public _GroupByAwait(
                IAppaTaskAsyncEnumerable<TSource> source,
                Func<TSource, AppaTask<TKey>> keySelector,
                Func<TSource, AppaTask<TElement>> elementSelector,
                Func<TKey, IEnumerable<TElement>, AppaTask<TResult>> resultSelector,
                IEqualityComparer<TKey> comparer,
                CancellationToken cancellationToken)
            {
                this.source = source;
                this.keySelector = keySelector;
                this.elementSelector = elementSelector;
                this.resultSelector = resultSelector;
                this.comparer = comparer;
                this.cancellationToken = cancellationToken;
                TaskTracker.TrackActiveTask(this, 3);
            }

            public TResult Current { get; private set; }

            public AppaTask<bool> MoveNextAsync()
            {
                cancellationToken.ThrowIfCancellationRequested();
                completionSource.Reset();

                if (groupEnumerator == null)
                {
                    CreateLookup().Forget();
                }
                else
                {
                    SourceMoveNext();
                }

                return new AppaTask<bool>(this, completionSource.Version);
            }

            private async AppaTaskVoid CreateLookup()
            {
                try
                {
                    var lookup = await source.ToLookupAwaitAsync(
                        keySelector,
                        elementSelector,
                        comparer,
                        cancellationToken
                    );
                    groupEnumerator = lookup.GetEnumerator();
                }
                catch (Exception ex)
                {
                    completionSource.TrySetException(ex);
                    return;
                }

                SourceMoveNext();
            }

            private void SourceMoveNext()
            {
                try
                {
                    if (groupEnumerator.MoveNext())
                    {
                        var current = groupEnumerator.Current;

                        awaiter = resultSelector(current.Key, current).GetAwaiter();
                        if (awaiter.IsCompleted)
                        {
                            ResultSelectCore(this);
                        }
                        else
                        {
                            awaiter.SourceOnCompleted(ResultSelectCoreDelegate, this);
                        }

                        return;
                    }

                    completionSource.TrySetResult(false);
                }
                catch (Exception ex)
                {
                    completionSource.TrySetException(ex);
                }
            }

            private static void ResultSelectCore(object state)
            {
                var self = (_GroupByAwait)state;

                if (self.TryGetResult(self.awaiter, out var result))
                {
                    self.Current = result;
                    self.completionSource.TrySetResult(true);
                }
            }

            public AppaTask DisposeAsync()
            {
                TaskTracker.RemoveTracking(this);
                if (groupEnumerator != null)
                {
                    groupEnumerator.Dispose();
                }

                return default;
            }
        }
    }

    internal sealed class
        GroupByAwaitWithCancellation<TSource, TKey, TElement> : IAppaTaskAsyncEnumerable<
            IGrouping<TKey, TElement>>
    {
        private readonly IAppaTaskAsyncEnumerable<TSource> source;
        private readonly Func<TSource, CancellationToken, AppaTask<TKey>> keySelector;
        private readonly Func<TSource, CancellationToken, AppaTask<TElement>> elementSelector;
        private readonly IEqualityComparer<TKey> comparer;

        public GroupByAwaitWithCancellation(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<TKey>> keySelector,
            Func<TSource, CancellationToken, AppaTask<TElement>> elementSelector,
            IEqualityComparer<TKey> comparer)
        {
            this.source = source;
            this.keySelector = keySelector;
            this.elementSelector = elementSelector;
            this.comparer = comparer;
        }

        public IAppaTaskAsyncEnumerator<IGrouping<TKey, TElement>> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            return new _GroupByAwaitWithCancellation(
                source,
                keySelector,
                elementSelector,
                comparer,
                cancellationToken
            );
        }

        private sealed class _GroupByAwaitWithCancellation : MoveNextSource,
                                                             IAppaTaskAsyncEnumerator<
                                                                 IGrouping<TKey, TElement>>
        {
            private readonly IAppaTaskAsyncEnumerable<TSource> source;
            private readonly Func<TSource, CancellationToken, AppaTask<TKey>> keySelector;
            private readonly Func<TSource, CancellationToken, AppaTask<TElement>> elementSelector;
            private readonly IEqualityComparer<TKey> comparer;
            private CancellationToken cancellationToken;

            private IEnumerator<IGrouping<TKey, TElement>> groupEnumerator;

            public _GroupByAwaitWithCancellation(
                IAppaTaskAsyncEnumerable<TSource> source,
                Func<TSource, CancellationToken, AppaTask<TKey>> keySelector,
                Func<TSource, CancellationToken, AppaTask<TElement>> elementSelector,
                IEqualityComparer<TKey> comparer,
                CancellationToken cancellationToken)
            {
                this.source = source;
                this.keySelector = keySelector;
                this.elementSelector = elementSelector;
                this.comparer = comparer;
                this.cancellationToken = cancellationToken;
                TaskTracker.TrackActiveTask(this, 3);
            }

            public IGrouping<TKey, TElement> Current { get; private set; }

            public AppaTask<bool> MoveNextAsync()
            {
                cancellationToken.ThrowIfCancellationRequested();
                completionSource.Reset();

                if (groupEnumerator == null)
                {
                    CreateLookup().Forget();
                }
                else
                {
                    SourceMoveNext();
                }

                return new AppaTask<bool>(this, completionSource.Version);
            }

            private async AppaTaskVoid CreateLookup()
            {
                try
                {
                    var lookup = await source.ToLookupAwaitWithCancellationAsync(
                        keySelector,
                        elementSelector,
                        comparer,
                        cancellationToken
                    );
                    groupEnumerator = lookup.GetEnumerator();
                }
                catch (Exception ex)
                {
                    completionSource.TrySetException(ex);
                    return;
                }

                SourceMoveNext();
            }

            private void SourceMoveNext()
            {
                try
                {
                    if (groupEnumerator.MoveNext())
                    {
                        Current = groupEnumerator.Current;
                        completionSource.TrySetResult(true);
                    }
                    else
                    {
                        completionSource.TrySetResult(false);
                    }
                }
                catch (Exception ex)
                {
                    completionSource.TrySetException(ex);
                }
            }

            public AppaTask DisposeAsync()
            {
                TaskTracker.RemoveTracking(this);
                if (groupEnumerator != null)
                {
                    groupEnumerator.Dispose();
                }

                return default;
            }
        }
    }

    internal sealed class
        GroupByAwaitWithCancellation<TSource, TKey, TElement, TResult> : IAppaTaskAsyncEnumerable<TResult>
    {
        private readonly IAppaTaskAsyncEnumerable<TSource> source;
        private readonly Func<TSource, CancellationToken, AppaTask<TKey>> keySelector;
        private readonly Func<TSource, CancellationToken, AppaTask<TElement>> elementSelector;

        private readonly Func<TKey, IEnumerable<TElement>, CancellationToken, AppaTask<TResult>>
            resultSelector;

        private readonly IEqualityComparer<TKey> comparer;

        public GroupByAwaitWithCancellation(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<TKey>> keySelector,
            Func<TSource, CancellationToken, AppaTask<TElement>> elementSelector,
            Func<TKey, IEnumerable<TElement>, CancellationToken, AppaTask<TResult>> resultSelector,
            IEqualityComparer<TKey> comparer)
        {
            this.source = source;
            this.keySelector = keySelector;
            this.elementSelector = elementSelector;
            this.resultSelector = resultSelector;
            this.comparer = comparer;
        }

        public IAppaTaskAsyncEnumerator<TResult> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            return new _GroupByAwaitWithCancellation(
                source,
                keySelector,
                elementSelector,
                resultSelector,
                comparer,
                cancellationToken
            );
        }

        private sealed class _GroupByAwaitWithCancellation : MoveNextSource, IAppaTaskAsyncEnumerator<TResult>
        {
            private static readonly Action<object> ResultSelectCoreDelegate = ResultSelectCore;

            private readonly IAppaTaskAsyncEnumerable<TSource> source;
            private readonly Func<TSource, CancellationToken, AppaTask<TKey>> keySelector;
            private readonly Func<TSource, CancellationToken, AppaTask<TElement>> elementSelector;

            private readonly Func<TKey, IEnumerable<TElement>, CancellationToken, AppaTask<TResult>>
                resultSelector;

            private readonly IEqualityComparer<TKey> comparer;
            private CancellationToken cancellationToken;

            private IEnumerator<IGrouping<TKey, TElement>> groupEnumerator;
            private AppaTask<TResult>.Awaiter awaiter;

            public _GroupByAwaitWithCancellation(
                IAppaTaskAsyncEnumerable<TSource> source,
                Func<TSource, CancellationToken, AppaTask<TKey>> keySelector,
                Func<TSource, CancellationToken, AppaTask<TElement>> elementSelector,
                Func<TKey, IEnumerable<TElement>, CancellationToken, AppaTask<TResult>> resultSelector,
                IEqualityComparer<TKey> comparer,
                CancellationToken cancellationToken)
            {
                this.source = source;
                this.keySelector = keySelector;
                this.elementSelector = elementSelector;
                this.resultSelector = resultSelector;
                this.comparer = comparer;
                this.cancellationToken = cancellationToken;
                TaskTracker.TrackActiveTask(this, 3);
            }

            public TResult Current { get; private set; }

            public AppaTask<bool> MoveNextAsync()
            {
                cancellationToken.ThrowIfCancellationRequested();
                completionSource.Reset();

                if (groupEnumerator == null)
                {
                    CreateLookup().Forget();
                }
                else
                {
                    SourceMoveNext();
                }

                return new AppaTask<bool>(this, completionSource.Version);
            }

            private async AppaTaskVoid CreateLookup()
            {
                try
                {
                    var lookup = await source.ToLookupAwaitWithCancellationAsync(
                        keySelector,
                        elementSelector,
                        comparer,
                        cancellationToken
                    );
                    groupEnumerator = lookup.GetEnumerator();
                }
                catch (Exception ex)
                {
                    completionSource.TrySetException(ex);
                    return;
                }

                SourceMoveNext();
            }

            private void SourceMoveNext()
            {
                try
                {
                    if (groupEnumerator.MoveNext())
                    {
                        var current = groupEnumerator.Current;

                        awaiter = resultSelector(current.Key, current, cancellationToken).GetAwaiter();
                        if (awaiter.IsCompleted)
                        {
                            ResultSelectCore(this);
                        }
                        else
                        {
                            awaiter.SourceOnCompleted(ResultSelectCoreDelegate, this);
                        }

                        return;
                    }

                    completionSource.TrySetResult(false);
                }
                catch (Exception ex)
                {
                    completionSource.TrySetException(ex);
                }
            }

            private static void ResultSelectCore(object state)
            {
                var self = (_GroupByAwaitWithCancellation)state;

                if (self.TryGetResult(self.awaiter, out var result))
                {
                    self.Current = result;
                    self.completionSource.TrySetResult(true);
                }
            }

            public AppaTask DisposeAsync()
            {
                TaskTracker.RemoveTracking(this);
                if (groupEnumerator != null)
                {
                    groupEnumerator.Dispose();
                }

                return default;
            }
        }
    }
}
