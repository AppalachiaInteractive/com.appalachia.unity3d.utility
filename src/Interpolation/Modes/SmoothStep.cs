namespace Appalachia.Utility.Interpolation.Modes
{
    public struct SmoothStep : IInterpolationMode
    {
        public float Interpolate(float v0, float v1, float t)
        {
            return InterpolatorFactory.SmoothStep(v0, v1, t);
        }
    }
}
