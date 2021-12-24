using Unity.Profiling;

namespace Appalachia.Utility.Interpolation.Modes
{
    public struct EaseInOutSine : IInterpolationMode
    {
        #region IInterpolationMode Members

        public float Interpolate(float min, float max, float percentage)
        {
            using (_PRF_Interpolate.Auto())
            {
                return InterpolatorFactory.EaseInOutSine(min, max, percentage);
            }
        }

        public InterpolationMode mode => InterpolationMode.EaseInOutSine;

        #endregion

        #region Profiling

        private const string _PRF_PFX = nameof(EaseInOutSine) + ".";

        private static readonly ProfilerMarker _PRF_Interpolate =
            new ProfilerMarker(_PRF_PFX + nameof(Interpolate));

        #endregion
    }
}
