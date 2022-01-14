using System;
using UnityEngine;

namespace Appalachia.Utility.Extensions
{
    [Flags]
    public enum GetComponentStrategy
    {
        /// <summary>
        ///     No <see cref="GameObject" />s will be searched.
        /// </summary>
        None = 0,

        /// <summary>
        ///     Searches for the component on the current <see cref="GameObject" />.
        /// </summary>
        CurrentObject = 1 << 0,

        /// <summary>
        ///     Searches for the component on the current <see cref="GameObject" />'s immediate parent.
        /// </summary>
        ParentObject = 1 << 1,

        /// <summary>
        ///     Searches for the component on the current <see cref="GameObject" />'s children (recursively).
        /// </summary>
        Children = 1 << 2,

        /// <summary>
        ///     Searches for the component on the current <see cref="GameObject" />'s parents (recursively).
        /// </summary>
        AnyParent = 1 << 3,

        /// <summary>
        ///     Includes inactive <see cref="GameObject" />s in the search (i.e. those with the checkbox unchecked).
        /// </summary>
        IncludeInactive = 1 << 4,
    }
}
