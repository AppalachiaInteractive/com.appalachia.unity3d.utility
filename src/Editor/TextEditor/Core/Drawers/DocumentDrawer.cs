using System;
using Appalachia.Utility.TextEditor.Core.Documents;
using UnityEditor;

namespace Appalachia.Utility.TextEditor.Core.Drawers
{
    [Serializable]
    public abstract class DocumentDrawer<TDocument> : DrawerBase
        where TDocument : EditableDocumentBase
    {
        protected abstract void DrawDocument(TDocument document);

        /// <inheritdoc />
        protected override void OnDraw(EditableDocumentBase document)
        {
            var cast = document as TDocument;

            if (cast.InTextMode)
            {
                DrawTextMode(cast);
            }
            else
            {
                DrawDocument(cast);
            }
        }

        protected void DrawTextMode(TDocument document)
        {
            document.RawText = EditorGUILayout.TextArea(document.RawText);
        }
    }
}
