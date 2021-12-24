using System.Collections.Generic;

namespace Appalachia.Utility.Algorithms.Sorting
{
    public static class CycleSorter
    {
        public static void CycleSort<T>(this IList<T> collection, Comparer<T> comparer = null)
        {
            comparer = comparer ?? Comparer<T>.Default;
            collection.CycleSortAscending(comparer);
        }

        public static void CycleSortAscending<T>(this IList<T> collection, Comparer<T> comparer)
        {
            for (var cycleStart = 0; cycleStart < collection.Count; cycleStart++)
            {
                var item = collection[cycleStart];
                var position = cycleStart;

                do
                {
                    var to = 0;
                    for (var i = 0; i < collection.Count; i++)
                    {
                        if ((i != cycleStart) && (comparer.Compare(collection[i], item) < 0))
                        {
                            to++;
                        }
                    }

                    if (position != to)
                    {
                        while ((position != to) && (comparer.Compare(item, collection[to]) == 0))
                        {
                            to++;
                        }

                        var temp = collection[to];
                        collection[to] = item;
                        item = temp;
                        position = to;
                    }
                } while (position != cycleStart);
            }
        }

        public static void CycleSortDescending<T>(this IList<T> collection, Comparer<T> comparer)
        {
            for (var cycleStart = 0; cycleStart < collection.Count; cycleStart++)
            {
                var item = collection[cycleStart];
                var position = cycleStart;

                do
                {
                    var to = 0;
                    for (var i = 0; i < collection.Count; i++)
                    {
                        if ((i != cycleStart) && (comparer.Compare(collection[i], item) > 0))
                        {
                            to++;
                        }
                    }

                    if (position != to)
                    {
                        while ((position != to) && (comparer.Compare(item, collection[to]) == 0))
                        {
                            to++;
                        }

                        var temp = collection[to];
                        collection[to] = item;
                        item = temp;
                        position = to;
                    }
                } while (position != cycleStart);
            }
        }
    }
}
