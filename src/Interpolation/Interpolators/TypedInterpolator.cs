using System;
using Appalachia.Utility.Interpolation.Modes;
using UnityEngine;

namespace Appalachia.Utility.Interpolation.Interpolators
{
    [Serializable]
    public struct TypedInterpolator<TMode> : IInterpolator
        where TMode : struct, IInterpolationMode
    {
        public float start { get; set; }
        public float target { get; set; }
        public float value { get; set; }
        public float time { get; set; }

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

        public float Update()
        {
            return Interpolator.Update(ref this, Time.deltaTime, new TMode());
        }

        public float Update(float dt)
        {
            return Interpolator.Update(ref this, dt, new TMode());
        }

        public static implicit operator float(TypedInterpolator<TMode> i)
        {
            return i.value;
        }

        public override string ToString()
        {
            return value.ToString();
        }
    }
}
