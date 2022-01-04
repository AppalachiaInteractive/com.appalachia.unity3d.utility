using System;

namespace Appalachia.Utility.Extensions
{
    [Flags]
    public enum GetComponentStrategy
    {
        None = 0,
        CurrentObject = 1 << 0,
        ParentObject = 1 << 1,
        Children = 1 << 2,
        AnyParent = 1 << 3,
        IncludeInactive = 1 << 4,
    }
}
