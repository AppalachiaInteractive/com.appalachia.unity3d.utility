namespace Appalachia.Utility.Interpolation.Modes
{
    public struct EaseOutCubic : IInterpolationMode
    {
        public float Interpolate(float min, float max, float percentage)
        {
            return InterpolatorFactory.EaseOutCubic(min, max, percentage);
        }

        public InterpolationMode mode => InterpolationMode.EaseOutCubic;
    }
}
