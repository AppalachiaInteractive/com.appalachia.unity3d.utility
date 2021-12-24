#region

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Appalachia.Utility.Strings;
using Unity.Mathematics;

#endregion

namespace Appalachia.Utility.Extensions
{
    public static class float3Extensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion fromToRotation(this float3 f, float3 t, bool normalize)
        {
            if (normalize)
            {
                f = math.normalize(f);
                t = math.normalize(t);
            }

            var dotFT = math.dot(f, t);

            if (Math.Abs(dotFT - 1f) < float.Epsilon)
            {
                return quaternion.identity;
            }

            if (Math.Abs(dotFT - -1f) < float.Epsilon)
            {
                return quaternion.EulerXYZ(180f * new float3(0, 1, 1));
            }

            var value = float4.zero;
            value.xyz = math.cross(f, t);
            value.w = math.sqrt(math.dot(f, f) * math.dot(t, t)) + dotFT;

            return math.normalize(new quaternion(value));
        }

        [DebuggerStepThrough]
        public static string ToStringF1(this float3 f)
        {
            return ZString.Format("({0:F1}),({1:F1}),({2:F1})", f.x, f.y, f.z);
        }

        [DebuggerStepThrough]
        public static string ToStringF2(this float3 f)
        {
            return ZString.Format("({0:F2}),({1:F2}),({2:F2})", f.x, f.y, f.z);
        }

        [DebuggerStepThrough]
        public static string ToStringF3(this float3 f)
        {
            return ZString.Format("({0:F3}),({1:F3}),({2:F3})", f.x, f.y, f.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 xyz1(this float3 val)
        {
            return new(val.x, val.y, val.z, 1f);
        }
    }
}
