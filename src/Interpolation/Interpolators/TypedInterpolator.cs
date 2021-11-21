using System;
using System.Diagnostics;
using Appalachia.Utility.Interpolation.Modes;
using UnityEngine;

namespace Appalachia.Utility.Interpolation.Interpolators
{
    [Serializable]
    public struct TypedInterpolator<TMode> : IInterpolator
        where TMode : struct, IInterpolationMode
    {
        public float min { get; set; }
        public float max { get; set; }
        public float current { get; set; }
        public float percentage { get; set; }

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

        public float Update()
        {
            return Interpolator.Update(ref this, Time.deltaTime, new TMode());
        }

        public float Update(float dt)
        {
            return Interpolator.Update(ref this, dt, new TMode());
        }

        [DebuggerStepThrough] public static implicit operator float(TypedInterpolator<TMode> i)
        {
            return i.current;
        }

        [DebuggerStepThrough] public override string ToString()
        {
            return current.ToString();
        }
    }
}
