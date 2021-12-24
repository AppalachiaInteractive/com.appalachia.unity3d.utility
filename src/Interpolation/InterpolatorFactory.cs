using System;
using Appalachia.Utility.Interpolation.Modes;
using Unity.Profiling;
using UnityEngine;

namespace Appalachia.Utility.Interpolation
{
#if UNITY_EDITOR
    [UnityEditor.InitializeOnLoad]
#endif
    public static class InterpolatorFactory
    {
        #region Constants and Static Readonly

        private static readonly IInterpolationMode[] interpolations;

        #endregion

        static InterpolatorFactory()
        {
            using (_PRF_InterpolatorFactory.Auto())
            {
                interpolations = new IInterpolationMode[]
                {
                    new Linear(),
                    new LinearAngle(),
                    new SmoothStep(),
                    new EaseInQuad(),
                    new EaseOutQuad(),
                    new EaseInCubic(),
                    new EaseOutCubic(),
                    new EaseInSine(),
                    new EaseOutSine(),
                    new EaseInOutSine()
                };
            }
        }

        public static float EaseInCubic(float min, float max, float percentage)
        {
            using (_PRF_EaseInCubic.Auto())
            {
                return ((max - min) * percentage * percentage * percentage) + min;
            }
        }

        public static float EaseInOutSine(float min, float max, float percentage)
        {
            using (_PRF_EaseInOutSine.Auto())
            {
                return ((max - min) * 0.5f * (1f - Mathf.Cos(Mathf.PI * percentage))) + min;
            }
        }

        public static float EaseInQuad(float min, float max, float percentage)
        {
            using (_PRF_EaseInQuad.Auto())
            {
                return ((max - min) * percentage * percentage) + min;
            }
        }

        public static float EaseInSine(float min, float max, float percentage)
        {
            using (_PRF_EaseInSine.Auto())
            {
                return (-(max - min) * Mathf.Cos(Mathf.PI * 0.5f * percentage)) + (max - min) + min;
            }
        }

        public static float EaseOutCubic(float min, float max, float percentage)
        {
            using (_PRF_EaseOutCubic.Auto())
            {
                return ((max - min) * (((percentage - 1) * (percentage - 1) * (percentage - 1)) + 1f)) + min;
            }
        }

        public static float EaseOutQuad(float min, float max, float percentage)
        {
            using (_PRF_EaseOutQuad.Auto())
            {
                return (-(max - min) * percentage * (percentage - 2)) + min;
            }
        }

        public static float EaseOutSine(float min, float max, float percentage)
        {
            using (_PRF_EaseOutSine.Auto())
            {
                return ((max - min) * Mathf.Sin(Mathf.PI * 0.5f * percentage)) + min;
            }
        }

        public static IInterpolationMode GetInterpolator(InterpolationMode i)
        {
            using (_PRF_GetInterpolator.Auto())
            {
                return interpolations[(int)i];
            }
        }

        public static IInterpolationMode GetInterpolator(string name)
        {
            using (_PRF_GetInterpolator.Auto())
            {
                Enum.TryParse(name, out InterpolationMode i);
                return GetInterpolator(i);
            }
        }

        public static float Linear(float min, float max, float percentage)
        {
            using (_PRF_Linear.Auto())
            {
                return ((max - min) * percentage) + min;
            }
        }

        public static float LinearAngle(float min, float max, float percentage)
        {
            using (_PRF_LinearAngle.Auto())
            {
                return Mathf.LerpAngle(min, max, percentage);
            }
        }

        public static float SmoothStep(float min, float max, float percentage)
        {
            using (_PRF_SmoothStep.Auto())
            {
                var u = (-2f * percentage * percentage * percentage) + (3f * percentage * percentage);
                return (max * u) + (min * (1f - u));
            }
        }

        #region Profiling

        private const string _PRF_PFX = nameof(InterpolatorFactory) + ".";

        private static readonly ProfilerMarker _PRF_InterpolatorFactory =
            new ProfilerMarker(_PRF_PFX + nameof(InterpolatorFactory));

        private static readonly ProfilerMarker _PRF_EaseInCubic =
            new ProfilerMarker(_PRF_PFX + nameof(EaseInCubic));

        private static readonly ProfilerMarker _PRF_EaseInOutSine =
            new ProfilerMarker(_PRF_PFX + nameof(EaseInOutSine));

        private static readonly ProfilerMarker _PRF_EaseInSine =
            new ProfilerMarker(_PRF_PFX + nameof(EaseInSine));

        private static readonly ProfilerMarker _PRF_EaseInQuad =
            new ProfilerMarker(_PRF_PFX + nameof(EaseInQuad));

        private static readonly ProfilerMarker _PRF_EaseOutCubic =
            new ProfilerMarker(_PRF_PFX + nameof(EaseOutCubic));

        private static readonly ProfilerMarker _PRF_EaseOutQuad =
            new ProfilerMarker(_PRF_PFX + nameof(EaseOutQuad));

        private static readonly ProfilerMarker _PRF_EaseOutSine =
            new ProfilerMarker(_PRF_PFX + nameof(EaseOutSine));

        private static readonly ProfilerMarker _PRF_GetInterpolator =
            new ProfilerMarker(_PRF_PFX + nameof(GetInterpolator));

        private static readonly ProfilerMarker _PRF_Linear = new ProfilerMarker(_PRF_PFX + nameof(Linear));

        private static readonly ProfilerMarker _PRF_SmoothStep =
            new ProfilerMarker(_PRF_PFX + nameof(SmoothStep));

        private static readonly ProfilerMarker _PRF_LinearAngle =
            new ProfilerMarker(_PRF_PFX + nameof(LinearAngle));

        #endregion
    }
}
