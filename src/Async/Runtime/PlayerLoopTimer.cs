#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Threading;
using Appalachia.Utility.Async.Internal;
using Appalachia.Utility.Timing;

namespace Appalachia.Utility.Async
{
    public abstract class PlayerLoopTimer : IDisposable, IPlayerLoopItem
    {
        protected PlayerLoopTimer(
            bool periodic,
            PlayerLoopTiming playerLoopTiming,
            CancellationToken cancellationToken,
            Action<object> timerCallback,
            object state)
        {
            this.periodic = periodic;
            this.playerLoopTiming = playerLoopTiming;
            this.cancellationToken = cancellationToken;
            this.timerCallback = timerCallback;
            this.state = state;
        }

        #region Fields and Autoproperties

        private readonly Action<object> timerCallback;
        private readonly bool periodic;
        private readonly CancellationToken cancellationToken;
        private readonly object state;
        private readonly PlayerLoopTiming playerLoopTiming;
        private bool isDisposed;

        private bool isRunning;
        private bool tryStop;

        #endregion

        public static PlayerLoopTimer Create(
            TimeSpan interval,
            bool periodic,
            DelayType delayType,
            PlayerLoopTiming playerLoopTiming,
            CancellationToken cancellationToken,
            Action<object> timerCallback,
            object state)
        {
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
                    return new IgnoreTimeScalePlayerLoopTimer(
                        interval,
                        periodic,
                        playerLoopTiming,
                        cancellationToken,
                        timerCallback,
                        state
                    );
                case DelayType.Realtime:
                    return new RealtimePlayerLoopTimer(
                        interval,
                        periodic,
                        playerLoopTiming,
                        cancellationToken,
                        timerCallback,
                        state
                    );
                case DelayType.DeltaTime:
                default:
                    return new DeltaTimePlayerLoopTimer(
                        interval,
                        periodic,
                        playerLoopTiming,
                        cancellationToken,
                        timerCallback,
                        state
                    );
            }
        }

        public static PlayerLoopTimer StartNew(
            TimeSpan interval,
            bool periodic,
            DelayType delayType,
            PlayerLoopTiming playerLoopTiming,
            CancellationToken cancellationToken,
            Action<object> timerCallback,
            object state)
        {
            var timer = Create(
                interval,
                periodic,
                delayType,
                playerLoopTiming,
                cancellationToken,
                timerCallback,
                state
            );
            timer.Restart();
            return timer;
        }

        /// <summary>
        ///     Restart(Reset and Start) timer.
        /// </summary>
        public void Restart()
        {
            if (isDisposed)
            {
                throw new ObjectDisposedException(null);
            }

            ResetCore(null); // init state
            if (!isRunning)
            {
                isRunning = true;
                PlayerLoopHelper.AddAction(playerLoopTiming, this);
            }

            tryStop = false;
        }

        /// <summary>
        ///     Restart(Reset and Start) and change interval.
        /// </summary>
        public void Restart(TimeSpan interval)
        {
            if (isDisposed)
            {
                throw new ObjectDisposedException(null);
            }

            ResetCore(interval); // init state
            if (!isRunning)
            {
                isRunning = true;
                PlayerLoopHelper.AddAction(playerLoopTiming, this);
            }

            tryStop = false;
        }

        /// <summary>
        ///     Stop timer.
        /// </summary>
        public void Stop()
        {
            tryStop = true;
        }

        protected abstract bool MoveNextCore();

        protected abstract void ResetCore(TimeSpan? newInterval);

        #region IDisposable Members

        public void Dispose()
        {
            isDisposed = true;
        }

        #endregion

        #region IPlayerLoopItem Members

        bool IPlayerLoopItem.MoveNext()
        {
            if (isDisposed)
            {
                isRunning = false;
                return false;
            }

            if (tryStop)
            {
                isRunning = false;
                return false;
            }

            if (cancellationToken.IsCancellationRequested)
            {
                isRunning = false;
                return false;
            }

            if (!MoveNextCore())
            {
                timerCallback(state);

                if (periodic)
                {
                    ResetCore(null);
                    return true;
                }

                isRunning = false;
                return false;
            }

            return true;
        }

        #endregion
    }

    internal sealed class DeltaTimePlayerLoopTimer : PlayerLoopTimer
    {
        public DeltaTimePlayerLoopTimer(
            TimeSpan interval,
            bool periodic,
            PlayerLoopTiming playerLoopTiming,
            CancellationToken cancellationToken,
            Action<object> timerCallback,
            object state) : base(periodic, playerLoopTiming, cancellationToken, timerCallback, state)
        {
            ResetCore(interval);
        }

        #region Fields and Autoproperties

        private float elapsed;
        private float interval;
        private int initialFrame;

        #endregion

        /// <inheritdoc />
        protected override bool MoveNextCore()
        {
            if (elapsed == 0.0f)
            {
                if (initialFrame == CoreClock.Instance.FrameCount)
                {
                    return true;
                }
            }

            elapsed += CoreClock.Instance.DeltaTime;
            if (elapsed >= interval)
            {
                return false;
            }

            return true;
        }

        /// <inheritdoc />
        protected override void ResetCore(TimeSpan? interval)
        {
            elapsed = 0.0f;
            initialFrame = PlayerLoopHelper.IsMainThread ? CoreClock.Instance.FrameCount : -1;
            if (interval != null)
            {
                this.interval = (float)interval.Value.TotalSeconds;
            }
        }
    }

    internal sealed class IgnoreTimeScalePlayerLoopTimer : PlayerLoopTimer
    {
        public IgnoreTimeScalePlayerLoopTimer(
            TimeSpan interval,
            bool periodic,
            PlayerLoopTiming playerLoopTiming,
            CancellationToken cancellationToken,
            Action<object> timerCallback,
            object state) : base(periodic, playerLoopTiming, cancellationToken, timerCallback, state)
        {
            ResetCore(interval);
        }

        #region Fields and Autoproperties

        private float elapsed;
        private float interval;
        private int initialFrame;

        #endregion

        /// <inheritdoc />
        protected override bool MoveNextCore()
        {
            if (elapsed == 0.0f)
            {
                if (initialFrame == CoreClock.Instance.FrameCount)
                {
                    return true;
                }
            }

            elapsed += CoreClock.Instance.UnscaledDeltaTime;
            if (elapsed >= interval)
            {
                return false;
            }

            return true;
        }

        /// <inheritdoc />
        protected override void ResetCore(TimeSpan? interval)
        {
            elapsed = 0.0f;
            initialFrame = PlayerLoopHelper.IsMainThread ? CoreClock.Instance.FrameCount : -1;
            if (interval != null)
            {
                this.interval = (float)interval.Value.TotalSeconds;
            }
        }
    }

    internal sealed class RealtimePlayerLoopTimer : PlayerLoopTimer
    {
        public RealtimePlayerLoopTimer(
            TimeSpan interval,
            bool periodic,
            PlayerLoopTiming playerLoopTiming,
            CancellationToken cancellationToken,
            Action<object> timerCallback,
            object state) : base(periodic, playerLoopTiming, cancellationToken, timerCallback, state)
        {
            ResetCore(interval);
        }

        #region Fields and Autoproperties

        private long intervalTicks;
        private ValueStopwatch stopwatch;

        #endregion

        /// <inheritdoc />
        protected override bool MoveNextCore()
        {
            if (stopwatch.ElapsedTicks >= intervalTicks)
            {
                return false;
            }

            return true;
        }

        /// <inheritdoc />
        protected override void ResetCore(TimeSpan? interval)
        {
            stopwatch = ValueStopwatch.StartNew();
            if (interval != null)
            {
                intervalTicks = interval.Value.Ticks;
            }
        }
    }
}
