using System;
using System.IO;
using Appalachia.CI.TextEditor.Core.Drawers;
using Appalachia.CI.TextEditor.Text;
using UnityEngine;

namespace Appalachia.CI.TextEditor.Core.Documents
{
    [Serializable]
    public abstract class EditableDocumentBase : ScriptableObject, IDisposable
    {
        private TextDocumentDrawer _defaultDrawer = new();
        private FileInfo _fileInfo;
        private bool _inTextMode;

        private DocumentStateData _loadState = new();
        private DocumentStateData _parseState = new();
        private string _path;
        private string _rawText;
        private DocumentStateData _saveState = new();
        private DocumentStateData _validationState = new();

        public DocumentStateData LoadState => _loadState;
        public DocumentStateData ParseState => _parseState;
        public DocumentStateData ValidationState => _validationState;
        public DocumentStateData SaveState => _saveState;

        protected TextDocumentDrawer defaultDrawer => _defaultDrawer ??= new TextDocumentDrawer();

        public string Path => _path;
        public FileInfo FileInfo => _fileInfo;

        public string RawText
        {
            get => _rawText;
            set => _rawText = value;
        }

        public bool InTextMode => _inTextMode;

        public void Dispose()
        {
            _path = null;
            _fileInfo = null;
            _rawText = null;
            ResetFileState();

            Dispose(true);
        }

        protected internal abstract void ParseText(string text);

        private void ResetFileState()
        {
            LoadState.Reset();
            ParseState.Reset();
            ValidationState.Reset();
            SaveState.ResetNotPending();
        }

        protected internal void Initialize(string path)
        {
            _path = path;
            _fileInfo = new FileInfo(_path);
            ResetFileState();

            if (!LoadState.pending)
            {
                return;
            }

            try
            {
                _rawText = File.ReadAllText(path);
                LoadState.Success();
            }
            catch (Exception ex)
            {
                LoadState.Error(ex);
            }
        }

        internal void ReloadFromFile()
        {
            Initialize(Path);

            if (!ParseState.pending)
            {
                return;
            }

            try
            {
                ParseText(RawText);
                ParseState.Success();
            }
            catch (Exception ex)
            {
                ParseState.Error(ex);
            }
        }

        protected abstract bool CheckIsValid(string text);

        internal bool IsValid()
        {
            if (!ValidationState.pending)
            {
                return !ValidationState.failed;
            }

            try
            {
                if (CheckIsValid(RawText))
                {
                    ValidationState.Success();
                }
                else
                {
                    ValidationState.Failed();
                }
            }
            catch (Exception ex)
            {
                ValidationState.Error(ex);
            }

            return !ValidationState.failed;
        }

        protected abstract string Serialize();

        internal void Save()
        {
            try
            {
                if (!InTextMode)
                {
                    _rawText = Serialize();
                }

                File.WriteAllText(Path, RawText);
                GUI.FocusControl("");
                ReloadFromFile();

                SaveState.Success();
            }
            catch (Exception ex)
            {
                SaveState.Error(ex);
            }
        }

        protected abstract void Dispose(bool isDisposing);

        public void ToggleTextMode()
        {
            _inTextMode = !_inTextMode;
        }

        public void SetTextMode(bool inTextMode)
        {
            _inTextMode = inTextMode;
        }

        public void Draw()
        {
            var drawer = GetDrawer();

            drawer.Draw(this);

            if (drawer.DidChange())
            {
                SaveState.pending = true;
            }
        }

        protected abstract DrawerBase GetDrawer();
    }
}
