namespace Appalachia.Utility.Interpolation.Modes
{
    public struct EaseInQuad : IInterpolationMode
    {
        public float Interpolate(float v0, float v1, float t)
        {
            return InterpolatorFactory.EaseInQuad(v0, v1, t);
        }
    }
}
