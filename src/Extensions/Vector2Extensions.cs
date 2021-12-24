using Unity.Profiling;
using UnityEngine;

namespace Appalachia.Utility.Extensions
{
    public static class Vector2Extensions
    {
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

        #region Profiling

        private const string _PRF_PFX = nameof(Vector2Extensions) + ".";

        private static readonly ProfilerMarker _PRF_RandomValue =
            new ProfilerMarker(_PRF_PFX + nameof(RandomValue));

        private static readonly ProfilerMarker _PRF_ClampValue =
            new ProfilerMarker(_PRF_PFX + nameof(ClampValue));

        private static readonly ProfilerMarker _PRF_RangedValue =
            new ProfilerMarker(_PRF_PFX + nameof(RangedValue));

        #endregion
    }
}
