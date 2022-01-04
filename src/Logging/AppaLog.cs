using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Appalachia.Utility.Constants;
using Appalachia.Utility.Logging.Formatters;
using Unity.Profiling;
using UnityEngine;
using Object = UnityEngine.Object;

// ReSharper disable InvalidXmlDocComment

namespace Appalachia.Utility.Logging
{
    public static class AppaLog
    {
        #region Constants and Static Readonly

        private const int MENU_PRIORITY = PKG.Menu.Appalachia.Logging.Priority;

        private const string MENU_BASE = PKG.Menu.Appalachia.Logging.Base;
        private const string MENU_DEBUGGER = MENU_BASE + "Debugger Boundary";

        #endregion

        private static AppaLogFormatter _consoleLogFormatter;

        private static AppaLogFormatter _traceLogFormatter;

        /// <summary>
        ///     <para>Logs a warning message to the console.</para>
        /// </summary>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        /// <param name="context">Object to which the message applies.</param>
        /// <param name="exception">The exception that occurred.</param>
        /// <param name="logIf">Whether or not to log.</param>
#if LOG_DEBUGGER_STEPTHROUGH
        [DebuggerStepperBoundary]
#endif
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
#if LOG_DEBUGGER_STEPTHROUGH
        [DebuggerStepperBoundary]
#endif
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
#if LOG_DEBUGGER_STEPTHROUGH
        [DebuggerStepperBoundary]
#endif
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
                LogInternal(
                    level,
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
                    null,
                    logIf,
                    memberName,
                    filePath,
                    lineNumber
                );
            }
        }

        private static bool _hasLoggedPreviously;

#if LOG_DEBUGGER_STEPTHROUGH
        [DebuggerStepperBoundary]
#endif
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

                if (!_hasLoggedPreviously)
                {
                    var logType = LogLevelToLogType(LogLevel.Debug);

                    UnityEngine.Debug.unityLogger.Log(logType, "-------------------------------");

#if UNITY_EDITOR
                    var playMode = false;
                    try
                    {
                        playMode = UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode;
                    }
                    catch
                    {
                        //
                    }

                    if (playMode)
#endif
                    {
                        UnityEngine.Debug.unityLogger.Log(
                            logType,
                            $"------{"ENTERING PLAY MODE".Bold()}-------"
                        );
                    }
#if UNITY_EDITOR
                    else
                    {
                        UnityEngine.Debug.unityLogger.Log(
                            logType,
                            $"------{"ENTERING EDIT MODE".Bold()}-------"
                        );
                    }
#endif

                    UnityEngine.Debug.unityLogger.Log(logType, "-------------------------------");

                    _hasLoggedPreviously = true;
                }

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

                    UnityEngine.Debug.unityLogger.Log(
                        logType,
                        consoleMessage,
                        context is Object o ? o : null
                    );
                }

                /*var traceLogMessage = _traceLogFormatter.FormatLogMessage(
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

                Console.WriteLine(traceLogMessage);*/
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
            public static AppaLogContext Animal = new(nameof(Animal));
            public static AppaLogContext Animation = new(nameof(Animation));
            public static AppaLogContext Application = new(nameof(Application));
            public static AppaLogContext Area = new(nameof(Area));
            public static AppaLogContext ArrayPooling = new(nameof(ArrayPooling));
            public static AppaLogContext Assets = new(nameof(Assets));
            public static AppaLogContext Object = new(nameof(Object));
            public static AppaLogContext Repo = new(nameof(Repo));
            public static AppaLogContext Audio = new(nameof(Audio));
            public static AppaLogContext Bazooka = new(nameof(Bazooka));
            public static AppaLogContext Behaviours = new(nameof(Behaviours));
            public static AppaLogContext Bootload = new(nameof(Bootload));
            public static AppaLogContext CI = new(nameof(CI));
            public static AppaLogContext Caching = new(nameof(Caching));
            public static AppaLogContext Character = new(nameof(Character));
            public static AppaLogContext Clock = new(nameof(Clock));
            public static AppaLogContext Collections = new(nameof(Collections));
            public static AppaLogContext Components = new(nameof(Components));
            public static AppaLogContext ConvexDecomposition = new(nameof(ConvexDecomposition));
            public static AppaLogContext Core = new(nameof(Core));
            public static AppaLogContext Crafting = new(nameof(Crafting));
            public static AppaLogContext Cursor = new(nameof(Cursor));
            public static AppaLogContext Data = new(nameof(Data));
            public static AppaLogContext Database = new(nameof(Database));
            public static AppaLogContext DebugOverlay = new(nameof(DebugOverlay));
            public static AppaLogContext Dependencies = new(nameof(Dependencies));
            public static AppaLogContext DevConsole = new(nameof(DevConsole));
            public static AppaLogContext Editing = new(nameof(Editing));
            public static AppaLogContext Editor = new(nameof(Editor));
            public static AppaLogContext Execution = new(nameof(Execution));
            public static AppaLogContext Extensions = new(nameof(Extensions));
            public static AppaLogContext Filtering = new(nameof(Filtering));
            public static AppaLogContext Fire = new(nameof(Fire));
            public static AppaLogContext FrameEvent = new(nameof(FrameEvent));
            public static AppaLogContext Game = new(nameof(Game));
            public static AppaLogContext Gameplay = new(nameof(Gameplay));
            public static AppaLogContext Globals = new(nameof(Globals));
            public static AppaLogContext HUD = new(nameof(HUD));
            public static AppaLogContext InGameMenu = new(nameof(InGameMenu));
            public static AppaLogContext Initialize = new(nameof(Initialize));
            public static AppaLogContext Input = new(nameof(Input));
            public static AppaLogContext Inventory = new(nameof(Inventory));
            public static AppaLogContext Jobs = new(nameof(Jobs));
            public static AppaLogContext KOC = new(nameof(KOC));
            public static AppaLogContext Labels = new(nameof(Labels));
            public static AppaLogContext Layers = new(nameof(Layers));
            public static AppaLogContext Lifetime = new(nameof(Lifetime));
            public static AppaLogContext Lighting = new(nameof(Lighting));
            public static AppaLogContext LoadingScreen = new(nameof(LoadingScreen));
            public static AppaLogContext Logging = new(nameof(Logging));
            public static AppaLogContext MainMenu = new(nameof(MainMenu));
            public static AppaLogContext Maintenance = new(nameof(Maintenance));
            public static AppaLogContext Math = new(nameof(Math));
            public static AppaLogContext MeshBurial = new(nameof(MeshBurial));
            public static AppaLogContext MeshData = new(nameof(MeshData));
            public static AppaLogContext Obi = new(nameof(Obi));
            public static AppaLogContext ObjectPooling = new(nameof(ObjectPooling));
            public static AppaLogContext Octree = new(nameof(Octree));
            public static AppaLogContext Optimization = new(nameof(Optimization));
            public static AppaLogContext Overrides = new(nameof(Overrides));
            public static AppaLogContext PauseMenu = new(nameof(PauseMenu));
            public static AppaLogContext Playables = new(nameof(Playables));
            public static AppaLogContext PostProcessing = new(nameof(PostProcessing));
            public static AppaLogContext PrefabRendering = new(nameof(PrefabRendering));
            public static AppaLogContext Prefabs = new(nameof(Prefabs));
            public static AppaLogContext Preferences = new(nameof(Preferences));
            public static AppaLogContext Prototype = new(nameof(Prototype));
            public static AppaLogContext ReactionSystem = new(nameof(ReactionSystem));
            public static AppaLogContext Rendering = new(nameof(Rendering));
            public static AppaLogContext RuntimeGraphs = new(nameof(RuntimeGraphs));
            public static AppaLogContext SDF = new(nameof(SDF));
            public static AppaLogContext Scriptables = new(nameof(Scriptables));
            public static AppaLogContext Shading = new(nameof(Shading));
            public static AppaLogContext Shell = new(nameof(Shell));
            public static AppaLogContext Simulation = new(nameof(Simulation));
            public static AppaLogContext Singleton = new(nameof(Singleton));
            public static AppaLogContext Spatial = new(nameof(Spatial));
            public static AppaLogContext SplashScreen = new(nameof(SplashScreen));
            public static AppaLogContext StartEnvironment = new(nameof(StartEnvironment));
            public static AppaLogContext StartScreen = new(nameof(StartScreen));
            public static AppaLogContext Styling = new(nameof(Styling));
            public static AppaLogContext Terrain = new(nameof(Terrain));
            public static AppaLogContext Timeline = new(nameof(Timeline));
            public static AppaLogContext TouchBend = new(nameof(TouchBend));
            public static AppaLogContext Trees = new(nameof(Trees));
            public static AppaLogContext UI => new(nameof(UI));
            public static AppaLogContext Uncategorized = new(nameof(Uncategorized));
            public static AppaLogContext Utility = new(nameof(Utility));
            public static AppaLogContext VFX = new(nameof(VFX));
            public static AppaLogContext Visualizers = new(nameof(Visualizers));
            public static AppaLogContext Volumes = new(nameof(Volumes));
            public static AppaLogContext Voxels = new(nameof(Voxels));
            public static AppaLogContext Water = new(nameof(Water));
            public static AppaLogContext Wind = new(nameof(Wind)); 

            public static AppaLogContext GetByType(Type t)
            {
                return AppaLogContextResolver.Get(t);
            }

            public static AppaLogContext GetByType<T>()
            {
                return GetByType(typeof(T));
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

            var defines = new HashSet<string>(scriptingDefines) { define };

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

            var line = 436;
            var step = 11;

            LogInternal(
                LogLevel.Fatal,
                null,
                null,
                "Testing 1 2 3...",
                null,
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
                new NotSupportedException("testing 123"),
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
                null,
                true,
                nameof(Test),
                "AppaLog.cs",
                line + (step * 8)
            );

            var contextProperties = typeof(Context).GetProperties()
                                                   .Where(p => p.PropertyType == typeof(AppaLogContext));

            foreach (var contextProperty in contextProperties)
            {
                var contextValue = contextProperty.GetValue(null) as AppaLogContext;

                contextValue?.Test();
            }
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

        #region Profiling

        private const string _PRF_PFX = nameof(AppaLog) + ".";

#if UNITY_EDITOR
        private static readonly ProfilerMarker _PRF_ClearDeveloperConsole =
            new(_PRF_PFX + nameof(ClearDeveloperConsole));
#endif

        private static readonly ProfilerMarker _PRF_Debug = new(_PRF_PFX + nameof(Debug));
        private static readonly ProfilerMarker _PRF_Error = new(_PRF_PFX + nameof(Error));

        private static readonly ProfilerMarker _PRF_Info = new(_PRF_PFX + nameof(Info));
        private static readonly ProfilerMarker _PRF_Log = new(_PRF_PFX + nameof(Log));
        private static readonly ProfilerMarker _PRF_Trace = new(_PRF_PFX + nameof(Trace));
        private static readonly ProfilerMarker _PRF_Warn = new(_PRF_PFX + nameof(Warn));

        private static readonly ProfilerMarker _PRF_LogInternal = new(_PRF_PFX + nameof(LogInternal));

        private static readonly ProfilerMarker _PRF_Critical = new(_PRF_PFX + nameof(Critical));

        private static readonly ProfilerMarker _PRF_Fatal = new(_PRF_PFX + nameof(Fatal));

        private static readonly ProfilerMarker _PRF_LogLevelToLogType =
            new(_PRF_PFX + nameof(LogLevelToLogType));

        #endregion
    }
}
