using Unity.Profiling;
using UnityEngine;
using UnityEngine.Diagnostics;

// ReSharper disable RedundantLogicalConditionalExpressionOperand

namespace Appalachia.Utility.Execution
{
    public static class AppalachiaApplication
    {
        /// <summary>
        ///     <para>Returns application install mode (Read Only).</para>
        /// </summary>
        /// <footer>
        ///     <a href="https://docs.unity3d.com/2021.2/Documentation/ScriptReference/30_search.html?q=Application-installMode">
        ///         `Application.installMode` on
        ///         docs.unity3d.com
        ///     </a>
        /// </footer>
        public static ApplicationInstallMode InstallMode => Application.installMode;

        /// <summary>
        ///     <para>Returns application running in sandbox (Read Only).</para>
        /// </summary>
        /// <footer>
        ///     <a href="https://docs.unity3d.com/2021.2/Documentation/ScriptReference/30_search.html?q=Application-sandboxType">
        ///         `Application.sandboxType` on
        ///         docs.unity3d.com
        ///     </a>
        /// </footer>
        public static ApplicationSandboxType SandboxType => Application.sandboxType;

        /// <summary>
        ///     <para>Returns false if application is altered in any way after it was built.</para>
        /// </summary>
        /// <footer>
        ///     <a href="https://docs.unity3d.com/2021.2/Documentation/ScriptReference/30_search.html?q=Application-genuine">
        ///         `Application.genuine` on
        ///         docs.unity3d.com
        ///     </a>
        /// </footer>
        public static bool Genuine => Application.genuine;

        /// <summary>
        ///     <para>Returns true if application integrity can be confirmed.</para>
        /// </summary>
        /// <footer>
        ///     <a href="https://docs.unity3d.com/2021.2/Documentation/ScriptReference/30_search.html?q=Application-genuineCheckAvailable">
        ///         `Application.genuineCheckAvailable`
        ///         on docs.unity3d.com
        ///     </a>
        /// </footer>
        public static bool GenuineCheckAvailable => Application.genuineCheckAvailable;

        /// <summary>
        ///     <para>Returns true when Unity is launched with the -batchmode flag from the command line (Read Only).</para>
        /// </summary>
        /// <footer>
        ///     <a href="https://docs.unity3d.com/2021.2/Documentation/ScriptReference/30_search.html?q=Application-isBatchMode">
        ///         `Application.isBatchMode` on
        ///         docs.unity3d.com
        ///     </a>
        /// </footer>
        public static bool IsBatchMode => Application.isBatchMode;

        /// <summary>
        ///     <para>Whether the player currently is compiling. Read-only.</para>
        /// </summary>
        /// <footer>
        ///     <a href="https://docs.unity3d.com/2021.2/Documentation/ScriptReference/EditorApplication-isPaused.html">
        ///         `EditorApplication.isPaused` on
        ///         docs.unity3d.com
        ///     </a>
        /// </footer>

        public static bool IsCompiling
        {
            get
            {
                return false
#if UNITY_EDITOR
                     ||
                       UnityEditor.EditorApplication.isCompiling
#endif
                    ;
            }
        }

        /// <summary>
        ///     <para>Is the current Runtime platform a known console platform.</para>
        /// </summary>
        /// <footer>
        ///     <a href="https://docs.unity3d.com/2021.2/Documentation/ScriptReference/30_search.html?q=Application-isConsolePlatform">
        ///         `Application.isConsolePlatform`
        ///         on docs.unity3d.com
        ///     </a>
        /// </footer>
        public static bool IsConsolePlatform => Application.isConsolePlatform;

        public static bool IsEditor => Application.isEditor;

        /// <summary>
        ///     <para>Whether the player currently has focus. Read-only.</para>
        /// </summary>
        /// <footer>
        ///     <a href="https://docs.unity3d.com/2021.2/Documentation/ScriptReference/30_search.html?q=Application-isFocused">
        ///         `Application.isFocused` on
        ///         docs.unity3d.com
        ///     </a>
        /// </footer>

        public static bool IsFocused => Application.isFocused;

        /// <summary>
        ///     <para>Is the current Runtime platform a known mobile platform.</para>
        /// </summary>
        /// <footer>
        ///     <a href="https://docs.unity3d.com/2021.2/Documentation/ScriptReference/30_search.html?q=Application-isMobilePlatform">
        ///         `Application.isMobilePlatform`
        ///         on docs.unity3d.com
        ///     </a>
        /// </footer>
        public static bool IsMobilePlatform => Application.isMobilePlatform;

        /// <summary>
        ///     <para>Whether the player currently is paused. Read-only.</para>
        /// </summary>
        /// <footer>
        ///     <a href="https://docs.unity3d.com/2021.2/Documentation/ScriptReference/EditorApplication-isPaused.html">
        ///         `EditorApplication.isPaused` on
        ///         docs.unity3d.com
        ///     </a>
        /// </footer>
        public static bool IsPaused
        {
            get
            {
                return false
#if UNITY_EDITOR
                     ||
                       UnityEditor.EditorApplication.isPaused
#endif
                    ;
            }
        }

        public static bool IsPlayer => !IsEditor;

        /// <summary>
        ///     <para>Returns true when called in any kind of built Player, or when called in the Editor in Play Mode (Read Only).</para>
        /// </summary>
        /// <footer>
        ///     <a href="https://docs.unity3d.com/2021.2/Documentation/ScriptReference/30_search.html?q=Application-isPlaying">
        ///         `Application.isPlaying` on
        ///         docs.unity3d.com
        ///     </a>
        /// </footer>
        public static bool IsPlaying
        {
            get
            {
                return Application.isPlaying
#if UNITY_EDITOR
                     ||
                       UnityEditor.EditorApplication.isPlaying
#endif
                    ;
            }
            set
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = value;
#else
                if (!value)
                {
                    Application.Quit(1);
                }
#endif
            }
        }

        /// <summary>
        ///     <para>Returns true when called in any kind of built Player, or when called in the Editor in Play Mode (Read Only).</para>
        /// </summary>
        /// <footer>
        ///     <a href="https://docs.unity3d.com/2021.2/Documentation/ScriptReference/30_search.html?q=Application-isPlaying">
        ///         `Application.isPlaying` on
        ///         docs.unity3d.com
        ///     </a>
        /// </footer>

        public static bool IsPlayingOrWillPlay
        {
            get
            {
                using (_PRF_IsPlayingOrWillPlay.Auto())
                {
                    return Application.isPlaying
#if UNITY_EDITOR
                         ||
                           UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode
#endif
                        ;
                }
            }
        }

        /// <summary>
        ///     <para>Whether the player currently is updating. Read-only.</para>
        /// </summary>
        /// <footer>
        ///     <a href="https://docs.unity3d.com/2021.2/Documentation/ScriptReference/EditorApplication-isPaused.html">
        ///         `EditorApplication.isPaused` on
        ///         docs.unity3d.com
        ///     </a>
        /// </footer>

        public static bool IsUpdating
        {
            get
            {
                return false
#if UNITY_EDITOR
                     ||
                       UnityEditor.EditorApplication.isUpdating
#endif
                    ;
            }
        }

        /// <summary>
        ///     <para>Returns the type of Internet reachability currently possible on the device.</para>
        /// </summary>
        /// <footer>
        ///     <a href="https://docs.unity3d.com/2021.2/Documentation/ScriptReference/30_search.html?q=Application-internetReachability">
        ///         `Application.internetReachability`
        ///         on docs.unity3d.com
        ///     </a>
        /// </footer>
        public static NetworkReachability InternetReachability => Application.internetReachability;

        /// <summary>
        ///     <para>Returns the platform the game is running on (Read Only).</para>
        /// </summary>
        /// <footer>
        ///     <a href="https://docs.unity3d.com/2021.2/Documentation/ScriptReference/30_search.html?q=Application-platform">
        ///         `Application.platform` on
        ///         docs.unity3d.com
        ///     </a>
        /// </footer>
        public static RuntimePlatform Platform => Application.platform;

        /// <summary>
        ///     <para>
        ///         The URL of the document. For WebGL, this a web URL. For Android, iOS, or Universal Windows Platform (UWP) this is a deep link URL. (Read
        ///         Only)
        ///     </para>
        /// </summary>
        /// <footer>
        ///     <a href="https://docs.unity3d.com/2021.2/Documentation/ScriptReference/30_search.html?q=Application-absoluteURL">
        ///         `Application.absoluteURL` on
        ///         docs.unity3d.com
        ///     </a>
        /// </footer>
        public static string AbsoluteURL => Application.absoluteURL;

        /// <summary>
        ///     <para>Returns a GUID for this build (Read Only).</para>
        /// </summary>
        /// <footer>
        ///     <a href="https://docs.unity3d.com/2021.2/Documentation/ScriptReference/30_search.html?q=Application-buildGUID">
        ///         `Application.buildGUID` on
        ///         docs.unity3d.com
        ///     </a>
        /// </footer>
        public static string BuildGuid => Application.buildGUID;

        /// <summary>
        ///     <para>A unique cloud project identifier. It is unique for every project (Read Only).</para>
        /// </summary>
        /// <footer>
        ///     <a href="https://docs.unity3d.com/2021.2/Documentation/ScriptReference/30_search.html?q=Application-cloudProjectId">
        ///         `Application.cloudProjectId`
        ///         on docs.unity3d.com
        ///     </a>
        /// </footer>
        public static string CloudProjectId => Application.cloudProjectId;

        /// <summary>
        ///     <para>Return application company name (Read Only).</para>
        /// </summary>
        /// <footer>
        ///     <a href="https://docs.unity3d.com/2021.2/Documentation/ScriptReference/30_search.html?q=Application-companyName">
        ///         `Application.companyName` on
        ///         docs.unity3d.com
        ///     </a>
        /// </footer>
        public static string CompanyName => Application.companyName;

        /// <summary>
        ///     <para>Returns the path to the console log file, or an empty string if the current platform does not support log files.</para>
        /// </summary>
        /// <footer>
        ///     <a href="https://docs.unity3d.com/2021.2/Documentation/ScriptReference/30_search.html?q=Application-consoleLogPath">
        ///         `Application.consoleLogPath`
        ///         on docs.unity3d.com
        ///     </a>
        /// </footer>
        public static string ConsoleLogPath => Application.consoleLogPath;

        /// <summary>
        ///     <para>Contains the path to the game data folder on the target device (Read Only).</para>
        /// </summary>
        /// <footer>
        ///     <a href="https://docs.unity3d.com/2021.2/Documentation/ScriptReference/30_search.html?q=Application-dataPath">
        ///         `Application.dataPath` on
        ///         docs.unity3d.com
        ///     </a>
        /// </footer>
        public static string DataPath => Application.dataPath;

        /// <summary>
        ///     <para>
        ///         Returns application identifier at runtime. On Apple platforms this is the 'bundleIdentifier' saved in the info.plist file, on Android it's
        ///         the 'package' from the AndroidManifest.xml.
        ///     </para>
        /// </summary>
        /// <footer>
        ///     <a href="https://docs.unity3d.com/2021.2/Documentation/ScriptReference/30_search.html?q=Application-identifier">
        ///         `Application.identifier` on
        ///         docs.unity3d.com
        ///     </a>
        /// </footer>
        public static string Identifier => Application.identifier;

        /// <summary>
        ///     <para>Returns the name of the store or package that installed the application (Read Only).</para>
        /// </summary>
        /// <footer>
        ///     <a href="https://docs.unity3d.com/2021.2/Documentation/ScriptReference/30_search.html?q=Application-installerName">
        ///         `Application.installerName` on
        ///         docs.unity3d.com
        ///     </a>
        /// </footer>
        public static string InstallerName => Application.installerName;

        /// <summary>
        ///     <para>(Read Only) Contains the path to a persistent data directory.</para>
        /// </summary>
        /// <footer>
        ///     <a href="https://docs.unity3d.com/2021.2/Documentation/ScriptReference/30_search.html?q=Application-persistentDataPath">
        ///         `Application.persistentDataPath`
        ///         on docs.unity3d.com
        ///     </a>
        /// </footer>
        public static string PersistentDataPath => Application.persistentDataPath;

        /// <summary>
        ///     <para>Returns application product name (Read Only).</para>
        /// </summary>
        /// <footer>
        ///     <a href="https://docs.unity3d.com/2021.2/Documentation/ScriptReference/30_search.html?q=Application-productName">
        ///         `Application.productName` on
        ///         docs.unity3d.com
        ///     </a>
        /// </footer>
        public static string ProductName => Application.productName;

        /// <summary>
        ///     <para>The path to the StreamingAssets folder (Read Only).</para>
        /// </summary>
        /// <footer>
        ///     <a href="https://docs.unity3d.com/2021.2/Documentation/ScriptReference/30_search.html?q=Application-streamingAssetsPath">
        ///         `Application.streamingAssetsPath`
        ///         on docs.unity3d.com
        ///     </a>
        /// </footer>
        public static string StreamingAssetsPath => Application.streamingAssetsPath;

        /// <summary>
        ///     <para>Contains the path to a temporary data / cache directory (Read Only).</para>
        /// </summary>
        /// <footer>
        ///     <a href="https://docs.unity3d.com/2021.2/Documentation/ScriptReference/30_search.html?q=Application-temporaryCachePath">
        ///         `Application.temporaryCachePath`
        ///         on docs.unity3d.com
        ///     </a>
        /// </footer>
        public static string TemporaryCachePath => Application.temporaryCachePath;

        /// <summary>
        ///     <para>The version of the Unity runtime used to play the content.</para>
        /// </summary>
        /// <footer>
        ///     <a href="https://docs.unity3d.com/2021.2/Documentation/ScriptReference/30_search.html?q=Application-unityVersion">
        ///         `Application.unityVersion` on
        ///         docs.unity3d.com
        ///     </a>
        /// </footer>
        public static string UnityVersion => Application.unityVersion;

        /// <summary>
        ///     <para>Returns application version number  (Read Only).</para>
        /// </summary>
        /// <footer>
        ///     <a href="https://docs.unity3d.com/2021.2/Documentation/ScriptReference/30_search.html?q=Application-version">
        ///         `Application.version` on
        ///         docs.unity3d.com
        ///     </a>
        /// </footer>
        public static string Version => Application.version;

        /// <summary>
        ///     <para>The language the user's operating system is running in.</para>
        /// </summary>
        /// <footer>
        ///     <a href="https://docs.unity3d.com/2021.2/Documentation/ScriptReference/30_search.html?q=Application-systemLanguage">
        ///         `Application.systemLanguage`
        ///         on docs.unity3d.com
        ///     </a>
        /// </footer>
        public static SystemLanguage SystemLanguage => Application.systemLanguage;

        /// <summary>
        ///     <para>Should the player be running when the application is in the background?</para>
        /// </summary>
        /// <footer>
        ///     <a href="https://docs.unity3d.com/2021.2/Documentation/ScriptReference/30_search.html?q=Application-runInBackground">
        ///         `Application.runInBackground`
        ///         on docs.unity3d.com
        ///     </a>
        /// </footer>
        public static bool RunInBackground
        {
            get => Application.runInBackground;
            set => Application.runInBackground = value;
        }

        /// <summary>
        ///     <para>Specifies the frame rate at which Unity tries to render your game.</para>
        /// </summary>
        /// <footer>
        ///     <a href="https://docs.unity3d.com/2021.2/Documentation/ScriptReference/30_search.html?q=Application-targetFrameRate">
        ///         `Application.targetFrameRate`
        ///         on docs.unity3d.com
        ///     </a>
        /// </footer>
        public static int TargetFrameRate
        {
            get => Application.targetFrameRate;
            set => Application.targetFrameRate = value;
        }

        /// <summary>
        ///     <para>Priority of background loading thread.</para>
        /// </summary>
        /// <footer>
        ///     <a href="https://docs.unity3d.com/2021.2/Documentation/ScriptReference/30_search.html?q=Application-backgroundLoadingPriority">
        ///         `Application.backgroundLoadingPriority`
        ///         on docs.unity3d.com
        ///     </a>
        /// </footer>
        public static ThreadPriority BackgroundLoadingPriority
        {
            get => Application.backgroundLoadingPriority;
            set => Application.backgroundLoadingPriority = value;
        }

        public static void ForceCrash(int mode)
        {
            Utils.ForceCrash((ForcedCrashCategory)mode);
        }

        /// <summary>
        ///     <para>Returns an array of feature tags in use for this build.</para>
        /// </summary>
        /// <footer>
        ///     <a href="https://docs.unity3d.com/2021.2/Documentation/ScriptReference/30_search.html?q=Application.GetBuildTags">
        ///         `Application.GetBuildTags` on
        ///         docs.unity3d.com
        ///     </a>
        /// </footer>
        public static string[] GetBuildTags()
        {
            using (_PRF_GetBuildTags.Auto())
            {
                return Application.GetBuildTags();
            }
        }

        /// <summary>
        ///     <para>Get stack trace logging options. The default value is StackTraceLogType.ScriptOnly.</para>
        /// </summary>
        /// <param name="logType"></param>
        /// <footer>
        ///     <a href="https://docs.unity3d.com/2021.2/Documentation/ScriptReference/30_search.html?q=Application.GetStackTraceLogType">
        ///         `Application.GetStackTraceLogType`
        ///         on docs.unity3d.com
        ///     </a>
        /// </footer>
        public static StackTraceLogType GetStackTraceLogType(LogType logType)
        {
            using (_PRF_GetStackTraceLogType.Auto())
            {
                return Application.GetStackTraceLogType(logType);
            }
        }

        /// <summary>
        ///     <para>
        ///         Opens the URL specified, subject to the permissions and limitations of your app’s current platform and environment. This is handled in
        ///         different ways depending on the nature of the URL, and with different security restrictions, depending on the runtime platform.
        ///     </para>
        /// </summary>
        /// <param name="url">The URL to open.</param>
        /// <footer>
        ///     <a href="https://docs.unity3d.com/2021.2/Documentation/ScriptReference/30_search.html?q=Application.OpenURL">
        ///         `Application.OpenURL` on
        ///         docs.unity3d.com
        ///     </a>
        /// </footer>
        public static void OpenURL(string url)
        {
            using (_PRF_OpenURL.Auto())
            {
                Application.OpenURL(url);
            }
        }

        /// <summary>
        ///     <para>Quits the player application.</para>
        /// </summary>
        /// <param name="exitCode">An optional exit code to return when the player application terminates on Windows, Mac and Linux. Defaults to 0.</param>
        /// <footer>
        ///     <a href="https://docs.unity3d.com/2021.2/Documentation/ScriptReference/30_search.html?q=Application.Quit">`Application.Quit` on docs.unity3d.com</a>
        /// </footer>
        public static void Quit(int exitCode)
        {
            Application.Quit(exitCode);
        }

        /// <summary>
        ///     <para>Quits the player application.</para>
        /// </summary>
        /// <footer>
        ///     <a href="https://docs.unity3d.com/2021.2/Documentation/ScriptReference/30_search.html?q=Application.Quit">`Application.Quit` on docs.unity3d.com</a>
        /// </footer>
        public static void Quit()
        {
            Application.Quit();
        }

        public static bool RequestAdvertisingIdentifierAsync(
            Application.AdvertisingIdentifierCallback delegateMethod)
        {
            using (_PRF_RequestAdvertisingIdentifierAsync.Auto())
            {
                return Application.RequestAdvertisingIdentifierAsync(delegateMethod);
            }
        }

        /// <summary>
        ///     <para>Set an array of feature tags for this build.</para>
        /// </summary>
        /// <param name="buildTags"></param>
        /// <footer>
        ///     <a href="https://docs.unity3d.com/2021.2/Documentation/ScriptReference/30_search.html?q=Application.SetBuildTags">
        ///         `Application.SetBuildTags` on
        ///         docs.unity3d.com
        ///     </a>
        /// </footer>
        public static void SetBuildTags(string[] buildTags)
        {
            using (_PRF_SetBuildTags.Auto())
            {
                Application.SetBuildTags(buildTags);
            }
        }

        /// <summary>
        ///     <para>Set stack trace logging options. The default value is StackTraceLogType.ScriptOnly.</para>
        /// </summary>
        /// <param name="logType"></param>
        /// <param name="stackTraceType"></param>
        /// <footer>
        ///     <a href="https://docs.unity3d.com/2021.2/Documentation/ScriptReference/30_search.html?q=Application.SetStackTraceLogType">
        ///         `Application.SetStackTraceLogType`
        ///         on docs.unity3d.com
        ///     </a>
        /// </footer>
        public static void SetStackTraceLogType(LogType logType, StackTraceLogType stackTraceType)
        {
            using (_PRF_SetStackTraceLogType.Auto())
            {
                Application.SetStackTraceLogType(logType, stackTraceType);
            }
        }

        /// <summary>
        ///     <para>Unloads the Unity Player.</para>
        /// </summary>
        /// <footer>
        ///     <a href="https://docs.unity3d.com/2021.2/Documentation/ScriptReference/30_search.html?q=Application.Unload">
        ///         `Application.Unload` on
        ///         docs.unity3d.com
        ///     </a>
        /// </footer>
        public static void Unload()
        {
            Application.Unload();
        }

        #region Profiling

        private const string _PRF_PFX = nameof(AppalachiaApplication) + ".";

        private static readonly ProfilerMarker _PRF_IsPlayingOrWillPlay =
            new ProfilerMarker(_PRF_PFX + nameof(IsPlayingOrWillPlay));

        private static readonly ProfilerMarker _PRF_GetBuildTags =
            new ProfilerMarker(_PRF_PFX + nameof(GetBuildTags));

        private static readonly ProfilerMarker _PRF_GetStackTraceLogType =
            new ProfilerMarker(_PRF_PFX + nameof(GetStackTraceLogType));

        private static readonly ProfilerMarker _PRF_OpenURL = new ProfilerMarker(_PRF_PFX + nameof(OpenURL));

        private static readonly ProfilerMarker _PRF_RequestAdvertisingIdentifierAsync =
            new ProfilerMarker(_PRF_PFX + nameof(RequestAdvertisingIdentifierAsync));

        private static readonly ProfilerMarker _PRF_SetBuildTags =
            new ProfilerMarker(_PRF_PFX + nameof(SetBuildTags));

        private static readonly ProfilerMarker _PRF_SetStackTraceLogType =
            new ProfilerMarker(_PRF_PFX + nameof(SetStackTraceLogType));

        #endregion
    }
}
