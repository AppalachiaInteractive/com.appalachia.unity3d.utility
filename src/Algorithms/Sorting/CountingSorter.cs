using System;
using System.Collections.Generic;
using Appalachia.Utility.Algorithms.Common;

namespace Appalachia.Utility.Algorithms.Sorting
{
    public static class CountingSorter
    {
        public static void CountingSort(this IList<int> collection)
        {
            if ((collection == null) || (collection.Count == 0))
            {
                return;
            }

            // Get the maximum number in array.
            var maxK = 0;
            var index = 0;
            while (true)
            {
                if (index >= collection.Count)
                {
                    break;
                }

                maxK = Math.Max(maxK, collection[index] + 1);
                index++;
            }

            // The array of keys, used to sort the original array.
            var keys = new int[maxK];
            keys.Populate(0); // populate it with zeros

            // Assign the keys
            for (var i = 0; i < collection.Count; ++i)
            {
                keys[collection[i]] += 1;
            }

            // Reset index.
            index = 0;

            // Sort the elements
            for (var j = 0; j < keys.Length; ++j)
            {
                var val = keys[j];

                if (val > 0)
                {
                    while (val-- > 0)
                    {
                        collection[index] = j;
                        index++;
                    }
                }
            }
        }
    }
}
