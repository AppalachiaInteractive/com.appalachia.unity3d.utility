using System;
using System.IO;
using System.Text;
using Appalachia.CI.TextEditor.Core.Documents;
using Appalachia.CI.TextEditor.Core.GUICache;
using UnityEditor;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

namespace Appalachia.CI.TextEditor.Core.Drawers
{
    [Serializable]
    public abstract class DrawerBase
    {
        public GUICollection guiCollection = new();

        private void IntializeGUICollectionInternal()
        {
            if (guiCollection == null)
            {
                guiCollection = new GUICollection();
            }

            if (guiCollection.Count == 0)
            {
                guiCollection.AddStyle(
                    GUIKEYS.HeaderLabel,
                    new GUIStyle(EditorStyles.boldLabel) {padding = new RectOffset(5, 0, 0, 0)}
                );
                guiCollection.AddStyle(
                    GUIKEYS.HeaderField,
                    new GUIStyle(EditorStyles.toolbarTextField)
                    {
                        padding = new RectOffset(5, 0, 0, 0)
                    }
                );
                guiCollection.AddOptions(GUIKEYS.HeaderField, GUILayout.ExpandWidth(true));

                guiCollection.AddStyle(
                    GUIKEYS.SaveButton,
                    new GUIStyle(EditorStyles.toolbarButton)
                );
                guiCollection.AddColor(GUIKEYS.SaveButton, new Color(1f, .65f, .25f, .75f));

                guiCollection.AddStyle(
                    GUIKEYS.TextModeButton,
                    new GUIStyle(EditorStyles.toolbarButton)
                );
                guiCollection.AddColor(GUIKEYS.TextModeButton, new Color(.25f, .7f, .55f, .75f));

                InitializeGUICollection(guiCollection);
            }
        }

        // ReSharper disable once UnusedParameter.Global
        public virtual void InitializeGUICollection(GUICollection collection)
        {
        }

        public void Draw(EditableDocumentBase document)
        {
            IntializeGUICollectionInternal();

            var enabled = GUI.enabled;
            GUI.enabled = true;

            using (new EditorGUILayout.VerticalScope())
            {
                DrawHeader(document);

                EditorGUI.BeginChangeCheck();

                OnDraw(document);
            }

            GUI.enabled = enabled;
        }

        protected abstract void OnDraw(EditableDocumentBase document);

        public virtual bool DidChange()
        {
            return EditorGUI.EndChangeCheck();
        }

        protected virtual void DrawHeaderField(string label, string value)
        {
            var headerLabelStyle = guiCollection.GetStyle(GUIKEYS.HeaderLabel).Value;
            var headerFieldStyle = guiCollection.GetStyle(GUIKEYS.HeaderField).Value;
            var headerFieldOptions = guiCollection.GetOptions(GUIKEYS.HeaderField).Value;

            using (new EditorGUILayout.HorizontalScope())
            {
                EditorGUILayout.PrefixLabel($"{label}:", headerFieldStyle, headerLabelStyle);
                EditorGUILayout.TextField(value, headerFieldStyle, headerFieldOptions);
            }
        }

        protected virtual void DrawHeader(EditableDocumentBase document)
        {
            if (!document.FileInfo.Exists)
            {
                EditorGUILayout.HelpBox(
                    $"Unable to locate file at: [{document.FileInfo.FullName}]",
                    MessageType.Error,
                    true
                );
                return;
            }

            using (new EditorGUILayout.HorizontalScope())
            {
                GUI.enabled = false;

                EditorGUIUtility.labelWidth = 50;

                using (new EditorGUILayout.VerticalScope())
                {
                    DrawHeaderField("Name", document.FileInfo.Name);
                    DrawHeaderField("Size", FormatSize(document.FileInfo.Length));
                }

                EditorGUIUtility.labelWidth = 60;

                using (new EditorGUILayout.VerticalScope())
                {
                    DrawHeaderField("Created",  FormatDate(document.FileInfo.CreationTime));
                    DrawHeaderField("Modified", FormatDate(document.FileInfo.LastWriteTime));
                }

                GUI.enabled = true;
                var backgroundColor = GUI.backgroundColor;

                using (new EditorGUILayout.VerticalScope())
                {
                    GUI.enabled = document.SaveState.pending;

                    GUI.backgroundColor = document.SaveState.pending
                        ? guiCollection.GetColor(GUIKEYS.SaveButton)
                        : backgroundColor;

                    if (GUILayout.Button("Save", guiCollection.GetStyle(GUIKEYS.SaveButton)))
                    {
                        document.Save();
                    }

                    GUI.backgroundColor = document.InTextMode
                        ? guiCollection.GetColor(GUIKEYS.TextModeButton)
                        : backgroundColor;
                    GUI.enabled = true;

                    document.SetTextMode(
                        GUILayout.Toggle(
                            document.InTextMode,
                            "Text Mode",
                            EditorStyles.toolbarButton
                        )
                    );
                }

                GUI.backgroundColor = backgroundColor;
            }

            if (document.LoadState.failed)
            {
                EditorGUILayout.HelpBox(
                    $"Unable to load file: [{document.LoadState.exception.Message}]",
                    MessageType.Error
                );
                return;
            }

            if (document.ParseState.failed)
            {
                EditorGUILayout.HelpBox(
                    $"Unable to parse file: [{document.ParseState.exception.Message}]",
                    MessageType.Error
                );

                document.SetTextMode(true);
            }
            else if (document.ValidationState.failed)
            {
                EditorGUILayout.HelpBox(
                    $"Unable to validate file: [{document.ValidationState.exception.Message}]",
                    MessageType.Warning
                );

                document.SetTextMode(true);
            }

            if (document.SaveState.failed)
            {
                EditorGUILayout.HelpBox(
                    $"Unable to save file: [{document.SaveState.exception.Message}]",
                    MessageType.Error
                );
            }
        }

        public virtual void Dispose()
        {
        }

        protected static string GetNewFileBasePath(string extension)
        {
            var assetPath = AssetDatabase.GetAssetPath(Selection.activeObject);

            if (assetPath == "")
            {
                assetPath = "Assets";
            }

            var cleanExtension = new StringBuilder();

            for (var i = 0; i < extension.Length; i++)
            {
                var character = extension[i];
                if (char.IsLetterOrDigit(character) || (character == '-'))
                {
                    cleanExtension.Append(character);
                }
            }

            extension = cleanExtension.ToString();

            var outputDirectory = Path.GetDirectoryName(assetPath);
            Debug.Assert(outputDirectory != null, nameof(outputDirectory) + " != null");

            var newFileName = $"{DateTime.Now:yyyyMMdd-hhmmss}.{extension.Trim().Trim('.')}";

            var newFilePath = Path.Combine(outputDirectory, newFileName);

            return newFilePath;
        }

        protected static void CreateNewFile(string content, string extension)
        {
            var newFilePath = GetNewFileBasePath(extension);

            File.WriteAllText(newFilePath, content);
            AssetDatabase.Refresh();
        }

        protected string FormatSize(long size)
        {
            return size < 1024f
                ? $"{size} B"
                : size < (1024f * 1024f)
                    ? $"{size / 1024f:#.00} KB"
                    : size < (1024f * 1024f * 1024f)
                        ? $"{size / 1024f / 1024f:#.00} MB"
                        : $"{size / 1024f / 1024f / 1024f:#.00} GB";
        }

        protected string FormatDate(DateTime dateTime)
        {
            return $"{dateTime.ToShortDateString()} {dateTime.ToLongTimeString()}";
        }

        protected class GUIKEYS
        {
            public const string HeaderLabel = "HeaderLabel";
            public const string HeaderField = "HeaderField";
            public const string SaveButton = "SaveButton";
            public const string TextModeButton = "TextModeButton";
        }
    }
}
