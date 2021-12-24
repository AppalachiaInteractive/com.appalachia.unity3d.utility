﻿using System;
using System.Threading;

namespace Appalachia.Utility.Async.Linq
{
    // note: refactor all inherit class and should remove this.
    // see Select and Where.
    internal abstract class AsyncEnumeratorBase<TSource, TResult> : MoveNextSource,
                                                                    IAppaTaskAsyncEnumerator<TResult>
    {
        private static readonly Action<object> moveNextCallbackDelegate = MoveNextCallBack;

        private readonly IAppaTaskAsyncEnumerable<TSource> source;
        protected CancellationToken cancellationToken;

        private IAppaTaskAsyncEnumerator<TSource> enumerator;
        private AppaTask<bool>.Awaiter sourceMoveNext;

        public AsyncEnumeratorBase(
            IAppaTaskAsyncEnumerable<TSource> source,
            CancellationToken cancellationToken)
        {
            this.source = source;
            this.cancellationToken = cancellationToken;
            TaskTracker.TrackActiveTask(this, 4);
        }

        // abstract

        /// <summary>
        ///     If return value is false, continue source.MoveNext.
        /// </summary>
        protected abstract bool TryMoveNextCore(bool sourceHasCurrent, out bool result);

        // Util
        protected TSource SourceCurrent => enumerator.Current;

        // IAppaTaskAsyncEnumerator<T>

        public TResult Current { get; protected set; }

        public AppaTask<bool> MoveNextAsync()
        {
            if (enumerator == null)
            {
                enumerator = source.GetAsyncEnumerator(cancellationToken);
            }

            completionSource.Reset();
            if (!OnFirstIteration())
            {
                SourceMoveNext();
            }

            return new AppaTask<bool>(this, completionSource.Version);
        }

        protected virtual bool OnFirstIteration()
        {
            return false;
        }

        protected void SourceMoveNext()
        {
            CONTINUE:
            sourceMoveNext = enumerator.MoveNextAsync().GetAwaiter();
            if (sourceMoveNext.IsCompleted)
            {
                var result = false;
                try
                {
                    if (!TryMoveNextCore(sourceMoveNext.GetResult(), out result))
                    {
                        goto CONTINUE;
                    }
                }
                catch (Exception ex)
                {
                    completionSource.TrySetException(ex);
                    return;
                }

                if (cancellationToken.IsCancellationRequested)
                {
                    completionSource.TrySetCanceled(cancellationToken);
                }
                else
                {
                    completionSource.TrySetResult(result);
                }
            }
            else
            {
                sourceMoveNext.SourceOnCompleted(moveNextCallbackDelegate, this);
            }
        }

        private static void MoveNextCallBack(object state)
        {
            var self = (AsyncEnumeratorBase<TSource, TResult>)state;
            bool result;
            try
            {
                if (!self.TryMoveNextCore(self.sourceMoveNext.GetResult(), out result))
                {
                    self.SourceMoveNext();
                    return;
                }
            }
            catch (Exception ex)
            {
                self.completionSource.TrySetException(ex);
                return;
            }

            if (self.cancellationToken.IsCancellationRequested)
            {
                self.completionSource.TrySetCanceled(self.cancellationToken);
            }
            else
            {
                self.completionSource.TrySetResult(result);
            }
        }

        // if require additional resource to dispose, override and call base.DisposeAsync.
        public virtual AppaTask DisposeAsync()
        {
            TaskTracker.RemoveTracking(this);
            if (enumerator != null)
            {
                return enumerator.DisposeAsync();
            }

            return default;
        }
    }

    internal abstract class AsyncEnumeratorAwaitSelectorBase<TSource, TResult, TAwait> : MoveNextSource,
        IAppaTaskAsyncEnumerator<TResult>
    {
        private static readonly Action<object> moveNextCallbackDelegate = MoveNextCallBack;
        private static readonly Action<object> setCurrentCallbackDelegate = SetCurrentCallBack;

        private readonly IAppaTaskAsyncEnumerable<TSource> source;
        protected CancellationToken cancellationToken;

        private IAppaTaskAsyncEnumerator<TSource> enumerator;
        private AppaTask<bool>.Awaiter sourceMoveNext;

        private AppaTask<TAwait>.Awaiter resultAwaiter;

        public AsyncEnumeratorAwaitSelectorBase(
            IAppaTaskAsyncEnumerable<TSource> source,
            CancellationToken cancellationToken)
        {
            this.source = source;
            this.cancellationToken = cancellationToken;
            TaskTracker.TrackActiveTask(this, 4);
        }

        // abstract

        protected abstract AppaTask<TAwait> TransformAsync(TSource sourceCurrent);
        protected abstract bool TrySetCurrentCore(TAwait awaitResult, out bool terminateIteration);

        // Util
        protected TSource SourceCurrent { get; private set; }

        protected (bool waitCallback, bool requireNextIteration) ActionCompleted(
            bool trySetCurrentResult,
            out bool moveNextResult)
        {
            if (trySetCurrentResult)
            {
                moveNextResult = true;
                return (false, false);
            }

            moveNextResult = default;
            return (false, true);
        }

        protected (bool waitCallback, bool requireNextIteration) WaitAwaitCallback(out bool moveNextResult)
        {
            moveNextResult = default;
            return (true, false);
        }

        protected (bool waitCallback, bool requireNextIteration) IterateFinished(out bool moveNextResult)
        {
            moveNextResult = false;
            return (false, false);
        }

        // IAppaTaskAsyncEnumerator<T>

        public TResult Current { get; protected set; }

        public AppaTask<bool> MoveNextAsync()
        {
            if (enumerator == null)
            {
                enumerator = source.GetAsyncEnumerator(cancellationToken);
            }

            completionSource.Reset();
            SourceMoveNext();
            return new AppaTask<bool>(this, completionSource.Version);
        }

        protected void SourceMoveNext()
        {
            CONTINUE:
            sourceMoveNext = enumerator.MoveNextAsync().GetAwaiter();
            if (sourceMoveNext.IsCompleted)
            {
                var result = false;
                try
                {
                    (var waitCallback, var requireNextIteration) = TryMoveNextCore(
                        sourceMoveNext.GetResult(),
                        out result
                    );

                    if (waitCallback)
                    {
                        return;
                    }

                    if (requireNextIteration)
                    {
                        goto CONTINUE;
                    }

                    completionSource.TrySetResult(result);
                }
                catch (Exception ex)
                {
                    completionSource.TrySetException(ex);
                }
            }
            else
            {
                sourceMoveNext.SourceOnCompleted(moveNextCallbackDelegate, this);
            }
        }

        private (bool waitCallback, bool requireNextIteration) TryMoveNextCore(
            bool sourceHasCurrent,
            out bool result)
        {
            if (sourceHasCurrent)
            {
                SourceCurrent = enumerator.Current;
                var task = TransformAsync(SourceCurrent);
                if (UnwarapTask(task, out var taskResult))
                {
                    var currentResult = TrySetCurrentCore(taskResult, out var terminateIteration);
                    if (terminateIteration)
                    {
                        return IterateFinished(out result);
                    }

                    return ActionCompleted(currentResult, out result);
                }

                return WaitAwaitCallback(out result);
            }

            return IterateFinished(out result);
        }

        protected bool UnwarapTask(AppaTask<TAwait> taskResult, out TAwait result)
        {
            resultAwaiter = taskResult.GetAwaiter();

            if (resultAwaiter.IsCompleted)
            {
                result = resultAwaiter.GetResult();
                return true;
            }

            resultAwaiter.SourceOnCompleted(setCurrentCallbackDelegate, this);
            result = default;
            return false;
        }

        private static void MoveNextCallBack(object state)
        {
            var self = (AsyncEnumeratorAwaitSelectorBase<TSource, TResult, TAwait>)state;
            var result = false;
            try
            {
                (var waitCallback, var requireNextIteration) = self.TryMoveNextCore(
                    self.sourceMoveNext.GetResult(),
                    out result
                );

                if (waitCallback)
                {
                    return;
                }

                if (requireNextIteration)
                {
                    self.SourceMoveNext();
                    return;
                }

                self.completionSource.TrySetResult(result);
            }
            catch (Exception ex)
            {
                self.completionSource.TrySetException(ex);
            }
        }

        private static void SetCurrentCallBack(object state)
        {
            var self = (AsyncEnumeratorAwaitSelectorBase<TSource, TResult, TAwait>)state;

            bool doneSetCurrent;
            bool terminateIteration;
            try
            {
                var result = self.resultAwaiter.GetResult();
                doneSetCurrent = self.TrySetCurrentCore(result, out terminateIteration);
            }
            catch (Exception ex)
            {
                self.completionSource.TrySetException(ex);
                return;
            }

            if (self.cancellationToken.IsCancellationRequested)
            {
                self.completionSource.TrySetCanceled(self.cancellationToken);
            }
            else
            {
                if (doneSetCurrent)
                {
                    self.completionSource.TrySetResult(true);
                }
                else
                {
                    if (terminateIteration)
                    {
                        self.completionSource.TrySetResult(false);
                    }
                    else
                    {
                        self.SourceMoveNext();
                    }
                }
            }
        }

        // if require additional resource to dispose, override and call base.DisposeAsync.
        public virtual AppaTask DisposeAsync()
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
