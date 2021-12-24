using System;
using Appalachia.Utility.Strings;
using Unity.Profiling;

// ReSharper disable FormatStringProblem

namespace Appalachia.Utility.Logging.Formatters
{
    public class UnityConsoleFormatter : AppaLogFormatter
    {
        #region Constants and Static Readonly

        private const string FMT_STR = "{0} {1} ";
        private const string FMT_W_EXCP_STR = "{0} {1} {2} {3} ";
        private const string FMT_W_PFX_EXCP_STR = "{0} {1} {2} {3} {4} ";
        private const string FMT_W_PFX_STR = "{0} {1} {2} ";

        private static readonly Utf16PreparedFormat<string, string> FORMAT = new(FMT_STR);

        private static readonly Utf16PreparedFormat<string, string, string, string> FORMAT_W_EXCP =
            new(FMT_W_EXCP_STR);

        private static readonly Utf16PreparedFormat<string, string, string, string, string>
            FORMAT_W_PFX_EXCP = new(FMT_W_PFX_EXCP_STR);

        private static readonly Utf16PreparedFormat<string, string, string> FORMAT_W_PFX = new(FMT_W_PFX_STR);

        #endregion

        protected override string AlterContent(string content, LogLevel level)
        {
            using (_PRF_AlterContent.Auto())
            {
                if (level == LogLevel.Exception)
                {
                    return content;
                }

                return AppaLogFormatHolder.specials.message.Format(content);
            }
        }

        protected override string GetLogPrefix(
            LogLevel level,
            string prefix,
            string formattedPrefix,
            object context,
            Exception exception,
            string memberName,
            string filePath,
            int lineNumber)
        {
            using (_PRF_GetLogPrefix.Auto())
            {
                var logLevel = GetLogLevelString(level, GetLogLevelString);

                string logContext;

                if (context == null)
                {
                    logContext = GetFileNameFromPathInternal(filePath, AdjustFileNameForLogging);
                }
                else
                {
                    logContext = AppaLogFormatHolder.specials.className.Format(context.GetType().Name);
                }

                string result;

                // loglevel *prefix context *exceptionName *exceptionMessage

                if ((formattedPrefix != null) && (exception != null))
                {
                    GetExceptionLoggingParts(exception, out var exceptionName, out var exceptionMessage);

                    result = FORMAT_W_PFX_EXCP.Format(
                        logLevel,
                        formattedPrefix,
                        logContext,
                        exceptionName,
                        exceptionMessage
                    );
                }
                else if (formattedPrefix != null)
                {
                    result = FORMAT_W_PFX.Format(logLevel, formattedPrefix, logContext);
                }
                else if (exception != null)
                {
                    GetExceptionLoggingParts(exception, out var exceptionName, out var exceptionMessage);

                    result = FORMAT_W_EXCP.Format(logLevel, logContext, exceptionName, exceptionMessage);
                }
                else
                {
                    result = FORMAT.Format(logLevel, logContext);
                }

                return result;
            }
        }

        private static void GetExceptionLoggingParts(
            Exception exception,
            out string exceptionName,
            out string exceptionMessage)
        {
            using (_PRF_GetExceptionLoggingParts.Auto())
            {
                exceptionName = AppaLogFormatHolder.specials.exceptionName.Format(exception.GetType().Name);
                exceptionMessage = AppaLogFormatHolder.specials.exceptionMessage.Format(exception.Message);
            }
        }

        private string AdjustFileNameForLogging(string fn)
        {
            using (_PRF_AdjustFileNameForLogging.Auto())
            {
                return AppaLogFormatHolder.specials.filename.Format(fn);
            }
        }

        private string GetLogLevelString(LogLevel l)
        {
            using (_PRF_GetLogLevelString.Auto())
            {
                var logLevelUpper = l.ToString().ToUpperInvariant();

                var logLevelResult = l switch
                {
                    LogLevel.Fatal     => AppaLogFormatHolder.levels.fatal.Format(logLevelUpper),
                    LogLevel.Critical  => AppaLogFormatHolder.levels.critical.Format(logLevelUpper),
                    LogLevel.Exception => AppaLogFormatHolder.levels.exception.Format(logLevelUpper),
                    LogLevel.Error     => AppaLogFormatHolder.levels.error.Format(logLevelUpper),
                    LogLevel.Warn      => AppaLogFormatHolder.levels.warn.Format(logLevelUpper),
                    LogLevel.Info      => AppaLogFormatHolder.levels.info.Format(logLevelUpper),
                    LogLevel.Debug     => AppaLogFormatHolder.levels.debug.Format(logLevelUpper),
                    _                  => AppaLogFormatHolder.levels.trace.Format(logLevelUpper)
                };

                return logLevelResult;
            }
        }

        #region Profiling

        private const string _PRF_PFX = nameof(UnityConsoleFormatter) + ".";

        private static readonly ProfilerMarker _PRF_GetExceptionLoggingParts =
            new ProfilerMarker(_PRF_PFX + nameof(GetExceptionLoggingParts));

        private static readonly ProfilerMarker _PRF_AlterContent =
            new ProfilerMarker(_PRF_PFX + nameof(AlterContent));

        private static readonly ProfilerMarker _PRF_GetLogLevelString =
            new ProfilerMarker(_PRF_PFX + nameof(GetLogLevelString));

        private static readonly ProfilerMarker _PRF_AdjustFileNameForLogging =
            new ProfilerMarker(_PRF_PFX + nameof(AdjustFileNameForLogging));

        private static readonly ProfilerMarker _PRF_GetLogPrefix =
            new ProfilerMarker(_PRF_PFX + nameof(GetLogPrefix));

        #endregion
    }
}
