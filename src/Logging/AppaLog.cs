using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Appalachia.Utility.Constants;
using Appalachia.Utility.Logging.Formatters;
using Appalachia.Utility.Strings;
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

                if (!_hasLoggedPreviously &&
                    APPASERIALIZE.InSerializationWindow &&
                    !AppaLogFormatHolder.HasFormat)
                {
                    var logType = LogLevelToLogType(level);

                    UnityEngine.Debug.unityLogger.Log(logType, message, context is Object o ? o : null);
                    return;
                }

                _consoleLogFormatter ??= new UnityConsoleFormatter();
                _traceLogFormatter ??= new FileLogFormatter();

                if (!_hasLoggedPreviously)
                {
                    object consoleMessage = _consoleLogFormatter.FormatLogMessage(
                        LogLevel.Debug,
                        null,
                        null,
                        ZString.Format(
                            "Console log formats can be controlled via the {0} asset.",
                            nameof(AppaLogFormats)
                        ),
                        AppaLogFormatHolder.formats,
                        null,
                        memberName,
                        filePath,
                        lineNumber
                    );

                    var logType = LogLevelToLogType(LogLevel.Debug);

                    UnityEngine.Debug.unityLogger.Log(logType, "-------------------------------");

#if UNITY_EDITOR
                    if (UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode)
#else
                    if (Application.isPlaying)
#endif
                    {
                        UnityEngine.Debug.unityLogger.Log(logType, "------ENTERING PLAY MODE-------");
                    }
                    else
                    {
                        UnityEngine.Debug.unityLogger.Log(logType, "------ENTERING EDIT MODE-------");
                    }

                    UnityEngine.Debug.unityLogger.Log(logType, "-------------------------------");
                    UnityEngine.Debug.unityLogger.Log(logType, consoleMessage, AppaLogFormatHolder.formats);
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
            public static AppaLogContext Animal => Contexts.Animal.Instance;
            public static AppaLogContext Animation => Contexts.Animation.Instance;
            public static AppaLogContext Application => Contexts.Application.Instance;
            public static AppaLogContext Area => Contexts.Area.Instance;
            public static AppaLogContext ArrayPooling => Contexts.ArrayPooling.Instance;
            public static AppaLogContext Assets => Contexts.Assets.Instance;
            public static AppaLogContext Audio => Contexts.Audio.Instance;
            public static AppaLogContext Bazooka => Contexts.Bazooka.Instance;
            public static AppaLogContext Behaviours => Contexts.Behaviours.Instance;
            public static AppaLogContext Bootload => Contexts.Bootload.Instance;
            public static AppaLogContext Caching => Contexts.Caching.Instance;
            public static AppaLogContext Character => Contexts.Character.Instance;
            public static AppaLogContext CI => Contexts.CI.Instance;
            public static AppaLogContext Clock => Contexts.Clock.Instance;
            public static AppaLogContext Collections => Contexts.Collections.Instance;
            public static AppaLogContext Components => Contexts.Components.Instance;
            public static AppaLogContext ConvexDecomposition => Contexts.ConvexDecomposition.Instance;
            public static AppaLogContext Core => Contexts.Core.Instance;
            public static AppaLogContext Crafting => Contexts.Crafting.Instance;
            public static AppaLogContext Cursor => Contexts.Cursor.Instance;
            public static AppaLogContext Data => Contexts.Data.Instance;
            public static AppaLogContext Database => Contexts.Database.Instance;
            public static AppaLogContext DebugOverlay => Contexts.DebugOverlay.Instance;
            public static AppaLogContext DevConsole => Contexts.DevConsole.Instance;
            public static AppaLogContext Editing => Contexts.Editing.Instance;
            public static AppaLogContext Editor => Contexts.Editor.Instance;
            public static AppaLogContext Execution => Contexts.Execution.Instance;
            public static AppaLogContext Extensions => Contexts.Extensions.Instance;
            public static AppaLogContext Filtering => Contexts.Filtering.Instance;
            public static AppaLogContext Fire => Contexts.Fire.Instance;
            public static AppaLogContext FrameEvent => Contexts.FrameEvent.Instance;
            public static AppaLogContext Game => Contexts.Game.Instance;
            public static AppaLogContext Gameplay => Contexts.Gameplay.Instance;
            public static AppaLogContext Globals => Contexts.Globals.Instance;
            public static AppaLogContext HUD => Contexts.HUD.Instance;
            public static AppaLogContext InGameMenu => Contexts.InGameMenu.Instance;
            public static AppaLogContext Initialize => Contexts.Initialize.Instance;
            public static AppaLogContext Input => Contexts.Input.Instance;
            public static AppaLogContext Inventory => Contexts.Inventory.Instance;
            public static AppaLogContext Jobs => Contexts.Jobs.Instance;
            public static AppaLogContext KOC => Contexts.KOC.Instance;
            public static AppaLogContext Labels => Contexts.Labels.Instance;
            public static AppaLogContext Layers => Contexts.Layers.Instance;
            public static AppaLogContext Lifetime => Contexts.Lifetime.Instance;
            public static AppaLogContext Lighting => Contexts.Lighting.Instance;
            public static AppaLogContext LoadingScreen => Contexts.LoadingScreen.Instance;
            public static AppaLogContext MainMenu => Contexts.MainMenu.Instance;
            public static AppaLogContext Maintenance => Contexts.Maintenance.Instance;
            public static AppaLogContext Math => Contexts.Math.Instance;
            public static AppaLogContext MeshBurial => Contexts.MeshBurial.Instance;
            public static AppaLogContext MeshData => Contexts.MeshData.Instance;
            public static AppaLogContext Obi => Contexts.Obi.Instance;
            public static AppaLogContext ObjectPooling => Contexts.ObjectPooling.Instance;
            public static AppaLogContext Octree => Contexts.Octree.Instance;
            public static AppaLogContext Optimization => Contexts.Optimization.Instance;
            public static AppaLogContext Overrides => Contexts.Overrides.Instance;
            public static AppaLogContext PauseMenu => Contexts.PauseMenu.Instance;
            public static AppaLogContext Playables => Contexts.Playables.Instance;
            public static AppaLogContext PostProcessing => Contexts.PostProcessing.Instance;
            public static AppaLogContext PrefabRendering => Contexts.PrefabRendering.Instance;
            public static AppaLogContext Prefabs => Contexts.Prefabs.Instance;
            public static AppaLogContext Preferences => Contexts.Preferences.Instance;
            public static AppaLogContext Prototype => Contexts.Prototype.Instance;
            public static AppaLogContext ReactionSystem => Contexts.ReactionSystem.Instance;
            public static AppaLogContext Rendering => Contexts.Rendering.Instance;
            public static AppaLogContext RuntimeGraphs => Contexts.RuntimeGraphs.Instance;
            public static AppaLogContext Scriptables => Contexts.Scriptables.Instance;
            public static AppaLogContext SDF => Contexts.SDF.Instance;
            public static AppaLogContext Shading => Contexts.Shading.Instance;
            public static AppaLogContext Shell => Contexts.Shell.Instance;
            public static AppaLogContext Simulation => Contexts.Simulation.Instance;
            public static AppaLogContext Singleton => Contexts.Singleton.Instance;
            public static AppaLogContext Spatial => Contexts.Spatial.Instance;
            public static AppaLogContext SplashScreen => Contexts.SplashScreen.Instance;
            public static AppaLogContext StartEnvironment => Contexts.StartEnvironment.Instance;
            public static AppaLogContext StartScreen => Contexts.StartScreen.Instance;
            public static AppaLogContext Styling => Contexts.Styling.Instance;
            public static AppaLogContext Terrain => Contexts.Terrain.Instance;
            public static AppaLogContext Timeline => Contexts.Timeline.Instance;
            public static AppaLogContext TouchBend => Contexts.TouchBend.Instance;
            public static AppaLogContext Trees => Contexts.Trees.Instance;
            public static AppaLogContext UI => Contexts.UI.Instance;
            public static AppaLogContext Uncategorized => Contexts.Uncategorized.Instance;
            public static AppaLogContext Utility => Contexts.Utility.Instance;
            public static AppaLogContext VFX => Contexts.VFX.Instance;
            public static AppaLogContext Visualizers => Contexts.Visualizers.Instance;
            public static AppaLogContext Volumes => Contexts.Volumes.Instance;
            public static AppaLogContext Voxels => Contexts.Voxels.Instance;
            public static AppaLogContext Water => Contexts.Water.Instance;
            public static AppaLogContext Wind => Contexts.Wind.Instance;
            public static AppaLogContext _Extra00 => Contexts._Extra00.Instance;
            public static AppaLogContext _Extra01 => Contexts._Extra01.Instance;
            public static AppaLogContext _Extra02 => Contexts._Extra02.Instance;
            public static AppaLogContext _Extra03 => Contexts._Extra03.Instance;
            public static AppaLogContext _Extra04 => Contexts._Extra04.Instance;
            public static AppaLogContext _Extra05 => Contexts._Extra05.Instance;
            public static AppaLogContext _Extra06 => Contexts._Extra06.Instance;
            public static AppaLogContext _Extra07 => Contexts._Extra07.Instance;
            public static AppaLogContext _Extra08 => Contexts._Extra08.Instance;
            public static AppaLogContext _Extra09 => Contexts._Extra09.Instance;
            public static AppaLogContext _Extra10 => Contexts._Extra10.Instance;
            public static AppaLogContext _Extra11 => Contexts._Extra11.Instance;
            public static AppaLogContext _Extra12 => Contexts._Extra12.Instance;
            public static AppaLogContext _Extra13 => Contexts._Extra13.Instance;
            public static AppaLogContext _Extra14 => Contexts._Extra14.Instance;
            public static AppaLogContext _Extra15 => Contexts._Extra15.Instance;
            public static AppaLogContext _Extra16 => Contexts._Extra16.Instance;
            public static AppaLogContext _Extra17 => Contexts._Extra17.Instance;
            public static AppaLogContext _Extra18 => Contexts._Extra18.Instance;
            public static AppaLogContext _Extra19 => Contexts._Extra19.Instance;
            public static AppaLogContext _Extra20 => Contexts._Extra20.Instance;
            public static AppaLogContext _Extra21 => Contexts._Extra21.Instance;
            public static AppaLogContext _Extra22 => Contexts._Extra22.Instance;
            public static AppaLogContext _Extra23 => Contexts._Extra23.Instance;
            public static AppaLogContext _Extra24 => Contexts._Extra24.Instance;
            public static AppaLogContext _Extra25 => Contexts._Extra25.Instance;
            public static AppaLogContext _Extra26 => Contexts._Extra26.Instance;
            public static AppaLogContext _Extra27 => Contexts._Extra27.Instance;
            public static AppaLogContext _Extra28 => Contexts._Extra28.Instance;
            public static AppaLogContext _Extra29 => Contexts._Extra29.Instance;
            public static AppaLogContext _Extra30 => Contexts._Extra30.Instance;
            public static AppaLogContext _Extra31 => Contexts._Extra31.Instance;
            public static AppaLogContext _Extra32 => Contexts._Extra32.Instance;
            public static AppaLogContext _Extra33 => Contexts._Extra33.Instance;
            public static AppaLogContext _Extra34 => Contexts._Extra34.Instance;
            public static AppaLogContext _Extra35 => Contexts._Extra35.Instance;
            public static AppaLogContext _Extra36 => Contexts._Extra36.Instance;
            public static AppaLogContext _Extra37 => Contexts._Extra37.Instance;
            public static AppaLogContext _Extra38 => Contexts._Extra38.Instance;
            public static AppaLogContext _Extra39 => Contexts._Extra39.Instance;
            public static AppaLogContext _Extra40 => Contexts._Extra40.Instance;
            public static AppaLogContext _Extra41 => Contexts._Extra41.Instance;
            public static AppaLogContext _Extra42 => Contexts._Extra42.Instance;
            public static AppaLogContext _Extra43 => Contexts._Extra43.Instance;
            public static AppaLogContext _Extra44 => Contexts._Extra44.Instance;
            public static AppaLogContext _Extra45 => Contexts._Extra45.Instance;
            public static AppaLogContext _Extra46 => Contexts._Extra46.Instance;
            public static AppaLogContext _Extra47 => Contexts._Extra47.Instance;
            public static AppaLogContext _Extra48 => Contexts._Extra48.Instance;
            public static AppaLogContext _Extra49 => Contexts._Extra49.Instance;
            public static AppaLogContext _Extra50 => Contexts._Extra50.Instance;
            public static AppaLogContext _Extra51 => Contexts._Extra51.Instance;
            public static AppaLogContext _Extra52 => Contexts._Extra52.Instance;
            public static AppaLogContext _Extra53 => Contexts._Extra53.Instance;
            public static AppaLogContext _Extra54 => Contexts._Extra54.Instance;
            public static AppaLogContext _Extra55 => Contexts._Extra55.Instance;
            public static AppaLogContext _Extra56 => Contexts._Extra56.Instance;
            public static AppaLogContext _Extra57 => Contexts._Extra57.Instance;
            public static AppaLogContext _Extra58 => Contexts._Extra58.Instance;
            public static AppaLogContext _Extra59 => Contexts._Extra59.Instance;
            public static AppaLogContext _Extra60 => Contexts._Extra60.Instance;
            public static AppaLogContext _Extra61 => Contexts._Extra61.Instance;
            public static AppaLogContext _Extra62 => Contexts._Extra62.Instance;
            public static AppaLogContext _Extra63 => Contexts._Extra63.Instance;
            public static AppaLogContext _Extra64 => Contexts._Extra64.Instance;
            public static AppaLogContext _Extra65 => Contexts._Extra65.Instance;
            public static AppaLogContext _Extra66 => Contexts._Extra66.Instance;
            public static AppaLogContext _Extra67 => Contexts._Extra67.Instance;
            public static AppaLogContext _Extra68 => Contexts._Extra68.Instance;
            public static AppaLogContext _Extra69 => Contexts._Extra69.Instance;
            public static AppaLogContext _Extra70 => Contexts._Extra70.Instance;
            public static AppaLogContext _Extra71 => Contexts._Extra71.Instance;
            public static AppaLogContext _Extra72 => Contexts._Extra72.Instance;
            public static AppaLogContext _Extra73 => Contexts._Extra73.Instance;
            public static AppaLogContext _Extra74 => Contexts._Extra74.Instance;
            public static AppaLogContext _Extra75 => Contexts._Extra75.Instance;
            public static AppaLogContext _Extra76 => Contexts._Extra76.Instance;
            public static AppaLogContext _Extra77 => Contexts._Extra77.Instance;
            public static AppaLogContext _Extra78 => Contexts._Extra78.Instance;
            public static AppaLogContext _Extra79 => Contexts._Extra79.Instance;
            public static AppaLogContext _Extra80 => Contexts._Extra80.Instance;
            public static AppaLogContext _Extra81 => Contexts._Extra81.Instance;
            public static AppaLogContext _Extra82 => Contexts._Extra82.Instance;
            public static AppaLogContext _Extra83 => Contexts._Extra83.Instance;
            public static AppaLogContext _Extra84 => Contexts._Extra84.Instance;
            public static AppaLogContext _Extra85 => Contexts._Extra85.Instance;
            public static AppaLogContext _Extra86 => Contexts._Extra86.Instance;
            public static AppaLogContext _Extra87 => Contexts._Extra87.Instance;
            public static AppaLogContext _Extra88 => Contexts._Extra88.Instance;
            public static AppaLogContext _Extra89 => Contexts._Extra89.Instance;
            public static AppaLogContext _Extra90 => Contexts._Extra90.Instance;
            public static AppaLogContext _Extra91 => Contexts._Extra91.Instance;
            public static AppaLogContext _Extra92 => Contexts._Extra92.Instance;
            public static AppaLogContext _Extra93 => Contexts._Extra93.Instance;
            public static AppaLogContext _Extra94 => Contexts._Extra94.Instance;
            public static AppaLogContext _Extra95 => Contexts._Extra95.Instance;
            public static AppaLogContext _Extra96 => Contexts._Extra96.Instance;
            public static AppaLogContext _Extra97 => Contexts._Extra97.Instance;
            public static AppaLogContext _Extra98 => Contexts._Extra98.Instance;
            public static AppaLogContext Dependencies => Contexts.Dependencies.Instance;

            public static void Awake()
            {
                Animal.Touch();
                Animation.Touch();
                Application.Touch();
                Area.Touch();
                ArrayPooling.Touch();
                Assets.Touch();
                Audio.Touch();
                Bazooka.Touch();
                Behaviours.Touch();
                Bootload.Touch();
                CI.Touch();
                Caching.Touch();
                Character.Touch();
                Clock.Touch();
                Collections.Touch();
                Components.Touch();
                ConvexDecomposition.Touch();
                Core.Touch();
                Crafting.Touch();
                Cursor.Touch();
                Data.Touch();
                Database.Touch();
                DebugOverlay.Touch();
                DevConsole.Touch();
                Editing.Touch();
                Editor.Touch();
                Execution.Touch();
                Extensions.Touch();
                Filtering.Touch();
                Fire.Touch();
                FrameEvent.Touch();
                Game.Touch();
                Gameplay.Touch();
                Globals.Touch();
                HUD.Touch();
                InGameMenu.Touch();
                Initialize.Touch();
                Input.Touch();
                Inventory.Touch();
                Jobs.Touch();
                KOC.Touch();
                Labels.Touch();
                Layers.Touch();
                Lifetime.Touch();
                Lighting.Touch();
                LoadingScreen.Touch();
                MainMenu.Touch();
                Maintenance.Touch();
                Math.Touch();
                MeshBurial.Touch();
                MeshData.Touch();
                Obi.Touch();
                ObjectPooling.Touch();
                Octree.Touch();
                Optimization.Touch();
                Overrides.Touch();
                PauseMenu.Touch();
                Playables.Touch();
                PostProcessing.Touch();
                PrefabRendering.Touch();
                Prefabs.Touch();
                Preferences.Touch();
                Prototype.Touch();
                ReactionSystem.Touch();
                Rendering.Touch();
                RuntimeGraphs.Touch();
                SDF.Touch();
                Scriptables.Touch();
                Shading.Touch();
                Shell.Touch();
                Simulation.Touch();
                Singleton.Touch();
                Spatial.Touch();
                SplashScreen.Touch();
                StartEnvironment.Touch();
                StartScreen.Touch();
                Styling.Touch();
                Terrain.Touch();
                Timeline.Touch();
                TouchBend.Touch();
                Trees.Touch();
                UI.Touch();
                Uncategorized.Touch();
                Utility.Touch();
                VFX.Touch();
                Visualizers.Touch();
                Volumes.Touch();
                Voxels.Touch();
                Water.Touch();
                Wind.Touch();
                _Extra00.Touch();
                _Extra01.Touch();
                _Extra02.Touch();
                _Extra03.Touch();
                _Extra04.Touch();
                _Extra05.Touch();
                _Extra06.Touch();
                _Extra07.Touch();
                _Extra08.Touch();
                _Extra09.Touch();
                _Extra10.Touch();
                _Extra11.Touch();
                _Extra12.Touch();
                _Extra13.Touch();
                _Extra14.Touch();
                _Extra15.Touch();
                _Extra16.Touch();
                _Extra17.Touch();
                _Extra18.Touch();
                _Extra19.Touch();
                _Extra20.Touch();
                _Extra21.Touch();
                _Extra22.Touch();
                _Extra23.Touch();
                _Extra24.Touch();
                _Extra25.Touch();
                _Extra26.Touch();
                _Extra27.Touch();
                _Extra28.Touch();
                _Extra29.Touch();
                _Extra30.Touch();
                _Extra31.Touch();
                _Extra32.Touch();
                _Extra33.Touch();
                _Extra34.Touch();
                _Extra35.Touch();
                _Extra36.Touch();
                _Extra37.Touch();
                _Extra38.Touch();
                _Extra39.Touch();
                _Extra40.Touch();
                _Extra41.Touch();
                _Extra42.Touch();
                _Extra43.Touch();
                _Extra44.Touch();
                _Extra45.Touch();
                _Extra46.Touch();
                _Extra47.Touch();
                _Extra48.Touch();
                _Extra49.Touch();
                _Extra50.Touch();
                _Extra51.Touch();
                _Extra52.Touch();
                _Extra53.Touch();
                _Extra54.Touch();
                _Extra55.Touch();
                _Extra56.Touch();
                _Extra57.Touch();
                _Extra58.Touch();
                _Extra59.Touch();
                _Extra60.Touch();
                _Extra61.Touch();
                _Extra62.Touch();
                _Extra63.Touch();
                _Extra64.Touch();
                _Extra65.Touch();
                _Extra66.Touch();
                _Extra67.Touch();
                _Extra68.Touch();
                _Extra69.Touch();
                _Extra70.Touch();
                _Extra71.Touch();
                _Extra72.Touch();
                _Extra73.Touch();
                _Extra74.Touch();
                _Extra75.Touch();
                _Extra76.Touch();
                _Extra77.Touch();
                _Extra78.Touch();
                _Extra79.Touch();
                _Extra80.Touch();
                _Extra81.Touch();
                _Extra82.Touch();
                _Extra83.Touch();
                _Extra84.Touch();
                _Extra85.Touch();
                _Extra86.Touch();
                _Extra87.Touch();
                _Extra88.Touch();
                _Extra89.Touch();
                _Extra90.Touch();
                _Extra91.Touch();
                _Extra92.Touch();
                _Extra93.Touch();
                _Extra94.Touch();
                _Extra95.Touch();
                _Extra96.Touch();
                _Extra97.Touch();
                _Extra98.Touch();
                Dependencies.Touch();
            }

            public static AppaLogContext GetByType(Type t)
            {
                return AppaLogContextResolver.Get(t);
            }

            public static AppaLogContext GetByType<T>()
            {
                return GetByType(typeof(T));
            }

            public static void Reset()
            {
                foreach (var context in Contexts.AllContexts)
                {
                    context.Reset();
                }
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

            Context.Reset();

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

            foreach (var context in Contexts.AllContexts)
            {
                context.Test();
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
    }
}
