using System;
using Appalachia.Utility.TextEditor.Core.Drawers;

namespace Appalachia.Utility.TextEditor.Core.Documents
{
    [Serializable]
    public abstract class EditableDocument<TDocument, TDrawer> : EditableDocumentBase
        where TDrawer : DocumentDrawer<TDocument>
        where TDocument : EditableDocumentBase
    {
        protected TDrawer drawerInstance;

        protected override void Dispose(bool isDisposing)
        {
            drawerInstance?.Dispose();
        }

        protected abstract TDrawer GetDocumentDrawer();

        protected override DrawerBase GetDrawer()
        {
            var drawer = GetDocumentDrawer();
            return drawer;
        }
    }
}
