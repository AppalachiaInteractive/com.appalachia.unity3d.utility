namespace Appalachia.Utility.Interpolation.Modes
{
    public struct EaseInOutSine : IInterpolationMode
    {
        public float Interpolate(float min, float max, float percentage)
        {
            return InterpolatorFactory.EaseInOutSine(min, max, percentage);
        }
    }
}
