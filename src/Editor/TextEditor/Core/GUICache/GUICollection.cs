using System.Collections.Generic;
using UnityEngine;

namespace Appalachia.CI.TextEditor.Core.GUICache
{
    public class GUICollection
    {
        private Dictionary<string, GUIColorCache> _colorsByName = new();
        private Dictionary<string, GUIContentCache> _contentByName = new();
        private Dictionary<string, GUILayoutOptionsCache> _optionsByName = new();
        private Dictionary<string, GUIStyleCache> _styleByName = new();

        public int Count
        {
            get
            {
                Initialize();
                return _contentByName.Count +
                       _optionsByName.Count +
                       _styleByName.Count +
                       _colorsByName.Count;
            }
        }

        private void Initialize()
        {
            if (_contentByName == null)
            {
                _contentByName = new Dictionary<string, GUIContentCache>();
            }

            if (_optionsByName == null)
            {
                _optionsByName = new Dictionary<string, GUILayoutOptionsCache>();
            }

            if (_styleByName == null)
            {
                _styleByName = new Dictionary<string, GUIStyleCache>();
            }

            if (_colorsByName == null)
            {
                _colorsByName = new Dictionary<string, GUIColorCache>();
            }
        }

        public void AddContent(string name, GUIContent content)
        {
            Initialize();
            var cached = new GUIContentCache(content);
            _contentByName.Add(name, cached);
        }

        public GUIContentCache GetContent(string name)
        {
            Initialize();
            return _contentByName[name];
        }

        public void AddOptions(string name, params GUILayoutOption[] options)
        {
            Initialize();
            var cached = new GUILayoutOptionsCache(options);
            _optionsByName.Add(name, cached);
        }

        public GUILayoutOptionsCache GetOptions(string name)
        {
            Initialize();
            return _optionsByName[name];
        }

        public void AddStyle(string name, GUIStyle style)
        {
            Initialize();
            var cached = new GUIStyleCache(style);
            _styleByName.Add(name, cached);
        }

        public GUIStyleCache GetStyle(string name)
        {
            Initialize();
            return _styleByName[name];
        }

        public void AddColor(string name, Color content)
        {
            Initialize();
            var cached = new GUIColorCache(content);
            _colorsByName.Add(name, cached);
        }

        public GUIColorCache GetColor(string name)
        {
            Initialize();
            return _colorsByName[name];
        }
    }
}
