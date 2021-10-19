using System;
using System.Collections.Generic;
using Appalachia.CI.TextEditor.Core.Documents;

namespace Appalachia.CI.TextEditor.Core.Drawers
{
    [Serializable]
    public abstract class HierarchyDocumentDrawer<T> : DocumentDrawer<T>
        where T : EditableDocumentBase
    {
        protected Dictionary<string, bool> foldoutPaths = new();

        public bool IsExpanded(string path, bool defaultValue = false)
        {
            if (foldoutPaths == null)
            {
                foldoutPaths = new Dictionary<string, bool>();
            }

            if (!foldoutPaths.ContainsKey(path))
            {
                foldoutPaths.Add(path, defaultValue);
            }

            return foldoutPaths[path];
        }

        public void SetExpanded(string path, bool isExpanded)
        {
            if (!foldoutPaths.ContainsKey(path))
            {
                foldoutPaths.Add(path, isExpanded);
            }
            else
            {
                foldoutPaths[path] = isExpanded;
            }
        }
    }
}
