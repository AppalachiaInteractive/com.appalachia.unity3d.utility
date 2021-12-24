using Unity.Profiling;

namespace Appalachia.Utility.Interpolation.Modes
{
    public struct LinearAngle : IInterpolationMode
    {
        #region IInterpolationMode Members

        public float Interpolate(float x, float y, float percentage)
        {
            using (_PRF_Interpolate.Auto())
            {
                return InterpolatorFactory.LinearAngle(x, y, percentage);
            }
        }

        public InterpolationMode mode => InterpolationMode.LinearAngle;

        #endregion

        #region Profiling

        private const string _PRF_PFX = nameof(LinearAngle) + ".";

        private static readonly ProfilerMarker _PRF_Interpolate =
            new ProfilerMarker(_PRF_PFX + nameof(Interpolate));

        #endregion
    }
}
