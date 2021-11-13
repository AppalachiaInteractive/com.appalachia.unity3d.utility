using UnityEngine;

namespace Appalachia.Utility.Framing
{
    internal readonly struct FramingParameters
    {
        public readonly Vector3 position;
        public readonly Quaternion direction;
        public readonly float size;

        public FramingParameters(Vector3 position, Quaternion direction, float size)
        {
            this.position = position;
            this.direction = direction;
            this.size = size;
        }
    }
}