using System;
using Appalachia.Utility.TextEditor.Core.Documents;
using Appalachia.Utility.TextEditor.Core.Repository;
using UnityEditor;
using UnityEngine;

namespace Appalachia.Utility.TextEditor
{
// Adds a nice editor to edit JSON files as well as a simple text editor incase
// the editor doesn't support the types you need. It works with strings, floats
// ints and bools at the moment.
// 
// * Requires the latest version of JSON.net compatible with Unity

//If you want to edit a JSON file in the "StreammingAssets" Folder change this to DefaultAsset.
//Hacky solution to a weird problem :/
    [CustomEditor(typeof(TextAsset), true)]
    public class TextEditor : Editor
    {
        private const int kMaxChars = 7000;
        private EditableDocumentBase _document;
        private TextAsset m_TextAsset;
        [NonSerialized] private GUIStyle m_TextStyle;

        private void OnEnable()
        {
            m_TextAsset = target as TextAsset;

            var path = AssetDatabase.GetAssetPath(target);

            _document = DocumentTypeRepository.Load(path);
        }

        private void OnDisable()
        {
            _document.Dispose();
        }

        public override void OnInspectorGUI()
        {
            if (m_TextStyle == null)
            {
                m_TextStyle = "ScriptText";
            }

            var enabled = GUI.enabled;

            GUI.enabled = true;

            _document.Draw();

            GUI.enabled = enabled;
        }
    }
}
