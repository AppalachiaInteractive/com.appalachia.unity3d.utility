using System.Collections.Generic;
using System.Linq;

namespace Appalachia.Utility.Algorithms.Sorting
{
    /// <summary>
    ///     Only support IList<int> Sort
    /// </summary>
    public static class BucketSorter
    {
        public static void BucketSort(this IList<int> collection)
        {
            collection.BucketSortAscending();
        }

        /// <summary>
        ///     Public API: Sorts ascending
        /// </summary>
        public static void BucketSortAscending(this IList<int> collection)
        {
            var maxValue = collection.Max();
            var minValue = collection.Min();

            var bucket = new List<int>[(maxValue - minValue) + 1];

            for (var i = 0; i < bucket.Length; i++)
            {
                bucket[i] = new List<int>();
            }

            foreach (var i in collection)
            {
                bucket[i - minValue].Add(i);
            }

            var k = 0;
            foreach (var i in bucket)
            {
                if (i.Count > 0)
                {
                    foreach (var j in i)
                    {
                        collection[k] = j;
                        k++;
                    }
                }
            }
        }

        /// <summary>
        ///     Public API: Sorts descending
        /// </summary>
        public static void BucketSortDescending(this IList<int> collection)
        {
            var maxValue = collection[0];
            var minValue = collection[0];
            for (var i = 1; i < collection.Count; i++)
            {
                if (collection[i] > maxValue)
                {
                    maxValue = collection[i];
                }

                if (collection[i] < minValue)
                {
                    minValue = collection[i];
                }
            }

            var bucket = new List<int>[(maxValue - minValue) + 1];

            for (var i = 0; i < bucket.Length; i++)
            {
                bucket[i] = new List<int>();
            }

            foreach (var i in collection)
            {
                bucket[i - minValue].Add(i);
            }

            var k = collection.Count - 1;
            foreach (var i in bucket)
            {
                if (i.Count > 0)
                {
                    foreach (var j in i)
                    {
                        collection[k] = j;
                        k--;
                    }
                }
            }
        }
    }
}
