using System;
using System.Diagnostics;
using Appalachia.Utility.Interpolation.Modes;
using Appalachia.Utility.Timing;
using Unity.Profiling;
using UnityEngine;

namespace Appalachia.Utility.Interpolation.Interpolators
{
    [Serializable]
    public struct Interpolator : IInterpolator
    {
        [DebuggerStepThrough]
        public static implicit operator float(Interpolator i)
        {
            using (_PRF_op_Implicit.Auto())
            {
                return i.current;
            }
        }

        public static float Update<TInterpolation, TMode>(ref TInterpolation i, float dt, TMode e)
            where TInterpolation : struct, IInterpolator
            where TMode : IInterpolationMode
        {
            using (_PRF_Update.Auto())
            {
                i.mode = e.mode;
                i.percentage = Mathf.Clamp01(i.percentage + dt);
                i.current = e.Interpolate(i);
                return i.current;
            }
        }

        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            using (_PRF_ToString.Auto())
            {
                return current.ToString();
            }
        }

        public void Reset(float v)
        {
            using (_PRF_Reset.Auto())
            {
                min = v;
                max = v;
                current = v;
                percentage = 0f;
            }
        }

        public void Target(float v)
        {
            using (_PRF_Target.Auto())
            {
                if (!Mathf.Approximately(max, v))
                {
                    min = current;
                    max = v;
                    percentage = 0f;
                }
            }
        }

        public float Update<E>()
            where E : struct, IInterpolationMode
        {
            using (_PRF_Update.Auto())
            {
                return Update(CoreClock.Instance.DeltaTime, new E());
            }
        }

        public float Update<E>(float dt)
            where E : struct, IInterpolationMode
        {
            using (_PRF_Update.Auto())
            {
                return Update(dt, new E());
            }
        }

        public float Update<E>(float dt, E e)
            where E : struct, IInterpolationMode
        {
            using (_PRF_Update.Auto())
            {
                return Update(ref this, dt, e);
            }
        }

        public float Update(float dt, InterpolationMode i)
        {
            using (_PRF_Update.Auto())
            {
                mode = i;

                return Update(ref this, dt, InterpolatorFactory.GetInterpolator(i));
            }
        }

        #region IInterpolator Members

        public float min { get; set; }
        public float max { get; set; }
        public float current { get; set; }
        public float percentage { get; set; }

        public InterpolationMode mode { get; set; }

        #endregion

        #region Profiling

        private const string _PRF_PFX = nameof(Interpolator) + ".";

        private static readonly ProfilerMarker _PRF_Target = new ProfilerMarker(_PRF_PFX + nameof(Target));
        private static readonly ProfilerMarker _PRF_Reset = new ProfilerMarker(_PRF_PFX + nameof(Reset));
        private static readonly ProfilerMarker _PRF_Update = new ProfilerMarker(_PRF_PFX + nameof(Update));

        private static readonly ProfilerMarker
            _PRF_ToString = new ProfilerMarker(_PRF_PFX + nameof(ToString));

        private static readonly ProfilerMarker _PRF_op_Implicit =
            new ProfilerMarker(_PRF_PFX + "op_Implicit");

        #endregion
    }
}
