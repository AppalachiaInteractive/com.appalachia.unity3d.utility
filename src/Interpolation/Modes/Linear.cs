namespace Appalachia.Utility.Interpolation.Modes
{
    public struct Linear : IInterpolationMode
    {
        public float Interpolate(float v0, float v1, float t)
        {
            return InterpolationFactory.Linear(v0, v1, t);
        }
    }
}
