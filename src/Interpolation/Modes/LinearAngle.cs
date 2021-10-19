namespace Appalachia.Utility.Interpolation.Modes
{
    public struct LinearAngle : IInterpolationMode
    {
        public float Interpolate(float x, float y, float t)
        {
            return InterpolationFactory.LinearAngle(x, y, t);
        }
    }
}
