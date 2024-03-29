﻿#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Appalachia.Utility.Async.Internal;
using Appalachia.Utility.Timing;

namespace Appalachia.Utility.Async
{
    public enum DelayType
    {
        /// <summary>use CoreClock.Instance.DeltaTime.</summary>
        DeltaTime,

        /// <summary>Ignore timescale, use CoreClock.Instance.UnscaledDeltaTime.</summary>
        UnscaledDeltaTime,

        /// <summary>use Stopwatch.GetTimestamp().</summary>
        Realtime
    }

    public partial struct AppaTask
    {
        public static YieldAwaitable Yield()
        {
            // optimized for single continuation
            return new YieldAwaitable(PlayerLoopTiming.Update);
        }

        public static YieldAwaitable Yield(PlayerLoopTiming timing)
        {
            // optimized for single continuation
            return new YieldAwaitable(timing);
        }

        public static AppaTask Yield(CancellationToken cancellationToken)
        {
            return new AppaTask(
                YieldPromise.Create(PlayerLoopTiming.Update, cancellationToken, out var token),
                token
            );
        }

        public static AppaTask Yield(PlayerLoopTiming timing, CancellationToken cancellationToken)
        {
            return new AppaTask(YieldPromise.Create(timing, cancellationToken, out var token), token);
        }

        /// <summary>
        ///     Similar as AppaTask.Yield but guaranteed run on next frame.
        /// </summary>
        public static AppaTask NextFrame()
        {
            return new AppaTask(
                NextFramePromise.Create(PlayerLoopTiming.Update, CancellationToken.None, out var token),
                token
            );
        }

        /// <summary>
        ///     Similar as AppaTask.Yield but guaranteed run on next frame.
        /// </summary>
        public static AppaTask NextFrame(PlayerLoopTiming timing)
        {
            return new AppaTask(
                NextFramePromise.Create(timing, CancellationToken.None, out var token),
                token
            );
        }

        /// <summary>
        ///     Similar as AppaTask.Yield but guaranteed run on next frame.
        /// </summary>
        public static AppaTask NextFrame(CancellationToken cancellationToken)
        {
            return new AppaTask(
                NextFramePromise.Create(PlayerLoopTiming.Update, cancellationToken, out var token),
                token
            );
        }

        /// <summary>
        ///     Similar as AppaTask.Yield but guaranteed run on next frame.
        /// </summary>
        public static AppaTask NextFrame(PlayerLoopTiming timing, CancellationToken cancellationToken)
        {
            return new AppaTask(NextFramePromise.Create(timing, cancellationToken, out var token), token);
        }

        /// <summary>
        ///     Same as AppaTask.Yield(PlayerLoopTiming.LastPostLateUpdate).
        /// </summary>
        public static YieldAwaitable WaitForEndOfFrame()
        {
            return Yield(PlayerLoopTiming.LastPostLateUpdate);
        }

        /// <summary>
        ///     Same as AppaTask.Yield(PlayerLoopTiming.LastPostLateUpdate, cancellationToken).
        /// </summary>
        public static AppaTask WaitForEndOfFrame(CancellationToken cancellationToken)
        {
            return Yield(PlayerLoopTiming.LastPostLateUpdate, cancellationToken);
        }

        /// <summary>
        ///     Same as AppaTask.Yield(PlayerLoopTiming.FixedUpdate).
        /// </summary>
        public static YieldAwaitable WaitForFixedUpdate()
        {
            return Yield(PlayerLoopTiming.FixedUpdate);
        }

        /// <summary>
        ///     Same as AppaTask.Yield(PlayerLoopTiming.FixedUpdate, cancellationToken).
        /// </summary>
        public static AppaTask WaitForFixedUpdate(CancellationToken cancellationToken)
        {
            return Yield(PlayerLoopTiming.FixedUpdate, cancellationToken);
        }

        public static AppaTask DelayFrame(
            int delayFrameCount,
            PlayerLoopTiming delayTiming = PlayerLoopTiming.Update,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (delayFrameCount < 0)
            {
                throw new ArgumentOutOfRangeException(
                    "Delay does not allow minus delayFrameCount. delayFrameCount:" + delayFrameCount
                );
            }

            return new AppaTask(
                DelayFramePromise.Create(delayFrameCount, delayTiming, cancellationToken, out var token),
                token
            );
        }

        public static AppaTask Delay(
            int millisecondsDelay,
            bool ignoreTimeScale = false,
            PlayerLoopTiming delayTiming = PlayerLoopTiming.Update,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var delayTimeSpan = TimeSpan.FromMilliseconds(millisecondsDelay);
            return Delay(delayTimeSpan, ignoreTimeScale, delayTiming, cancellationToken);
        }

        public static AppaTask Delay(
            TimeSpan delayTimeSpan,
            bool ignoreTimeScale = false,
            PlayerLoopTiming delayTiming = PlayerLoopTiming.Update,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var delayType = ignoreTimeScale ? DelayType.UnscaledDeltaTime : DelayType.DeltaTime;
            return Delay(delayTimeSpan, delayType, delayTiming, cancellationToken);
        }

        public static AppaTask Delay(
            int millisecondsDelay,
            DelayType delayType,
            PlayerLoopTiming delayTiming = PlayerLoopTiming.Update,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var delayTimeSpan = TimeSpan.FromMilliseconds(millisecondsDelay);
            return Delay(delayTimeSpan, delayType, delayTiming, cancellationToken);
        }

        public static AppaTask Delay(
            TimeSpan delayTimeSpan,
            DelayType delayType,
            PlayerLoopTiming delayTiming = PlayerLoopTiming.Update,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (delayTimeSpan < TimeSpan.Zero)
            {
                throw new ArgumentOutOfRangeException(
                    "Delay does not allow minus delayTimeSpan. delayTimeSpan:" + delayTimeSpan
                );
            }

#if UNITY_EDITOR

            // force use Realtime.
            if (PlayerLoopHelper.IsMainThread && !UnityEditor.EditorApplication.isPlaying)
            {
                delayType = DelayType.Realtime;
            }
#endif

            switch (delayType)
            {
                case DelayType.UnscaledDeltaTime:
                {
                    return new AppaTask(
                        DelayIgnoreTimeScalePromise.Create(
                            delayTimeSpan,
                            delayTiming,
                            cancellationToken,
                            out var token
                        ),
                        token
                    );
                }
                case DelayType.Realtime:
                {
                    return new AppaTask(
                        DelayRealtimePromise.Create(
                            delayTimeSpan,
                            delayTiming,
                            cancellationToken,
                            out var token
                        ),
                        token
                    );
                }
                case DelayType.DeltaTime:
                default:
                {
                    return new AppaTask(
                        DelayPromise.Create(delayTimeSpan, delayTiming, cancellationToken, out var token),
                        token
                    );
                }
            }
        }

        private sealed class YieldPromise : IAppaTaskSource, IPlayerLoopItem, ITaskPoolNode<YieldPromise>
        {
            private static TaskPool<YieldPromise> pool;
            private YieldPromise nextNode;
            public ref YieldPromise NextNode => ref nextNode;

            static YieldPromise()
            {
                TaskPool.RegisterSizeGetter(typeof(YieldPromise), () => pool.Size);
            }

            private CancellationToken cancellationToken;
            private AppaTaskCompletionSourceCore<object> core;

            private YieldPromise()
            {
            }

            public static IAppaTaskSource Create(
                PlayerLoopTiming timing,
                CancellationToken cancellationToken,
                out short token)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return AutoResetAppaTaskCompletionSource.CreateFromCanceled(cancellationToken, out token);
                }

                if (!pool.TryPop(out var result))
                {
                    result = new YieldPromise();
                }

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

                core.TrySetResult(null);
                return false;
            }

            private bool TryReturn()
            {
                TaskTracker.RemoveTracking(this);
                core.Reset();
                cancellationToken = default;
                return pool.TryPush(this);
            }
        }

        private sealed class NextFramePromise : IAppaTaskSource,
                                                IPlayerLoopItem,
                                                ITaskPoolNode<NextFramePromise>
        {
            private static TaskPool<NextFramePromise> pool;
            private NextFramePromise nextNode;
            public ref NextFramePromise NextNode => ref nextNode;

            static NextFramePromise()
            {
                TaskPool.RegisterSizeGetter(typeof(NextFramePromise), () => pool.Size);
            }

            private int frameCount;
            private CancellationToken cancellationToken;
            private AppaTaskCompletionSourceCore<AsyncUnit> core;

            private NextFramePromise()
            {
            }

            public static IAppaTaskSource Create(
                PlayerLoopTiming timing,
                CancellationToken cancellationToken,
                out short token)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return AutoResetAppaTaskCompletionSource.CreateFromCanceled(cancellationToken, out token);
                }

                if (!pool.TryPop(out var result))
                {
                    result = new NextFramePromise();
                }

                result.frameCount = PlayerLoopHelper.IsMainThread ? CoreClock.Instance.FrameCount : -1;
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

                if (frameCount == CoreClock.Instance.FrameCount)
                {
                    return true;
                }

                core.TrySetResult(AsyncUnit.Default);
                return false;
            }

            private bool TryReturn()
            {
                TaskTracker.RemoveTracking(this);
                core.Reset();
                cancellationToken = default;
                return pool.TryPush(this);
            }
        }

        private sealed class DelayFramePromise : IAppaTaskSource,
                                                 IPlayerLoopItem,
                                                 ITaskPoolNode<DelayFramePromise>
        {
            private static TaskPool<DelayFramePromise> pool;
            private DelayFramePromise nextNode;
            public ref DelayFramePromise NextNode => ref nextNode;

            static DelayFramePromise()
            {
                TaskPool.RegisterSizeGetter(typeof(DelayFramePromise), () => pool.Size);
            }

            private int initialFrame;
            private int delayFrameCount;
            private CancellationToken cancellationToken;

            private int currentFrameCount;
            private AppaTaskCompletionSourceCore<AsyncUnit> core;

            private DelayFramePromise()
            {
            }

            public static IAppaTaskSource Create(
                int delayFrameCount,
                PlayerLoopTiming timing,
                CancellationToken cancellationToken,
                out short token)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return AutoResetAppaTaskCompletionSource.CreateFromCanceled(cancellationToken, out token);
                }

                if (!pool.TryPop(out var result))
                {
                    result = new DelayFramePromise();
                }

                result.delayFrameCount = delayFrameCount;
                result.cancellationToken = cancellationToken;
                result.initialFrame = PlayerLoopHelper.IsMainThread ? CoreClock.Instance.FrameCount : -1;

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

                if (currentFrameCount == 0)
                {
                    if (delayFrameCount == 0) // same as Yield
                    {
                        core.TrySetResult(AsyncUnit.Default);
                        return false;
                    }

                    // skip in initial frame.
                    if (initialFrame == CoreClock.Instance.FrameCount)
                    {
                        return true;
                    }
                }

                if (++currentFrameCount >= delayFrameCount)
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
                currentFrameCount = default;
                delayFrameCount = default;
                cancellationToken = default;
                return pool.TryPush(this);
            }
        }

        private sealed class DelayPromise : IAppaTaskSource, IPlayerLoopItem, ITaskPoolNode<DelayPromise>
        {
            private static TaskPool<DelayPromise> pool;
            private DelayPromise nextNode;
            public ref DelayPromise NextNode => ref nextNode;

            static DelayPromise()
            {
                TaskPool.RegisterSizeGetter(typeof(DelayPromise), () => pool.Size);
            }

            private int initialFrame;
            private float delayTimeSpan;
            private float elapsed;
            private CancellationToken cancellationToken;

            private AppaTaskCompletionSourceCore<object> core;

            private DelayPromise()
            {
            }

            public static IAppaTaskSource Create(
                TimeSpan delayTimeSpan,
                PlayerLoopTiming timing,
                CancellationToken cancellationToken,
                out short token)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return AutoResetAppaTaskCompletionSource.CreateFromCanceled(cancellationToken, out token);
                }

                if (!pool.TryPop(out var result))
                {
                    result = new DelayPromise();
                }

                result.elapsed = 0.0f;
                result.delayTimeSpan = (float)delayTimeSpan.TotalSeconds;
                result.cancellationToken = cancellationToken;
                result.initialFrame = PlayerLoopHelper.IsMainThread ? CoreClock.Instance.FrameCount : -1;

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

                if (elapsed == 0.0f)
                {
                    if (initialFrame == CoreClock.Instance.FrameCount)
                    {
                        return true;
                    }
                }

                elapsed += CoreClock.Instance.DeltaTime;
                if (elapsed >= delayTimeSpan)
                {
                    core.TrySetResult(null);
                    return false;
                }

                return true;
            }

            private bool TryReturn()
            {
                TaskTracker.RemoveTracking(this);
                core.Reset();
                delayTimeSpan = default;
                elapsed = default;
                cancellationToken = default;
                return pool.TryPush(this);
            }
        }

        private sealed class DelayIgnoreTimeScalePromise : IAppaTaskSource,
                                                           IPlayerLoopItem,
                                                           ITaskPoolNode<DelayIgnoreTimeScalePromise>
        {
            private static TaskPool<DelayIgnoreTimeScalePromise> pool;
            private DelayIgnoreTimeScalePromise nextNode;
            public ref DelayIgnoreTimeScalePromise NextNode => ref nextNode;

            static DelayIgnoreTimeScalePromise()
            {
                TaskPool.RegisterSizeGetter(typeof(DelayIgnoreTimeScalePromise), () => pool.Size);
            }

            private float delayFrameTimeSpan;
            private float elapsed;
            private int initialFrame;
            private CancellationToken cancellationToken;

            private AppaTaskCompletionSourceCore<object> core;

            private DelayIgnoreTimeScalePromise()
            {
            }

            public static IAppaTaskSource Create(
                TimeSpan delayFrameTimeSpan,
                PlayerLoopTiming timing,
                CancellationToken cancellationToken,
                out short token)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return AutoResetAppaTaskCompletionSource.CreateFromCanceled(cancellationToken, out token);
                }

                if (!pool.TryPop(out var result))
                {
                    result = new DelayIgnoreTimeScalePromise();
                }

                result.elapsed = 0.0f;
                result.delayFrameTimeSpan = (float)delayFrameTimeSpan.TotalSeconds;
                result.initialFrame = PlayerLoopHelper.IsMainThread ? CoreClock.Instance.FrameCount : -1;
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

                if (elapsed == 0.0f)
                {
                    if (initialFrame == CoreClock.Instance.FrameCount)
                    {
                        return true;
                    }
                }

                elapsed += CoreClock.Instance.UnscaledDeltaTime;
                if (elapsed >= delayFrameTimeSpan)
                {
                    core.TrySetResult(null);
                    return false;
                }

                return true;
            }

            private bool TryReturn()
            {
                TaskTracker.RemoveTracking(this);
                core.Reset();
                delayFrameTimeSpan = default;
                elapsed = default;
                cancellationToken = default;
                return pool.TryPush(this);
            }
        }

        private sealed class DelayRealtimePromise : IAppaTaskSource,
                                                    IPlayerLoopItem,
                                                    ITaskPoolNode<DelayRealtimePromise>
        {
            private static TaskPool<DelayRealtimePromise> pool;
            private DelayRealtimePromise nextNode;
            public ref DelayRealtimePromise NextNode => ref nextNode;

            static DelayRealtimePromise()
            {
                TaskPool.RegisterSizeGetter(typeof(DelayRealtimePromise), () => pool.Size);
            }

            private long delayTimeSpanTicks;
            private ValueStopwatch stopwatch;
            private CancellationToken cancellationToken;

            private AppaTaskCompletionSourceCore<AsyncUnit> core;

            private DelayRealtimePromise()
            {
            }

            public static IAppaTaskSource Create(
                TimeSpan delayTimeSpan,
                PlayerLoopTiming timing,
                CancellationToken cancellationToken,
                out short token)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return AutoResetAppaTaskCompletionSource.CreateFromCanceled(cancellationToken, out token);
                }

                if (!pool.TryPop(out var result))
                {
                    result = new DelayRealtimePromise();
                }

                result.stopwatch = ValueStopwatch.StartNew();
                result.delayTimeSpanTicks = delayTimeSpan.Ticks;
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

                if (stopwatch.IsInvalid)
                {
                    core.TrySetResult(AsyncUnit.Default);
                    return false;
                }

                if (stopwatch.ElapsedTicks >= delayTimeSpanTicks)
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
                stopwatch = default;
                cancellationToken = default;
                return pool.TryPush(this);
            }
        }
    }

    public readonly struct YieldAwaitable
    {
        private readonly PlayerLoopTiming timing;

        public YieldAwaitable(PlayerLoopTiming timing)
        {
            this.timing = timing;
        }

        public Awaiter GetAwaiter()
        {
            return new Awaiter(timing);
        }

        public AppaTask ToAppaTask()
        {
            return AppaTask.Yield(timing, CancellationToken.None);
        }

        public readonly struct Awaiter : ICriticalNotifyCompletion
        {
            private readonly PlayerLoopTiming timing;

            public Awaiter(PlayerLoopTiming timing)
            {
                this.timing = timing;
            }

            public bool IsCompleted => false;

            public void GetResult()
            {
            }

            public void OnCompleted(Action continuation)
            {
                PlayerLoopHelper.AddContinuation(timing, continuation);
            }

            public void UnsafeOnCompleted(Action continuation)
            {
                PlayerLoopHelper.AddContinuation(timing, continuation);
            }
        }
    }
}
