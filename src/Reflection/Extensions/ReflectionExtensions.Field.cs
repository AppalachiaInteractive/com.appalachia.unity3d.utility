#region

using System;
using System.Reflection;
using Appalachia.Utility.Constants;
using Appalachia.Utility.Logging;
using Appalachia.Utility.Strings;
using Unity.Profiling;

#endregion

namespace Appalachia.Utility.Reflection.Extensions
{
    public static partial class ReflectionExtensions
    {
        public static FieldInfo GetBestField(FieldInfo[] candidates, string name)
        {
            using (_PRF_GetBestField.Auto())
            {
                for (var i = 0; i < candidates.Length; i++)
                {
                    var candidate = candidates[i];

                    if (candidate.Name == name)
                    {
                        return candidate;
                    }
                }

                return null;
            }
        }

        public static FieldInfo GetField_CACHE(this Type t, string fieldName, BindingFlags flags)
        {
            using (_PRF_GetFieldsCached.Auto())
            {
                if (_FIELD_CACHE.ContainsKey(t) &&
                    _FIELD_CACHE[t].ContainsKey(flags) &&
                    _FIELD_CACHE[t][flags].ContainsKey(fieldName))
                {
                    return _FIELD_CACHE[t][flags][fieldName];
                }

                PopulateFields_INTERNAL(t, flags);

                if (_FIELD_CACHE[t][flags].ContainsKey(fieldName))
                {
                    return _FIELD_CACHE[t][flags][fieldName];
                }

                AppaLog.Context.Extensions.Error(
                    ZString.Format(
                        "Could not find field {0} on type {1}.",
                        fieldName.FormatFieldForLogging(),
                        t.FormatForLogging()
                    )
                );

                return null;
            }
        }

        public static FieldInfo[] GetFields_CACHE(this Type t)
        {
            using (_PRF_GetFieldsCached.Auto())
            {
                return t.GetFields_CACHE(BindingFlags.Default);
            }
        }

        public static FieldInfo[] GetFields_CACHE(this Type t, BindingFlags flags)
        {
            using (_PRF_GetFieldsCached.Auto())
            {
                if (_FIELD_CACHE_BASIC.ContainsKey(t) && _FIELD_CACHE_BASIC[t].ContainsKey(flags))
                {
                    return _FIELD_CACHE_BASIC[t][flags];
                }

                PopulateFields_INTERNAL(t, flags);

                return _FIELD_CACHE_BASIC[t][flags];
            }
        }

        #region Profiling

        private static readonly ProfilerMarker _PRF_GetBestField = new(_PRF_PFX + nameof(GetBestField));

        private static readonly ProfilerMarker _PRF_GetFieldsCached = new(_PRF_PFX + nameof(GetFields_CACHE));

        private static readonly ProfilerMarker _PRF_PopulateFields =
            new(_PRF_PFX + nameof(PopulateFields_INTERNAL));

        #endregion
    }
}
