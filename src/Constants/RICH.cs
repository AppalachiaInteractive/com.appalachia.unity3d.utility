using System;
using Appalachia.Utility.Colors;
using Appalachia.Utility.Strings;
using Unity.Profiling;
using UnityEngine;

// ReSharper disable FormatStringProblem

namespace Appalachia.Utility.Constants
{
    public static class RICH
    {
        #region Constants and Static Readonly

        public static readonly Color Black = new(023f / 255f, 022f / 255f, 022f / 255f, 1.0f);
        public static readonly Color DarkBrown = new(046f / 255f, 041f / 255f, 034f / 255f, 1.0f);
        public static readonly Color Gray = new(103f / 255f, 096f / 255f, 081f / 255f, 1.0f);
        public static readonly Color Tan = new(175f / 255f, 165f / 255f, 143f / 255f, 1.0f);
        public static readonly Color Bone = new(219f / 255f, 214f / 255f, 203f / 255f, 1.0f);
        public static readonly Color Cream = new(222f / 255f, 208f / 255f, 183f / 255f, 1.0f);
        public static readonly Color LightYellow = new(229f / 255f, 204f / 255f, 159f / 255f, 1.0f);
        public static readonly Color Yellow = new(217f / 255f, 183f / 255f, 118f / 255f, 1.0f);
        public static readonly Color RichYellow = new(211f / 255f, 170f / 255f, 095f / 255f, 1.0f);
        public static readonly Color DarkYellow = new(202f / 255f, 154f / 255f, 064f / 255f, 1.0f);
        public static readonly Color Red = new(128f / 255f, 062f / 255f, 040f / 255f, 1.0f);
        public static readonly Color Green = new(089f / 255f, 113f / 255f, 080f / 255f, 1.0f);
        public static readonly Color Blue = new(109f / 255f, 127f / 255f, 130f / 255f, 1.0f);
        public static readonly Color Normal = new(127f / 255f, 127f / 255f, 255f / 255f, 1.0f);
        public static readonly Color Orange = new(022f / 255f, 095f / 255f, 050f / 255f, 1.0f);

        private static readonly Color LogName = Green;
        private static readonly Color LogInt = new(068f / 255f, 137f / 255f, 240f / 255f, 1.0f);
        private static readonly Color LogType = new(228f / 255f, 168f / 255f, 064f / 255f, 1.0f);
        private static readonly Color LogMethod = new(174f / 255f, 084f / 255f, 047f / 255f, 1.0f);
        private static readonly Color LogField = new(127f / 255f, 163f / 255f, 155f / 255f, 1.0f);
        private static readonly Color LogEvent = new(071f / 255f, 119f / 255f, 198f / 255f, 1.0f);
        private static readonly Color LogEnum = new(135f / 255f, 126f / 255f, 200f / 255f, 1.0f);

        private static readonly Utf16PreparedFormat<int, string> FONT_WEIGHT =
            ZString.PrepareUtf16<int, string>("<font-weight={0}>{1}</font-weight>");

        private static readonly Utf16PreparedFormat<int, string> SIZE =
            ZString.PrepareUtf16<int, string>("<size={0}>{1}</size>");

        private static readonly Utf16PreparedFormat<string, string> COLOR =
            ZString.PrepareUtf16<string, string>("<color={0}>{1}</color>");

        private static readonly Utf16PreparedFormat<string> BOLD = ZString.PrepareUtf16<string>("<b>{0}</b>");

        private static readonly Utf16PreparedFormat<string> ITALIC =
            ZString.PrepareUtf16<string>("<i>{0}</i>");

        #endregion

        public static string Bold(this string value)
        {
            using (_PRF_Bold.Auto())
            {
                return BOLD.Format(value);
            }
        }

        public static string Color(this string value, Color color)
        {
            using (_PRF_Color.Auto())
            {
                var hex = color.ToHexCode(HexCodeFormat.RichText);

                return COLOR.Format(hex, value);
            }
        }

        public static string FormatForLogging(this Type value)
        {
            using (_PRF_FormatNameForLogging.Auto())
            {
                return Color(Bold(value.Name), LogType);
            }
        }

        public static string FormatNameForLogging(this string value)
        {
            using (_PRF_FormatNameForLogging.Auto())
            {
                return Color(Bold(value), LogName);
            }
        }

        public static string FormatForLogging(this int value)
        {
            using (_PRF_FormatNameForLogging.Auto())
            {
                return Color(Bold(value.ToString()), LogInt);
            }
        }

        public static string FormatForLogging(this object value)
        {
            using (_PRF_FormatNameForLogging.Auto())
            {
                return Color(Bold(value.ToString()), LogName);
            }
        }

        public static string FormatFieldForLogging(this string value)
        {
            using (_PRF_FormatMethodForLogging.Auto())
            {
                return Color(Bold(value), LogField);
            }
        }

        public static string FormatMethodForLogging(this string value)
        {
            using (_PRF_FormatMethodForLogging.Auto())
            {
                return Color(Bold(value), LogMethod);
            }
        }

        public static string FormatForLogging<T>(this T value)
            where T : Enum
        {
            using (_PRF_FormatMethodForLogging.Auto())
            {
                return Color(Bold(value.ToString()), LogEnum);
            }
        }

        public static string FormatEventForLogging(this string value)
        {
            using (_PRF_FormatMethodForLogging.Auto())
            {
                return Color(Bold(value), LogEvent);
            }
        }

        public static string Italic(this string value)
        {
            using (_PRF_Italic.Auto())
            {
                return ITALIC.Format(value);
            }
        }

        public static string Size(this string value, int size)
        {
            using (_PRF_Size.Auto())
            {
                size = Mathf.Clamp(size, 1, 100);

                return SIZE.Format(size, value);
            }
        }

        public static string Size(this string value, float size)
        {
            using (_PRF_Size.Auto())
            {
                var intSize = Mathf.Clamp((int)size, 1, 100);

                return SIZE.Format(intSize, value);
            }
        }

        public static void SupportRichText(this GUIStyle style)
        {
            using (_PRF_SupportRichText.Auto())
            {
                style.richText = true;
            }
        }

        public static string Weight(this string value, FontWeight weight)
        {
            using (_PRF_Weight.Auto())
            {
                var weightInteger = (int)weight;

                return FONT_WEIGHT.Format(weightInteger, value);
            }
        }

        #region Profiling

        private const string _PRF_PFX = nameof(RICH) + ".";

        private static readonly ProfilerMarker _PRF_FormatMethodForLogging =
            new ProfilerMarker(_PRF_PFX + nameof(FormatMethodForLogging));

        private static readonly ProfilerMarker _PRF_FormatNameForLogging =
            new ProfilerMarker(_PRF_PFX + nameof(FormatNameForLogging));

        private static readonly ProfilerMarker _PRF_Bold = new ProfilerMarker(_PRF_PFX + nameof(Bold));

        private static readonly ProfilerMarker _PRF_Color = new ProfilerMarker(_PRF_PFX + nameof(Color));

        private static readonly ProfilerMarker _PRF_Italic = new ProfilerMarker(_PRF_PFX + nameof(Italic));
        private static readonly ProfilerMarker _PRF_Size = new ProfilerMarker(_PRF_PFX + nameof(Size));

        private static readonly ProfilerMarker _PRF_SupportRichText =
            new ProfilerMarker(_PRF_PFX + nameof(SupportRichText));

        private static readonly ProfilerMarker _PRF_Weight = new ProfilerMarker(_PRF_PFX + nameof(Weight));

        #endregion
    }
}
