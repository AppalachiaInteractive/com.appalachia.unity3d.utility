#region

using System;
using System.Collections.Generic;
using Appalachia.Utility.Logging;
using Appalachia.Utility.Strings;
using Unity.Profiling;
using UnityEngine;
using Object = UnityEngine.Object;

#endregion

namespace Appalachia.Utility.Extensions
{
#if UNITY_EDITOR
    public static partial class GameObjectExtensions
    {
        public static string EditorGetFriendlyPath(this GameObject gameObj)
        {
            using (_PRF_EditorGetFriendlyPath.Auto())
            {
                if (EditorIsSceneObject(gameObj))
                {
                    return GetFullName(gameObj) + " (Scene Object)";
                }

                var assetPath = UnityEditor.AssetDatabase.GetAssetOrScenePath(gameObj);
                return assetPath + GetFullName(gameObj) + " (Asset Object)";
            }
        }

        public static GameObject EditorGetGameObjectFromComponent(this Object obj)
        {
            using (_PRF_EditorGetGameObjectFromComponent.Auto())
            {
                if (!obj)
                {
                    return null;
                }

                var gameObjID =
                    UnityEditorInternal.InternalEditorUtility.GetGameObjectInstanceIDFromComponent(
                        obj.GetInstanceID()
                    );
                return gameObjID != 0
                    ? (GameObject)UnityEditor.EditorUtility.InstanceIDToObject(gameObjID)
                    : obj as GameObject;
            }
        }

        public static IEnumerable<T> EditorGetScriptableObjects<T>(this GameObject gameObj)
            where T : ScriptableObject
        {
            using (_PRF_EditorGetScriptableObjects.Auto())
            {
                var returnList = new List<T>();

                var so = new UnityEditor.SerializedObject(gameObj);
                var spComponents = so.FindProperty("m_Component");

                for (var i = 0; i < spComponents.arraySize; ++i)
                {
                    var spElement = spComponents.GetArrayElementAtIndex(i);
                    var spObjRef = spElement.FindPropertyRelative("second");

                    var scriptObj = spObjRef.objectReferenceValue as T;
                    if (scriptObj)
                    {
                        returnList.Add(scriptObj);
                    }
                }

                return returnList;
            }
        }

        public static bool EditorIsSceneObject(this GameObject gameObj)
        {
            using (_PRF_EditorIsSceneObject.Auto())
            {
                var assetPath = UnityEditor.AssetDatabase.GetAssetOrScenePath(gameObj);
                return !UnityEditor.EditorUtility.IsPersistent(gameObj) || assetPath.EndsWith(".unity");
            }
        }

        public static GameObject GetAsset(this GameObject prefab)
        {
            using (_PRF_GetAsset.Auto())
            {
                if (!UnityEditor.PrefabUtility.IsPartOfPrefabInstance(prefab))
                {
                    if (!UnityEditor.PrefabUtility.IsPartOfPrefabAsset(prefab))
                    {
                        return null;
                    }

                    return prefab;
                }

                return UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>(
                    UnityEditor.PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(prefab)
                );
            }
        }

        public static string GetAssetGUID(this GameObject go)
        {
            using (_PRF_GetAssetGUID.Auto())
            {
                if (_assetGUIDLookup == null)
                {
                    _assetGUIDLookup = new Dictionary<int, string>();
                }

                var hashCode = go.GetHashCode();

                if (_assetGUIDLookup.ContainsKey(hashCode))
                {
                    return _assetGUIDLookup[hashCode];
                }

                if (UnityEditor.AssetDatabase.TryGetGUIDAndLocalFileIdentifier(
                    go,
                    out var assetGUID,
                    out long _
                ))
                {
                    _assetGUIDLookup.Add(hashCode, assetGUID);

                    return assetGUID;
                }

                return null;
            }
        }

#if UNITY_EDITOR

        private static readonly ProfilerMarker _PRF_Editor_GetTransformFromRelativePath =
            new ProfilerMarker(_PRF_PFX + nameof(Editor_GetTransformFromRelativePath));

        public static Transform Editor_GetTransformFromRelativePath(this Transform relativeTo, string path)
        {
            using (_PRF_Editor_GetTransformFromRelativePath.Auto())
            {
                if (path.IsNullOrWhiteSpace())
                {
                    throw new ArgumentNullException(nameof(path));
                }

                // path = "MyObject/Child1/child2";
                var pathParts = path.Split('/'); // [MyObject], [Child1], [child2]

                var currentTransform = relativeTo;

                for (var pathPartIndex = 0; pathPartIndex < pathParts.Length; pathPartIndex++)
                {
                    var pathPart = pathParts[pathPartIndex];

                    var foundChild = currentTransform.Find(pathPart);

                    if (foundChild == null)
                    {
                        var logMessage = ZString.Format(
                            "Could not find transform path [{0}] relative to [{1}]",
                            path,
                            relativeTo.gameObject.name
                        );

                        AppaLog.Context.Extensions.Warn(logMessage, relativeTo);

                        return null;
                    }

                    currentTransform = foundChild;
                }

                return currentTransform;
            }
        }
#endif

        public static bool IsDescendentOf(this GameObject prospectiveChild, GameObject prospectiveParent)
        {
            using (_PRF_IsDescendentOf.Auto())
            {
                return IsDescendentOf(prospectiveChild.transform, prospectiveParent.transform);
            }
        }

        public static bool IsDescendentOf(this Transform prospectiveChild, Transform prospectiveParent)
        {
            using (_PRF_IsDescendentOf.Auto())
            {
                if ((prospectiveChild == null) || (prospectiveParent == null))
                {
                    return false;
                }

                if (prospectiveChild.parent == null)
                {
                    return false;
                }

                var currentlyChecking = prospectiveChild;

                while (currentlyChecking.parent != null)
                {
                    if (currentlyChecking == prospectiveParent)
                    {
                        return true;
                    }

                    currentlyChecking = currentlyChecking.parent;
                }

                return false;
            }
        }

        #region Profiling

        private static readonly ProfilerMarker _PRF_IsDescendentOf =
            new ProfilerMarker(_PRF_PFX + nameof(IsDescendentOf));

        private static readonly ProfilerMarker _PRF_GetAssetGUID =
            new ProfilerMarker(_PRF_PFX + nameof(GetAssetGUID));

        private static readonly ProfilerMarker _PRF_EditorGetFriendlyPath =
            new ProfilerMarker(_PRF_PFX + nameof(EditorGetFriendlyPath));

        private static readonly ProfilerMarker _PRF_EditorGetGameObjectFromComponent =
            new ProfilerMarker(_PRF_PFX + nameof(EditorGetGameObjectFromComponent));

        private static readonly ProfilerMarker _PRF_EditorGetScriptableObjects =
            new ProfilerMarker(_PRF_PFX + nameof(EditorGetScriptableObjects));

        private static readonly ProfilerMarker _PRF_EditorIsSceneObject =
            new ProfilerMarker(_PRF_PFX + nameof(EditorIsSceneObject));

        private static readonly ProfilerMarker
            _PRF_GetAsset = new ProfilerMarker(_PRF_PFX + nameof(GetAsset));

        #endregion
    }
#endif
}
