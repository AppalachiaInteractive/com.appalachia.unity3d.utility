using Unity.Mathematics;

namespace Appalachia.Utility.Constants
{
    public struct double4c
    {
        public static readonly double4 one = new(1, 1, 1, 1);
        public static readonly double4 half = new(.5, .5, .5, .5);
        public static readonly double4 neg_one = new(-1, -1, -1, -1);

        public static readonly double4 epsilon =
            new(double.Epsilon, double.Epsilon, double.Epsilon, double.Epsilon);
    }
}
