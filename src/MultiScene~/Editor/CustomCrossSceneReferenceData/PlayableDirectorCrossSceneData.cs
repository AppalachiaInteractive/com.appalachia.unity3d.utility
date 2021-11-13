#if UNITY_2017_1_OR_NEWER
using System.Collections.Generic;
using UnityEngine.Playables;

namespace Appalachia.Utility.MultiScene.CustomCrossSceneReferenceData
{
    internal static class PlayableDirectorCrossSceneData
    {
        private static List<GenericData> GetCustomCrossSceneReferenceData(EditorCrossSceneReference crossRef)
        {
            var playableDirector = crossRef.fromObject as PlayableDirector;
            if (!playableDirector)
            {
                throw new System.ArgumentException("crossRef.fromObject contained an incompatible class");
            }

            var genericData = new List<GenericData>();

            var fromProperty = crossRef.fromProperty;
            var propertyPath = fromProperty.propertyPath;
            var serializedObject = fromProperty.serializedObject;

            if (propertyPath.StartsWith("m_SceneBindings") && propertyPath.EndsWith("value"))
            {
                var spElement = serializedObject.FindProperty(
                    fromProperty.propertyPath.Substring(
                        0,
                        fromProperty.propertyPath.Length - fromProperty.name.Length - 1
                    )
                );
                genericData.Add(spElement.FindPropertyRelative("key").objectReferenceValue);
            }
            else if (propertyPath.StartsWith("m_ExposedReferences") && propertyPath.EndsWith("second"))
            {
                var spElement = serializedObject.FindProperty(
                    propertyPath.Substring(0, propertyPath.Length - fromProperty.name.Length - 1)
                );
                genericData.Add(spElement.FindPropertyRelative("first").stringValue);
            }

            return genericData;
        }

        [UnityEditor.InitializeOnLoadMethod]
        private static void RegisterCustomDataProcessor()
        {
            CrossSceneReferenceProcessor.AddCustomCrossSceneDataProcessor<PlayableDirector>(
                GetCustomCrossSceneReferenceData
            );
        }
    }
}
#endif
