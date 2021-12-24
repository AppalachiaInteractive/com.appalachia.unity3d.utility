#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Appalachia.Utility.Async.Internal;
using UnityEngine;
#if ENABLE_UNITYWEBREQUEST && (!UNITY_2019_1_OR_NEWER || APPATASK_WEBREQUEST_SUPPORT)
using UnityEngine.Networking;
#endif

namespace Appalachia.Utility.Async
{
    public static partial class UnityAsyncExtensions
    {
        #region AsyncOperation

        public static AsyncOperationAwaiter GetAwaiter(this AsyncOperation asyncOperation)
        {
            Error.ThrowArgumentNullException(asyncOperation, nameof(asyncOperation));
            return new AsyncOperationAwaiter(asyncOperation);
        }

        public static AppaTask WithCancellation(
            this AsyncOperation asyncOperation,
            CancellationToken cancellationToken)
        {
            return ToAppaTask(asyncOperation, cancellationToken: cancellationToken);
        }

        public static AppaTask ToAppaTask(
            this AsyncOperation asyncOperation,
            IProgress<float> progress = null,
            PlayerLoopTiming timing = PlayerLoopTiming.Update,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            Error.ThrowArgumentNullException(asyncOperation, nameof(asyncOperation));
            if (cancellationToken.IsCancellationRequested)
            {
                return AppaTask.FromCanceled(cancellationToken);
            }

            if (asyncOperation.isDone)
            {
                return AppaTask.CompletedTask;
            }

            return new AppaTask(
                AsyncOperationConfiguredSource.Create(
                    asyncOperation,
                    timing,
                    progress,
                    cancellationToken,
                    out var token
                ),
                token
            );
        }

        public struct AsyncOperationAwaiter : ICriticalNotifyCompletion
        {
            private AsyncOperation asyncOperation;
            private Action<AsyncOperation> continuationAction;

            public AsyncOperationAwaiter(AsyncOperation asyncOperation)
            {
                this.asyncOperation = asyncOperation;
                continuationAction = null;
            }

            public bool IsCompleted => asyncOperation.isDone;

            public void GetResult()
            {
                if (continuationAction != null)
                {
                    asyncOperation.completed -= continuationAction;
                    continuationAction = null;
                    asyncOperation = null;
                }
                else
                {
                    asyncOperation = null;
                }
            }

            public void OnCompleted(Action continuation)
            {
                UnsafeOnCompleted(continuation);
            }

            public void UnsafeOnCompleted(Action continuation)
            {
                Error.ThrowWhenContinuationIsAlreadyRegistered(continuationAction);
                continuationAction = PooledDelegate<AsyncOperation>.Create(continuation);
                asyncOperation.completed += continuationAction;
            }
        }

        private sealed class AsyncOperationConfiguredSource : IAppaTaskSource,
                                                              IPlayerLoopItem,
                                                              ITaskPoolNode<AsyncOperationConfiguredSource>
        {
            private static TaskPool<AsyncOperationConfiguredSource> pool;
            private AsyncOperationConfiguredSource nextNode;
            public ref AsyncOperationConfiguredSource NextNode => ref nextNode;

            static AsyncOperationConfiguredSource()
            {
                TaskPool.RegisterSizeGetter(typeof(AsyncOperationConfiguredSource), () => pool.Size);
            }

            private AsyncOperation asyncOperation;
            private IProgress<float> progress;
            private CancellationToken cancellationToken;

            private AppaTaskCompletionSourceCore<AsyncUnit> core;

            private AsyncOperationConfiguredSource()
            {
            }

            public static IAppaTaskSource Create(
                AsyncOperation asyncOperation,
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
                    result = new AsyncOperationConfiguredSource();
                }

                result.asyncOperation = asyncOperation;
                result.progress = progress;
                result.cancellationToken = cancellationToken;

                TaskTracker.TrackActiveTask(result, 3);

                PlayerLoopHelper.AddAction(timing, result);

                token = result.core.Version;
                return result;
            }

            public void GetResult(short token)
            {
                try
                {
                    core.GetResult(token);
                }
                finally
                {
                    TryReturn();
                }
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
                if (cancellationToken.IsCancellationRequested)
                {
                    core.TrySetCanceled(cancellationToken);
                    return false;
                }

                if (progress != null)
                {
                    progress.Report(asyncOperation.progress);
                }

                if (asyncOperation.isDone)
                {
                    core.TrySetResult(AsyncUnit.Default);
                    return false;
                }

                return true;
            }

            private bool TryReturn()
            {
                TaskTracker.RemoveTracking(this);
                core.Reset();
                asyncOperation = default;
                progress = default;
                cancellationToken = default;
                return pool.TryPush(this);
            }
        }

        #endregion

        #region ResourceRequest

        public static ResourceRequestAwaiter GetAwaiter(this ResourceRequest asyncOperation)
        {
            Error.ThrowArgumentNullException(asyncOperation, nameof(asyncOperation));
            return new ResourceRequestAwaiter(asyncOperation);
        }

        public static AppaTask<UnityEngine.Object> WithCancellation(
            this ResourceRequest asyncOperation,
            CancellationToken cancellationToken)
        {
            return ToAppaTask(asyncOperation, cancellationToken: cancellationToken);
        }

        public static AppaTask<UnityEngine.Object> ToAppaTask(
            this ResourceRequest asyncOperation,
            IProgress<float> progress = null,
            PlayerLoopTiming timing = PlayerLoopTiming.Update,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            Error.ThrowArgumentNullException(asyncOperation, nameof(asyncOperation));
            if (cancellationToken.IsCancellationRequested)
            {
                return AppaTask.FromCanceled<UnityEngine.Object>(cancellationToken);
            }

            if (asyncOperation.isDone)
            {
                return AppaTask.FromResult(asyncOperation.asset);
            }

            return new AppaTask<UnityEngine.Object>(
                ResourceRequestConfiguredSource.Create(
                    asyncOperation,
                    timing,
                    progress,
                    cancellationToken,
                    out var token
                ),
                token
            );
        }

        public struct ResourceRequestAwaiter : ICriticalNotifyCompletion
        {
            private ResourceRequest asyncOperation;
            private Action<AsyncOperation> continuationAction;

            public ResourceRequestAwaiter(ResourceRequest asyncOperation)
            {
                this.asyncOperation = asyncOperation;
                continuationAction = null;
            }

            public bool IsCompleted => asyncOperation.isDone;

            public UnityEngine.Object GetResult()
            {
                if (continuationAction != null)
                {
                    asyncOperation.completed -= continuationAction;
                    continuationAction = null;
                    var result = asyncOperation.asset;
                    asyncOperation = null;
                    return result;
                }
                else
                {
                    var result = asyncOperation.asset;
                    asyncOperation = null;
                    return result;
                }
            }

            public void OnCompleted(Action continuation)
            {
                UnsafeOnCompleted(continuation);
            }

            public void UnsafeOnCompleted(Action continuation)
            {
                Error.ThrowWhenContinuationIsAlreadyRegistered(continuationAction);
                continuationAction = PooledDelegate<AsyncOperation>.Create(continuation);
                asyncOperation.completed += continuationAction;
            }
        }

        private sealed class ResourceRequestConfiguredSource : IAppaTaskSource<UnityEngine.Object>,
                                                               IPlayerLoopItem,
                                                               ITaskPoolNode<ResourceRequestConfiguredSource>
        {
            private static TaskPool<ResourceRequestConfiguredSource> pool;
            private ResourceRequestConfiguredSource nextNode;
            public ref ResourceRequestConfiguredSource NextNode => ref nextNode;

            static ResourceRequestConfiguredSource()
            {
                TaskPool.RegisterSizeGetter(typeof(ResourceRequestConfiguredSource), () => pool.Size);
            }

            private ResourceRequest asyncOperation;
            private IProgress<float> progress;
            private CancellationToken cancellationToken;

            private AppaTaskCompletionSourceCore<UnityEngine.Object> core;

            private ResourceRequestConfiguredSource()
            {
            }

            public static IAppaTaskSource<UnityEngine.Object> Create(
                ResourceRequest asyncOperation,
                PlayerLoopTiming timing,
                IProgress<float> progress,
                CancellationToken cancellationToken,
                out short token)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return AutoResetAppaTaskCompletionSource<UnityEngine.Object>.CreateFromCanceled(
                        cancellationToken,
                        out token
                    );
                }

                if (!pool.TryPop(out var result))
                {
                    result = new ResourceRequestConfiguredSource();
                }

                result.asyncOperation = asyncOperation;
                result.progress = progress;
                result.cancellationToken = cancellationToken;

                TaskTracker.TrackActiveTask(result, 3);

                PlayerLoopHelper.AddAction(timing, result);

                token = result.core.Version;
                return result;
            }

            public UnityEngine.Object GetResult(short token)
            {
                try
                {
                    return core.GetResult(token);
                }
                finally
                {
                    TryReturn();
                }
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
                if (cancellationToken.IsCancellationRequested)
                {
                    core.TrySetCanceled(cancellationToken);
                    return false;
                }

                if (progress != null)
                {
                    progress.Report(asyncOperation.progress);
                }

                if (asyncOperation.isDone)
                {
                    core.TrySetResult(asyncOperation.asset);
                    return false;
                }

                return true;
            }

            private bool TryReturn()
            {
                TaskTracker.RemoveTracking(this);
                core.Reset();
                asyncOperation = default;
                progress = default;
                cancellationToken = default;
                return pool.TryPush(this);
            }
        }

        #endregion

#if APPATASK_ASSETBUNDLE_SUPPORT

        #region AssetBundleRequest

        public static AssetBundleRequestAwaiter GetAwaiter(this AssetBundleRequest asyncOperation)
        {
            Error.ThrowArgumentNullException(asyncOperation, nameof(asyncOperation));
            return new AssetBundleRequestAwaiter(asyncOperation);
        }

        public static AppaTask<UnityEngine.Object> WithCancellation(
            this AssetBundleRequest asyncOperation,
            CancellationToken cancellationToken)
        {
            return ToAppaTask(asyncOperation, cancellationToken: cancellationToken);
        }

        public static AppaTask<UnityEngine.Object> ToAppaTask(
            this AssetBundleRequest asyncOperation,
            IProgress<float> progress = null,
            PlayerLoopTiming timing = PlayerLoopTiming.Update,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            Error.ThrowArgumentNullException(asyncOperation, nameof(asyncOperation));
            if (cancellationToken.IsCancellationRequested)
            {
                return AppaTask.FromCanceled<UnityEngine.Object>(cancellationToken);
            }

            if (asyncOperation.isDone)
            {
                return AppaTask.FromResult(asyncOperation.asset);
            }

            return new AppaTask<UnityEngine.Object>(
                AssetBundleRequestConfiguredSource.Create(
                    asyncOperation,
                    timing,
                    progress,
                    cancellationToken,
                    out var token
                ),
                token
            );
        }

        public struct AssetBundleRequestAwaiter : ICriticalNotifyCompletion
        {
            private AssetBundleRequest asyncOperation;
            private Action<AsyncOperation> continuationAction;

            public AssetBundleRequestAwaiter(AssetBundleRequest asyncOperation)
            {
                this.asyncOperation = asyncOperation;
                continuationAction = null;
            }

            public bool IsCompleted => asyncOperation.isDone;

            public UnityEngine.Object GetResult()
            {
                if (continuationAction != null)
                {
                    asyncOperation.completed -= continuationAction;
                    continuationAction = null;
                    var result = asyncOperation.asset;
                    asyncOperation = null;
                    return result;
                }
                else
                {
                    var result = asyncOperation.asset;
                    asyncOperation = null;
                    return result;
                }
            }

            public void OnCompleted(Action continuation)
            {
                UnsafeOnCompleted(continuation);
            }

            public void UnsafeOnCompleted(Action continuation)
            {
                Error.ThrowWhenContinuationIsAlreadyRegistered(continuationAction);
                continuationAction = PooledDelegate<AsyncOperation>.Create(continuation);
                asyncOperation.completed += continuationAction;
            }
        }

        private sealed class AssetBundleRequestConfiguredSource : IAppaTaskSource<UnityEngine.Object>,
                                                                  IPlayerLoopItem,
                                                                  ITaskPoolNode<
                                                                      AssetBundleRequestConfiguredSource>
        {
            private static TaskPool<AssetBundleRequestConfiguredSource> pool;
            private AssetBundleRequestConfiguredSource nextNode;
            public ref AssetBundleRequestConfiguredSource NextNode => ref nextNode;

            static AssetBundleRequestConfiguredSource()
            {
                TaskPool.RegisterSizeGetter(typeof(AssetBundleRequestConfiguredSource), () => pool.Size);
            }

            private AssetBundleRequest asyncOperation;
            private IProgress<float> progress;
            private CancellationToken cancellationToken;

            private AppaTaskCompletionSourceCore<UnityEngine.Object> core;

            private AssetBundleRequestConfiguredSource()
            {
            }

            public static IAppaTaskSource<UnityEngine.Object> Create(
                AssetBundleRequest asyncOperation,
                PlayerLoopTiming timing,
                IProgress<float> progress,
                CancellationToken cancellationToken,
                out short token)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return AutoResetAppaTaskCompletionSource<UnityEngine.Object>.CreateFromCanceled(
                        cancellationToken,
                        out token
                    );
                }

                if (!pool.TryPop(out var result))
                {
                    result = new AssetBundleRequestConfiguredSource();
                }

                result.asyncOperation = asyncOperation;
                result.progress = progress;
                result.cancellationToken = cancellationToken;

                TaskTracker.TrackActiveTask(result, 3);

                PlayerLoopHelper.AddAction(timing, result);

                token = result.core.Version;
                return result;
            }

            public UnityEngine.Object GetResult(short token)
            {
                try
                {
                    return core.GetResult(token);
                }
                finally
                {
                    TryReturn();
                }
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
                if (cancellationToken.IsCancellationRequested)
                {
                    core.TrySetCanceled(cancellationToken);
                    return false;
                }

                if (progress != null)
                {
                    progress.Report(asyncOperation.progress);
                }

                if (asyncOperation.isDone)
                {
                    core.TrySetResult(asyncOperation.asset);
                    return false;
                }

                return true;
            }

            private bool TryReturn()
            {
                TaskTracker.RemoveTracking(this);
                core.Reset();
                asyncOperation = default;
                progress = default;
                cancellationToken = default;
                return pool.TryPush(this);
            }
        }

        #endregion

#endif

#if APPATASK_ASSETBUNDLE_SUPPORT

        #region AssetBundleCreateRequest

        public static AssetBundleCreateRequestAwaiter GetAwaiter(this AssetBundleCreateRequest asyncOperation)
        {
            Error.ThrowArgumentNullException(asyncOperation, nameof(asyncOperation));
            return new AssetBundleCreateRequestAwaiter(asyncOperation);
        }

        public static AppaTask<AssetBundle> WithCancellation(
            this AssetBundleCreateRequest asyncOperation,
            CancellationToken cancellationToken)
        {
            return ToAppaTask(asyncOperation, cancellationToken: cancellationToken);
        }

        public static AppaTask<AssetBundle> ToAppaTask(
            this AssetBundleCreateRequest asyncOperation,
            IProgress<float> progress = null,
            PlayerLoopTiming timing = PlayerLoopTiming.Update,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            Error.ThrowArgumentNullException(asyncOperation, nameof(asyncOperation));
            if (cancellationToken.IsCancellationRequested)
            {
                return AppaTask.FromCanceled<AssetBundle>(cancellationToken);
            }

            if (asyncOperation.isDone)
            {
                return AppaTask.FromResult(asyncOperation.assetBundle);
            }

            return new AppaTask<AssetBundle>(
                AssetBundleCreateRequestConfiguredSource.Create(
                    asyncOperation,
                    timing,
                    progress,
                    cancellationToken,
                    out var token
                ),
                token
            );
        }

        public struct AssetBundleCreateRequestAwaiter : ICriticalNotifyCompletion
        {
            private AssetBundleCreateRequest asyncOperation;
            private Action<AsyncOperation> continuationAction;

            public AssetBundleCreateRequestAwaiter(AssetBundleCreateRequest asyncOperation)
            {
                this.asyncOperation = asyncOperation;
                continuationAction = null;
            }

            public bool IsCompleted => asyncOperation.isDone;

            public AssetBundle GetResult()
            {
                if (continuationAction != null)
                {
                    asyncOperation.completed -= continuationAction;
                    continuationAction = null;
                    var result = asyncOperation.assetBundle;
                    asyncOperation = null;
                    return result;
                }
                else
                {
                    var result = asyncOperation.assetBundle;
                    asyncOperation = null;
                    return result;
                }
            }

            public void OnCompleted(Action continuation)
            {
                UnsafeOnCompleted(continuation);
            }

            public void UnsafeOnCompleted(Action continuation)
            {
                Error.ThrowWhenContinuationIsAlreadyRegistered(continuationAction);
                continuationAction = PooledDelegate<AsyncOperation>.Create(continuation);
                asyncOperation.completed += continuationAction;
            }
        }

        private sealed class AssetBundleCreateRequestConfiguredSource : IAppaTaskSource<AssetBundle>,
                                                                        IPlayerLoopItem,
                                                                        ITaskPoolNode<
                                                                            AssetBundleCreateRequestConfiguredSource>
        {
            private static TaskPool<AssetBundleCreateRequestConfiguredSource> pool;
            private AssetBundleCreateRequestConfiguredSource nextNode;
            public ref AssetBundleCreateRequestConfiguredSource NextNode => ref nextNode;

            static AssetBundleCreateRequestConfiguredSource()
            {
                TaskPool.RegisterSizeGetter(
                    typeof(AssetBundleCreateRequestConfiguredSource),
                    () => pool.Size
                );
            }

            private AssetBundleCreateRequest asyncOperation;
            private IProgress<float> progress;
            private CancellationToken cancellationToken;

            private AppaTaskCompletionSourceCore<AssetBundle> core;

            private AssetBundleCreateRequestConfiguredSource()
            {
            }

            public static IAppaTaskSource<AssetBundle> Create(
                AssetBundleCreateRequest asyncOperation,
                PlayerLoopTiming timing,
                IProgress<float> progress,
                CancellationToken cancellationToken,
                out short token)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return AutoResetAppaTaskCompletionSource<AssetBundle>.CreateFromCanceled(
                        cancellationToken,
                        out token
                    );
                }

                if (!pool.TryPop(out var result))
                {
                    result = new AssetBundleCreateRequestConfiguredSource();
                }

                result.asyncOperation = asyncOperation;
                result.progress = progress;
                result.cancellationToken = cancellationToken;

                TaskTracker.TrackActiveTask(result, 3);

                PlayerLoopHelper.AddAction(timing, result);

                token = result.core.Version;
                return result;
            }

            public AssetBundle GetResult(short token)
            {
                try
                {
                    return core.GetResult(token);
                }
                finally
                {
                    TryReturn();
                }
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
                if (cancellationToken.IsCancellationRequested)
                {
                    core.TrySetCanceled(cancellationToken);
                    return false;
                }

                if (progress != null)
                {
                    progress.Report(asyncOperation.progress);
                }

                if (asyncOperation.isDone)
                {
                    core.TrySetResult(asyncOperation.assetBundle);
                    return false;
                }

                return true;
            }

            private bool TryReturn()
            {
                TaskTracker.RemoveTracking(this);
                core.Reset();
                asyncOperation = default;
                progress = default;
                cancellationToken = default;
                return pool.TryPush(this);
            }
        }

        #endregion

#endif

#if ENABLE_UNITYWEBREQUEST && (!UNITY_2019_1_OR_NEWER || APPATASK_WEBREQUEST_SUPPORT)

        #region UnityWebRequestAsyncOperation

        public static UnityWebRequestAsyncOperationAwaiter GetAwaiter(
            this UnityWebRequestAsyncOperation asyncOperation)
        {
            Error.ThrowArgumentNullException(asyncOperation, nameof(asyncOperation));
            return new UnityWebRequestAsyncOperationAwaiter(asyncOperation);
        }

        public static AppaTask<UnityWebRequest> WithCancellation(
            this UnityWebRequestAsyncOperation asyncOperation,
            CancellationToken cancellationToken)
        {
            return ToAppaTask(asyncOperation, cancellationToken: cancellationToken);
        }

        public static AppaTask<UnityWebRequest> ToAppaTask(
            this UnityWebRequestAsyncOperation asyncOperation,
            IProgress<float> progress = null,
            PlayerLoopTiming timing = PlayerLoopTiming.Update,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            Error.ThrowArgumentNullException(asyncOperation, nameof(asyncOperation));
            if (cancellationToken.IsCancellationRequested)
            {
                return AppaTask.FromCanceled<UnityWebRequest>(cancellationToken);
            }

            if (asyncOperation.isDone)
            {
                if (asyncOperation.webRequest.IsError())
                {
                    return AppaTask.FromException<UnityWebRequest>(
                        new UnityWebRequestException(asyncOperation.webRequest)
                    );
                }

                return AppaTask.FromResult(asyncOperation.webRequest);
            }

            return new AppaTask<UnityWebRequest>(
                UnityWebRequestAsyncOperationConfiguredSource.Create(
                    asyncOperation,
                    timing,
                    progress,
                    cancellationToken,
                    out var token
                ),
                token
            );
        }

        public struct UnityWebRequestAsyncOperationAwaiter : ICriticalNotifyCompletion
        {
            private UnityWebRequestAsyncOperation asyncOperation;
            private Action<AsyncOperation> continuationAction;

            public UnityWebRequestAsyncOperationAwaiter(UnityWebRequestAsyncOperation asyncOperation)
            {
                this.asyncOperation = asyncOperation;
                continuationAction = null;
            }

            public bool IsCompleted => asyncOperation.isDone;

            public UnityWebRequest GetResult()
            {
                if (continuationAction != null)
                {
                    asyncOperation.completed -= continuationAction;
                    continuationAction = null;
                    var result = asyncOperation.webRequest;
                    asyncOperation = null;
                    if (result.IsError())
                    {
                        throw new UnityWebRequestException(result);
                    }

                    return result;
                }
                else
                {
                    var result = asyncOperation.webRequest;
                    asyncOperation = null;
                    if (result.IsError())
                    {
                        throw new UnityWebRequestException(result);
                    }

                    return result;
                }
            }

            public void OnCompleted(Action continuation)
            {
                UnsafeOnCompleted(continuation);
            }

            public void UnsafeOnCompleted(Action continuation)
            {
                Error.ThrowWhenContinuationIsAlreadyRegistered(continuationAction);
                continuationAction = PooledDelegate<AsyncOperation>.Create(continuation);
                asyncOperation.completed += continuationAction;
            }
        }

        private sealed class UnityWebRequestAsyncOperationConfiguredSource : IAppaTaskSource<UnityWebRequest>,
            IPlayerLoopItem,
            ITaskPoolNode<UnityWebRequestAsyncOperationConfiguredSource>
        {
            private static TaskPool<UnityWebRequestAsyncOperationConfiguredSource> pool;
            private UnityWebRequestAsyncOperationConfiguredSource nextNode;
            public ref UnityWebRequestAsyncOperationConfiguredSource NextNode => ref nextNode;

            static UnityWebRequestAsyncOperationConfiguredSource()
            {
                TaskPool.RegisterSizeGetter(
                    typeof(UnityWebRequestAsyncOperationConfiguredSource),
                    () => pool.Size
                );
            }

            private UnityWebRequestAsyncOperation asyncOperation;
            private IProgress<float> progress;
            private CancellationToken cancellationToken;

            private AppaTaskCompletionSourceCore<UnityWebRequest> core;

            private UnityWebRequestAsyncOperationConfiguredSource()
            {
            }

            public static IAppaTaskSource<UnityWebRequest> Create(
                UnityWebRequestAsyncOperation asyncOperation,
                PlayerLoopTiming timing,
                IProgress<float> progress,
                CancellationToken cancellationToken,
                out short token)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return AutoResetAppaTaskCompletionSource<UnityWebRequest>.CreateFromCanceled(
                        cancellationToken,
                        out token
                    );
                }

                if (!pool.TryPop(out var result))
                {
                    result = new UnityWebRequestAsyncOperationConfiguredSource();
                }

                result.asyncOperation = asyncOperation;
                result.progress = progress;
                result.cancellationToken = cancellationToken;

                TaskTracker.TrackActiveTask(result, 3);

                PlayerLoopHelper.AddAction(timing, result);

                token = result.core.Version;
                return result;
            }

            public UnityWebRequest GetResult(short token)
            {
                try
                {
                    return core.GetResult(token);
                }
                finally
                {
                    TryReturn();
                }
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
                if (cancellationToken.IsCancellationRequested)
                {
                    asyncOperation.webRequest.Abort();
                    core.TrySetCanceled(cancellationToken);
                    return false;
                }

                if (progress != null)
                {
                    progress.Report(asyncOperation.progress);
                }

                if (asyncOperation.isDone)
                {
                    if (asyncOperation.webRequest.IsError())
                    {
                        core.TrySetException(new UnityWebRequestException(asyncOperation.webRequest));
                    }
                    else
                    {
                        core.TrySetResult(asyncOperation.webRequest);
                    }

                    return false;
                }

                return true;
            }

            private bool TryReturn()
            {
                TaskTracker.RemoveTracking(this);
                core.Reset();
                asyncOperation = default;
                progress = default;
                cancellationToken = default;
                return pool.TryPush(this);
            }
        }

        #endregion

#endif
    }
}
