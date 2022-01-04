#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.Profiling;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Build;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.AddressableAssets.Settings.GroupSchemas;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Appalachia.Utility.Extensions
{
    [InitializeOnLoad]
    public static partial class AddressableExtensions
    {
        #region Constants and Static Readonly

        private static readonly HashSet<string> ExcludedExtensions;

        #endregion

        static AddressableExtensions()
        {
            ExcludedExtensions = new HashSet<string>(
                new[] { ".cs", ".js", ".boo", ".exe", ".dll", ".meta", ".preset", ".asmdef" }
            );
        }

        #region Static Fields and Autoproperties

        private static HashSet<Type> _runtimeTypes;

        #endregion

        public static bool EnsureIsAddressable<T>(this T objectAtPath, out string Guid)
            where T : ScriptableObject
        {
            using (_PRF_EnsureIsAddressable.Auto())
            {
                if (!objectAtPath.IsAddressable(out var info))
                {
                    objectAtPath.SetAddressableGroup();

                    if (objectAtPath.IsAddressable(out var info2))
                    {
                        Guid = info2.Guid;
                        return true;
                    }
                }
                else
                {
                    Guid = info.Guid;
                    return true;
                }

                Guid = null;
                return false;
            }
        }

        public static bool EnsurePrefabIsAddressable(this GameObject objectAtPath, out string Guid)
        {
            using (_PRF_EnsurePrefabIsAddressable.Auto())
            {
                if (!objectAtPath.IsAddressable(out var info))
                {
                    objectAtPath.SetAddressableGroup();

                    if (objectAtPath.IsAddressable(out var info2))
                    {
                        Guid = info2.Guid;
                        return true;
                    }
                }
                else
                {
                    Guid = info.Guid;
                    return true;
                }

                Guid = null;
                return false;
            }
        }

        public static TargetInfo GetAddressableTargetInfo(this Object o)
        {
            using (_PRF_GetAddressableTargetInfo.Auto())
            {
                if (TryGetPathAndGUIDFromTarget(o, out var path, out var guid))
                {
                    var mainAssetType = AssetDatabase.GetMainAssetTypeAtPath(path);

                    // Is asset
                    if ((mainAssetType != null) && !BuildUtility.IsEditorAssembly(mainAssetType.Assembly))
                    {
                        var isMainAsset = o is AssetImporter || AssetDatabase.IsMainAsset(o);
                        var info = new TargetInfo
                        {
                            TargetObject = o,
                            Guid = guid,
                            Path = path,
                            IsMainAsset = isMainAsset
                        };

                        var aaSettings = AddressableAssetSettingsDefaultObject.Settings;

                        if (aaSettings != null)
                        {
                            var entry = aaSettings.FindAssetEntry(guid, true);
                            if (entry != null)
                            {
                                info.MainAssetEntry = entry;
                            }
                        }

                        return info;
                    }
                }

                return null;
            }
        }

        public static bool IsAddressable(this Type t)
        {
            using (_PRF_IsAddressable.Auto())
            {
                _runtimeTypes ??= MonoImporter.GetAllRuntimeMonoScripts()
                                              .Select(ms => ms.GetClass())
                                              .ToHashSet();

                return !BuildUtility.IsEditorAssembly(t.Assembly) || !_runtimeTypes.Contains(t);
            }
        }

        public static bool IsAddressable(this Object o, out TargetInfo targetInfo)
        {
            using (_PRF_IsAddressable.Auto())
            {
                targetInfo = GetAddressableTargetInfo(o);

                return targetInfo is { MainAssetEntry: { } };
            }
        }

        public static void SetAddressableGroup(this Object obj, string groupName = "Uncategorized")
        {
            using (_PRF_SetAddressableGroup.Auto())
            {
                var settings = AddressableAssetSettingsDefaultObject.Settings;

                if (settings)
                {
                    var group = settings.FindGroup(groupName);
                    if (!group)
                    {
                        group = settings.CreateGroup(
                            groupName,
                            false,
                            false,
                            true,
                            null,
                            typeof(ContentUpdateGroupSchema),
                            typeof(BundledAssetGroupSchema)
                        );
                    }

                    var assetpath = AssetDatabase.GetAssetPath(obj);
                    var guid = AssetDatabase.AssetPathToGUID(assetpath);

                    var e = settings.CreateOrMoveEntry(guid, group, false, false);
                    var entriesAdded = new List<AddressableAssetEntry> { e };

                    group.SetDirty(
                        AddressableAssetSettings.ModificationEvent.EntryMoved,
                        entriesAdded,
                        false,
                        true
                    );
                    settings.SetDirty(
                        AddressableAssetSettings.ModificationEvent.EntryMoved,
                        entriesAdded,
                        true
                    );
                }
            }
        }

        internal static bool IsInResources(string path)
        {
            using (_PRF_IsInResources.Auto())
            {
                return path.Replace('\\', '/').ToLower().Contains("/resources/");
            }
        }

        internal static bool IsPathValidForEntry(string path)
        {
            using (_PRF_IsPathValidForEntry.Auto())
            {
                if (string.IsNullOrEmpty(path))
                {
                    return false;
                }

                if (!path.StartsWith("assets", StringComparison.OrdinalIgnoreCase) &&
                    !IsPathValidPackageAsset(path))
                {
                    return false;
                }

                if ((path == CommonStrings.UnityEditorResourcePath) ||
                    (path == CommonStrings.UnityDefaultResourcePath) ||
                    (path == CommonStrings.UnityBuiltInExtraPath))
                {
                    return false;
                }

                if (path.EndsWith("/Editor") || path.Contains("/Editor/"))
                {
                    return false;
                }

                if (path == "Assets")
                {
                    return false;
                }

                var settings = AddressableAssetSettingsDefaultObject.SettingsExists
                    ? AddressableAssetSettingsDefaultObject.Settings
                    : null;
                if (((settings != null) && path.StartsWith(settings.ConfigFolder)) ||
                    path.StartsWith(AddressableAssetSettingsDefaultObject.kDefaultConfigFolder))
                {
                    return false;
                }

                return !ExcludedExtensions.Contains(Path.GetExtension(path));
            }
        }

        internal static bool IsPathValidPackageAsset(string path)
        {
            using (_PRF_IsPathValidPackageAsset.Auto())
            {
                var convertPath = path.ToLower().Replace("\\", "/");
                var splitPath = convertPath.Split('/');

                if (splitPath.Length < 3)
                {
                    return false;
                }

                if (splitPath[0] != "packages")
                {
                    return false;
                }

                if (splitPath[2] == "package.json")
                {
                    return false;
                }

                return true;
            }
        }

        internal static bool TryGetPathAndGUIDFromTarget(Object target, out string path, out string guid)
        {
            using (_PRF_TryGetPathAndGUIDFromTarget.Auto())
            {
                guid = string.Empty;
                path = string.Empty;
                if (target == null)
                {
                    return false;
                }

                path = AssetDatabase.GetAssetOrScenePath(target);
                if (!IsPathValidForEntry(path))
                {
                    return false;
                }

                if (!AssetDatabase.TryGetGUIDAndLocalFileIdentifier(target, out guid, out long id))
                {
                    return false;
                }

                return true;
            }
        }

        #region Nested type: CommonStrings

        /// <summary>
        ///     Static class of common strings and string formats used through out the build process
        /// </summary>
        private static class CommonStrings
        {
            #region Constants and Static Readonly

            /// <summary>
            ///     Default Asset Bundle internal file name format
            /// </summary>
            public const string AssetBundleNameFormat = "archive:/{0}/{0}";

            /// <summary>
            ///     Default Scene Bundle internal file name format
            /// </summary>
            public const string SceneBundleNameFormat = "archive:/{0}/{1}.sharedAssets";

            /// <summary>
            ///     Unity Built-In Extras path
            /// </summary>
            public const string UnityBuiltInExtraPath = "resources/unity_builtin_extra";

            /// <summary>
            ///     Unity Default Resources path
            /// </summary>
            public const string UnityDefaultResourcePath = "library/unity default resources";

            /// <summary>
            ///     Unity Editor Resources path
            /// </summary>
            public const string UnityEditorResourcePath = "library/unity editor resources";

            #endregion
        }

        #endregion

        #region Nested type: TargetInfo

        public class TargetInfo
        {
            #region Fields and Autoproperties

            public AddressableAssetEntry MainAssetEntry;
            public bool IsMainAsset;
            public Object TargetObject;
            public string Guid;
            public string Path;

            #endregion

            public string Address
            {
                get
                {
                    if (MainAssetEntry == null)
                    {
                        throw new NullReferenceException(
                            $"No Entry set for Target info with AssetPath {Path}"
                        );
                    }

                    return MainAssetEntry.address;
                }
            }
        }

        #endregion

        #region Profiling

        private const string _PRF_PFX = nameof(AddressableExtensions) + ".";

        private static readonly ProfilerMarker _PRF_IsInResources =
            new ProfilerMarker(_PRF_PFX + nameof(IsInResources));

        private static readonly ProfilerMarker _PRF_IsPathValidForEntry =
            new ProfilerMarker(_PRF_PFX + nameof(IsPathValidForEntry));

        private static readonly ProfilerMarker _PRF_IsPathValidPackageAsset =
            new ProfilerMarker(_PRF_PFX + nameof(IsPathValidPackageAsset));

        private static readonly ProfilerMarker _PRF_EnsurePrefabIsAddressable =
            new ProfilerMarker(_PRF_PFX + nameof(EnsurePrefabIsAddressable));

        private static readonly ProfilerMarker _PRF_EnsureIsAddressable =
            new ProfilerMarker(_PRF_PFX + nameof(EnsureIsAddressable));

        private static readonly ProfilerMarker _PRF_IsAddressable =
            new ProfilerMarker(_PRF_PFX + nameof(IsAddressable));

        private static readonly ProfilerMarker _PRF_SetAddressableGroup =
            new ProfilerMarker(_PRF_PFX + nameof(SetAddressableGroup));

        private static readonly ProfilerMarker _PRF_GetAddressableTargetInfo =
            new ProfilerMarker(_PRF_PFX + nameof(GetAddressableTargetInfo));

        private static readonly ProfilerMarker _PRF_TryGetPathAndGUIDFromTarget =
            new ProfilerMarker(_PRF_PFX + nameof(TryGetPathAndGUIDFromTarget));

        #endregion
    }
}
#endif
