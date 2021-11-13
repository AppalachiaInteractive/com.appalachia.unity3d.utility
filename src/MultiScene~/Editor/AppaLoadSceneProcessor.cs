using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace Appalachia.Utility.MultiScene
{
    public static class AppaLoadSceneProcessor
    {
        [InitializeOnLoadMethod]
        private static void AssemblyReloaded()
        {
            EditorSceneManager.sceneOpened -= EditorSceneManager_sceneOpened;
            EditorSceneManager.sceneOpened += EditorSceneManager_sceneOpened;
        }

        private static void EditorSceneManager_sceneOpened(Scene scene, OpenSceneMode mode)
        {
            if (!scene.isLoaded)
            {
                return;
            }

            var sceneSetup = GameObjectEx.GetSceneSingleton<AppaMultiSceneSetup>(scene, false);
            if (!sceneSetup)
            {
                sceneSetup = GameObjectEx.GetSceneSingleton<AppaMultiSceneSetup>(scene, true);
                sceneSetup.transform.SetSiblingIndex(0);
            }
        }
    }
}
