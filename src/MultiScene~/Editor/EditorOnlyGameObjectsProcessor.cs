using UnityEditor;
using UnityEngine;

namespace Appalachia.Utility.MultiScene
{
    public static class EditorOnlyGameObjectsProcessor
    {
        [UnityEditor.Callbacks.PostProcessScene(-1)]
        private static void OnPostProcessScene()
        {
            if (!BuildPipeline.isBuildingPlayer)
            {
                return;
            }

            var allGameObjects = Resources.FindObjectsOfTypeAll<GameObject>();
            foreach (var gameObject in allGameObjects)
            {
                if (!gameObject.CompareTag("EditorOnly"))
                {
                    continue;
                }

                if (gameObject && !EditorUtility.IsPersistent(gameObject))
                {
                    Debug.LogFormat(
                        gameObject.transform.parent,
                        "Destroying left-over 'EditorOnly'-tagged GameObject: {0}",
                        gameObject.name
                    );
                    Object.DestroyImmediate(gameObject, false);
                }
            }
        }
    }
}
