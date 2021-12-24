using Appalachia.Utility.Interpolation.Interpolators;
using Appalachia.Utility.Interpolation.Modes;
using Unity.Profiling;

namespace Appalachia.Utility.Interpolation
{
    public static class Extensions
    {
        public static float Interpolate<TMode, TInterpolation>(this TMode e, TInterpolation i)
            where TMode : IInterpolationMode
            where TInterpolation : IInterpolator
        {
            using (_PRF_Interpolate.Auto())
            {
                i.mode = e.mode;
                return e.Interpolate(i.current, i.max, i.percentage);
            }
        }

        #region Profiling

        private const string _PRF_PFX = nameof(Extensions) + ".";

        private static readonly ProfilerMarker _PRF_Interpolate =
            new ProfilerMarker(_PRF_PFX + nameof(Interpolate));

        #endregion
    }
}
