using System;
using System.Runtime.CompilerServices;
using Appalachia.Utility.Logging.Formatters;
using Unity.Profiling;
using UnityEngine;
using Object = UnityEngine.Object;

// ReSharper disable InvalidXmlDocComment

namespace Appalachia.Utility.Logging
{
    public static partial class AppaLog
    {
        #region Constants and Static Readonly

        private const bool TRACING_ENABLED = false;
        private const int MENU_PRIORITY = PKG.Menu.Appalachia.Logging.Priority;
        private const string MENU_BASE = PKG.Menu.Appalachia.Logging.Base;

        #endregion

        #region Static Fields and Autoproperties

        private static AppaLogFormatter _consoleLogFormatter;

        private static AppaLogFormatter _traceLogFormatter;

        #endregion

        /// <summary>
        ///     <para>Logs a warning message to the console.</para>
        /// </summary>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        /// <param name="context">Object to which the message applies.</param>
        /// <param name="exception">The exception that occurred.</param>
        /// <param name="logIf">Whether or not to log.</param>
        [System.Diagnostics.DebuggerStepperBoundary]
        public static void Critical(
            object message,
            Object context = null,
            Exception exception = null,
            bool logIf = true,
            [CallerMemberName] string memberName = null,
            [CallerFilePath] string filePath = null,
            [CallerLineNumber] int lineNumber = 0)
        {
            using (_PRF_Critical.Auto())
            {
                LogInternal(
                    LogLevel.Critical,
                    null,
                    null,
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
        [System.Diagnostics.DebuggerStepperBoundary]
        public static void Debug(
            object message,
            Object context = null,
            bool logIf = true,
            [CallerMemberName] string memberName = null,
            [CallerFilePath] string filePath = null,
            [CallerLineNumber] int lineNumber = 0)
        {
            using (_PRF_Debug.Auto())
            {
                LogInternal(
                    LogLevel.Debug,
                    null,
                    null,
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
        [System.Diagnostics.DebuggerStepperBoundary]
        public static void Error(
            object message,
            Object context = null,
            Exception exception = null,
            bool logIf = true,
            [CallerMemberName] string memberName = null,
            [CallerFilePath] string filePath = null,
            [CallerLineNumber] int lineNumber = 0)
        {
            using (_PRF_Error.Auto())
            {
                LogInternal(
                    exception == null ? LogLevel.Error : LogLevel.Exception,
                    null,
                    null,
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
        [System.Diagnostics.DebuggerStepperBoundary]
        public static void Fatal(
            string message,
            Object context = null,
            Exception exception = null,
            bool logIf = true,
            [CallerMemberName] string memberName = null,
            [CallerFilePath] string filePath = null,
            [CallerLineNumber] int lineNumber = 0)
        {
            using (_PRF_Fatal.Auto())
            {
                LogInternal(
                    LogLevel.Fatal,
                    null,
                    null,
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
        [System.Diagnostics.DebuggerStepperBoundary]
        public static void Info(
            object message,
            Object context = null,
            bool logIf = true,
            [CallerMemberName] string memberName = null,
            [CallerFilePath] string filePath = null,
            [CallerLineNumber] int lineNumber = 0)
        {
            using (_PRF_Info.Auto())
            {
                LogInternal(LogLevel.Info, null, null, message, context, null, logIf, memberName, filePath, lineNumber);
            }
        }

        /// <summary>
        ///     <para>Logs a message to the console with the specified log level.</para>
        /// </summary>
        /// <param name="level">The log level.</param>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        /// <param name="context">Object to which the message applies.</param>
        /// <param name="logIf">Whether or not to log.</param>
        [System.Diagnostics.DebuggerStepperBoundary]
        public static void Log(
            LogLevel level,
            object message,
            Object context = null,
            bool logIf = true,
            [CallerMemberName] string memberName = null,
            [CallerFilePath] string filePath = null,
            [CallerLineNumber] int lineNumber = 0)
        {
            using (_PRF_Log.Auto())
            {
                LogInternal(level, null, null, message, context, null, logIf, memberName, filePath, lineNumber);
            }
        }

        /// <summary>
        ///     <para>Logs a trace message to the log file.</para>
        /// </summary>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        /// <param name="context">Object to which the message applies.</param>
        /// <param name="logIf">Whether or not to log.</param>
        [System.Diagnostics.DebuggerStepperBoundary]
        public static void Trace(
            string message,
            Object context = null,
            bool logIf = true,
            [CallerMemberName] string memberName = null,
            [CallerFilePath] string filePath = null,
            [CallerLineNumber] int lineNumber = 0)
        {
            using (_PRF_Trace.Auto())
            {
                LogInternal(
                    LogLevel.Trace,
                    null,
                    null,
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
        [System.Diagnostics.DebuggerStepperBoundary]
        public static void Warn(
            object message,
            Object context = null,
            bool logIf = true,
            [CallerMemberName] string memberName = null,
            [CallerFilePath] string filePath = null,
            [CallerLineNumber] int lineNumber = 0)
        {
            using (_PRF_Warn.Auto())
            {
                LogInternal(LogLevel.Warn, null, null, message, context, null, logIf, memberName, filePath, lineNumber);
            }
        }

        [System.Diagnostics.DebuggerStepperBoundary]
        internal static void LogInternal(
            LogLevel level,
            string prefix,
            string formattedPrefix,
            object message,
            object context,
            Exception exception,
            bool logIf,
            string memberName,
            string filePath,
            int lineNumber)
        {
            using (_PRF_LogInternal.Auto())
            {
                if (!logIf)
                {
                    return;
                }

                _consoleLogFormatter ??= new UnityConsoleFormatter();
                _traceLogFormatter ??= new FileLogFormatter();

#if UNITY_EDITOR
                if (!_hasLoggedPreviously)
                {
                    LogInitialMessage();
                }
#endif

                if (level != LogLevel.Trace)
                {
                    object consoleMessage = _consoleLogFormatter.FormatLogMessage(
                        level,
                        prefix,
                        formattedPrefix,
                        message,
                        context,
                        exception,
                        memberName,
                        filePath,
                        lineNumber
                    );

                    var logType = LogLevelToLogType(level);
                    using (_PRF_LogInternal_UnityInternal.Auto())
                    {
                        UnityEngine.Debug.unityLogger.Log(logType, consoleMessage, context is Object o ? o : null);
                    }

                    if (exception != null)
                    {
                        UnityEngine.Debug.unityLogger.LogException(exception);
                    }
                }

// ReSharper disable HeuristicUnreachableCode
#pragma warning disable CS0162
                if (TRACING_ENABLED)
                {
                    LogInternalTrace(
                        level,
                        prefix,
                        formattedPrefix,
                        message,
                        context,
                        exception,
                        memberName,
                        filePath,
                        lineNumber
                    );
                }

// ReSharper restore HeuristicUnreachableCode
#pragma warning restore CS0162
            }
        }

        private static void LogInternalTrace(
            LogLevel level,
            string prefix,
            string formattedPrefix,
            object message,
            object context,
            Exception exception,
            string memberName,
            string filePath,
            int lineNumber)
        {
            using (_PRF_LogInternalTrace.Auto())
            {
                var traceLogMessage = _traceLogFormatter.FormatLogMessage(
                    level,
                    prefix,
                    formattedPrefix,
                    message,
                    context,
                    exception,
                    memberName,
                    filePath,
                    lineNumber
                );

                System.Diagnostics.Debug.Write(traceLogMessage);
                Console.WriteLine(traceLogMessage);
            }
        }

        private static LogType LogLevelToLogType(LogLevel level)
        {
            using (_PRF_LogLevelToLogType.Auto())
            {
                switch (level)
                {
                    case LogLevel.Trace:
                        return LogType.Log;
                    case LogLevel.Debug:
                        return LogType.Log;
                    case LogLevel.Info:
                        return LogType.Log;
                    case LogLevel.Warn:
                        return LogType.Warning;
                    case LogLevel.Error:
                        return LogType.Error;
                    case LogLevel.Exception:
                        return LogType.Exception;
                    case LogLevel.Critical:
                        return LogType.Exception;
                    case LogLevel.Fatal:
                        return LogType.Exception;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(level), level, null);
                }
            }
        }

        #region Profiling

        private const string _PRF_PFX = nameof(AppaLog) + ".";

        private static readonly ProfilerMarker _PRF_LogInternal_UnityInternal =
            new ProfilerMarker(_PRF_PFX + nameof(LogInternal) + ".UnityInternal");

        private static readonly ProfilerMarker _PRF_Debug = new(_PRF_PFX + nameof(Debug));
        private static readonly ProfilerMarker _PRF_Error = new(_PRF_PFX + nameof(Error));

        private static readonly ProfilerMarker _PRF_LogInternalTrace =
            new ProfilerMarker(_PRF_PFX + nameof(LogInternalTrace));

        private static readonly ProfilerMarker _PRF_Info = new(_PRF_PFX + nameof(Info));
        private static readonly ProfilerMarker _PRF_Log = new(_PRF_PFX + nameof(Log));
        private static readonly ProfilerMarker _PRF_Trace = new(_PRF_PFX + nameof(Trace));
        private static readonly ProfilerMarker _PRF_Warn = new(_PRF_PFX + nameof(Warn));
        private static readonly ProfilerMarker _PRF_LogInternal = new(_PRF_PFX + nameof(LogInternal));
        private static readonly ProfilerMarker _PRF_Critical = new(_PRF_PFX + nameof(Critical));

        private static readonly ProfilerMarker _PRF_Fatal = new(_PRF_PFX + nameof(Fatal));

        private static readonly ProfilerMarker _PRF_LogLevelToLogType = new(_PRF_PFX + nameof(LogLevelToLogType));

        #endregion

        /*private const string MENU_DEBUGGER = MENU_BASE + "Debugger Boundary";*/
    }
}
