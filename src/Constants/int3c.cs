using Unity.Mathematics;

namespace Appalachia.Utility.Constants
{
    public struct int3c
    {
        public static readonly int3 forward = new(0, 0, 1);
        public static readonly int3 back = new(0, 0, -1);
        public static readonly int3 right = new(1, 0, 0);
        public static readonly int3 left = new(-1, 0, 0);
        public static readonly int3 up = new(0, 1, 0);
        public static readonly int3 down = new(0, -1, 0);
        public static readonly int3 one = new(1, 1, 1);
        public static readonly int3 neg_one = new(-1, -1, -1);
        public static readonly int3 up_forward = new(0, 1, 1);
        public static readonly int3 up_back = new(0, 1, -1);
        public static readonly int3 up_right = new(1, 1, 0);
        public static readonly int3 up_left = new(-1, 1, 0);
        public static readonly int3 down_forward = new(0, -1, 1);
        public static readonly int3 down_back = new(0, -1, -1);
        public static readonly int3 down_right = new(1, -1, 0);
        public static readonly int3 down_left = new(-1, -1, 0);
    }
}
