using Appalachia.Utility.Interpolation.Interpolators;
using Appalachia.Utility.Interpolation.Modes;

namespace Appalachia.Utility.Interpolation
{
    public static class Extensions
    {
        public static float Interpolate<TMode, TInterpolation>(this TMode e, TInterpolation i)
            where TMode : IInterpolationMode
            where TInterpolation : IInterpolator
        {
            return e.Interpolate(i.current, i.max, i.percentage);
        }
    }
}
