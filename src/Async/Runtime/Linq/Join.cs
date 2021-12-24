﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Appalachia.Utility.Async.Internal;

namespace Appalachia.Utility.Async.Linq
{
    public static partial class AppaTaskAsyncEnumerable
    {
        public static IAppaTaskAsyncEnumerable<TResult> Join<TOuter, TInner, TKey, TResult>(
            this IAppaTaskAsyncEnumerable<TOuter> outer,
            IAppaTaskAsyncEnumerable<TInner> inner,
            Func<TOuter, TKey> outerKeySelector,
            Func<TInner, TKey> innerKeySelector,
            Func<TOuter, TInner, TResult> resultSelector)
        {
            Error.ThrowArgumentNullException(outer,            nameof(outer));
            Error.ThrowArgumentNullException(inner,            nameof(inner));
            Error.ThrowArgumentNullException(outerKeySelector, nameof(outerKeySelector));
            Error.ThrowArgumentNullException(innerKeySelector, nameof(innerKeySelector));
            Error.ThrowArgumentNullException(resultSelector,   nameof(resultSelector));

            return new Join<TOuter, TInner, TKey, TResult>(
                outer,
                inner,
                outerKeySelector,
                innerKeySelector,
                resultSelector,
                EqualityComparer<TKey>.Default
            );
        }

        public static IAppaTaskAsyncEnumerable<TResult> Join<TOuter, TInner, TKey, TResult>(
            this IAppaTaskAsyncEnumerable<TOuter> outer,
            IAppaTaskAsyncEnumerable<TInner> inner,
            Func<TOuter, TKey> outerKeySelector,
            Func<TInner, TKey> innerKeySelector,
            Func<TOuter, TInner, TResult> resultSelector,
            IEqualityComparer<TKey> comparer)
        {
            Error.ThrowArgumentNullException(outer,            nameof(outer));
            Error.ThrowArgumentNullException(inner,            nameof(inner));
            Error.ThrowArgumentNullException(outerKeySelector, nameof(outerKeySelector));
            Error.ThrowArgumentNullException(innerKeySelector, nameof(innerKeySelector));
            Error.ThrowArgumentNullException(resultSelector,   nameof(resultSelector));
            Error.ThrowArgumentNullException(comparer,         nameof(comparer));

            return new Join<TOuter, TInner, TKey, TResult>(
                outer,
                inner,
                outerKeySelector,
                innerKeySelector,
                resultSelector,
                comparer
            );
        }

        public static IAppaTaskAsyncEnumerable<TResult> JoinAwait<TOuter, TInner, TKey, TResult>(
            this IAppaTaskAsyncEnumerable<TOuter> outer,
            IAppaTaskAsyncEnumerable<TInner> inner,
            Func<TOuter, AppaTask<TKey>> outerKeySelector,
            Func<TInner, AppaTask<TKey>> innerKeySelector,
            Func<TOuter, TInner, AppaTask<TResult>> resultSelector)
        {
            Error.ThrowArgumentNullException(outer,            nameof(outer));
            Error.ThrowArgumentNullException(inner,            nameof(inner));
            Error.ThrowArgumentNullException(outerKeySelector, nameof(outerKeySelector));
            Error.ThrowArgumentNullException(innerKeySelector, nameof(innerKeySelector));
            Error.ThrowArgumentNullException(resultSelector,   nameof(resultSelector));

            return new JoinAwait<TOuter, TInner, TKey, TResult>(
                outer,
                inner,
                outerKeySelector,
                innerKeySelector,
                resultSelector,
                EqualityComparer<TKey>.Default
            );
        }

        public static IAppaTaskAsyncEnumerable<TResult> JoinAwait<TOuter, TInner, TKey, TResult>(
            this IAppaTaskAsyncEnumerable<TOuter> outer,
            IAppaTaskAsyncEnumerable<TInner> inner,
            Func<TOuter, AppaTask<TKey>> outerKeySelector,
            Func<TInner, AppaTask<TKey>> innerKeySelector,
            Func<TOuter, TInner, AppaTask<TResult>> resultSelector,
            IEqualityComparer<TKey> comparer)
        {
            Error.ThrowArgumentNullException(outer,            nameof(outer));
            Error.ThrowArgumentNullException(inner,            nameof(inner));
            Error.ThrowArgumentNullException(outerKeySelector, nameof(outerKeySelector));
            Error.ThrowArgumentNullException(innerKeySelector, nameof(innerKeySelector));
            Error.ThrowArgumentNullException(resultSelector,   nameof(resultSelector));
            Error.ThrowArgumentNullException(comparer,         nameof(comparer));

            return new JoinAwait<TOuter, TInner, TKey, TResult>(
                outer,
                inner,
                outerKeySelector,
                innerKeySelector,
                resultSelector,
                comparer
            );
        }

        public static IAppaTaskAsyncEnumerable<TResult>
            JoinAwaitWithCancellation<TOuter, TInner, TKey, TResult>(
                this IAppaTaskAsyncEnumerable<TOuter> outer,
                IAppaTaskAsyncEnumerable<TInner> inner,
                Func<TOuter, CancellationToken, AppaTask<TKey>> outerKeySelector,
                Func<TInner, CancellationToken, AppaTask<TKey>> innerKeySelector,
                Func<TOuter, TInner, CancellationToken, AppaTask<TResult>> resultSelector)
        {
            Error.ThrowArgumentNullException(outer,            nameof(outer));
            Error.ThrowArgumentNullException(inner,            nameof(inner));
            Error.ThrowArgumentNullException(outerKeySelector, nameof(outerKeySelector));
            Error.ThrowArgumentNullException(innerKeySelector, nameof(innerKeySelector));
            Error.ThrowArgumentNullException(resultSelector,   nameof(resultSelector));

            return new JoinAwaitWithCancellation<TOuter, TInner, TKey, TResult>(
                outer,
                inner,
                outerKeySelector,
                innerKeySelector,
                resultSelector,
                EqualityComparer<TKey>.Default
            );
        }

        public static IAppaTaskAsyncEnumerable<TResult>
            JoinAwaitWithCancellation<TOuter, TInner, TKey, TResult>(
                this IAppaTaskAsyncEnumerable<TOuter> outer,
                IAppaTaskAsyncEnumerable<TInner> inner,
                Func<TOuter, CancellationToken, AppaTask<TKey>> outerKeySelector,
                Func<TInner, CancellationToken, AppaTask<TKey>> innerKeySelector,
                Func<TOuter, TInner, CancellationToken, AppaTask<TResult>> resultSelector,
                IEqualityComparer<TKey> comparer)
        {
            Error.ThrowArgumentNullException(outer,            nameof(outer));
            Error.ThrowArgumentNullException(inner,            nameof(inner));
            Error.ThrowArgumentNullException(outerKeySelector, nameof(outerKeySelector));
            Error.ThrowArgumentNullException(innerKeySelector, nameof(innerKeySelector));
            Error.ThrowArgumentNullException(resultSelector,   nameof(resultSelector));
            Error.ThrowArgumentNullException(comparer,         nameof(comparer));

            return new JoinAwaitWithCancellation<TOuter, TInner, TKey, TResult>(
                outer,
                inner,
                outerKeySelector,
                innerKeySelector,
                resultSelector,
                comparer
            );
        }
    }

    internal sealed class Join<TOuter, TInner, TKey, TResult> : IAppaTaskAsyncEnumerable<TResult>
    {
        private readonly IAppaTaskAsyncEnumerable<TOuter> outer;
        private readonly IAppaTaskAsyncEnumerable<TInner> inner;
        private readonly Func<TOuter, TKey> outerKeySelector;
        private readonly Func<TInner, TKey> innerKeySelector;
        private readonly Func<TOuter, TInner, TResult> resultSelector;
        private readonly IEqualityComparer<TKey> comparer;

        public Join(
            IAppaTaskAsyncEnumerable<TOuter> outer,
            IAppaTaskAsyncEnumerable<TInner> inner,
            Func<TOuter, TKey> outerKeySelector,
            Func<TInner, TKey> innerKeySelector,
            Func<TOuter, TInner, TResult> resultSelector,
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
            return new _Join(
                outer,
                inner,
                outerKeySelector,
                innerKeySelector,
                resultSelector,
                comparer,
                cancellationToken
            );
        }

        private sealed class _Join : MoveNextSource, IAppaTaskAsyncEnumerator<TResult>
        {
            private static readonly Action<object> MoveNextCoreDelegate = MoveNextCore;

            private readonly IAppaTaskAsyncEnumerable<TOuter> outer;
            private readonly IAppaTaskAsyncEnumerable<TInner> inner;
            private readonly Func<TOuter, TKey> outerKeySelector;
            private readonly Func<TInner, TKey> innerKeySelector;
            private readonly Func<TOuter, TInner, TResult> resultSelector;
            private readonly IEqualityComparer<TKey> comparer;
            private CancellationToken cancellationToken;

            private ILookup<TKey, TInner> lookup;
            private IAppaTaskAsyncEnumerator<TOuter> enumerator;
            private AppaTask<bool>.Awaiter awaiter;
            private TOuter currentOuterValue;
            private IEnumerator<TInner> valueEnumerator;

            private bool continueNext;

            public _Join(
                IAppaTaskAsyncEnumerable<TOuter> outer,
                IAppaTaskAsyncEnumerable<TInner> inner,
                Func<TOuter, TKey> outerKeySelector,
                Func<TInner, TKey> innerKeySelector,
                Func<TOuter, TInner, TResult> resultSelector,
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
                    CreateInnerHashSet().Forget();
                }
                else
                {
                    SourceMoveNext();
                }

                return new AppaTask<bool>(this, completionSource.Version);
            }

            private async AppaTaskVoid CreateInnerHashSet()
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
                    LOOP:
                    if (valueEnumerator != null)
                    {
                        if (valueEnumerator.MoveNext())
                        {
                            Current = resultSelector(currentOuterValue, valueEnumerator.Current);
                            goto TRY_SET_RESULT_TRUE;
                        }

                        valueEnumerator.Dispose();
                        valueEnumerator = null;
                    }

                    awaiter = enumerator.MoveNextAsync().GetAwaiter();
                    if (awaiter.IsCompleted)
                    {
                        continueNext = true;
                        MoveNextCore(this);
                        if (continueNext)
                        {
                            continueNext = false;
                            goto LOOP; // avoid recursive
                        }
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

                return;

                TRY_SET_RESULT_TRUE:
                completionSource.TrySetResult(true);
            }

            private static void MoveNextCore(object state)
            {
                var self = (_Join)state;

                if (self.TryGetResult(self.awaiter, out var result))
                {
                    if (result)
                    {
                        self.currentOuterValue = self.enumerator.Current;
                        var key = self.outerKeySelector(self.currentOuterValue);
                        self.valueEnumerator = self.lookup[key].GetEnumerator();

                        if (self.continueNext)
                        {
                            return;
                        }

                        self.SourceMoveNext();
                    }
                    else
                    {
                        self.continueNext = false;
                        self.completionSource.TrySetResult(false);
                    }
                }
                else
                {
                    self.continueNext = false;
                }
            }

            public AppaTask DisposeAsync()
            {
                TaskTracker.RemoveTracking(this);
                if (valueEnumerator != null)
                {
                    valueEnumerator.Dispose();
                }

                if (enumerator != null)
                {
                    return enumerator.DisposeAsync();
                }

                return default;
            }
        }
    }

    internal sealed class JoinAwait<TOuter, TInner, TKey, TResult> : IAppaTaskAsyncEnumerable<TResult>
    {
        private readonly IAppaTaskAsyncEnumerable<TOuter> outer;
        private readonly IAppaTaskAsyncEnumerable<TInner> inner;
        private readonly Func<TOuter, AppaTask<TKey>> outerKeySelector;
        private readonly Func<TInner, AppaTask<TKey>> innerKeySelector;
        private readonly Func<TOuter, TInner, AppaTask<TResult>> resultSelector;
        private readonly IEqualityComparer<TKey> comparer;

        public JoinAwait(
            IAppaTaskAsyncEnumerable<TOuter> outer,
            IAppaTaskAsyncEnumerable<TInner> inner,
            Func<TOuter, AppaTask<TKey>> outerKeySelector,
            Func<TInner, AppaTask<TKey>> innerKeySelector,
            Func<TOuter, TInner, AppaTask<TResult>> resultSelector,
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
            return new _JoinAwait(
                outer,
                inner,
                outerKeySelector,
                innerKeySelector,
                resultSelector,
                comparer,
                cancellationToken
            );
        }

        private sealed class _JoinAwait : MoveNextSource, IAppaTaskAsyncEnumerator<TResult>
        {
            private static readonly Action<object> MoveNextCoreDelegate = MoveNextCore;
            private static readonly Action<object> OuterSelectCoreDelegate = OuterSelectCore;
            private static readonly Action<object> ResultSelectCoreDelegate = ResultSelectCore;

            private readonly IAppaTaskAsyncEnumerable<TOuter> outer;
            private readonly IAppaTaskAsyncEnumerable<TInner> inner;
            private readonly Func<TOuter, AppaTask<TKey>> outerKeySelector;
            private readonly Func<TInner, AppaTask<TKey>> innerKeySelector;
            private readonly Func<TOuter, TInner, AppaTask<TResult>> resultSelector;
            private readonly IEqualityComparer<TKey> comparer;
            private CancellationToken cancellationToken;

            private ILookup<TKey, TInner> lookup;
            private IAppaTaskAsyncEnumerator<TOuter> enumerator;
            private AppaTask<bool>.Awaiter awaiter;
            private TOuter currentOuterValue;
            private IEnumerator<TInner> valueEnumerator;

            private AppaTask<TResult>.Awaiter resultAwaiter;
            private AppaTask<TKey>.Awaiter outerKeyAwaiter;

            private bool continueNext;

            public _JoinAwait(
                IAppaTaskAsyncEnumerable<TOuter> outer,
                IAppaTaskAsyncEnumerable<TInner> inner,
                Func<TOuter, AppaTask<TKey>> outerKeySelector,
                Func<TInner, AppaTask<TKey>> innerKeySelector,
                Func<TOuter, TInner, AppaTask<TResult>> resultSelector,
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
                    CreateInnerHashSet().Forget();
                }
                else
                {
                    SourceMoveNext();
                }

                return new AppaTask<bool>(this, completionSource.Version);
            }

            private async AppaTaskVoid CreateInnerHashSet()
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
                    LOOP:
                    if (valueEnumerator != null)
                    {
                        if (valueEnumerator.MoveNext())
                        {
                            resultAwaiter = resultSelector(currentOuterValue, valueEnumerator.Current)
                               .GetAwaiter();
                            if (resultAwaiter.IsCompleted)
                            {
                                ResultSelectCore(this);
                            }
                            else
                            {
                                resultAwaiter.SourceOnCompleted(ResultSelectCoreDelegate, this);
                            }

                            return;
                        }

                        valueEnumerator.Dispose();
                        valueEnumerator = null;
                    }

                    awaiter = enumerator.MoveNextAsync().GetAwaiter();
                    if (awaiter.IsCompleted)
                    {
                        continueNext = true;
                        MoveNextCore(this);
                        if (continueNext)
                        {
                            continueNext = false;
                            goto LOOP; // avoid recursive
                        }
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
                var self = (_JoinAwait)state;

                if (self.TryGetResult(self.awaiter, out var result))
                {
                    if (result)
                    {
                        self.currentOuterValue = self.enumerator.Current;

                        self.outerKeyAwaiter = self.outerKeySelector(self.currentOuterValue).GetAwaiter();

                        if (self.outerKeyAwaiter.IsCompleted)
                        {
                            OuterSelectCore(self);
                        }
                        else
                        {
                            self.continueNext = false;
                            self.outerKeyAwaiter.SourceOnCompleted(OuterSelectCoreDelegate, self);
                        }
                    }
                    else
                    {
                        self.continueNext = false;
                        self.completionSource.TrySetResult(false);
                    }
                }
                else
                {
                    self.continueNext = false;
                }
            }

            private static void OuterSelectCore(object state)
            {
                var self = (_JoinAwait)state;

                if (self.TryGetResult(self.outerKeyAwaiter, out var key))
                {
                    self.valueEnumerator = self.lookup[key].GetEnumerator();

                    if (self.continueNext)
                    {
                        return;
                    }

                    self.SourceMoveNext();
                }
                else
                {
                    self.continueNext = false;
                }
            }

            private static void ResultSelectCore(object state)
            {
                var self = (_JoinAwait)state;

                if (self.TryGetResult(self.resultAwaiter, out var result))
                {
                    self.Current = result;
                    self.completionSource.TrySetResult(true);
                }
            }

            public AppaTask DisposeAsync()
            {
                TaskTracker.RemoveTracking(this);
                if (valueEnumerator != null)
                {
                    valueEnumerator.Dispose();
                }

                if (enumerator != null)
                {
                    return enumerator.DisposeAsync();
                }

                return default;
            }
        }
    }

    internal sealed class
        JoinAwaitWithCancellation<TOuter, TInner, TKey, TResult> : IAppaTaskAsyncEnumerable<TResult>
    {
        private readonly IAppaTaskAsyncEnumerable<TOuter> outer;
        private readonly IAppaTaskAsyncEnumerable<TInner> inner;
        private readonly Func<TOuter, CancellationToken, AppaTask<TKey>> outerKeySelector;
        private readonly Func<TInner, CancellationToken, AppaTask<TKey>> innerKeySelector;
        private readonly Func<TOuter, TInner, CancellationToken, AppaTask<TResult>> resultSelector;
        private readonly IEqualityComparer<TKey> comparer;

        public JoinAwaitWithCancellation(
            IAppaTaskAsyncEnumerable<TOuter> outer,
            IAppaTaskAsyncEnumerable<TInner> inner,
            Func<TOuter, CancellationToken, AppaTask<TKey>> outerKeySelector,
            Func<TInner, CancellationToken, AppaTask<TKey>> innerKeySelector,
            Func<TOuter, TInner, CancellationToken, AppaTask<TResult>> resultSelector,
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
            return new _JoinAwaitWithCancellation(
                outer,
                inner,
                outerKeySelector,
                innerKeySelector,
                resultSelector,
                comparer,
                cancellationToken
            );
        }

        private sealed class _JoinAwaitWithCancellation : MoveNextSource, IAppaTaskAsyncEnumerator<TResult>
        {
            private static readonly Action<object> MoveNextCoreDelegate = MoveNextCore;
            private static readonly Action<object> OuterSelectCoreDelegate = OuterSelectCore;
            private static readonly Action<object> ResultSelectCoreDelegate = ResultSelectCore;

            private readonly IAppaTaskAsyncEnumerable<TOuter> outer;
            private readonly IAppaTaskAsyncEnumerable<TInner> inner;
            private readonly Func<TOuter, CancellationToken, AppaTask<TKey>> outerKeySelector;
            private readonly Func<TInner, CancellationToken, AppaTask<TKey>> innerKeySelector;
            private readonly Func<TOuter, TInner, CancellationToken, AppaTask<TResult>> resultSelector;
            private readonly IEqualityComparer<TKey> comparer;
            private CancellationToken cancellationToken;

            private ILookup<TKey, TInner> lookup;
            private IAppaTaskAsyncEnumerator<TOuter> enumerator;
            private AppaTask<bool>.Awaiter awaiter;
            private TOuter currentOuterValue;
            private IEnumerator<TInner> valueEnumerator;

            private AppaTask<TResult>.Awaiter resultAwaiter;
            private AppaTask<TKey>.Awaiter outerKeyAwaiter;

            private bool continueNext;

            public _JoinAwaitWithCancellation(
                IAppaTaskAsyncEnumerable<TOuter> outer,
                IAppaTaskAsyncEnumerable<TInner> inner,
                Func<TOuter, CancellationToken, AppaTask<TKey>> outerKeySelector,
                Func<TInner, CancellationToken, AppaTask<TKey>> innerKeySelector,
                Func<TOuter, TInner, CancellationToken, AppaTask<TResult>> resultSelector,
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
                    CreateInnerHashSet().Forget();
                }
                else
                {
                    SourceMoveNext();
                }

                return new AppaTask<bool>(this, completionSource.Version);
            }

            private async AppaTaskVoid CreateInnerHashSet()
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
                    LOOP:
                    if (valueEnumerator != null)
                    {
                        if (valueEnumerator.MoveNext())
                        {
                            resultAwaiter = resultSelector(
                                    currentOuterValue,
                                    valueEnumerator.Current,
                                    cancellationToken
                                )
                               .GetAwaiter();
                            if (resultAwaiter.IsCompleted)
                            {
                                ResultSelectCore(this);
                            }
                            else
                            {
                                resultAwaiter.SourceOnCompleted(ResultSelectCoreDelegate, this);
                            }

                            return;
                        }

                        valueEnumerator.Dispose();
                        valueEnumerator = null;
                    }

                    awaiter = enumerator.MoveNextAsync().GetAwaiter();
                    if (awaiter.IsCompleted)
                    {
                        continueNext = true;
                        MoveNextCore(this);
                        if (continueNext)
                        {
                            continueNext = false;
                            goto LOOP; // avoid recursive
                        }
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
                var self = (_JoinAwaitWithCancellation)state;

                if (self.TryGetResult(self.awaiter, out var result))
                {
                    if (result)
                    {
                        self.currentOuterValue = self.enumerator.Current;

                        self.outerKeyAwaiter = self.outerKeySelector(
                                                        self.currentOuterValue,
                                                        self.cancellationToken
                                                    )
                                                   .GetAwaiter();

                        if (self.outerKeyAwaiter.IsCompleted)
                        {
                            OuterSelectCore(self);
                        }
                        else
                        {
                            self.continueNext = false;
                            self.outerKeyAwaiter.SourceOnCompleted(OuterSelectCoreDelegate, self);
                        }
                    }
                    else
                    {
                        self.continueNext = false;
                        self.completionSource.TrySetResult(false);
                    }
                }
                else
                {
                    self.continueNext = false;
                }
            }

            private static void OuterSelectCore(object state)
            {
                var self = (_JoinAwaitWithCancellation)state;

                if (self.TryGetResult(self.outerKeyAwaiter, out var key))
                {
                    self.valueEnumerator = self.lookup[key].GetEnumerator();

                    if (self.continueNext)
                    {
                        return;
                    }

                    self.SourceMoveNext();
                }
                else
                {
                    self.continueNext = false;
                }
            }

            private static void ResultSelectCore(object state)
            {
                var self = (_JoinAwaitWithCancellation)state;

                if (self.TryGetResult(self.resultAwaiter, out var result))
                {
                    self.Current = result;
                    self.completionSource.TrySetResult(true);
                }
            }

            public AppaTask DisposeAsync()
            {
                TaskTracker.RemoveTracking(this);
                if (valueEnumerator != null)
                {
                    valueEnumerator.Dispose();
                }

                if (enumerator != null)
                {
                    return enumerator.DisposeAsync();
                }

                return default;
            }
        }
    }
}
