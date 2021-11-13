namespace Appalachia.Utility.Interpolation.Modes
{
    public struct EaseOutSine : IInterpolationMode
    {
        public float Interpolate(float v0, float v1, float t)
        {
            return InterpolatorFactory.EaseOutSine(v0, v1, t);
        }
    }
}
