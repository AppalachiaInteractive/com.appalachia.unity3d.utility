#if UNITY_2017_1_OR_NEWER
using UnityEngine;

namespace Appalachia.Utility.MultiScene.CustomResolvers
{
    internal static class PlayableDirectorResolver
    {
#if UNITY_EDITOR
        [UnityEditor.InitializeOnLoadMethod]
#endif
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void AddCustomResolver()
        {
            AppaCrossSceneReferenceResolver.AddCustomResolver(HandleCrossSceneReference);
        }

        private static bool HandleCrossSceneReference(RuntimeCrossSceneReference xRef)
        {
            if (!(xRef.fromObject is UnityEngine.Playables.PlayableDirector))
            {
                return false;
            }

            var isDirty = false;

            var sourceField = xRef.sourceField;
            if (sourceField.StartsWith("m_SceneBindings"))
            {
                PlayableDirector_SceneBindings(xRef);
                isDirty = true;
            }

            if (sourceField.StartsWith("m_ExposedReferences"))
            {
                PlayableDirector_ExposedReferences(xRef);
                isDirty = true;
            }

            if (isDirty)
            {
                var playableDirector = xRef.fromObject as UnityEngine.Playables.PlayableDirector;
                if (playableDirector)
                {
#if UNITY_2017_3_OR_NEWER
                    if (playableDirector.state == UnityEngine.Playables.PlayState.Playing)
                    {
                        AppaDebug.LogWarning(
                            playableDirector,
                            "To prevent issues, delay the PlayableDirector '{0}' until after cross-scene references are loaded. Cross-Scene Reference: {1}",
                            playableDirector,
                            xRef
                        );
                        playableDirector.RebuildGraph();
                    }
#else
					if ( playableDirector.gameObject.activeSelf )
					{
						AppaDebug.LogWarning( playableDirector, "Upgrade to Unity 2017.3 for proper Playables support. Hack work-around for 2017.1 and 2017.2: Disable/ReEnable the GameObject" );
						playableDirector.gameObject.SetActive( false );
						playableDirector.gameObject.SetActive( true );
					}
#endif
                }
            }

            return isDirty;
        }

        private static void PlayableDirector_ExposedReferences(RuntimeCrossSceneReference xRef)
        {
            var data = xRef.data;

            var playableDirector = xRef.fromObject as UnityEngine.Playables.PlayableDirector;
            for (var i = 0; i < data.Count; i += 2)
            {
                var key = data[i].@string;

                AppaDebug.Log(
                    xRef.fromObject,
                    "Restoring PlayableDirector Exposed Binding {0} = {1}",
                    key,
                    xRef.toObject
                );
                playableDirector.ClearReferenceValue(key);
                playableDirector.SetReferenceValue(key, xRef.toObject);
            }
        }

        private static void PlayableDirector_SceneBindings(RuntimeCrossSceneReference xRef)
        {
            var data = xRef.data;

            var playableDirector = xRef.fromObject as UnityEngine.Playables.PlayableDirector;
            for (var i = 0; i < data.Count; i += 2)
            {
                var key = data[i].@object;

                AppaDebug.Log(
                    xRef.fromObject,
                    "Restoring PlayableDirector Scene Binding {0} = {1}",
                    key,
                    xRef.toObject
                );
                playableDirector.SetGenericBinding(key, xRef.toObject);
            }
        }
    }
}
#endif
