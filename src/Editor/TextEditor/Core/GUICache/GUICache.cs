using System;

namespace Appalachia.CI.TextEditor.Core.GUICache
{
    [Serializable]
    public abstract class GUICache<T>
    {
        [NonSerialized] private T _cached;
        [NonSerialized] private T _initial;

        protected GUICache(T initial)
        {
            _initial = initial;
            _cached = initial;
        }

        public T Value => _cached ??= _initial ?? Default();

        public bool IsSet => _cached != null;

        public void Set(T value)
        {
            _cached = value;
        }

        public abstract T Default();
    }
}
