using System.Collections.Generic;
using Appalachia.Utility.Strings;
using Unity.Profiling;

namespace Appalachia.Utility.Extensions.Cleaning
{
    public abstract class StringCleanerBase<T>
        where T : StringCleanerBase<T>
    {
        public delegate string ExecuteClean(T instance, string input);

        protected StringCleanerBase(ExecuteClean action)
        {
            using (_PRF_StringCleanerBase.Auto())
            {
                _builder = new Utf16ValueStringBuilder(false);
                _lookup = new Dictionary<string, string>();
                _action = action;
            }
        }

        #region Fields and Autoproperties

        private Dictionary<string, string> _lookup;
        private Utf16ValueStringBuilder _builder;
        private ExecuteClean _action;

        #endregion

        public string Clean(string input)
        {
            using (_PRF_Clean.Auto())
            {
                if (_lookup.TryGetValue(input, out var result)) return result;

                using (_PRF_Clean_Action.Auto())
                {
                    result = _action((T) this, input);
                }

                _lookup.Add(input, result);

                return result;
            }
        }

        #region Profiling

        private const string _PRF_PFX = nameof(StringCleanerBase<T>) + ".";

        private static readonly ProfilerMarker _PRF_Clean = new(_PRF_PFX + nameof(Clean));

        private static readonly ProfilerMarker _PRF_StringCleanerBase =
            new(_PRF_PFX + nameof(StringCleanerBase<T>));

        private static readonly ProfilerMarker _PRF_Clean_Action = new(_PRF_PFX + nameof(Clean) + ".Action");

        #endregion
    }
}
