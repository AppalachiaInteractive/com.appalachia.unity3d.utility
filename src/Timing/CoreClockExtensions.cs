using Unity.Profiling;

namespace Appalachia.Utility.Timing
{
    public static class CoreClockExtensions
    {
        public static double TimeSince(this double timeSince)
        {
            using (_PRF_TimeSince.Auto())
            {
                return CoreClock.Instance.TimeProvider.WorldAge - timeSince;
            }
        }

        public static float TimeSince(this float timeSince)
        {
            using (_PRF_TimeSince.Auto())
            {
                return (float)(CoreClock.Instance.TimeProvider.WorldAge - timeSince);
            }
        }

        #region Profiling

        private const string _PRF_PFX = nameof(CoreClockExtensions) + ".";

        private static readonly ProfilerMarker _PRF_TimeSince =
            new ProfilerMarker(_PRF_PFX + nameof(TimeSince));

        #endregion
    }
}
