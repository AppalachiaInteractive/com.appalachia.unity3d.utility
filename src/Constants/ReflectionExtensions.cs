using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Appalachia.Utility.Strings;
using Unity.Profiling;

namespace Appalachia.Utility.Constants
{
    internal static class ReflectionExtensions
    {
        #region Constants and Static Readonly

        public const int TYPE_ESTIMATE = 1000;

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

        private static Dictionary<Type, string> _GetSimpleReadableFullNameLookup;

        private static Dictionary<Type, string> _GetSimpleReadableNameLookup;
        private static Dictionary<Type, string> READABLE_NAMES_CACHE = new();

        private static object READABLE_NAME_CACHE_LOCK = new();

        #endregion

        public static string GetReadableName(this Type type)
        {
            using (_PRF_GetReadableName.Auto())
            {
                return type.IsNested && !type.IsGenericParameter
                    ? ZString.Format("{0}.{1}", type.DeclaringType.GetReadableName(), GetCachedReadableName(type))
                    : GetCachedReadableName(type);
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
                InitializeConstantsAndCollections();

                if (!READABLE_NAMES_CACHE.ContainsKey(type))
                {
                    PreCalculateReadableNames(type);
                }

                return READABLE_NAMES_CACHE[type];
            }
        }

        private static bool InheritsFrom(this Type type, Type baseType)
        {
            using (_PRF_InheritsFrom.Auto())
            {
                if (baseType.IsAssignableFrom(type))
                {
                    return true;
                }

                if (type.IsInterface && !baseType.IsInterface)
                {
                    return false;
                }

                if (baseType.IsInterface)
                {
                    return type.GetInterfaces().Contains(baseType);
                }

                for (var type1 = type; type1 != null; type1 = type1.BaseType)
                {
                    if ((type1 == baseType) ||
                        (baseType.IsGenericTypeDefinition &&
                         type1.IsGenericType &&
                         (type1.GetGenericTypeDefinition() == baseType)))
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        private static void InitializeConstantsAndCollections()
        {
            using (_PRF_InitializeConstantsAndCollections.Auto())
            {
                READABLE_NAMES_CACHE ??= new Dictionary<Type, string>(TYPE_ESTIMATE);

                READABLE_NAME_CACHE_LOCK ??= new object();
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

        private const string _PRF_PFX = nameof(ReflectionExtensions) + ".";

        private static readonly ProfilerMarker _PRF_InitializeConstantsAndCollections =
            new ProfilerMarker(_PRF_PFX + nameof(InitializeConstantsAndCollections));

        private static readonly ProfilerMarker _PRF_InheritsFrom = new ProfilerMarker(_PRF_PFX + nameof(InheritsFrom));

        private static readonly ProfilerMarker _PRF_GetReadableName =
            new ProfilerMarker(_PRF_PFX + nameof(GetReadableName));

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
