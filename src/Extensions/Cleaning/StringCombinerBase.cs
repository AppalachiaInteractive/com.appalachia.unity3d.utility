using System.Collections.Generic;
using System.Text;
using Unity.Profiling;

namespace Appalachia.Utility.Extensions.Cleaning
{
    public abstract class StringCombinerBase<T>
        where T : StringCombinerBase<T>
    {
        public delegate string ExecuteClean(T instance, string input1, string input2);

        protected StringCombinerBase(ExecuteClean action, int capacity = 100)
        {
            using (_PRF_StringCombinerBase.Auto())
            {
                builder = new StringBuilder(capacity);
                _lookup = new Dictionary<string, Dictionary<string, string>>();
                _action = action;
            }
        }

        #region Fields and Autoproperties

        public StringBuilder builder;

        private Dictionary<string, Dictionary<string, string>> _lookup;
        private ExecuteClean _action;

        #endregion

        public string Clean(string input1, string input2)
        {
            using (_PRF_Clean.Auto())
            {
                Dictionary<string, string> lookup1;

                if (_lookup.ContainsKey(input1))
                {
                    lookup1 = _lookup[input1];

                    if (lookup1.ContainsKey(input2))
                    {
                        return lookup1[input2];
                    }
                }
                else
                {
                    lookup1 = new Dictionary<string, string>();
                    _lookup.Add(input1, lookup1);
                }

                string result;

                using (_PRF_Clean_Action.Auto())
                {
                    result = _action((T) this, input1, input2);
                }

                lookup1.Add(input2, result);

                return result;
            }
        }

        #region Profiling

        private const string _PRF_PFX = nameof(StringCombinerBase<T>) + ".";

        private static readonly ProfilerMarker _PRF_Clean = new(_PRF_PFX + nameof(Clean));

        private static readonly ProfilerMarker _PRF_StringCombinerBase =
            new(_PRF_PFX + nameof(StringCombinerBase<T>));

        private static readonly ProfilerMarker _PRF_Clean_Action = new(_PRF_PFX + nameof(Clean) + ".Action");

        #endregion
    }
}
