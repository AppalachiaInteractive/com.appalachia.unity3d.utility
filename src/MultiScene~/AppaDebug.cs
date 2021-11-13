using UnityEngine;

#if UNITY_EDITOR

#endif

namespace Appalachia.Utility.MultiScene
{
    public static class AppaDebug
    {
        public static void Log(Object context, string message, params object[] parms)
        {
#if UNITY_EDITOR
            if (AppaPreferences.VerboseLogging)
#endif
            {
                Debug.LogFormat(context, "Appa Plugin: " + message, parms);
            }
        }

        public static void LogError(Object context, string message, params object[] parms)
        {
            Debug.LogErrorFormat(context, "Appa Plugin: " + message, parms);
        }

        public static void LogPerf(Object context, string message, params object[] parms)
        {
#if UNITY_EDITOR
            if (AppaPreferences.PerfLogging)
#endif
            {
                Debug.LogFormat(context, "Appa Perf: " + message, parms);
            }
        }

        public static void LogWarning(Object context, string message, params object[] parms)
        {
            Debug.LogWarningFormat(context, "Appa Plugin: " + message, parms);
        }

        internal static void EditorConditionalRestoreAllCrossSceneReferences()
        {
            /*
            foreach( var subScene in GetSubScenesSortedBottomUp() )
            {
                if ( subScene && !subScene.IsLocked() )
                {
                    CrossSceneReferenceProcessor.RestoreCrossSceneReferences(subScene);
                }
            }
             * */
        }
    }
}
