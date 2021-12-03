namespace Appalachia.Utility.Interpolation.Modes
{
    public interface IInterpolationMode
    {
        float Interpolate(float min, float max, float percentage);
        public InterpolationMode mode { get; }
    }
}
