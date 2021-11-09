using Appalachia.Utility.Colors;
using Appalachia.Utility.Constants;
using Unity.Profiling;

namespace Appalachia.Utility.Logging.Formatters
{
    public class UnityConsoleFormatter : AppaLogFormatter
    {
        #region Profiling And Tracing Markers

        private const string _PRF_PFX = nameof(UnityConsoleFormatter) + ".";

        private static readonly ProfilerMarker _PRF_GetLogPrefix =
            new ProfilerMarker(_PRF_PFX + nameof(GetLogPrefix));

        #endregion

        protected override string GetLogPrefix(
            LogLevel level,
            string memberName,
            string filePath,
            int lineNumber)
        {
            using (_PRF_GetLogPrefix.Auto())
            {
                var fileName = GetFileNameFromPathInternal(
                    filePath,
                    fn =>
                    {
                        var italic = fn.Italic().Bold();
                        var colored = italic.Color(ColorPalette.Default.classes.Middle);

                        return colored;
                    }
                );

                var logLevelString = GetLogLevelString(
                    level,
                    l =>
                    {
                        var upper = l.ToString().ToUpperInvariant();
                        string result;

                        switch (l)
                        {
                            case LogLevel.Fatal:
                                result = upper.Bold().Color(ColorPalette.Default.bad.First);
                                break;
                            case LogLevel.Critical:
                                result = upper.Bold().Color(ColorPalette.Default.bad.First);
                                break;
                            case LogLevel.Exception:
                                result = upper.Bold().Color(ColorPalette.Default.bad.Quarter);
                                break;
                            case LogLevel.Error:
                                result = upper.Bold().Color(ColorPalette.Default.bad.Middle);
                                break;
                            case LogLevel.Warn:
                                result = upper.Bold().Color(ColorPalette.Default.bad.Last);
                                break;
                            case LogLevel.Info:
                                result = upper.Bold().Color(ColorPalette.Default.notable.Middle);
                                break;
                            case LogLevel.Debug:
                                result = upper.Bold().Color(ColorPalette.Default.notable.ThreeQuarters);
                                break;
                            default:
                                result = upper.Bold();
                                break;
                        }

                        return result;
                    }
                );

                var prefix = $"{logLevelString} {fileName} ";
                return prefix;
            }
        }
    }
}
