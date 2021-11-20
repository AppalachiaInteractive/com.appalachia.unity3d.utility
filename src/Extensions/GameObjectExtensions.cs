#region

using System;
using System.Collections.Generic;
using System.Linq;
using Appalachia.Utility.Logging;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

#endregion

namespace Appalachia.Utility.Extensions
{
    public static partial class GameObjectExtensions
    {
        #region Static Fields and Autoproperties

        private static Dictionary<int, string> _assetGUIDLookup = new();
        private static List<GameObject> s_GameObjects = new List<GameObject>();

        #endregion

        public static GameObject AddChild(this GameObject go, string name)
        {
            using (_PRF_AddChild.Auto())
            {
                var newObject = new GameObject(name);
                newObject.transform.SetParent(go.transform);

                return newObject;
            }
        }

        public static GameObject CreateGameObjectInScene(string name, Scene scene)
        {
            using (_PRF_CreateGameObjectInScene.Auto())
            {
                var oldActiveScene = SceneManager.GetActiveScene();
                SceneManager.SetActiveScene(scene);
                var gameObject = new GameObject(name);
                SceneManager.SetActiveScene(oldActiveScene);

                return gameObject;
            }
        }

        public static void DestroySafely(this Object o)
        {
            using (_PRF_GameObjectExtensions_DestroySafely.Auto())
            {
                var wasNull = false;

                try
                {
                    if (o is IDisposable i)
                    {
                        i.Dispose();
                    }
                }
                catch (NullReferenceException)
                {
                    wasNull = true;
                }
                catch (Exception ex)
                {
                    AppaLog.Error($"Not able to dispose of [{o.name}] before destroying.", o);
                    AppaLog.Exception(ex, o);
                }
                finally
                {
                    if (!wasNull)
                    {
                        try
                        {
                            if (Application.isPlaying)
                            {
                                Object.Destroy(o);
                            }
                            else
                            {
                                Object.DestroyImmediate(o);
                            }
                        }
                        catch (Exception ex)
                        {
                            AppaLog.Error("Exception while destroying object.", o);
                            AppaLog.Exception(ex, o);
                        }
                    }
                }
            }
        }

        public static void DestroySafely<T>(this T o)
            where T : Object, IDisposable
        {
            using (_PRF_GameObjectExtensions_DestroySafely.Auto())
            {
                var wasNull = false;

                try
                {
                    o.Dispose();
                }
                catch (NullReferenceException)
                {
                    wasNull = true;
                }
                catch (Exception ex)
                {
                    AppaLog.Error($"Not able to dispose of [{typeof(T).Name} before destroying.", o);
                    AppaLog.Exception(ex, o);
                }
                finally
                {
                    if (!wasNull)
                    {
                        try
                        {
                            if (Application.isPlaying)
                            {
                                Object.Destroy(o);
                            }
                            else
                            {
                                Object.DestroyImmediate(o);
                            }
                        }
                        catch (Exception ex)
                        {
                            AppaLog.Error("Exception while destroying object", o);
                            AppaLog.Exception(ex, o);
                        }
                    }
                }
            }
        }

        public static void DestroySafely(this GameObject o)
        {
            using (_PRF_GameObjectExtensions_DestroySafely.Auto())
            {
                if (o == null)
                {
                    return;
                }

                try
                {
                    if (Application.isPlaying)
                    {
                        Object.Destroy(o);
                    }
                    else
                    {
                        Object.DestroyImmediate(o);
                    }
                }
                catch (NullReferenceException)
                {
                }
                catch (Exception ex)
                {
                    AppaLog.Error("Exception while destroying GameObject.", o);
                    AppaLog.Exception(ex, o);
                }
            }
        }

        public static void DestroySafely(this Transform o)
        {
            using (_PRF_GameObjectExtensions_DestroySafely.Auto())
            {
                try
                {
                    if (Application.isPlaying)
                    {
                        Object.Destroy(o.gameObject);
                    }
                    else
                    {
                        if (o != null)
                        {
                            Object.DestroyImmediate(o.gameObject);
                        }
                    }
                }
                catch (NullReferenceException)
                {
                }
                catch (Exception ex)
                {
                    AppaLog.Error("Exception while destroying transform.", o);
                    AppaLog.Exception(ex, o);
                }
            }
        }

        public static GameObject FindChild(this GameObject go, string name)
        {
            using (_PRF_AddChild.Auto())
            {
                var t = go.transform;

                for (var i = 0; i < t.childCount; i++)
                {
                    var child = t.GetChild(i);
                    if (child.name == name)
                    {
                        return child.gameObject;
                    }
                }

                return null;
            }
        }

        public static void FindOrCreateChild(this GameObject go, ref GameObject target, string name)
        {
            using (_PRF_FindOrCreateChild.Auto())
            {
                if (target != null)
                {
                    return;
                }

                target = FindChild(go, name);

                if (target != null)
                {
                    return;
                }

                target = new GameObject(name).SetParentTo(go);
            }
        }

        public static T GetComponentInImmediateChildren<T>(this GameObject go)
            where T : Component
        {
            using (_PRF_GameObjectExtensions_GetComponentInImmediateChildren.Auto())
            {
                var cs = go.GetComponentsInChildren<T>();
                for (var index = 0; index < cs.Length; index++)
                {
                    var c = cs[index];
                    if (c.transform.parent == go.transform)
                    {
                        return c;
                    }
                }

                return null;
            }
        }

        public static string GetFullName(this GameObject gameObj)
        {
            using (_PRF_GetFullName.Auto())
            {
                return gameObj.transform.FullPath();
            }
        }

        public static void GetOrCreateComponent<T>(this Component obj, ref T component)
            where T : Component
        {
            using (_PRF_GetOrCreateComponent.Auto())
            {
                if (component == null)
                {
                    component = obj.gameObject.GetComponent<T>();

                    if (component == null)
                    {
                        component = obj.gameObject.AddComponent<T>();
                    }
                }
            }
        }

        public static void GetOrCreateComponent<T>(this GameObject obj, ref T component)
            where T : Component
        {
            using (_PRF_GetOrCreateComponent.Auto())
            {
                if (component == null)
                {
                    component = obj.GetComponent<T>();

                    if (component == null)
                    {
                        component = obj.AddComponent<T>();
                    }
                }
            }
        }

        public static void GetOrCreateComponentInChild<T>(this GameObject obj, ref T component, string name)
            where T : Component
        {
            using (_PRF_GetOrCreateComponent.Auto())
            {
                if (component == null)
                {
                    component = obj.GetComponentsInChildren<T>()
                                   .FirstOrDefault(
                                        c => (c.gameObject.name == name) &&
                                             (c.transform.parent == obj.transform)
                                    );

                    if (component == null)
                    {
                        var child = new GameObject(name).SetParentTo(obj.transform);

                        component = child.AddComponent<T>();
                    }
                }
            }
        }

        public static T GetRequiredComponent<T>(this GameObject gameObject)
            where T : Component
        {
            using (_PRF_GetRequiredComponent.Auto())
            {
                var instance = gameObject.GetComponent<T>();
                if (instance)
                {
                    return instance;
                }

                return gameObject.AddComponent<T>();
            }
        }

        public static List<GameObject> GetRootObjectsEvenIfNotLoaded(this Scene scene)
        {
            using (_PRF_GetRootObjectsEvenIfNotLoaded.Auto())
            {
                s_GameObjects.Clear();

                if (scene.IsValid())
                {
                    s_GameObjects.Capacity = Mathf.Max(s_GameObjects.Capacity, scene.rootCount);

                    scene.GetRootGameObjects(s_GameObjects);
                }

                return s_GameObjects;
            }
        }

        public static T GetSceneSingleton<T>(this Scene scene, bool bCreate)
            where T : MonoBehaviour
        {
            using (_PRF_GetSceneSingleton.Auto())
            {
                foreach (var gameObj in scene.GetRootGameObjects())
                {
                    var instance = gameObj.GetComponent<T>();
                    if (instance)
                    {
                        return instance;
                    }
                }

                if (bCreate)
                {
                    var gameObj = CreateGameObjectInScene("! " + typeof(T).Name, scene);
                    return gameObj.AddComponent<T>();
                }

                // None found
                return null;
            }
        }

        public static GameObject InstantiatePrefab(
            this GameObject prefab,
            Transform parent = null,
            Matrix4x4 worldPosition = default)
        {
            using (_PRF_GameObjectExtensions_InstantiatePrefab.Auto())
            {
#if UNITY_EDITOR
                var go = UnityEditor.PrefabUtility.InstantiatePrefab(prefab) as GameObject;
#else
                var go = GameObject.Instantiate(prefab) as GameObject;
#endif

                var transform = go.transform;

                if (parent != null)
                {
                    transform.SetParent(parent, false);
                }

                var column0 = worldPosition.GetColumn(0);
                var column1 = worldPosition.GetColumn(1);
                var column2 = worldPosition.GetColumn(2);
                var column3 = worldPosition.GetColumn(3);

                if (worldPosition != default)
                {
                    transform.position = column3;
                    transform.localScale = new Vector3(
                        column0.magnitude,
                        column1.magnitude,
                        column2.magnitude
                    );
                    transform.rotation = Quaternion.LookRotation(column2, column1);
                }

                return go;
            }
        }

        public static void MoveToLayerRecursive(this GameObject go, int layer)
        {
            using (_PRF_GameObjectExtensions_MoveToLayerRecursive.Auto())
            {
                go.layer = layer;

                var children = go.GetComponentsInChildren<Transform>();
                foreach (var child in children)
                {
                    child.gameObject.layer = layer;
                }
            }
        }

        public static Dictionary<Transform, int> MoveToLayerRecursiveRecoverable(
            this GameObject go,
            int layer)
        {
            using (_PRF_GameObjectExtensions_MoveToLayerRecursiveRecoverable.Auto())
            {
                var originalLayers = new Dictionary<Transform, int>();
                originalLayers.Add(go.transform, go.layer);
                go.layer = layer;

                var children = go.GetComponentsInChildren<Transform>();
                foreach (var child in children)
                {
                    if (!originalLayers.ContainsKey(child))
                    {
                        originalLayers.Add(child, child.gameObject.layer);
                    }

                    child.gameObject.layer = layer;
                }

                return originalLayers;
            }
        }

        public static void RecoverLayersRecursive(
            this GameObject go,
            Dictionary<Transform, int> originalLayers)
        {
            using (_PRF_GameObjectExtensions_RecoverLayersRecursive.Auto())
            {
                go.layer = originalLayers[go.transform];
                var children = go.GetComponentsInChildren<Transform>();
                foreach (var child in children)
                {
                    child.gameObject.layer = originalLayers[child];
                }
            }
        }

        public static GameObject SetAsSiblingTo(this GameObject go, Transform sibling)
        {
            using (_PRF_SetAsSiblingTo.Auto())
            {
                go.transform.SetParent(sibling.parent);
                return go;
            }
        }

        public static Component SetAsSiblingTo(this Component go, Transform sibling)
        {
            using (_PRF_SetAsSiblingTo.Auto())
            {
                go.transform.SetParent(sibling.parent);
                return go;
            }
        }

        public static GameObject SetAsSiblingTo(this GameObject go, GameObject sibling)
        {
            using (_PRF_SetAsSiblingTo.Auto())
            {
                go.transform.SetParent(sibling.transform.parent);
                return go;
            }
        }

        public static Component SetAsSiblingTo(this Component go, GameObject sibling)
        {
            using (_PRF_SetAsSiblingTo.Auto())
            {
                go.transform.SetParent(sibling.transform.parent);
                return go;
            }
        }

        public static GameObject SetParentTo(this GameObject go, Transform parent)
        {
            using (_PRF_SetParentTo.Auto())
            {
                go.transform.SetParent(parent);
                return go;
            }
        }

        public static Component SetParentTo(this Component go, Transform parent)
        {
            using (_PRF_SetParentTo.Auto())
            {
                go.transform.SetParent(parent);
                return go;
            }
        }

        public static GameObject SetParentTo(this GameObject go, GameObject parent)
        {
            using (_PRF_SetParentTo.Auto())
            {
                go.transform.SetParent(parent.transform);
                return go;
            }
        }

        public static Component SetParentTo(this Component go, GameObject parent)
        {
            using (_PRF_SetParentTo.Auto())
            {
                go.transform.SetParent(parent.transform);
                return go;
            }
        }

        #region Profiling

        private const string _PRF_PFX = nameof(GameObjectExtensions) + ".";

        private static readonly ProfilerMarker _PRF_GameObjectExtensions_DestroySafely =
            new("GameObjectExtensions.DestroySafely");

        private static readonly ProfilerMarker _PRF_GameObjectExtensions_GetAsset =
            new("GameObjectExtensions.GetAsset");

        private static readonly ProfilerMarker _PRF_GameObjectExtensions_GetComponentInImmediateChildren =
            new("GameObjectExtensions.GetComponentInImmediateChildren");

        private static readonly ProfilerMarker _PRF_GameObjectExtensions_InstantiatePrefab =
            new("GameObjectExtensions.InstantiatePrefab");

        private static readonly ProfilerMarker _PRF_GameObjectExtensions_MoveToLayerRecursive =
            new("GameObjectExtensions.MoveToLayerRecursive");

        private static readonly ProfilerMarker _PRF_GameObjectExtensions_MoveToLayerRecursiveRecoverable =
            new("GameObjectExtensions.MoveToLayerRecursiveRecoverable");

        private static readonly ProfilerMarker _PRF_GameObjectExtensions_RecoverLayersRecursive =
            new("GameObjectExtensions.RecoverLayersRecursive");

        private static readonly ProfilerMarker _PRF_GetOrCreateComponent =
            new ProfilerMarker(_PRF_PFX + nameof(GetOrCreateComponent));

        private static readonly ProfilerMarker
            _PRF_AddChild = new ProfilerMarker(_PRF_PFX + nameof(AddChild));

        private static readonly ProfilerMarker _PRF_GetFullName =
            new ProfilerMarker(_PRF_PFX + nameof(GetFullName));

        private static readonly ProfilerMarker _PRF_CreateGameObjectInScene =
            new ProfilerMarker(_PRF_PFX + nameof(CreateGameObjectInScene));

        private static readonly ProfilerMarker _PRF_GetRequiredComponent =
            new ProfilerMarker(_PRF_PFX + nameof(GetRequiredComponent));

        private static readonly ProfilerMarker _PRF_GetRootObjectsEvenIfNotLoaded =
            new ProfilerMarker(_PRF_PFX + nameof(GetRootObjectsEvenIfNotLoaded));

        private static readonly ProfilerMarker _PRF_GetSceneSingleton =
            new ProfilerMarker(_PRF_PFX + nameof(GetSceneSingleton));

        private static readonly ProfilerMarker _PRF_SetAsSiblingTo =
            new ProfilerMarker(_PRF_PFX + nameof(SetAsSiblingTo));

        private static readonly ProfilerMarker _PRF_SetParentTo =
            new ProfilerMarker(_PRF_PFX + nameof(SetParentTo));

        private static readonly ProfilerMarker _PRF_FindOrCreateChild =
            new ProfilerMarker(_PRF_PFX + nameof(FindOrCreateChild));

        #endregion
    }
}
