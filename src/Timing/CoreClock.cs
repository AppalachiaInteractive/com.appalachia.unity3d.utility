#region

using Appalachia.Utility.Timing.Contracts;
using Unity.Profiling;

#endregion

namespace Appalachia.Utility.Timing
{
#if UNITY_EDITOR
    [UnityEditor.InitializeOnLoad]
#endif
    public class CoreClock : ICoreTimeProvider
    {
        static CoreClock()
        {
            _lock = new object();
        }

        public CoreClock()
        {
            using (_PRF_CoreClock.Auto())
            {
                _defaultTimeProvider = new DefaultCoreTimeProvider();
            }
        }

        #region Static Fields and Autoproperties

        private static CoreClock _instance;

        private static object _lock;

        #endregion

        #region Fields and Autoproperties

        private ICoreTimeProvider _defaultTimeProvider;
        private ICoreTimeProvider _timeProvider;

        #endregion

        public static CoreClock Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new CoreClock();
                        }
                    }
                }

                return _instance;
            }
        }

        public ICoreTimeProvider TimeProvider => _timeProvider ?? _defaultTimeProvider;

        public IReadOnlyCoreTimeProvider ReadOnlyTimeProvider => _timeProvider ?? _defaultTimeProvider;

        public void UseTimeProvider(ICoreTimeProvider timeProvider)
        {
            using (_PRF_UseTimeProvider.Auto())
            {
                _timeProvider = timeProvider;
            }
        }

        #region ICoreTimeProvider Members

        public bool InFixedTimeStep => TimeProvider.InFixedTimeStep;

        public double FixedTimeAsDouble => TimeProvider.FixedTimeAsDouble;

        public double FixedUnscaledTimeAsDouble => TimeProvider.FixedUnscaledTimeAsDouble;

        public double RealtimeSinceStartupAsDouble => TimeProvider.RealtimeSinceStartupAsDouble;

        public double TimeAsDouble => TimeProvider.TimeAsDouble;

        public double TimeSinceLevelLoadAsDouble => TimeProvider.TimeSinceLevelLoadAsDouble;

        public double UnscaledTimeAsDouble => TimeProvider.UnscaledTimeAsDouble;

        public double WorldAge
        {
            get => TimeProvider.WorldAge;
            set => TimeProvider.WorldAge = value;
        }

        public float DeltaTime => TimeProvider.DeltaTime;

        public float FixedDeltaTime
        {
            get => TimeProvider.FixedDeltaTime;
            set => TimeProvider.FixedDeltaTime = value;
        }

        public float FixedTime => TimeProvider.FixedTime;

        public float FixedUnscaledDeltaTime => TimeProvider.FixedUnscaledDeltaTime;

        public float FixedUnscaledTime => TimeProvider.FixedUnscaledTime;

        public float RealtimeSinceStartup => TimeProvider.RealtimeSinceStartup;

        public float SmoothDeltaTime => TimeProvider.SmoothDeltaTime;

        public float Time => TimeProvider.Time;

        public float TimeScale
        {
            get => TimeProvider.TimeScale;
            set => TimeProvider.TimeScale = value;
        }

        public float TimeSinceLevelLoad => TimeProvider.TimeSinceLevelLoad;

        public float UnscaledDeltaTime => TimeProvider.UnscaledDeltaTime;

        public float UnscaledTime => TimeProvider.UnscaledTime;

        public int FrameCount => TimeProvider.FrameCount;

        #endregion

        #region Profiling

        private const string _PRF_PFX = nameof(CoreClock) + ".";

        private static readonly ProfilerMarker _PRF_UseTimeProvider =
            new ProfilerMarker(_PRF_PFX + nameof(UseTimeProvider));

        private static readonly ProfilerMarker _PRF_CoreClock = new(_PRF_PFX + nameof(CoreClock));

        #endregion
    }
}
