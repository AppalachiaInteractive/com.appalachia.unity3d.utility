using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR

#endif

namespace Appalachia.Utility.MultiScene
{
    public static class AppaPreferences
    {
        public enum CrossSceneReferenceHandling
        {
            UnityDefault,
            DoNotDuplicate,
            Save
        }

#if UNITY_EDITOR
        public static bool AllowAutoload
        {
            get => EditorPrefs.GetBool("AppaAllowAutoload", true);
            set => EditorPrefs.SetBool("AppaAllowAutoload", value);
        }

        public static CrossSceneReferenceHandling CrossSceneReferencing
        {
            get =>
                (CrossSceneReferenceHandling) EditorPrefs.GetInt(
                    "AppaCrossSceneReferencing",
                    (int) CrossSceneReferenceHandling.Save
                );
            set => EditorPrefs.SetInt("AppaCrossSceneReferencing", (int) value);
        }

        public static bool VerboseLogging
        {
            get => EditorPrefs.GetBool("AppaDebugLog", false);
            set => EditorPrefs.SetBool("AppaDebugLog", value);
        }

        public static bool PerfLogging
        {
            get => EditorPrefs.GetBool("AppaDebugPerfLog", false);
            set => EditorPrefs.SetBool("AppaDebugPerfLog", value);
        }

        public static bool DebugEnabled
        {
            get => EditorPrefs.GetBool("AppaDebugEnabled", false);
            set => EditorPrefs.SetBool("AppaDebugEnabled", value);
        }
#else
#endif

#if UNITY_EDITOR

#if UNITY_2018_4_OR_NEWER
        [SettingsProvider]
        private static SettingsProvider AppaPreferencesSettingsProvder()
        {
            return new SettingsProvider(
                "Preferences/Advanced Multi-Scene",
                SettingsScope.User,
                new[] {"AMS", "Multi-Scene", "References", "Cross-Scene"}
            ) {guiHandler = searchContext => { AppaPreferencesOnGUI(); }};
        }

#else
		[PreferenceItem("Multi-Scene")]
#endif
        private static void AppaPreferencesOnGUI()
        {
            var newCrossSceneRefs = (CrossSceneReferenceHandling) EditorGUILayout.EnumPopup(
                "Cross-Scene Referencing",
                CrossSceneReferencing
            );
            var bNewAutoload = EditorGUILayout.Toggle("Allow SubScene Auto-Load", AllowAutoload);

            EditorGUILayout.Space();

            var bNewVerboseLog = EditorGUILayout.Toggle("Verbose Logging",  VerboseLogging);
            var bNewPerfLog = EditorGUILayout.Toggle("Performance Logging", PerfLogging);

            if (GUI.changed)
            {
                CrossSceneReferencing = newCrossSceneRefs;
                AllowAutoload = bNewAutoload;
                VerboseLogging = bNewVerboseLog;
                PerfLogging = bNewPerfLog;

                GUI.changed = false;
            }

#if false
			if ( DebugEnabled )
			{
				EditorGUILayout.Space();

				bool bNewDisableDrawer =
 EditorGUILayout.Toggle( "DEBUG Disable Hierarchy Drawer", DebugDisableHierarchyDrawer );

				EditorGUI.indentLevel += 1;
				GUI.enabled = !bNewDisableDrawer;
				bool bNewDebugShowGameObjectFlags =
 EditorGUILayout.Toggle( "Draw GameObject Flags", DebugShowGameObjectFlags );
				GUI.enabled = true;
				EditorGUI.indentLevel -= 1;

				bool bNewDisableModificationProcessor =
 EditorGUILayout.Toggle( "DEBUG Disable Modification Processor", DebugDisableModificationProcessor );
				bool bNewShowBookkeepingObjects =
 EditorGUILayout.Toggle( "DEBUG Show Bookkeping Objects", DebugShowBookkepingObjects );

				if ( GUI.changed )
				{
					DebugDisableHierarchyDrawer = bNewDisableDrawer;
					DebugDisableModificationProcessor = bNewDisableModificationProcessor;
					DebugShowBookkepingObjects = bNewShowBookkeepingObjects;
					DebugShowGameObjectFlags = bNewDebugShowGameObjectFlags;
				}
			}
#endif
        }

#if !UNITY_5_3
        [InitializeOnLoadMethod]
        private static void InitCrossSceneReferenceHandling()
        {
            UnityEditor.SceneManagement.EditorSceneManager.preventCrossSceneReferences =
                CrossSceneReferencing == CrossSceneReferenceHandling.UnityDefault;
        }
#endif
#endif
    }
}
