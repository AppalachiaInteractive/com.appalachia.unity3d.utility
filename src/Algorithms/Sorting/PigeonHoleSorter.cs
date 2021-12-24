using System.Collections.Generic;
using System.Linq;

namespace Appalachia.Utility.Algorithms.Sorting
{
    /// <summary>
    ///     Only support IList
    ///     <int>
    ///         Sort
    ///         Also called CountSort (not CountingSort)
    /// </summary>
    public static class PigeonHoleSorter
    {
        public static void PigeonHoleSort(this IList<int> collection)
        {
            collection.PigeonHoleSortAscending();
        }

        public static void PigeonHoleSortAscending(this IList<int> collection)
        {
            var min = collection.Min();
            var max = collection.Max();
            var size = (max - min) + 1;
            var holes = new int[size];
            foreach (var x in collection)
            {
                holes[x - min]++;
            }

            var i = 0;
            for (var count = 0; count < size; count++)
            {
                while (holes[count]-- > 0)
                {
                    collection[i] = count + min;
                    i++;
                }
            }
        }

        public static void PigeonHoleSortDescending(this IList<int> collection)
        {
            var min = collection.Min();
            var max = collection.Max();
            var size = (max - min) + 1;
            var holes = new int[size];
            foreach (var x in collection)
            {
                holes[x - min]++;
            }

            var i = 0;
            for (var count = size - 1; count >= 0; count--)
            {
                while (holes[count]-- > 0)
                {
                    collection[i] = count + min;
                    i++;
                }
            }
        }
    }
}
