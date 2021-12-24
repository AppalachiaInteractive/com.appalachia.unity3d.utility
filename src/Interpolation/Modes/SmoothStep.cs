using Unity.Profiling;

namespace Appalachia.Utility.Interpolation.Modes
{
    public struct SmoothStep : IInterpolationMode
    {
        #region IInterpolationMode MembeIInterpolats

        public float Interpolate(float min, float max, float percentage)
        {
            using (_PRF_Interpolate.Auto())
            {
                return InterpolatorFactory.SmoothStep(min, max, percentage);
            }
        }

        public InterpolationMode mode => InterpolationMode.SmoothStep;

        #endregion

        #region Profiling

        private const string _PRF_PFX = nameof(SmoothStep) + ".";

        private static readonly ProfilerMarker _PRF_Interpolate =
            new ProfilerMarker(_PRF_PFX + nameof(Interpolate));

        #endregion
    }
}
