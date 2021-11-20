using UnityEngine;

namespace Appalachia.Utility.Extensions
{
    public static class DirtyExtensions
    {
        public static void SetDirty(this GameObject target)
        {
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(target);
#endif
        }

        public static void SetDirty(this Component target)
        {
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(target);
#endif
        }

        public static void SetDirtySO(this ScriptableObject target)
        {
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(target);
#endif
        }
    }
}
