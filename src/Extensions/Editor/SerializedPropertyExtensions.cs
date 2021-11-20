#if UNITY_EDITOR
using UnityEngine;

namespace Appalachia.Utility.Extensions
{
    public static class SerializedPropertyExtensions
    {
        public static bool Equals(this UnityEditor.SerializedProperty a, UnityEditor.SerializedProperty b)
        {
            return a.propertyPath.Equals(b.propertyPath) &&
                   a.propertyType.Equals(b.propertyType) &&
                   a.GetPropertyStringValue().Equals(b.GetPropertyStringValue());
        }

        public static string GetPropertyStringValue(this UnityEditor.SerializedProperty property)
        {
            switch (property.propertyType)
            {
                case UnityEditor.SerializedPropertyType.AnimationCurve:
                    goto default;

                case UnityEditor.SerializedPropertyType.ArraySize:
                    return property.arraySize.ToString();

                case UnityEditor.SerializedPropertyType.Boolean:
                    return property.boolValue.ToString();

                case UnityEditor.SerializedPropertyType.Bounds:
                    return property.boundsValue.ToString();

                case UnityEditor.SerializedPropertyType.Character:
                    goto default;

                case UnityEditor.SerializedPropertyType.Color:
                    return property.colorValue.ToString();

                case UnityEditor.SerializedPropertyType.Enum:
                    return property.enumNames[property.enumValueIndex];

                case UnityEditor.SerializedPropertyType.Float:
                    return property.floatValue.ToString();

                case UnityEditor.SerializedPropertyType.Generic:
                    goto default;

                case UnityEditor.SerializedPropertyType.Gradient:
                    goto default;

                case UnityEditor.SerializedPropertyType.Integer:
                case UnityEditor.SerializedPropertyType.LayerMask:
                    return property.intValue.ToString();

                case UnityEditor.SerializedPropertyType.ObjectReference:
                    return property.objectReferenceValue ? property.objectReferenceValue.name : null;

                case UnityEditor.SerializedPropertyType.Rect:
                    return property.rectValue.ToString();

                case UnityEditor.SerializedPropertyType.String:
                    return property.stringValue;

                case UnityEditor.SerializedPropertyType.Vector2:
                    return property.vector2Value.ToString();

                case UnityEditor.SerializedPropertyType.Vector3:
                    return property.vector3Value.ToString();

                default:
                    Debug.LogError("GetPropertyStringValue type not supported: " + property.propertyType);
                    return null;
            }
        }

        public static bool Supported(this UnityEditor.SerializedProperty property)
        {
            switch (property.propertyType)
            {
                case UnityEditor.SerializedPropertyType.AnimationCurve:
                case UnityEditor.SerializedPropertyType.ArraySize:
                case UnityEditor.SerializedPropertyType.Character:
                case UnityEditor.SerializedPropertyType.Generic:
                case UnityEditor.SerializedPropertyType.Gradient:
                    return false;
            }

            return true;
        }
    }
}

#endif
