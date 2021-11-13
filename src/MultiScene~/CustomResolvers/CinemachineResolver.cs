#if UNITY_2018_1_OR_NEWER
using System;
using UnityEngine;

namespace Appalachia.Utility.MultiScene.CustomResolvers
{
    internal static class CinemachineResolver
    {
        #region Fields

        private static bool hasCinemachine;

        #endregion

#if UNITY_EDITOR
        [UnityEditor.InitializeOnLoadMethod]
#endif
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void AddCustomResolver()
        {
            hasCinemachine = false;

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                try
                {
                    foreach (var type in assembly.GetTypes())
                    {
                        if (type.Namespace == "Cinemachine")
                        {
                            hasCinemachine = true;
                            break;
                        }
                    }
                }
                catch (Exception)
                {
                    // ignored
                }

                if (hasCinemachine)
                {
                    break;
                }
            }

            if (hasCinemachine)
            {
                AppaCrossSceneReferenceResolver.AddCustomResolver(HandleCrossSceneReference);
            }
        }

        private static bool HandleCrossSceneReference(RuntimeCrossSceneReference xRef)
        {
            var cinemachineBehaviour = xRef.fromObject as MonoBehaviour;
            if (!cinemachineBehaviour || !cinemachineBehaviour.isActiveAndEnabled)
            {
                return false;
            }

            var nameSpace = cinemachineBehaviour.GetType().Namespace;
            if (string.IsNullOrEmpty(nameSpace) || !nameSpace.StartsWith("Cinemachine"))
            {
                return false;
            }

            AppaDebug.LogWarning(
                xRef.fromObject,
                "xSceneRef on Cinemachine Behaviour: {0}. Disabling/Enabling to ensure pipeline is up to date.",
                xRef
            );
            cinemachineBehaviour.enabled = false;
            
            // ReSharper disable once Unity.InefficientPropertyAccess
            cinemachineBehaviour.enabled = true;

            return false;
        }
    }
}
#endif
