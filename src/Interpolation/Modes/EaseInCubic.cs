using Unity.Profiling;

namespace Appalachia.Utility.Interpolation.Modes
{
    public struct EaseInCubic : IInterpolationMode
    {
        #region IInterpolationMode Members

        public float Interpolate(float min, float max, float percentage)
        {
            using (_PRF_Interpolate.Auto())
            {
                return InterpolatorFactory.EaseInCubic(min, max, percentage);
            }
        }

        public InterpolationMode mode => InterpolationMode.EaseInCubic;

        #endregion

        #region Profiling

        private const string _PRF_PFX = nameof(EaseInCubic) + ".";

        private static readonly ProfilerMarker _PRF_Interpolate =
            new ProfilerMarker(_PRF_PFX + nameof(Interpolate));

        #endregion
    }
}
