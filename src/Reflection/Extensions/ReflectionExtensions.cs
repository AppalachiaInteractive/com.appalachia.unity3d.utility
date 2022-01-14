#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Appalachia.Utility.Strings;
using Unity.Profiling;
using UnityEngine;

#endregion

namespace Appalachia.Utility.Reflection.Extensions
{
    [DefaultExecutionOrder(-10000)]
    public static partial class ReflectionExtensions
    {
        #region Constants and Static Readonly

        public const BindingFlags All = AllStatic | AllInstance;

        public const BindingFlags AllInstance = PrivateInstance | PublicInstance;

        public const BindingFlags AllStatic = PublicStatic | PrivateStatic;

        public const BindingFlags NonInheritedAllInstance =
            NonInheritedPrivateInstance | NonInheritedPublicInstance;

        public const BindingFlags NonInheritedPrivateInstance = PrivateInstance | BindingFlags.DeclaredOnly;

        public const BindingFlags NonInheritedPublicInstance = PublicInstance | BindingFlags.DeclaredOnly;

        public const BindingFlags PrivateInstance = BindingFlags.NonPublic | BindingFlags.Instance;

        public const BindingFlags PrivateStatic = BindingFlags.NonPublic | BindingFlags.Static;

        public const BindingFlags PublicInstance = BindingFlags.Public | BindingFlags.Instance;

        public const BindingFlags PublicStatic = BindingFlags.Public | BindingFlags.Static;
        public const int ASSEMBLY_ESTIMATE = 500;
        public const int TYPE_AMOUNT = 60000;
        public const int TYPE_ESTIMATE = 1000;

        #endregion

        #region Static Fields and Autoproperties

        private static Assembly[] _ASSEMBLIES_CACHE;
        private static BindingFlags[] _baseFlags;
        private static bool _initializedCaches;
        private static bool _initializingCaches;
        private static Dictionary<Assembly, Type[]> _ASSEMBLY_TYPE_CACHE;
        private static Dictionary<MemberInfo, bool> _MEMBER_STATIC_LOOKUP_CACHE;
        private static Dictionary<MemberInfo, Dictionary<bool, Attribute[]>> _ATTRIBUTE_BASE_CACHE = new();

        private static Dictionary<MemberInfo, Dictionary<Type, Dictionary<bool, Attribute[]>>>
            _ATTRIBUTE_CACHE = new();

        private static Dictionary<Type, Dictionary<BindingFlags, Dictionary<string, FieldInfo>>>
            _FIELD_CACHE = new();

        private static Dictionary<Type, Dictionary<BindingFlags, Dictionary<string, MethodInfo[]>>>
            _METHOD_CACHE = new();

        private static Dictionary<Type, Dictionary<BindingFlags, Dictionary<string, PropertyInfo>>>
            _PROPERTY_CACHE = new();

        private static Dictionary<Type, Dictionary<BindingFlags, FieldInfo[]>> _FIELD_CACHE_BASIC = new();
        private static Dictionary<Type, Dictionary<BindingFlags, MethodInfo[]>> _METHOD_CACHE_BASIC = new();

        private static Dictionary<Type, Dictionary<BindingFlags, PropertyInfo[]>> _PROPERTY_CACHE_BASIC =
            new();

        private static Dictionary<Type, string> READABLE_NAMES_CACHE = new();

        private static HashSet<Type> _POPULATED_TYPES_CACHE;
        private static object READABLE_NAME_CACHE_LOCK = new();
        private static Type[] _ALL_TYPES_CACHE;

        private static readonly ProfilerMarker _PRF_InitializeCaches =
            new(_PRF_PFX + nameof(InitializeCaches));

        private static readonly ProfilerMarker _PRF_InitializeConstantsAndCollections =
            new(_PRF_PFX + nameof(InitializeConstantsAndCollections));

        private static readonly ProfilerMarker _PRF_InitializeAllTypesCache =
            new(_PRF_PFX + nameof(InitializeAllTypesCache));

        private static readonly ProfilerMarker _PRF_InitializeAssemblyTypeCache =
            new(_PRF_PFX + nameof(InitializeAssemblyTypeCache));

        private static readonly ProfilerMarker _PRF_GetAssemblies =
            new(_PRF_PFX + nameof(GetAssemblies_CACHED));

        private static readonly ProfilerMarker _PRF_GetAllTypes = new(_PRF_PFX + nameof(GetAllTypes_CACHED));
        private static readonly ProfilerMarker _PRF_SafeGetTypes = new(_PRF_PFX + nameof(GetTypes_CACHED));

        private static readonly ProfilerMarker _PRF_IsStatic_INTERNAL =
            new(_PRF_PFX + nameof(IsStatic_INTERNAL));

        private static readonly ProfilerMarker _PRF_PopulateMethods_INTERNAL =
            new(_PRF_PFX + nameof(PopulateMethods_INTERNAL));

        private static readonly ProfilerMarker _PRF_GetTypesWithAttribute_CACHED =
            new ProfilerMarker(_PRF_PFX + nameof(GetTypesWithAttribute_CACHED));

        private static Dictionary<Type, Type[]> _typesByAttributeLookup;

        private static Type[] _appalachiaTypes;

        #endregion

        public static Type[] GetAllTypes_CACHED()
        {
            using (_PRF_GetAllTypes.Auto())
            {
                if (!_initializedCaches && !_initializingCaches)
                {
                    InitializeCaches();
                }

                InitializeAllTypesCache();

                return _ALL_TYPES_CACHE;
            }
        }

        public static Type[] GetAppalachiaTypes_CACHED()
        {
            using (_PRF_GetAppalachiaTypes_CACHED.Auto())
            {
                if ((_appalachiaTypes == null) || (_appalachiaTypes.Length == 0))
                {
                    _appalachiaTypes = GetAppalachiaTypesInternal().ToArray();
                }

                return _appalachiaTypes;
            }
        }

        public static Assembly[] GetAssemblies_CACHED()
        {
            using (_PRF_GetAssemblies.Auto())
            {
                if (!_initializedCaches && !_initializingCaches)
                {
                    InitializeCaches();
                }

                return _ASSEMBLIES_CACHE;
            }
        }

        public static Type[] GetTypes_CACHED(this Assembly assembly)
        {
            using (_PRF_SafeGetTypes.Auto())
            {
                if (!_initializedCaches && !_initializingCaches)
                {
                    InitializeCaches();
                }

                try
                {
                    return _ASSEMBLY_TYPE_CACHE[assembly];
                }
                catch
                {
                    return Type.EmptyTypes;
                }
            }
        }

        public static Type[] GetTypesWithAttribute_CACHED<T>()
            where T : Attribute
        {
            using (_PRF_GetTypesWithAttribute_CACHED.Auto())
            {
                _typesByAttributeLookup ??= new Dictionary<Type, Type[]>();
                var attributeType = typeof(T);

                if (_typesByAttributeLookup.ContainsKey(attributeType))
                {
                    return _typesByAttributeLookup[attributeType];
                }

                var results = new List<Type>();
                var types = GetAllTypes_CACHED();

                foreach (var type in types)
                {
                    var atty = type.GetAttribute_CACHE<T>();

                    if (atty != null)
                    {
                        results.Add(type);
                    }
                }

                var resultArray = results.ToArray();
                _typesByAttributeLookup.Add(attributeType, resultArray);

                return resultArray;
            }
        }

        private static void CheckInitialization(Type t)
        {
            if (!_initializedCaches && !_initializingCaches)
            {
                InitializeCaches();
            }

            if (!_METHOD_CACHE_BASIC.ContainsKey(t))
            {
                _METHOD_CACHE_BASIC.Add(t, new Dictionary<BindingFlags, MethodInfo[]>());
            }

            if (!_METHOD_CACHE.ContainsKey(t))
            {
                _METHOD_CACHE.Add(t, new Dictionary<BindingFlags, Dictionary<string, MethodInfo[]>>());
            }

            if (!_FIELD_CACHE_BASIC.ContainsKey(t))
            {
                _FIELD_CACHE_BASIC.Add(t, new Dictionary<BindingFlags, FieldInfo[]>());
            }

            if (!_FIELD_CACHE.ContainsKey(t))
            {
                _FIELD_CACHE.Add(t, new Dictionary<BindingFlags, Dictionary<string, FieldInfo>>());
            }

            if (!_PROPERTY_CACHE_BASIC.ContainsKey(t))
            {
                _PROPERTY_CACHE_BASIC.Add(t, new Dictionary<BindingFlags, PropertyInfo[]>());
            }

            if (!_PROPERTY_CACHE.ContainsKey(t))
            {
                _PROPERTY_CACHE.Add(t, new Dictionary<BindingFlags, Dictionary<string, PropertyInfo>>());
            }

            /*t.PopulateType_INTERNAL();*/
        }

        private static IEnumerable<Type> GetAppalachiaTypesInternal()
        {
            using (_PRF_GetAppalachiaTypesInternal.Auto())
            {
                var assemblies = GetAssemblies_CACHED();

                foreach (var assembly in assemblies)
                {
                    var shortName = assembly.GetName().Name;

                    if (!shortName.StartsWith("Appalachia"))
                    {
                        continue;
                    }

                    var assemblyTypes = assembly.GetTypes_CACHED();

                    for (var typeIndex = 0; typeIndex < assemblyTypes.Length; typeIndex++)
                    {
                        var type = assemblyTypes[typeIndex];
                        yield return type;
                    }
                }
            }
        }

        private static void InitializeAllTypesCache()
        {
            using (_PRF_InitializeAllTypesCache.Auto())
            {
                if (_ALL_TYPES_CACHE == null)
                {
                    var allTypes = new List<Type>(TYPE_AMOUNT);

                    var assemblies = GetAssemblies_CACHED();

                    for (var i = 0; i < assemblies.Length; i++)
                    {
                        var assembly = assemblies[i];

                        InitializeAssemblyTypeCache(assembly, allTypes);
                    }

                    _ALL_TYPES_CACHE = allTypes.ToArray();
                }
            }
        }

        private static void InitializeAssemblyTypeCache(Assembly assembly, List<Type> allTypes)
        {
            using (_PRF_InitializeAssemblyTypeCache.Auto())
            {
                if (_ASSEMBLY_TYPE_CACHE.ContainsKey(assembly))
                {
                    return;
                }

                var types = assembly.GetTypes();

                _ASSEMBLY_TYPE_CACHE.Add(assembly, types);

                allTypes.AddRange(types);
            }
        }

#if UNITY_EDITOR
        [UnityEditor.InitializeOnLoadMethod]
#else
        [RuntimeInitializeOnLoadMethod]
#endif
        private static void InitializeCaches()
        {
            using (_PRF_InitializeCaches.Auto())
            {
                if (_initializingCaches || _initializedCaches)
                {
                    return;
                }

                _initializingCaches = true;

                InitializeConstantsAndCollections();

                _initializedCaches = true;
                _initializingCaches = false;
            }
        }

        private static void InitializeConstantsAndCollections()
        {
            using (_PRF_InitializeConstantsAndCollections.Auto())
            {
                _baseFlags = new[]
                {
                    BindingFlags.Default,
                    PrivateInstance,
                    PublicInstance,
                    AllInstance,
                    NonInheritedPrivateInstance,
                    NonInheritedPublicInstance,
                    NonInheritedAllInstance,
                    PrivateStatic,
                    PublicStatic,
                    AllStatic,
                    All
                };

                if (_POPULATED_TYPES_CACHE == null)
                {
                    _POPULATED_TYPES_CACHE = new HashSet<Type>(TYPE_ESTIMATE);
                }

                if (_ASSEMBLIES_CACHE == null)
                {
                    _ASSEMBLIES_CACHE = AppDomain.CurrentDomain.GetAssemblies();
                }

                if (_ASSEMBLY_TYPE_CACHE == null)
                {
                    _ASSEMBLY_TYPE_CACHE = new Dictionary<Assembly, Type[]>(ASSEMBLY_ESTIMATE);
                }

                if (_ATTRIBUTE_CACHE == null)
                {
                    _ATTRIBUTE_CACHE =
                        new Dictionary<MemberInfo, Dictionary<Type, Dictionary<bool, Attribute[]>>>(
                            TYPE_ESTIMATE
                        );
                }

                if (_FIELD_CACHE_BASIC == null)
                {
                    _FIELD_CACHE_BASIC =
                        new Dictionary<Type, Dictionary<BindingFlags, FieldInfo[]>>(TYPE_ESTIMATE);
                }

                if (_FIELD_CACHE == null)
                {
                    _FIELD_CACHE =
                        new Dictionary<Type, Dictionary<BindingFlags, Dictionary<string, FieldInfo>>>(
                            TYPE_ESTIMATE
                        );
                }

                if (_PROPERTY_CACHE_BASIC == null)
                {
                    _PROPERTY_CACHE_BASIC =
                        new Dictionary<Type, Dictionary<BindingFlags, PropertyInfo[]>>(TYPE_ESTIMATE);
                }

                if (_PROPERTY_CACHE == null)
                {
                    _PROPERTY_CACHE =
                        new Dictionary<Type, Dictionary<BindingFlags, Dictionary<string, PropertyInfo>>>(
                            TYPE_ESTIMATE
                        );
                }

                if (_METHOD_CACHE_BASIC == null)
                {
                    _METHOD_CACHE_BASIC =
                        new Dictionary<Type, Dictionary<BindingFlags, MethodInfo[]>>(TYPE_ESTIMATE);
                }

                if (_METHOD_CACHE == null)
                {
                    _METHOD_CACHE =
                        new Dictionary<Type, Dictionary<BindingFlags, Dictionary<string, MethodInfo[]>>>(
                            TYPE_ESTIMATE
                        );
                }

                if (_MEMBER_STATIC_LOOKUP_CACHE == null)
                {
                    _MEMBER_STATIC_LOOKUP_CACHE = new Dictionary<MemberInfo, bool>(TYPE_ESTIMATE);
                }

                if (READABLE_NAMES_CACHE == null)
                {
                    READABLE_NAMES_CACHE = new Dictionary<Type, string>(TYPE_ESTIMATE);
                }

                if (READABLE_NAME_CACHE_LOCK == null)
                {
                    READABLE_NAME_CACHE_LOCK = new object();
                }
            }
        }

        private static bool IsStatic_INTERNAL(this MemberInfo member)
        {
            using (_PRF_IsStatic_INTERNAL.Auto())
            {
                if (_MEMBER_STATIC_LOOKUP_CACHE == null)
                {
                    _MEMBER_STATIC_LOOKUP_CACHE = new Dictionary<MemberInfo, bool>();
                }

                if (_MEMBER_STATIC_LOOKUP_CACHE.ContainsKey(member))
                {
                    return _MEMBER_STATIC_LOOKUP_CACHE[member];
                }

                bool result;

                switch (member)
                {
                    case FieldInfo fieldInfo:
                        result = fieldInfo.IsStatic;
                        break;
                    case PropertyInfo propertyInfo:
                        result = !propertyInfo.CanRead
                            ? propertyInfo.GetSetMethod(true).IsStatic
                            : propertyInfo.GetGetMethod(true).IsStatic;
                        break;
                    case MethodBase methodBase:
                        result = methodBase.IsStatic;
                        break;
                    case EventInfo eventInfo:
                        result = eventInfo.GetRaiseMethod(true)?.IsStatic ?? false;
                        break;
                    case Type type:
                        return type.IsSealed && type.IsAbstract;
                    default:
                        throw new NotSupportedException(
                            ZString.Format(
                                "Unable to determine IsStatic for member {0}.{1}MemberType was {2} but only fields, properties, methods, events and types are supported.",
                                member.DeclaringType.FullName,
                                member.Name,
                                member.GetType().FullName
                            )
                        );
                }

                _MEMBER_STATIC_LOOKUP_CACHE.Add(member, result);

                return result;
            }
        }

        private static void PopulateFields_INTERNAL(this Type t, BindingFlags flags)
        {
            using (_PRF_PopulateFields.Auto())
            {
                CheckInitialization(t);

                var typeFieldCacheBasic = _FIELD_CACHE_BASIC[t];
                var typeFieldCache = _FIELD_CACHE[t];

                if (!typeFieldCache.ContainsKey(flags))
                {
                    typeFieldCache.Add(flags, new Dictionary<string, FieldInfo>());
                }

                var flagTypeFieldCache = typeFieldCache[flags];

                FieldInfo[] fields;

                if (!typeFieldCacheBasic.ContainsKey(flags))
                {
                    fields = t.GetFields(flags);

                    typeFieldCacheBasic.Add(flags, fields);
                }
                else
                {
                    fields = typeFieldCacheBasic[flags];
                }

                for (var index = 0; index < fields.Length; index++)
                {
                    var field = fields[index];
                    if (!flagTypeFieldCache.ContainsKey(field.Name))
                    {
                        flagTypeFieldCache.Add(field.Name, field);
                    }
                }
            }
        }

        private static void PopulateMethods_INTERNAL(this Type t, BindingFlags flags)
        {
            using (_PRF_PopulateMethods_INTERNAL.Auto())
            {
                CheckInitialization(t);

                var typeMethodCacheBasic = _METHOD_CACHE_BASIC[t];
                var typeMethodCache = _METHOD_CACHE[t];

                if (!typeMethodCache.ContainsKey(flags))
                {
                    typeMethodCache.Add(flags, new Dictionary<string, MethodInfo[]>());
                }

                var flagTypeMethodCache = typeMethodCache[flags];

                MethodInfo[] methods;

                if (!typeMethodCacheBasic.ContainsKey(flags))
                {
                    methods = t.GetMethods(flags);

                    typeMethodCacheBasic.Add(flags, methods);
                }
                else
                {
                    methods = typeMethodCacheBasic[flags];
                }

                var suitableMethods = new List<MethodInfo>();

                for (var index = 0; index < methods.Length; index++)
                {
                    var method = methods[index];

                    if (!flagTypeMethodCache.ContainsKey(method.Name))
                    {
                        suitableMethods.Clear();

                        for (var innerIndex = 0; innerIndex < methods.Length; innerIndex++)
                        {
                            var innerMethod = methods[index];

                            if (innerMethod.Name == method.Name)
                            {
                                suitableMethods.Add(innerMethod);
                            }
                        }

                        flagTypeMethodCache.Add(method.Name, suitableMethods.ToArray());
                    }
                }
            }
        }

        private static void PopulateProperties_INTERNAL(this Type t, BindingFlags flags)
        {
            using (_PRF_PopulateProperties.Auto())
            {
                CheckInitialization(t);

                var typePropertyCacheBasic = _PROPERTY_CACHE_BASIC[t];
                var typePropertyCache = _PROPERTY_CACHE[t];

                if (!typePropertyCache.ContainsKey(flags))
                {
                    typePropertyCache.Add(flags, new Dictionary<string, PropertyInfo>());
                }

                var flagTypePropertyCache = typePropertyCache[flags];

                PropertyInfo[] propertys;

                if (!typePropertyCacheBasic.ContainsKey(flags))
                {
                    propertys = t.GetProperties(flags);

                    typePropertyCacheBasic.Add(flags, propertys);
                }
                else
                {
                    propertys = typePropertyCacheBasic[flags];
                }

                for (var index = 0; index < propertys.Length; index++)
                {
                    var property = propertys[index];
                    if (!flagTypePropertyCache.ContainsKey(property.Name))
                    {
                        flagTypePropertyCache.Add(property.Name, property);
                    }
                }
            }
        }

        #region Profiling

        private static readonly ProfilerMarker _PRF_GetAppalachiaTypes_CACHED =
            new ProfilerMarker(_PRF_PFX + nameof(GetAppalachiaTypes_CACHED));

        private static readonly ProfilerMarker _PRF_GetAppalachiaTypesInternal =
            new ProfilerMarker(_PRF_PFX + nameof(GetAppalachiaTypesInternal));

        #endregion

        //private static Dictionary<Type, Dictionary<BindingFlags, MemberInfo[]>> _TYPE_MEMBERS_CACHE;         
    }
}
