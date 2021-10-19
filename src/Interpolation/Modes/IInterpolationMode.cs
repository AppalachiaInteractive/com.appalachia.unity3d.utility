namespace Appalachia.Utility.Interpolation.Modes
{
    public interface IInterpolationMode
    {
        float Interpolate(float v0, float v1, float t);
    }
}
