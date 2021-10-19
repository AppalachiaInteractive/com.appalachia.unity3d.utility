#region

#endregion

using Unity.Mathematics;

namespace Appalachia.Utility.Constants
{
    public struct double2c
    {
        public static readonly double2 forward = new(0, 1);
        public static readonly double2 back = new(0, -1);
        public static readonly double2 right = new(1, 0);
        public static readonly double2 left = new(-1, 0);
        public static readonly double2 one = new(1, 1);
        public static readonly double2 half = new(.5, .5);
        public static readonly double2 neg_one = new(-1, -1);
        public static readonly double2 epsilon = new(double.Epsilon, double.Epsilon);
    }
}
