using Appalachia.Utility.TextEditor.Core.Documents;

namespace Appalachia.Utility.TextEditor.Text
{
    public class EditableTextDocument : EditableDocument<EditableTextDocument, TextDocumentDrawer>
    {
        /// <inheritdoc />
        protected internal override void ParseText(string text)
        {
        }

        /// <inheritdoc />
        protected override bool CheckIsValid(string text)
        {
            return true;
        }

        /// <inheritdoc />
        protected override TextDocumentDrawer GetDocumentDrawer()
        {
            return drawerInstance ??= new TextDocumentDrawer();
        }

        /// <inheritdoc />
        protected override string Serialize()
        {
            return RawText;
        }
    }
}
