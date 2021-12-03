namespace Appalachia.Utility.Interpolation.Modes
{
    public struct Linear : IInterpolationMode
    {
        public float Interpolate(float min, float max, float percentage)
        {
            return InterpolatorFactory.Linear(min, max, percentage);
        }

        public InterpolationMode mode => InterpolationMode.Linear;
    }
}
