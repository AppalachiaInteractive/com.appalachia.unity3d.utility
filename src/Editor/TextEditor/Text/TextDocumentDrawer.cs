using Appalachia.CI.TextEditor.Core.Drawers;
using UnityEditor;

namespace Appalachia.CI.TextEditor.Text
{
    public class TextDocumentDrawer : DocumentDrawer<EditableTextDocument>
    {
        protected override void DrawDocument(EditableTextDocument document)
        {
            document.RawText = EditorGUILayout.TextArea(document.RawText);
        }

        [MenuItem("Assets/Create/Text/Text", priority = 200)]
        public static void CreateNew()
        {
            CreateNewFile("\n", "txt");
        }
    }
}
