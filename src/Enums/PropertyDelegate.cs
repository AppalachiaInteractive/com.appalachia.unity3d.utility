using System;

namespace Appalachia.Utility.Enums
{
    public sealed class PropertyDelegate<T>
    {
        public PropertyDelegate(Func<T> getter, Action<T> setter)
        {
            _getter = getter;
            _setter = setter;
        }

        #region Fields and Autoproperties

        private readonly Action<T> _setter;
        private readonly Func<T> _getter;

        #endregion

        public T Value
        {
            get => _getter();
            set => _setter(value);
        }
    }
}
