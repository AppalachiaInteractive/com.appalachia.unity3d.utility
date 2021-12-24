using Appalachia.Utility.Constants;
using Unity.Profiling;
using UnityEditor;
using UnityEngine;

namespace Appalachia.Utility.Extensions
{
    public static class Modifications
    {
        public static bool CanModifyAssets()
        {
            using (_PRF_CanModifyAssets.Auto())
            {
                return
#if UNITY_EDITOR
                    !APPASERIALIZE.InSerializationWindow &&
                    !Application.isPlaying &&
                    !EditorApplication.isPlayingOrWillChangePlaymode
                    /*&& !EditorApplication.isUpdating*/
#else
                        false
#endif
                    ;
            }
        }

        public static void CreateUndoStep(this Object[] go, string stepName)
        {
            using (_PRF_CreateUndoStep.Auto())
            {
#if UNITY_EDITOR
                if (CanModifyAssets())
                {
                    Undo.RegisterCompleteObjectUndo(go, stepName);
                }
#endif
            }
        }

        public static void CreateUndoStep(this Component[] go, string stepName)
        {
            using (_PRF_CreateUndoStep.Auto())
            {
#if UNITY_EDITOR
                if (CanModifyAssets())
                {
                    Undo.RegisterCompleteObjectUndo(go, stepName);
                }
#endif
            }
        }

        public static void CreateUndoStep(this ScriptableObject[] go, string stepName)
        {
            using (_PRF_CreateUndoStep.Auto())
            {
#if UNITY_EDITOR
                if (CanModifyAssets())
                {
                    Undo.RegisterCompleteObjectUndo(go, stepName);
                }
#endif
            }
        }

        public static void CreateUndoStep(this GameObject[] go, string stepName)
        {
            using (_PRF_CreateUndoStep.Auto())
            {
#if UNITY_EDITOR
                if (CanModifyAssets())
                {
                    Undo.RegisterCompleteObjectUndo(go, stepName);
                }
#endif
            }
        }

        public static void CreateUndoStep(this Object go, string stepName)
        {
            using (_PRF_CreateUndoStep.Auto())
            {
#if UNITY_EDITOR
                if (CanModifyAssets())
                {
                    Undo.RegisterCompleteObjectUndo(new[] { go }, stepName);
                }
#endif
            }
        }

        public static void CreateUndoStep(this Component go, string stepName)
        {
            using (_PRF_CreateUndoStep.Auto())
            {
#if UNITY_EDITOR
                if (CanModifyAssets())
                {
                    Undo.RegisterCompleteObjectUndo(new[] { go }, stepName);
                }
#endif
            }
        }

        public static void CreateUndoStep(this ScriptableObject go, string stepName)
        {
            using (_PRF_CreateUndoStep.Auto())
            {
#if UNITY_EDITOR
                if (CanModifyAssets())
                {
                    Undo.RegisterCompleteObjectUndo(new[] { go }, stepName);
                }
#endif
            }
        }

        public static void CreateUndoStep(this GameObject go, string stepName)
        {
            using (_PRF_CreateUndoStep.Auto())
            {
#if UNITY_EDITOR
                if (CanModifyAssets())
                {
                    Undo.RegisterCompleteObjectUndo(new[] { go }, stepName);
                }
#endif
            }
        }

        public static void CreateUndoStep(string stepName, params Object[] objects)
        {
            using (_PRF_CreateUndoStep.Auto())
            {
#if UNITY_EDITOR
                if (CanModifyAssets())
                {
                    Undo.RegisterCompleteObjectUndo(objects, stepName);
                }
#endif
            }
        }

        public static void MarkAsModified(this GameObject target)
        {
            using (_PRF_MarkAsModified.Auto())
            {
#if UNITY_EDITOR
                if (CanModifyAssets())
                {
                    EditorUtility.SetDirty(target);

                    if (target.scene != default)
                    {
                        UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(target.scene);
                    }
                }
#endif
            }
        }

        public static void MarkAsModified(this Component target)
        {
            using (_PRF_MarkAsModified.Auto())
            {
#if UNITY_EDITOR
                if (CanModifyAssets())
                {
                    EditorUtility.SetDirty(target);

                    target.gameObject.MarkAsModified();
                }
#endif
            }
        }

        public static void MarkAsModified(this ScriptableObject target)
        {
            using (_PRF_MarkAsModified.Auto())
            {
#if UNITY_EDITOR
                if (CanModifyAssets())
                {
                    EditorUtility.SetDirty(target);
                }
#endif
            }
        }

        public static void MarkAsModified(this Object target)
        {
            using (_PRF_MarkAsModified.Auto())
            {
#if UNITY_EDITOR
                if (CanModifyAssets())
                {
                    EditorUtility.SetDirty(target);
                }
#endif
            }
        }

        #region Profiling

        private const string _PRF_PFX = nameof(Modifications) + ".";

        private static readonly ProfilerMarker _PRF_CanModifyAssets =
            new ProfilerMarker(_PRF_PFX + nameof(CanModifyAssets));

        private static readonly ProfilerMarker _PRF_MarkAsModified =
            new ProfilerMarker(_PRF_PFX + nameof(MarkAsModified));

        private static readonly ProfilerMarker _PRF_CreateUndoStep =
            new ProfilerMarker(_PRF_PFX + nameof(CreateUndoStep));

        #endregion
    }
}
