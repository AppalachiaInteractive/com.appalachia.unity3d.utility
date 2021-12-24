// asmdef Version Defines, enabled when com.demigiant.dotween is imported.
//#if APPATASK_DOTWEEN_SUPPORT 

using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Threading;
using Appalachia.Utility.Async.Internal;
using DG.Tweening;

namespace Appalachia.Utility.Async.External.DOTween
{
    public enum TweenCancelBehaviour
    {
        Kill,
        KillWithCompleteCallback,
        Complete,
        CompleteWithSeqeunceCallback,
        CancelAwait,

        // AndCancelAwait
        KillAndCancelAwait,
        KillWithCompleteCallbackAndCancelAwait,
        CompleteAndCancelAwait,
        CompleteWithSeqeunceCallbackAndCancelAwait
    }

    public static class DOTweenAsyncExtensions
    {
        private enum CallbackType
        {
            Kill,
            Complete,
            Pause,
            Play,
            Rewind,
            StepComplete
        }

        public static TweenAwaiter GetAwaiter(this Tween tween)
        {
            return new TweenAwaiter(tween);
        }

        public static AppaTask WithCancellation(this Tween tween, CancellationToken cancellationToken)
        {
            Error.ThrowArgumentNullException(tween, nameof(tween));

            if (!tween.IsActive())
            {
                return AppaTask.CompletedTask;
            }

            return new AppaTask(
                TweenConfiguredSource.Create(
                    tween,
                    TweenCancelBehaviour.Kill,
                    cancellationToken,
                    CallbackType.Kill,
                    out var token
                ),
                token
            );
        }

        public static AppaTask ToAppaTask(
            this Tween tween,
            TweenCancelBehaviour tweenCancelBehaviour = TweenCancelBehaviour.Kill,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(tween, nameof(tween));

            if (!tween.IsActive())
            {
                return AppaTask.CompletedTask;
            }

            return new AppaTask(
                TweenConfiguredSource.Create(
                    tween,
                    tweenCancelBehaviour,
                    cancellationToken,
                    CallbackType.Kill,
                    out var token
                ),
                token
            );
        }

        public static AppaTask AwaitForComplete(
            this Tween tween,
            TweenCancelBehaviour tweenCancelBehaviour = TweenCancelBehaviour.Kill,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(tween, nameof(tween));

            if (!tween.IsActive())
            {
                return AppaTask.CompletedTask;
            }

            return new AppaTask(
                TweenConfiguredSource.Create(
                    tween,
                    tweenCancelBehaviour,
                    cancellationToken,
                    CallbackType.Complete,
                    out var token
                ),
                token
            );
        }

        public static AppaTask AwaitForPause(
            this Tween tween,
            TweenCancelBehaviour tweenCancelBehaviour = TweenCancelBehaviour.Kill,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(tween, nameof(tween));

            if (!tween.IsActive())
            {
                return AppaTask.CompletedTask;
            }

            return new AppaTask(
                TweenConfiguredSource.Create(
                    tween,
                    tweenCancelBehaviour,
                    cancellationToken,
                    CallbackType.Pause,
                    out var token
                ),
                token
            );
        }

        public static AppaTask AwaitForPlay(
            this Tween tween,
            TweenCancelBehaviour tweenCancelBehaviour = TweenCancelBehaviour.Kill,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(tween, nameof(tween));

            if (!tween.IsActive())
            {
                return AppaTask.CompletedTask;
            }

            return new AppaTask(
                TweenConfiguredSource.Create(
                    tween,
                    tweenCancelBehaviour,
                    cancellationToken,
                    CallbackType.Play,
                    out var token
                ),
                token
            );
        }

        public static AppaTask AwaitForRewind(
            this Tween tween,
            TweenCancelBehaviour tweenCancelBehaviour = TweenCancelBehaviour.Kill,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(tween, nameof(tween));

            if (!tween.IsActive())
            {
                return AppaTask.CompletedTask;
            }

            return new AppaTask(
                TweenConfiguredSource.Create(
                    tween,
                    tweenCancelBehaviour,
                    cancellationToken,
                    CallbackType.Rewind,
                    out var token
                ),
                token
            );
        }

        public static AppaTask AwaitForStepComplete(
            this Tween tween,
            TweenCancelBehaviour tweenCancelBehaviour = TweenCancelBehaviour.Kill,
            CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(tween, nameof(tween));

            if (!tween.IsActive())
            {
                return AppaTask.CompletedTask;
            }

            return new AppaTask(
                TweenConfiguredSource.Create(
                    tween,
                    tweenCancelBehaviour,
                    cancellationToken,
                    CallbackType.StepComplete,
                    out var token
                ),
                token
            );
        }

        public struct TweenAwaiter : ICriticalNotifyCompletion
        {
            private readonly Tween tween;

            // killed(non active) as completed.
            public bool IsCompleted => !tween.IsActive();

            public TweenAwaiter(Tween tween)
            {
                this.tween = tween;
            }

            public TweenAwaiter GetAwaiter()
            {
                return this;
            }

            public void GetResult()
            {
            }

            public void OnCompleted(Action continuation)
            {
                UnsafeOnCompleted(continuation);
            }

            public void UnsafeOnCompleted(Action continuation)
            {
                // onKill is called after OnCompleted, both Complete(false/true) and Kill(false/true).
                tween.onKill = PooledTweenCallback.Create(continuation);
            }
        }

        private sealed class TweenConfiguredSource : IAppaTaskSource, ITaskPoolNode<TweenConfiguredSource>
        {
            private static TaskPool<TweenConfiguredSource> pool;
            private TweenConfiguredSource nextNode;
            public ref TweenConfiguredSource NextNode => ref nextNode;

            static TweenConfiguredSource()
            {
                TaskPool.RegisterSizeGetter(typeof(TweenConfiguredSource), () => pool.Size);
            }

            private static readonly TweenCallback EmptyTweenCallback = () => { };

            private readonly TweenCallback onCompleteCallbackDelegate;
            private readonly TweenCallback onUpdateDelegate;

            private Tween tween;
            private TweenCancelBehaviour cancelBehaviour;
            private CancellationToken cancellationToken;
            private CallbackType callbackType;
            private bool canceled;

            private TweenCallback originalUpdateAction;
            private AppaTaskCompletionSourceCore<AsyncUnit> core;

            private TweenConfiguredSource()
            {
                onCompleteCallbackDelegate = OnCompleteCallbackDelegate;
                onUpdateDelegate = OnUpdate;
            }

            public static IAppaTaskSource Create(
                Tween tween,
                TweenCancelBehaviour cancelBehaviour,
                CancellationToken cancellationToken,
                CallbackType callbackType,
                out short token)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    DoCancelBeforeCreate(tween, cancelBehaviour);
                    return AutoResetAppaTaskCompletionSource.CreateFromCanceled(cancellationToken, out token);
                }

                if (!pool.TryPop(out var result))
                {
                    result = new TweenConfiguredSource();
                }

                result.tween = tween;
                result.cancelBehaviour = cancelBehaviour;
                result.cancellationToken = cancellationToken;
                result.callbackType = callbackType;

                result.originalUpdateAction = tween.onUpdate;
                result.canceled = false;

                if (result.originalUpdateAction == result.onUpdateDelegate)
                {
                    result.originalUpdateAction = null;
                }

                tween.onUpdate = result.onUpdateDelegate;

                switch (callbackType)
                {
                    case CallbackType.Kill:
                        tween.onKill = result.onCompleteCallbackDelegate;
                        break;
                    case CallbackType.Complete:
                        tween.onComplete = result.onCompleteCallbackDelegate;
                        break;
                    case CallbackType.Pause:
                        tween.onPause = result.onCompleteCallbackDelegate;
                        break;
                    case CallbackType.Play:
                        tween.onPlay = result.onCompleteCallbackDelegate;
                        break;
                    case CallbackType.Rewind:
                        tween.onRewind = result.onCompleteCallbackDelegate;
                        break;
                    case CallbackType.StepComplete:
                        tween.onStepComplete = result.onCompleteCallbackDelegate;
                        break;
                }

                TaskTracker.TrackActiveTask(result, 3);

                token = result.core.Version;
                return result;
            }

            private void OnCompleteCallbackDelegate()
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    if ((cancelBehaviour == TweenCancelBehaviour.KillAndCancelAwait) ||
                        (cancelBehaviour == TweenCancelBehaviour.KillWithCompleteCallbackAndCancelAwait) ||
                        (cancelBehaviour == TweenCancelBehaviour.CompleteAndCancelAwait) ||
                        (cancelBehaviour ==
                         TweenCancelBehaviour.CompleteWithSeqeunceCallbackAndCancelAwait) ||
                        (cancelBehaviour == TweenCancelBehaviour.CancelAwait))
                    {
                        canceled = true;
                    }
                }

                if (canceled)
                {
                    core.TrySetCanceled(cancellationToken);
                }
                else
                {
                    core.TrySetResult(AsyncUnit.Default);
                }
            }

            private void OnUpdate()
            {
                originalUpdateAction?.Invoke();

                if (!cancellationToken.IsCancellationRequested)
                {
                    return;
                }

                switch (cancelBehaviour)
                {
                    case TweenCancelBehaviour.Kill:
                    default:
                        tween.Kill();
                        break;
                    case TweenCancelBehaviour.KillAndCancelAwait:
                        canceled = true;
                        tween.Kill();
                        break;
                    case TweenCancelBehaviour.KillWithCompleteCallback:
                        tween.Kill(true);
                        break;
                    case TweenCancelBehaviour.KillWithCompleteCallbackAndCancelAwait:
                        canceled = true;
                        tween.Kill(true);
                        break;
                    case TweenCancelBehaviour.Complete:
                        tween.Complete(false);
                        break;
                    case TweenCancelBehaviour.CompleteAndCancelAwait:
                        canceled = true;
                        tween.Complete(false);
                        break;
                    case TweenCancelBehaviour.CompleteWithSeqeunceCallback:
                        tween.Complete(true);
                        break;
                    case TweenCancelBehaviour.CompleteWithSeqeunceCallbackAndCancelAwait:
                        canceled = true;
                        tween.Complete(true);
                        break;
                    case TweenCancelBehaviour.CancelAwait:
                        // replace to empty(avoid callback after Canceled(instance is returned to pool.)
                        switch (callbackType)
                        {
                            case CallbackType.Kill:
                                tween.onKill = EmptyTweenCallback;
                                break;
                            case CallbackType.Complete:
                                tween.onComplete = EmptyTweenCallback;
                                break;
                            case CallbackType.Pause:
                                tween.onPause = EmptyTweenCallback;
                                break;
                            case CallbackType.Play:
                                tween.onPlay = EmptyTweenCallback;
                                break;
                            case CallbackType.Rewind:
                                tween.onRewind = EmptyTweenCallback;
                                break;
                            case CallbackType.StepComplete:
                                tween.onStepComplete = EmptyTweenCallback;
                                break;
                        }

                        core.TrySetCanceled(cancellationToken);
                        break;
                }
            }

            private static void DoCancelBeforeCreate(Tween tween, TweenCancelBehaviour tweenCancelBehaviour)
            {
                switch (tweenCancelBehaviour)
                {
                    case TweenCancelBehaviour.Kill:
                    default:
                        tween.Kill();
                        break;
                    case TweenCancelBehaviour.KillAndCancelAwait:
                        tween.Kill();
                        break;
                    case TweenCancelBehaviour.KillWithCompleteCallback:
                        tween.Kill(true);
                        break;
                    case TweenCancelBehaviour.KillWithCompleteCallbackAndCancelAwait:
                        tween.Kill(true);
                        break;
                    case TweenCancelBehaviour.Complete:
                        tween.Complete(false);
                        break;
                    case TweenCancelBehaviour.CompleteAndCancelAwait:
                        tween.Complete(false);
                        break;
                    case TweenCancelBehaviour.CompleteWithSeqeunceCallback:
                        tween.Complete(true);
                        break;
                    case TweenCancelBehaviour.CompleteWithSeqeunceCallbackAndCancelAwait:
                        tween.Complete(true);
                        break;
                    case TweenCancelBehaviour.CancelAwait:
                        break;
                }
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

            private bool TryReturn()
            {
                TaskTracker.RemoveTracking(this);
                core.Reset();
                tween.onUpdate = originalUpdateAction;

                switch (callbackType)
                {
                    case CallbackType.Kill:
                        tween.onKill = null;
                        break;
                    case CallbackType.Complete:
                        tween.onComplete = null;
                        break;
                    case CallbackType.Pause:
                        tween.onPause = null;
                        break;
                    case CallbackType.Play:
                        tween.onPlay = null;
                        break;
                    case CallbackType.Rewind:
                        tween.onRewind = null;
                        break;
                    case CallbackType.StepComplete:
                        tween.onStepComplete = null;
                        break;
                }

                tween = default;
                cancellationToken = default;
                originalUpdateAction = default;
                return pool.TryPush(this);
            }
        }
    }

    internal sealed class PooledTweenCallback
    {
        private static readonly ConcurrentQueue<PooledTweenCallback> pool =
            new ConcurrentQueue<PooledTweenCallback>();

        private readonly TweenCallback runDelegate;

        private Action continuation;

        private PooledTweenCallback()
        {
            runDelegate = Run;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TweenCallback Create(Action continuation)
        {
            if (!pool.TryDequeue(out var item))
            {
                item = new PooledTweenCallback();
            }

            item.continuation = continuation;
            return item.runDelegate;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Run()
        {
            var call = continuation;
            continuation = null;
            if (call != null)
            {
                pool.Enqueue(this);
                call.Invoke();
            }
        }
    }
}

// #endif
