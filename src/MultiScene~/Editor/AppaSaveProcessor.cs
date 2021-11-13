using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Appalachia.Utility.MultiScene
{
    public class AppaSaveProcessor : AssetModificationProcessor
    {
#if UNITY_5_6_OR_NEWER
        [InitializeOnLoadMethod]
        private static void InitAppaSaveProcessor()
        {
            EditorSceneManager.sceneSaving += EditorSceneManager_sceneSaving;
        }

        private static void EditorSceneManager_sceneSaving(Scene scene, string path)
        {
            var scenes = new List<Scene> {scene};
            HandleSavingScenes(scenes);
            HandleCrossSceneReferences(scenes);
        }
#else
		static string[] OnWillSaveAssets( string[] filenames )
		{
			
			
			if ( EditorApplication.delayCall != null )
			{
				var delayCall = EditorApplication.delayCall;
				EditorApplication.delayCall = null;

				delayCall();
			}

			
			List<Scene> savingScenes = new List<Scene>();
			foreach( var filename in filenames )
			{
				var scene = EditorSceneManager.GetSceneByPath(filename);
				if ( scene.IsValid() )
				{
					savingScenes.Add(scene);
				}
			}

			
			bool bIsSaveNewScene = (filenames.Length < 1);
			if ( bIsSaveNewScene )
			{
				savingScenes.Add( EditorSceneManager.GetActiveScene() );
			}

			HandleSavingScenes( savingScenes );
			HandleCrossSceneReferences( savingScenes );

			return filenames;
		}
#endif

        private static void HandleSavingScenes(IList<Scene> scenes)
        {
            foreach (var scene in scenes)
            {
                if (!scene.isLoaded)
                {
                    continue;
                }

                var sceneSetup = GameObjectEx.GetSceneSingleton<AppaMultiSceneSetup>(scene, true);
                sceneSetup.OnBeforeSerialize();
            }
        }

        public static void HandleCrossSceneReferences(IList<Scene> scenes)
        {
            var crossSceneReferenceBehaviour = AppaPreferences.CrossSceneReferencing;
            var bSkipCrossSceneReferences = crossSceneReferenceBehaviour ==
                                            AppaPreferences.CrossSceneReferenceHandling.UnityDefault;
            var bSaveCrossSceneReferences =
                crossSceneReferenceBehaviour == AppaPreferences.CrossSceneReferenceHandling.Save;

            if (bSkipCrossSceneReferences || (scenes.Count < 1))
            {
                return;
            }

            foreach (var scene in scenes)
            {
                if (!scene.isLoaded)
                {
                    continue;
                }

                var crossSceneRefBehaviour = AppaCrossSceneReferences.GetSceneSingleton(scene, true);
                for (var i = 0; i < SceneManager.sceneCount; ++i)
                {
                    var otherScene = SceneManager.GetSceneAt(i);
                    if (otherScene.isLoaded)
                    {
                        crossSceneRefBehaviour.ResetCrossSceneReferences(otherScene);
                    }
                }
            }

            var xSceneRefs = CrossSceneReferenceProcessor.GetCrossSceneReferencesForScenes(scenes);
            if (bSaveCrossSceneReferences && (xSceneRefs.Count > 0))
            {
                var sceneNames = scenes.Select(x => x.name);
                AppaDebug.LogWarning(
                    null,
                    "Appa Plugin: Saving {0} Cross-Scene References in Scenes: {1}",
                    xSceneRefs.Count,
                    string.Join(",", sceneNames.ToArray())
                );
                CrossSceneReferenceProcessor.SaveCrossSceneReferences(xSceneRefs);
            }

            for (var i = 0; i < xSceneRefs.Count; ++i)
            {
                var xRef = xSceneRefs[i];

                if (!bSaveCrossSceneReferences)
                {
                    Debug.LogWarningFormat("Cross-Scene Reference {0} will become null", xRef);
                }

                var refIdToRestore = xRef.fromProperty.objectReferenceInstanceIDValue;
                xRef.fromProperty.objectReferenceInstanceIDValue = 0;
                xRef.fromProperty.serializedObject.ApplyModifiedPropertiesWithoutUndo();

                EditorApplication.delayCall += () =>
                {
                    if (!EditorApplication.isPlayingOrWillChangePlaymode)
                    {
                        AppaDebug.Log(null, "Restoring Cross-Scene Ref (Post-Save): {0}", xRef);

                        var fromProperty = xRef.fromProperty;
                        fromProperty.objectReferenceInstanceIDValue = refIdToRestore;

                        if (fromProperty.serializedObject.targetObject)
                        {
                            fromProperty.serializedObject.ApplyModifiedPropertiesWithoutUndo();
                            fromProperty.serializedObject.Update();
                        }
                    }
                };
            }
        }
    }
}
