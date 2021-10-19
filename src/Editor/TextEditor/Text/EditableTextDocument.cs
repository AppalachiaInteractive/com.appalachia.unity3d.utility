using Appalachia.CI.TextEditor.Core.Documents;

namespace Appalachia.CI.TextEditor.Text
{
    public class EditableTextDocument : EditableDocument<EditableTextDocument, TextDocumentDrawer>
    {
        protected internal override void ParseText(string text)
        {
        }

        protected override string Serialize()
        {
            return RawText;
        }

        protected override TextDocumentDrawer GetDocumentDrawer()
        {
            return drawerInstance ??= new TextDocumentDrawer();
        }

        protected override bool CheckIsValid(string text)
        {
            return true;
        }
    }
}
