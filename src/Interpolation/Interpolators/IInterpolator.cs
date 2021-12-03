namespace Appalachia.Utility.Interpolation.Interpolators
{
    public interface IInterpolator
    {
        public InterpolationMode mode { get; set; }
        float min { get; }
        float max { get; }
        float current { get; set; }
        float percentage { get; set; }
    }
}
