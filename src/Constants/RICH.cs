using Appalachia.Utility.Colors;
using UnityEngine;

namespace Appalachia.Utility.Constants
{
    public static class RICH
    {
        #region Constants and Static Readonly

        private const string BOLD = "b";
        private const string COLOR = "color";
        private const string FONT_WEIGHT = "font-weight";
        private const string ITALIC = "i";
        private const string SIZE = "size";

        #endregion

        public static string Bold(this string value)
        {
            return FormatElement(BOLD, value);
        }

        public static string Color(this string value, Color color)
        {
            var hex = color.ToHexCode(Colors.Colors.HexCodeFormat.RichText);

            return FormatElementValue(COLOR, hex, value);
        }

        public static string Italic(this string value)
        {
            return FormatElement(ITALIC, value);
        }

        public static string Size(this string value, int size)
        {
            size = Mathf.Clamp(size, 1, 100);

            return FormatElementValue(SIZE, size, value);
        }

        public static string Size(this string value, float size)
        {
            size = Mathf.Clamp((int) size, 1, 100);

            return FormatElementValue(SIZE, size, value);
        }

        public static void SupportRichText(this GUIStyle style)
        {
            style.richText = true;
        }

        public static string Weight(this string value, FontWeight weight)
        {
            var weightInteger = (int) weight;

            return FormatElementValue(FONT_WEIGHT, weightInteger, value);
        }

        private static string FormatElement(string element, string value)
        {
            return $"<{element}>{value}</{element}>";
        }

        private static string FormatElementValue(string element, object elementValue, string value)
        {
            return $"<{element}={elementValue}>{value}</{element}>";
        }
    }
}