using System;
using Appalachia.Utility.Strings;
using Unity.Profiling;

// ReSharper disable FormatStringProblem

namespace Appalachia.Utility.Logging.Formatters
{
    public class FileLogFormatter : AppaLogFormatter
    {
        #region Constants and Static Readonly

        private const string appalachia = "Appalachia";
        private const string FMT_STR = "{0:O} {1} [" + appalachia + "]";
        private const string FMT_W_EXCP_STR = FMT_STR + " [{2}: {3}] {4}.{5}:{6} ";
        private const string FMT_W_PFX_EXCP_STR = FMT_STR + " [{2}] [{3}: {4}] {5}.{6}:{7} ";
        private const string FMT_W_PFX_STR = FMT_STR + " [{2}] {3}.{4}:{5} ";

        private static readonly Utf16PreparedFormat<DateTime, string, string, string, int> FORMAT = new(
            FMT_STR
        );

        private static readonly Utf16PreparedFormat<DateTime, string, Exception, string, string, int>
            FORMAT_W_EXCP = new(FMT_W_EXCP_STR);

        private static readonly Utf16PreparedFormat<DateTime, string, string, Exception, string, string, int>
            FORMAT_W_PFX_EXCP = new(FMT_W_PFX_EXCP_STR);

        private static readonly Utf16PreparedFormat<DateTime, string, string, string, string, int>
            FORMAT_W_PFX = new(FMT_W_PFX_STR);

        #endregion

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
                var time = DateTime.Now;
                var fileName = GetFileNameFromPath(filePath);
                var logLevelString = GetLogLevelString(level, CreateLogLevelString);

                string result;

                if ((prefix != null) && (exception != null))
                {
                    result = FORMAT_W_PFX_EXCP.Format(
                        time,
                        logLevelString,
                        prefix,
                        exception,
                        fileName,
                        memberName,
                        lineNumber
                    );
                }
                else if (exception != null)
                {
                    result = FORMAT_W_EXCP.Format(
                        time,
                        logLevelString,
                        exception,
                        fileName,
                        memberName,
                        lineNumber
                    );
                }
                else if (prefix != null)
                {
                    result = FORMAT_W_PFX.Format(
                        time,
                        logLevelString,
                        prefix,
                        fileName,
                        memberName,
                        lineNumber
                    );
                }
                else
                {
                    result = FORMAT.Format(time, logLevelString, fileName, memberName, lineNumber);
                }

                return result;
            }
        }

        private string CreateLogLevelString(LogLevel l)
        {
            using (_PRF_CreateLogLevelString.Auto())
            {
                return l.ToString().ToUpperInvariant();
            }
        }

        private string GetFileNameFromPath(string filePath)
        {
            using (_PRF_GetFileNameFromPath.Auto())
            {
                return GetFileNameFromPathInternal(filePath, null);
            }
        }

        #region Profiling

        private const string _PRF_PFX = nameof(FileLogFormatter) + ".";

        private static readonly ProfilerMarker _PRF_GetFileNameFromPath =
            new ProfilerMarker(_PRF_PFX + nameof(GetFileNameFromPath));

        private static readonly ProfilerMarker _PRF_GetLogPrefix =
            new ProfilerMarker(_PRF_PFX + nameof(GetLogPrefix));

        private static readonly ProfilerMarker _PRF_CreateLogLevelString =
            new ProfilerMarker(_PRF_PFX + nameof(CreateLogLevelString));

        #endregion
    }
}
