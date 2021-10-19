namespace Appalachia.Utility.Interpolation.Modes
{
    public struct EaseOutSine : IInterpolationMode
    {
        public float Interpolate(float v0, float v1, float t)
        {
            return InterpolationFactory.EaseOutSine(v0, v1, t);
        }
    }
}
