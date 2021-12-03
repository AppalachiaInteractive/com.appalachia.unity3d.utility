using Unity.Profiling;

namespace Appalachia.Utility.Logging.Formatters
{
    public class UnityConsoleFormatter : AppaLogFormatter
    {
        protected override string AlterContent(string content, LogLevel level)
        {
            if (level == LogLevel.Exception)
            {
                return content;
            }

            return formats.message.Format(content);
        }

        protected override string GetLogPrefix(
            LogLevel level,
            string prefix,
            string formattedPrefix,
            string memberName,
            string filePath,
            int lineNumber)
        {
            using (_PRF_GetLogPrefix.Auto())
            {
                var fileName = GetFileNameFromPathInternal(filePath, fn => formats.filename.Format(fn));

                var logLevelString = GetLogLevelString(
                    level,
                    l =>
                    {
                        var upper = l.ToString().ToUpperInvariant();
                        string logLevelResult;

                        switch (l)
                        {
                            case LogLevel.Fatal:
                                logLevelResult = formats.levels.fatal.Format(upper);
                                break;
                            case LogLevel.Critical:
                                logLevelResult = formats.levels.critical.Format(upper);
                                break;
                            case LogLevel.Exception:
                                logLevelResult = formats.levels.exception.Format(upper);
                                break;
                            case LogLevel.Error:
                                logLevelResult = formats.levels.error.Format(upper);
                                break;
                            case LogLevel.Warn:
                                logLevelResult = formats.levels.warn.Format(upper);
                                break;
                            case LogLevel.Info:
                                logLevelResult = formats.levels.info.Format(upper);
                                break;
                            case LogLevel.Debug:
                                logLevelResult = formats.levels.debug.Format(upper);
                                break;
                            default:
                                logLevelResult = formats.levels.trace.Format(upper);
                                break;
                        }

                        return logLevelResult;
                    }
                );

                string result;
                if (formattedPrefix != null)
                {
                    result = $"{logLevelString} {formattedPrefix} {fileName} ";
                }
                else
                {
                    result = $"{logLevelString} {fileName} ";
                }

                return result;
            }
        }

        #region Profiling

        private const string _PRF_PFX = nameof(UnityConsoleFormatter) + ".";

        private static readonly ProfilerMarker _PRF_GetLogPrefix =
            new ProfilerMarker(_PRF_PFX + nameof(GetLogPrefix));

        #endregion
    }
}
