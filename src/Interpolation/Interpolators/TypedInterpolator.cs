using System;
using System.Diagnostics;
using Appalachia.Utility.Interpolation.Modes;
using Appalachia.Utility.Timing;
using Unity.Profiling;
using UnityEngine;

namespace Appalachia.Utility.Interpolation.Interpolators
{
    [Serializable]
    public struct TypedInterpolator<TMode> : IInterpolator
        where TMode : struct, IInterpolationMode
    {
        [DebuggerStepThrough]
        public static implicit operator float(TypedInterpolator<TMode> i)
        {
            using (_PRF_op_Implicit.Auto())
            {
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

        public float Update()
        {
            using (_PRF_Update.Auto())
            {
                var m = new TMode();
                mode = m.mode;
                return Interpolator.Update(ref this, CoreClock.Instance.DeltaTime, m);
            }
        }

        public float Update(float dt)
        {
            using (_PRF_Update.Auto())
            {
                var m = new TMode();
                mode = m.mode;
                return Interpolator.Update(ref this, dt, m);
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

        private const string _PRF_PFX = nameof(TypedInterpolator<TMode>) + ".";

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
