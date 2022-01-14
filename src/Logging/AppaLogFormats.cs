using System;
using System.Collections.Generic;
using System.Linq;
using Appalachia.Utility.Colors;
using Appalachia.Utility.Constants;
using Appalachia.Utility.Strings;
using Sirenix.OdinInspector;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.Serialization;

namespace Appalachia.Utility.Logging
{
#if UNITY_EDITOR
    [UnityEditor.InitializeOnLoad]
#endif
    public static class AppaLogFormats
    {
        [Flags]
        public enum LogFormatFlags
        {
            None = 0,
            Bold = 1 << 1,
            Italic = 1 << 2,
            Surrounded = 1 << 3,
            Hyphenated = 1 << 4,
        }

        public enum TextTransformation
        {
            None = 0,
            UPPER = 10,
            lower = 20,
        }

        static AppaLogFormats()
        {
            contexts = new Contexts();
            levels = new LogLevel();
            specials = new Specials();
        }

        #region Static Fields and Autoproperties

        public static Contexts contexts;

        public static LogLevel levels;

        public static Specials specials;

        #endregion

        #region Nested type: Contexts

        [Serializable]
        public class Contexts
        {
            public Contexts()
            {
                _formatsByKey = new Dictionary<string, LogFormat>();
            }

            #region Fields and Autoproperties

            private Dictionary<string, LogFormat> _formatsByKey;

            #endregion

            public IEnumerable<LogFormat> All => _formatsByKey.Values;

            public LogFormat this[string s]
            {
                get
                {
                    if (_formatsByKey.ContainsKey(s))
                    {
                        return _formatsByKey[s];
                    }

                    var newFormat = CreateFormatForKey(s, _formatsByKey);

                    return newFormat;
                }
            }

            internal static LogFormat CreateFormatForKey(string s, Dictionary<string, LogFormat> formatsByKey)
            {
                var newFormat = LogFormat.Context(s);

                var hueRange = new Vector2(90f,        260f);
                var saturationRange = new Vector2(20f, 50f);
                var valueRange = new Vector2(70f,      100f);

                var upperString = s.ToUpperInvariant();

                var hueTime = GetStringHashTime(s);
                var saturationTime = GetStringHashTime(s.Reverse());
                var valueTime = GetStringHashTime(upperString.Reverse());

                var hue = hueRange.x + ((hueRange.y - hueRange.x) * hueTime);
                var saturation = saturationRange.x +
                                 ((saturationRange.y - saturationRange.x) * saturationTime);
                var value = valueRange.x + ((valueRange.y - valueRange.x) * valueTime);

                newFormat.color = Colors.Colors.HSVToRGB(hue, saturation, value, 1f, false);

                formatsByKey.Add(upperString, newFormat);

                return newFormat;
            }

            private static float GetStringHashTime(IEnumerable<char> preconfiguredString)
            {
                var time = 0f;

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

        #endregion

        #region Nested type: LogFormat

        [Serializable, InlineProperty, LabelWidth(110)]
        public class LogFormat
        {
            #region Constants and Static Readonly

            private const string HYPHEN = "-";

            #endregion

            #region Fields and Autoproperties

            [FormerlySerializedAs("color")]
            [OnValueChanged(nameof(Validate))]
            [HorizontalGroup("Format")]
            [LabelWidth(40)]
            [PropertyOrder(3)]
            [SerializeField]
            private Color _color;

            private Dictionary<string, string> _formattedStrings;

            [FormerlySerializedAs("format")]
            [OnValueChanged(nameof(Validate))]
            [HorizontalGroup("Format")]
            [LabelWidth(45)]
            [PropertyOrder(1)]
            [SerializeField]
            private LogFormatFlags _format;

            [FormerlySerializedAs("surroundLeft")]
            [OnValueChanged(nameof(Validate))]
            [HorizontalGroup("Format")]
            [LabelWidth(10)]
            [LabelText("L")]
            [EnableIf(nameof(_enableSurroundFields))]
            [PropertyOrder(14)]
            [SerializeField]
            private string _surroundLeft;

            [FormerlySerializedAs("surroundRight")]
            [OnValueChanged(nameof(Validate))]
            [HorizontalGroup("Format")]
            [LabelWidth(10)]
            [LabelText("R")]
            [EnableIf(nameof(_enableSurroundFields))]
            [PropertyOrder(15)]
            [SerializeField]
            private string _surroundRight;

            [FormerlySerializedAs("style")]
            [OnValueChanged(nameof(Validate))]
            [HorizontalGroup("Format")]
            [LabelWidth(35)]
            [PropertyOrder(10)]
            [SerializeField]
            private TextTransformation _style;

            #endregion

            public Color color
            {
                get => _color == Color.clear ? Color.white : _color;
                set => _color = value;
            }

            public LogFormatFlags format
            {
                get => _format;
                set => _format = value;
            }

            public string surroundLeft
            {
                get => _surroundLeft;
                set => _surroundLeft = value;
            }

            public string surroundRight
            {
                get => _surroundRight;
                set => _surroundRight = value;
            }

            public TextTransformation style
            {
                get => _style;
                set => _style = value;
            }

            private bool _enableSurroundFields => format.HasFlag(LogFormatFlags.Surrounded);

            public static LogFormat Context(string key)
            {
                var anyUpperCharacter = false;

                for (var i = 0; i < key.Length; i++)
                {
                    var c = key[i];

                    if (char.IsUpper(c))
                    {
                        anyUpperCharacter = true;
                    }
                }

                var useHyphen = anyUpperCharacter ? LogFormatFlags.Hyphenated : LogFormatFlags.None;

                return new LogFormat
                {
                    style = TextTransformation.UPPER,
                    format = LogFormatFlags.Bold | LogFormatFlags.Surrounded | useHyphen,
                    surroundLeft = "[",
                    surroundRight = "]",
                };
            }

            public string Format(string v)
            {
                using (_PRF_Format.Auto())
                {
                    if (v == null)
                    {
                        return null;
                    }

                    if (TryGetCachedString(v, out var result))
                    {
                        return result;
                    }

                    var input = v;

                    var output = new Utf16ValueStringBuilder(true);

                    output.Append(v);

                    if (format.HasFlag(LogFormatFlags.Hyphenated))
                    {
                        for (var i = 1; i < output.Length; i++)
                        {
                            if (char.IsUpper(output[i]) && !char.IsUpper(output[i - 1]))
                            {
                                output.Insert(i, HYPHEN);
                                i += 1;
                            }
                        }
                    }

                    if (style != TextTransformation.None)
                    {
                        for (var i = 0; i < output.Length; i++)
                        {
                            if (style == TextTransformation.UPPER)
                            {
                                output[i] = char.ToUpperInvariant(output[i]);
                            }
                            else if (style == TextTransformation.lower)
                            {
                                output[i] = char.ToLowerInvariant(output[i]);
                            }
                        }
                    }

                    if (format.HasFlag(LogFormatFlags.Surrounded))
                    {
                        output.Insert(0, surroundLeft);
                        output.Append(surroundRight);
                    }

                    result = output.ToString();
                    output.Dispose();

                    if (format.HasFlag(LogFormatFlags.Bold))
                    {
                        result = result.Bold();
                    }

                    if (format.HasFlag(LogFormatFlags.Italic))
                    {
                        result = result.Italic();
                    }

                    result = result.Color(_color);

                    _formattedStrings ??= new Dictionary<string, string>();

                    _formattedStrings.Add(input, result);

                    return result;
                }
            }

            private void InvalidateCache()
            {
                using (_PRF_InvalidateCache.Auto())
                {
                    _formattedStrings?.Clear();
                }
            }

            private bool TryGetCachedString(string input, out string output)
            {
                using (_PRF_TryGetCachedString.Auto())
                {
                    _formattedStrings ??= new Dictionary<string, string>();

                    if (_formattedStrings.ContainsKey(input))
                    {
                        output = _formattedStrings[input];
                        return true;
                    }

                    output = null;
                    return false;
                }
            }

            private void Validate()
            {
                using (_PRF_Validate.Auto())
                {
                    if (_color.a == 0f)
                    {
                        _color.a = 1f;
                    }

                    InvalidateCache();
                }
            }

            #region Profiling

            private const string _PRF_PFX = nameof(LogFormat) + ".";

            private static readonly ProfilerMarker _PRF_InvalidateCache =
                new ProfilerMarker(_PRF_PFX + nameof(InvalidateCache));

            private static readonly ProfilerMarker _PRF_TryGetCachedString =
                new ProfilerMarker(_PRF_PFX + nameof(TryGetCachedString));

            private static readonly ProfilerMarker
                _PRF_Format = new ProfilerMarker(_PRF_PFX + nameof(Format));

            private static readonly ProfilerMarker _PRF_Validate =
                new ProfilerMarker(_PRF_PFX + nameof(Validate));

            #endregion
        }

        #endregion

        #region Nested type: LogLevel

        [Serializable]
        public class LogLevel
        {
            public LogLevel()
            {
                fatal = new LogFormat
                {
                    color = "FF3333".ColorFromHex(),
                    format = LogFormatFlags.Bold | LogFormatFlags.Surrounded,
                    surroundLeft = "** [",
                    surroundRight = "] **",
                    style = TextTransformation.UPPER
                };

                critical = new LogFormat
                {
                    color = "FF3333".ColorFromHex(),
                    format = LogFormatFlags.Bold | LogFormatFlags.Surrounded,
                    surroundLeft = "* [",
                    surroundRight = "] *",
                    style = TextTransformation.UPPER
                };

                exception = LogFormat.Context(nameof(exception));
                error = LogFormat.Context(nameof(error));
                warn = LogFormat.Context(nameof(warn));
                info = LogFormat.Context(nameof(info));
                debug = LogFormat.Context(nameof(debug));
                trace = LogFormat.Context(nameof(trace));

                exception.color = "FF4133".ColorFromHex();
                error.color = "F25130".ColorFromHex();
                warn.color = "EE7F30".ColorFromHex();
                info.color = "FBF7EF".ColorFromHex();
                debug.color = "EAD6AE".ColorFromHex();
                trace.color = "EAD6AE".ColorFromHex();
            }

            #region Fields and Autoproperties

            public LogFormat critical;
            public LogFormat debug;
            public LogFormat error;
            public LogFormat exception;
            public LogFormat fatal;
            public LogFormat info;
            public LogFormat trace;
            public LogFormat warn;

            #endregion
        }

        #endregion

        #region Nested type: Specials

        [Serializable]
        public class Specials
        {
            public Specials()
            {
                className = new LogFormat
                {
                    color = "FFFFFF".ColorFromHex(.85f),
                    format = LogFormatFlags.Bold | LogFormatFlags.Surrounded,
                    surroundLeft = "[",
                    surroundRight = "]",
                    style = TextTransformation.None
                };
                exceptionMessage = new LogFormat
                {
                    color = "E67A45".ColorFromHex(),
                    format = LogFormatFlags.Surrounded,
                    surroundRight = " ->",
                    style = TextTransformation.None
                };
                exceptionName = new LogFormat
                {
                    color = "CC4C33".ColorFromHex(),
                    format = LogFormatFlags.Bold | LogFormatFlags.Surrounded,
                    surroundLeft = "[",
                    surroundRight = "]",
                    style = TextTransformation.None
                };
                filename = new LogFormat
                {
                    color = "FFFFFF".ColorFromHex(.85f),
                    format = LogFormatFlags.Bold | LogFormatFlags.Surrounded,
                    surroundLeft = "[",
                    surroundRight = "]",
                    style = TextTransformation.None
                };
                message = new LogFormat
                {
                    color = "FFFFFF".ColorFromHex(.70f),
                    format = LogFormatFlags.None,
                    style = TextTransformation.None
                };
                structural = new LogFormat
                {
                    color = "FFFFFF".ColorFromHex(.5f),
                    format = LogFormatFlags.None,
                    style = TextTransformation.None
                };
            }

            #region Fields and Autoproperties

            [PropertyOrder(10)] public LogFormat className;
            [PropertyOrder(40)] public LogFormat exceptionMessage;
            [PropertyOrder(30)] public LogFormat exceptionName;
            [PropertyOrder(20)] public LogFormat filename;
            [PropertyOrder(60)] public LogFormat message;
            [PropertyOrder(50)] public LogFormat structural;

            #endregion
        }

        #endregion
    }
}
