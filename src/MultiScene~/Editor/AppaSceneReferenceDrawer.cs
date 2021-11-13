using UnityEditor;
using UnityEngine;

namespace Appalachia.Utility.MultiScene
{
    [CustomPropertyDrawer(typeof(AppaSceneReference))]
    public class AppaSceneReferenceDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            var oldIndentLevel = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            var propAssetGUID = property.FindPropertyRelative("editorAssetGUID");
            var assetGUID = propAssetGUID.stringValue;

            var propName = property.FindPropertyRelative("name");
            var name = propName.stringValue;

            var propPath = property.FindPropertyRelative("_path");
            var path = propPath.stringValue;

            var realPath = AssetDatabase.GUIDToAssetPath(assetGUID);
            var sceneAsset = AssetDatabase.LoadMainAssetAtPath(realPath);

            EditorGUI.BeginChangeCheck();
            sceneAsset = EditorGUI.ObjectField(position, sceneAsset, typeof(SceneAsset), false);
            if (EditorGUI.EndChangeCheck())
            {
                path = AssetDatabase.GetAssetOrScenePath(sceneAsset);
                name = System.IO.Path.GetFileNameWithoutExtension(path);
                assetGUID = AssetDatabase.AssetPathToGUID(path);

                propAssetGUID.stringValue = assetGUID;
                propPath.stringValue = path;
                propName.stringValue = name;

                property.serializedObject.ApplyModifiedProperties();
            }

            propPath.Dispose();
            propName.Dispose();
            propAssetGUID.Dispose();

            EditorGUI.indentLevel = oldIndentLevel;
            EditorGUI.EndProperty();
        }
    }
}
