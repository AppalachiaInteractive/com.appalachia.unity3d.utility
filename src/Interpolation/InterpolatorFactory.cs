using System;
using Appalachia.Utility.Interpolation.Modes;
using UnityEngine;

namespace Appalachia.Utility.Interpolation
{
    public static class InterpolatorFactory
    {
        #region Constants and Static Readonly

        private static readonly IInterpolationMode[] interpolations;

        #endregion

        static InterpolatorFactory()
        {
            interpolations = new IInterpolationMode[]
            {
                new Linear(),
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

        public static float EaseInCubic(float min, float max, float percentage)
        {
            return ((max - min) * percentage * percentage * percentage) + min;
        }

        public static float EaseInOutSine(float min, float max, float percentage)
        {
            return ((max - min) * 0.5f * (1f - Mathf.Cos(Mathf.PI * percentage))) + min;
        }

        public static float EaseInQuad(float min, float max, float percentage)
        {
            return ((max - min) * percentage * percentage) + min;
        }

        public static float EaseInSine(float min, float max, float percentage)
        {
            return (-(max - min) * Mathf.Cos(Mathf.PI * 0.5f * percentage)) + (max - min) + min;
        }

        public static float EaseOutCubic(float min, float max, float percentage)
        {
            return ((max - min) * (((percentage - 1) * (percentage - 1) * (percentage - 1)) + 1f)) + min;
        }

        public static float EaseOutQuad(float min, float max, float percentage)
        {
            return (-(max - min) * percentage * (percentage - 2)) + min;
        }

        public static float EaseOutSine(float min, float max, float percentage)
        {
            return ((max - min) * Mathf.Sin(Mathf.PI * 0.5f * percentage)) + min;
        }

        public static IInterpolationMode GetInterpolator(InterpolationMode i)
        {
            return interpolations[(int)i];
        }

        public static IInterpolationMode GetInterpolator(string name)
        {
            Enum.TryParse(name, out InterpolationMode i);
            return GetInterpolator(i);
        }

        public static float Linear(float min, float max, float percentage)
        {
            return ((max - min) * percentage) + min;
        }

        public static float LinearAngle(float min, float max, float percentage)
        {
            return Mathf.LerpAngle(min, max, percentage);
        }

        public static float SmoothStep(float min, float max, float percentage)
        {
            var u = (-2f * percentage * percentage * percentage) + (3f * percentage * percentage);
            return (max * u) + (min * (1f - u));
        }
    }
}
