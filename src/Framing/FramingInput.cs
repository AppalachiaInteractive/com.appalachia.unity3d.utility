using UnityEngine;

namespace Appalachia.Utility.Framing
{
    internal readonly struct FramingInput
    {
        public readonly Vector3 forward;
        public readonly Vector3 up;

        public FramingInput(Vector3 forward, Vector3 up)
        {
            this.forward = forward;
            this.up = up;
        }
    }
}