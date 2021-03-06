using System;
using Appalachia.Utility.TextEditor.Core.Drawers;
using UnityEditor;

namespace Appalachia.Utility.TextEditor.Text
{
    [Serializable]
    public class TextDocumentDrawer : DocumentDrawer<EditableTextDocument>
    {
        /// <inheritdoc />
        protected override void DrawDocument(EditableTextDocument document)
        {
            document.RawText = EditorGUILayout.TextArea(document.RawText);
        }

        #region Menu Items

        [MenuItem(PKG.Menu.Assets.Base + "Create/Text/Text", priority = 200)]
        public static void CreateNew()
        {
            CreateNewFile("\n", "txt");
        }

        #endregion
    }
}
