using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Profiling;

// ReSharper disable InvalidXmlDocComment
// ReSharper disable ExplicitCallerInfoArgument

namespace Appalachia.Utility.Logging.Contexts
{
    public abstract class AppaLogContextBase
    {
        #region Profiling And Tracing Markers

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
        private static readonly ProfilerMarker _PRF_Warning = new ProfilerMarker(_PRF_PFX + nameof(Warning));

        #endregion

        protected abstract string LogPrefix { get; }

        /// <summary>
        ///     <para>Logs a warning message to the console.</para>
        /// </summary>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        /// <param name="context">Object to which the message applies.</param>
        /// <param name="logIf">Whether or not to log.</param>
        [DebuggerStepperBoundary]
        public void Critical(
            object message,
            UnityEngine.Object context = null,
            bool logIf = true,
            [CallerMemberName] string memberName = null,
            [CallerFilePath] string filePath = null,
            [CallerLineNumber] int lineNumber = 0)
        {
            using (_PRF_Critical.Auto())
            {
                AppaLog.Critical($"{LogPrefix} {message}", context, logIf, memberName, filePath, lineNumber);
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
                AppaLog.Debug($"{LogPrefix} {message}", context, logIf, memberName, filePath, lineNumber);
            }
        }

        /// <summary>
        ///     <para>Logs an error message to the console.</para>
        /// </summary>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        /// <param name="context">Object to which the message applies.</param>
        /// <param name="logIf">Whether or not to log.</param>
        [DebuggerStepperBoundary]
        public void Error(
            object message,
            UnityEngine.Object context = null,
            bool logIf = true,
            [CallerMemberName] string memberName = null,
            [CallerFilePath] string filePath = null,
            [CallerLineNumber] int lineNumber = 0)
        {
            using (_PRF_Error.Auto())
            {
                AppaLog.Error($"{LogPrefix} {message}", context, logIf, memberName, filePath, lineNumber);
            }
        }

        /// <summary>
        ///     <para>Logs an exception to the console.</para>
        /// </summary>
        /// <param name="exception">String for display.</param>
        /// <param name="context">Object to which the message applies.</param>
        /// <param name="logIf">Whether or not to log.</param>
        [DebuggerStepperBoundary]
        public void Exception(
            string exception,
            UnityEngine.Object context = null,
            bool logIf = true,
            [CallerMemberName] string memberName = null,
            [CallerFilePath] string filePath = null,
            [CallerLineNumber] int lineNumber = 0)
        {
            using (_PRF_Exception.Auto())
            {
                AppaLog.Exception(
                    $"{LogPrefix} {exception}",
                    context,
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
                AppaLog.Exception(
                    $"{LogPrefix} {exception}",
                    context,
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
        /// <param name="logIf">Whether or not to log.</param>
        [DebuggerStepperBoundary]
        public void Fatal(
            object message,
            UnityEngine.Object context = null,
            bool logIf = true,
            [CallerMemberName] string memberName = null,
            [CallerFilePath] string filePath = null,
            [CallerLineNumber] int lineNumber = 0)
        {
            using (_PRF_Fatal.Auto())
            {
                AppaLog.Fatal($"{LogPrefix} {message}", context, logIf, memberName, filePath, lineNumber);
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
                AppaLog.Info($"{LogPrefix} {message}", context, logIf, memberName, filePath, lineNumber);
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
                AppaLog.Log(
                    level,
                    $"{LogPrefix} {message}",
                    context,
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
                AppaLog.Trace($"{LogPrefix} {message}", context, logIf, memberName, filePath, lineNumber);
            }
        }

        /// <summary>
        ///     <para>Logs a warning message to the console.</para>
        /// </summary>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        /// <param name="context">Object to which the message applies.</param>
        /// <param name="logIf">Whether or not to log.</param>
        [DebuggerStepperBoundary]
        public void Warning(
            object message,
            UnityEngine.Object context = null,
            bool logIf = true,
            [CallerMemberName] string memberName = null,
            [CallerFilePath] string filePath = null,
            [CallerLineNumber] int lineNumber = 0)
        {
            using (_PRF_Warning.Auto())
            {
                AppaLog.Warning($"{LogPrefix} {message}", context, logIf, memberName, filePath, lineNumber);
            }
        }
    }
}
