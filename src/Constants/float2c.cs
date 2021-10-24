using Unity.Mathematics;

namespace Appalachia.Utility.Constants
{
    public struct float2c
    {
        public static readonly float2 forward = new(0f, 1f);
        public static readonly float2 back = new(0f, -1f);
        public static readonly float2 right = new(1f, 0f);
        public static readonly float2 left = new(-1f, 0f);
        public static readonly float2 one = new(1f, 1f);
        public static readonly float2 neg_one = new(-1f, -1f);
        public static readonly float2 half = new(.5f, .5f);
        public static readonly float2 epsilon = new(float.Epsilon, float.Epsilon);
    }
}
