namespace Appalachia.Utility.Interpolation.Modes
{
    public struct EaseOutCubic : IInterpolationMode
    {
        public float Interpolate(float v0, float v1, float t)
        {
            return InterpolatorFactory.EaseOutCubic(v0, v1, t);
        }
    }
}
