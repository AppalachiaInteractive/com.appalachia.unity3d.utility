using System;
using System.Collections.Generic;
using System.Text;
using Appalachia.Utility.Strings;
using Unity.Profiling;

namespace Appalachia.Utility.Reflection.Extensions
{
    public static partial class ReflectionExtensions
    {
        #region Constants and Static Readonly

        private static readonly Dictionary<string, string> TypeNameKeywordAlternatives = new()
        {
            { "Single", "float" },
            { "Double", "double" },
            { "SByte", "sbyte" },
            { "Int16", "short" },
            { "Int32", "int" },
            { "Int64", "long" },
            { "Byte", "byte" },
            { "UInt16", "ushort" },
            { "UInt32", "uint" },
            { "UInt64", "ulong" },
            { "Decimal", "decimal" },
            { "String", "string" },
            { "Char", "char" },
            { "Boolean", "bool" },
            { "Single[]", "float[]" },
            { "Double[]", "double[]" },
            { "SByte[]", "sbyte[]" },
            { "Int16[]", "short[]" },
            { "Int32[]", "int[]" },
            { "Int64[]", "long[]" },
            { "Byte[]", "byte[]" },
            { "UInt16[]", "ushort[]" },
            { "UInt32[]", "uint[]" },
            { "UInt64[]", "ulong[]" },
            { "Decimal[]", "decimal[]" },
            { "String[]", "string[]" },
            { "Char[]", "char[]" },
            { "Boolean[]", "bool[]" }
        };

        #endregion

        #region Static Fields and Autoproperties

        private static readonly ProfilerMarker _PRF_GetReadableName =
            new ProfilerMarker(_PRF_PFX + nameof(GetReadableName));

        private static readonly ProfilerMarker _PRF_GetReadableFullName =
            new ProfilerMarker(_PRF_PFX + nameof(GetReadableFullName));

        private static Dictionary<Type, string> _GetSimpleReadableNameLookup;

        private static readonly ProfilerMarker _PRF_GetSimpleReadableName =
            new ProfilerMarker(_PRF_PFX + nameof(GetSimpleReadableName));

        private static Dictionary<Type, string> _GetSimpleReadableFullNameLookup;

        #endregion

        public static string GetReadableFullName(this Type type)
        {
            using (_PRF_GetReadableFullName.Auto())
            {
                if (type.IsNested && !type.IsGenericParameter)
                {
                    return ZString.Format(
                        "{0}.{1}",
                        GetReadableFullName(type.DeclaringType),
                        GetCachedReadableName(type)
                    );
                }

                var str = GetCachedReadableName(type);
                if (type.Namespace != null)
                {
                    str = ZString.Format("{0}.{1}", type.Namespace, str);
                }

                return str;
            }
        }

        public static string GetReadableName(this Type type)
        {
            using (_PRF_GetReadableName.Auto())
            {
                return type.IsNested && !type.IsGenericParameter
                    ? ZString.Format(
                        "{0}.{1}",
                        type.DeclaringType.GetReadableName(),
                        GetCachedReadableName(type)
                    )
                    : GetCachedReadableName(type);
            }
        }

        public static string GetSimpleReadableFullName(this Type type)
        {
            using (_PRF_GetSimpleReadableFullName.Auto())
            {
                _GetSimpleReadableFullNameLookup ??= new Dictionary<Type, string>();

                if (_GetSimpleReadableNameLookup.ContainsKey(type))
                {
                    return _GetSimpleReadableFullNameLookup[type];
                }

                var result = type.GetReadableFullName().Replace('<', '_').Replace('>', '_').TrimEnd('_');

                _GetSimpleReadableFullNameLookup.Add(type, result);

                return result;
            }
        }

        public static string GetSimpleReadableName(this Type type)
        {
            using (_PRF_GetSimpleReadableName.Auto())
            {
                _GetSimpleReadableNameLookup ??= new Dictionary<Type, string>();

                if (_GetSimpleReadableNameLookup.ContainsKey(type))
                {
                    return _GetSimpleReadableNameLookup[type];
                }

                var result = type.GetReadableName().Replace('<', '_').Replace('>', '_').TrimEnd('_');

                _GetSimpleReadableNameLookup.Add(type, result);

                return result;
            }
        }

        private static string CalculateReadableName(Type type)
        {
            using (_PRF_CalculateReadableName.Auto())
            {
                if (type.IsArray)
                {
                    var arrayRank = type.GetArrayRank();
                    return type.GetElementType().GetReadableName() + (arrayRank == 1 ? "[]" : "[,]");
                }

                if (type.InheritsFrom(typeof(Nullable<>)))
                {
                    return ZString.Format("{0}?", CalculateReadableName(type.GetGenericArguments()[0]));
                }

                if (type.IsByRef)
                {
                    return ZString.Format("ref {0}", CalculateReadableName(type.GetElementType()));
                }

                if (type.IsGenericParameter || !type.IsGenericType)
                {
                    return type.GetAlternateTypeNames();
                }

                var stringBuilder = new StringBuilder();
                var name = type.Name;
                var length = name.IndexOf("`");
                stringBuilder.Append(length != -1 ? name.Substring(0, length) : name);

                stringBuilder.Append('<');
                var genericArguments = type.GetGenericArguments();
                for (var index = 0; index < genericArguments.Length; ++index)
                {
                    var type1 = genericArguments[index];
                    if (index != 0)
                    {
                        stringBuilder.Append(", ");
                    }

                    stringBuilder.Append(type1.GetReadableName());
                }

                stringBuilder.Append('>');
                return stringBuilder.ToString();
            }
        }

        private static string GetAlternateTypeNames(this Type type)
        {
            using (_PRF_GetAlternateTypeNames.Auto())
            {
                var key = type.Name;
                string empty;
                if (TypeNameKeywordAlternatives.TryGetValue(key, out empty))
                {
                    key = empty;
                }

                return key;
            }
        }

        private static string GetCachedReadableName(Type type)
        {
            using (_PRF_GetCachedReadableName.Auto())
            {
                if (!READABLE_NAMES_CACHE.ContainsKey(type))
                {
                    CheckInitialization(type);
                }

                if (!READABLE_NAMES_CACHE.ContainsKey(type))
                {
                    PreCalculateReadableNames(type);
                }

                return READABLE_NAMES_CACHE[type];
            }
        }

        private static void PreCalculateReadableNames(Type type)
        {
            using (_PRF_PreCalculateReadableNames.Auto())
            {
                string readableName;

                lock (READABLE_NAME_CACHE_LOCK)
                {
                    if (READABLE_NAMES_CACHE.ContainsKey(type))
                    {
                        return;
                    }

                    readableName = CalculateReadableName(type);
                    READABLE_NAMES_CACHE.Add(type, readableName);
                }
            }
        }

        #region Profiling

        private static readonly ProfilerMarker _PRF_GetSimpleReadableFullName =
            new ProfilerMarker(_PRF_PFX + nameof(GetSimpleReadableFullName));

        private static readonly ProfilerMarker _PRF_CalculateReadableName =
            new ProfilerMarker(_PRF_PFX + nameof(CalculateReadableName));

        private static readonly ProfilerMarker _PRF_GetAlternateTypeNames =
            new ProfilerMarker(_PRF_PFX + nameof(GetAlternateTypeNames));

        private static readonly ProfilerMarker _PRF_GetCachedReadableName =
            new ProfilerMarker(_PRF_PFX + nameof(GetCachedReadableName));

        private static readonly ProfilerMarker _PRF_PreCalculateReadableNames =
            new ProfilerMarker(_PRF_PFX + nameof(PreCalculateReadableNames));

        #endregion
    }
}
