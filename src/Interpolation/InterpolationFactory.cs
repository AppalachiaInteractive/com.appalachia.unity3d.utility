using System;
using Appalachia.Utility.Interpolation.Modes;
using UnityEngine;

namespace Appalachia.Utility.Interpolation
{
    public static class InterpolationFactory
    {
        private static readonly IInterpolationMode[] interpolations;

        static InterpolationFactory()
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

        public static IInterpolationMode GetInterpolation(InterpolationMode i)
        {
            return interpolations[(int) i];
        }

        public static IInterpolationMode GetInterpolation(string name)
        {
            Enum.TryParse(name, out InterpolationMode i);
            return GetInterpolation(i);
        }

        public static float Linear(float v0, float v1, float t)
        {
            return ((v1 - v0) * t) + v0;
        }

        public static float LinearAngle(float v0, float v1, float t)
        {
            return Mathf.LerpAngle(v0, v1, t);
        }

        public static float SmoothStep(float v0, float v1, float t)
        {
            var u = (-2f * t * t * t) + (3f * t * t);
            return (v1 * u) + (v0 * (1f - u));
        }

        public static float EaseInQuad(float v0, float v1, float t)
        {
            return ((v1 - v0) * t * t) + v0;
        }

        public static float EaseOutQuad(float v0, float v1, float t)
        {
            return (-(v1 - v0) * t * (t - 2)) + v0;
        }

        public static float EaseInCubic(float v0, float v1, float t)
        {
            return ((v1 - v0) * t * t * t) + v0;
        }

        public static float EaseOutCubic(float v0, float v1, float t)
        {
            return ((v1 - v0) * (((t - 1) * (t - 1) * (t - 1)) + 1f)) + v0;
        }

        public static float EaseInSine(float v0, float v1, float t)
        {
            return (-(v1 - v0) * Mathf.Cos(Mathf.PI * 0.5f * t)) + (v1 - v0) + v0;
        }

        public static float EaseOutSine(float v0, float v1, float t)
        {
            return ((v1 - v0) * Mathf.Sin(Mathf.PI * 0.5f * t)) + v0;
        }

        public static float EaseInOutSine(float v0, float v1, float t)
        {
            return ((v1 - v0) * 0.5f * (1f - Mathf.Cos(Mathf.PI * t))) + v0;
        }
    }
}
