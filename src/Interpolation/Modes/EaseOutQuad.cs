namespace Appalachia.Utility.Interpolation.Modes
{
    public struct EaseOutQuad : IInterpolationMode
    {
        public float Interpolate(float min, float max, float percentage)
        {
            return InterpolatorFactory.EaseOutQuad(min, max, percentage);
        }
    }
}
