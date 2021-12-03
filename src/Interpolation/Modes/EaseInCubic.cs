namespace Appalachia.Utility.Interpolation.Modes
{
    public struct EaseInCubic : IInterpolationMode
    {
        public float Interpolate(float min, float max, float percentage)
        {
            return InterpolatorFactory.EaseInCubic(min, max, percentage);
        }

        public InterpolationMode mode => InterpolationMode.EaseInCubic;
    }
}
