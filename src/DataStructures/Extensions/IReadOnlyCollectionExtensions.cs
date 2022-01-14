using System.Collections.Generic;
using Unity.Profiling;

namespace Appalachia.Utility.DataStructures.Extensions
{
    public static class IReadOnlyCollectionExtensions
    {
        /// <summary>
        ///     Checks whether or not the collection is empty.
        /// </summary>
        /// <param name="collection">The collection to check.</param>
        /// <typeparam name="T">The collection element type.</typeparam>
        /// <returns>Whether or not the collection is empty</returns>
        public static bool IsEmpty<T>(this IReadOnlyCollection<T> collection)
        {
            using (_PRF_IsEmpty.Auto())
            {
                return collection.Count == 0;
            }
        }

        #region Profiling

        private const string _PRF_PFX = nameof(IReadOnlyCollectionExtensions) + ".";

        private static readonly ProfilerMarker _PRF_IsEmpty = new ProfilerMarker(_PRF_PFX + nameof(IsEmpty));

        #endregion
    }
}
