#if UNITY_EDITOR
using System;
using Appalachia.Utility.Events;
using Appalachia.Utility.Events.Extensions;
using Unity.Profiling;
using UnityEditor;

namespace Appalachia.Utility.Execution
{
    [InitializeOnLoad]
    public static partial class AppalachiaApplication
    {
        private static void StaticInitializeInEditor()
        {
            using (_PRF_StaticInitializeInEditor.Auto())
            {
                AssemblyReloadEvents.beforeAssemblyReload -= Editor.AssemblyReloadEventsOnBeforeAssemblyReload;
                AssemblyReloadEvents.afterAssemblyReload -= Editor.AssemblyReloadEventsOnAfterAssemblyReload;
                EditorApplication.update -= Editor.EditorUpdate;
                EditorApplication.playModeStateChanged -= Editor.EditorApplicationOnPlayModeStateChanged;
                
                AssemblyReloadEvents.beforeAssemblyReload += Editor.AssemblyReloadEventsOnBeforeAssemblyReload;
                AssemblyReloadEvents.afterAssemblyReload += Editor.AssemblyReloadEventsOnAfterAssemblyReload;
                EditorApplication.update += Editor.EditorUpdate;
                EditorApplication.playModeStateChanged += Editor.EditorApplicationOnPlayModeStateChanged;
            }
        }

        #region Nested type: Editor

        public static class Editor
        {
            #region Static Fields and Autoproperties

            public static AppaEvent.Data EnteredEditMode;
            public static AppaEvent.Data EnteredPlayMode;
            public static AppaEvent.Data ExitingEditMode;
            public static AppaEvent.Data ExitingPlayMode;
            public static AppaEvent.Data PlayModeStateChanged;
            public static AppaEvent.Data Updated;

            #endregion

            public static void ForceRecompile()
            {
                UnityEditor.Compilation.CompilationPipeline.RequestScriptCompilation(
                    UnityEditor.Compilation.RequestScriptCompilationOptions.None
                );
            }

            internal static void AssemblyReloadEventsOnAfterAssemblyReload()
            {
                using (_PRF_AssemblyReloadEventsOnAfterAssemblyReload.Auto())
                {
                    _domainIsReloading = false;
                }
            }

            internal static void AssemblyReloadEventsOnBeforeAssemblyReload()
            {
                using (_PRF_AssemblyReloadEventsOnBeforeAssemblyReload.Auto())
                {
                    _domainIsReloading = true;
                }
            }

            internal static void EditorApplicationOnPlayModeStateChanged(PlayModeStateChange obj)
            {
                using (_PRF_EditorApplicationOnPlayModeStateChanged.Auto())
                {
                    PlayModeStateChanged.RaiseEvent();

                    switch (obj)
                    {
                        case PlayModeStateChange.EnteredEditMode:
                            EnteredEditMode.RaiseEvent();
                            break;
                        case PlayModeStateChange.ExitingEditMode:
                            ExitingEditMode.RaiseEvent();
                            break;
                        case PlayModeStateChange.EnteredPlayMode:
                            EnteredPlayMode.RaiseEvent();
                            break;
                        case PlayModeStateChange.ExitingPlayMode:
                            ExitingPlayMode.RaiseEvent();
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(obj), obj, null);
                    }
                }
            }

            internal static void EditorUpdate()
            {
                using (_PRF_EditorUpdate.Auto())
                {
                    Updated.RaiseEvent();
                }
            }

            #region Profiling

            private const string _PRF_PFX = nameof(AppalachiaApplication) + "." + nameof(Editor) + ".";

            private static readonly ProfilerMarker _PRF_EditorApplicationOnPlayModeStateChanged =
                new ProfilerMarker(_PRF_PFX + nameof(EditorApplicationOnPlayModeStateChanged));

            private static readonly ProfilerMarker _PRF_AssemblyReloadEventsOnAfterAssemblyReload =
                new ProfilerMarker(_PRF_PFX + nameof(AssemblyReloadEventsOnAfterAssemblyReload));

            private static readonly ProfilerMarker _PRF_AssemblyReloadEventsOnBeforeAssemblyReload =
                new ProfilerMarker(_PRF_PFX + nameof(AssemblyReloadEventsOnBeforeAssemblyReload));

            private static readonly ProfilerMarker _PRF_EditorUpdate =
                new ProfilerMarker(_PRF_PFX + nameof(EditorUpdate));

            #endregion
        }

        #endregion

        #region Profiling

        private static readonly ProfilerMarker _PRF_StaticInitializeInEditor =
            new ProfilerMarker(_PRF_PFX + nameof(StaticInitializeInEditor));

        #endregion
    }
}

#endif