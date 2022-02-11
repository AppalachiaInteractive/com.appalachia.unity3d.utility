#if UNITY_EDITOR

using System;
using System.IO;
using System.Linq;
using Unity.Profiling;
using UnityEditor;
using UnityEngine;
using PackageInfo = UnityEditor.PackageManager.PackageInfo;

namespace Appalachia.Utility.WakaTime
{
    internal static class Configuration
    {
        #region Constants and Static Readonly

        private const string WAKATIME_PATH_INDICATOR = "wakatime";

        #endregion

        #region Static Fields and Autoproperties

        internal static bool Debugging = true;
        internal static bool Enabled = true;
        internal static bool WakaTimePathAuto = true;

        internal static string ApiKey = "";
        internal static string ProjectName = "";

        [NonSerialized] private static PackageInfo _package;

        [NonSerialized] private static string _wakaTimePath = "";

        #endregion

        private static readonly ProfilerMarker _PRF_WakaTimePath_Search =
            new ProfilerMarker(_PRF_PFX + nameof(WakaTimePath) + ".Search");

        
        internal static string WakaTimePath
        {
            get
            {
                using (_PRF_WakaTimePath.Auto())
                {
                    string SearchForWakaTimePath(string assetsBasePath)
                    {
                        using (_PRF_WakaTimePath_Search.Auto())
                        {
                            return Directory
                                  .EnumerateFileSystemEntries(
                                       assetsBasePath,
                                       "cli.py",
                                       SearchOption.AllDirectories
                                   )
                                  .FirstOrDefault(
                                       p => p.Contains(
                                           WAKATIME_PATH_INDICATOR,
                                           StringComparison.InvariantCultureIgnoreCase
                                       )
                                   );
                        }
                    }

                    if (WakaTimePathAuto)
                    {
                        if (string.IsNullOrWhiteSpace(_wakaTimePath) || !File.Exists(_wakaTimePath))
                        {
                            var assetsBasePath = Application.dataPath;

                            _wakaTimePath = SearchForWakaTimePath(assetsBasePath);

                            if (_wakaTimePath == null)
                            {
                                var libraryBasePath = assetsBasePath.Replace(
                                    "Assets",
                                    "Library/PackageCache"
                                );

                                _wakaTimePath = SearchForWakaTimePath(libraryBasePath);
                            }
                        }
                    }

                    return _wakaTimePath;
                }
            }
            set
            {
                _wakaTimePath = value;
                EditorPrefs.SetString(Constants.ConfigurationKeys.WakaTimePath, _wakaTimePath);
            }
        }

        internal static void RefreshPreferences()
        {
            using (_PRF_RefreshPreferences.Auto())
            {
                if (EditorPrefs.HasKey(Constants.ConfigurationKeys.WakaTimePathAuto))
                {
                    WakaTimePathAuto = EditorPrefs.GetBool(Constants.ConfigurationKeys.WakaTimePathAuto);
                }

                if (EditorPrefs.HasKey(Constants.ConfigurationKeys.WakaTimePath))
                {
                    WakaTimePath = EditorPrefs.GetString(Constants.ConfigurationKeys.WakaTimePath);
                }

                if (EditorPrefs.HasKey(Constants.ConfigurationKeys.ApiKey))
                {
                    ApiKey = EditorPrefs.GetString(Constants.ConfigurationKeys.ApiKey);
                }

                if (EditorPrefs.HasKey(Constants.ConfigurationKeys.Enabled))
                {
                    Enabled = EditorPrefs.GetBool(Constants.ConfigurationKeys.Enabled);
                }

                if (EditorPrefs.HasKey(Constants.ConfigurationKeys.Debugging))
                {
                    Debugging = EditorPrefs.GetBool(Constants.ConfigurationKeys.Debugging);
                }

                ProjectName = File.Exists(Constants.Project.WakaTimeProjectFile)
                    ? File.ReadAllLines(Constants.Project.WakaTimeProjectFile)[0]
                    : Application.productName;
            }
        }

        internal static void SavePreferences()
        {
            using (_PRF_SavePreferences.Auto())
            {
                EditorPrefs.SetString(Constants.ConfigurationKeys.ApiKey, ApiKey);
                EditorPrefs.SetBool(Constants.ConfigurationKeys.WakaTimePathAuto, WakaTimePathAuto);
                EditorPrefs.SetString(Constants.ConfigurationKeys.WakaTimePath, WakaTimePath);
                EditorPrefs.SetBool(Constants.ConfigurationKeys.Enabled,   Enabled);
                EditorPrefs.SetBool(Constants.ConfigurationKeys.Debugging, Debugging);
            }
        }

        #region Profiling

        private const string _PRF_PFX = nameof(Configuration) + ".";

        private static readonly ProfilerMarker _PRF_WakaTimePath =
            new ProfilerMarker(_PRF_PFX + nameof(WakaTimePath));

        private static readonly ProfilerMarker _PRF_SavePreferences =
            new ProfilerMarker(_PRF_PFX + nameof(SavePreferences));

        private static readonly ProfilerMarker _PRF_RefreshPreferences =
            new ProfilerMarker(_PRF_PFX + nameof(RefreshPreferences));

        #endregion
    }
}
#endif
