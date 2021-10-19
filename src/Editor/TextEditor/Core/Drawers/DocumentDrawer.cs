using System;
using Appalachia.CI.TextEditor.Core.Documents;
using UnityEditor;

namespace Appalachia.CI.TextEditor.Core.Drawers
{
    [Serializable]
    public abstract class DocumentDrawer<TDocument> : DrawerBase
        where TDocument : EditableDocumentBase
    {
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

        protected abstract void DrawDocument(TDocument document);

        protected void DrawTextMode(TDocument document)
        {
            document.RawText = EditorGUILayout.TextArea(document.RawText);
        }
    }
}
