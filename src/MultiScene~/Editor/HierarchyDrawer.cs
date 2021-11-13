using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Appalachia.Utility.MultiScene
{
    internal static class AppaHierarchyDrawer
    {
        #region Fields

        private static GUIStyle _justifyRightLabel;
        private static GUIStyle _justifyRightPopup;

        #endregion

        /**
         * Helper function to color text using RTF format
         */
        private static string ColorText(string text, Color32 color)
        {
            return string.Format(
                "<color=#{1:X2}{2:X2}{3:X2}{4:X2}>{0}</color>",
                text,
                color.r,
                color.g,
                color.b,
                color.a
            );
        }

        private static Scene GetSceneFromHandleID(int handleID)
        {
            var numScenes = SceneManager.sceneCount;
            for (var i = 0; i < numScenes; ++i)
            {
                var scene = SceneManager.GetSceneAt(i);
                if (scene.GetHashCode() == handleID)
                {
                    return scene;
                }
            }

            return new Scene();
        }

        [InitializeOnLoadMethod]
        private static void HookUpDrawer()
        {
            EditorApplication.hierarchyWindowItemOnGUI -= OnHierarchyWindowItemOnGUI;
            EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyWindowItemOnGUI;
        }

        private static void OnHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
        {
            if (Application.isPlaying)
            {
                return;
            }

            if (_justifyRightLabel == null)
            {
                _justifyRightLabel = new GUIStyle(GUI.skin.label);
                _justifyRightLabel.alignment = TextAnchor.UpperRight;
                _justifyRightLabel.richText = true;
            }

            if (_justifyRightPopup == null)
            {
                _justifyRightPopup = new GUIStyle(GUI.skin.FindStyle("Popup"));
                _justifyRightPopup.stretchWidth = false;
                _justifyRightPopup.alignment = TextAnchor.UpperRight;
                _justifyRightPopup.richText = true;
            }

            var obj = EditorUtility.InstanceIDToObject(instanceID);
            var bIsSceneHeader = (instanceID != 0) && !obj;
            if (!bIsSceneHeader)
            {
                return;
            }

            var scene = GetSceneFromHandleID(instanceID);
            if (!scene.IsValid())
            {
                return;
            }

            selectionRect.xMax -= 32.0f;

            var activeScene = SceneManager.GetActiveScene();
            var sceneSetup = GameObjectEx.GetSceneSingleton<AppaMultiSceneSetup>(
                SceneManager.GetActiveScene(),
                false
            );
            if (!sceneSetup)
            {
                GUI.Label(
                    selectionRect,
                    ColorText("AMS Not Found in " + activeScene.name, Color.red),
                    _justifyRightLabel
                );
                return;
            }

            if (activeScene == scene)
            {
                GUI.Label(selectionRect, ColorText("<b>Active</b>", Color.green), _justifyRightLabel);
                return;
            }

            var entries = sceneSetup.GetSceneSetup();
            var entry = entries.FirstOrDefault(x => x.scene.editorPath == scene.path);
            if (entry == null)
            {
                GUI.Label(selectionRect, ColorText("(Not Managed)", Color.red), _justifyRightLabel);
                return;
            }

            if ((entry.loadMethod == AppaMultiSceneSetup.LoadMethod.Additive) ||
                (entry.loadMethod == AppaMultiSceneSetup.LoadMethod.AdditiveAsync))
            {
                var buildEntry = EditorBuildSettings.scenes.FirstOrDefault(x => x.path == scene.path);
                if ((buildEntry == null) || !buildEntry.enabled)
                {
                    var textRect = new Rect(selectionRect);
                    textRect.xMax -= 100.0f;
                    GUI.Label(textRect, ColorText("Not in Build", Color.red), _justifyRightLabel);
                }
            }

            EditorGUI.BeginChangeCheck();
            selectionRect.xMin = selectionRect.xMax - 100.0f;
            GUIUtility.GetControlID(FocusType.Passive);
            entry.loadMethod =
                (AppaMultiSceneSetup.LoadMethod) EditorGUI.EnumPopup(selectionRect, entry.loadMethod);
            if (EditorGUI.EndChangeCheck())
            {
                EditorSceneManager.MarkSceneDirty(sceneSetup.gameObject.scene);
            }
        }
    }
}
