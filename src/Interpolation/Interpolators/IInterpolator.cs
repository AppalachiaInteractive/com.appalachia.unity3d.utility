namespace Appalachia.Utility.Interpolation.Interpolators
{
    public interface IInterpolator
    {
        float min { get; }
        float max { get; }
        float current { get; set; }
        float percentage { get; set; }
    }
}
