using Unity.Mathematics;

namespace Appalachia.Utility.Constants
{
    public struct float4c
    {
        public static readonly float4 epsilon =
            new(float.Epsilon, float.Epsilon, float.Epsilon, float.Epsilon);

        public static readonly float4 one = new(1f, 1f, 1f, 1f);
        public static readonly float4 neg_one = new(1f, 1f, 1f, 1f);
        public static readonly float4 half = new(.5f, .5f, .5f, .5f);
    }
}
