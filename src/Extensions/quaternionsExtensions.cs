#region

using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

#endregion

namespace Appalachia.Utility.Extensions
{
    public static class quaternionsExtensions
    {
        #region Constants and Static Readonly

        public static readonly float3 _back = new(0f, 0f, -1f);
        public static readonly float3 _down = new(0f, -1f, 0f);

        public static readonly float3 _forward = new(0f, 0f, 1f);
        public static readonly float3 _left = new(-1f, 0f, 0f);
        public static readonly float3 _right = new(1f, 0f, 0f);
        public static readonly float3 _up = new(0f, 1f, 0f);

        #endregion

        public static quaternion Anticipate(
            this quaternion first,
            float elapsed,
            quaternion second,
            float anticipationDuration)
        {
            return new(second.value.x + ((anticipationDuration / elapsed) * (second.value.x - first.value.x)),
                second.value.y + ((anticipationDuration / elapsed) * (second.value.y - first.value.y)),
                second.value.z + ((anticipationDuration / elapsed) * (second.value.z - first.value.z)),
                second.value.w + ((anticipationDuration / elapsed) * (second.value.w - first.value.w)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Approximately(this quaternion input, quaternion other)
        {
            return input.Equals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 back(this quaternion quaternion)
        {
            return math.mul(quaternion, _back);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 down(this quaternion quaternion)
        {
            return math.mul(quaternion, _down);
        }

        public static float4x4 float4x4(this quaternion input)
        {
            return new(input, float3.zero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 forward(this quaternion quaternion)
        {
            return math.mul(quaternion, _forward);
        }

        public static float3 GetAngularVelocity(this quaternion older, quaternion newer, float elapsed)
        {
            var q = math.mul(newer, math.inverse(older));

            if (Mathf.Abs(q.value.w) > 0.999f)
            {
                return new float3(0, 0, 0);
            }

            float gain;

            if (q.value.w < 0.0f)
            {
                var angle = Mathf.Acos(-q.value.w);
                gain = (-2.0f * angle) / (Mathf.Sin(angle) * elapsed);
            }
            else
            {
                var angle = Mathf.Acos(q.value.w);
                gain = (2.0f * angle) / (Mathf.Sin(angle) * elapsed);
            }

            return new float3(q.value.x * gain, q.value.y * gain, q.value.z * gain);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 left(this quaternion quaternion)
        {
            return math.mul(quaternion, _left);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 right(this quaternion quaternion)
        {
            return math.mul(quaternion, _right);
        }

        public static float3 ToEuler(this quaternion quat)
        {
            var q = quat.value;
            float3 angles;

            // roll (x-axis rotation)
            var sinr_cosp = 2.0 * ((q.w * q.x) + (q.y * q.z));
            var cosr_cosp = 1.0 - (2.0 * ((q.x * q.x) + (q.y * q.y)));
            angles.x = (float) math.atan2(sinr_cosp, cosr_cosp);

            // pitch (y-axis rotation)
            var sinp = 2.0 * ((q.w * q.y) - (q.z * q.x));
            if (math.abs(sinp) >= 1.0)
            {
                const double val = math.PI / 2.0;
                angles.y = (float) (math.sign(sinp) >= 0.0 ? val : -val);
            }
            else
            {
                angles.y = (float) math.asin(sinp);
            }

            // yaw (z-axis rotation)
            var siny_cosp = 2.0 * ((q.w * q.z) + (q.x * q.y));
            var cosy_cosp = 1.0 - (2.0 * ((q.y * q.y) + (q.z * q.z)));
            angles.z = (float) math.atan2(siny_cosp, cosy_cosp);

            return angles;
        }

        public static Vector3 ToEulerV3(this quaternion quat)
        {
            return ToEuler(quat);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 up(this quaternion quaternion)
        {
            return math.mul(quaternion, _up);
        }
    }
}