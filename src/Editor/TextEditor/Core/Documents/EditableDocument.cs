using System;
using Appalachia.Utility.TextEditor.Core.Drawers;

namespace Appalachia.Utility.TextEditor.Core.Documents
{
    [Serializable]
    public abstract class EditableDocument<TDocument, TDrawer> : EditableDocumentBase
        where TDrawer : DocumentDrawer<TDocument>
        where TDocument : EditableDocumentBase
    {
        #region Fields and Autoproperties

        protected TDrawer drawerInstance;

        #endregion

        protected abstract TDrawer GetDocumentDrawer();

        /// <inheritdoc />
        protected override void Dispose(bool isDisposing)
        {
            drawerInstance?.Dispose();
        }

        /// <inheritdoc />
        protected override DrawerBase GetDrawer()
        {
            var drawer = GetDocumentDrawer();
            return drawer;
        }
    }
}
