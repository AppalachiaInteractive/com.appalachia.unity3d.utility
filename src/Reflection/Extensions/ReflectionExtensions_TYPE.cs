#region

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using Appalachia.Utility.Reflection.Common;
using Unity.Profiling;

#endregion

namespace Appalachia.Utility.Reflection.Extensions
{
    public static partial class ReflectionExtensions
    {
        #region Profiling And Tracing Markers

        private static readonly ProfilerMarker _PRF_GetByName =
            new ProfilerMarker(_PRF_PFX + nameof(GetByName));

        private static readonly ProfilerMarker _PRF_IsNullableType =
            new ProfilerMarker(_PRF_PFX + nameof(IsNullableType));

        private static readonly ProfilerMarker _PRF_GetEnumBitmask =
            new ProfilerMarker(_PRF_PFX + nameof(GetEnumBitmask));

        private static readonly ProfilerMarker _PRF_SafeIsDefined =
            new ProfilerMarker(_PRF_PFX + nameof(SafeIsDefined));

        private static readonly ProfilerMarker _PRF_CanConvert =
            new ProfilerMarker(_PRF_PFX + nameof(CanConvert));

        #endregion

        private static Dictionary<string, Type> _typeByNameLookup;

        public static bool CanConvert(this Type from, Type to)
        {
            using (_PRF_CanConvert.Auto())
            {
                if (from == null)
                {
                    throw new ArgumentNullException(nameof(from));
                }

                if (to == null)
                {
                    throw new ArgumentNullException(nameof(to));
                }

                return (from == to) ||
                       (to == typeof(object)) ||
                       (to == typeof(string)) ||
                       from.IsCastableTo(to) ||
                       (GenericNumberUtility.IsNumber(from) && GenericNumberUtility.IsNumber(to)) ||
                       (ConvertUtility.GetCastDelegate(from, to) != null);
            }
        }

        public static Type GetByName(string nameWithNamespace)
        {
            using (_PRF_GetByName.Auto())
            {
                _typeByNameLookup ??= new Dictionary<string, Type>();

                if (_typeByNameLookup.ContainsKey(nameWithNamespace))
                {
                    return _typeByNameLookup[nameWithNamespace];
                }

                foreach (var type in GetAllTypes_CACHED())
                {
                    if (nameWithNamespace == $"{type.Namespace}.{type.Name}")
                    {
                        _typeByNameLookup.Add(nameWithNamespace, type);
                        return type;
                    }
                }
            }

            _typeByNameLookup.Add(nameWithNamespace, null);
            return null;
        }

        public static ulong GetEnumBitmask(object value, Type enumType)
        {
            using (_PRF_GetEnumBitmask.Auto())
            {
                if (!enumType.IsEnum)
                {
                    throw new ArgumentException(nameof(enumType));
                }

                try
                {
                    return Convert.ToUInt64(value, CultureInfo.InvariantCulture);
                }
                catch (OverflowException)
                {
                    return (ulong) Convert.ToInt64(value, CultureInfo.InvariantCulture);
                }
            }
        }

        public static bool IsNullableType(this Type type)
        {
            using (_PRF_IsNullableType.Auto())
            {
                return !type.IsPrimitive && !type.IsValueType && !type.IsEnum;
            }
        }

        public static bool SafeIsDefined(this Assembly assembly, Type attribute, bool inherit)
        {
            using (_PRF_SafeIsDefined.Auto())
            {
                try
                {
                    return assembly.IsDefined(attribute, inherit);
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
