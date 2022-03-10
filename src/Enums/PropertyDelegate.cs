using System;
using Appalachia.Utility.Pooling.Objects;

namespace Appalachia.Utility.Enums
{
#pragma warning disable CS0612
    public sealed class PropertyDelegate<T> : SelfPoolingObject<PropertyDelegate<T>>
#pragma warning restore CS0612
    {
        #region Fields and Autoproperties

        private Action<T> _setter;
        private Func<T> _getter;

        #endregion

        public T Value
        {
            get => _getter();
            set => _setter(value);
        }

        public static PropertyDelegate<T> Get(Func<T> getter, Action<T> setter)
        {
            var instance = Get();

            instance._getter = getter;
            instance._setter = setter;

            return instance;
        }

        public override void Initialize()
        {
            using (_PRF_Initialize.Auto())
            {
                _getter = null;
                _setter = null;
            }
        }

        public override void Reset()
        {
            using (_PRF_Reset.Auto())
            {
                _getter = null;
                _setter = null;
            }
        }
    }
}
