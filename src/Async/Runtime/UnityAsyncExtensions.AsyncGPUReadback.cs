#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Threading;
using UnityEngine.Rendering;

namespace Appalachia.Utility.Async
{
    public static partial class UnityAsyncExtensions
    {
        #region AsyncGPUReadbackRequest

        public static AppaTask<AsyncGPUReadbackRequest>.Awaiter GetAwaiter(
            this AsyncGPUReadbackRequest asyncOperation)
        {
            return ToAppaTask(asyncOperation).GetAwaiter();
        }

        public static AppaTask<AsyncGPUReadbackRequest> WithCancellation(
            this AsyncGPUReadbackRequest asyncOperation,
            CancellationToken cancellationToken)
        {
            return ToAppaTask(asyncOperation, cancellationToken: cancellationToken);
        }

        public static AppaTask<AsyncGPUReadbackRequest> ToAppaTask(
            this AsyncGPUReadbackRequest asyncOperation,
            PlayerLoopTiming timing = PlayerLoopTiming.Update,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (asyncOperation.done)
            {
                return AppaTask.FromResult(asyncOperation);
            }

            return new AppaTask<AsyncGPUReadbackRequest>(
                AsyncGPUReadbackRequestAwaiterConfiguredSource.Create(
                    asyncOperation,
                    timing,
                    cancellationToken,
                    out var token
                ),
                token
            );
        }

        private sealed class AsyncGPUReadbackRequestAwaiterConfiguredSource :
            IAppaTaskSource<AsyncGPUReadbackRequest>,
            IPlayerLoopItem,
            ITaskPoolNode<AsyncGPUReadbackRequestAwaiterConfiguredSource>
        {
            private static TaskPool<AsyncGPUReadbackRequestAwaiterConfiguredSource> pool;
            private AsyncGPUReadbackRequestAwaiterConfiguredSource nextNode;
            public ref AsyncGPUReadbackRequestAwaiterConfiguredSource NextNode => ref nextNode;

            static AsyncGPUReadbackRequestAwaiterConfiguredSource()
            {
                TaskPool.RegisterSizeGetter(
                    typeof(AsyncGPUReadbackRequestAwaiterConfiguredSource),
                    () => pool.Size
                );
            }

            private AsyncGPUReadbackRequest asyncOperation;
            private CancellationToken cancellationToken;

            private AppaTaskCompletionSourceCore<AsyncGPUReadbackRequest> core;

            private AsyncGPUReadbackRequestAwaiterConfiguredSource()
            {
            }

            public static IAppaTaskSource<AsyncGPUReadbackRequest> Create(
                AsyncGPUReadbackRequest asyncOperation,
                PlayerLoopTiming timing,
                CancellationToken cancellationToken,
                out short token)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return AutoResetAppaTaskCompletionSource<AsyncGPUReadbackRequest>.CreateFromCanceled(
                        cancellationToken,
                        out token
                    );
                }

                if (!pool.TryPop(out var result))
                {
                    result = new AsyncGPUReadbackRequestAwaiterConfiguredSource();
                }

                result.asyncOperation = asyncOperation;
                result.cancellationToken = cancellationToken;

                TaskTracker.TrackActiveTask(result, 3);

                PlayerLoopHelper.AddAction(timing, result);

                token = result.core.Version;
                return result;
            }

            public AsyncGPUReadbackRequest GetResult(short token)
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

                if (asyncOperation.hasError)
                {
                    core.TrySetException(new Exception("AsyncGPUReadbackRequest.hasError = true"));
                    return false;
                }

                if (asyncOperation.done)
                {
                    core.TrySetResult(asyncOperation);
                    return false;
                }

                return true;
            }

            private bool TryReturn()
            {
                TaskTracker.RemoveTracking(this);
                core.Reset();
                asyncOperation = default;
                cancellationToken = default;
                return pool.TryPush(this);
            }
        }

        #endregion
    }
}