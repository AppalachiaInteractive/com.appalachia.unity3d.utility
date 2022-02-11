using System;
using System.Collections.Generic;
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
        public static readonly Color Blue = new(109f / 255f, 127f / 255f, 130f / 255f, 1.0f);
        public static readonly Color Bone = new(219f / 255f, 214f / 255f, 203f / 255f, 1.0f);
        public static readonly Color Cream = new(222f / 255f, 208f / 255f, 183f / 255f, 1.0f);
        public static readonly Color DarkBrown = new(046f / 255f, 041f / 255f, 034f / 255f, 1.0f);
        public static readonly Color DarkYellow = new(202f / 255f, 154f / 255f, 064f / 255f, 1.0f);
        public static readonly Color Gray = new(103f / 255f, 096f / 255f, 081f / 255f, 1.0f);
        public static readonly Color Green = new(089f / 255f, 113f / 255f, 080f / 255f, 1.0f);
        public static readonly Color LightYellow = new(229f / 255f, 204f / 255f, 159f / 255f, 1.0f);
        public static readonly Color Normal = new(127f / 255f, 127f / 255f, 255f / 255f, 1.0f);
        public static readonly Color Orange = new(022f / 255f, 095f / 255f, 050f / 255f, 1.0f);
        public static readonly Color Red = new(128f / 255f, 062f / 255f, 040f / 255f, 1.0f);
        public static readonly Color RichYellow = new(211f / 255f, 170f / 255f, 095f / 255f, 1.0f);
        public static readonly Color Tan = new(175f / 255f, 165f / 255f, 143f / 255f, 1.0f);
        public static readonly Color Yellow = new(217f / 255f, 183f / 255f, 118f / 255f, 1.0f);
        private static readonly Color LogConstant = new(095f / 255f, 156f / 255f, 171f / 255f, 1.0f);
        private static readonly Color LogEnum = new(135f / 255f, 126f / 255f, 200f / 255f, 1.0f);
        private static readonly Color LogEvent = new(071f / 255f, 119f / 255f, 198f / 255f, 1.0f);
        private static readonly Color LogField = new(127f / 255f, 163f / 255f, 155f / 255f, 1.0f);
        private static readonly Color LogInt = new(068f / 255f, 137f / 255f, 240f / 255f, 1.0f);
        private static readonly Color LogMethod = new(174f / 255f, 084f / 255f, 047f / 255f, 1.0f);

        private static readonly Color LogName = Green;
        private static readonly Color LogType = new(228f / 255f, 168f / 255f, 064f / 255f, 1.0f);
        private const string COMPONENT_FORMAT_STRING = "{0}.{1}";
        private const string GENERIC_METHOD_EXCEPTION_FORMAT_STRING = "Error in {0} of {1}.";

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

        #region Static Fields and Autoproperties

        private static Dictionary<int, string> _cachedintFormatForLoggingResults;

        private static Dictionary<object, string> _cachedobjectFormatForLoggingResults;

        private static Dictionary<string, string> _cachedstringBoldResults;

        private static Dictionary<string, string> _cachedstringFormatConstantForLoggingResults;

        private static Dictionary<string, string> _cachedstringFormatEventForLoggingResults;

        private static Dictionary<string, string> _cachedstringFormatFieldForLoggingResults;

        private static Dictionary<string, string> _cachedstringFormatMethodForLoggingResults;

        private static Dictionary<string, string> _cachedstringFormatNameForLoggingResults;

        private static Dictionary<string, string> _cachedstringItalicResults;

        private static Dictionary<Type, Dictionary<object, string>> _cachedFormatEnumForLogging;

        private static Dictionary<Type, string> _cachedTypeFormatForLoggingResults;

        private static Dictionary<Type, string> _formattedTypes;

        private static Utf8PreparedFormat<string, string> _componentFormatString;

        private static Utf8PreparedFormat<string, string> _genericMethodExceptionFormat;

        #endregion

        private static Dictionary<int, string> CachedintFormatForLoggingResults
        {
            get
            {
                if (_cachedintFormatForLoggingResults == null)
                {
                    _cachedintFormatForLoggingResults = new Dictionary<int, string>();
                }

                return _cachedintFormatForLoggingResults;
            }
        }

        private static Dictionary<object, string> CachedobjectFormatForLoggingResults
        {
            get
            {
                if (_cachedobjectFormatForLoggingResults == null)
                {
                    _cachedobjectFormatForLoggingResults = new Dictionary<object, string>();
                }

                return _cachedobjectFormatForLoggingResults;
            }
        }

        private static Dictionary<string, string> CachedstringBoldResults
        {
            get
            {
                if (_cachedstringBoldResults == null)
                {
                    _cachedstringBoldResults = new Dictionary<string, string>();
                }

                return _cachedstringBoldResults;
            }
        }

        private static Dictionary<string, string> CachedstringFormatConstantForLoggingResults
        {
            get
            {
                if (_cachedstringFormatConstantForLoggingResults == null)
                {
                    _cachedstringFormatConstantForLoggingResults = new Dictionary<string, string>();
                }

                return _cachedstringFormatConstantForLoggingResults;
            }
        }

        private static Dictionary<string, string> CachedstringFormatEventForLoggingResults
        {
            get
            {
                if (_cachedstringFormatEventForLoggingResults == null)
                {
                    _cachedstringFormatEventForLoggingResults = new Dictionary<string, string>();
                }

                return _cachedstringFormatEventForLoggingResults;
            }
        }

        private static Dictionary<string, string> CachedstringFormatFieldForLoggingResults
        {
            get
            {
                if (_cachedstringFormatFieldForLoggingResults == null)
                {
                    _cachedstringFormatFieldForLoggingResults = new Dictionary<string, string>();
                }

                return _cachedstringFormatFieldForLoggingResults;
            }
        }

        private static Dictionary<string, string> CachedstringFormatMethodForLoggingResults
        {
            get
            {
                if (_cachedstringFormatMethodForLoggingResults == null)
                {
                    _cachedstringFormatMethodForLoggingResults = new Dictionary<string, string>();
                }

                return _cachedstringFormatMethodForLoggingResults;
            }
        }

        private static Dictionary<string, string> CachedstringFormatNameForLoggingResults
        {
            get
            {
                if (_cachedstringFormatNameForLoggingResults == null)
                {
                    _cachedstringFormatNameForLoggingResults = new Dictionary<string, string>();
                }

                return _cachedstringFormatNameForLoggingResults;
            }
        }

        private static Dictionary<string, string> CachedstringItalicResults
        {
            get
            {
                if (_cachedstringItalicResults == null)
                {
                    _cachedstringItalicResults = new Dictionary<string, string>();
                }

                return _cachedstringItalicResults;
            }
        }

        private static Dictionary<Type, Dictionary<object, string>> CachedFormatEnumForLogging
        {
            get
            {
                if (_cachedFormatEnumForLogging == null)
                {
                    _cachedFormatEnumForLogging = new Dictionary<Type, Dictionary<object, string>>();
                }

                return _cachedFormatEnumForLogging;
            }
        }

        private static Dictionary<Type, string> CachedTypeFormatForLoggingResults
        {
            get
            {
                if (_cachedTypeFormatForLoggingResults == null)
                {
                    _cachedTypeFormatForLoggingResults = new Dictionary<Type, string>();
                }

                return _cachedTypeFormatForLoggingResults;
            }
        }

        public static string Bold(this string value)
        {
            using (_PRF_Bold.Auto())
            {
                if (!CachedstringBoldResults.ContainsKey(value))
                {
                    CachedstringBoldResults.Add(value, BOLD.Format(value));
                }

                return CachedstringBoldResults[value];
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

        public static string FormatComponentForLogging<T>(this T value)
            where T : Component
        {
            using (_PRF_FormatForLogging.Auto())
            {
                if (_componentFormatString == null)
                {
                    _componentFormatString = new Utf8PreparedFormat<string, string>(COMPONENT_FORMAT_STRING);
                }

                if (value == null)
                {
                    return "MISSING COMPONENT";
                }

                var objName = value.gameObject.FormatGameObjectForLogging();
                var typeName = value.GetType().FormatForLogging();

                return _componentFormatString.Format(objName, typeName);
            }
        }

        public static string FormatConstantForLogging(this string value)
        {
            using (_PRF_FormatConstantForLogging.Auto())
            {
                if (!CachedstringFormatConstantForLoggingResults.ContainsKey(value))
                {
                    CachedstringFormatConstantForLoggingResults.Add(value, Color(Bold(value), LogConstant));
                }

                return CachedstringFormatConstantForLoggingResults[value];
            }
        }

        public static string FormatEnumForLogging<T>(this T value)
            where T : Enum
        {
            using (_PRF_FormatForLogging.Auto())
            {
                if (!CachedFormatEnumForLogging.ContainsKey(typeof(T)))
                {
                    CachedFormatEnumForLogging.Add(typeof(T), new Dictionary<object, string>());
                }

                if (!CachedFormatEnumForLogging[typeof(T)].ContainsKey(value))
                {
                    var result = Color(Bold(value.ToString()), LogEnum);
                    CachedFormatEnumForLogging[typeof(T)].Add(value, result);

                    return result;
                }

                return CachedFormatEnumForLogging[typeof(T)][value];
            }
        }

        public static string FormatEventForLogging(this string value)
        {
            using (_PRF_FormatEventForLogging.Auto())
            {
                if (!CachedstringFormatEventForLoggingResults.ContainsKey(value))
                {
                    CachedstringFormatEventForLoggingResults.Add(value, Color(Bold(value), LogEvent));
                }

                return CachedstringFormatEventForLoggingResults[value];
            }
        }

        public static string FormatFieldForLogging(this string value)
        {
            using (_PRF_FormatFieldForLogging.Auto())
            {
                if (!CachedstringFormatFieldForLoggingResults.ContainsKey(value))
                {
                    CachedstringFormatFieldForLoggingResults.Add(value, Color(Bold(value), LogField));
                }

                return CachedstringFormatFieldForLoggingResults[value];
            }
        }

        public static string FormatForLogging(this Type value)
        {
            using (_PRF_FormatForLogging.Auto())
            {
                if (!CachedTypeFormatForLoggingResults.ContainsKey(value))
                {
                    CachedTypeFormatForLoggingResults.Add(value, Color(Bold(value.Name), LogType));
                }

                return CachedTypeFormatForLoggingResults[value];
            }
        }

        public static string FormatForLogging(this int value)
        {
            using (_PRF_FormatForLogging.Auto())
            {
                if (!CachedintFormatForLoggingResults.ContainsKey(value))
                {
                    CachedintFormatForLoggingResults.Add(value, Color(Bold(value.ToString()), LogInt));
                }

                return CachedintFormatForLoggingResults[value];
            }
        }

        public static string FormatForLogging(this object value)
        {
            using (_PRF_FormatForLogging.Auto())
            {
                if (!CachedobjectFormatForLoggingResults.ContainsKey(value))
                {
                    CachedobjectFormatForLoggingResults.Add(value, Color(Bold(value.ToString()), LogName));
                }

                return CachedobjectFormatForLoggingResults[value];
            }
        }

        public static string FormatGameObjectForLogging(this GameObject value)
        {
            using (_PRF_FormatForLogging.Auto())
            {
                return FormatNameForLogging(value.name);
            }
        }

        public static string FormatMethodForLogging(this string value)
        {
            using (_PRF_FormatMethodForLogging.Auto())
            {
                if (!CachedstringFormatMethodForLoggingResults.ContainsKey(value))
                {
                    CachedstringFormatMethodForLoggingResults.Add(value, Color(Bold(value), LogMethod));
                }

                return CachedstringFormatMethodForLoggingResults[value];
            }
        }

        public static string FormatNameForLogging(this string value)
        {
            using (_PRF_FormatNameForLogging.Auto())
            {
                if (!CachedstringFormatNameForLoggingResults.ContainsKey(value))
                {
                    CachedstringFormatNameForLoggingResults.Add(value, Color(Bold(value), LogName));
                }

                return CachedstringFormatNameForLoggingResults[value];
            }
        }

        public static string GenericMethodException(this string methodName, Type componentType)
        {
            using (_PRF_GenericMethodException.Auto())
            {
                if (_genericMethodExceptionFormat == null)
                {
                    _genericMethodExceptionFormat =
                        new Utf8PreparedFormat<string, string>(GENERIC_METHOD_EXCEPTION_FORMAT_STRING);
                }

                var methodLogFormat = methodName.FormatMethodForLogging();
                var typeName = componentType.FormatForLogging();

                return _genericMethodExceptionFormat.Format(methodLogFormat, typeName);
            }
        }

        public static string GenericMethodException<T>(this string methodName)
        {
            using (_PRF_GenericMethodException.Auto())
            {
                if (_genericMethodExceptionFormat == null)
                {
                    _genericMethodExceptionFormat =
                        new Utf8PreparedFormat<string, string>(GENERIC_METHOD_EXCEPTION_FORMAT_STRING);
                }

                var methodLogFormat = methodName.FormatMethodForLogging();
                var contextFormat = typeof(T).FormatForLogging();

                return _genericMethodExceptionFormat.Format(methodLogFormat, contextFormat);
            }
        }

        public static string GenericMethodException<T>(this string methodName, T component)
            where T : Component
        {
            using (_PRF_GenericMethodException.Auto())
            {
                if (_genericMethodExceptionFormat == null)
                {
                    _genericMethodExceptionFormat =
                        new Utf8PreparedFormat<string, string>(GENERIC_METHOD_EXCEPTION_FORMAT_STRING);
                }

                var methodLogFormat = methodName.FormatMethodForLogging();
                var contextFormat = FormatComponentForLogging(component);

                return _genericMethodExceptionFormat.Format(methodLogFormat, contextFormat);
            }
        }

        public static string Italic(this string value)
        {
            using (_PRF_Italic.Auto())
            {
                if (!CachedstringItalicResults.ContainsKey(value))
                {
                    CachedstringItalicResults.Add(value, ITALIC.Format(value));
                }

                return CachedstringItalicResults[value];
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

        private static readonly ProfilerMarker _PRF_GenericMethodException =
            new ProfilerMarker(_PRF_PFX + nameof(GenericMethodException));

        private static readonly ProfilerMarker _PRF_FormatConstantForLogging =
            new ProfilerMarker(_PRF_PFX + nameof(FormatConstantForLogging));

        private static readonly ProfilerMarker _PRF_FormatEventForLogging =
            new ProfilerMarker(_PRF_PFX + nameof(FormatEventForLogging));

        private static readonly ProfilerMarker _PRF_FormatFieldForLogging =
            new ProfilerMarker(_PRF_PFX + nameof(FormatFieldForLogging));

        private static readonly ProfilerMarker _PRF_FormatForLogging =
            new ProfilerMarker(_PRF_PFX + nameof(FormatEnumForLogging));

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
