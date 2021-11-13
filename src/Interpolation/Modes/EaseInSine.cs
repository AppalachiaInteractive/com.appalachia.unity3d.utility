namespace Appalachia.Utility.Interpolation.Modes
{
    public struct EaseInSine : IInterpolationMode
    {
        public float Interpolate(float v0, float v1, float t)
        {
            return InterpolatorFactory.EaseInSine(v0, v1, t);
        }
    }
}
