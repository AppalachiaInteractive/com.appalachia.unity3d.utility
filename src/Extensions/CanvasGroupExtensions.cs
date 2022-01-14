using Unity.Profiling;
using UnityEngine;

namespace Appalachia.Utility.Extensions
{
    public static class CanvasGroupExtensions
    {
        public static bool IsHidden(this CanvasGroup cg, float threshold = 0.01f)
        {
            using (_PRF_IsHidden.Auto())
            {
                return cg.alpha < threshold;
            }
        }

        public static bool IsVisible(this CanvasGroup cg, float threshold = 0.01f)
        {
            using (_PRF_IsVisible.Auto())
            {
                return cg.alpha >= threshold;
            }
        }

        #region Profiling

        private const string _PRF_PFX = nameof(CanvasGroupExtensions) + ".";

        private static readonly ProfilerMarker _PRF_IsVisible =
            new ProfilerMarker(_PRF_PFX + nameof(IsVisible));

        private static readonly ProfilerMarker _PRF_IsHidden =
            new ProfilerMarker(_PRF_PFX + nameof(IsHidden));

        #endregion
    }
}
