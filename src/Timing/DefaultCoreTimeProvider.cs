using Appalachia.Utility.Timing.Contracts;

namespace Appalachia.Utility.Timing
{
    internal class DefaultCoreTimeProvider : ICoreTimeProvider
    {
        #region ICoreTimeProvider Members

        public bool InFixedTimeStep => UnityEngine.Time.inFixedTimeStep;

        public double FixedTimeAsDouble => UnityEngine.Time.fixedTimeAsDouble;

        public double FixedUnscaledTimeAsDouble => UnityEngine.Time.fixedUnscaledTimeAsDouble;

        public double RealtimeSinceStartupAsDouble => UnityEngine.Time.realtimeSinceStartupAsDouble;

        public double TimeAsDouble => UnityEngine.Time.timeAsDouble;

        public double TimeSinceLevelLoadAsDouble => UnityEngine.Time.timeSinceLevelLoadAsDouble;

        public double UnscaledTimeAsDouble => UnityEngine.Time.unscaledTimeAsDouble;

        public double WorldAge
        {
            get => TimeSinceLevelLoadAsDouble;
            set { }
        }

        public float DeltaTime => UnityEngine.Time.deltaTime;

        public float FixedTime => UnityEngine.Time.fixedTime;

        public float FixedUnscaledDeltaTime => UnityEngine.Time.fixedUnscaledDeltaTime;

        public float FixedUnscaledTime => UnityEngine.Time.fixedUnscaledTime;

        public float RealtimeSinceStartup => UnityEngine.Time.realtimeSinceStartup;

        public float SmoothDeltaTime => UnityEngine.Time.smoothDeltaTime;

        public float Time => UnityEngine.Time.time;

        public float TimeSinceLevelLoad => UnityEngine.Time.timeSinceLevelLoad;

        public float UnscaledDeltaTime => UnityEngine.Time.unscaledDeltaTime;

        public float UnscaledTime => UnityEngine.Time.unscaledTime;

        public int FrameCount => UnityEngine.Time.frameCount;

        public float FixedDeltaTime
        {
            get => UnityEngine.Time.fixedDeltaTime;
            set => UnityEngine.Time.fixedDeltaTime = value;
        }

        public float TimeScale
        {
            get => UnityEngine.Time.timeScale;
            set => UnityEngine.Time.timeScale = value;
        }

        #endregion

        /*
        public int CaptureFramerate
        {
            get => UnityEngine.Time.captureFramerate;
            set => UnityEngine.Time.captureFramerate = value;
        }
        
        public int RenderedFrameCount => UnityEngine.Time.renderedFrameCount;

        public float CaptureDeltaTime
        {
            get => UnityEngine.Time.captureDeltaTime;
            set => UnityEngine.Time.captureDeltaTime = value;
        }


        public float MaximumDeltaTime
        {
            get => UnityEngine.Time.maximumDeltaTime;
            set => UnityEngine.Time.maximumDeltaTime = value;
        }

        public float MaximumParticleDeltaTime
        {
            get => UnityEngine.Time.maximumParticleDeltaTime;
            set => UnityEngine.Time.maximumParticleDeltaTime = value;
        }
        */
    }
}
