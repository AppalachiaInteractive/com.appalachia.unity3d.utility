#if UNITY_EDITOR

#region

using System.Diagnostics;
using Appalachia.Utility.AutoSave.Configuration;
using UnityEditor;
using UnityEngine;

#endregion

namespace Appalachia.Utility.AutoSave
{
    [InitializeOnLoad]
    internal static class AutoSaverExecutionHandler
    {
        private static float? _launchTime;
        private static float _editorTimer;

        static AutoSaverExecutionHandler()
        {
            EditorApplication.update -= OnEditorApplicationUpdate;
            EditorApplication.update += OnEditorApplicationUpdate;
        }

        public static void OnEditorApplicationUpdate()
        {
            if (Debugger.IsAttached)
            {
                return;
            }
            
            if (!AutoSaverConfiguration.Enable)
            {
                return;
            }

            if (UnityEditor.BuildPipeline.isBuildingPlayer)
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

            if (Application.isPlaying)
            {
                if (_launchTime == null)
                {
                    _launchTime = AutoSaverConfiguration.EditorTimer;
                }

                return;
            }

            if (_launchTime != null)
            {
                AutoSaverConfiguration.LastSave +=
                    AutoSaverConfiguration.EditorTimer - _launchTime.Value;
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
