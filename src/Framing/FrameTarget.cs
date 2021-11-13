namespace Appalachia.Utility.Framing
{
    public enum FrameTarget
    {
#if UNITY_EDITOR
        SceneView,
#endif
        MainCamera,
    }
}