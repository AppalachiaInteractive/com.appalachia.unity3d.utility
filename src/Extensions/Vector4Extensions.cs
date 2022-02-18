#region

using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Profiling;
using UnityEngine;

#endregion

namespace Appalachia.Utility.Extensions
{
    public static class Vector4Extensions
    {
        public static Vector4 Derivative(this Vector4 older, Vector4 newer, float elapsed)
        {
            return (newer - older) / elapsed;
        }

        public static bool GreaterThan(this Vector4 vector4, Vector4 other)
        {
            return (vector4.x > other.x) &&
                   (vector4.y > other.y) &&
                   (vector4.z > other.z) &&
                   (vector4.w > other.w);
        }

        public static bool IsUniform(this Vector4 vector)
        {
            return (Math.Abs(vector.x - vector.y) < float.Epsilon) &&
                   (Math.Abs(vector.x - vector.z) < float.Epsilon) &&
                   (Math.Abs(vector.x - vector.w) < float.Epsilon);
        }

        public static bool LessThan(this Vector4 vector4, Vector4 other)
        {
            return (vector4.x < other.x) &&
                   (vector4.y < other.y) &&
                   (vector4.z < other.z) &&
                   (vector4.w < other.w);
        }

        public static Vector4[] Normalize(
            this IEnumerable<Vector4> args,
            Vector4 min,
            Vector4 max,
            bool clamped)
        {
            return args.Select(
                            v =>
                            {
                                var x = (v.x - min.x) / (max.x - min.x);
                                var y = (v.y - min.y) / (max.y - min.y);
                                var z = (v.z - min.z) / (max.z - min.z);
                                var w = (v.w - min.w) / (max.w - min.w);
                                return clamped ? new Vector4(x, y, z, w).normalized : new Vector4(x, y, z, w);
                            }
                        )
                       .ToArray();
        }

        public static Vector4[] Normalize(this IEnumerable<Vector4> args, bool clamped)
        {
            var min = Vector4.positiveInfinity;
            var max = Vector4.negativeInfinity;

            foreach (var arg in args)
            {
                min.x = arg.x < min.x ? arg.x : min.x;
                min.y = arg.y < min.y ? arg.y : min.y;
                min.z = arg.z < min.z ? arg.z : min.z;
                min.w = arg.w < min.w ? arg.w : min.w;

                max.x = arg.x > max.x ? arg.x : max.x;
                max.y = arg.y > max.y ? arg.y : max.y;
                max.z = arg.z > max.z ? arg.z : max.z;
                max.w = arg.w > max.w ? arg.w : max.w;
            }

            return Normalize(args, min, max, clamped);
        }

        public static Vector4[] NormalizeFrom0(this IEnumerable<Vector4> args, bool clamped)
        {
            var max = Vector4.negativeInfinity;

            foreach (var arg in args)
            {
                max.x = arg.x > max.x ? arg.x : max.x;
                max.y = arg.y > max.y ? arg.y : max.y;
                max.z = arg.z > max.z ? arg.z : max.z;
                max.w = arg.w > max.w ? arg.w : max.w;
            }

            return Normalize(args, Vector4.zero, max, clamped);
        }

        public static bool NotRoughlyOne(this Vector4 input)
        {
            return !Roughly(input, 1);
        }

        public static bool NotRoughlyZero(this Vector4 input)
        {
            return !Roughly(input, 0);
        }

        public static Vector4 Reciprocal(this Vector4 value)
        {
            using (_PRF_Reciprocal.Auto())
            {
                return new(1f / value.x, 1f / value.y, 1f / value.z, 1f / value.w);
            }
        }

        public static bool Roughly(this Vector4 input, float scalar)
        {
            return input.x.RoughlyEqual(scalar) &&
                   input.y.RoughlyEqual(scalar) &&
                   input.z.RoughlyEqual(scalar) &&
                   input.w.RoughlyEqual(scalar);
        }

        public static bool RoughlyEqual(this Vector4 input, Vector4 other)
        {
            return input.x.RoughlyEqual(other.x) &&
                   input.y.RoughlyEqual(other.y) &&
                   input.z.RoughlyEqual(other.z) &&
                   input.w.RoughlyEqual(other.w);
        }

        public static bool RoughlyOne(this Vector4 input)
        {
            return Roughly(input, 1);
        }

        public static bool RoughlyZero(this Vector4 input)
        {
            return Roughly(input, 0);
        }

        public static Vector4 Round(this Vector4 vector4, int decimalPlaces = 2)
        {
            float multiplier = 1;
            for (var i = 0; i < decimalPlaces; i++)
            {
                multiplier *= 10f;
            }

            return new Vector4(
                Mathf.Round(vector4.x * multiplier) / multiplier,
                Mathf.Round(vector4.y * multiplier) / multiplier,
                Mathf.Round(vector4.z * multiplier) / multiplier,
                Mathf.Round(vector4.w * multiplier) / multiplier
            );
        }

        public static Vector4 ScaleBy(this Vector4 vector, Vector4 scaleBy)
        {
            return Vector4.Scale(vector, scaleBy);
        }

        #region Profiling

        private const string _PRF_PFX = nameof(Vector4Extensions) + ".";

        private static readonly ProfilerMarker _PRF_Reciprocal =
            new ProfilerMarker(_PRF_PFX + nameof(Reciprocal));

        #endregion
    }
}
