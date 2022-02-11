/*#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Profiling;
using UnityEditor;

namespace Appalachia.Utility.Logging
{
    public static partial class AppaLog
    {
        #region Constants and Static Readonly

        private const string LOG_DEBUGGER_STEPTHROUGH = "LOG_DEBUGGER_STEPTHROUGH";

        #endregion

        private static void AddScriptingDefine(string define)
        {
            using (_PRF_AddScriptingDefine.Auto())
            {
                PlayerSettings.GetScriptingDefineSymbolsForGroup(
                    EditorUserBuildSettings.selectedBuildTargetGroup,
                    out var scriptingDefines
                );

                var defines = new HashSet<string>(scriptingDefines) { define };

                var sortedDefines = defines.ToArray();
                Array.Sort(sortedDefines);

                PlayerSettings.SetScriptingDefineSymbolsForGroup(
                    EditorUserBuildSettings.selectedBuildTargetGroup,
                    sortedDefines
                );
            }
        }

        private static void RemoveScriptingDefine(string define)
        {
            using (_PRF_RemoveScriptingDefine.Auto())
            {
                PlayerSettings.GetScriptingDefineSymbolsForGroup(
                    EditorUserBuildSettings.selectedBuildTargetGroup,
                    out var scriptingDefines
                );

                var defines = new HashSet<string>(scriptingDefines);

                defines.Remove(define);

                var sortedDefines = defines.ToArray();
                Array.Sort(sortedDefines);

                PlayerSettings.SetScriptingDefineSymbolsForGroup(
                    EditorUserBuildSettings.selectedBuildTargetGroup,
                    sortedDefines
                );
            }
        }

        #region Menu Items

        [MenuItem(MENU_DEBUGGER, false, MENU_PRIORITY + 100)]
        private static void DefineDebuggerStepthrough()
        {
            using (_PRF_DefineDebuggerStepthrough.Auto())
            {
#if LOG_DEBUGGER_STEPTHROUGH
                RemoveScriptingDefine(LOG_DEBUGGER_STEPTHROUGH);
#else
                AddScriptingDefine(LOG_DEBUGGER_STEPTHROUGH);
#endif
            }
        }

        [MenuItem(MENU_DEBUGGER, true, MENU_PRIORITY + 100)]
        private static bool DefineDebuggerStepthroughValidate()
        {
            using (_PRF_DefineDebuggerStepthroughValidate.Auto())
            {
#if LOG_DEBUGGER_STEPTHROUGH
            UnityEditor.Menu.SetChecked(MENU_DEBUGGER, true);
#else
                Menu.SetChecked(MENU_DEBUGGER, false);
#endif
                return true;
            }
        }

        #endregion

        #region Profiling

        private static readonly ProfilerMarker _PRF_AddScriptingDefine =
            new ProfilerMarker(_PRF_PFX + nameof(AddScriptingDefine));

        private static readonly ProfilerMarker _PRF_RemoveScriptingDefine =
            new ProfilerMarker(_PRF_PFX + nameof(RemoveScriptingDefine));

        private static readonly ProfilerMarker _PRF_DefineDebuggerStepthrough =
            new ProfilerMarker(_PRF_PFX + nameof(DefineDebuggerStepthrough));

        private static readonly ProfilerMarker _PRF_DefineDebuggerStepthroughValidate =
            new ProfilerMarker(_PRF_PFX + nameof(DefineDebuggerStepthroughValidate));

        #endregion
    }
}

#endif*/


