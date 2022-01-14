using System.Collections.Generic;
using Appalachia.Utility.Strings;
using Unity.Profiling;

namespace Appalachia.Utility.DataStructures.Extensions
{
    public static class IListExtensions
    {
        /// <summary>
        ///     Returns the first item in the list.
        /// </summary>
        /// <param name="collection">The list to pull the item from.</param>
        /// <typeparam name="T">The list element type.</typeparam>
        /// <returns>The item.</returns>
        public static T GetFirst<T>(this IList<T> collection)
        {
            using (_PRF_GetFirst.Auto())
            {
                return collection[0];
            }
        }

        /// <summary>
        ///     Returns the last item in the list.
        /// </summary>
        /// <param name="collection">The list to pull the item from.</param>
        /// <typeparam name="T">The list element type.</typeparam>
        /// <returns>The item.</returns>
        public static T GetLast<T>(this IList<T> collection)
        {
            using (_PRF_GetLast.Auto())
            {
                return collection[^1];
            }
        }

        /// <summary>
        ///     Returns the second item in the list.
        /// </summary>
        /// <param name="collection">The list to pull the item from.</param>
        /// <typeparam name="T">The list element type.</typeparam>
        /// <returns>The item.</returns>
        public static T GetSecond<T>(this IList<T> collection)
        {
            using (_PRF_GetSecond.Auto())
            {
                return collection[1];
            }
        }

        /// <summary>
        ///     Return a human readable, multi-line, print-out (string) of this list.
        /// </summary>
        /// <returns>The human readable string.</returns>
        /// <param name="addHeader">If set to <c>true</c> a header with count and Type is added; otherwise, only elements are printed.</param>
        public static string ToHumanReadable<T>(this IList<T> collection, bool addHeader = false)
        {
            using (_PRF_ToHumanReadable.Auto())
            {
                var i = 0;
                var listAsString = string.Empty;

                var preLineIndent = addHeader == false ? "" : "\t";

                for (i = 0; i < collection.Count; ++i)
                {
                    listAsString = ZString.Format(
                        "{0}{1}[{2}] => {3}\r\n",
                        listAsString,
                        preLineIndent,
                        i,
                        collection[i]
                    );
                }

                if (addHeader)
                {
                    listAsString = ZString.Format(
                        "ArrayList of count: {0}.\r\n(\r\n{1})",
                        collection.Count,
                        listAsString
                    );
                }

                return listAsString;
            }
        }

        #region Profiling

        private const string _PRF_PFX = nameof(IListExtensions) + ".";

        private static readonly ProfilerMarker
            _PRF_GetFirst = new ProfilerMarker(_PRF_PFX + nameof(GetFirst));

        private static readonly ProfilerMarker _PRF_GetLast = new ProfilerMarker(_PRF_PFX + nameof(GetLast));

        private static readonly ProfilerMarker _PRF_GetSecond =
            new ProfilerMarker(_PRF_PFX + nameof(GetSecond));

        private static readonly ProfilerMarker _PRF_ToHumanReadable =
            new ProfilerMarker(_PRF_PFX + nameof(ToHumanReadable));

        #endregion
    }
}
