using Unity.Mathematics;

namespace Appalachia.Utility.Constants
{
    public struct double3c
    {
        public static readonly double3 forward = new(0, 0, 1);
        public static readonly double3 back = new(0, 0, -1);
        public static readonly double3 right = new(1, 0, 0);
        public static readonly double3 left = new(-1, 0, 0);
        public static readonly double3 up = new(0, 1, 0);
        public static readonly double3 down = new(0, -1, 0);
        public static readonly double3 one = new(1, 1, 1);
        public static readonly double3 neg_one = new(-1, -1, -1);
        public static readonly double3 up_forward = new(0, 1, 1);
        public static readonly double3 up_back = new(0, 1, -1);
        public static readonly double3 up_right = new(1, 1, 0);
        public static readonly double3 up_left = new(-1, 1, 0);
        public static readonly double3 down_forward = new(0, -1, 1);
        public static readonly double3 down_back = new(0, -1, -1);
        public static readonly double3 down_right = new(1, -1, 0);
        public static readonly double3 down_left = new(-1, -1, 0);

        public static readonly double3
            epsilon = new(double.Epsilon, double.Epsilon, double.Epsilon);
    }
}
