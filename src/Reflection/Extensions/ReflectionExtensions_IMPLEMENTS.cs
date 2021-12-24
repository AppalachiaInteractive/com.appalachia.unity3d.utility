using System;
using System.Collections.Generic;

namespace Appalachia.Utility.Reflection.Extensions
{
    public static partial class ReflectionExtensions
    {
        private static Dictionary<Type, List<Type>> _implementorLookup;
        private static Dictionary<Type, List<Type>> _concreteImplementorLookup;

        private static Dictionary<Type, List<Type>> ImplementorLookup
        {
            get
            {
                if (_implementorLookup == null)
                {
                    _implementorLookup = new Dictionary<Type, List<Type>>();
                }

                return _implementorLookup;
            }
        }

        private static Dictionary<Type, List<Type>> ConcreteImplementorLookup
        {
            get
            {
                if (_concreteImplementorLookup == null)
                {
                    _concreteImplementorLookup = new Dictionary<Type, List<Type>>();
                }

                return _concreteImplementorLookup;
            }
        }

        public static List<Type> GetAllConcreteImplementors(this Type t)
        {
            var l = ConcreteImplementorLookup;

            if (!l.ContainsKey(t))
            {
                var implementors = GetImplementors(t, true);

                l.Add(t, implementors);

                return implementors;
            }

            return l[t];
        }

        public static List<Type> GetAllConcreteImplementors<T>()
        {
            return typeof(T).GetAllConcreteImplementors();
        }

        public static List<Type> GetAllImplementors(this Type t)
        {
            var l = ImplementorLookup;

            if (!l.ContainsKey(t))
            {
                var implementors = GetImplementors(t, false);

                l.Add(t, implementors);

                return implementors;
            }

            return l[t];
        }

        public static List<Type> GetAllImplementors<T>()
        {
            return typeof(T).GetAllImplementors();
        }

        private static List<Type> GetImplementors(Type t, bool concreteOnly)
        {
            var list = new List<Type>();
            var types = GetAllTypes_CACHED();

            for (var i = 0; i < types.Length; i++)
            {
                var assemblyType = types[i];

                if (t.IsAssignableFrom(assemblyType))
                {
                    if (concreteOnly && assemblyType.IsInterface)
                    {
                        continue;
                    }

                    list.Add(assemblyType);
                }
            }

            return list;
        }
    }
}
