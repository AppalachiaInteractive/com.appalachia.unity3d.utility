#region

#endregion

using Unity.Mathematics;

namespace Appalachia.Utility.Constants
{
    public struct float3c
    {
        public static readonly float3 forward = new(0f, 0f, 1f);
        public static readonly float3 back = new(0f, 0f, -1f);
        public static readonly float3 right = new(1f, 0f, 0f);
        public static readonly float3 left = new(-1f, 0f, 0f);
        public static readonly float3 up = new(0f, 1f, 0f);
        public static readonly float3 down = new(0f, -1f, 0f);
        public static readonly float3 one = new(1f, 1f, 1f);
        public static readonly float3 half = new(.5f, .5f, .5f);
        public static readonly float3 neg_one = new(-1f, -1f, -1f);
        public static readonly float3 up_forward = new(0f, 1f, 1f);
        public static readonly float3 up_back = new(0f, 1f, -1f);
        public static readonly float3 up_right = new(1f, 1f, 0f);
        public static readonly float3 up_left = new(-1f, 1f, 0f);
        public static readonly float3 down_forward = new(0f, -1f, 1f);
        public static readonly float3 down_back = new(0f, -1f, -1f);
        public static readonly float3 down_right = new(1f, -1f, 0f);
        public static readonly float3 down_left = new(-1f, -1f, 0f);

        public static readonly float3 forward_right = new(1f, 0f, 1f);
        public static readonly float3 forward_left = new(-1f, 0f, 1f);

        public static readonly float3 back_right = new(1f, 0f, -1f);
        public static readonly float3 back_left = new(-1f, 0f, -1f);
        public static readonly float3 epsilon = new(float.Epsilon, float.Epsilon, float.Epsilon);
    }
}
