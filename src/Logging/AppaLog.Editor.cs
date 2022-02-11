#if UNITY_EDITOR
using Appalachia.Utility.Constants;
using Unity.Profiling;
using UnityEditor;

namespace Appalachia.Utility.Logging
{
    public static partial class AppaLog
    {
        #region Static Fields and Autoproperties

        private static bool _hasLoggedPreviously;

        #endregion

        private static void LogInitialMessage()
        {
            using (_PRF_LogInitialMessage.Auto())
            {
                var logType = LogLevelToLogType(LogLevel.Debug);

                UnityEngine.Debug.unityLogger.Log(logType, "-------------------------------");

                var playMode = false;
                try
                {
                    playMode = EditorApplication.isPlaying;
                }
                catch
                {
                    //
                }

                if (playMode)
                {
                    UnityEngine.Debug.unityLogger.Log(logType, $"------{"ENTERING PLAY MODE".Bold()}-------");
                }
                else
                {
                    UnityEngine.Debug.unityLogger.Log(logType, $"------{"ENTERING EDIT MODE".Bold()}-------");
                }

                UnityEngine.Debug.unityLogger.Log(logType, "-------------------------------");

                _hasLoggedPreviously = true;
            }
        }

        #region Menu Items

        /// <summary>
        ///     <para>Clears errors from the developer console.</para>
        /// </summary>
        [MenuItem(PKG.Menu.Appalachia.Logging.Base + "Clear Developer Console" + SHC.CTRL_ALT_C)]
        public static void ClearDeveloperConsole()
        {
            using (_PRF_ClearDeveloperConsole.Auto())
            {
                UnityEngine.Debug.ClearDeveloperConsole();
            }
        }

        #endregion

        #region Profiling

        private static readonly ProfilerMarker _PRF_LogInitialMessage =
            new ProfilerMarker(_PRF_PFX + nameof(LogInitialMessage));

        private static readonly ProfilerMarker _PRF_ClearDeveloperConsole =
            new(_PRF_PFX + nameof(ClearDeveloperConsole));

        #endregion
    }
}

#endif
