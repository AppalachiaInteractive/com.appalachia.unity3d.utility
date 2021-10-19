using System;

namespace Appalachia.CI.TextEditor.Core.Repository
{
    internal class EditableDocumentType
    {
        public Type documentType;
        public string fileExtension;

        public bool IsMatch(string extension)
        {
            if (string.IsNullOrWhiteSpace(fileExtension))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(extension))
            {
                return false;
            }

            if (string.Equals(
                fileExtension,
                extension,
                StringComparison.InvariantCultureIgnoreCase
            ))
            {
                return true;
            }

            return false;
        }
    }
}
