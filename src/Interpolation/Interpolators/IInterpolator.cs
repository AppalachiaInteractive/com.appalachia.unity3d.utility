namespace Appalachia.Utility.Interpolation.Interpolators
{
    public interface IInterpolator
    {
        float start { get; }
        float target { get; }
        float value { get; set; }
        float time { get; set; }
    }
}
