using System;
using System.Collections.Generic;
using Appalachia.Utility.Colors;
using Appalachia.Utility.Standards;
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
        private static readonly Color LogConstant = new(048f / 255f, 105f / 255f, 0851f / 255f, 1.0f);
        private static readonly Color LogEnum = new(101f / 255f, 135f / 255f, 134f / 255f, 1.0f);
        private static readonly Color LogEvent = new(087f / 255f, 091f / 255f, 1028f / 255f, 1.0f);
        private static readonly Color LogField = new(106f / 255f, 128f / 255f, 099f / 255f, 1.0f);
        private static readonly Color LogInt = new(079f / 255f, 123f / 255f, 180f / 255f, 1.0f);
        private static readonly Color LogMethod = new(166f / 255f, 090f / 255f, 050f / 255f, 1.0f);

        private static readonly Color LogName = Green;
        private static readonly Color LogNull = new(022f / 255f, 095f / 255f, 050f / 255f, 1.0f);
        private static readonly Color LogPath = new(176f / 255f, 083f / 255f, 070f / 255f, 1.0f);
        private static readonly Color LogType = new(191f / 255f, 144f / 255f, 067f / 255f, 1.0f);
        private const string COMPONENT_FORMAT_STRING = "{0}.{1}";
        private const string GENERIC_METHOD_EXCEPTION_FORMAT_STRING = "Error in {0} of {1}.";

        private static readonly Utf16PreparedFormat<int, string> FONT_WEIGHT =
            ZString.PrepareUtf16<int, string>("<font-weight={0}>{1}</font-weight>");

        private static readonly Utf16PreparedFormat<int, string> SIZE =
            ZString.PrepareUtf16<int, string>("<size={0}>{1}</size>");

        private static readonly Utf16PreparedFormat<string, string> COLOR =
            ZString.PrepareUtf16<string, string>("<color={0}>{1}</color>");

        private static readonly Utf16PreparedFormat<string> BOLD = ZString.PrepareUtf16<string>("<b>{0}</b>");
        private static readonly Utf16PreparedFormat<string> ITALIC = ZString.PrepareUtf16<string>("<i>{0}</i>");
        private static Dictionary<double, string> _cachedDoubleFormatForLoggingResults;
        private static Dictionary<float, string> _cachedFloatFormatForLoggingResults;
        private static Dictionary<int, string> _cachedIntFormatForLoggingResults;
        private static Dictionary<object, string> _cachedObjectFormatForLoggingResults;
        private static Dictionary<string, Dictionary<string, Dictionary<int, string>>> _cachedCallerMemberFormatResults;
        private static Dictionary<string, string> _cachedStringBoldResults;
        private static Dictionary<string, string> _cachedStringFormatConstantForLoggingResults;
        private static Dictionary<string, string> _cachedStringFormatEventForLoggingResults;
        private static Dictionary<string, string> _cachedStringFormatFieldForLoggingResults;
        private static Dictionary<string, string> _cachedStringFormatMethodForLoggingResults;
        private static Dictionary<string, string> _cachedStringFormatNameForLoggingResults;
        private static Dictionary<string, string> _cachedStringItalicResults;
        private static Dictionary<Type, Dictionary<object, string>> _cachedFormatEnumForLogging;
        private static Dictionary<Type, string> _cachedTypeFormatForLoggingResults;
        private static Dictionary<Type, string> _formattedTypes;
        private static Utf8PreparedFormat<string, string> _componentFormatString;
        private static Utf8PreparedFormat<string, string> _genericMethodExceptionFormat;
        private static readonly string NULL = Color(Bold("<NULL>"), LogNull);

        #endregion

        #region Static Fields and Autoproperties

        #endregion

        private static Dictionary<double, string> CachedDoubleFormatForLoggingResults
        {
            get
            {
                if (_cachedDoubleFormatForLoggingResults == null)
                {
                    _cachedDoubleFormatForLoggingResults = new Dictionary<double, string>();
                }

                return _cachedDoubleFormatForLoggingResults;
            }
        }

        private static Dictionary<float, string> CachedFloatFormatForLoggingResults
        {
            get
            {
                if (_cachedFloatFormatForLoggingResults == null)
                {
                    _cachedFloatFormatForLoggingResults = new Dictionary<float, string>();
                }

                return _cachedFloatFormatForLoggingResults;
            }
        }

        private static Dictionary<int, string> CachedIntFormatForLoggingResults
        {
            get
            {
                if (_cachedIntFormatForLoggingResults == null)
                {
                    _cachedIntFormatForLoggingResults = new Dictionary<int, string>();
                }

                return _cachedIntFormatForLoggingResults;
            }
        }

        private static Dictionary<object, string> CachedObjectFormatForLoggingResults
        {
            get
            {
                if (_cachedObjectFormatForLoggingResults == null)
                {
                    _cachedObjectFormatForLoggingResults = new Dictionary<object, string>();
                }

                return _cachedObjectFormatForLoggingResults;
            }
        }

        private static Dictionary<string, Dictionary<string, Dictionary<int, string>>> CachedCallerMemberFormatResults
        {
            get
            {
                if (_cachedCallerMemberFormatResults == null)
                {
                    _cachedCallerMemberFormatResults =
                        new Dictionary<string, Dictionary<string, Dictionary<int, string>>>();
                }

                return _cachedCallerMemberFormatResults;
            }
        }

        private static Dictionary<string, string> CachedStringBoldResults
        {
            get
            {
                if (_cachedStringBoldResults == null)
                {
                    _cachedStringBoldResults = new Dictionary<string, string>();
                }

                return _cachedStringBoldResults;
            }
        }

        private static Dictionary<string, string> CachedStringFormatConstantForLoggingResults
        {
            get
            {
                if (_cachedStringFormatConstantForLoggingResults == null)
                {
                    _cachedStringFormatConstantForLoggingResults = new Dictionary<string, string>();
                }

                return _cachedStringFormatConstantForLoggingResults;
            }
        }

        private static Dictionary<string, string> CachedStringFormatEventForLoggingResults
        {
            get
            {
                if (_cachedStringFormatEventForLoggingResults == null)
                {
                    _cachedStringFormatEventForLoggingResults = new Dictionary<string, string>();
                }

                return _cachedStringFormatEventForLoggingResults;
            }
        }

        private static Dictionary<string, string> CachedStringFormatFieldForLoggingResults
        {
            get
            {
                if (_cachedStringFormatFieldForLoggingResults == null)
                {
                    _cachedStringFormatFieldForLoggingResults = new Dictionary<string, string>();
                }

                return _cachedStringFormatFieldForLoggingResults;
            }
        }

        private static Dictionary<string, string> CachedStringFormatMethodForLoggingResults
        {
            get
            {
                if (_cachedStringFormatMethodForLoggingResults == null)
                {
                    _cachedStringFormatMethodForLoggingResults = new Dictionary<string, string>();
                }

                return _cachedStringFormatMethodForLoggingResults;
            }
        }

        private static Dictionary<string, string> CachedStringFormatNameForLoggingResults
        {
            get
            {
                if (_cachedStringFormatNameForLoggingResults == null)
                {
                    _cachedStringFormatNameForLoggingResults = new Dictionary<string, string>();
                }

                return _cachedStringFormatNameForLoggingResults;
            }
        }

        private static Dictionary<string, string> CachedStringItalicResults
        {
            get
            {
                if (_cachedStringItalicResults == null)
                {
                    _cachedStringItalicResults = new Dictionary<string, string>();
                }

                return _cachedStringItalicResults;
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
                if (!CachedStringBoldResults.ContainsKey(value))
                {
                    CachedStringBoldResults.Add(value, BOLD.Format(value));
                }

                return CachedStringBoldResults[value];
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

        public static string FormatCallerMembersForLogging(
            this string callerFilePath,
            string callerMemberName,
            int callerLineNumber)
        {
            using (_PRF_FormatCallerMembersForLogging.Auto())
            {
                if (!CachedCallerMemberFormatResults.ContainsKey(callerFilePath))
                {
                    CachedCallerMemberFormatResults.Add(
                        callerFilePath,
                        new Dictionary<string, Dictionary<int, string>>()
                    );
                }

                var callerFilePathDictionary = CachedCallerMemberFormatResults[callerFilePath];
                if (!callerFilePathDictionary.ContainsKey(callerMemberName))
                {
                    callerFilePathDictionary.Add(callerMemberName, new Dictionary<int, string>());
                }

                var callerMemberNameDictionary = callerFilePathDictionary[callerMemberName];

                if (!callerFilePathDictionary[callerMemberName].ContainsKey(callerLineNumber))
                {
                    var fileName = System.IO.Path.GetFileNameWithoutExtension(callerFilePath).Bold().Color(LogType);
                    var lineNumber = callerLineNumber.FormatNumberForLogging();
                    var memberName = callerMemberName.Bold().Color(LogMethod);
                    var filePath = callerFilePath.Italic().Color(LogPath);

                    var result = $"{fileName}.{memberName}:{lineNumber} [{filePath}]";

                    callerMemberNameDictionary.Add(callerLineNumber, result);
                }

                return callerMemberNameDictionary[callerLineNumber];
            }
        }

        public static string FormatComponentForLogging<T>(this T value)
            where T : Component
        {
            using (_PRF_FormatForLogging.Auto())
            {
                if (value == null)
                {
                    return NULL;
                }

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
                if (value == null)
                {
                    return NULL;
                }

                if (!CachedStringFormatConstantForLoggingResults.ContainsKey(value))
                {
                    CachedStringFormatConstantForLoggingResults.Add(value, Color(Bold(value), LogConstant));
                }

                return CachedStringFormatConstantForLoggingResults[value];
            }
        }

        public static string FormatEnumForLogging<T>(this T value)
            where T : Enum
        {
            using (_PRF_FormatForLogging.Auto())
            {
                if (value == null)
                {
                    return NULL;
                }

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
                if (value == null)
                {
                    return NULL;
                }

                if (!CachedStringFormatEventForLoggingResults.ContainsKey(value))
                {
                    CachedStringFormatEventForLoggingResults.Add(value, Color(Bold(value), LogEvent));
                }

                return CachedStringFormatEventForLoggingResults[value];
            }
        }

        public static string FormatFieldForLogging(this string value)
        {
            using (_PRF_FormatFieldForLogging.Auto())
            {
                if (value == null)
                {
                    return NULL;
                }

                if (!CachedStringFormatFieldForLoggingResults.ContainsKey(value))
                {
                    CachedStringFormatFieldForLoggingResults.Add(value, Color(Bold(value), LogField));
                }

                return CachedStringFormatFieldForLoggingResults[value];
            }
        }

        public static string FormatForLogging(this Type value)
        {
            using (_PRF_FormatForLogging.Auto())
            {
                if (value == null)
                {
                    return NULL;
                }

                if (!CachedTypeFormatForLoggingResults.ContainsKey(value))
                {
                    CachedTypeFormatForLoggingResults.Add(value, Color(Bold(value.GetReadableName()), LogType));
                }

                return CachedTypeFormatForLoggingResults[value];
            }
        }

        public static string FormatForLogging(this int value)
        {
            using (_PRF_FormatForLogging.Auto())
            {
                if (!CachedIntFormatForLoggingResults.ContainsKey(value))
                {
                    CachedIntFormatForLoggingResults.Add(value, Color(Bold(value.ToString()), LogInt));
                }

                return CachedIntFormatForLoggingResults[value];
            }
        }

        public static string FormatForLogging(this ObjectID value)
        {
            using (_PRF_FormatForLogging.Auto())
            {
                if (value == null)
                {
                    return NULL;
                }

                if (!CachedObjectFormatForLoggingResults.ContainsKey(value))
                {
                    CachedObjectFormatForLoggingResults.Add(value, Color(Bold(value.ToString()), LogName));
                }

                return CachedObjectFormatForLoggingResults[value];
            }
        }

        public static string FormatForLogging(this string value)
        {
            using (_PRF_FormatForLogging.Auto())
            {
                if (value == null)
                {
                    return NULL;
                }

                if (!CachedObjectFormatForLoggingResults.ContainsKey(value))
                {
                    CachedObjectFormatForLoggingResults.Add(value, Color(Bold(value), LogName));
                }

                return CachedObjectFormatForLoggingResults[value];
            }
        }

        public static string FormatForLogging(this object value)
        {
            using (_PRF_FormatForLogging.Auto())
            {
                if (value == null)
                {
                    return NULL;
                }

                if (!CachedObjectFormatForLoggingResults.ContainsKey(value))
                {
                    CachedObjectFormatForLoggingResults.Add(value, Color(Bold(value.ToString()), LogName));
                }

                return CachedObjectFormatForLoggingResults[value];
            }
        }

        public static string FormatGameObjectForLogging(this GameObject value)
        {
            using (_PRF_FormatForLogging.Auto())
            {
                if (value == null)
                {
                    return NULL;
                }

                return FormatNameForLogging(value.name);
            }
        }

        public static string FormatMethodForLogging(this string value)
        {
            using (_PRF_FormatMethodForLogging.Auto())
            {
                if (value == null)
                {
                    return NULL;
                }

                if (!CachedStringFormatMethodForLoggingResults.ContainsKey(value))
                {
                    CachedStringFormatMethodForLoggingResults.Add(value, Color(Bold(value), LogMethod));
                }

                return CachedStringFormatMethodForLoggingResults[value];
            }
        }

        public static string FormatNameForLogging(this string value)
        {
            using (_PRF_FormatNameForLogging.Auto())
            {
                if (value == null)
                {
                    return NULL;
                }

                if (!CachedStringFormatNameForLoggingResults.ContainsKey(value))
                {
                    CachedStringFormatNameForLoggingResults.Add(value, Color(Bold(value), LogName));
                }

                return CachedStringFormatNameForLoggingResults[value];
            }
        }

        public static string FormatNumberForLogging(this int value)
        {
            using (_PRF_FormatForLogging.Auto())
            {
                if (!CachedIntFormatForLoggingResults.ContainsKey(value))
                {
                    CachedIntFormatForLoggingResults.Add(value, Color(Bold(value.ToString()), LogInt));
                }

                return CachedIntFormatForLoggingResults[value];
            }
        }

        public static string FormatNumberForLogging(this double value)
        {
            using (_PRF_FormatForLogging.Auto())
            {
                if (!CachedDoubleFormatForLoggingResults.ContainsKey(value))
                {
                    CachedDoubleFormatForLoggingResults.Add(value, Color(Bold(value.ToString("F3")), LogInt));
                }

                return CachedDoubleFormatForLoggingResults[value];
            }
        }

        public static string FormatNumberForLogging(this float value)
        {
            using (_PRF_FormatForLogging.Auto())
            {
                if (!CachedFloatFormatForLoggingResults.ContainsKey(value))
                {
                    CachedFloatFormatForLoggingResults.Add(value, Color(Bold(value.ToString("F3")), LogInt));
                }

                return CachedFloatFormatForLoggingResults[value];
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
                if (!CachedStringItalicResults.ContainsKey(value))
                {
                    CachedStringItalicResults.Add(value, ITALIC.Format(value));
                }

                return CachedStringItalicResults[value];
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

        private static readonly ProfilerMarker _PRF_FormatCallerMembersForLogging =
            new ProfilerMarker(_PRF_PFX + nameof(FormatCallerMembersForLogging));

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
