#region

using System;
using System.Collections.Generic;
using System.Linq;
using Appalachia.Utility.Constants;
using Appalachia.Utility.Logging;
using Appalachia.Utility.Strings;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

#endregion

namespace Appalachia.Utility.Extensions
{
    public static partial class GameObjectExtensions
    {
        private enum DestructionMode
        {
            DestroyIfOnThisGameObject,
            DestroyIfOnOtherGameObject,
            DestroyIfNotOnChildGameObject,
        }

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

        public static GameObject AddGameObjectInScene(string name, Scene scene)
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

        public static void DestroySafely(this Object o, bool allowDestroyingAssets = false)
        {
            using (_PRF_DestroySafely.Auto())
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
                    AppaLog.Context.Extensions.Error(
                        ZString.Format("Not able to dispose of [{0}] before destroying.", o.name),
                        o,
                        ex
                    );
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
                                Object.DestroyImmediate(o, allowDestroyingAssets);
                            }
                        }
                        catch (Exception ex)
                        {
                            AppaLog.Context.Extensions.Error("Exception while destroying object.", o, ex);
                        }
                    }
                }
            }
        }

        public static void DestroySafely<T>(this T o)
            where T : Object, IDisposable
        {
            using (_PRF_DestroySafely.Auto())
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
                    AppaLog.Context.Extensions.Error(
                        ZString.Format("Not able to dispose of [{0} before destroying.", typeof(T).Name),
                        o,
                        ex
                    );
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
                            AppaLog.Context.Extensions.Error("Exception while destroying object", o, ex);
                        }
                    }
                }
            }
        }

        public static void DestroySafely(this Scene scene)
        {
            using (_PRF_DestroySafely.Auto())
            {
                DestroySafely(scene.GetRootGameObjects());
            }
        }

        public static void DestroySafely(this IEnumerable<GameObject> gos)
        {
            using (_PRF_DestroySafely.Auto())
            {
                foreach (var go in gos)
                {
                    go.DestroySafely();
                }
            }
        }

        public static void DestroySafely(this GameObject o)
        {
            using (_PRF_DestroySafely.Auto())
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
                    AppaLog.Context.Extensions.Error("Exception while destroying GameObject.", o, ex);
                }
            }
        }

        public static void DestroySafely(this Transform o)
        {
            using (_PRF_DestroySafely.Auto())
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
                    AppaLog.Context.Extensions.Error("Exception while destroying transform.", o, ex);
                }
            }
        }

        public static void Get<T>(
            this GameObject obj,
            ref T component,
            GetComponentStrategy getStyle = GetComponentStrategy.CurrentObject)
            where T : Component
        {
            using (_PRF_Get.Auto())
            {
                var includeInactive = getStyle.HasFlag(GetComponentStrategy.IncludeInactive);

                if (component != null)
                {
                    return;
                }

                if (getStyle.HasFlag(GetComponentStrategy.CurrentObject))
                {
                    component = obj.GetComponent<T>();
                }

                if (component != null)
                {
                    return;
                }

                if (getStyle.HasFlag(GetComponentStrategy.ParentObject))
                {
                    component = obj.GetComponentInParent<T>(includeInactive);
                }

                if (component != null)
                {
                    return;
                }

                if (getStyle.HasFlag(GetComponentStrategy.Children))
                {
                    component = obj.GetComponentInChildren<T>(includeInactive);
                }

                if (component != null)
                {
                    return;
                }

                if (getStyle.HasFlag(GetComponentStrategy.AnyParent))
                {
                    component = obj.transform.root.GetComponentInChildren<T>(includeInactive);
                }
            }
        }

        public static T Get<T>(this GameObject obj, GetComponentStrategy getStyle = GetComponentStrategy.CurrentObject)
            where T : Component
        {
            using (_PRF_Get.Auto())
            {
                T result = null;
                Get(obj, ref result, getStyle);

                return result;
            }
        }

        public static GameObject GetChild(this MonoBehaviour go, string name)
        {
            using (_PRF_GetChild.Auto())
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

        public static Transform GetChild(this Transform go, string name)
        {
            using (_PRF_GetChild.Auto())
            {
                var t = go.transform;

                for (var i = 0; i < t.childCount; i++)
                {
                    var child = t.GetChild(i);
                    if (child.name == name)
                    {
                        return child;
                    }
                }

                return null;
            }
        }

        public static GameObject GetChild(this GameObject go, string name)
        {
            using (_PRF_GetChild.Auto())
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

        public static Transform[] GetChildren(this Transform go)
        {
            using (_PRF_GetChildren.Auto())
            {
                var t = go.transform;

                var results = new Transform[t.childCount];

                for (var i = 0; i < t.childCount; i++)
                {
                    var child = t.GetChild(i);
                    results[i] = child;
                }

                return results;
            }
        }

        public static GameObject[] GetChildren(this GameObject go)
        {
            using (_PRF_GetChildren.Auto())
            {
                var t = go.transform;

                var results = new GameObject[t.childCount];

                for (var i = 0; i < t.childCount; i++)
                {
                    var child = t.GetChild(i);
                    results[i] = child.gameObject;
                }

                return results;
            }
        }

        public static T GetComponentInImmediateChildren<T>(this GameObject go)
            where T : Component
        {
            using (_PRF_GetComponentInImmediateChildren.Auto())
            {
                var cs = go.GetComponentsInChildren<T>(true);
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

        public static void GetComponentInParent<T>(this GameObject obj, ref T component)
            where T : Component
        {
            using (_PRF_GetComponentInParent.Auto())
            {
                if (component == null)
                {
                    component = obj.GetComponentInParent<T>(true);
                }
            }
        }

        public static IEnumerable<T> GetComponentsInImmediateChildren<T>(this GameObject go)
            where T : Component
        {
            using (_PRF_GetComponentInImmediateChildren.Auto())
            {
                var cs = go.GetComponentsInChildren<T>(true);

                for (var index = 0; index < cs.Length; index++)
                {
                    var c = cs[index];
                    if (c.transform.parent == go.transform)
                    {
                        yield return c;
                    }
                }
            }
        }

        public static string GetFullName(this GameObject gameObj)
        {
            using (_PRF_GetFullName.Auto())
            {
                return gameObj.transform.GetFullPath();
            }
        }

        public static string GetFullNameForFile(this GameObject gameObj)
        {
            using (_PRF_GetFullNameForFile.Auto())
            {
                var path = GetFullName(gameObj);

                var clean = path.Replace('/', '_').Replace('\\', '_');
                return clean;
            }
        }

        /// <summary>
        ///     Searches for a child game object by name.
        ///     If it exists, it will be assigned to the <paramref name="target" /> argument.
        ///     If it does not exist, it will be created and then assigned to the <paramref name="target" /> argument.
        /// </summary>
        /// <param name="behaviour">The current behaviour.</param>
        /// <param name="target">The field we will assign the result to.</param>
        /// <param name="name">The name of the game object to search for or create.</param>
        /// <param name="ui">
        ///     Whether or not this game object is a UI object (using <see cref="RectTransform" />) or a 3D object (using <see cref="Transform" />).
        /// </param>
        public static void GetOrAddChild(this MonoBehaviour behaviour, ref GameObject target, string name, bool ui)
        {
            using (_PRF_GetOrCreateChild.Auto())
            {
                GetOrAddChild(behaviour.gameObject, ref target, name, ui);
            }
        }

        /// <summary>
        ///     Searches for a child game object by name.
        ///     If it exists, it will be assigned to the <paramref name="target" /> argument.
        ///     If it does not exist, it will be created and then assigned to the <paramref name="target" /> argument.
        /// </summary>
        /// <param name="go">The current game object.</param>
        /// <param name="target">The field we will assign the result to.</param>
        /// <param name="name">The name of the game object to search for or create.</param>
        /// <param name="ui">
        ///     Whether or not this game object is a UI object (using <see cref="RectTransform" />) or a 3D object (using <see cref="Transform" />).
        /// </param>
        public static void GetOrAddChild(this GameObject go, ref GameObject target, string name, bool ui)
        {
            using (_PRF_GetOrCreateChild.Auto())
            {
                if (target != null)
                {
                    if (target.transform.parent == go.transform)
                    {
                        if (ui && (target.GetComponent<RectTransform>() == null))
                        {
                            target.AddComponent<RectTransform>();
                        }

                        return;
                    }
                }

                target = GetChild(go, name);

                if (target != null)
                {
                    if (ui && (target.GetComponent<RectTransform>() == null))
                    {
                        target.AddComponent<RectTransform>();
                    }

                    return;
                }

                if (ui)
                {
                    target = new GameObject(name, typeof(RectTransform)).SetParentTo(go);
                }
                else
                {
                    target = new GameObject(name).SetParentTo(go);
                }
            }
        }

        /// <summary>
        ///     Searches for a <see cref="Component" /> or <see cref="MonoBehaviour" /> on the current <see cref="GameObject" />.
        ///     If it exists, it will be assigned to the <paramref name="component" /> argument.
        ///     If it does not exist, it will be created and then assigned to the <paramref name="component" /> argument.
        /// </summary>
        /// <param name="obj">The current game object.</param>
        /// <param name="component">The field we will assign the result to.</param>
        /// <typeparam name="T">The <see cref="Component" /> or <see cref="MonoBehaviour" /> type to search for.</typeparam>
        public static void GetOrAddComponent<T>(this MonoBehaviour obj, ref T component)
            where T : Component
        {
            using (_PRF_GetOrCreateComponent.Auto())
            {
                if (component == null)
                {
                    component = obj.GetComponent<T>();

                    if (component == null)
                    {
                        component = obj.gameObject.AddComponent<T>();
                    }
                }
            }
        }

        /// <summary>
        ///     Searches for a <see cref="Component" /> or <see cref="MonoBehaviour" /> on the current <see cref="GameObject" />.
        ///     If it exists, it will be assigned to the <paramref name="component" /> argument.
        ///     If it does not exist, it will be created and then assigned to the <paramref name="component" /> argument.
        /// </summary>
        /// <param name="obj">The current game object.</param>
        /// <param name="component">The field we will assign the result to.</param>
        /// <typeparam name="T">The <see cref="Component" /> or <see cref="MonoBehaviour" /> type to search for.</typeparam>
        public static void GetOrAddComponent<T>(this GameObject obj, ref T component)
            where T : Component
        {
            using (_PRF_GetOrCreateComponent.Auto())
            {
                DestroyComponentOnWrongGameObject(obj, ref component, DestructionMode.DestroyIfOnOtherGameObject);

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

        public static void GetOrAddComponentInChild<T>(
            this GameObject obj,
            ref T component,
            string name,
            bool requireNameMatch = true)
            where T : Component
        {
            using (_PRF_GetOrCreateComponent.Auto())
            {
                DestroyComponentOnWrongGameObject(obj, ref component, DestructionMode.DestroyIfNotOnChildGameObject);

                if (component == null)
                {
                    var components = obj.GetComponentsInChildren<T>(true);

                    var childComponents = components.Where(c => c.transform.parent == obj.transform);
                    var match = childComponents.FirstOrDefault(c => !requireNameMatch || (c.gameObject.name == name));

                    component = match;
                    if (component == null)
                    {
                        var child = new GameObject(name).SetParentTo(obj.transform);

                        component = child.AddComponent<T>();
                    }
                }
            }
        }

        public static void GetOrAddLifetimeComponentInChild<T>(this GameObject obj, ref T component, string name = null)
            where T : Component
        {
            using (_PRF_GetOrCreateLifetimeComponentInChild.Auto())
            {
                if (component != null)
                {
                    return;
                }

                component = Object.FindObjectOfType<T>(true);

                if (component)
                {
                    if (component.transform.parent != obj.transform)
                    {
                        component.transform.SetParent(obj.transform);
                    }

                    if (name != null)
                    {
                        component.name = name;
                    }
                    else if (component.name != "New Game Object")
                    {
                    }
                    else
                    {
                        component.name = typeof(T).Name;
                    }

                    return;
                }

                obj.GetOrAddComponentInChild(ref component, name, false);
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
                    var gameObj = AddGameObjectInScene("! " + typeof(T).Name, scene);
                    return gameObj.AddComponent<T>();
                }

                // None found
                return null;
            }
        }

        public static GameObject[] GetSiblings(this GameObject go)
        {
            using (_PRF_GetSiblings.Auto())
            {
                return GetChildren(go.transform.parent.gameObject);
            }
        }

        public static Transform[] GetSiblings(this Transform go)
        {
            using (_PRF_GetSiblings.Auto())
            {
                return GetChildren(go.transform.parent);
            }
        }

        public static GameObject InstantiatePrefab(
            this GameObject prefab,
            Transform parent = null,
            Matrix4x4 worldTransform = default)
        {
            using (_PRF_InstantiatePrefab.Auto())
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

                var column0 = worldTransform.GetColumn(0);
                var column1 = worldTransform.GetColumn(1);
                var column2 = worldTransform.GetColumn(2);
                var column3 = worldTransform.GetColumn(3);

                if (worldTransform != default)
                {
                    transform.position = column3;
                    transform.localScale = new Vector3(column0.magnitude, column1.magnitude, column2.magnitude);
                    transform.rotation = Quaternion.LookRotation(column2, column1);
                }

                return go;
            }
        }

        public static void MoveToActiveScene(this GameObject go)
        {
            using (_PRF_MoveToActiveScene.Auto())
            {
                AppaLog.Context.Extensions.Debug(ZString.Format("Moving [{0}] to currently active scene.", go.name));

                var scene = SceneManager.GetActiveScene();
                SceneManager.MoveGameObjectToScene(go, scene);
            }
        }

        public static void MoveToLayerRecursive(this GameObject go, int layer)
        {
            using (_PRF_MoveToLayerRecursive.Auto())
            {
                go.layer = layer;

                var children = go.GetComponentsInChildren<Transform>();
                foreach (var child in children)
                {
                    child.gameObject.layer = layer;
                }
            }
        }

        public static Dictionary<Transform, int> MoveToLayerRecursiveRecoverable(this GameObject go, int layer)
        {
            using (_PRF_MoveToLayerRecursiveRecoverable.Auto())
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

        public static void MoveToLoadedScene(this GameObject go)
        {
            using (_PRF_MoveToLoadedScene.Auto())
            {
                if (!go.scene.isLoaded)
                {
                    AppaLog.Context.Extensions.Debug(
                        ZString.Format("Moving [{0}] to a loaded scene.", go.name.FormatNameForLogging())
                    );

                    for (var i = 0; i < SceneManager.sceneCount; i++)
                    {
                        var scene = SceneManager.GetSceneAt(i);

                        if (scene.isLoaded)
                        {
                            SceneManager.MoveGameObjectToScene(go, scene);
                        }
                    }
                }
            }
        }

        public static void RecoverLayersRecursive(this GameObject go, Dictionary<Transform, int> originalLayers)
        {
            using (_PRF_RecoverLayersRecursive.Auto())
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

        public static GameObject SetAsSiblingTo(this GameObject go, GameObject sibling)
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
                if (go.transform.parent == parent)
                {
                    return go;
                }

                go.transform.SetParent(parent);
                return go;
            }
        }

        public static GameObject SetParentTo(this GameObject go, GameObject parent)
        {
            using (_PRF_SetParentTo.Auto())
            {
                if (go.transform.parent == parent.transform)
                {
                    return go;
                }

                go.transform.SetParent(parent.transform);
                return go;
            }
        }

        private static void DestroyComponentOnWrongGameObject<T>(
            GameObject obj,
            ref T component,
            DestructionMode destructionMode)
            where T : Component
        {
            using (_PRF_DestroyComponentOnWrongGameObject.Auto())
            {
                if (component != null)
                {
                    if (destructionMode == DestructionMode.DestroyIfNotOnChildGameObject)
                    {
                        if (component.gameObject.transform.parent == null)
                        {
                            DestroyObjectForRecreate(ref component, "a child object");
                        }
                        else if (component.gameObject.transform.parent.gameObject != obj)
                        {
                            DestroyObjectForRecreate(ref component, "a child object");
                        }
                    }

                    if (destructionMode == DestructionMode.DestroyIfOnThisGameObject)
                    {
                        if (component.gameObject == obj)
                        {
                            DestroyObjectForRecreate(ref component, "another object");
                        }
                    }

                    if (destructionMode == DestructionMode.DestroyIfOnOtherGameObject)
                    {
                        if (component.gameObject != obj)
                        {
                            DestroyObjectForRecreate(ref component, obj.name);
                        }
                    }
                }
            }
        }

        private static void DestroyObjectForRecreate<T>(ref T component, string recreatingOn)
            where T : Component
        {
            AppaLog.Context.Extensions.Warn(
                ZString.Format(
                    "Destroying component {0} on object {1} before recreating on {2}.",
                    typeof(T).Name,
                    component.gameObject.name,
                    recreatingOn
                )
            );

            component.DestroySafely();
            component = null;
        }

        #region Profiling

        private const string _PRF_PFX = nameof(GameObjectExtensions) + ".";

        private static readonly ProfilerMarker _PRF_GetFullNameForFile =
            new ProfilerMarker(_PRF_PFX + nameof(GetFullNameForFile));

        private static readonly ProfilerMarker _PRF_GetSiblings = new ProfilerMarker(_PRF_PFX + nameof(GetSiblings));
        private static readonly ProfilerMarker _PRF_GetChildren = new ProfilerMarker(_PRF_PFX + nameof(GetChildren));
        private static readonly ProfilerMarker _PRF_GetChild = new ProfilerMarker(_PRF_PFX + nameof(GetChild));
        private static readonly ProfilerMarker _PRF_Get = new ProfilerMarker(_PRF_PFX + nameof(Get));

        private static readonly ProfilerMarker _PRF_GetComponentInParent =
            new ProfilerMarker(_PRF_PFX + nameof(GetComponentInParent));

        private static readonly ProfilerMarker _PRF_GetOrCreateLifetimeComponentInChild =
            new ProfilerMarker(_PRF_PFX + nameof(GetOrAddLifetimeComponentInChild));

        private static readonly ProfilerMarker _PRF_MoveToActiveScene =
            new ProfilerMarker(_PRF_PFX + nameof(MoveToActiveScene));

        private static readonly ProfilerMarker _PRF_MoveToLoadedScene =
            new ProfilerMarker(_PRF_PFX + nameof(MoveToLoadedScene));

        private static readonly ProfilerMarker _PRF_DestroySafely =
            new ProfilerMarker(_PRF_PFX + nameof(DestroySafely));

        private static readonly ProfilerMarker _PRF_GetComponentInImmediateChildren =
            new ProfilerMarker(_PRF_PFX + nameof(GetComponentInImmediateChildren));

        private static readonly ProfilerMarker _PRF_InstantiatePrefab =
            new ProfilerMarker(_PRF_PFX + nameof(InstantiatePrefab));

        private static readonly ProfilerMarker _PRF_MoveToLayerRecursive =
            new ProfilerMarker(_PRF_PFX + nameof(MoveToLayerRecursive));

        private static readonly ProfilerMarker _PRF_MoveToLayerRecursiveRecoverable =
            new ProfilerMarker(_PRF_PFX + nameof(MoveToLayerRecursiveRecoverable));

        private static readonly ProfilerMarker _PRF_RecoverLayersRecursive =
            new ProfilerMarker(_PRF_PFX + nameof(RecoverLayersRecursive));

        private static readonly ProfilerMarker _PRF_DestroyComponentOnWrongGameObject =
            new ProfilerMarker(_PRF_PFX + nameof(DestroyComponentOnWrongGameObject));

        private static readonly ProfilerMarker _PRF_GetOrCreateComponent =
            new ProfilerMarker(_PRF_PFX + nameof(GetOrAddComponent));

        private static readonly ProfilerMarker _PRF_AddChild = new ProfilerMarker(_PRF_PFX + nameof(AddChild));

        private static readonly ProfilerMarker _PRF_GetFullName = new ProfilerMarker(_PRF_PFX + nameof(GetFullName));

        private static readonly ProfilerMarker _PRF_CreateGameObjectInScene =
            new ProfilerMarker(_PRF_PFX + nameof(AddGameObjectInScene));

        private static readonly ProfilerMarker _PRF_GetRequiredComponent =
            new ProfilerMarker(_PRF_PFX + nameof(GetRequiredComponent));

        private static readonly ProfilerMarker _PRF_GetRootObjectsEvenIfNotLoaded =
            new ProfilerMarker(_PRF_PFX + nameof(GetRootObjectsEvenIfNotLoaded));

        private static readonly ProfilerMarker _PRF_GetSceneSingleton =
            new ProfilerMarker(_PRF_PFX + nameof(GetSceneSingleton));

        private static readonly ProfilerMarker _PRF_SetAsSiblingTo =
            new ProfilerMarker(_PRF_PFX + nameof(SetAsSiblingTo));

        private static readonly ProfilerMarker _PRF_SetParentTo = new ProfilerMarker(_PRF_PFX + nameof(SetParentTo));

        private static readonly ProfilerMarker _PRF_GetOrCreateChild =
            new ProfilerMarker(_PRF_PFX + nameof(GetOrAddChild));

        #endregion
    }
}
