namespace Appalachia.Utility.Interpolation.Modes
{
    public struct SmoothStep : IInterpolationMode
    {
        public float Interpolate(float min, float max, float percentage)
        {
            return InterpolatorFactory.SmoothStep(min, max, percentage);
        }

        public InterpolationMode mode => InterpolationMode.SmoothStep;
    }
}
