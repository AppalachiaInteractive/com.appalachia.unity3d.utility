using Unity.Profiling;

namespace Appalachia.Utility.Interpolation.Modes
{
    public struct EaseOutCubic : IInterpolationMode
    {
        #region IInterpolationMode Members

        public float Interpolate(float min, float max, float percentage)
        {
            using (_PRF_Interpolate.Auto())
            {
                return InterpolatorFactory.EaseOutCubic(min, max, percentage);
            }
        }

        public InterpolationMode mode => InterpolationMode.EaseOutCubic;

        #endregion

        #region Profiling

        private const string _PRF_PFX = nameof(EaseOutCubic) + ".";

        private static readonly ProfilerMarker _PRF_Interpolate =
            new ProfilerMarker(_PRF_PFX + nameof(Interpolate));

        #endregion
    }
}
