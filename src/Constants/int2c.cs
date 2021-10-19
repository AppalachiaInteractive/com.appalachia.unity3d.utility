using Unity.Mathematics;

namespace Appalachia.Utility.Constants
{
    public struct int2c
    {
        public static readonly int2 forward = new(0, 1);
        public static readonly int2 back = new(0, -1);
        public static readonly int2 right = new(1, 0);
        public static readonly int2 left = new(-1, 0);
        public static readonly int2 one = new(1, 1);
        public static readonly int2 neg_one = new(-1, -1);
    }
}
