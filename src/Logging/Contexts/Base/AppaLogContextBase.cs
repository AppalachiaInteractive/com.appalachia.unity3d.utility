using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Profiling;
using UnityEngine;

// ReSharper disable InvalidXmlDocComment
// ReSharper disable ExplicitCallerInfoArgument

namespace Appalachia.Utility.Logging.Contexts.Base
{
    public abstract class AppaLogContextBase
    {
        protected AppaLogContextBase()
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

        #endregion

        protected abstract string LogPrefix { get; }

        protected abstract string LogPrefixFormatted { get; }

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
                    LogLevel.Error,
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
        ///     <para>Logs an exception to the console.</para>
        /// </summary>
        /// <param name="message">String for display.</param>
        /// <param name="exception">The exception that occurred.</param>
        /// <param name="context">Object to which the message applies.</param>
        /// <param name="logIf">Whether or not to log.</param>
        [DebuggerStepperBoundary]
        public void Exception(
            string message,
            UnityEngine.Object context = null,
            Exception exception = null,
            bool logIf = true,
            [CallerMemberName] string memberName = null,
            [CallerFilePath] string filePath = null,
            [CallerLineNumber] int lineNumber = 0)
        {
            using (_PRF_Exception.Auto())
            {
                AppaLog.LogInternal(
                    LogLevel.Exception,
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
        ///     <para>Logs an exception to the console.</para>
        /// </summary>
        /// <param name="exception">Exception to be converted to string representation for display.</param>
        /// <param name="context">Object to which the message applies.</param>
        /// <param name="logIf">Whether or not to log.</param>
        [DebuggerStepperBoundary]
        public void Exception(
            Exception exception,
            UnityEngine.Object context = null,
            bool logIf = true,
            [CallerMemberName] string memberName = null,
            [CallerFilePath] string filePath = null,
            [CallerLineNumber] int lineNumber = 0)
        {
            using (_PRF_Exception.Auto())
            {
                AppaLog.LogInternal(
                    LogLevel.Exception,
                    LogPrefix,
                    LogPrefixFormatted,
                    exception.Message,
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
        }

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

            Exception(
                "Testing 1 2 3...",
                null,
                new NotSupportedException("Testing 1 2 3..."),
                true,
                nameof(Test),
                "AppaLog.cs",
                line + (step * 3)
            );

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

            Trace("Testing 1 2 3...", null, true, nameof(Test), "AppaLog.cs", line + (step * 8));
        }

        protected abstract AppaLogFormats.LogFormat GetPrefixFormat();

        #region Profiling

        private const string _PRF_PFX = nameof(AppaLogContextBase) + ".";

        private static readonly ProfilerMarker
            _PRF_Critical = new ProfilerMarker(_PRF_PFX + nameof(Critical));

        private static readonly ProfilerMarker _PRF_Debug = new ProfilerMarker(_PRF_PFX + nameof(Debug));
        private static readonly ProfilerMarker _PRF_Error = new ProfilerMarker(_PRF_PFX + nameof(Error));

        private static readonly ProfilerMarker _PRF_Exception =
            new ProfilerMarker(_PRF_PFX + nameof(Exception));

        private static readonly ProfilerMarker _PRF_Fatal = new ProfilerMarker(_PRF_PFX + nameof(Fatal));
        private static readonly ProfilerMarker _PRF_Info = new ProfilerMarker(_PRF_PFX + nameof(Info));
        private static readonly ProfilerMarker _PRF_Log = new ProfilerMarker(_PRF_PFX + nameof(Log));
        private static readonly ProfilerMarker _PRF_Trace = new ProfilerMarker(_PRF_PFX + nameof(Trace));
        private static readonly ProfilerMarker _PRF_Warn = new ProfilerMarker(_PRF_PFX + nameof(Warn));

        #endregion
    }
}
