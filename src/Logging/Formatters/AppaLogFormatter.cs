using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Appalachia.Utility.Strings;
using Unity.Profiling;

// ReSharper disable FormatStringProblem

namespace Appalachia.Utility.Logging.Formatters
{
    public abstract class AppaLogFormatter
    {
        #region Constants and Static Readonly

        private static readonly Utf16PreparedFormat<string, string> _messageFormatter = new("{0}{1}");

        #endregion

        #region Fields and Autoproperties

        protected Dictionary<LogLevel, string> _logLevelStrings;
        protected Dictionary<string, string> _fileNames;

        #endregion

        public string FormatLogMessage(
            LogLevel level,
            string prefix,
            string formattedPrefix,
            object content,
            object context,
            Exception exception,
            string memberName,
            string filePath,
            int lineNumber)
        {
            using (_PRF_FormatLogMessage.Auto())
            {
                var fullPrefix = GetLogPrefix(
                    level,
                    prefix,
                    formattedPrefix,
                    context,
                    exception,
                    memberName,
                    filePath,
                    lineNumber
                );

                var messageContent = AlterContent(content.ToString(), level);

                return _messageFormatter.Format(fullPrefix, messageContent);
            }
        }

        protected abstract string GetLogPrefix(
            LogLevel level,
            string prefix,
            string formattedPrefix,
            object context,
            Exception exception,
            string memberName,
            string filePath,
            int lineNumber);

        protected virtual string AlterContent(string content, LogLevel level)
        {
            return content;
        }

        protected string GetFileNameFromPathInternal(string filePath, Func<string, string> adjustment)
        {
            using (_PRF_GetFileNameFromPathInternal.Auto())
            {
                _fileNames ??= new Dictionary<string, string>();

                string fileName = null;

                if (!_fileNames.ContainsKey(filePath))
                {
                    var rawFileName = Path.GetFileNameWithoutExtension(filePath);

                    if (adjustment != null)
                    {
                        fileName = adjustment(rawFileName);
                    }

                    _fileNames.Add(filePath, fileName);
                }
                else
                {
                    fileName = _fileNames[filePath];
                }

                return fileName;
            }
        }

        protected string GetLogLevelString(LogLevel level, Func<LogLevel, string> adjustment)
        {
            using (_PRF_GetLogLevelString.Auto())
            {
                _logLevelStrings ??= InitializeLogLevelStringLookup(adjustment);

                return _logLevelStrings[level];
            }
        }

        private static Dictionary<LogLevel, string> InitializeLogLevelStringLookup(
            Func<LogLevel, string> adjustment)
        {
            using (_PRF_InitializeLogLevelStringLookup.Auto())
            {
                var lookup = new Dictionary<LogLevel, string>();

                foreach (var value in Enum.GetValues(typeof(LogLevel)).Cast<LogLevel>())
                {
                    var adjustmentValue = adjustment(value);

                    lookup.Add(value, adjustmentValue);
                }

                return lookup;
            }
        }

        #region Profiling

        private const string _PRF_PFX = nameof(AppaLogFormatter) + ".";

        private static readonly ProfilerMarker _PRF_GetLogLevelString =
            new ProfilerMarker(_PRF_PFX + nameof(GetLogLevelString));

        private static readonly ProfilerMarker _PRF_FormatLogMessage =
            new ProfilerMarker(_PRF_PFX + nameof(FormatLogMessage));

        private static readonly ProfilerMarker _PRF_GetFileNameFromPathInternal =
            new ProfilerMarker(_PRF_PFX + nameof(GetFileNameFromPathInternal));

        private static readonly ProfilerMarker _PRF_InitializeLogLevelStringLookup =
            new ProfilerMarker(_PRF_PFX + nameof(InitializeLogLevelStringLookup));

        #endregion
    }
}
