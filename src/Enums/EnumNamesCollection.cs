using System;
using System.Collections.Generic;
using Unity.Profiling;

namespace Appalachia.Utility.Enums
{
    public class EnumNamesCollection<T>
        where T : Enum
    {
        public EnumNamesCollection()
        {
            Initialize();
        }

        #region Fields and Autoproperties

        private Dictionary<T, string> _lookup;

        #endregion

        public string this[T t] => GetName(t);

        public string GetName(T value)
        {
            using (_PRF_GetName.Auto())
            {
                Initialize();

                return _lookup[value];
            }
        }

        private void Initialize()
        {
            using (_PRF_Initialize.Auto())
            {
                if (_lookup == null)
                {
                    _lookup = new Dictionary<T, string>();

                    _lookup.PopulateEnumKeys(t => t.ToString(), clear: false);
                }
            }
        }

        #region Profiling

        private const string _PRF_PFX = nameof(EnumNamesCollection<T>) + ".";
        private static readonly ProfilerMarker _PRF_GetName = new ProfilerMarker(_PRF_PFX + nameof(GetName));

        private static readonly ProfilerMarker _PRF_Initialize =
            new ProfilerMarker(_PRF_PFX + nameof(Initialize));

        #endregion
    }
}
