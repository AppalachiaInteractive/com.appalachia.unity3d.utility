﻿using System.Collections.Generic;
using Appalachia.Utility.Algorithms.Common;

namespace Appalachia.Utility.Algorithms.Sorting
{
    public static class ShellSorter
    {
        public static void ShellSort<T>(this IList<T> collection, Comparer<T> comparer = null)
        {
            comparer = comparer ?? Comparer<T>.Default;
            collection.ShellSortAscending(comparer);
        }

        /// <summary>
        ///     Public API: Sorts ascending
        /// </summary>
        public static void ShellSortAscending<T>(this IList<T> collection, Comparer<T> comparer)
        {
            var flag = true;
            var d = collection.Count;
            while (flag || (d > 1))
            {
                flag = false;
                d = (d + 1) / 2;
                for (var i = 0; i < (collection.Count - d); i++)
                {
                    if (comparer.Compare(collection[i + d], collection[i]) < 0)
                    {
                        collection.Swap(i + d, i);
                        flag = true;
                    }
                }
            }
        }

        /// <summary>
        ///     Public API: Sorts descending
        /// </summary>
        public static void ShellSortDescending<T>(this IList<T> collection, Comparer<T> comparer)
        {
            var flag = true;
            var d = collection.Count;
            while (flag || (d > 1))
            {
                flag = false;
                d = (d + 1) / 2;
                for (var i = 0; i < (collection.Count - d); i++)
                {
                    if (comparer.Compare(collection[i + d], collection[i]) > 0)
                    {
                        collection.Swap(i + d, i);
                        flag = true;
                    }
                }
            }
        }
    }
}
