using UnityEditor;
using UnityEngine;

namespace Appalachia.Utility.MultiScene
{
    [CustomPropertyDrawer(typeof(AppaMultiSceneSetup.SceneEntry))]
    internal class AppaMultiSceneEntryDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, null, true);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var propScene = property.FindPropertyRelative("scene");
            if (propScene != null)
            {
                var propName = propScene.FindPropertyRelative("name");
                if (propName != null)
                {
                    label.text = propName.stringValue;
                }
            }

            EditorGUI.PropertyField(position, property, label, true);
        }
    }
}