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
            bool includeNumberSign = true,
            bool includeAlpha = false)
        {
            var rPart = (int) (color.r * 255f);
            var gPart = (int) (color.g * 255f);
            var bPart = (int) (color.b * 255f);
            var aPart = (int) (color.a * 255f);

            var num = $"{(includeNumberSign ? "#" : "")}";
            var r = $"{rPart:X2}";
            var g = $"{gPart:X2}";
            var b = $"{bPart:X2}";
            var a = includeAlpha ? $"{aPart:X2}" : string.Empty;

            return $"{num}{a}{r}{g}{b}";
        }

        public static string ToHexCodeShort(this Color color)
        {
            return ToHexCode(color, false);
        }

        public static string ToHexCodeFull(this Color color)
        {
            return ToHexCode(color, true, true);
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
        public static Color FromHexCode(string hexCode)
        {
            if (hexCode == null)
            {
                throw new ArgumentNullException(nameof(hexCode));
            }

            if (_lookup == null)
            {
                _lookup = new Dictionary<string, Color>();
            }

            hexCode = hexCode.Replace("#", "").ToUpperInvariant().Trim();

            if ((hexCode.Length != 6) && (hexCode.Length != 8))
            {
                throw new ArgumentException($"{nameof(hexCode)} was not appropriate length.");
            }

            foreach (var character in hexCode)
            {
                if (!_hexChars.Contains(character))
                {
                    throw new ArgumentException(
                        $"{nameof(hexCode)} character [{character}] is not appropriate."
                    );
                }
            }

            if (_lookup.ContainsKey(hexCode))
            {
                return _lookup[hexCode];
            }

            var clean = new char[8];
            for (var i = 0; i < 8; i++)
            {
                clean[i] = i < 2 ? 'F' : '0';
            }

            var offset = 0;

            if (hexCode.Length == 6)
            {
                offset = 2;
            }

            for (var i = 0; (i + offset) < clean.Length; i++)
            {
                var character = hexCode[i];
                clean[i + offset] = character;
            }

            var alphaPart = $"{clean[0]}{clean[1]}";
            var redPart = $"{clean[2]}{clean[3]}";
            var greenPart = $"{clean[4]}{clean[5]}";
            var bluePart = $"{clean[6]}{clean[7]}";

            var alpha = int.Parse(alphaPart, NumberStyles.HexNumber);
            var red = int.Parse(redPart,     NumberStyles.HexNumber);
            var green = int.Parse(greenPart, NumberStyles.HexNumber);
            var blue = int.Parse(bluePart,   NumberStyles.HexNumber);

            var result = new Color(red / 255f, green / 255f, blue / 255f, alpha / 255f);

            _lookup.Add(hexCode, result);

            return result;
        }
    }
}
