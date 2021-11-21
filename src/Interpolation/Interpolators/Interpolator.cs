using System;
using System.Diagnostics;
using Appalachia.Utility.Interpolation.Modes;
using UnityEngine;

namespace Appalachia.Utility.Interpolation.Interpolators
{
    [Serializable]
    public struct Interpolator : IInterpolator
    {
        public float min { get; set; }
        public float max { get; set; }
        public float current { get; set; }
        public float percentage { get; set; }

        public static float Update<TInterpolation, TMode>(ref TInterpolation i, float dt, TMode e)
            where TInterpolation : struct, IInterpolator
            where TMode : IInterpolationMode
        {
            i.percentage = Mathf.Clamp01(i.percentage + dt);
            i.current = e.Interpolate(i);
            return i.current;
        }

        public void Target(float v)
        {
            if (!Mathf.Approximately(max, v))
            {
                min = current;
                max = v;
                percentage = 0f;
            }
        }

        public void Reset(float v)
        {
            min = v;
            max = v;
            current = v;
            percentage = 0f;
        }

        public float Update<E>()
            where E : struct, IInterpolationMode
        {
            return Update(Time.deltaTime, new E());
        }

        public float Update<E>(float dt)
            where E : struct, IInterpolationMode
        {
            return Update(dt, new E());
        }

        public float Update<E>(float dt, E e)
            where E : struct, IInterpolationMode
        {
            return Update(ref this, dt, e);
        }

        public float Update(float dt, InterpolationMode i)
        {
            return Update(ref this, dt, InterpolatorFactory.GetInterpolator(i));
        }

        [DebuggerStepThrough] public static implicit operator float(Interpolator i)
        {
            return i.current;
        }

        [DebuggerStepThrough] public override string ToString()
        {
            return current.ToString();
        }
    }
}
