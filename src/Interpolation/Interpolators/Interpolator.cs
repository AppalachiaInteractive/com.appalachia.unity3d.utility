using System;
using Appalachia.Utility.Interpolation.Modes;
using UnityEngine;

namespace Appalachia.Utility.Interpolation.Interpolators
{
    [Serializable]
    public struct Interpolator : IInterpolator
    {
        public float start { get; set; }
        public float target { get; set; }
        public float value { get; set; }
        public float time { get; set; }

        public static float Update<TInterpolation, TMode>(ref TInterpolation i, float dt, TMode e)
            where TInterpolation : struct, IInterpolator
            where TMode : IInterpolationMode
        {
            i.time = Mathf.Clamp01(i.time + dt);
            i.value = e.Interpolate(i);
            return i.value;
        }

        public void Target(float v)
        {
            if (!Mathf.Approximately(target, v))
            {
                start = value;
                target = v;
                time = 0f;
            }
        }

        public void Reset(float v)
        {
            start = v;
            target = v;
            value = v;
            time = 0f;
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
            return Update(ref this, dt, InterpolationFactory.GetInterpolation(i));
        }

        public static implicit operator float(Interpolator i)
        {
            return i.value;
        }

        public override string ToString()
        {
            return value.ToString();
        }
    }
}
