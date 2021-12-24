#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

#if UNITY_2018_4 || UNITY_2019_4_OR_NEWER
#if APPATASK_ASSETBUNDLE_SUPPORT

using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Appalachia.Utility.Async.Internal;
using UnityEngine;

namespace Appalachia.Utility.Async
{
    public static partial class UnityAsyncExtensions
    {
        public static AssetBundleRequestAllAssetsAwaiter AwaitForAllAssets(
            this AssetBundleRequest asyncOperation)
        {
            Error.ThrowArgumentNullException(asyncOperation, nameof(asyncOperation));
            return new AssetBundleRequestAllAssetsAwaiter(asyncOperation);
        }

        public static AppaTask<UnityEngine.Object[]> AwaitForAllAssets(
            this AssetBundleRequest asyncOperation,
            CancellationToken cancellationToken)
        {
            return AwaitForAllAssets(asyncOperation, cancelToken: cancellationToken);
        }

        // ReSharper disable once MethodOverloadWithOptionalParameter
        public static AppaTask<UnityEngine.Object[]> AwaitForAllAssets(
            this AssetBundleRequest asyncOperation,
            IProgress<float> progress = null,
            PlayerLoopTiming timing = PlayerLoopTiming.Update,
            CancellationToken cancelToken = default(CancellationToken))
        {
            Error.ThrowArgumentNullException(asyncOperation, nameof(asyncOperation));
            if (cancelToken.IsCancellationRequested)
            {
                return AppaTask.FromCanceled<UnityEngine.Object[]>(cancelToken);
            }

            if (asyncOperation.isDone)
            {
                return AppaTask.FromResult(asyncOperation.allAssets);
            }

            return new AppaTask<UnityEngine.Object[]>(
                AssetBundleRequestAllAssetsConfiguredSource.Create(
                    asyncOperation,
                    timing,
                    progress,
                    cancelToken,
                    out var token
                ),
                token
            );
        }

        public struct AssetBundleRequestAllAssetsAwaiter : ICriticalNotifyCompletion
        {
            private AssetBundleRequest asyncOperation;
            private Action<AsyncOperation> continuationAction;

            public AssetBundleRequestAllAssetsAwaiter(AssetBundleRequest asyncOperation)
            {
                this.asyncOperation = asyncOperation;
                continuationAction = null;
            }

            public AssetBundleRequestAllAssetsAwaiter GetAwaiter()
            {
                return this;
            }

            public bool IsCompleted => asyncOperation.isDone;

            public UnityEngine.Object[] GetResult()
            {
                if (continuationAction != null)
                {
                    asyncOperation.completed -= continuationAction;
                    continuationAction = null;
                    var result = asyncOperation.allAssets;
                    asyncOperation = null;
                    return result;
                }
                else
                {
                    var result = asyncOperation.allAssets;
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

        private sealed class AssetBundleRequestAllAssetsConfiguredSource :
            IAppaTaskSource<UnityEngine.Object[]>,
            IPlayerLoopItem,
            ITaskPoolNode<AssetBundleRequestAllAssetsConfiguredSource>
        {
            private static TaskPool<AssetBundleRequestAllAssetsConfiguredSource> pool;
            private AssetBundleRequestAllAssetsConfiguredSource nextNode;
            public ref AssetBundleRequestAllAssetsConfiguredSource NextNode => ref nextNode;

            static AssetBundleRequestAllAssetsConfiguredSource()
            {
                TaskPool.RegisterSizeGetter(
                    typeof(AssetBundleRequestAllAssetsConfiguredSource),
                    () => pool.Size
                );
            }

            private AssetBundleRequest asyncOperation;
            private IProgress<float> progress;
            private CancellationToken cancellationToken;

            private AppaTaskCompletionSourceCore<UnityEngine.Object[]> core;

            private AssetBundleRequestAllAssetsConfiguredSource()
            {
            }

            public static IAppaTaskSource<UnityEngine.Object[]> Create(
                AssetBundleRequest asyncOperation,
                PlayerLoopTiming timing,
                IProgress<float> progress,
                CancellationToken cancellationToken,
                out short token)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return AutoResetAppaTaskCompletionSource<UnityEngine.Object[]>.CreateFromCanceled(
                        cancellationToken,
                        out token
                    );
                }

                if (!pool.TryPop(out var result))
                {
                    result = new AssetBundleRequestAllAssetsConfiguredSource();
                }

                result.asyncOperation = asyncOperation;
                result.progress = progress;
                result.cancellationToken = cancellationToken;

                TaskTracker.TrackActiveTask(result, 3);

                PlayerLoopHelper.AddAction(timing, result);

                token = result.core.Version;
                return result;
            }

            public UnityEngine.Object[] GetResult(short token)
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
                    core.TrySetResult(asyncOperation.allAssets);
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
    }
}

#endif
#endif
