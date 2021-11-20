using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.Profiling;
using UnityEngine;

namespace Appalachia.Utility.Logging.Formatters
{
    public abstract class AppaLogFormatter
    {
        protected AppaLogFormatter()
        {
            if (formats == null)
            {
                formats = Resources.Load<AppaLogFormats>(AppaLogFormats.ADDRESS);
            }

            if (formats == null)
            {
                formats = ScriptableObject.CreateInstance<AppaLogFormats>();
            }
        }

        #region Fields and Autoproperties

        protected AppaLogFormats formats;

        protected Dictionary<LogLevel, string> _logLevelStrings;
        protected Dictionary<string, string> _fileNames;

        #endregion

        public object FormatLogMessage(
            LogLevel level,
            string prefix,
            string formattedPrefix,
            object content,
            string memberName,
            string filePath,
            int lineNumber)
        {
            var fullPrefix = GetLogPrefix(level, prefix, formattedPrefix, memberName, filePath, lineNumber);

            var messageContent = AlterContent(content.ToString());

            return $"{fullPrefix}{messageContent}";
        }

        protected abstract string GetLogPrefix(
            LogLevel level,
            string prefix,
            string formattedPrefix,
            string memberName,
            string filePath,
            int lineNumber);

        protected virtual string AlterContent(string content)
        {
            return content;
        }

        protected string GetFileNameFromPathInternal(string filePath, Func<string, string> adjustment)
        {
            using (_PRF_GetFileNameFromPathInternal.Auto())
            {
                _fileNames ??= new Dictionary<string, string>();

                string fileName;

                if (!_fileNames.ContainsKey(filePath))
                {
                    var rawFileName = Path.GetFileNameWithoutExtension(filePath);

                    fileName = adjustment(rawFileName);
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
            _logLevelStrings ??= InitializeLogLevelStringLookup(adjustment);

            return _logLevelStrings[level];
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

        private static readonly ProfilerMarker _PRF_GetFileNameFromPathInternal =
            new ProfilerMarker(_PRF_PFX + nameof(GetFileNameFromPathInternal));

        private static readonly ProfilerMarker _PRF_InitializeLogLevelStringLookup =
            new ProfilerMarker(_PRF_PFX + nameof(InitializeLogLevelStringLookup));

        #endregion
    }
}
