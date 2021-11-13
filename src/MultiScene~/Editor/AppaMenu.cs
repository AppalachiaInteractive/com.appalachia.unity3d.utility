using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Appalachia.Utility.MultiScene
{
    internal static class AppaMenu
    {
#if TODO_DEBUG
		[MenuItem("Tools/Advanced Multi-Scene/Debug")]
		private static void ShowDebugMenu()
		{
			EditorUtility.DisplayPopupMenu( new Rect(0.0f, 0.0f, 100.0f, 100.0f), "CONTEXT/AppaDebug", new MenuCommand(null) );
		}

		[MenuItem("Tools/Advanced Multi-Scene/Debug", true)]
		private static bool AllowDebugMenuItem()
        {
			return AppaPreferences.DebugEnabled;
        }
#endif

        [MenuItem("Tools/Advanced Multi-Scene/Support")]
        private static void GotoForumThread()
        {
            System.Diagnostics.Process.Start("http:
        }

        [MenuItem("Tools/Advanced Multi-Scene/Detect Cross-Scene Refs")]
        public static void DebugShowAllCrossSceneReferences()
        {
            var allScenes = new List<Scene>();
            for (var i = 0; i < SceneManager.sceneCount; ++i)
            {
                allScenes.Add(SceneManager.GetSceneAt(i));
            }

            var allCrossSceneRefs =
                CrossSceneReferenceProcessor.GetCrossSceneReferencesForScenes(allScenes);
            foreach (var xRef in allCrossSceneRefs)
            {
                Debug.LogFormat("Cross Scene Reference: {0}", xRef);
            }
        }
    }
}
