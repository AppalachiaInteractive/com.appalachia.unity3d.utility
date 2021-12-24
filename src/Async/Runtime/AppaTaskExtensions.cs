#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Collections;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;
using Appalachia.Utility.Async.Internal;

namespace Appalachia.Utility.Async
{
    public static partial class AppaTaskExtensions
    {
        /// <summary>
        ///     Convert Task[T] -> AppaTask[T].
        /// </summary>
        public static AppaTask<T> AsAppaTask<T>(
            this Task<T> task,
            bool useCurrentSynchronizationContext = true)
        {
            var promise = new AppaTaskCompletionSource<T>();

            task.ContinueWith(
                (x, state) =>
                {
                    var p = (AppaTaskCompletionSource<T>)state;

                    switch (x.Status)
                    {
                        case TaskStatus.Canceled:
                            p.TrySetCanceled();
                            break;
                        case TaskStatus.Faulted:
                            p.TrySetException(x.Exception);
                            break;
                        case TaskStatus.RanToCompletion:
                            p.TrySetResult(x.Result);
                            break;
                        default:
                            throw new NotSupportedException();
                    }
                },
                promise,
                useCurrentSynchronizationContext
                    ? TaskScheduler.FromCurrentSynchronizationContext()
                    : TaskScheduler.Current
            );

            return promise.Task;
        }

        /// <summary>
        ///     Convert Task -> AppaTask.
        /// </summary>
        public static AppaTask AsAppaTask(this Task task, bool useCurrentSynchronizationContext = true)
        {
            var promise = new AppaTaskCompletionSource();

            task.ContinueWith(
                (x, state) =>
                {
                    var p = (AppaTaskCompletionSource)state;

                    switch (x.Status)
                    {
                        case TaskStatus.Canceled:
                            p.TrySetCanceled();
                            break;
                        case TaskStatus.Faulted:
                            p.TrySetException(x.Exception);
                            break;
                        case TaskStatus.RanToCompletion:
                            p.TrySetResult();
                            break;
                        default:
                            throw new NotSupportedException();
                    }
                },
                promise,
                useCurrentSynchronizationContext
                    ? TaskScheduler.FromCurrentSynchronizationContext()
                    : TaskScheduler.Current
            );

            return promise.Task;
        }

        public static Task<T> AsTask<T>(this AppaTask<T> task)
        {
            try
            {
                AppaTask<T>.Awaiter awaiter;
                try
                {
                    awaiter = task.GetAwaiter();
                }
                catch (Exception ex)
                {
                    return Task.FromException<T>(ex);
                }

                if (awaiter.IsCompleted)
                {
                    try
                    {
                        var result = awaiter.GetResult();
                        return Task.FromResult(result);
                    }
                    catch (Exception ex)
                    {
                        return Task.FromException<T>(ex);
                    }
                }

                var tcs = new TaskCompletionSource<T>();

                awaiter.SourceOnCompleted(
                    state =>
                    {
                        using (var tuple = (StateTuple<TaskCompletionSource<T>, AppaTask<T>.Awaiter>)state)
                        {
                            var (inTcs, inAwaiter) = tuple;
                            try
                            {
                                var result = inAwaiter.GetResult();
                                inTcs.SetResult(result);
                            }
                            catch (Exception ex)
                            {
                                inTcs.SetException(ex);
                            }
                        }
                    },
                    StateTuple.Create(tcs, awaiter)
                );

                return tcs.Task;
            }
            catch (Exception ex)
            {
                return Task.FromException<T>(ex);
            }
        }

        public static Task AsTask(this AppaTask task)
        {
            try
            {
                AppaTask.Awaiter awaiter;
                try
                {
                    awaiter = task.GetAwaiter();
                }
                catch (Exception ex)
                {
                    return Task.FromException(ex);
                }

                if (awaiter.IsCompleted)
                {
                    try
                    {
                        awaiter.GetResult(); // check token valid on Succeeded
                        return Task.CompletedTask;
                    }
                    catch (Exception ex)
                    {
                        return Task.FromException(ex);
                    }
                }

                var tcs = new TaskCompletionSource<object>();

                awaiter.SourceOnCompleted(
                    state =>
                    {
                        using (var tuple = (StateTuple<TaskCompletionSource<object>, AppaTask.Awaiter>)state)
                        {
                            var (inTcs, inAwaiter) = tuple;
                            try
                            {
                                inAwaiter.GetResult();
                                inTcs.SetResult(null);
                            }
                            catch (Exception ex)
                            {
                                inTcs.SetException(ex);
                            }
                        }
                    },
                    StateTuple.Create(tcs, awaiter)
                );

                return tcs.Task;
            }
            catch (Exception ex)
            {
                return Task.FromException(ex);
            }
        }

        public static AsyncLazy ToAsyncLazy(this AppaTask task)
        {
            return new AsyncLazy(task);
        }

        public static AsyncLazy<T> ToAsyncLazy<T>(this AppaTask<T> task)
        {
            return new AsyncLazy<T>(task);
        }

        /// <summary>
        ///     Ignore task result when cancel raised first.
        /// </summary>
        public static AppaTask AttachExternalCancellation(
            this AppaTask task,
            CancellationToken cancellationToken)
        {
            if (!cancellationToken.CanBeCanceled)
            {
                return task;
            }

            if (cancellationToken.IsCancellationRequested)
            {
                return AppaTask.FromCanceled(cancellationToken);
            }

            if (task.Status.IsCompleted())
            {
                return task;
            }

            return new AppaTask(new AttachExternalCancellationSource(task, cancellationToken), 0);
        }

        /// <summary>
        ///     Ignore task result when cancel raised first.
        /// </summary>
        public static AppaTask<T> AttachExternalCancellation<T>(
            this AppaTask<T> task,
            CancellationToken cancellationToken)
        {
            if (!cancellationToken.CanBeCanceled)
            {
                return task;
            }

            if (cancellationToken.IsCancellationRequested)
            {
                return AppaTask.FromCanceled<T>(cancellationToken);
            }

            if (task.Status.IsCompleted())
            {
                return task;
            }

            return new AppaTask<T>(new AttachExternalCancellationSource<T>(task, cancellationToken), 0);
        }

        private sealed class AttachExternalCancellationSource : IAppaTaskSource
        {
            private static readonly Action<object> cancellationCallbackDelegate = CancellationCallback;

            private CancellationToken cancellationToken;
            private CancellationTokenRegistration tokenRegistration;
            private AppaTaskCompletionSourceCore<AsyncUnit> core;

            public AttachExternalCancellationSource(AppaTask task, CancellationToken cancellationToken)
            {
                this.cancellationToken = cancellationToken;
                tokenRegistration =
                    cancellationToken.RegisterWithoutCaptureExecutionContext(
                        cancellationCallbackDelegate,
                        this
                    );
                RunTask(task).Forget();
            }

            private async AppaTaskVoid RunTask(AppaTask task)
            {
                try
                {
                    await task;
                    core.TrySetResult(AsyncUnit.Default);
                }
                catch (Exception ex)
                {
                    core.TrySetException(ex);
                }
                finally
                {
                    tokenRegistration.Dispose();
                }
            }

            private static void CancellationCallback(object state)
            {
                var self = (AttachExternalCancellationSource)state;
                self.core.TrySetCanceled(self.cancellationToken);
            }

            public void GetResult(short token)
            {
                core.GetResult(token);
            }

            public AppaTaskStatus GetStatus(short token)
            {
                return core.GetStatus(token);
            }

            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                core.OnCompleted(continuation, state, token);
            }

            public AppaTaskStatus UnsafeGetStatus()
            {
                return core.UnsafeGetStatus();
            }
        }

        private sealed class AttachExternalCancellationSource<T> : IAppaTaskSource<T>
        {
            private static readonly Action<object> cancellationCallbackDelegate = CancellationCallback;

            private CancellationToken cancellationToken;
            private CancellationTokenRegistration tokenRegistration;
            private AppaTaskCompletionSourceCore<T> core;

            public AttachExternalCancellationSource(AppaTask<T> task, CancellationToken cancellationToken)
            {
                this.cancellationToken = cancellationToken;
                tokenRegistration =
                    cancellationToken.RegisterWithoutCaptureExecutionContext(
                        cancellationCallbackDelegate,
                        this
                    );
                RunTask(task).Forget();
            }

            private async AppaTaskVoid RunTask(AppaTask<T> task)
            {
                try
                {
                    core.TrySetResult(await task);
                }
                catch (Exception ex)
                {
                    core.TrySetException(ex);
                }
                finally
                {
                    tokenRegistration.Dispose();
                }
            }

            private static void CancellationCallback(object state)
            {
                var self = (AttachExternalCancellationSource<T>)state;
                self.core.TrySetCanceled(self.cancellationToken);
            }

            void IAppaTaskSource.GetResult(short token)
            {
                core.GetResult(token);
            }

            public T GetResult(short token)
            {
                return core.GetResult(token);
            }

            public AppaTaskStatus GetStatus(short token)
            {
                return core.GetStatus(token);
            }

            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                core.OnCompleted(continuation, state, token);
            }

            public AppaTaskStatus UnsafeGetStatus()
            {
                return core.UnsafeGetStatus();
            }
        }

#if UNITY_2018_3_OR_NEWER

        public static IEnumerator ToCoroutine<T>(
            this AppaTask<T> task,
            Action<T> resultHandler = null,
            Action<Exception> exceptionHandler = null)
        {
            return new ToCoroutineEnumerator<T>(task, resultHandler, exceptionHandler);
        }

        public static IEnumerator ToCoroutine(this AppaTask task, Action<Exception> exceptionHandler = null)
        {
            return new ToCoroutineEnumerator(task, exceptionHandler);
        }

        public static async AppaTask Timeout(
            this AppaTask task,
            TimeSpan timeout,
            DelayType delayType = DelayType.DeltaTime,
            PlayerLoopTiming timeoutCheckTiming = PlayerLoopTiming.Update,
            CancellationTokenSource taskCancellationTokenSource = null)
        {
            var delayCancellationTokenSource = new CancellationTokenSource();
            var timeoutTask = AppaTask.Delay(
                                           timeout,
                                           delayType,
                                           timeoutCheckTiming,
                                           delayCancellationTokenSource.Token
                                       )
                                      .SuppressCancellationThrow();

            int winArgIndex;
            bool taskResultIsCanceled;
            try
            {
                (winArgIndex, taskResultIsCanceled, _) = await AppaTask.WhenAny(
                    task.SuppressCancellationThrow(),
                    timeoutTask
                );
            }
            catch
            {
                delayCancellationTokenSource.Cancel();
                delayCancellationTokenSource.Dispose();
                throw;
            }

            // timeout
            if (winArgIndex == 1)
            {
                if (taskCancellationTokenSource != null)
                {
                    taskCancellationTokenSource.Cancel();
                    taskCancellationTokenSource.Dispose();
                }

                throw new TimeoutException("Exceed Timeout:" + timeout);
            }

            delayCancellationTokenSource.Cancel();
            delayCancellationTokenSource.Dispose();

            if (taskResultIsCanceled)
            {
                Error.ThrowOperationCanceledException();
            }
        }

        public static async AppaTask<T> Timeout<T>(
            this AppaTask<T> task,
            TimeSpan timeout,
            DelayType delayType = DelayType.DeltaTime,
            PlayerLoopTiming timeoutCheckTiming = PlayerLoopTiming.Update,
            CancellationTokenSource taskCancellationTokenSource = null)
        {
            var delayCancellationTokenSource = new CancellationTokenSource();
            var timeoutTask = AppaTask.Delay(
                                           timeout,
                                           delayType,
                                           timeoutCheckTiming,
                                           delayCancellationTokenSource.Token
                                       )
                                      .SuppressCancellationThrow();

            int winArgIndex;
            (bool IsCanceled, T Result) taskResult;
            try
            {
                (winArgIndex, taskResult, _) = await AppaTask.WhenAny(
                    task.SuppressCancellationThrow(),
                    timeoutTask
                );
            }
            catch
            {
                delayCancellationTokenSource.Cancel();
                delayCancellationTokenSource.Dispose();
                throw;
            }

            // timeout
            if (winArgIndex == 1)
            {
                if (taskCancellationTokenSource != null)
                {
                    taskCancellationTokenSource.Cancel();
                    taskCancellationTokenSource.Dispose();
                }

                throw new TimeoutException("Exceed Timeout:" + timeout);
            }

            delayCancellationTokenSource.Cancel();
            delayCancellationTokenSource.Dispose();

            if (taskResult.IsCanceled)
            {
                Error.ThrowOperationCanceledException();
            }

            return taskResult.Result;
        }

        /// <summary>
        ///     Timeout with suppress OperationCanceledException. Returns (bool, IsCacneled).
        /// </summary>
        public static async AppaTask<bool> TimeoutWithoutException(
            this AppaTask task,
            TimeSpan timeout,
            DelayType delayType = DelayType.DeltaTime,
            PlayerLoopTiming timeoutCheckTiming = PlayerLoopTiming.Update,
            CancellationTokenSource taskCancellationTokenSource = null)
        {
            var delayCancellationTokenSource = new CancellationTokenSource();
            var timeoutTask = AppaTask.Delay(
                                           timeout,
                                           delayType,
                                           timeoutCheckTiming,
                                           delayCancellationTokenSource.Token
                                       )
                                      .SuppressCancellationThrow();

            int winArgIndex;
            bool taskResultIsCanceled;
            try
            {
                (winArgIndex, taskResultIsCanceled, _) = await AppaTask.WhenAny(
                    task.SuppressCancellationThrow(),
                    timeoutTask
                );
            }
            catch
            {
                delayCancellationTokenSource.Cancel();
                delayCancellationTokenSource.Dispose();
                return true;
            }

            // timeout
            if (winArgIndex == 1)
            {
                if (taskCancellationTokenSource != null)
                {
                    taskCancellationTokenSource.Cancel();
                    taskCancellationTokenSource.Dispose();
                }

                return true;
            }

            delayCancellationTokenSource.Cancel();
            delayCancellationTokenSource.Dispose();

            if (taskResultIsCanceled)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        ///     Timeout with suppress OperationCanceledException. Returns (bool IsTimeout, T Result).
        /// </summary>
        public static async AppaTask<(bool IsTimeout, T Result)> TimeoutWithoutException<T>(
            this AppaTask<T> task,
            TimeSpan timeout,
            DelayType delayType = DelayType.DeltaTime,
            PlayerLoopTiming timeoutCheckTiming = PlayerLoopTiming.Update,
            CancellationTokenSource taskCancellationTokenSource = null)
        {
            var delayCancellationTokenSource = new CancellationTokenSource();
            var timeoutTask = AppaTask.Delay(
                                           timeout,
                                           delayType,
                                           timeoutCheckTiming,
                                           delayCancellationTokenSource.Token
                                       )
                                      .SuppressCancellationThrow();

            int winArgIndex;
            (bool IsCanceled, T Result) taskResult;
            try
            {
                (winArgIndex, taskResult, _) = await AppaTask.WhenAny(
                    task.SuppressCancellationThrow(),
                    timeoutTask
                );
            }
            catch
            {
                delayCancellationTokenSource.Cancel();
                delayCancellationTokenSource.Dispose();
                return (true, default);
            }

            // timeout
            if (winArgIndex == 1)
            {
                if (taskCancellationTokenSource != null)
                {
                    taskCancellationTokenSource.Cancel();
                    taskCancellationTokenSource.Dispose();
                }

                return (true, default);
            }

            delayCancellationTokenSource.Cancel();
            delayCancellationTokenSource.Dispose();

            if (taskResult.IsCanceled)
            {
                return (true, default);
            }

            return (false, taskResult.Result);
        }

#endif

        public static void Forget(this AppaTask task)
        {
            var awaiter = task.GetAwaiter();
            if (awaiter.IsCompleted)
            {
                try
                {
                    awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    AppaTaskScheduler.PublishUnobservedTaskException(ex);
                }
            }
            else
            {
                awaiter.SourceOnCompleted(
                    state =>
                    {
                        using (var t = (StateTuple<AppaTask.Awaiter>)state)
                        {
                            try
                            {
                                t.Item1.GetResult();
                            }
                            catch (Exception ex)
                            {
                                AppaTaskScheduler.PublishUnobservedTaskException(ex);
                            }
                        }
                    },
                    StateTuple.Create(awaiter)
                );
            }
        }

        public static void Forget(
            this AppaTask task,
            Action<Exception> exceptionHandler,
            bool handleExceptionOnMainThread = true)
        {
            if (exceptionHandler == null)
            {
                Forget(task);
            }
            else
            {
                ForgetCoreWithCatch(task, exceptionHandler, handleExceptionOnMainThread).Forget();
            }
        }

        private static async AppaTaskVoid ForgetCoreWithCatch(
            AppaTask task,
            Action<Exception> exceptionHandler,
            bool handleExceptionOnMainThread)
        {
            try
            {
                await task;
            }
            catch (Exception ex)
            {
                try
                {
                    if (handleExceptionOnMainThread)
                    {
#if UNITY_2018_3_OR_NEWER
                        await AppaTask.SwitchToMainThread();
#endif
                    }

                    exceptionHandler(ex);
                }
                catch (Exception ex2)
                {
                    AppaTaskScheduler.PublishUnobservedTaskException(ex2);
                }
            }
        }

        public static void Forget<T>(this AppaTask<T> task)
        {
            var awaiter = task.GetAwaiter();
            if (awaiter.IsCompleted)
            {
                try
                {
                    awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    AppaTaskScheduler.PublishUnobservedTaskException(ex);
                }
            }
            else
            {
                awaiter.SourceOnCompleted(
                    state =>
                    {
                        using (var t = (StateTuple<AppaTask<T>.Awaiter>)state)
                        {
                            try
                            {
                                t.Item1.GetResult();
                            }
                            catch (Exception ex)
                            {
                                AppaTaskScheduler.PublishUnobservedTaskException(ex);
                            }
                        }
                    },
                    StateTuple.Create(awaiter)
                );
            }
        }

        public static void Forget<T>(
            this AppaTask<T> task,
            Action<Exception> exceptionHandler,
            bool handleExceptionOnMainThread = true)
        {
            if (exceptionHandler == null)
            {
                task.Forget();
            }
            else
            {
                ForgetCoreWithCatch(task, exceptionHandler, handleExceptionOnMainThread).Forget();
            }
        }

        private static async AppaTaskVoid ForgetCoreWithCatch<T>(
            AppaTask<T> task,
            Action<Exception> exceptionHandler,
            bool handleExceptionOnMainThread)
        {
            try
            {
                await task;
            }
            catch (Exception ex)
            {
                try
                {
                    if (handleExceptionOnMainThread)
                    {
#if UNITY_2018_3_OR_NEWER
                        await AppaTask.SwitchToMainThread();
#endif
                    }

                    exceptionHandler(ex);
                }
                catch (Exception ex2)
                {
                    AppaTaskScheduler.PublishUnobservedTaskException(ex2);
                }
            }
        }

        public static async AppaTask ContinueWith<T>(this AppaTask<T> task, Action<T> continuationFunction)
        {
            continuationFunction(await task);
        }

        public static async AppaTask ContinueWith<T>(
            this AppaTask<T> task,
            Func<T, AppaTask> continuationFunction)
        {
            await continuationFunction(await task);
        }

        public static async AppaTask<TR> ContinueWith<T, TR>(
            this AppaTask<T> task,
            Func<T, TR> continuationFunction)
        {
            return continuationFunction(await task);
        }

        public static async AppaTask<TR> ContinueWith<T, TR>(
            this AppaTask<T> task,
            Func<T, AppaTask<TR>> continuationFunction)
        {
            return await continuationFunction(await task);
        }

        public static async AppaTask ContinueWith(this AppaTask task, Action continuationFunction)
        {
            await task;
            continuationFunction();
        }

        public static async AppaTask ContinueWith(this AppaTask task, Func<AppaTask> continuationFunction)
        {
            await task;
            await continuationFunction();
        }

        public static async AppaTask<T> ContinueWith<T>(this AppaTask task, Func<T> continuationFunction)
        {
            await task;
            return continuationFunction();
        }

        public static async AppaTask<T> ContinueWith<T>(
            this AppaTask task,
            Func<AppaTask<T>> continuationFunction)
        {
            await task;
            return await continuationFunction();
        }

        public static async AppaTask<T> Unwrap<T>(this AppaTask<AppaTask<T>> task)
        {
            return await await task;
        }

        public static async AppaTask Unwrap(this AppaTask<AppaTask> task)
        {
            await await task;
        }

        public static async AppaTask<T> Unwrap<T>(this Task<AppaTask<T>> task)
        {
            return await await task;
        }

        public static async AppaTask<T> Unwrap<T>(this Task<AppaTask<T>> task, bool continueOnCapturedContext)
        {
            return await await task.ConfigureAwait(continueOnCapturedContext);
        }

        public static async AppaTask Unwrap(this Task<AppaTask> task)
        {
            await await task;
        }

        public static async AppaTask Unwrap(this Task<AppaTask> task, bool continueOnCapturedContext)
        {
            await await task.ConfigureAwait(continueOnCapturedContext);
        }

        public static async AppaTask<T> Unwrap<T>(this AppaTask<Task<T>> task)
        {
            return await await task;
        }

        public static async AppaTask<T> Unwrap<T>(this AppaTask<Task<T>> task, bool continueOnCapturedContext)
        {
            return await (await task).ConfigureAwait(continueOnCapturedContext);
        }

        public static async AppaTask Unwrap(this AppaTask<Task> task)
        {
            await await task;
        }

        public static async AppaTask Unwrap(this AppaTask<Task> task, bool continueOnCapturedContext)
        {
            await (await task).ConfigureAwait(continueOnCapturedContext);
        }

#if UNITY_2018_3_OR_NEWER

        private sealed class ToCoroutineEnumerator : IEnumerator
        {
            private bool completed;
            private AppaTask task;
            private Action<Exception> exceptionHandler;
            private bool isStarted;
            private ExceptionDispatchInfo exception;

            public ToCoroutineEnumerator(AppaTask task, Action<Exception> exceptionHandler)
            {
                completed = false;
                this.exceptionHandler = exceptionHandler;
                this.task = task;
            }

            private async AppaTaskVoid RunTask(AppaTask task)
            {
                try
                {
                    await task;
                }
                catch (Exception ex)
                {
                    if (exceptionHandler != null)
                    {
                        exceptionHandler(ex);
                    }
                    else
                    {
                        exception = ExceptionDispatchInfo.Capture(ex);
                    }
                }
                finally
                {
                    completed = true;
                }
            }

            public object Current => null;

            public bool MoveNext()
            {
                if (!isStarted)
                {
                    isStarted = true;
                    RunTask(task).Forget();
                }

                if (exception != null)
                {
                    exception.Throw();
                    return false;
                }

                return !completed;
            }

            void IEnumerator.Reset()
            {
            }
        }

        private sealed class ToCoroutineEnumerator<T> : IEnumerator
        {
            private bool completed;
            private Action<T> resultHandler;
            private Action<Exception> exceptionHandler;
            private bool isStarted;
            private AppaTask<T> task;
            private object current;
            private ExceptionDispatchInfo exception;

            public ToCoroutineEnumerator(
                AppaTask<T> task,
                Action<T> resultHandler,
                Action<Exception> exceptionHandler)
            {
                completed = false;
                this.task = task;
                this.resultHandler = resultHandler;
                this.exceptionHandler = exceptionHandler;
            }

            private async AppaTaskVoid RunTask(AppaTask<T> task)
            {
                try
                {
                    var value = await task;
                    current = value; // boxed if T is struct...
                    if (resultHandler != null)
                    {
                        resultHandler(value);
                    }
                }
                catch (Exception ex)
                {
                    if (exceptionHandler != null)
                    {
                        exceptionHandler(ex);
                    }
                    else
                    {
                        exception = ExceptionDispatchInfo.Capture(ex);
                    }
                }
                finally
                {
                    completed = true;
                }
            }

            public object Current => current;

            public bool MoveNext()
            {
                if (!isStarted)
                {
                    isStarted = true;
                    RunTask(task).Forget();
                }

                if (exception != null)
                {
                    exception.Throw();
                    return false;
                }

                return !completed;
            }

            void IEnumerator.Reset()
            {
            }
        }

#endif
    }
}
