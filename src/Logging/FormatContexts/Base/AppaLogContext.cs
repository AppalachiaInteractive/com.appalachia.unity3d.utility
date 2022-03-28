using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Unity.Profiling;

namespace Appalachia.Utility.Logging
{
    [SuppressMessage("ReSharper", "ExplicitCallerInfoArgument")]
    public sealed class AppaLogContext
    {
        public AppaLogContext([NotNull] string logPrefix)
        {
            _logPrefix = logPrefix;
            _format = AppaLogFormats.contexts[logPrefix.ToUpperInvariant()];
        }

        #region Fields and Autoproperties

        private readonly AppaLogFormats.LogFormat _format;

        private string _formattedLogPrefix;

        private string _logPrefix;

        #endregion

        internal string LogPrefix
        {
            get
            {
                using (_PRF_LogPrefix.Auto())
                {
                    return _logPrefix;
                }
            }
        }

        internal string LogPrefixFormatted
        {
            get
            {
                using (_PRF_LogPrefixFormatted.Auto())
                {
                    if (_formattedLogPrefix == null)
                    {
                        _formattedLogPrefix = _format.Format(_logPrefix);
                    }

                    return _formattedLogPrefix;
                }
            }
        }

        /// <summary>
        ///     <para>Logs a warning message to the console.</para>
        /// </summary>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        /// <param name="context">Object to which the message applies.</param>
        /// <param name="exception">The exception that occured.</param>
        /// <param name="logIf">Whether or not to log.</param>
        [DebuggerStepperBoundary]
        public void Critical(
            object message,
            UnityEngine.Object context = null,
            Exception exception = null,
            bool logIf = true,
            [CallerMemberName] string memberName = null,
            [CallerFilePath] string filePath = null,
            [CallerLineNumber] int lineNumber = 0)
        {
            using (_PRF_Critical.Auto())
            {
                AppaLog.LogInternal(
                    LogLevel.Critical,
                    LogPrefix,
                    LogPrefixFormatted,
                    message,
                    context,
                    exception,
                    logIf,
                    memberName,
                    filePath,
                    lineNumber
                );
            }
        }

        /// <summary>
        ///     <para>Logs a debug message to the console.</para>
        /// </summary>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        /// <param name="context">Object to which the message applies.</param>
        /// <param name="logIf">Whether or not to log.</param>
        [DebuggerStepperBoundary]
        public void Debug(
            object message,
            UnityEngine.Object context = null,
            bool logIf = true,
            [CallerMemberName] string memberName = null,
            [CallerFilePath] string filePath = null,
            [CallerLineNumber] int lineNumber = 0)
        {
            using (_PRF_Debug.Auto())
            {
                AppaLog.LogInternal(
                    LogLevel.Debug,
                    LogPrefix,
                    LogPrefixFormatted,
                    message,
                    context,
                    null,
                    logIf,
                    memberName,
                    filePath,
                    lineNumber
                );
            }
        }

        /// <summary>
        ///     <para>Logs an error message to the console.</para>
        /// </summary>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        /// <param name="context">Object to which the message applies.</param>
        /// <param name="exception">The exception that occurred.</param>
        /// <param name="logIf">Whether or not to log.</param>
        [DebuggerStepperBoundary]
        public void Error(
            object message,
            UnityEngine.Object context = null,
            Exception exception = null,
            bool logIf = true,
            [CallerMemberName] string memberName = null,
            [CallerFilePath] string filePath = null,
            [CallerLineNumber] int lineNumber = 0)
        {
            using (_PRF_Error.Auto())
            {
                AppaLog.LogInternal(
                    exception == null ? LogLevel.Error : LogLevel.Exception,
                    LogPrefix,
                    LogPrefixFormatted,
                    message,
                    context,
                    exception,
                    logIf,
                    memberName,
                    filePath,
                    lineNumber
                );
            }
        }

        /// <summary>
        ///     <para>Logs a fatal message to the console.</para>
        /// </summary>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        /// <param name="context">Object to which the message applies.</param>
        /// <param name="exception">The exception that occurred.</param>
        /// <param name="logIf">Whether or not to log.</param>
        [DebuggerStepperBoundary]
        public void Fatal(
            object message,
            UnityEngine.Object context = null,
            Exception exception = null,
            bool logIf = true,
            [CallerMemberName] string memberName = null,
            [CallerFilePath] string filePath = null,
            [CallerLineNumber] int lineNumber = 0)
        {
            using (_PRF_Fatal.Auto())
            {
                AppaLog.LogInternal(
                    LogLevel.Fatal,
                    LogPrefix,
                    LogPrefixFormatted,
                    message,
                    context,
                    exception,
                    logIf,
                    memberName,
                    filePath,
                    lineNumber
                );
            }
        }

        /// <summary>
        ///     <para>Logs an informational message to the console.</para>
        /// </summary>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        /// <param name="context">Object to which the message applies.</param>
        /// <param name="logIf">Whether or not to log.</param>
        [DebuggerStepperBoundary]
        public void Info(
            object message,
            UnityEngine.Object context = null,
            bool logIf = true,
            [CallerMemberName] string memberName = null,
            [CallerFilePath] string filePath = null,
            [CallerLineNumber] int lineNumber = 0)
        {
            using (_PRF_Info.Auto())
            {
                AppaLog.LogInternal(
                    LogLevel.Info,
                    LogPrefix,
                    LogPrefixFormatted,
                    message,
                    context,
                    null,
                    logIf,
                    memberName,
                    filePath,
                    lineNumber
                );
            }
        }

        /// <summary>
        ///     <para>Logs a message to the console with the specified log level.</para>
        /// </summary>
        /// <param name="level">The log level.</param>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        /// <param name="context">Object to which the message applies.</param>
        /// <param name="logIf">Whether or not to log.</param>
        [DebuggerStepperBoundary]
        public void Log(
            LogLevel level,
            object message,
            UnityEngine.Object context = null,
            bool logIf = true,
            [CallerMemberName] string memberName = null,
            [CallerFilePath] string filePath = null,
            [CallerLineNumber] int lineNumber = 0)
        {
            using (_PRF_Log.Auto())
            {
                AppaLog.LogInternal(
                    level,
                    LogPrefix,
                    LogPrefixFormatted,
                    message,
                    context,
                    null,
                    logIf,
                    memberName,
                    filePath,
                    lineNumber
                );
            }
        }

        /*
        /// <summary>
        ///     <para>Logs a trace message to the log file.</para>
        /// </summary>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        /// <param name="context">Object to which the message applies.</param>
        /// <param name="logIf">Whether or not to log.</param>
        [DebuggerStepperBoundary]
        public void Trace(
            string message,
            UnityEngine.Object context = null,
            bool logIf = true,
            [CallerMemberName] string memberName = null,
            [CallerFilePath] string filePath = null,
            [CallerLineNumber] int lineNumber = 0)
        {
            using (_PRF_Trace.Auto())
            {
                AppaLog.LogInternal(
                    LogLevel.Trace,
                    LogPrefix,
                    LogPrefixFormatted,
                    message,
                    context,
                    null,
                    logIf,
                    memberName,
                    filePath,
                    lineNumber
                );
            }
        }*/

        /// <summary>
        ///     <para>Logs a warning message to the console.</para>
        /// </summary>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        /// <param name="context">Object to which the message applies.</param>
        /// <param name="logIf">Whether or not to log.</param>
        [DebuggerStepperBoundary]
        public void Warn(
            object message,
            UnityEngine.Object context = null,
            bool logIf = true,
            [CallerMemberName] string memberName = null,
            [CallerFilePath] string filePath = null,
            [CallerLineNumber] int lineNumber = 0)
        {
            using (_PRF_Warn.Auto())
            {
                AppaLog.LogInternal(
                    LogLevel.Warn,
                    LogPrefix,
                    LogPrefixFormatted,
                    message,
                    context,
                    null,
                    logIf,
                    memberName,
                    filePath,
                    lineNumber
                );
            }
        }

        internal void Test()
        {
            var line = 436;
            var step = 11;

            Fatal(
                "Testing 1 2 3...",
                null,
                new NotSupportedException("Testing 1 2 3..."),
                true,
                nameof(Test),
                "AppaLog.cs",
                line + (step * 1)
            );

            Critical(
                "Testing 1 2 3...",
                null,
                new NotSupportedException("Testing 1 2 3..."),
                true,
                nameof(Test),
                "AppaLog.cs",
                line + (step * 2)
            );

            Error("Testing 1 2 3...", null, null, true, nameof(Test), "AppaLog.cs", line + (step * 4));

            Error(
                "Testing 1 2 3...",
                null,
                new NotSupportedException("Testing 1 2 3..."),
                true,
                nameof(Test),
                "AppaLog.cs",
                line + (step * 4)
            );

            Warn("Testing 1 2 3...", null, true, nameof(Test), "AppaLog.cs", line + (step * 5));

            Info("Testing 1 2 3...", null, true, nameof(Test), "AppaLog.cs", line + (step * 6));

            Debug("Testing 1 2 3...", null, true, nameof(Test), "AppaLog.cs", line + (step * 7));

            /*Trace("Testing 1 2 3...", null, true, nameof(Test), "AppaLog.cs", line + (step * 8));*/
        }

        #region Profiling

        private const string _PRF_PFX = nameof(AppaLogContext) + ".";

        private static readonly ProfilerMarker
            _PRF_Critical = new ProfilerMarker(_PRF_PFX + nameof(Critical));

        private static readonly ProfilerMarker _PRF_Debug = new ProfilerMarker(_PRF_PFX + nameof(Debug));
        private static readonly ProfilerMarker _PRF_Error = new ProfilerMarker(_PRF_PFX + nameof(Error));

        private static readonly ProfilerMarker _PRF_Fatal = new ProfilerMarker(_PRF_PFX + nameof(Fatal));
        private static readonly ProfilerMarker _PRF_Info = new ProfilerMarker(_PRF_PFX + nameof(Info));

        private static readonly ProfilerMarker _PRF_Log = new ProfilerMarker(_PRF_PFX + nameof(Log));

        private static readonly ProfilerMarker _PRF_LogPrefix =
            new ProfilerMarker(_PRF_PFX + nameof(LogPrefix));

        private static readonly ProfilerMarker _PRF_LogPrefixFormatted =
            new ProfilerMarker(_PRF_PFX + nameof(LogPrefixFormatted));

        private static readonly ProfilerMarker _PRF_Trace = new ProfilerMarker(_PRF_PFX + nameof(Trace));
        private static readonly ProfilerMarker _PRF_Warn = new ProfilerMarker(_PRF_PFX + nameof(Warn));

        #endregion
    }
}
