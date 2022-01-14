using System;
using System.Collections.Generic;
using Appalachia.Utility.DataStructures.Graphs;
using Unity.Profiling;

namespace Appalachia.Utility.Algorithms.Graphs
{
    public static class TopologicalSorter
    {
        /// <summary>
        ///     The Topological Sorting algorithm
        /// </summary>
        public static IEnumerable<T> Sort<T>(IGraph<T> Graph)
            where T : IComparable<T>
        {
            using (_PRF_Sort.Auto())
            {
                // If the graph is either null or is not a DAG, throw exception.
                if (Graph == null)
                {
                    throw new ArgumentNullException();
                }

                if (!Graph.IsDirected || CyclesDetector.IsCyclic(Graph))
                {
                    throw new Exception("The graph is not a DAG.");
                }

                var visited = new HashSet<T>();
                var topoSortStack = new Stack<T>();

                foreach (var vertex in Graph.Vertices)
                {
                    if (!visited.Contains(vertex))
                    {
                        _topoSortHelper(Graph, vertex, ref topoSortStack, ref visited);
                    }
                }

                return topoSortStack;
            }
        }

        /// <summary>
        ///     Private recursive helper.
        /// </summary>
        private static void _topoSortHelper<T>(
            IGraph<T> graph,
            T source,
            ref Stack<T> topoSortStack,
            ref HashSet<T> visited)
            where T : IComparable<T>
        {
            using (_PRF__topoSortHelper.Auto())
            {
                visited.Add(source);

                foreach (var adjacent in graph.Neighbours(source))
                {
                    if (!visited.Contains(adjacent))
                    {
                        _topoSortHelper(graph, adjacent, ref topoSortStack, ref visited);
                    }
                }

                topoSortStack.Push(source);
            }
        }

        #region Profiling

        private const string _PRF_PFX = nameof(TopologicalSorter) + ".";

        private static readonly ProfilerMarker _PRF__topoSortHelper =
            new ProfilerMarker(_PRF_PFX + nameof(_topoSortHelper));

        private static readonly ProfilerMarker _PRF_Sort = new ProfilerMarker(_PRF_PFX + nameof(Sort));

        #endregion
    }
}
