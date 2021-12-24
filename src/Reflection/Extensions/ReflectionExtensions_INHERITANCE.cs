using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Appalachia.Utility.Strings;

namespace Appalachia.Utility.Reflection.Extensions
{
    public static partial class ReflectionExtensions
    {
        private static Dictionary<Type, List<Type>> _inheritorLookup;
        private static Dictionary<Type, List<Type>> _concreteInheritorLookup;

        private static Dictionary<Type, List<Type>> InheritorLookup
        {
            get
            {
                if (_inheritorLookup == null)
                {
                    _inheritorLookup = new Dictionary<Type, List<Type>>();
                }

                return _inheritorLookup;
            }
        }

        private static Dictionary<Type, List<Type>> ConcreteInheritorLookup
        {
            get
            {
                if (_concreteInheritorLookup == null)
                {
                    _concreteInheritorLookup = new Dictionary<Type, List<Type>>();
                }

                return _concreteInheritorLookup;
            }
        }

        public static List<Type> GetAllConcreteInheritors(this Type t)
        {
            var l = ConcreteInheritorLookup;

            if (!l.ContainsKey(t))
            {
                var inheritors = GetInheritors(t, true);

                l.Add(t, inheritors);

                return inheritors;
            }

            return l[t];
        }

        public static List<Type> GetAllConcreteInheritors<T>()
        {
            return typeof(T).GetAllConcreteInheritors();
        }

        public static IEnumerable<Type> GetAllConcreteInheritorsWithDefaultConstructors(this Type t)
        {
            var inheritors = GetAllConcreteInheritors(t);

            foreach (var inheritor in inheritors)
            {
                if (inheritor.HasPublicParameterlessConstructor())
                {
                    yield return inheritor;
                }
            }
        }

        public static IEnumerable<Type> GetAllConcreteInheritorsWithDefaultConstructors<T>()
        {
            return GetAllConcreteInheritorsWithDefaultConstructors(typeof(T));
        }
        
        public static bool HasPublicParameterlessConstructor<T>()
        {
            return HasPublicParameterlessConstructor(typeof(T));
        }
        
        public static bool HasPublicParameterlessConstructor(this Type t)
        {
            var constructors =
                t.GetConstructors(BindingFlags.Default | BindingFlags.Public | BindingFlags.Instance);
            
            foreach (var constructor in constructors)
            {
                if (constructor.IsPublic)
                {
                    var parameters = constructor.GetParameters();

                    if (parameters.Length == 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        
        public static List<Type> GetAllInheritors(this Type t)
        {
            var l = InheritorLookup;

            if (!l.ContainsKey(t))
            {
                var inheritors = GetInheritors(t, false);

                l.Add(t, inheritors);

                return inheritors;
            }

            return l[t];
        }

        public static List<Type> GetAllInheritors<T>()
        {
            return typeof(T).GetAllInheritors();
        }

        private static List<Type> GetInheritors(Type t, bool concreteOnly)
        {
            var list = new List<Type>();
            var types = GetAllTypes_CACHED();

            for (var i = 0; i < types.Length; i++)
            {
                var assemblyType = types[i];

                if (t.IsAssignableFrom(assemblyType))
                {
                    if (concreteOnly && assemblyType.IsAbstract)
                    {
                        continue;
                    }

                    list.Add(assemblyType);
                }
            }

            return list;
        }

        public static bool ImplementsOrInherits(this Type type, Type to)
        {
            return to.IsAssignableFrom(type);
        }

        public static bool InheritsFrom<TBase>(this Type type)
        {
            return type.InheritsFrom(typeof(TBase));
        }

        public static bool InheritsFrom(this Type type, Type baseType)
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

        public static Type GetGenericBaseType(this Type type, Type baseType)
        {
            return type.GetGenericBaseType(baseType, out _);
        }

        public static Type GetGenericBaseType(this Type type, Type baseType, out int depthCount)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (baseType == null)
            {
                throw new ArgumentNullException(nameof(baseType));
            }

            if (!baseType.IsGenericType)
            {
                throw new ArgumentException(ZString.Format("Type {0} is not a generic type.", baseType.Name));
            }

            if (!type.InheritsFrom(baseType))
            {
                throw new ArgumentException(
                    ZString.Format("Type {0} does not inherit from {1}.", type.Name, baseType.Name)
                );
            }

            var type1 = type;
            depthCount = 0;
            for (;
                (type1 != null) &&
                (!type1.IsGenericType || (type1.GetGenericTypeDefinition() != baseType));
                type1 = type1.BaseType)
            {
                ++depthCount;
            }

            if (type1 == null)
            {
                throw new ArgumentException(
                    ZString.Format(
                        "{0} is assignable from {1}, but base type was not found?",
                        type.Name,
                        baseType.Name
                    )
                );
            }

            return type1;
        }

        public static IEnumerable<Type> GetBaseTypes(this Type type, bool includeSelf = false)
        {
            var first = type.GetBaseClasses(includeSelf).Concat(type.GetInterfaces());

            if (includeSelf && type.IsInterface)
            {
                first = first.Concat(new Type[1] {type});
            }

            return first;
        }

        public static IEnumerable<Type> GetBaseClasses(this Type type, bool includeSelf = false)
        {
            if ((type == null) || (type.BaseType == null))
            {
                yield break;
            }

            if (includeSelf)
            {
                yield return type;
            }

            for (var current = type.BaseType; current != null; current = current.BaseType)
            {
                yield return current;
            }
        }
    }
}
