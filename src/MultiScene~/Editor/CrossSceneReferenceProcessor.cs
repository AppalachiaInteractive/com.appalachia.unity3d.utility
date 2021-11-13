using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Appalachia.Utility.MultiScene
{
    internal static class CrossSceneReferenceProcessor
    {
        public delegate List<GenericData> CustomDataProcessorDelegate(EditorCrossSceneReference crossRef);

        #region Fields

        private static Dictionary<System.Type, CustomDataProcessorDelegate>
            _customCrossSceneReferenceDataProcessors =
                new Dictionary<System.Type, CustomDataProcessorDelegate>();

        private static List<KeyValuePair<SerializedProperty, Object>> _referenceMap;

        #endregion

        public static void AddCustomCrossSceneDataProcessor<ObjectType>(
            CustomDataProcessorDelegate customDataProcessor)
            where ObjectType : Object
        {
            _customCrossSceneReferenceDataProcessors[typeof(ObjectType)] = customDataProcessor;
        }

        public static List<EditorCrossSceneReference> GetCrossSceneReferencesForScenes(
            IEnumerable<Scene> scenes)
        {
            var crossSubSceneRefs = ComputeAllCrossSceneReferences();

            crossSubSceneRefs.RemoveAll(x => !scenes.Contains(x.fromScene));

            return crossSubSceneRefs;
        }

        public static void SaveCrossSceneReferences(List<EditorCrossSceneReference> editorCrossSceneRefs)
        {
            for (var i = editorCrossSceneRefs.Count - 1; i >= 0; --i)
            {
                var xRef = editorCrossSceneRefs[i];

                AppaDebug.Log(null, "Saving Cross-Scene Reference: {0}", xRef);

                try
                {
                    var serializedReference = xRef.ToSerializable();

                    try
                    {
                        var initialObject = xRef.fromProperty.objectReferenceValue;

                        AppaCrossSceneReferenceResolver.Resolve(serializedReference);
#if UNITY_5_6_OR_NEWER
                        xRef.fromProperty.serializedObject.UpdateIfRequiredOrScript();
#else
						xRef.fromProperty.serializedObject.UpdateIfDirtyOrScript();
#endif

                        if (initialObject && (xRef.fromProperty.objectReferenceValue != initialObject))
                        {
                            throw new ResolveException(
                                string.Format(
                                    "Resolve should have pointed to {0} ({1}) but instead resolved to {2} ({3})",
                                    initialObject ? initialObject.ToString() : "(null)",
                                    initialObject ? initialObject.GetInstanceID() : 0,
                                    xRef.fromProperty.objectReferenceValue,
                                    xRef.fromProperty.objectReferenceInstanceIDValue
                                )
                            );
                        }
                    }
                    catch (System.Exception ex)
                    {
                        AppaDebug.LogError(
                            xRef.fromObject,
                            "Could not perform a runtime resolve on cross-scene reference {0}.\nReason: {1}. Please review Documentation.",
                            serializedReference,
                            ex.Message
                        );
                        continue;
                    }

                    var crossSceneRefBehaviour =
                        AppaCrossSceneReferences.GetSceneSingleton(xRef.fromScene, true);
                    crossSceneRefBehaviour.AddReference(serializedReference);

                    if (_referenceMap != null)
                    {
                        _referenceMap.Add(
                            new KeyValuePair<SerializedProperty, Object>(
                                xRef.fromProperty,
                                xRef.fromProperty.objectReferenceValue
                            )
                        );
                    }
                }
                catch (UnityException ex)
                {
                    Debug.LogException(ex);
                }
            }
        }

        private static List<EditorCrossSceneReference> ComputeAllCrossSceneReferences()
        {
            UpdateReferencesMap();
            return ComputeCrossSceneReferences(_referenceMap);
        }

        private static List<EditorCrossSceneReference> ComputeCrossSceneReferences(
            List<KeyValuePair<SerializedProperty, Object>> map)
        {
            var refSubSceneMap = new List<EditorCrossSceneReference>();
            var cache = new Dictionary<Object, Scene>();

            foreach (var pair in map)
            {
                var fromObject = pair.Key.serializedObject.targetObject;
                var toObject = pair.Value;

                var fromScene = FindSceneCached(fromObject, cache);
                var toScene = FindSceneCached(toObject,     cache);

                if (!fromScene.IsValid() || !toScene.IsValid())
                {
                    continue;
                }

                var bIsWithinSameScene = fromScene.Equals(toScene);
                if (bIsWithinSameScene)
                {
                    continue;
                }

                var crossRef = CreateEditorCrossSceneReference(
                    fromObject,
                    fromScene,
                    pair.Key,
                    toObject,
                    toScene
                );
                refSubSceneMap.Add(crossRef);
            }

            return refSubSceneMap;
        }

        private static EditorCrossSceneReference CreateEditorCrossSceneReference(
            Object fromObject,
            Scene fromScene,
            SerializedProperty fromProperty,
            Object toObject,
            Scene toScene)
        {
            var crossRef = new EditorCrossSceneReference();
            crossRef.fromScene = fromScene;
            crossRef.toScene = toScene;
            crossRef.fromProperty = fromProperty;
            crossRef.toInstance = toObject;

            CustomDataProcessorDelegate customDataProcessor;
            if (_customCrossSceneReferenceDataProcessors.TryGetValue(
                fromObject.GetType(),
                out customDataProcessor
            ))
            {
                var customData = customDataProcessor(crossRef);
                if ((customData != null) && (customData.Count > 0))
                {
                    crossRef.data = customData;
                }
            }

            return crossRef;
        }

        private static Scene FindSceneCached(Object instance, Dictionary<Object, Scene> cache)
        {
            if (!instance)
            {
                return new Scene();
            }

            var scene = new Scene();
            if (cache.TryGetValue(instance, out scene))
            {
                return scene;
            }

            if (!EditorUtility.IsPersistent(instance))
            {
                var gameObj = GameObjectEx.EditorGetGameObjectFromComponent(instance);
                if (gameObj)
                {
                    scene = gameObj.scene;
                }
            }

            cache.Add(instance, scene);
            return scene;
        }

        private static void PopulateReferenceMap(
            List<KeyValuePair<SerializedProperty, Object>> map,
            IEnumerable<Object> allObjects)
        {
            var unityEditorAssembly = typeof(EditorApplication).Assembly;
            var userEditorAssembly = typeof(CrossSceneReferenceProcessor).Assembly;

            foreach (var obj in allObjects)
            {
                if (obj.hideFlags == HideFlags.HideAndDontSave)
                {
                    continue;
                }

                var assembly = obj.GetType().Assembly;
                if ((assembly == unityEditorAssembly) || (assembly == userEditorAssembly))
                {
                    continue;
                }

                var so = new SerializedObject(obj);
                var sp = so.GetIterator();

                var bCanDispose = true;
                while (sp.Next(true))
                {
                    if (sp.propertyType != SerializedPropertyType.ObjectReference)
                    {
                        continue;
                    }

                    if (sp.objectReferenceInstanceIDValue == 0)
                    {
                        continue;
                    }

                    map.Add(new KeyValuePair<SerializedProperty, Object>(sp.Copy(), sp.objectReferenceValue));
                    bCanDispose = false;
                }

                if (bCanDispose)
                {
                    sp.Dispose();
                    so.Dispose();
                }
            }
        }

        private static void UpdateReferencesMap()
        {
            if (_referenceMap != null)
            {
                return;
            }

            var startTime = EditorApplication.timeSinceStartup;

            var allMonoBehaviourObjs = Resources.FindObjectsOfTypeAll<MonoBehaviour>()
                                                .Where(x => !EditorUtility.IsPersistent(x))
                                                .Cast<Object>();
            var allScriptableObjs = Resources.FindObjectsOfTypeAll<ScriptableObject>()
                                             .Where(
                                                  x => !EditorUtility.IsPersistent(x) &&
                                                       !typeof(EditorWindow).IsAssignableFrom(x.GetType())
                                              )
                                             .Cast<Object>();

            EditorApplication.delayCall += () => { _referenceMap = null; };
            _referenceMap = new List<KeyValuePair<SerializedProperty, Object>>(
                allMonoBehaviourObjs.Count() + allScriptableObjs.Count()
            );

            PopulateReferenceMap(_referenceMap, allMonoBehaviourObjs);
            PopulateReferenceMap(_referenceMap, allScriptableObjs);

            foreach (var customType in _customCrossSceneReferenceDataProcessors.Keys)
            {
                if (typeof(MonoBehaviour).IsAssignableFrom(customType))
                {
                    continue;
                }

                if (typeof(ScriptableObject).IsAssignableFrom(customType))
                {
                    continue;
                }

                var sceneCustomObjects = Resources.FindObjectsOfTypeAll(customType)
                                                  .Where(x => !EditorUtility.IsPersistent(x));
                PopulateReferenceMap(_referenceMap, sceneCustomObjects);
            }

            AppaDebug.LogPerf(
                null,
                "Cross-Scene Reference Map Update: {0}",
                EditorApplication.timeSinceStartup - startTime
            );
        }
    }
}
