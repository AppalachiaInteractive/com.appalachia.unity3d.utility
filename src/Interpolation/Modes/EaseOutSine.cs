namespace Appalachia.Utility.Interpolation.Modes
{
    public struct EaseOutSine : IInterpolationMode
    {
        public float Interpolate(float min, float max, float percentage)
        {
            return InterpolatorFactory.EaseOutSine(min, max, percentage);
        }
    }
}
