﻿// asmdef Version Defines, enabled when com.unity.addressables is imported.

#if APPATASK_ADDRESSABLE_SUPPORT

using System;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Threading;
using Appalachia.Utility.Async.Internal;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Appalachia.Utility.Async.External.Addressables
{
    public static class AddressablesAsyncExtensions
    {
        #region AsyncOperationHandle

        public static AppaTask.Awaiter GetAwaiter(this AsyncOperationHandle handle)
        {
            return ToAppaTask(handle).GetAwaiter();
        }

        public static AppaTask WithCancellation(
            this AsyncOperationHandle handle,
            CancellationToken cancellationToken)
        {
            return ToAppaTask(handle, cancellationToken: cancellationToken);
        }

        public static AppaTask ToAppaTask(
            this AsyncOperationHandle handle,
            IProgress<float> progress = null,
            PlayerLoopTiming timing = PlayerLoopTiming.Update,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return AppaTask.FromCanceled(cancellationToken);
            }

            if (!handle.IsValid())
            {
                // autoReleaseHandle:true handle is invalid(immediately internal handle == null) so return completed.
                return AppaTask.CompletedTask;
            }

            if (handle.IsDone)
            {
                if (handle.Status == AsyncOperationStatus.Failed)
                {
                    return AppaTask.FromException(handle.OperationException);
                }

                return AppaTask.CompletedTask;
            }

            return new AppaTask(
                AsyncOperationHandleConfiguredSource.Create(
                    handle,
                    timing,
                    progress,
                    cancellationToken,
                    out var token
                ),
                token
            );
        }

        public struct AsyncOperationHandleAwaiter : ICriticalNotifyCompletion
        {
            private AsyncOperationHandle handle;
            private Action<AsyncOperationHandle> continuationAction;

            public AsyncOperationHandleAwaiter(AsyncOperationHandle handle)
            {
                this.handle = handle;
                continuationAction = null;
            }

            public bool IsCompleted => handle.IsDone;

            public void GetResult()
            {
                if (continuationAction != null)
                {
                    handle.Completed -= continuationAction;
                    continuationAction = null;
                }

                if (handle.Status == AsyncOperationStatus.Failed)
                {
                    var e = handle.OperationException;
                    handle = default;
                    ExceptionDispatchInfo.Capture(e).Throw();
                }

                var result = handle.Result;
                handle = default;
            }

            public void OnCompleted(Action continuation)
            {
                UnsafeOnCompleted(continuation);
            }

            public void UnsafeOnCompleted(Action continuation)
            {
                Error.ThrowWhenContinuationIsAlreadyRegistered(continuationAction);
                continuationAction = PooledDelegate<AsyncOperationHandle>.Create(continuation);
                handle.Completed += continuationAction;
            }
        }

        private sealed class AsyncOperationHandleConfiguredSource : IAppaTaskSource,
                                                                    IPlayerLoopItem,
                                                                    ITaskPoolNode<
                                                                        AsyncOperationHandleConfiguredSource>
        {
            private static TaskPool<AsyncOperationHandleConfiguredSource> pool;
            private AsyncOperationHandleConfiguredSource nextNode;
            public ref AsyncOperationHandleConfiguredSource NextNode => ref nextNode;

            static AsyncOperationHandleConfiguredSource()
            {
                TaskPool.RegisterSizeGetter(typeof(AsyncOperationHandleConfiguredSource), () => pool.Size);
            }

            private readonly Action<AsyncOperationHandle> continuationAction;
            private AsyncOperationHandle handle;
            private CancellationToken cancellationToken;
            private IProgress<float> progress;
            private bool completed;

            private AppaTaskCompletionSourceCore<AsyncUnit> core;

            private AsyncOperationHandleConfiguredSource()
            {
                continuationAction = Continuation;
            }

            public static IAppaTaskSource Create(
                AsyncOperationHandle handle,
                PlayerLoopTiming timing,
                IProgress<float> progress,
                CancellationToken cancellationToken,
                out short token)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return AutoResetAppaTaskCompletionSource.CreateFromCanceled(cancellationToken, out token);
                }

                if (!pool.TryPop(out var result))
                {
                    result = new AsyncOperationHandleConfiguredSource();
                }

                result.handle = handle;
                result.progress = progress;
                result.cancellationToken = cancellationToken;
                result.completed = false;

                TaskTracker.TrackActiveTask(result, 3);

                PlayerLoopHelper.AddAction(timing, result);

                handle.Completed += result.continuationAction;

                token = result.core.Version;
                return result;
            }

            private void Continuation(AsyncOperationHandle _)
            {
                handle.Completed -= continuationAction;

                if (completed)
                {
                    TryReturn();
                }
                else
                {
                    completed = true;
                    if (cancellationToken.IsCancellationRequested)
                    {
                        core.TrySetCanceled(cancellationToken);
                    }
                    else if (handle.Status == AsyncOperationStatus.Failed)
                    {
                        core.TrySetException(handle.OperationException);
                    }
                    else
                    {
                        core.TrySetResult(AsyncUnit.Default);
                    }
                }
            }

            public void GetResult(short token)
            {
                core.GetResult(token);
            }

            public AppaTaskStatus GetStatus(short token)
            {
                return core.GetStatus(token);
            }

            public AppaTaskStatus UnsafeGetStatus()
            {
                return core.UnsafeGetStatus();
            }

            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                core.OnCompleted(continuation, state, token);
            }

            public bool MoveNext()
            {
                if (completed)
                {
                    TryReturn();
                    return false;
                }

                if (cancellationToken.IsCancellationRequested)
                {
                    completed = true;
                    core.TrySetCanceled(cancellationToken);
                    return false;
                }

                if ((progress != null) && handle.IsValid())
                {
                    progress.Report(handle.PercentComplete);
                }

                return true;
            }

            private bool TryReturn()
            {
                TaskTracker.RemoveTracking(this);
                core.Reset();
                handle = default;
                progress = default;
                cancellationToken = default;
                return pool.TryPush(this);
            }
        }

        #endregion

        #region AsyncOperationHandle_T

        public static AppaTask<T>.Awaiter GetAwaiter<T>(this AsyncOperationHandle<T> handle)
        {
            return ToAppaTask(handle).GetAwaiter();
        }

        public static AppaTask<T> WithCancellation<T>(
            this AsyncOperationHandle<T> handle,
            CancellationToken cancellationToken)
        {
            return ToAppaTask(handle, cancellationToken: cancellationToken);
        }

        public static AppaTask<T> ToAppaTask<T>(
            this AsyncOperationHandle<T> handle,
            IProgress<float> progress = null,
            PlayerLoopTiming timing = PlayerLoopTiming.Update,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return AppaTask.FromCanceled<T>(cancellationToken);
            }

            if (!handle.IsValid())
            {
                throw new Exception("Attempting to use an invalid operation handle");
            }

            if (handle.IsDone)
            {
                if (handle.Status == AsyncOperationStatus.Failed)
                {
                    return AppaTask.FromException<T>(handle.OperationException);
                }

                return AppaTask.FromResult(handle.Result);
            }

            return new AppaTask<T>(
                AsyncOperationHandleConfiguredSource<T>.Create(
                    handle,
                    timing,
                    progress,
                    cancellationToken,
                    out var token
                ),
                token
            );
        }

        private sealed class AsyncOperationHandleConfiguredSource<T> : IAppaTaskSource<T>,
                                                                       IPlayerLoopItem,
                                                                       ITaskPoolNode<
                                                                           AsyncOperationHandleConfiguredSource
                                                                           <T>>
        {
            private static TaskPool<AsyncOperationHandleConfiguredSource<T>> pool;
            private AsyncOperationHandleConfiguredSource<T> nextNode;
            public ref AsyncOperationHandleConfiguredSource<T> NextNode => ref nextNode;

            static AsyncOperationHandleConfiguredSource()
            {
                TaskPool.RegisterSizeGetter(typeof(AsyncOperationHandleConfiguredSource<T>), () => pool.Size);
            }

            private readonly Action<AsyncOperationHandle<T>> continuationAction;
            private AsyncOperationHandle<T> handle;
            private CancellationToken cancellationToken;
            private IProgress<float> progress;
            private bool completed;

            private AppaTaskCompletionSourceCore<T> core;

            private AsyncOperationHandleConfiguredSource()
            {
                continuationAction = Continuation;
            }

            public static IAppaTaskSource<T> Create(
                AsyncOperationHandle<T> handle,
                PlayerLoopTiming timing,
                IProgress<float> progress,
                CancellationToken cancellationToken,
                out short token)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return AutoResetAppaTaskCompletionSource<T>.CreateFromCanceled(
                        cancellationToken,
                        out token
                    );
                }

                if (!pool.TryPop(out var result))
                {
                    result = new AsyncOperationHandleConfiguredSource<T>();
                }

                result.handle = handle;
                result.cancellationToken = cancellationToken;
                result.completed = false;
                result.progress = progress;

                TaskTracker.TrackActiveTask(result, 3);

                PlayerLoopHelper.AddAction(timing, result);

                handle.Completed += result.continuationAction;

                token = result.core.Version;
                return result;
            }

            private void Continuation(AsyncOperationHandle<T> argHandle)
            {
                handle.Completed -= continuationAction;

                if (completed)
                {
                    TryReturn();
                }
                else
                {
                    completed = true;
                    if (cancellationToken.IsCancellationRequested)
                    {
                        core.TrySetCanceled(cancellationToken);
                    }
                    else if (argHandle.Status == AsyncOperationStatus.Failed)
                    {
                        core.TrySetException(argHandle.OperationException);
                    }
                    else
                    {
                        core.TrySetResult(argHandle.Result);
                    }
                }
            }

            public T GetResult(short token)
            {
                return core.GetResult(token);
            }

            void IAppaTaskSource.GetResult(short token)
            {
                GetResult(token);
            }

            public AppaTaskStatus GetStatus(short token)
            {
                return core.GetStatus(token);
            }

            public AppaTaskStatus UnsafeGetStatus()
            {
                return core.UnsafeGetStatus();
            }

            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                core.OnCompleted(continuation, state, token);
            }

            public bool MoveNext()
            {
                if (completed)
                {
                    TryReturn();
                    return false;
                }

                if (cancellationToken.IsCancellationRequested)
                {
                    completed = true;
                    core.TrySetCanceled(cancellationToken);
                    return false;
                }

                if ((progress != null) && handle.IsValid())
                {
                    progress.Report(handle.PercentComplete);
                }

                return true;
            }

            private bool TryReturn()
            {
                TaskTracker.RemoveTracking(this);
                core.Reset();
                handle = default;
                progress = default;
                cancellationToken = default;
                return pool.TryPush(this);
            }
        }

        #endregion
    }
}

#endif
