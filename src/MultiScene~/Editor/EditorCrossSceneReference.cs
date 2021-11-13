using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Appalachia.Utility.MultiScene
{
    public struct EditorCrossSceneReference
    {
        #region Fields

        public List<GenericData> data;
        public Object toInstance;

        public Scene fromScene;
        public Scene toScene;
        public SerializedProperty fromProperty;

        #endregion

        public Object fromObject => fromProperty.serializedObject.targetObject;

        public override bool Equals(object obj)
        {
            if (obj is EditorCrossSceneReference)
            {
                var other = (EditorCrossSceneReference) obj;
                return (other.fromProperty == fromProperty) && (other.toInstance == toInstance);
            }

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return fromProperty.GetHashCode() + toInstance.GetHashCode();
        }

        public override string ToString()
        {
            var fromString = fromObject ? fromObject.ToString() : "(null)";
            var toString = toInstance ? toInstance.ToString() : "(null)";

            var fromGameObject = GameObjectEx.EditorGetGameObjectFromComponent(fromObject);
            if (fromGameObject)
            {
                fromString = string.Format("{0} ({1})", fromGameObject.GetFullName(), fromObject.GetType());
            }

            var toGameObject = GameObjectEx.EditorGetGameObjectFromComponent(toInstance);
            if (toGameObject)
            {
                toString = string.Format("{0} ({1})", toGameObject.GetFullName(), toInstance.GetType());
            }

            return string.Format("{0}.{1} => {2}", fromString, fromProperty.propertyPath, toString);
        }

        public RuntimeCrossSceneReference ToSerializable()
        {
            var fromField = ToRuntimeSerializableField(fromProperty);
            return new RuntimeCrossSceneReference(fromObject, fromField, new UniqueObject(toInstance), data);
        }

        private string ToRuntimeSerializableField(SerializedProperty property)
        {
            const string ARRAY_INDICATOR = "@ArrayIndex[";
            var arrayIndicatorLength = ARRAY_INDICATOR.Length;

            var parseablePropertyPath = property.propertyPath.Replace(".Array.data[", "." + ARRAY_INDICATOR);
            var splitPaths = parseablePropertyPath.Split('.');

            var sb = new System.Text.StringBuilder();
            for (var i = 0; i < splitPaths.Length; ++i)
            {
                var pathPiece = splitPaths[i];

                var bIsArrayIndex = pathPiece.StartsWith(ARRAY_INDICATOR);
                if (!bIsArrayIndex)
                {
                    if (i > 0)
                    {
                        sb.Append('.');
                    }

                    sb.Append(pathPiece);
                }
                else
                {
                    var indexString = pathPiece.Substring(
                        arrayIndicatorLength,
                        pathPiece.Length - arrayIndicatorLength - 1
                    );

                    var arrayIndex = 0;
                    if (int.TryParse(indexString, out arrayIndex))
                    {
                        sb.Append(',');
                        sb.Append(arrayIndex);
                    }
                    else
                    {
                        AppaDebug.LogError(
                            null,
                            "Could not parse array index for property path {0}",
                            property.propertyPath
                        );
                    }
                }
            }

            return sb.ToString();
        }
    }
}
