using System;
using System.Collections.Generic;
using System.Globalization;
using Appalachia.Utility.Strings;
using JetBrains.Annotations;
using Unity.Profiling;
using UnityEngine;

namespace Appalachia.Utility.Colors
{
    public static partial class Colors
    {
        #region Constants and Static Readonly

        private static readonly HashSet<char> _hexChars = new(
            new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' }
        );

        #endregion

        #region Static Fields and Autoproperties

        private static Dictionary<string, Color> _lookup = new();

        #endregion

        public static void ClearHexLookup()
        {
            using (_PRF_ClearHexLookup.Auto())
            {
                _lookup.Clear();
            }
        }

        /// <inheritdoc cref="FromHexCode" />
        public static Color ColorFromHex([NotNull] this string hexCode)
        {
            using (_PRF_ColorFromHex.Auto())
            {
                return FromHexCode(hexCode);
            }
        }

        /// <summary>
        ///     Parses the following formats:
        ///     rrggbb
        ///     RRGGBB
        ///     #RRGGBB
        ///     aarrggbb
        ///     AARRGGBB
        ///     #AARRGGBB
        /// </summary>
        /// <param name="hexCode">The hexadecimal code.</param>
        /// <exception cref="ArgumentException">The argument was not appropriate.</exception>
        /// <exception cref="ArgumentNullException">The argument was null.</exception>
        /// <returns>The color that the code represents.</returns>
        public static Color FromHexCode(string hexCode, bool alphaLast = false)
        {
            using (_PRF_FromHexCode.Auto())
            {
                if (hexCode == null)
                {
                    throw new ArgumentNullException(nameof(hexCode));
                }

                _lookup ??= new Dictionary<string, Color>();

                if (_lookup.ContainsKey(hexCode))
                {
                    return _lookup[hexCode];
                }

                var cleanHexCode = hexCode.Replace("#", "").ToUpperInvariant().Trim();

                if ((cleanHexCode.Length != 6) && (cleanHexCode.Length != 8))
                {
                    throw new ArgumentException(
                        ZString.Format("{0} was not appropriate length.", nameof(hexCode))
                    );
                }

                foreach (var character in cleanHexCode)
                {
                    if (!_hexChars.Contains(character))
                    {
                        throw new ArgumentException(
                            ZString.Format(
                                "{0} character [{1}] is not appropriate.",
                                nameof(hexCode),
                                character
                            )
                        );
                    }
                }

                var part1 = ZString.Format("{0}{1}", cleanHexCode[0], cleanHexCode[1]);
                var part2 = ZString.Format("{0}{1}", cleanHexCode[2], cleanHexCode[3]);
                var part3 = ZString.Format("{0}{1}", cleanHexCode[4], cleanHexCode[5]);
                var part4 = ZString.Format("{0}{1}", cleanHexCode[6], cleanHexCode[7]);

                var int1 = int.Parse(part1, NumberStyles.HexNumber);
                var int2 = int.Parse(part2, NumberStyles.HexNumber);
                var int3 = int.Parse(part3, NumberStyles.HexNumber);
                var int4 = int.Parse(part4, NumberStyles.HexNumber);

                var r = int1 / 255f;
                var g = int2 / 255f;
                var b = int3 / 255f;
                var a = int4 / 255f;

                if (!alphaLast)
                {
                    var temp = a;
                    a = r;
                    r = temp;
                }

                var result = new Color(r, g, b, a);

                _lookup.Add(hexCode,      result);
                _lookup.Add(cleanHexCode, result);

                return result;
            }
        }

        public static string ToHexCode(this Color color, HexCodeFormat format)
        {
            using (_PRF_ToHexCode.Auto())
            {
                var includeNumberSign = (format | HexCodeFormat.IncludeNumberSign) == format;
                var includeAlpha = (format | HexCodeFormat.IncludeAlpha) == format;
                var alphaFirst = (format | HexCodeFormat.AlphaFirst) == format;

                var rPart = (int)(color.r * 255f);
                var gPart = (int)(color.g * 255f);
                var bPart = (int)(color.b * 255f);
                var aPart = (int)(color.a * 255f);

                var num = ZString.Format("{0}",  includeNumberSign ? "#" : "");
                var r = ZString.Format("{0:X2}", rPart);
                var g = ZString.Format("{0:X2}", gPart);
                var b = ZString.Format("{0:X2}", bPart);
                var a = includeAlpha ? ZString.Format("{0:X2}", aPart) : string.Empty;

                return alphaFirst
                    ? ZString.Format("{0}{1}{2}{3}{4}", num, a, r, g, b)
                    : ZString.Format("{0}{1}{2}{3}{4}", num, r, g, b, a);
            }
        }

        public static string ToHexCodeFull(this Color color)
        {
            using (_PRF_ToHexCodeFull.Auto())
            {
                return ToHexCode(
                    color,
                    HexCodeFormat.IncludeNumberSign | HexCodeFormat.IncludeAlpha | HexCodeFormat.AlphaFirst
                );
            }
        }

        public static string ToHexCodeShort(this Color color)
        {
            using (_PRF_ToHexCodeShort.Auto())
            {
                return ToHexCode(color, HexCodeFormat.Default);
            }
        }

        #region Profiling

        private const string _PRF_PFX = nameof(Colors) + ".";

        private static readonly ProfilerMarker _PRF_ClearHexLookup =
            new ProfilerMarker(_PRF_PFX + nameof(ClearHexLookup));

        private static readonly ProfilerMarker _PRF_ColorFromHex =
            new ProfilerMarker(_PRF_PFX + nameof(ColorFromHex));

        private static readonly ProfilerMarker _PRF_FromHexCode =
            new ProfilerMarker(_PRF_PFX + nameof(FromHexCode));

        private static readonly ProfilerMarker _PRF_ToHexCode =
            new ProfilerMarker(_PRF_PFX + nameof(ToHexCode));

        private static readonly ProfilerMarker _PRF_ToHexCodeFull =
            new ProfilerMarker(_PRF_PFX + nameof(ToHexCodeFull));

        private static readonly ProfilerMarker _PRF_ToHexCodeShort =
            new ProfilerMarker(_PRF_PFX + nameof(ToHexCodeShort));

        #endregion
    }
}
