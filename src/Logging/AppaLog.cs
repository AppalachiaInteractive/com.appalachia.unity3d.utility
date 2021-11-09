using System;
using System.Runtime.CompilerServices;
using Appalachia.Utility.Logging.Formatters;
using Unity.Profiling;
using UnityEngine;
using Object = UnityEngine.Object;

// ReSharper disable InvalidXmlDocComment

namespace Appalachia.Utility.Logging
{
    public static class AppaLog
    {
        #region Profiling And Tracing Markers

        private const string _PRF_PFX = nameof(AppaLog) + ".";

        private static readonly ProfilerMarker _PRF_ClearDeveloperConsole =
            new ProfilerMarker(_PRF_PFX + nameof(ClearDeveloperConsole));

        private static readonly ProfilerMarker _PRF_Debug = new ProfilerMarker(_PRF_PFX + nameof(Debug));
        private static readonly ProfilerMarker _PRF_Error = new ProfilerMarker(_PRF_PFX + nameof(Error));

        private static readonly ProfilerMarker _PRF_Exception =
            new ProfilerMarker(_PRF_PFX + nameof(Exception));

        private static readonly ProfilerMarker _PRF_Info = new ProfilerMarker(_PRF_PFX + nameof(Info));
        private static readonly ProfilerMarker _PRF_Log = new ProfilerMarker(_PRF_PFX + nameof(Log));

        private static readonly ProfilerMarker _PRF_Trace = new ProfilerMarker(_PRF_PFX + nameof(Trace));
        private static readonly ProfilerMarker _PRF_Warning = new ProfilerMarker(_PRF_PFX + nameof(Warning));

        private static readonly ProfilerMarker _PRF_LogInternal =
            new ProfilerMarker(_PRF_PFX + nameof(LogInternal));

        private static readonly ProfilerMarker _PRF_LogLevelToLogType =
            new ProfilerMarker(_PRF_PFX + nameof(LogLevelToLogType));

        #endregion

        #region Constants and Static Readonly

        private const int MENU_PRIORITY = PKG.Menu.Appalachia.Logging.Priority;

        private const string MENU_BASE = PKG.Menu.Appalachia.Logging.Base + "Test";

        #endregion

        private static AppaLogFormatter _consoleLogFormatter;

        private static AppaLogFormatter _traceLogFormatter;

        #region Menu Items

        [UnityEditor.MenuItem(MENU_BASE, false, MENU_PRIORITY+0)]
        private static void Test()
        {
            var line = 58;
            LogInternal(LogLevel.Fatal, "Testing 1 2 3...", null, true, nameof(Test), "AppaLog.cs", line+1);

            LogInternal(LogLevel.Critical, "Testing 1 2 3...", null, true, nameof(Test), "AppaLog.cs", line+3);

            LogInternal(LogLevel.Exception, "Testing 1 2 3...", null, true, nameof(Test), "AppaLog.cs", line+5);

            LogInternal(LogLevel.Error, "Testing 1 2 3...", null, true, nameof(Test), "AppaLog.cs", line+7);

            LogInternal(LogLevel.Warn, "Testing 1 2 3...", null, true, nameof(Test), "AppaLog.cs", line+9);
  
            LogInternal(LogLevel.Info, "Testing 1 2 3...", null, true, nameof(Test), "AppaLog.cs", line+11);
 
            LogInternal(LogLevel.Debug, "Testing 1 2 3...", null, true, nameof(Test), "AppaLog.cs", line+13);
            
            LogInternal(LogLevel.Trace, "Testing 1 2 3...", null, true, nameof(Test), "AppaLog.cs", line+15);
        }

        #endregion

        /// <summary>
        ///     <para>Clears errors from the developer console.</para>
        /// </summary>
        public static void ClearDeveloperConsole()
        {
            using (_PRF_ClearDeveloperConsole.Auto())
            {
                UnityEngine.Debug.ClearDeveloperConsole();
            }
        }

        /// <summary>
        ///     <para>Logs a warning message to the console.</para>
        /// </summary>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        /// <param name="context">Object to which the message applies.</param>
        /// <param name="logIf">Whether or not to log.</param>
        public static void Critical(
            object message,
            Object context = null,
            bool logIf = true,
            [CallerMemberName] string memberName = null,
            [CallerFilePath] string filePath = null,
            [CallerLineNumber] int lineNumber = 0)
        {
            using (_PRF_Warning.Auto())
            {
                LogInternal(LogLevel.Critical, message, context, logIf, memberName, filePath, lineNumber);
            }
        }

        /// <summary>
        ///     <para>Logs a debug message to the console.</para>
        /// </summary>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        /// <param name="context">Object to which the message applies.</param>
        /// <param name="logIf">Whether or not to log.</param>
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
                LogInternal(LogLevel.Debug, message, context, logIf, memberName, filePath, lineNumber);
            }
        }

        /// <summary>
        ///     <para>Logs an error message to the console.</para>
        /// </summary>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        /// <param name="context">Object to which the message applies.</param>
        /// <param name="logIf">Whether or not to log.</param>
        public static void Error(
            object message,
            Object context = null,
            bool logIf = true,
            [CallerMemberName] string memberName = null,
            [CallerFilePath] string filePath = null,
            [CallerLineNumber] int lineNumber = 0)
        {
            using (_PRF_Error.Auto())
            {
                LogInternal(LogLevel.Error, message, context, logIf, memberName, filePath, lineNumber);
            }
        }

        /// <summary>
        ///     <para>Logs an exception to the console.</para>
        /// </summary>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        /// <param name="context">Object to which the message applies.</param>
        /// <param name="logIf">Whether or not to log.</param>
        public static void Exception(
            Exception exception,
            Object context = null,
            bool logIf = true,
            [CallerMemberName] string memberName = null,
            [CallerFilePath] string filePath = null,
            [CallerLineNumber] int lineNumber = 0)
        {
            using (_PRF_Exception.Auto())
            {
                LogInternal(
                    LogLevel.Critical,
                    exception.ToString(),
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
        public static void Fatal(
            object message,
            Object context = null,
            bool logIf = true,
            [CallerMemberName] string memberName = null,
            [CallerFilePath] string filePath = null,
            [CallerLineNumber] int lineNumber = 0)
        {
            using (_PRF_Warning.Auto())
            {
                LogInternal(LogLevel.Fatal, message, context, logIf, memberName, filePath, lineNumber);
            }
        }

        /// <summary>
        ///     <para>Logs an informational message to the console.</para>
        /// </summary>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        /// <param name="context">Object to which the message applies.</param>
        /// <param name="logIf">Whether or not to log.</param>
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
                LogInternal(LogLevel.Info, message, context, logIf, memberName, filePath, lineNumber);
            }
        }

        /// <summary>
        ///     <para>Logs a message to the console with the specified log level.</para>
        /// </summary>
        /// <param name="level">The log level.</param>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        /// <param name="context">Object to which the message applies.</param>
        /// <param name="logIf">Whether or not to log.</param>
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
                LogInternal(level, message, context, logIf, memberName, filePath, lineNumber);
            }
        }

        /// <summary>
        ///     <para>Logs a trace message to the log file.</para>
        /// </summary>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        /// <param name="context">Object to which the message applies.</param>
        /// <param name="logIf">Whether or not to log.</param>
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
                LogInternal(LogLevel.Trace, message, context, logIf, memberName, filePath, lineNumber);
            }
        }

        /// <summary>
        ///     <para>Logs a warning message to the console.</para>
        /// </summary>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        /// <param name="context">Object to which the message applies.</param>
        /// <param name="logIf">Whether or not to log.</param>
        public static void Warning(
            object message,
            Object context = null,
            bool logIf = true,
            [CallerMemberName] string memberName = null,
            [CallerFilePath] string filePath = null,
            [CallerLineNumber] int lineNumber = 0)
        {
            using (_PRF_Warning.Auto())
            {
                LogInternal(LogLevel.Warn, message, context, logIf, memberName, filePath, lineNumber);
            }
        }

        private static void LogInternal(
            LogLevel level,
            object message,
            Object context,
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

                if (level != LogLevel.Trace)
                {
                    var consoleMessage = _consoleLogFormatter.FormatLogMessage(
                        level,
                        message,
                        memberName,
                        filePath,
                        lineNumber
                    );

                    var logType = LogLevelToLogType(level);
                    UnityEngine.Debug.unityLogger.Log(logType, consoleMessage, context);
                }

                var traceLogMessage = _traceLogFormatter.FormatLogMessage(
                    level,
                    message,
                    memberName,
                    filePath,
                    lineNumber
                );

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
    }
}
