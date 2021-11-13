using UnityEngine;

namespace Appalachia.Utility.Framing.Targets
{
    public interface IFrameable
    {
        public Camera camera { get; }
        public void LookAt(Vector3 point, Quaternion direction, float newSize, FramingPerspective perspective);
    }
}