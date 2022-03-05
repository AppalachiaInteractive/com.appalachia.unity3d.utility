#region

using System;
using System.Reflection;
using Unity.Profiling;

#endregion

namespace Appalachia.Utility.Reflection.Extensions
{
    public static partial class ReflectionExtensions
    {
        private static readonly ProfilerMarker _PRF_GetBestProperty = new(_PRF_PFX + nameof(GetBestProperty));

        private static readonly ProfilerMarker _PRF_GetPropertiesCached =
            new(_PRF_PFX + nameof(GetProperties_CACHE));

        private static readonly ProfilerMarker _PRF_PopulateProperties =
            new(_PRF_PFX + nameof(PopulateProperties_INTERNAL));

        public static PropertyInfo[] GetProperties_CACHE(this Type t)
        {
            using (_PRF_GetPropertiesCached.Auto())
            {
                return t.GetProperties_CACHE(BindingFlags.Default);
            }
        }

        public static PropertyInfo[] GetProperties_CACHE(this Type t, BindingFlags flags)
        {
            using (_PRF_GetPropertiesCached.Auto())
            {
                if (_PROPERTY_CACHE_BASIC.ContainsKey(t) && _PROPERTY_CACHE_BASIC[t].TryGetValue(flags, out var result)) return result;

                PopulateProperties_INTERNAL(t, flags);

                return _PROPERTY_CACHE_BASIC[t][flags];
            }
        }

        public static PropertyInfo GetBestProperty(PropertyInfo[] candidates, string name)
        {
            using (_PRF_GetBestProperty.Auto())
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

        public static PropertyInfo GetProperty_CACHE(this Type t, string propertyName, BindingFlags flags)
        {
            using (_PRF_GetPropertiesCached.Auto())
            {
                if (_PROPERTY_CACHE.ContainsKey(t) &&
                    _PROPERTY_CACHE[t].ContainsKey(flags) &&
                    _PROPERTY_CACHE[t][flags].ContainsKey(propertyName))
                {
                    return _PROPERTY_CACHE[t][flags][propertyName];
                }

                PopulateProperties_INTERNAL(t, flags);

                return _PROPERTY_CACHE[t][flags][propertyName];
            }
        }
    }
}
