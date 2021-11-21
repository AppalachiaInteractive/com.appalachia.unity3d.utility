namespace Appalachia.Utility.Interpolation.Modes
{
    public struct EaseInQuad : IInterpolationMode
    {
        public float Interpolate(float min, float max, float percentage)
        {
            return InterpolatorFactory.EaseInQuad(min, max, percentage);
        }
    }
}
