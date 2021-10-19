using System;
using System.Collections.Generic;
using System.IO;
using Appalachia.CI.TextEditor.Core.Documents;
using Appalachia.CI.TextEditor.JSON;
using Appalachia.CI.TextEditor.Text;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace Appalachia.CI.TextEditor.Core.Repository
{
    [InitializeOnLoad]
    public static class DocumentTypeRepository
    {
        private static readonly List<EditableDocumentType> EditableDocumentTypes;

        static DocumentTypeRepository()
        {
            EditableDocumentTypes = new List<EditableDocumentType>();
            RegisterExtension<EditableJsonDocument>("json");
        }

        public static EditableDocumentBase Load(string path)
        {
            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
            {
                return null;
            }

            var documentType = GetDocumentType(path);

            var document = ScriptableObject.CreateInstance(documentType) as EditableDocumentBase;

            document.Initialize(path);
            document.ParseText(document.RawText);

            return document;
        }

        public static void RegisterExtension<T>([NotNull] string extension)
            where T : EditableDocumentBase
        {
            if (extension == null)
            {
                throw new ArgumentNullException(nameof(extension));
            }

            if (string.IsNullOrWhiteSpace(extension))
            {
                throw new ArgumentException(
                    "Value cannot be null or whitespace.",
                    nameof(extension)
                );
            }

            extension = extension.Trim('.');

            if (string.IsNullOrWhiteSpace(extension))
            {
                throw new ArgumentException("Value cannot be a period.", nameof(extension));
            }

            var type =
                new EditableDocumentType {fileExtension = extension, documentType = typeof(T)};

            EditableDocumentTypes.Add(type);
        }

        private static Type GetDocumentType([NotNull] string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(path));
            }

            var extension = Path.GetExtension(path).Trim('.');

            // reverse order to prioritize user document handlers.
            for (var index = EditableDocumentTypes.Count - 1; index >= 0; index--)
            {
                var documentType = EditableDocumentTypes[index];
                if (documentType.IsMatch(extension))
                {
                    return documentType.documentType;
                }
            }

            return typeof(EditableTextDocument);
        }
    }
}
