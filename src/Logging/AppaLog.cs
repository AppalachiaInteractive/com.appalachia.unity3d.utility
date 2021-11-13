using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Appalachia.Utility.Constants;
using Appalachia.Utility.Logging.Contexts;
using Appalachia.Utility.Logging.Contexts.Base;
using Appalachia.Utility.Logging.Formatters;
using Unity.Profiling;
using UnityEngine;
using Application = Appalachia.Utility.Logging.Contexts.Application;
using Object = UnityEngine.Object;

// ReSharper disable InvalidXmlDocComment

namespace Appalachia.Utility.Logging
{
    public static class AppaLog
    {
        #region Profiling

        private const string _PRF_PFX = nameof(AppaLog) + ".";

#if UNITY_EDITOR
        private static readonly ProfilerMarker _PRF_ClearDeveloperConsole =
            new ProfilerMarker(_PRF_PFX + nameof(ClearDeveloperConsole));
#endif

        private static readonly ProfilerMarker _PRF_Debug = new ProfilerMarker(_PRF_PFX + nameof(Debug));
        private static readonly ProfilerMarker _PRF_Error = new ProfilerMarker(_PRF_PFX + nameof(Error));

        private static readonly ProfilerMarker _PRF_Exception =
            new ProfilerMarker(_PRF_PFX + nameof(Exception));

        private static readonly ProfilerMarker _PRF_Info = new ProfilerMarker(_PRF_PFX + nameof(Info));
        private static readonly ProfilerMarker _PRF_Log = new ProfilerMarker(_PRF_PFX + nameof(Log));
        private static readonly ProfilerMarker _PRF_Trace = new ProfilerMarker(_PRF_PFX + nameof(Trace));
        private static readonly ProfilerMarker _PRF_Warn = new ProfilerMarker(_PRF_PFX + nameof(Warn));

        private static readonly ProfilerMarker _PRF_LogInternal =
            new ProfilerMarker(_PRF_PFX + nameof(LogInternal));

        private static readonly ProfilerMarker
            _PRF_Critical = new ProfilerMarker(_PRF_PFX + nameof(Critical));

        private static readonly ProfilerMarker _PRF_Fatal = new ProfilerMarker(_PRF_PFX + nameof(Fatal));

        private static readonly ProfilerMarker _PRF_LogLevelToLogType =
            new ProfilerMarker(_PRF_PFX + nameof(LogLevelToLogType));

        #endregion

        #region Constants and Static Readonly

        private const int MENU_PRIORITY = PKG.Menu.Appalachia.Logging.Priority;

        private const string MENU_BASE = PKG.Menu.Appalachia.Logging.Base;
        private const string MENU_DEBUGGER = MENU_BASE + "Debugger Boundary";

        #endregion

        #region Fields

        private static AppaLogFormatter _consoleLogFormatter;

        private static AppaLogFormatter _traceLogFormatter;

        #endregion

        /// <summary>
        ///     <para>Logs a warning message to the console.</para>
        /// </summary>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        /// <param name="context">Object to which the message applies.</param>
        /// <param name="logIf">Whether or not to log.</param>
#if LOG_DEBUGGER_STEPTHROUGH
        [DebuggerStepperBoundary]
#endif
        public static void Critical(
            object message,
            Object context = null,
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
#if LOG_DEBUGGER_STEPTHROUGH
        [DebuggerStepperBoundary]
#endif
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
        /// <param name="logIf">Whether or not to log.</param>
#if LOG_DEBUGGER_STEPTHROUGH
        [DebuggerStepperBoundary]
#endif
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
                LogInternal(
                    LogLevel.Error,
                    null,
                    null,
                    message,
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
        /// <param name="exception">String for display.</param>
        /// <param name="context">Object to which the message applies.</param>
        /// <param name="logIf">Whether or not to log.</param>
#if LOG_DEBUGGER_STEPTHROUGH
        [DebuggerStepperBoundary]
#endif
        public static void Exception(
            string exception,
            Object context = null,
            bool logIf = true,
            [CallerMemberName] string memberName = null,
            [CallerFilePath] string filePath = null,
            [CallerLineNumber] int lineNumber = 0)
        {
            using (_PRF_Exception.Auto())
            {
                LogInternal(
                    LogLevel.Exception,
                    null,
                    null,
                    exception,
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
        /// <param name="message">String to prepend the exception message with.</param>
        /// <param name="exception">Exception to be converted to string representation for display.</param>
        /// <param name="context">Object to which the message applies.</param>
        /// <param name="logIf">Whether or not to log.</param>
#if LOG_DEBUGGER_STEPTHROUGH
        [DebuggerStepperBoundary]
#endif
        public static void Exception(
            string message,
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
                    LogLevel.Exception,
                    null,
                    null,
                    $"{message}\n{exception}",
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
#if LOG_DEBUGGER_STEPTHROUGH
        [DebuggerStepperBoundary]
#endif
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
                    LogLevel.Exception,
                    null,
                    null,
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
#if LOG_DEBUGGER_STEPTHROUGH
        [DebuggerStepperBoundary]
#endif
        public static void Fatal(
            object message,
            Object context = null,
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
#if LOG_DEBUGGER_STEPTHROUGH
        [DebuggerStepperBoundary]
#endif
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
                LogInternal(
                    LogLevel.Info,
                    null,
                    null,
                    message,
                    context,
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
#if LOG_DEBUGGER_STEPTHROUGH
        [DebuggerStepperBoundary]
#endif
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
                LogInternal(level, null, null, message, context, logIf, memberName, filePath, lineNumber);
            }
        }

        /// <summary>
        ///     <para>Logs a trace message to the log file.</para>
        /// </summary>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        /// <param name="context">Object to which the message applies.</param>
        /// <param name="logIf">Whether or not to log.</param>
#if LOG_DEBUGGER_STEPTHROUGH
        [DebuggerStepperBoundary]
#endif
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
#if LOG_DEBUGGER_STEPTHROUGH
        [DebuggerStepperBoundary]
#endif
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
                LogInternal(
                    LogLevel.Warn,
                    null,
                    null,
                    message,
                    context,
                    logIf,
                    memberName,
                    filePath,
                    lineNumber
                );
            }
        }
#if LOG_DEBUGGER_STEPTHROUGH
        [DebuggerStepperBoundary]
#endif
        internal static void LogInternal(
            LogLevel level,
            string prefix,
            string formattedPrefix,
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
                        prefix,
                        formattedPrefix,
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
                    prefix,
                    formattedPrefix,
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

        #region Nested type: Context

        #region Nested Types

        public static class Context
        {
            public static AppaLogContextBase Application => Contexts.Application.Instance;
            public static AppaLogContextBase Area => Contexts.Area.Instance;
            public static AppaLogContextBase Bootload => Contexts.Bootload.Instance;
            public static AppaLogContextBase SubArea => Contexts.SubArea.Instance;

            public static void Reset()
            {
                Contexts.Application.Instance.Reset();
                Contexts.Area.Instance.Reset();
                Contexts.Bootload.Instance.Reset();
                Contexts.SubArea.Instance.Reset();
            }
        }

        #endregion

        #endregion

#if UNITY_EDITOR

        #region Menu Items

        [UnityEditor.MenuItem(MENU_DEBUGGER, true, MENU_PRIORITY + 100)]
        private static bool DefineDebuggerStepthroughValidate()
        {
#if LOG_DEBUGGER_STEPTHROUGH
            UnityEditor.Menu.SetChecked(MENU_DEBUGGER, true);
#else
            UnityEditor.Menu.SetChecked(MENU_DEBUGGER, false);
#endif
            return true;
        }

        private const string LOG_DEBUGGER_STEPTHROUGH = "LOG_DEBUGGER_STEPTHROUGH";
        
        [UnityEditor.MenuItem(MENU_DEBUGGER, false, MENU_PRIORITY + 100)]
        private static void DefineDebuggerStepthrough()
        {
#if LOG_DEBUGGER_STEPTHROUGH
            RemoveScriptingDefine(LOG_DEBUGGER_STEPTHROUGH);
#else
            AddScriptingDefine(LOG_DEBUGGER_STEPTHROUGH);
#endif
        }

        private static void AddScriptingDefine(string define)
        {
            UnityEditor.PlayerSettings.GetScriptingDefineSymbolsForGroup(
                UnityEditor.EditorUserBuildSettings.selectedBuildTargetGroup,
                out var scriptingDefines
            );

            var defines = new HashSet<string>(scriptingDefines);

            defines.Add(define);

            var sortedDefines = defines.ToArray();
            Array.Sort(sortedDefines);

            UnityEditor.PlayerSettings.SetScriptingDefineSymbolsForGroup(
                UnityEditor.EditorUserBuildSettings.selectedBuildTargetGroup,
                sortedDefines
            );
        }

        private static void RemoveScriptingDefine(string define)
        {
            UnityEditor.PlayerSettings.GetScriptingDefineSymbolsForGroup(
                UnityEditor.EditorUserBuildSettings.selectedBuildTargetGroup,
                out var scriptingDefines
            );

            var defines = new HashSet<string>(scriptingDefines);

            defines.Remove(define);

            var sortedDefines = defines.ToArray();
            Array.Sort(sortedDefines);

            UnityEditor.PlayerSettings.SetScriptingDefineSymbolsForGroup(
                UnityEditor.EditorUserBuildSettings.selectedBuildTargetGroup,
                sortedDefines
            );
        }

        [UnityEditor.MenuItem(MENU_BASE, false, MENU_PRIORITY + 0)]
        internal static void Test()
        {
            _consoleLogFormatter = new UnityConsoleFormatter();
            _traceLogFormatter = new FileLogFormatter();

            Context.Reset();

            var line = 436;
            var step = 11;

            LogInternal(
                LogLevel.Fatal,
                null,
                null,
                "Testing 1 2 3...",
                null,
                true,
                nameof(Test),
                "AppaLog.cs",
                line + (step * 1)
            );

            LogInternal(
                LogLevel.Critical,
                null,
                null,
                "Testing 1 2 3...",
                null,
                true,
                nameof(Test),
                "AppaLog.cs",
                line + (step * 2)
            );

            LogInternal(
                LogLevel.Exception,
                null,
                null,
                "Testing 1 2 3...",
                null,
                true,
                nameof(Test),
                "AppaLog.cs",
                line + (step * 3)
            );

            LogInternal(
                LogLevel.Error,
                null,
                null,
                "Testing 1 2 3...",
                null,
                true,
                nameof(Test),
                "AppaLog.cs",
                line + (step * 4)
            );

            LogInternal(
                LogLevel.Warn,
                null,
                null,
                "Testing 1 2 3...",
                null,
                true,
                nameof(Test),
                "AppaLog.cs",
                line + (step * 5)
            );

            LogInternal(
                LogLevel.Info,
                null,
                null,
                "Testing 1 2 3...",
                null,
                true,
                nameof(Test),
                "AppaLog.cs",
                line + (step * 6)
            );

            LogInternal(
                LogLevel.Debug,
                null,
                null,
                "Testing 1 2 3...",
                null,
                true,
                nameof(Test),
                "AppaLog.cs",
                line + (step * 7)
            );

            LogInternal(
                LogLevel.Trace,
                null,
                null,
                "Testing 1 2 3...",
                null,
                true,
                nameof(Test),
                "AppaLog.cs",
                line + (step * 8)
            );

            Application.Instance.Test();
            Area.Instance.Test();
            Bootload.Instance.Test();
            SubArea.Instance.Test();
        }

        #endregion

        /// <summary>
        ///     <para>Clears errors from the developer console.</para>
        /// </summary>
        [UnityEditor.MenuItem(PKG.Menu.Appalachia.Logging.Base + "Clear Developer Console" + SHC.CTRL_ALT_C)]
        public static void ClearDeveloperConsole()
        {
            using (_PRF_ClearDeveloperConsole.Auto())
            {
                UnityEngine.Debug.ClearDeveloperConsole();
            }
        }
#endif
    }
}
