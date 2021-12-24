using System.Collections.Generic;
using Appalachia.Utility.Algorithms.Common;

namespace Appalachia.Utility.Algorithms.Sorting
{
    public static class SelectionSorter
    {
        public static void SelectionSort<T>(this IList<T> collection, Comparer<T> comparer = null)
        {
            comparer = comparer ?? Comparer<T>.Default;
            collection.SelectionSortAscending(comparer);
        }

        /// <summary>
        ///     Public API: Sorts ascending
        /// </summary>
        public static void SelectionSortAscending<T>(this IList<T> collection, Comparer<T> comparer)
        {
            int i;
            for (i = 0; i < collection.Count; i++)
            {
                var min = i;
                for (var j = i + 1; j < collection.Count; j++)
                {
                    if (comparer.Compare(collection[j], collection[min]) < 0)
                    {
                        min = j;
                    }
                }

                collection.Swap(i, min);
            }
        }

        /// <summary>
        ///     Public API: Sorts ascending
        /// </summary>
        public static void SelectionSortDescending<T>(this IList<T> collection, Comparer<T> comparer)
        {
            int i;
            for (i = collection.Count - 1; i > 0; i--)
            {
                var max = i;
                for (var j = 0; j <= i; j++)
                {
                    if (comparer.Compare(collection[j], collection[max]) < 0)
                    {
                        max = j;
                    }
                }

                collection.Swap(i, max);
            }
        }
    }
}
