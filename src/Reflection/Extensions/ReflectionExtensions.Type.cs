#region

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using Appalachia.Utility.Reflection.Common;
using Appalachia.Utility.Strings;
using Unity.Profiling;

#endregion

namespace Appalachia.Utility.Reflection.Extensions
{
    public static partial class ReflectionExtensions
    {
        #region Constants and Static Readonly

        private const string TYPE_WITH_NAMESPACE_FORMAT_STRING = "{0}.{1}";

        #endregion

        #region Static Fields and Autoproperties

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

        private static Dictionary<string, Type> _typeByNameLookup;
        private static HashSet<Type> _typesInNameLookup;

        private static Utf8PreparedFormat<string, string> _typeWithNamespaceFormat;

        #endregion

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

        public static Type GetByName(string typeNamespace, string typeName)
        {
            using (_PRF_GetByName.Auto())
            {
                _typeWithNamespaceFormat ??=
                    new Utf8PreparedFormat<string, string>(TYPE_WITH_NAMESPACE_FORMAT_STRING);

                var nameWithNamespace = _typeWithNamespaceFormat.Format(typeNamespace, typeName);

                var types = GetAllTypes_CACHED();

                _typeByNameLookup ??= new Dictionary<string, Type>(types.Length);
                _typesInNameLookup ??= new();

                if (_typeByNameLookup.TryGetValue(nameWithNamespace, out var result)) return result;

                foreach (var type in types)
                {
                    if (_typesInNameLookup.Contains(type))
                    {
                        continue;
                    }

                    _typesInNameLookup.Add(type);

                    var formattedNameWithNamespace =
                        _typeWithNamespaceFormat.Format(type.Namespace, type.Name);

                    if (_typeByNameLookup.ContainsKey(formattedNameWithNamespace))
                    {
                        continue;
                    }

                    _typeByNameLookup.Add(formattedNameWithNamespace, type);

                    if (formattedNameWithNamespace != nameWithNamespace)
                    {
                        continue;
                    }

                    result = type;
                    break;
                }

                return result;
            }
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
                    return (ulong)Convert.ToInt64(value, CultureInfo.InvariantCulture);
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

        #region Profiling

        #endregion
    }
}
