namespace Appalachia.Utility.Interpolation.Modes
{
    public struct EaseInSine : IInterpolationMode
    {
        public float Interpolate(float min, float max, float percentage)
        {
            return InterpolatorFactory.EaseInSine(min, max, percentage);
        }
    }
}
