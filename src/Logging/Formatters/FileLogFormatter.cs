using System;
using Unity.Profiling;

namespace Appalachia.Utility.Logging.Formatters
{
    public class FileLogFormatter : AppaLogFormatter
    {
        #region Profiling And Tracing Markers

        private const string _PRF_PFX = nameof(FileLogFormatter) + ".";

        private static readonly ProfilerMarker _PRF_GetFileNameFromPath =
            new ProfilerMarker(_PRF_PFX + nameof(GetFileNameFromPath));

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
                const string appalachia = "Appalachia";

                var time = $"{DateTime.Now:O}";
                var fileName = GetFileNameFromPath(filePath);
                var logLevelString = GetLogLevelString(level, l => l.ToString().ToUpperInvariant());

                var prefix = $"{time} {logLevelString} [{appalachia}] {fileName}.{memberName}:{lineNumber} ";
                return prefix;
            }
        }

        private string GetFileNameFromPath(string filePath)
        {
            using (_PRF_GetFileNameFromPath.Auto())
            {
                return GetFileNameFromPathInternal(filePath, fn => fn);
            }
        }
    }
}
