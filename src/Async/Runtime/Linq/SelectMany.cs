using System;
using System.Threading;
using Appalachia.Utility.Async.Internal;

namespace Appalachia.Utility.Async.Linq
{
    public static partial class AppaTaskAsyncEnumerable
    {
        public static IAppaTaskAsyncEnumerable<TResult> SelectMany<TSource, TResult>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, IAppaTaskAsyncEnumerable<TResult>> selector)
        {
            Error.ThrowArgumentNullException(source,   nameof(source));
            Error.ThrowArgumentNullException(selector, nameof(selector));

            return new SelectMany<TSource, TResult, TResult>(source, selector, (x, y) => y);
        }

        public static IAppaTaskAsyncEnumerable<TResult> SelectMany<TSource, TResult>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, int, IAppaTaskAsyncEnumerable<TResult>> selector)
        {
            Error.ThrowArgumentNullException(source,   nameof(source));
            Error.ThrowArgumentNullException(selector, nameof(selector));

            return new SelectMany<TSource, TResult, TResult>(source, selector, (x, y) => y);
        }

        public static IAppaTaskAsyncEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, IAppaTaskAsyncEnumerable<TCollection>> collectionSelector,
            Func<TSource, TCollection, TResult> resultSelector)
        {
            Error.ThrowArgumentNullException(source,             nameof(source));
            Error.ThrowArgumentNullException(collectionSelector, nameof(collectionSelector));

            return new SelectMany<TSource, TCollection, TResult>(source, collectionSelector, resultSelector);
        }

        public static IAppaTaskAsyncEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, int, IAppaTaskAsyncEnumerable<TCollection>> collectionSelector,
            Func<TSource, TCollection, TResult> resultSelector)
        {
            Error.ThrowArgumentNullException(source,             nameof(source));
            Error.ThrowArgumentNullException(collectionSelector, nameof(collectionSelector));

            return new SelectMany<TSource, TCollection, TResult>(source, collectionSelector, resultSelector);
        }

        public static IAppaTaskAsyncEnumerable<TResult> SelectManyAwait<TSource, TResult>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<IAppaTaskAsyncEnumerable<TResult>>> selector)
        {
            Error.ThrowArgumentNullException(source,   nameof(source));
            Error.ThrowArgumentNullException(selector, nameof(selector));

            return new SelectManyAwait<TSource, TResult, TResult>(
                source,
                selector,
                (x, y) => AppaTask.FromResult(y)
            );
        }

        public static IAppaTaskAsyncEnumerable<TResult> SelectManyAwait<TSource, TResult>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, int, AppaTask<IAppaTaskAsyncEnumerable<TResult>>> selector)
        {
            Error.ThrowArgumentNullException(source,   nameof(source));
            Error.ThrowArgumentNullException(selector, nameof(selector));

            return new SelectManyAwait<TSource, TResult, TResult>(
                source,
                selector,
                (x, y) => AppaTask.FromResult(y)
            );
        }

        public static IAppaTaskAsyncEnumerable<TResult> SelectManyAwait<TSource, TCollection, TResult>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<IAppaTaskAsyncEnumerable<TCollection>>> collectionSelector,
            Func<TSource, TCollection, AppaTask<TResult>> resultSelector)
        {
            Error.ThrowArgumentNullException(source,             nameof(source));
            Error.ThrowArgumentNullException(collectionSelector, nameof(collectionSelector));

            return new SelectManyAwait<TSource, TCollection, TResult>(
                source,
                collectionSelector,
                resultSelector
            );
        }

        public static IAppaTaskAsyncEnumerable<TResult> SelectManyAwait<TSource, TCollection, TResult>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, int, AppaTask<IAppaTaskAsyncEnumerable<TCollection>>> collectionSelector,
            Func<TSource, TCollection, AppaTask<TResult>> resultSelector)
        {
            Error.ThrowArgumentNullException(source,             nameof(source));
            Error.ThrowArgumentNullException(collectionSelector, nameof(collectionSelector));

            return new SelectManyAwait<TSource, TCollection, TResult>(
                source,
                collectionSelector,
                resultSelector
            );
        }

        public static IAppaTaskAsyncEnumerable<TResult> SelectManyAwaitWithCancellation<TSource, TResult>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<IAppaTaskAsyncEnumerable<TResult>>> selector)
        {
            Error.ThrowArgumentNullException(source,   nameof(source));
            Error.ThrowArgumentNullException(selector, nameof(selector));

            return new SelectManyAwaitWithCancellation<TSource, TResult, TResult>(
                source,
                selector,
                (x, y, c) => AppaTask.FromResult(y)
            );
        }

        public static IAppaTaskAsyncEnumerable<TResult> SelectManyAwaitWithCancellation<TSource, TResult>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, int, CancellationToken, AppaTask<IAppaTaskAsyncEnumerable<TResult>>> selector)
        {
            Error.ThrowArgumentNullException(source,   nameof(source));
            Error.ThrowArgumentNullException(selector, nameof(selector));

            return new SelectManyAwaitWithCancellation<TSource, TResult, TResult>(
                source,
                selector,
                (x, y, c) => AppaTask.FromResult(y)
            );
        }

        public static IAppaTaskAsyncEnumerable<TResult>
            SelectManyAwaitWithCancellation<TSource, TCollection, TResult>(
                this IAppaTaskAsyncEnumerable<TSource> source,
                Func<TSource, CancellationToken, AppaTask<IAppaTaskAsyncEnumerable<TCollection>>>
                    collectionSelector,
                Func<TSource, TCollection, CancellationToken, AppaTask<TResult>> resultSelector)
        {
            Error.ThrowArgumentNullException(source,             nameof(source));
            Error.ThrowArgumentNullException(collectionSelector, nameof(collectionSelector));

            return new SelectManyAwaitWithCancellation<TSource, TCollection, TResult>(
                source,
                collectionSelector,
                resultSelector
            );
        }

        public static IAppaTaskAsyncEnumerable<TResult>
            SelectManyAwaitWithCancellation<TSource, TCollection, TResult>(
                this IAppaTaskAsyncEnumerable<TSource> source,
                Func<TSource, int, CancellationToken, AppaTask<IAppaTaskAsyncEnumerable<TCollection>>>
                    collectionSelector,
                Func<TSource, TCollection, CancellationToken, AppaTask<TResult>> resultSelector)
        {
            Error.ThrowArgumentNullException(source,             nameof(source));
            Error.ThrowArgumentNullException(collectionSelector, nameof(collectionSelector));

            return new SelectManyAwaitWithCancellation<TSource, TCollection, TResult>(
                source,
                collectionSelector,
                resultSelector
            );
        }
    }

    internal sealed class SelectMany<TSource, TCollection, TResult> : IAppaTaskAsyncEnumerable<TResult>
    {
        private readonly IAppaTaskAsyncEnumerable<TSource> source;
        private readonly Func<TSource, IAppaTaskAsyncEnumerable<TCollection>> selector1;
        private readonly Func<TSource, int, IAppaTaskAsyncEnumerable<TCollection>> selector2;
        private readonly Func<TSource, TCollection, TResult> resultSelector;

        public SelectMany(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, IAppaTaskAsyncEnumerable<TCollection>> selector,
            Func<TSource, TCollection, TResult> resultSelector)
        {
            this.source = source;
            selector1 = selector;
            selector2 = null;
            this.resultSelector = resultSelector;
        }

        public SelectMany(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, int, IAppaTaskAsyncEnumerable<TCollection>> selector,
            Func<TSource, TCollection, TResult> resultSelector)
        {
            this.source = source;
            selector1 = null;
            selector2 = selector;
            this.resultSelector = resultSelector;
        }

        public IAppaTaskAsyncEnumerator<TResult> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            return new _SelectMany(source, selector1, selector2, resultSelector, cancellationToken);
        }

        private sealed class _SelectMany : MoveNextSource, IAppaTaskAsyncEnumerator<TResult>
        {
            private static readonly Action<object> sourceMoveNextCoreDelegate = SourceMoveNextCore;

            private static readonly Action<object> selectedSourceMoveNextCoreDelegate =
                SeletedSourceMoveNextCore;

            private static readonly Action<object> selectedEnumeratorDisposeAsyncCoreDelegate =
                SelectedEnumeratorDisposeAsyncCore;

            private readonly IAppaTaskAsyncEnumerable<TSource> source;

            private readonly Func<TSource, IAppaTaskAsyncEnumerable<TCollection>> selector1;
            private readonly Func<TSource, int, IAppaTaskAsyncEnumerable<TCollection>> selector2;
            private readonly Func<TSource, TCollection, TResult> resultSelector;
            private CancellationToken cancellationToken;

            private TSource sourceCurrent;
            private int sourceIndex;
            private IAppaTaskAsyncEnumerator<TSource> sourceEnumerator;
            private IAppaTaskAsyncEnumerator<TCollection> selectedEnumerator;
            private AppaTask<bool>.Awaiter sourceAwaiter;
            private AppaTask<bool>.Awaiter selectedAwaiter;
            private AppaTask.Awaiter selectedDisposeAsyncAwaiter;

            public _SelectMany(
                IAppaTaskAsyncEnumerable<TSource> source,
                Func<TSource, IAppaTaskAsyncEnumerable<TCollection>> selector1,
                Func<TSource, int, IAppaTaskAsyncEnumerable<TCollection>> selector2,
                Func<TSource, TCollection, TResult> resultSelector,
                CancellationToken cancellationToken)
            {
                this.source = source;
                this.selector1 = selector1;
                this.selector2 = selector2;
                this.resultSelector = resultSelector;
                this.cancellationToken = cancellationToken;
                TaskTracker.TrackActiveTask(this, 3);
            }

            public TResult Current { get; private set; }

            public AppaTask<bool> MoveNextAsync()
            {
                completionSource.Reset();

                // iterate selected field
                if (selectedEnumerator != null)
                {
                    MoveNextSelected();
                }
                else
                {
                    // iterate source field
                    if (sourceEnumerator == null)
                    {
                        sourceEnumerator = source.GetAsyncEnumerator(cancellationToken);
                    }

                    MoveNextSource();
                }

                return new AppaTask<bool>(this, completionSource.Version);
            }

            private void MoveNextSource()
            {
                try
                {
                    sourceAwaiter = sourceEnumerator.MoveNextAsync().GetAwaiter();
                }
                catch (Exception ex)
                {
                    completionSource.TrySetException(ex);
                    return;
                }

                if (sourceAwaiter.IsCompleted)
                {
                    SourceMoveNextCore(this);
                }
                else
                {
                    sourceAwaiter.SourceOnCompleted(sourceMoveNextCoreDelegate, this);
                }
            }

            private void MoveNextSelected()
            {
                try
                {
                    selectedAwaiter = selectedEnumerator.MoveNextAsync().GetAwaiter();
                }
                catch (Exception ex)
                {
                    completionSource.TrySetException(ex);
                    return;
                }

                if (selectedAwaiter.IsCompleted)
                {
                    SeletedSourceMoveNextCore(this);
                }
                else
                {
                    selectedAwaiter.SourceOnCompleted(selectedSourceMoveNextCoreDelegate, this);
                }
            }

            private static void SourceMoveNextCore(object state)
            {
                var self = (_SelectMany)state;

                if (self.TryGetResult(self.sourceAwaiter, out var result))
                {
                    if (result)
                    {
                        try
                        {
                            self.sourceCurrent = self.sourceEnumerator.Current;
                            if (self.selector1 != null)
                            {
                                self.selectedEnumerator = self.selector1(self.sourceCurrent)
                                                              .GetAsyncEnumerator(self.cancellationToken);
                            }
                            else
                            {
                                self.selectedEnumerator =
                                    self.selector2(self.sourceCurrent, checked(self.sourceIndex++))
                                        .GetAsyncEnumerator(self.cancellationToken);
                            }
                        }
                        catch (Exception ex)
                        {
                            self.completionSource.TrySetException(ex);
                            return;
                        }

                        self.MoveNextSelected(); // iterated selected source.
                    }
                    else
                    {
                        self.completionSource.TrySetResult(false);
                    }
                }
            }

            private static void SeletedSourceMoveNextCore(object state)
            {
                var self = (_SelectMany)state;

                if (self.TryGetResult(self.selectedAwaiter, out var result))
                {
                    if (result)
                    {
                        try
                        {
                            self.Current = self.resultSelector(
                                self.sourceCurrent,
                                self.selectedEnumerator.Current
                            );
                        }
                        catch (Exception ex)
                        {
                            self.completionSource.TrySetException(ex);
                            return;
                        }

                        self.completionSource.TrySetResult(true);
                    }
                    else
                    {
                        // dispose selected source and try iterate source.
                        try
                        {
                            self.selectedDisposeAsyncAwaiter =
                                self.selectedEnumerator.DisposeAsync().GetAwaiter();
                        }
                        catch (Exception ex)
                        {
                            self.completionSource.TrySetException(ex);
                            return;
                        }

                        if (self.selectedDisposeAsyncAwaiter.IsCompleted)
                        {
                            SelectedEnumeratorDisposeAsyncCore(self);
                        }
                        else
                        {
                            self.selectedDisposeAsyncAwaiter.SourceOnCompleted(
                                selectedEnumeratorDisposeAsyncCoreDelegate,
                                self
                            );
                        }
                    }
                }
            }

            private static void SelectedEnumeratorDisposeAsyncCore(object state)
            {
                var self = (_SelectMany)state;

                if (self.TryGetResult(self.selectedDisposeAsyncAwaiter))
                {
                    self.selectedEnumerator = null;
                    self.selectedAwaiter = default;

                    self.MoveNextSource(); // iterate next source
                }
            }

            public async AppaTask DisposeAsync()
            {
                TaskTracker.RemoveTracking(this);
                if (selectedEnumerator != null)
                {
                    await selectedEnumerator.DisposeAsync();
                }

                if (sourceEnumerator != null)
                {
                    await sourceEnumerator.DisposeAsync();
                }
            }
        }
    }

    internal sealed class SelectManyAwait<TSource, TCollection, TResult> : IAppaTaskAsyncEnumerable<TResult>
    {
        private readonly IAppaTaskAsyncEnumerable<TSource> source;
        private readonly Func<TSource, AppaTask<IAppaTaskAsyncEnumerable<TCollection>>> selector1;
        private readonly Func<TSource, int, AppaTask<IAppaTaskAsyncEnumerable<TCollection>>> selector2;
        private readonly Func<TSource, TCollection, AppaTask<TResult>> resultSelector;

        public SelectManyAwait(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<IAppaTaskAsyncEnumerable<TCollection>>> selector,
            Func<TSource, TCollection, AppaTask<TResult>> resultSelector)
        {
            this.source = source;
            selector1 = selector;
            selector2 = null;
            this.resultSelector = resultSelector;
        }

        public SelectManyAwait(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, int, AppaTask<IAppaTaskAsyncEnumerable<TCollection>>> selector,
            Func<TSource, TCollection, AppaTask<TResult>> resultSelector)
        {
            this.source = source;
            selector1 = null;
            selector2 = selector;
            this.resultSelector = resultSelector;
        }

        public IAppaTaskAsyncEnumerator<TResult> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            return new _SelectManyAwait(source, selector1, selector2, resultSelector, cancellationToken);
        }

        private sealed class _SelectManyAwait : MoveNextSource, IAppaTaskAsyncEnumerator<TResult>
        {
            private static readonly Action<object> sourceMoveNextCoreDelegate = SourceMoveNextCore;

            private static readonly Action<object> selectedSourceMoveNextCoreDelegate =
                SeletedSourceMoveNextCore;

            private static readonly Action<object> selectedEnumeratorDisposeAsyncCoreDelegate =
                SelectedEnumeratorDisposeAsyncCore;

            private static readonly Action<object> selectorAwaitCoreDelegate = SelectorAwaitCore;
            private static readonly Action<object> resultSelectorAwaitCoreDelegate = ResultSelectorAwaitCore;

            private readonly IAppaTaskAsyncEnumerable<TSource> source;

            private readonly Func<TSource, AppaTask<IAppaTaskAsyncEnumerable<TCollection>>> selector1;
            private readonly Func<TSource, int, AppaTask<IAppaTaskAsyncEnumerable<TCollection>>> selector2;
            private readonly Func<TSource, TCollection, AppaTask<TResult>> resultSelector;
            private CancellationToken cancellationToken;

            private TSource sourceCurrent;
            private int sourceIndex;
            private IAppaTaskAsyncEnumerator<TSource> sourceEnumerator;
            private IAppaTaskAsyncEnumerator<TCollection> selectedEnumerator;
            private AppaTask<bool>.Awaiter sourceAwaiter;
            private AppaTask<bool>.Awaiter selectedAwaiter;
            private AppaTask.Awaiter selectedDisposeAsyncAwaiter;

            // await additional
            private AppaTask<IAppaTaskAsyncEnumerable<TCollection>>.Awaiter collectionSelectorAwaiter;
            private AppaTask<TResult>.Awaiter resultSelectorAwaiter;

            public _SelectManyAwait(
                IAppaTaskAsyncEnumerable<TSource> source,
                Func<TSource, AppaTask<IAppaTaskAsyncEnumerable<TCollection>>> selector1,
                Func<TSource, int, AppaTask<IAppaTaskAsyncEnumerable<TCollection>>> selector2,
                Func<TSource, TCollection, AppaTask<TResult>> resultSelector,
                CancellationToken cancellationToken)
            {
                this.source = source;
                this.selector1 = selector1;
                this.selector2 = selector2;
                this.resultSelector = resultSelector;
                this.cancellationToken = cancellationToken;
                TaskTracker.TrackActiveTask(this, 3);
            }

            public TResult Current { get; private set; }

            public AppaTask<bool> MoveNextAsync()
            {
                completionSource.Reset();

                // iterate selected field
                if (selectedEnumerator != null)
                {
                    MoveNextSelected();
                }
                else
                {
                    // iterate source field
                    if (sourceEnumerator == null)
                    {
                        sourceEnumerator = source.GetAsyncEnumerator(cancellationToken);
                    }

                    MoveNextSource();
                }

                return new AppaTask<bool>(this, completionSource.Version);
            }

            private void MoveNextSource()
            {
                try
                {
                    sourceAwaiter = sourceEnumerator.MoveNextAsync().GetAwaiter();
                }
                catch (Exception ex)
                {
                    completionSource.TrySetException(ex);
                    return;
                }

                if (sourceAwaiter.IsCompleted)
                {
                    SourceMoveNextCore(this);
                }
                else
                {
                    sourceAwaiter.SourceOnCompleted(sourceMoveNextCoreDelegate, this);
                }
            }

            private void MoveNextSelected()
            {
                try
                {
                    selectedAwaiter = selectedEnumerator.MoveNextAsync().GetAwaiter();
                }
                catch (Exception ex)
                {
                    completionSource.TrySetException(ex);
                    return;
                }

                if (selectedAwaiter.IsCompleted)
                {
                    SeletedSourceMoveNextCore(this);
                }
                else
                {
                    selectedAwaiter.SourceOnCompleted(selectedSourceMoveNextCoreDelegate, this);
                }
            }

            private static void SourceMoveNextCore(object state)
            {
                var self = (_SelectManyAwait)state;

                if (self.TryGetResult(self.sourceAwaiter, out var result))
                {
                    if (result)
                    {
                        try
                        {
                            self.sourceCurrent = self.sourceEnumerator.Current;

                            if (self.selector1 != null)
                            {
                                self.collectionSelectorAwaiter =
                                    self.selector1(self.sourceCurrent).GetAwaiter();
                            }
                            else
                            {
                                self.collectionSelectorAwaiter = self.selector2(
                                                                          self.sourceCurrent,
                                                                          checked(self.sourceIndex++)
                                                                      )
                                                                     .GetAwaiter();
                            }

                            if (self.collectionSelectorAwaiter.IsCompleted)
                            {
                                SelectorAwaitCore(self);
                            }
                            else
                            {
                                self.collectionSelectorAwaiter.SourceOnCompleted(
                                    selectorAwaitCoreDelegate,
                                    self
                                );
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

            private static void SeletedSourceMoveNextCore(object state)
            {
                var self = (_SelectManyAwait)state;

                if (self.TryGetResult(self.selectedAwaiter, out var result))
                {
                    if (result)
                    {
                        try
                        {
                            self.resultSelectorAwaiter = self.resultSelector(
                                                                  self.sourceCurrent,
                                                                  self.selectedEnumerator.Current
                                                              )
                                                             .GetAwaiter();
                            if (self.resultSelectorAwaiter.IsCompleted)
                            {
                                ResultSelectorAwaitCore(self);
                            }
                            else
                            {
                                self.resultSelectorAwaiter.SourceOnCompleted(
                                    resultSelectorAwaitCoreDelegate,
                                    self
                                );
                            }
                        }
                        catch (Exception ex)
                        {
                            self.completionSource.TrySetException(ex);
                        }
                    }
                    else
                    {
                        // dispose selected source and try iterate source.
                        try
                        {
                            self.selectedDisposeAsyncAwaiter =
                                self.selectedEnumerator.DisposeAsync().GetAwaiter();
                        }
                        catch (Exception ex)
                        {
                            self.completionSource.TrySetException(ex);
                            return;
                        }

                        if (self.selectedDisposeAsyncAwaiter.IsCompleted)
                        {
                            SelectedEnumeratorDisposeAsyncCore(self);
                        }
                        else
                        {
                            self.selectedDisposeAsyncAwaiter.SourceOnCompleted(
                                selectedEnumeratorDisposeAsyncCoreDelegate,
                                self
                            );
                        }
                    }
                }
            }

            private static void SelectedEnumeratorDisposeAsyncCore(object state)
            {
                var self = (_SelectManyAwait)state;

                if (self.TryGetResult(self.selectedDisposeAsyncAwaiter))
                {
                    self.selectedEnumerator = null;
                    self.selectedAwaiter = default;

                    self.MoveNextSource(); // iterate next source
                }
            }

            private static void SelectorAwaitCore(object state)
            {
                var self = (_SelectManyAwait)state;

                if (self.TryGetResult(self.collectionSelectorAwaiter, out var result))
                {
                    self.selectedEnumerator = result.GetAsyncEnumerator(self.cancellationToken);
                    self.MoveNextSelected(); // iterated selected source.
                }
            }

            private static void ResultSelectorAwaitCore(object state)
            {
                var self = (_SelectManyAwait)state;

                if (self.TryGetResult(self.resultSelectorAwaiter, out var result))
                {
                    self.Current = result;
                    self.completionSource.TrySetResult(true);
                }
            }

            public async AppaTask DisposeAsync()
            {
                TaskTracker.RemoveTracking(this);
                if (selectedEnumerator != null)
                {
                    await selectedEnumerator.DisposeAsync();
                }

                if (sourceEnumerator != null)
                {
                    await sourceEnumerator.DisposeAsync();
                }
            }
        }
    }

    internal sealed class
        SelectManyAwaitWithCancellation<TSource, TCollection, TResult> : IAppaTaskAsyncEnumerable<TResult>
    {
        private readonly IAppaTaskAsyncEnumerable<TSource> source;

        private readonly Func<TSource, CancellationToken, AppaTask<IAppaTaskAsyncEnumerable<TCollection>>>
            selector1;

        private readonly
            Func<TSource, int, CancellationToken, AppaTask<IAppaTaskAsyncEnumerable<TCollection>>> selector2;

        private readonly Func<TSource, TCollection, CancellationToken, AppaTask<TResult>> resultSelector;

        public SelectManyAwaitWithCancellation(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<IAppaTaskAsyncEnumerable<TCollection>>> selector,
            Func<TSource, TCollection, CancellationToken, AppaTask<TResult>> resultSelector)
        {
            this.source = source;
            selector1 = selector;
            selector2 = null;
            this.resultSelector = resultSelector;
        }

        public SelectManyAwaitWithCancellation(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, int, CancellationToken, AppaTask<IAppaTaskAsyncEnumerable<TCollection>>> selector,
            Func<TSource, TCollection, CancellationToken, AppaTask<TResult>> resultSelector)
        {
            this.source = source;
            selector1 = null;
            selector2 = selector;
            this.resultSelector = resultSelector;
        }

        public IAppaTaskAsyncEnumerator<TResult> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            return new _SelectManyAwaitWithCancellation(
                source,
                selector1,
                selector2,
                resultSelector,
                cancellationToken
            );
        }

        private sealed class _SelectManyAwaitWithCancellation : MoveNextSource,
                                                                IAppaTaskAsyncEnumerator<TResult>
        {
            private static readonly Action<object> sourceMoveNextCoreDelegate = SourceMoveNextCore;

            private static readonly Action<object> selectedSourceMoveNextCoreDelegate =
                SeletedSourceMoveNextCore;

            private static readonly Action<object> selectedEnumeratorDisposeAsyncCoreDelegate =
                SelectedEnumeratorDisposeAsyncCore;

            private static readonly Action<object> selectorAwaitCoreDelegate = SelectorAwaitCore;
            private static readonly Action<object> resultSelectorAwaitCoreDelegate = ResultSelectorAwaitCore;

            private readonly IAppaTaskAsyncEnumerable<TSource> source;

            private readonly Func<TSource, CancellationToken, AppaTask<IAppaTaskAsyncEnumerable<TCollection>>>
                selector1;

            private readonly
                Func<TSource, int, CancellationToken, AppaTask<IAppaTaskAsyncEnumerable<TCollection>>>
                selector2;

            private readonly Func<TSource, TCollection, CancellationToken, AppaTask<TResult>> resultSelector;
            private CancellationToken cancellationToken;

            private TSource sourceCurrent;
            private int sourceIndex;
            private IAppaTaskAsyncEnumerator<TSource> sourceEnumerator;
            private IAppaTaskAsyncEnumerator<TCollection> selectedEnumerator;
            private AppaTask<bool>.Awaiter sourceAwaiter;
            private AppaTask<bool>.Awaiter selectedAwaiter;
            private AppaTask.Awaiter selectedDisposeAsyncAwaiter;

            // await additional
            private AppaTask<IAppaTaskAsyncEnumerable<TCollection>>.Awaiter collectionSelectorAwaiter;
            private AppaTask<TResult>.Awaiter resultSelectorAwaiter;

            public _SelectManyAwaitWithCancellation(
                IAppaTaskAsyncEnumerable<TSource> source,
                Func<TSource, CancellationToken, AppaTask<IAppaTaskAsyncEnumerable<TCollection>>> selector1,
                Func<TSource, int, CancellationToken, AppaTask<IAppaTaskAsyncEnumerable<TCollection>>>
                    selector2,
                Func<TSource, TCollection, CancellationToken, AppaTask<TResult>> resultSelector,
                CancellationToken cancellationToken)
            {
                this.source = source;
                this.selector1 = selector1;
                this.selector2 = selector2;
                this.resultSelector = resultSelector;
                this.cancellationToken = cancellationToken;
                TaskTracker.TrackActiveTask(this, 3);
            }

            public TResult Current { get; private set; }

            public AppaTask<bool> MoveNextAsync()
            {
                completionSource.Reset();

                // iterate selected field
                if (selectedEnumerator != null)
                {
                    MoveNextSelected();
                }
                else
                {
                    // iterate source field
                    if (sourceEnumerator == null)
                    {
                        sourceEnumerator = source.GetAsyncEnumerator(cancellationToken);
                    }

                    MoveNextSource();
                }

                return new AppaTask<bool>(this, completionSource.Version);
            }

            private void MoveNextSource()
            {
                try
                {
                    sourceAwaiter = sourceEnumerator.MoveNextAsync().GetAwaiter();
                }
                catch (Exception ex)
                {
                    completionSource.TrySetException(ex);
                    return;
                }

                if (sourceAwaiter.IsCompleted)
                {
                    SourceMoveNextCore(this);
                }
                else
                {
                    sourceAwaiter.SourceOnCompleted(sourceMoveNextCoreDelegate, this);
                }
            }

            private void MoveNextSelected()
            {
                try
                {
                    selectedAwaiter = selectedEnumerator.MoveNextAsync().GetAwaiter();
                }
                catch (Exception ex)
                {
                    completionSource.TrySetException(ex);
                    return;
                }

                if (selectedAwaiter.IsCompleted)
                {
                    SeletedSourceMoveNextCore(this);
                }
                else
                {
                    selectedAwaiter.SourceOnCompleted(selectedSourceMoveNextCoreDelegate, this);
                }
            }

            private static void SourceMoveNextCore(object state)
            {
                var self = (_SelectManyAwaitWithCancellation)state;

                if (self.TryGetResult(self.sourceAwaiter, out var result))
                {
                    if (result)
                    {
                        try
                        {
                            self.sourceCurrent = self.sourceEnumerator.Current;

                            if (self.selector1 != null)
                            {
                                self.collectionSelectorAwaiter = self.selector1(
                                                                          self.sourceCurrent,
                                                                          self.cancellationToken
                                                                      )
                                                                     .GetAwaiter();
                            }
                            else
                            {
                                self.collectionSelectorAwaiter = self.selector2(
                                                                          self.sourceCurrent,
                                                                          checked(self.sourceIndex++),
                                                                          self.cancellationToken
                                                                      )
                                                                     .GetAwaiter();
                            }

                            if (self.collectionSelectorAwaiter.IsCompleted)
                            {
                                SelectorAwaitCore(self);
                            }
                            else
                            {
                                self.collectionSelectorAwaiter.SourceOnCompleted(
                                    selectorAwaitCoreDelegate,
                                    self
                                );
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

            private static void SeletedSourceMoveNextCore(object state)
            {
                var self = (_SelectManyAwaitWithCancellation)state;

                if (self.TryGetResult(self.selectedAwaiter, out var result))
                {
                    if (result)
                    {
                        try
                        {
                            self.resultSelectorAwaiter = self.resultSelector(
                                                                  self.sourceCurrent,
                                                                  self.selectedEnumerator.Current,
                                                                  self.cancellationToken
                                                              )
                                                             .GetAwaiter();
                            if (self.resultSelectorAwaiter.IsCompleted)
                            {
                                ResultSelectorAwaitCore(self);
                            }
                            else
                            {
                                self.resultSelectorAwaiter.SourceOnCompleted(
                                    resultSelectorAwaitCoreDelegate,
                                    self
                                );
                            }
                        }
                        catch (Exception ex)
                        {
                            self.completionSource.TrySetException(ex);
                        }
                    }
                    else
                    {
                        // dispose selected source and try iterate source.
                        try
                        {
                            self.selectedDisposeAsyncAwaiter =
                                self.selectedEnumerator.DisposeAsync().GetAwaiter();
                        }
                        catch (Exception ex)
                        {
                            self.completionSource.TrySetException(ex);
                            return;
                        }

                        if (self.selectedDisposeAsyncAwaiter.IsCompleted)
                        {
                            SelectedEnumeratorDisposeAsyncCore(self);
                        }
                        else
                        {
                            self.selectedDisposeAsyncAwaiter.SourceOnCompleted(
                                selectedEnumeratorDisposeAsyncCoreDelegate,
                                self
                            );
                        }
                    }
                }
            }

            private static void SelectedEnumeratorDisposeAsyncCore(object state)
            {
                var self = (_SelectManyAwaitWithCancellation)state;

                if (self.TryGetResult(self.selectedDisposeAsyncAwaiter))
                {
                    self.selectedEnumerator = null;
                    self.selectedAwaiter = default;

                    self.MoveNextSource(); // iterate next source
                }
            }

            private static void SelectorAwaitCore(object state)
            {
                var self = (_SelectManyAwaitWithCancellation)state;

                if (self.TryGetResult(self.collectionSelectorAwaiter, out var result))
                {
                    self.selectedEnumerator = result.GetAsyncEnumerator(self.cancellationToken);
                    self.MoveNextSelected(); // iterated selected source.
                }
            }

            private static void ResultSelectorAwaitCore(object state)
            {
                var self = (_SelectManyAwaitWithCancellation)state;

                if (self.TryGetResult(self.resultSelectorAwaiter, out var result))
                {
                    self.Current = result;
                    self.completionSource.TrySetResult(true);
                }
            }

            public async AppaTask DisposeAsync()
            {
                TaskTracker.RemoveTracking(this);
                if (selectedEnumerator != null)
                {
                    await selectedEnumerator.DisposeAsync();
                }

                if (sourceEnumerator != null)
                {
                    await sourceEnumerator.DisposeAsync();
                }
            }
        }
    }
}
