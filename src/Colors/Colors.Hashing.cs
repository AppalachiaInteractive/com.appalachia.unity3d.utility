using System.Collections.Generic;
using System.Linq;
using Unity.Profiling;
using UnityEngine;

namespace Appalachia.Utility.Colors
{
    public static partial class Colors
    {
        #region Static Fields and Autoproperties

        private static Dictionary<string, Color> _hashedStrings;

        #endregion

        public static Color DeterministicColorFromString(
            string s,
            Vector2Int hueRange,
            Vector2Int saturationRange,
            Vector2Int valueRange)
        {
            _hashedStrings ??= new Dictionary<string, Color>();

            if (_hashedStrings.ContainsKey(s))
            {
                return _hashedStrings[s];
            }

            var upperString = s.ToUpperInvariant();

            var hueTime = GetStringHashTime(s);
            var saturationTime = GetStringHashTime(s.Reverse());
            var valueTime = GetStringHashTime(upperString.Reverse());

            var hue = hueRange.x + ((hueRange.y - hueRange.x) * hueTime);
            var saturation = saturationRange.x + ((saturationRange.y - saturationRange.x) * saturationTime);
            var value = valueRange.x + ((valueRange.y - valueRange.x) * valueTime);

            var result = HSVToRGB(hue, saturation, value, 1f, false);

            _hashedStrings.Add(s, result);

            return result;
        }

        private static float GetStringHashTime(IEnumerable<char> preconfiguredString)
        {
            using (_PRF_GetStringHashTime.Auto())
            {
                float time;

                var hash = preconfiguredString.GetHashCode();
                if (hash < 0)
                {
                    time = hash / (float)int.MinValue;
                }
                else
                {
                    time = hash / (float)int.MaxValue;
                }

                time = Mathf.Clamp01(time);
                return time;
            }
        }

        #region Profiling

        private static readonly ProfilerMarker _PRF_DeterministicColorFromString =
            new ProfilerMarker(_PRF_PFX + nameof(DeterministicColorFromString));

        private static readonly ProfilerMarker _PRF_GetStringHashTime =
            new ProfilerMarker(_PRF_PFX + nameof(GetStringHashTime));

        #endregion
    }
}
