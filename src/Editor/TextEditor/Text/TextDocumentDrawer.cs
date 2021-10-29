using Appalachia.Utility.TextEditor.Core.Drawers;
using UnityEditor;

namespace Appalachia.Utility.TextEditor.Text
{
    public class TextDocumentDrawer : DocumentDrawer<EditableTextDocument>
    {
        protected override void DrawDocument(EditableTextDocument document)
        {
            document.RawText = EditorGUILayout.TextArea(document.RawText);
        }

        [UnityEditor.MenuItem(PKG.Menu.Assets.Base + "Create/Text/Text", priority = 200)]
        public static void CreateNew()
        {
            CreateNewFile("\n", "txt");
        }
    }
}
