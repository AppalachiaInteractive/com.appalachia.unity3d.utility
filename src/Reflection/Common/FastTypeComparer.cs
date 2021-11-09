using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Appalachia.Utility.Reflection.Common
{
    public class FastTypeComparer : IEqualityComparer<Type>
    {
        public static readonly FastTypeComparer Instance = new();

        [DebuggerStepThrough] public bool Equals(Type x, Type y)
        {
            return (x == y) || (x == y);
        }

        [DebuggerStepThrough] public int GetHashCode(Type obj)
        {
            return obj.GetHashCode();
        }
    }
}
