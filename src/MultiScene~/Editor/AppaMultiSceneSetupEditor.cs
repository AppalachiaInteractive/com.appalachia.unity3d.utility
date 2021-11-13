using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Appalachia.Utility.MultiScene
{
    [CustomEditor(typeof(AppaMultiSceneSetup), true)]
    internal class AppaMultiSceneSetupEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            var target = (AppaMultiSceneSetup) this.target;

            if (target.sceneSetupMode == AppaMultiSceneSetup.SceneSetupManagement.Automatic)
            {
                var isActiveScene = SceneManager.GetActiveScene() == target.gameObject.scene;
                if (isActiveScene)
                {
                    EditorGUILayout.HelpBox(
                        "Scene Setup is automatically generated and saved with the scene based on the hierarchy.",
                        MessageType.Info
                    );
                }
                else
                {
                    EditorGUILayout.HelpBox(
                        "Scene Setup will not be updated or saved unless this Scene is set as Active",
                        MessageType.Warning
                    );
                }

                if (target.GetSceneSetup().Count < 1)
                {
                    DrawPropertiesExcluding(serializedObject, "m_Script", "_sceneSetup");
                    EditorGUILayout.HelpBox(
                        "This scene was never saved as the Active Scene.\nTherefore this Scene will not auto-load other scenes.",
                        MessageType.Info
                    );
                }
                else
                {
                    DrawPropertiesExcluding(serializedObject, "m_Script");
                }
            }
            else if (target.sceneSetupMode == AppaMultiSceneSetup.SceneSetupManagement.Manual)
            {
                EditorGUILayout.HelpBox(
                    "Scene Setup will not changed unless you modify it manually (or change Scene Setup Mode).",
                    MessageType.Info
                );
                DrawPropertiesExcluding(serializedObject, "m_Script");
            }
            else
            {
                EditorGUILayout.HelpBox("Scene Setup will not be saved", MessageType.Warning);
                DrawPropertiesExcluding(serializedObject, "m_Script", "_sceneSetup");
            }

            EditorGUILayout.HelpBox(
                "Note: This behaviour is always required for cross-scene referencing to work.",
                MessageType.Info
            );

            serializedObject.ApplyModifiedProperties();
            serializedObject.Update();

            if (GUI.changed)
            {
                AppaMultiSceneSetup.OnSceneSaving(target.gameObject.scene, target.scenePath);
                EditorApplication.RepaintHierarchyWindow();
            }
        }
    }
}
