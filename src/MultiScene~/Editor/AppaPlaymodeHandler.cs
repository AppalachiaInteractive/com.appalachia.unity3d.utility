using System.Collections.Generic;
using UnityEditor;
using UnityEngine.SceneManagement;

namespace Appalachia.Utility.MultiScene
{
    public static class AppaPlaymodeHandler
    {
#if UNITY_2017_2_OR_NEWER
        private static void EditorApplication_playModeStateChanged(PlayModeStateChange playmodeState)
        {
            var isExitingEditMode = playmodeState == PlayModeStateChange.ExitingEditMode;
#else
		private static void EditorApplication_playModeStateChanged()
		{
			bool isExitingEditMode =
 !EditorApplication.isPlaying && EditorApplication.isPlayingOrWillChangePlaymode;
#endif

#if UNITY_5_6_OR_NEWER
            if (EditorUtility.scriptCompilationFailed)
            {
                AppaDebug.Log(null, "Skipping cross-scene references due to compilation errors");
                return;
            }
#endif

            if (isExitingEditMode)
            {
                var allScenes = new List<Scene>(SceneManager.sceneCount);
                for (var i = 0; i < SceneManager.sceneCount; ++i)
                {
                    var scene = SceneManager.GetSceneAt(i);
                    if (scene.IsValid() && scene.isLoaded)
                    {
                        allScenes.Add(scene);
                    }
                }

                AppaDebug.Log(null, "Handling Cross-Scene Referencing for Playmode");
                AppaSaveProcessor.HandleCrossSceneReferences(allScenes);
            }
        }

        [InitializeOnLoadMethod]
        private static void SaveCrossSceneReferencesBeforePlayInEditMode()
        {
#if UNITY_2017_2_OR_NEWER
            EditorApplication.playModeStateChanged += EditorApplication_playModeStateChanged;
#else
			EditorApplication.playmodeStateChanged += EditorApplication_playModeStateChanged;
#endif
        }
    }
}
