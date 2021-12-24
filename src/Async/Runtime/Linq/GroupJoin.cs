using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Appalachia.Utility.Async.Internal;

namespace Appalachia.Utility.Async.Linq
{
    public static partial class AppaTaskAsyncEnumerable
    {
        public static IAppaTaskAsyncEnumerable<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(
            this IAppaTaskAsyncEnumerable<TOuter> outer,
            IAppaTaskAsyncEnumerable<TInner> inner,
            Func<TOuter, TKey> outerKeySelector,
            Func<TInner, TKey> innerKeySelector,
            Func<TOuter, IEnumerable<TInner>, TResult> resultSelector)
        {
            Error.ThrowArgumentNullException(outer,            nameof(outer));
            Error.ThrowArgumentNullException(inner,            nameof(inner));
            Error.ThrowArgumentNullException(outerKeySelector, nameof(outerKeySelector));
            Error.ThrowArgumentNullException(innerKeySelector, nameof(innerKeySelector));
            Error.ThrowArgumentNullException(resultSelector,   nameof(resultSelector));

            return new GroupJoin<TOuter, TInner, TKey, TResult>(
                outer,
                inner,
                outerKeySelector,
                innerKeySelector,
                resultSelector,
                EqualityComparer<TKey>.Default
            );
        }

        public static IAppaTaskAsyncEnumerable<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(
            this IAppaTaskAsyncEnumerable<TOuter> outer,
            IAppaTaskAsyncEnumerable<TInner> inner,
            Func<TOuter, TKey> outerKeySelector,
            Func<TInner, TKey> innerKeySelector,
            Func<TOuter, IEnumerable<TInner>, TResult> resultSelector,
            IEqualityComparer<TKey> comparer)
        {
            Error.ThrowArgumentNullException(outer,            nameof(outer));
            Error.ThrowArgumentNullException(inner,            nameof(inner));
            Error.ThrowArgumentNullException(outerKeySelector, nameof(outerKeySelector));
            Error.ThrowArgumentNullException(innerKeySelector, nameof(innerKeySelector));
            Error.ThrowArgumentNullException(resultSelector,   nameof(resultSelector));
            Error.ThrowArgumentNullException(comparer,         nameof(comparer));

            return new GroupJoin<TOuter, TInner, TKey, TResult>(
                outer,
                inner,
                outerKeySelector,
                innerKeySelector,
                resultSelector,
                comparer
            );
        }

        public static IAppaTaskAsyncEnumerable<TResult> GroupJoinAwait<TOuter, TInner, TKey, TResult>(
            this IAppaTaskAsyncEnumerable<TOuter> outer,
            IAppaTaskAsyncEnumerable<TInner> inner,
            Func<TOuter, AppaTask<TKey>> outerKeySelector,
            Func<TInner, AppaTask<TKey>> innerKeySelector,
            Func<TOuter, IEnumerable<TInner>, AppaTask<TResult>> resultSelector)
        {
            Error.ThrowArgumentNullException(outer,            nameof(outer));
            Error.ThrowArgumentNullException(inner,            nameof(inner));
            Error.ThrowArgumentNullException(outerKeySelector, nameof(outerKeySelector));
            Error.ThrowArgumentNullException(innerKeySelector, nameof(innerKeySelector));
            Error.ThrowArgumentNullException(resultSelector,   nameof(resultSelector));

            return new GroupJoinAwait<TOuter, TInner, TKey, TResult>(
                outer,
                inner,
                outerKeySelector,
                innerKeySelector,
                resultSelector,
                EqualityComparer<TKey>.Default
            );
        }

        public static IAppaTaskAsyncEnumerable<TResult> GroupJoinAwait<TOuter, TInner, TKey, TResult>(
            this IAppaTaskAsyncEnumerable<TOuter> outer,
            IAppaTaskAsyncEnumerable<TInner> inner,
            Func<TOuter, AppaTask<TKey>> outerKeySelector,
            Func<TInner, AppaTask<TKey>> innerKeySelector,
            Func<TOuter, IEnumerable<TInner>, AppaTask<TResult>> resultSelector,
            IEqualityComparer<TKey> comparer)
        {
            Error.ThrowArgumentNullException(outer,            nameof(outer));
            Error.ThrowArgumentNullException(inner,            nameof(inner));
            Error.ThrowArgumentNullException(outerKeySelector, nameof(outerKeySelector));
            Error.ThrowArgumentNullException(innerKeySelector, nameof(innerKeySelector));
            Error.ThrowArgumentNullException(resultSelector,   nameof(resultSelector));
            Error.ThrowArgumentNullException(comparer,         nameof(comparer));

            return new GroupJoinAwait<TOuter, TInner, TKey, TResult>(
                outer,
                inner,
                outerKeySelector,
                innerKeySelector,
                resultSelector,
                comparer
            );
        }

        public static IAppaTaskAsyncEnumerable<TResult>
            GroupJoinAwaitWithCancellation<TOuter, TInner, TKey, TResult>(
                this IAppaTaskAsyncEnumerable<TOuter> outer,
                IAppaTaskAsyncEnumerable<TInner> inner,
                Func<TOuter, CancellationToken, AppaTask<TKey>> outerKeySelector,
                Func<TInner, CancellationToken, AppaTask<TKey>> innerKeySelector,
                Func<TOuter, IEnumerable<TInner>, CancellationToken, AppaTask<TResult>> resultSelector)
        {
            Error.ThrowArgumentNullException(outer,            nameof(outer));
            Error.ThrowArgumentNullException(inner,            nameof(inner));
            Error.ThrowArgumentNullException(outerKeySelector, nameof(outerKeySelector));
            Error.ThrowArgumentNullException(innerKeySelector, nameof(innerKeySelector));
            Error.ThrowArgumentNullException(resultSelector,   nameof(resultSelector));

            return new GroupJoinAwaitWithCancellation<TOuter, TInner, TKey, TResult>(
                outer,
                inner,
                outerKeySelector,
                innerKeySelector,
                resultSelector,
                EqualityComparer<TKey>.Default
            );
        }

        public static IAppaTaskAsyncEnumerable<TResult>
            GroupJoinAwaitWithCancellation<TOuter, TInner, TKey, TResult>(
                this IAppaTaskAsyncEnumerable<TOuter> outer,
                IAppaTaskAsyncEnumerable<TInner> inner,
                Func<TOuter, CancellationToken, AppaTask<TKey>> outerKeySelector,
                Func<TInner, CancellationToken, AppaTask<TKey>> innerKeySelector,
                Func<TOuter, IEnumerable<TInner>, CancellationToken, AppaTask<TResult>> resultSelector,
                IEqualityComparer<TKey> comparer)
        {
            Error.ThrowArgumentNullException(outer,            nameof(outer));
            Error.ThrowArgumentNullException(inner,            nameof(inner));
            Error.ThrowArgumentNullException(outerKeySelector, nameof(outerKeySelector));
            Error.ThrowArgumentNullException(innerKeySelector, nameof(innerKeySelector));
            Error.ThrowArgumentNullException(resultSelector,   nameof(resultSelector));
            Error.ThrowArgumentNullException(comparer,         nameof(comparer));

            return new GroupJoinAwaitWithCancellation<TOuter, TInner, TKey, TResult>(
                outer,
                inner,
                outerKeySelector,
                innerKeySelector,
                resultSelector,
                comparer
            );
        }
    }

    internal sealed class GroupJoin<TOuter, TInner, TKey, TResult> : IAppaTaskAsyncEnumerable<TResult>
    {
        private readonly IAppaTaskAsyncEnumerable<TOuter> outer;
        private readonly IAppaTaskAsyncEnumerable<TInner> inner;
        private readonly Func<TOuter, TKey> outerKeySelector;
        private readonly Func<TInner, TKey> innerKeySelector;
        private readonly Func<TOuter, IEnumerable<TInner>, TResult> resultSelector;
        private readonly IEqualityComparer<TKey> comparer;

        public GroupJoin(
            IAppaTaskAsyncEnumerable<TOuter> outer,
            IAppaTaskAsyncEnumerable<TInner> inner,
            Func<TOuter, TKey> outerKeySelector,
            Func<TInner, TKey> innerKeySelector,
            Func<TOuter, IEnumerable<TInner>, TResult> resultSelector,
            IEqualityComparer<TKey> comparer)
        {
            this.outer = outer;
            this.inner = inner;
            this.outerKeySelector = outerKeySelector;
            this.innerKeySelector = innerKeySelector;
            this.resultSelector = resultSelector;
            this.comparer = comparer;
        }

        public IAppaTaskAsyncEnumerator<TResult> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            return new _GroupJoin(
                outer,
                inner,
                outerKeySelector,
                innerKeySelector,
                resultSelector,
                comparer,
                cancellationToken
            );
        }

        private sealed class _GroupJoin : MoveNextSource, IAppaTaskAsyncEnumerator<TResult>
        {
            private static readonly Action<object> MoveNextCoreDelegate = MoveNextCore;

            private readonly IAppaTaskAsyncEnumerable<TOuter> outer;
            private readonly IAppaTaskAsyncEnumerable<TInner> inner;
            private readonly Func<TOuter, TKey> outerKeySelector;
            private readonly Func<TInner, TKey> innerKeySelector;
            private readonly Func<TOuter, IEnumerable<TInner>, TResult> resultSelector;
            private readonly IEqualityComparer<TKey> comparer;
            private CancellationToken cancellationToken;

            private ILookup<TKey, TInner> lookup;
            private IAppaTaskAsyncEnumerator<TOuter> enumerator;
            private AppaTask<bool>.Awaiter awaiter;

            public _GroupJoin(
                IAppaTaskAsyncEnumerable<TOuter> outer,
                IAppaTaskAsyncEnumerable<TInner> inner,
                Func<TOuter, TKey> outerKeySelector,
                Func<TInner, TKey> innerKeySelector,
                Func<TOuter, IEnumerable<TInner>, TResult> resultSelector,
                IEqualityComparer<TKey> comparer,
                CancellationToken cancellationToken)
            {
                this.outer = outer;
                this.inner = inner;
                this.outerKeySelector = outerKeySelector;
                this.innerKeySelector = innerKeySelector;
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

                if (lookup == null)
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
                    lookup = await inner.ToLookupAsync(innerKeySelector, comparer, cancellationToken);
                    enumerator = outer.GetAsyncEnumerator(cancellationToken);
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
                    awaiter = enumerator.MoveNextAsync().GetAwaiter();
                    if (awaiter.IsCompleted)
                    {
                        MoveNextCore(this);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(MoveNextCoreDelegate, this);
                    }
                }
                catch (Exception ex)
                {
                    completionSource.TrySetException(ex);
                }
            }

            private static void MoveNextCore(object state)
            {
                var self = (_GroupJoin)state;

                if (self.TryGetResult(self.awaiter, out var result))
                {
                    if (result)
                    {
                        var outer = self.enumerator.Current;
                        var key = self.outerKeySelector(outer);
                        var values = self.lookup[key];

                        self.Current = self.resultSelector(outer, values);
                        self.completionSource.TrySetResult(true);
                    }
                    else
                    {
                        self.completionSource.TrySetResult(false);
                    }
                }
            }

            public AppaTask DisposeAsync()
            {
                TaskTracker.RemoveTracking(this);
                if (enumerator != null)
                {
                    return enumerator.DisposeAsync();
                }

                return default;
            }
        }
    }

    internal sealed class GroupJoinAwait<TOuter, TInner, TKey, TResult> : IAppaTaskAsyncEnumerable<TResult>
    {
        private readonly IAppaTaskAsyncEnumerable<TOuter> outer;
        private readonly IAppaTaskAsyncEnumerable<TInner> inner;
        private readonly Func<TOuter, AppaTask<TKey>> outerKeySelector;
        private readonly Func<TInner, AppaTask<TKey>> innerKeySelector;
        private readonly Func<TOuter, IEnumerable<TInner>, AppaTask<TResult>> resultSelector;
        private readonly IEqualityComparer<TKey> comparer;

        public GroupJoinAwait(
            IAppaTaskAsyncEnumerable<TOuter> outer,
            IAppaTaskAsyncEnumerable<TInner> inner,
            Func<TOuter, AppaTask<TKey>> outerKeySelector,
            Func<TInner, AppaTask<TKey>> innerKeySelector,
            Func<TOuter, IEnumerable<TInner>, AppaTask<TResult>> resultSelector,
            IEqualityComparer<TKey> comparer)
        {
            this.outer = outer;
            this.inner = inner;
            this.outerKeySelector = outerKeySelector;
            this.innerKeySelector = innerKeySelector;
            this.resultSelector = resultSelector;
            this.comparer = comparer;
        }

        public IAppaTaskAsyncEnumerator<TResult> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            return new _GroupJoinAwait(
                outer,
                inner,
                outerKeySelector,
                innerKeySelector,
                resultSelector,
                comparer,
                cancellationToken
            );
        }

        private sealed class _GroupJoinAwait : MoveNextSource, IAppaTaskAsyncEnumerator<TResult>
        {
            private static readonly Action<object> MoveNextCoreDelegate = MoveNextCore;
            private static readonly Action<object> ResultSelectCoreDelegate = ResultSelectCore;
            private static readonly Action<object> OuterKeySelectCoreDelegate = OuterKeySelectCore;

            private readonly IAppaTaskAsyncEnumerable<TOuter> outer;
            private readonly IAppaTaskAsyncEnumerable<TInner> inner;
            private readonly Func<TOuter, AppaTask<TKey>> outerKeySelector;
            private readonly Func<TInner, AppaTask<TKey>> innerKeySelector;
            private readonly Func<TOuter, IEnumerable<TInner>, AppaTask<TResult>> resultSelector;
            private readonly IEqualityComparer<TKey> comparer;
            private CancellationToken cancellationToken;

            private ILookup<TKey, TInner> lookup;
            private IAppaTaskAsyncEnumerator<TOuter> enumerator;
            private TOuter outerValue;
            private AppaTask<bool>.Awaiter awaiter;
            private AppaTask<TKey>.Awaiter outerKeyAwaiter;
            private AppaTask<TResult>.Awaiter resultAwaiter;

            public _GroupJoinAwait(
                IAppaTaskAsyncEnumerable<TOuter> outer,
                IAppaTaskAsyncEnumerable<TInner> inner,
                Func<TOuter, AppaTask<TKey>> outerKeySelector,
                Func<TInner, AppaTask<TKey>> innerKeySelector,
                Func<TOuter, IEnumerable<TInner>, AppaTask<TResult>> resultSelector,
                IEqualityComparer<TKey> comparer,
                CancellationToken cancellationToken)
            {
                this.outer = outer;
                this.inner = inner;
                this.outerKeySelector = outerKeySelector;
                this.innerKeySelector = innerKeySelector;
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

                if (lookup == null)
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
                    lookup = await inner.ToLookupAwaitAsync(innerKeySelector, comparer, cancellationToken);
                    enumerator = outer.GetAsyncEnumerator(cancellationToken);
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
                    awaiter = enumerator.MoveNextAsync().GetAwaiter();
                    if (awaiter.IsCompleted)
                    {
                        MoveNextCore(this);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(MoveNextCoreDelegate, this);
                    }
                }
                catch (Exception ex)
                {
                    completionSource.TrySetException(ex);
                }
            }

            private static void MoveNextCore(object state)
            {
                var self = (_GroupJoinAwait)state;

                if (self.TryGetResult(self.awaiter, out var result))
                {
                    if (result)
                    {
                        try
                        {
                            self.outerValue = self.enumerator.Current;
                            self.outerKeyAwaiter = self.outerKeySelector(self.outerValue).GetAwaiter();
                            if (self.outerKeyAwaiter.IsCompleted)
                            {
                                OuterKeySelectCore(self);
                            }
                            else
                            {
                                self.outerKeyAwaiter.SourceOnCompleted(OuterKeySelectCoreDelegate, self);
                            }
                        }
                        catch (Exception ex)
                        {
                            self.completionSource.TrySetException(ex);
                        }
                    }
                    else
                    {
                        self.completionSource.TrySetResult(false);
                    }
                }
            }

            private static void OuterKeySelectCore(object state)
            {
                var self = (_GroupJoinAwait)state;

                if (self.TryGetResult(self.outerKeyAwaiter, out var result))
                {
                    try
                    {
                        var values = self.lookup[result];
                        self.resultAwaiter = self.resultSelector(self.outerValue, values).GetAwaiter();
                        if (self.resultAwaiter.IsCompleted)
                        {
                            ResultSelectCore(self);
                        }
                        else
                        {
                            self.resultAwaiter.SourceOnCompleted(ResultSelectCoreDelegate, self);
                        }
                    }
                    catch (Exception ex)
                    {
                        self.completionSource.TrySetException(ex);
                    }
                }
            }

            private static void ResultSelectCore(object state)
            {
                var self = (_GroupJoinAwait)state;

                if (self.TryGetResult(self.resultAwaiter, out var result))
                {
                    self.Current = result;
                    self.completionSource.TrySetResult(true);
                }
            }

            public AppaTask DisposeAsync()
            {
                TaskTracker.RemoveTracking(this);
                if (enumerator != null)
                {
                    return enumerator.DisposeAsync();
                }

                return default;
            }
        }
    }

    internal sealed class
        GroupJoinAwaitWithCancellation<TOuter, TInner, TKey, TResult> : IAppaTaskAsyncEnumerable<TResult>
    {
        private readonly IAppaTaskAsyncEnumerable<TOuter> outer;
        private readonly IAppaTaskAsyncEnumerable<TInner> inner;
        private readonly Func<TOuter, CancellationToken, AppaTask<TKey>> outerKeySelector;
        private readonly Func<TInner, CancellationToken, AppaTask<TKey>> innerKeySelector;

        private readonly Func<TOuter, IEnumerable<TInner>, CancellationToken, AppaTask<TResult>>
            resultSelector;

        private readonly IEqualityComparer<TKey> comparer;

        public GroupJoinAwaitWithCancellation(
            IAppaTaskAsyncEnumerable<TOuter> outer,
            IAppaTaskAsyncEnumerable<TInner> inner,
            Func<TOuter, CancellationToken, AppaTask<TKey>> outerKeySelector,
            Func<TInner, CancellationToken, AppaTask<TKey>> innerKeySelector,
            Func<TOuter, IEnumerable<TInner>, CancellationToken, AppaTask<TResult>> resultSelector,
            IEqualityComparer<TKey> comparer)
        {
            this.outer = outer;
            this.inner = inner;
            this.outerKeySelector = outerKeySelector;
            this.innerKeySelector = innerKeySelector;
            this.resultSelector = resultSelector;
            this.comparer = comparer;
        }

        public IAppaTaskAsyncEnumerator<TResult> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            return new _GroupJoinAwaitWithCancellation(
                outer,
                inner,
                outerKeySelector,
                innerKeySelector,
                resultSelector,
                comparer,
                cancellationToken
            );
        }

        private sealed class _GroupJoinAwaitWithCancellation : MoveNextSource,
                                                               IAppaTaskAsyncEnumerator<TResult>
        {
            private static readonly Action<object> MoveNextCoreDelegate = MoveNextCore;
            private static readonly Action<object> ResultSelectCoreDelegate = ResultSelectCore;
            private static readonly Action<object> OuterKeySelectCoreDelegate = OuterKeySelectCore;

            private readonly IAppaTaskAsyncEnumerable<TOuter> outer;
            private readonly IAppaTaskAsyncEnumerable<TInner> inner;
            private readonly Func<TOuter, CancellationToken, AppaTask<TKey>> outerKeySelector;
            private readonly Func<TInner, CancellationToken, AppaTask<TKey>> innerKeySelector;

            private readonly Func<TOuter, IEnumerable<TInner>, CancellationToken, AppaTask<TResult>>
                resultSelector;

            private readonly IEqualityComparer<TKey> comparer;
            private CancellationToken cancellationToken;

            private ILookup<TKey, TInner> lookup;
            private IAppaTaskAsyncEnumerator<TOuter> enumerator;
            private TOuter outerValue;
            private AppaTask<bool>.Awaiter awaiter;
            private AppaTask<TKey>.Awaiter outerKeyAwaiter;
            private AppaTask<TResult>.Awaiter resultAwaiter;

            public _GroupJoinAwaitWithCancellation(
                IAppaTaskAsyncEnumerable<TOuter> outer,
                IAppaTaskAsyncEnumerable<TInner> inner,
                Func<TOuter, CancellationToken, AppaTask<TKey>> outerKeySelector,
                Func<TInner, CancellationToken, AppaTask<TKey>> innerKeySelector,
                Func<TOuter, IEnumerable<TInner>, CancellationToken, AppaTask<TResult>> resultSelector,
                IEqualityComparer<TKey> comparer,
                CancellationToken cancellationToken)
            {
                this.outer = outer;
                this.inner = inner;
                this.outerKeySelector = outerKeySelector;
                this.innerKeySelector = innerKeySelector;
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

                if (lookup == null)
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
                    lookup = await inner.ToLookupAwaitWithCancellationAsync(
                        innerKeySelector,
                        comparer,
                        cancellationToken
                    );
                    enumerator = outer.GetAsyncEnumerator(cancellationToken);
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
                    awaiter = enumerator.MoveNextAsync().GetAwaiter();
                    if (awaiter.IsCompleted)
                    {
                        MoveNextCore(this);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(MoveNextCoreDelegate, this);
                    }
                }
                catch (Exception ex)
                {
                    completionSource.TrySetException(ex);
                }
            }

            private static void MoveNextCore(object state)
            {
                var self = (_GroupJoinAwaitWithCancellation)state;

                if (self.TryGetResult(self.awaiter, out var result))
                {
                    if (result)
                    {
                        try
                        {
                            self.outerValue = self.enumerator.Current;
                            self.outerKeyAwaiter = self.outerKeySelector(
                                                            self.outerValue,
                                                            self.cancellationToken
                                                        )
                                                       .GetAwaiter();
                            if (self.outerKeyAwaiter.IsCompleted)
                            {
                                OuterKeySelectCore(self);
                            }
                            else
                            {
                                self.outerKeyAwaiter.SourceOnCompleted(OuterKeySelectCoreDelegate, self);
                            }
                        }
                        catch (Exception ex)
                        {
                            self.completionSource.TrySetException(ex);
                        }
                    }
                    else
                    {
                        self.completionSource.TrySetResult(false);
                    }
                }
            }

            private static void OuterKeySelectCore(object state)
            {
                var self = (_GroupJoinAwaitWithCancellation)state;

                if (self.TryGetResult(self.outerKeyAwaiter, out var result))
                {
                    try
                    {
                        var values = self.lookup[result];
                        self.resultAwaiter = self.resultSelector(
                                                      self.outerValue,
                                                      values,
                                                      self.cancellationToken
                                                  )
                                                 .GetAwaiter();
                        if (self.resultAwaiter.IsCompleted)
                        {
                            ResultSelectCore(self);
                        }
                        else
                        {
                            self.resultAwaiter.SourceOnCompleted(ResultSelectCoreDelegate, self);
                        }
                    }
                    catch (Exception ex)
                    {
                        self.completionSource.TrySetException(ex);
                    }
                }
            }

            private static void ResultSelectCore(object state)
            {
                var self = (_GroupJoinAwaitWithCancellation)state;

                if (self.TryGetResult(self.resultAwaiter, out var result))
                {
                    self.Current = result;
                    self.completionSource.TrySetResult(true);
                }
            }

            public AppaTask DisposeAsync()
            {
                TaskTracker.RemoveTracking(this);
                if (enumerator != null)
                {
                    return enumerator.DisposeAsync();
                }

                return default;
            }
        }
    }
}
