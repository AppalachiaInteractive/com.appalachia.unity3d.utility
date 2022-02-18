namespace Appalachia.Utility.TextEditor.Core.GUICache
{
    public abstract class GUICache<T>
    {
        protected GUICache(T initial)
        {
            _initial = initial;
            _cached = initial;
        }

        #region Fields and Autoproperties

        private T _cached;
        private T _initial;

        #endregion

        public bool IsSet => _cached != null;

        public T Value => _cached ??= _initial ?? Default();

        public abstract T Default();

        public void Set(T value)
        {
            _cached = value;
        }
    }
}
