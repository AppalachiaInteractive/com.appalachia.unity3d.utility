using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Profiling;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Appalachia.Utility.Extensions
{
    public static class Vector2Extensions
    {
        public static (float deltaX, float deltaY, Vector2 center) AnchorInfo(
            this Vector2 anchorMin,
            Vector2 anchorMax)
        {
            using (_PRF_FromAnchors.Auto())
            {
                return new(
                    anchorMax.x - anchorMin.x,
                    anchorMax.y - anchorMin.y,
                    anchorMin + (.5f * (anchorMax - anchorMin))
                );
            }
        }

        public static float ClampValue(this Vector2 value, float v)
        {
            using (_PRF_ClampValue.Auto())
            {
                return Mathf.Clamp(v, value.x, value.y);
            }
        }

        public static Vector2 ClampValue(this Vector2 value, Vector2 xRange, Vector2 yRange)
        {
            using (_PRF_ClampValue.Auto())
            {
                if (value.x < xRange.x)
                {
                    value.x = xRange.x;
                }

                if (value.x > xRange.y)
                {
                    value.x = xRange.y;
                }

                if (value.y < yRange.x)
                {
                    value.y = yRange.x;
                }

                if (value.y > yRange.y)
                {
                    value.y = yRange.y;
                }

                return value;
            }
        }

        public static bool GreaterThan(this Vector2 vector3, Vector2 other)
        {
            return (vector3.x > other.x) && (vector3.y > other.y);
        }

        public static bool IsUniform(this Vector2 vector)
        {
            return Math.Abs(vector.x - vector.y) < float.Epsilon;
        }

        public static bool LessThan(this Vector2 vector3, Vector2 other)
        {
            return (vector3.x < other.x) && (vector3.y < other.y);
        }

        public static Vector2[] Normalize(
            this IEnumerable<Vector2> args,
            Vector2 min,
            Vector2 max,
            bool clamped)
        {
            return args.Select(
                            v =>
                            {
                                var x = (v.x - min.x) / (max.x - min.x);
                                var y = (v.y - min.y) / (max.y - min.y);
                                return clamped ? new Vector2(x, y).normalized : new Vector2(x, y);
                            }
                        )
                       .ToArray();
        }

        public static Vector2[] Normalize(this IEnumerable<Vector2> args, bool clamped)
        {
            var min = Vector2.positiveInfinity;
            var max = Vector2.negativeInfinity;

            foreach (var arg in args)
            {
                min.x = arg.x < min.x ? arg.x : min.x;
                min.y = arg.y < min.y ? arg.y : min.y;

                max.x = arg.x > max.x ? arg.x : max.x;
                max.y = arg.y > max.y ? arg.y : max.y;
            }

            return Normalize(args, min, max, clamped);
        }

        public static Vector2[] NormalizeFrom0(this IEnumerable<Vector2> args, bool clamped)
        {
            var max = Vector2.negativeInfinity;

            foreach (var arg in args)
            {
                max.x = arg.x > max.x ? arg.x : max.x;
                max.y = arg.y > max.y ? arg.y : max.y;
            }

            return Normalize(args, Vector2.zero, max, clamped);
        }

        public static float RandomValue(this Vector2 value)
        {
            using (_PRF_RandomValue.Auto())
            {
                return Random.Range(value.x, value.y);
            }
        }

        public static float RangedValue(this Vector2 value, float v)
        {
            using (_PRF_RangedValue.Auto())
            {
                return (v * (value.y - value.x)) + value.x;
            }
        }

        public static Vector2 Reciprocal(this Vector2 value)
        {
            using (_PRF_Reciprocal.Auto())
            {
                return new(1f / value.x, 1f / value.y);
            }
        }

        public static Vector2 Round(this Vector2 vector3, int decimalPlaces = 2)
        {
            float multiplier = 1;
            for (var i = 0; i < decimalPlaces; i++)
            {
                multiplier *= 10f;
            }

            return new Vector2(
                Mathf.Round(vector3.x * multiplier) / multiplier,
                Mathf.Round(vector3.y * multiplier) / multiplier
            );
        }

        public static Vector3 ViewRotationToEulerAngles(this Vector2 input)
        {
            input.y = -input.y;

            return new Vector3(input.y, input.x, 0);
        }

        #region Profiling

        private const string _PRF_PFX = nameof(Vector2Extensions) + ".";

        private static readonly ProfilerMarker _PRF_Reciprocal =
            new ProfilerMarker(_PRF_PFX + nameof(Reciprocal));

        private static readonly ProfilerMarker _PRF_FromAnchors =
            new ProfilerMarker(_PRF_PFX + nameof(AnchorInfo));

        private static readonly ProfilerMarker _PRF_RandomValue =
            new ProfilerMarker(_PRF_PFX + nameof(RandomValue));

        private static readonly ProfilerMarker _PRF_ClampValue =
            new ProfilerMarker(_PRF_PFX + nameof(ClampValue));

        private static readonly ProfilerMarker _PRF_RangedValue =
            new ProfilerMarker(_PRF_PFX + nameof(RangedValue));

        #endregion
    }
}
