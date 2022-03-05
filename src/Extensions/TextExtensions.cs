using System;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Profiling;

namespace Appalachia.Utility.Extensions
{
    [DebuggerStepThrough]
    public static class TextExtensions
    {
        #region Static Fields and Autoproperties

        private static Dictionary<int, string> _intLookup;
        private static Dictionary<float, string> _floatLookup;

        #endregion

        public static string ToStringNonAlloc(this float value, int decimalPoints)
        {
            using (_PRF_ToStringNonAlloc.Auto())
            {
                var rounded = (float)Math.Round(value, decimalPoints);
                _floatLookup ??= new Dictionary<float, string>();

                if (_floatLookup.TryGetValue(rounded, out var result)) return result;

                var newString = rounded.ToString();

                _floatLookup.Add(rounded, newString);

                return newString;
            }
        }

        public static string ToStringNonAlloc(this int value)
        {
            using (_PRF_ToStringNonAlloc.Auto())
            {
                _intLookup ??= new Dictionary<int, string>();

                if (_intLookup.TryGetValue(value, out var result)) return result;

                var newString = value.ToString();

                _intLookup.Add(value, newString);

                return newString;
            }
        }

        #region Profiling

        private const string _PRF_PFX = nameof(TextExtensions) + ".";

        private static readonly ProfilerMarker _PRF_ToStringNonAlloc =
            new ProfilerMarker(_PRF_PFX + nameof(ToStringNonAlloc));

        #endregion
    }
}
