namespace Appalachia.Utility.Interpolation.Modes
{
    public struct LinearAngle : IInterpolationMode
    {
        public float Interpolate(float x, float y, float percentage)
        {
            return InterpolatorFactory.LinearAngle(x, y, percentage);
        }
    }
}
