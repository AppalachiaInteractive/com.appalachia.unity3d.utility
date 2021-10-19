namespace Appalachia.Utility.Interpolation.Modes
{
    public struct EaseInOutSine : IInterpolationMode
    {
        public float Interpolate(float v0, float v1, float t)
        {
            return InterpolationFactory.EaseInOutSine(v0, v1, t);
        }
    }
}
