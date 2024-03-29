﻿using System;
using System.Threading;
using Appalachia.Utility.Timing;

namespace Appalachia.Utility.Async.Linq.UnityExtensions
{
    public static partial class AppaTaskAsyncEnumerable
    {
        public static IAppaTaskAsyncEnumerable<AsyncUnit> Timer(
            TimeSpan dueTime,
            PlayerLoopTiming updateTiming = PlayerLoopTiming.Update,
            bool ignoreTimeScale = false)
        {
            return new Timer(dueTime, null, updateTiming, ignoreTimeScale);
        }

        public static IAppaTaskAsyncEnumerable<AsyncUnit> Timer(
            TimeSpan dueTime,
            TimeSpan period,
            PlayerLoopTiming updateTiming = PlayerLoopTiming.Update,
            bool ignoreTimeScale = false)
        {
            return new Timer(dueTime, period, updateTiming, ignoreTimeScale);
        }

        public static IAppaTaskAsyncEnumerable<AsyncUnit> Interval(
            TimeSpan period,
            PlayerLoopTiming updateTiming = PlayerLoopTiming.Update,
            bool ignoreTimeScale = false)
        {
            return new Timer(period, period, updateTiming, ignoreTimeScale);
        }

        public static IAppaTaskAsyncEnumerable<AsyncUnit> TimerFrame(
            int dueTimeFrameCount,
            PlayerLoopTiming updateTiming = PlayerLoopTiming.Update)
        {
            if (dueTimeFrameCount < 0)
            {
                throw new ArgumentOutOfRangeException(
                    "Delay does not allow minus delayFrameCount. dueTimeFrameCount:" + dueTimeFrameCount
                );
            }

            return new TimerFrame(dueTimeFrameCount, null, updateTiming);
        }

        public static IAppaTaskAsyncEnumerable<AsyncUnit> TimerFrame(
            int dueTimeFrameCount,
            int periodFrameCount,
            PlayerLoopTiming updateTiming = PlayerLoopTiming.Update)
        {
            if (dueTimeFrameCount < 0)
            {
                throw new ArgumentOutOfRangeException(
                    "Delay does not allow minus delayFrameCount. dueTimeFrameCount:" + dueTimeFrameCount
                );
            }

            if (periodFrameCount < 0)
            {
                throw new ArgumentOutOfRangeException(
                    "Delay does not allow minus periodFrameCount. periodFrameCount:" + dueTimeFrameCount
                );
            }

            return new TimerFrame(dueTimeFrameCount, periodFrameCount, updateTiming);
        }

        public static IAppaTaskAsyncEnumerable<AsyncUnit> IntervalFrame(
            int intervalFrameCount,
            PlayerLoopTiming updateTiming = PlayerLoopTiming.Update)
        {
            if (intervalFrameCount < 0)
            {
                throw new ArgumentOutOfRangeException(
                    "Delay does not allow minus intervalFrameCount. intervalFrameCount:" + intervalFrameCount
                );
            }

            return new TimerFrame(intervalFrameCount, intervalFrameCount, updateTiming);
        }
    }

    internal class Timer : IAppaTaskAsyncEnumerable<AsyncUnit>
    {
        private readonly PlayerLoopTiming updateTiming;
        private readonly TimeSpan dueTime;
        private readonly TimeSpan? period;
        private readonly bool ignoreTimeScale;

        public Timer(TimeSpan dueTime, TimeSpan? period, PlayerLoopTiming updateTiming, bool ignoreTimeScale)
        {
            this.updateTiming = updateTiming;
            this.dueTime = dueTime;
            this.period = period;
            this.ignoreTimeScale = ignoreTimeScale;
        }

        public IAppaTaskAsyncEnumerator<AsyncUnit> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            return new _Timer(dueTime, period, updateTiming, ignoreTimeScale, cancellationToken);
        }

        private class _Timer : MoveNextSource, IAppaTaskAsyncEnumerator<AsyncUnit>, IPlayerLoopItem
        {
            private readonly float dueTime;
            private readonly float? period;
            private readonly PlayerLoopTiming updateTiming;
            private readonly bool ignoreTimeScale;
            private CancellationToken cancellationToken;

            private int initialFrame;
            private float elapsed;
            private bool dueTimePhase;
            private bool completed;
            private bool disposed;

            public _Timer(
                TimeSpan dueTime,
                TimeSpan? period,
                PlayerLoopTiming updateTiming,
                bool ignoreTimeScale,
                CancellationToken cancellationToken)
            {
                this.dueTime = (float)dueTime.TotalSeconds;
                this.period = period == null ? null : (float?)period.Value.TotalSeconds;

                if (this.dueTime <= 0)
                {
                    this.dueTime = 0;
                }

                if (this.period != null)
                {
                    if (this.period <= 0)
                    {
                        this.period = 1;
                    }
                }

                initialFrame = PlayerLoopHelper.IsMainThread ? CoreClock.Instance.FrameCount : -1;
                dueTimePhase = true;
                this.updateTiming = updateTiming;
                this.ignoreTimeScale = ignoreTimeScale;
                this.cancellationToken = cancellationToken;
                TaskTracker.TrackActiveTask(this, 2);
                PlayerLoopHelper.AddAction(updateTiming, this);
            }

            public AsyncUnit Current => default;

            public AppaTask<bool> MoveNextAsync()
            {
                // return false instead of throw
                if (disposed || cancellationToken.IsCancellationRequested || completed)
                {
                    return CompletedTasks.False;
                }

                // reset value here.
                elapsed = 0;

                completionSource.Reset();
                return new AppaTask<bool>(this, completionSource.Version);
            }

            public AppaTask DisposeAsync()
            {
                if (!disposed)
                {
                    disposed = true;
                    TaskTracker.RemoveTracking(this);
                }

                return default;
            }

            public bool MoveNext()
            {
                if (disposed || cancellationToken.IsCancellationRequested)
                {
                    completionSource.TrySetResult(false);
                    return false;
                }

                if (dueTimePhase)
                {
                    if (elapsed == 0)
                    {
                        // skip in initial frame.
                        if (initialFrame == CoreClock.Instance.FrameCount)
                        {
                            return true;
                        }
                    }

                    elapsed += ignoreTimeScale
                        ? CoreClock.Instance.UnscaledDeltaTime
                        : CoreClock.Instance.DeltaTime;

                    if (elapsed >= dueTime)
                    {
                        dueTimePhase = false;
                        completionSource.TrySetResult(true);
                    }
                }
                else
                {
                    if (period == null)
                    {
                        completed = true;
                        completionSource.TrySetResult(false);
                        return false;
                    }

                    elapsed += ignoreTimeScale
                        ? CoreClock.Instance.UnscaledDeltaTime
                        : CoreClock.Instance.DeltaTime;

                    if (elapsed >= period)
                    {
                        completionSource.TrySetResult(true);
                    }
                }

                return true;
            }
        }
    }

    internal class TimerFrame : IAppaTaskAsyncEnumerable<AsyncUnit>
    {
        private readonly PlayerLoopTiming updateTiming;
        private readonly int dueTimeFrameCount;
        private readonly int? periodFrameCount;

        public TimerFrame(int dueTimeFrameCount, int? periodFrameCount, PlayerLoopTiming updateTiming)
        {
            this.updateTiming = updateTiming;
            this.dueTimeFrameCount = dueTimeFrameCount;
            this.periodFrameCount = periodFrameCount;
        }

        public IAppaTaskAsyncEnumerator<AsyncUnit> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            return new _TimerFrame(dueTimeFrameCount, periodFrameCount, updateTiming, cancellationToken);
        }

        private class _TimerFrame : MoveNextSource, IAppaTaskAsyncEnumerator<AsyncUnit>, IPlayerLoopItem
        {
            private readonly int dueTimeFrameCount;
            private readonly int? periodFrameCount;
            private CancellationToken cancellationToken;

            private int initialFrame;
            private int currentFrame;
            private bool dueTimePhase;
            private bool completed;
            private bool disposed;

            public _TimerFrame(
                int dueTimeFrameCount,
                int? periodFrameCount,
                PlayerLoopTiming updateTiming,
                CancellationToken cancellationToken)
            {
                if (dueTimeFrameCount <= 0)
                {
                    dueTimeFrameCount = 0;
                }

                if (periodFrameCount != null)
                {
                    if (periodFrameCount <= 0)
                    {
                        periodFrameCount = 1;
                    }
                }

                initialFrame = PlayerLoopHelper.IsMainThread ? CoreClock.Instance.FrameCount : -1;
                dueTimePhase = true;
                this.dueTimeFrameCount = dueTimeFrameCount;
                this.periodFrameCount = periodFrameCount;
                this.cancellationToken = cancellationToken;

                TaskTracker.TrackActiveTask(this, 2);
                PlayerLoopHelper.AddAction(updateTiming, this);
            }

            public AsyncUnit Current => default;

            public AppaTask<bool> MoveNextAsync()
            {
                // return false instead of throw
                if (disposed || cancellationToken.IsCancellationRequested || completed)
                {
                    return CompletedTasks.False;
                }

                // reset value here.
                currentFrame = 0;

                completionSource.Reset();
                return new AppaTask<bool>(this, completionSource.Version);
            }

            public AppaTask DisposeAsync()
            {
                if (!disposed)
                {
                    disposed = true;
                    TaskTracker.RemoveTracking(this);
                }

                return default;
            }

            public bool MoveNext()
            {
                if (disposed || cancellationToken.IsCancellationRequested)
                {
                    completionSource.TrySetResult(false);
                    return false;
                }

                if (dueTimePhase)
                {
                    if (currentFrame == 0)
                    {
                        if (dueTimeFrameCount == 0)
                        {
                            dueTimePhase = false;
                            completionSource.TrySetResult(true);
                            return true;
                        }

                        // skip in initial frame.
                        if (initialFrame == CoreClock.Instance.FrameCount)
                        {
                            return true;
                        }
                    }

                    if (++currentFrame >= dueTimeFrameCount)
                    {
                        dueTimePhase = false;
                        completionSource.TrySetResult(true);
                    }
                }
                else
                {
                    if (periodFrameCount == null)
                    {
                        completed = true;
                        completionSource.TrySetResult(false);
                        return false;
                    }

                    if (++currentFrame >= periodFrameCount)
                    {
                        completionSource.TrySetResult(true);
                    }
                }

                return true;
            }
        }
    }
}
