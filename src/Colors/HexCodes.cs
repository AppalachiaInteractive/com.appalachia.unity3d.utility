using System;
using System.Collections.Generic;
using System.Globalization;
using JetBrains.Annotations;
using UnityEngine;

namespace Appalachia.Utility.Colors
{
    public static partial class Colors
    {
        private static Dictionary<string, Color> _lookup = new();

        private static readonly HashSet<char> _hexChars = new(new[]
        {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F'
        });

        public static void ClearHexLookup()
        {
            _lookup.Clear();
        }

        public static string ToHexCode(
            this Color color,
            HexCodeFormat format)
        {
            var includeNumberSign = (format | HexCodeFormat.IncludeNumberSign) == format;
            var includeAlpha = (format | HexCodeFormat.IncludeAlpha) == format;
            var alphaFirst = (format | HexCodeFormat.AlphaFirst) == format;
                
            var rPart = (int) (color.r * 255f);
            var gPart = (int) (color.g * 255f);
            var bPart = (int) (color.b * 255f);
            var aPart = (int) (color.a * 255f);

            var num = $"{(includeNumberSign ? "#" : "")}";
            var r = $"{rPart:X2}";
            var g = $"{gPart:X2}";
            var b = $"{bPart:X2}";
            var a = includeAlpha ? $"{aPart:X2}" : string.Empty;

            return alphaFirst ? $"{num}{a}{r}{g}{b}" : $"{num}{r}{g}{b}{a}";
        }

        public static string ToHexCodeShort(this Color color)
        {
            return ToHexCode(color, HexCodeFormat.Default);
        }

        public static string ToHexCodeFull(this Color color)
        {
            return ToHexCode(
                color,
                HexCodeFormat.IncludeNumberSign | HexCodeFormat.IncludeAlpha | HexCodeFormat.AlphaFirst
            );
        }

        /// <inheritdoc cref="FromHexCode" />
        public static Color ColorFromHex([NotNull] this string hexCode)
        {
            return FromHexCode(hexCode);
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
                throw new ArgumentException($"{nameof(hexCode)} was not appropriate length.");
            }

            foreach (var character in cleanHexCode)
            {
                if (!_hexChars.Contains(character))
                {
                    throw new ArgumentException(
                        $"{nameof(hexCode)} character [{character}] is not appropriate."
                    );
                }
            }

            var part1 = $"{cleanHexCode[0]}{cleanHexCode[1]}";
            var part2 = $"{cleanHexCode[2]}{cleanHexCode[3]}";
            var part3 = $"{cleanHexCode[4]}{cleanHexCode[5]}";
            var part4 = $"{cleanHexCode[6]}{cleanHexCode[7]}";

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
}
