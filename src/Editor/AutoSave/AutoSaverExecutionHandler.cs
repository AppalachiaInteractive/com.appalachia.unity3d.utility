#if UNITY_EDITOR

#region

using System.Diagnostics;
using Appalachia.Utility.AutoSave.Configuration;
using Appalachia.Utility.Execution;
using UnityEditor;
using UnityEngine;

#endregion

namespace Appalachia.Utility.AutoSave
{
    [InitializeOnLoad]
    internal static class AutoSaverExecutionHandler
    {
        static AutoSaverExecutionHandler()
        {
            EditorApplication.update -= OnEditorApplicationUpdate;
            EditorApplication.update += OnEditorApplicationUpdate;

            UnityEditor.Compilation.CompilationPipeline.compilationStarted +=
                o => _compilationDisabled = true;
            UnityEditor.Compilation.CompilationPipeline.compilationFinished +=
                o => _compilationDisabled = false;
        }

        private static bool _compilationDisabled;
        private static float _editorTimer;
        private static float? _launchTime;

        public static void OnEditorApplicationUpdate()
        {
            if (_compilationDisabled)
            {
                return;
            }

            if (Debugger.IsAttached)
            {
                return;
            }

            if (!AutoSaverConfiguration.Enable)
            {
                return;
            }

            if (BuildPipeline.isBuildingPlayer)
            {
                return;
            }

            if (EditorApplication.isCompiling)
            {
                return;
            }

            if (EditorApplication.isUpdating)
            {
                return;
            }

            if (AppalachiaApplication.IsPlayingOrWillPlay)
            {
                if (_launchTime == null)
                {
                    _launchTime = AutoSaverConfiguration.EditorTimer;
                }

                return;
            }

            if (_launchTime != null)
            {
                AutoSaverConfiguration.LastSave += AutoSaverConfiguration.EditorTimer - _launchTime.Value;
                _launchTime = null;
            }

            if (Mathf.Abs(_editorTimer - AutoSaverConfiguration.EditorTimer) < 4)
            {
                return;
            }

            _editorTimer = AutoSaverConfiguration.EditorTimer;

            var saveAge = AutoSaverConfiguration.LastSave - AutoSaverConfiguration.EditorTimer;

            if (Mathf.Abs(saveAge) >= AutoSaverConfiguration.SaveInterval)
            {
                AutoSaver.Execute();
                EditorApplication.update -= OnEditorApplicationUpdate;
                EditorApplication.update += OnEditorApplicationUpdate;
            }
        }
    }
}

#endif
