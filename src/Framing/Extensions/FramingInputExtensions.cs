using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;

namespace Appalachia.Utility.Framing.Extensions
{
    internal static class FramingInputExtensions
    {
        #region Profiling And Tracing Markers

        private const string _PRF_PFX = nameof(FramingInputExtensions) + ".";

        private static readonly ProfilerMarker _PRF_AverageForward =
            new ProfilerMarker(_PRF_PFX + nameof(AverageForward));

        private static readonly ProfilerMarker _PRF_AverageUp =
            new ProfilerMarker(_PRF_PFX + nameof(AverageUp));

        #endregion

        public static Vector3 AverageForward(this IReadOnlyList<FramingInput> array)
        {
            using (_PRF_AverageForward.Auto())
            {
                var sum = Vector3.zero;

                for (var i = 0; i < array.Count; i++)
                {
                    sum += array[i].forward;
                }

                return sum / array.Count;
            }
        }

        public static Vector3 AverageUp(this IReadOnlyList<FramingInput> array)
        {
            using (_PRF_AverageUp.Auto())
            {
                var sum = Vector3.zero;

                for (var i = 0; i < array.Count; i++)
                {
                    sum += array[i].up;
                }

                return sum / array.Count;
            }
        }
    }
}