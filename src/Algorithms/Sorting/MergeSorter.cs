using System.Collections.Generic;

namespace Appalachia.Utility.Algorithms.Sorting
{
    public static class MergeSorter
    {
        //
        // Public merge-sort API
        public static List<T> MergeSort<T>(this List<T> collection, Comparer<T> comparer = null)
        {
            comparer = comparer ?? Comparer<T>.Default;

            return InternalMergeSort(collection, comparer);
        }

        //
        // Private static method
        // Implements the merge function inside the merge-sort
        private static List<T> InternalMerge<T>(
            List<T> leftCollection,
            List<T> rightCollection,
            Comparer<T> comparer)
        {
            var left = 0;
            var right = 0;
            int index;
            var length = leftCollection.Count + rightCollection.Count;

            var result = new List<T>(length);

            for (index = 0; (right < rightCollection.Count) && (left < leftCollection.Count); ++index)
            {
                if (comparer.Compare(
                        rightCollection[right],
                        leftCollection[left]
                    ) <=
                    0) // rightElement <= leftElement
                {
                    //resultArray.Add(rightCollection[right]);
                    result.Insert(index, rightCollection[right++]);
                }
                else
                {
                    //result.Add(leftCollection[left]);
                    result.Insert(index, leftCollection[left++]);
                }
            }

            //
            // At most one of left and right might still have elements left

            while (right < rightCollection.Count)
            {
                result.Insert(index++, rightCollection[right++]);
            }

            while (left < leftCollection.Count)
            {
                result.Insert(index++, leftCollection[left++]);
            }

            return result;
        }

        //
        // Private static method
        // Implements the recursive merge-sort algorithm
        private static List<T> InternalMergeSort<T>(List<T> collection, Comparer<T> comparer)
        {
            if (collection.Count < 2)
            {
                return collection;
            }

            var midIndex = collection.Count / 2;

            var leftCollection = collection.GetRange(0,         midIndex);
            var rightCollection = collection.GetRange(midIndex, collection.Count - midIndex);

            leftCollection = InternalMergeSort(leftCollection,   comparer);
            rightCollection = InternalMergeSort(rightCollection, comparer);

            return InternalMerge(leftCollection, rightCollection, comparer);
        }
    }
}
