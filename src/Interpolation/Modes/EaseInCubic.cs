namespace Appalachia.Utility.Interpolation.Modes
{
    public struct EaseInCubic : IInterpolationMode
    {
        public float Interpolate(float v0, float v1, float t)
        {
            return InterpolationFactory.EaseInCubic(v0, v1, t);
        }
    }
}
