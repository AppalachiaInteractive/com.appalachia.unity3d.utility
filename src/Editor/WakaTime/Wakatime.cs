#if UNITY_EDITOR

#region

using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Appalachia.Utility.Strings;
using Unity.EditorCoroutines.Editor;
using Unity.Profiling;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine.Device;
using UnityEngine.SceneManagement;

#endregion

namespace Appalachia.Utility.WakaTime
{
    internal static class WakaTime
    {
        #region Profiling And Tracing Markers

        private const string _PRF_PFX = nameof(WakaTime) + ".";
        private static readonly object _sync = new();
        private static Heartbeat _lastHeartbeat;
        private static readonly ProfilerMarker _PRF_Initialize = new(_PRF_PFX + nameof(Initialize));

        private static readonly ProfilerMarker _PRF_SendHeartbeatInternal =
            new(_PRF_PFX + nameof(SendHeartbeatInternal));

        private static readonly ProfilerMarker _PRF_OnScriptReload = new(_PRF_PFX + nameof(OnScriptReload));

        #endregion

        [InitializeOnLoadMethod]
        public static void Initialize()
        {
            using (_PRF_Initialize.Auto())
            {
                if (EditorApplication.isPlayingOrWillChangePlaymode)
                {
                    return;
                }
                
                Configuration.RefreshPreferences();
                Logger.DebugLog("Initializing...");

                if (!Configuration.Enabled)
                {
                    Logger.DebugLog("Explicitly disabled, skipping initialization...");
                    return;
                }

                if (Configuration.ApiKey == string.Empty)
                {
                    Logger.LogWarning("API key is not set, skipping initialization...");
                    return;
                }

                EditorApplication.delayCall += () =>
                {
                    Logger.DebugLog("Initialized.  Sending first heartbeat...");
                    SendHeartbeat();
                    Events.LinkCallbacks();
                };
            }
        }

        internal static void SendHeartbeat(
            bool fromSave = false,
            [CallerMemberName] string callerMemberName = "")
        {
            EditorCoroutineUtility.StartCoroutine(SendHeartbeatInternal(fromSave, callerMemberName), _sync);
        }

        [DidReloadScripts]
        private static void OnScriptReload()
        {
            if (EditorApplication.isPlayingOrWillChangePlaymode)
            {
                return;
            }
            
            using (_PRF_OnScriptReload.Auto())
            {
                Logger.DebugLog("Reloading scripts..");
                Initialize();
                Logger.DebugLog("Reload completed!");
            }
        }

        private static IEnumerator SendHeartbeatInternal(bool fromSave, string callerMemberName)
        {
            Logger.DebugLog(
                ZString.Format(
                    "[{0}] Heartbeat generated - checking if it should be sent...",
                    callerMemberName
                )
            );

            var scene = SceneManager.GetActiveScene();

            yield return null;

            var scenePath = scene.path;
            var sceneFilePath = scenePath != string.Empty
                ? Application.dataPath + "/" + scenePath.Substring("Assets/".Length)
                : string.Empty;

            var heartbeat = new Heartbeat(sceneFilePath, fromSave);
            var timeSinceLastHeartbeat = heartbeat.time - _lastHeartbeat.time;

            var processHeartbeat = fromSave ||
                                   (timeSinceLastHeartbeat > Constants.WakaTime.HeartbeatCooldown) ||
                                   (heartbeat.entity != _lastHeartbeat.entity);

            yield return null;

            if (!processHeartbeat)
            {
                Logger.DebugLog(ZString.Format("[{0}] Skipping this heartbeat.", callerMemberName));
                yield break;
            }

            yield return null;

            var wakatimePath = Configuration.WakaTimePath;
            var cliTargetPath = ZString.Format("\"{0}\"", wakatimePath);

            yield return null;
            var process = new Process();
            var processStartInfo = new ProcessStartInfo
            {
                CreateNoWindow = true,
                FileName = "python",
                Arguments = ZString.Format(" {0} ",             cliTargetPath) +
                            ZString.Format(" --entity \"{0}\"", heartbeat.entity) +
                            (heartbeat.isWrite ? " --write" : string.Empty) +
                            (heartbeat.isDebugging ? " --verbose" : string.Empty) +
                            ZString.Format(" --entity-type \"{0}\"", heartbeat.type) +
                            ZString.Format(" --language \"{0}\"",    heartbeat.language) +
                            ZString.Format(" --plugin \"{0}\"",      heartbeat.plugin) +
                            ZString.Format(" --time \"{0}\"",        heartbeat.time) +
                            ZString.Format(" --project \"{0}\"",     heartbeat.project),
                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardOutput = true
            };

            process.StartInfo = processStartInfo;

            yield return null;

            process.Start();

            yield return null;

            var error = process.StandardError.ReadToEnd();

            yield return null;

            var output = process.StandardOutput.ReadToEnd();

            yield return null;

            if (string.Empty == error)
            {
                Logger.DebugLog(output);
                Logger.DebugLog("Sent heartbeat!");
                _lastHeartbeat = heartbeat;
            }
            else
            {
                Configuration.WakaTimePath = null;
                Logger.Log(processStartInfo.Arguments);
                Logger.LogError(
                    ZString.Format("Unable to utilize WakaTime CLI: [{0}].  Disable this plugin.", error)
                );
            }
        }
    }
}

#endif
