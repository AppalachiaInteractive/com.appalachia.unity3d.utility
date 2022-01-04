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
        public static Color ColorFromHex([NotNull] this string hexCode, float alpha)
        {
            using (_PRF_ColorFromHex.Auto())
            {
                return FromHexCode(hexCode, false).UpdateAlpha(alpha);
            }
        }

        /// <inheritdoc cref="FromHexCode" />
        public static Color ColorFromHex([NotNull] this string hexCode, bool alphaLast = true)
        {
            using (_PRF_ColorFromHex.Auto())
            {
                return FromHexCode(hexCode, alphaLast);
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
        public static Color FromHexCode(string hexCode, bool alphaLast = true)
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

                var threePart = cleanHexCode.Length == 6;
                var fourPart = cleanHexCode.Length == 8;

                if (!(threePart || fourPart))
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

                var rawPart1 = ZString.Format("{0}{1}", cleanHexCode[0], cleanHexCode[1]);
                var parsedPart1 = int.Parse(rawPart1, NumberStyles.HexNumber);
                var floatPart1 = parsedPart1 / 255f;

                var rawPart2 = ZString.Format("{0}{1}", cleanHexCode[2], cleanHexCode[3]);
                var parsedPart2 = int.Parse(rawPart2, NumberStyles.HexNumber);
                var floatPart2 = parsedPart2 / 255f;

                var rawPart3 = ZString.Format("{0}{1}", cleanHexCode[4], cleanHexCode[5]);
                var parsedPart3 = int.Parse(rawPart3, NumberStyles.HexNumber);
                var floatPart3 = parsedPart3 / 255f;

                Color result;

                if (fourPart)
                {
                    var rawPart4 = ZString.Format("{0}{1}", cleanHexCode[6], cleanHexCode[7]);
                    var parsedPart4 = int.Parse(rawPart4, NumberStyles.HexNumber);
                    var floatPart4 = parsedPart4 / 255f;

                    if (!alphaLast)
                    {
                        (floatPart4, floatPart1) = (floatPart1, floatPart4);
                    }

                    result = new Color(floatPart1, floatPart2, floatPart3, floatPart4);
                }
                else
                {
                    result = new Color(floatPart1, floatPart2, floatPart3);
                }

                _lookup.Add(hexCode, result);

                if (hexCode != cleanHexCode)
                {
                    _lookup.Add(cleanHexCode, result);
                }

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
