using System;
using System.Collections.Generic;
using Appalachia.Utility.Algorithms.Common;
using Appalachia.Utility.DataStructures.Graphs;
using Unity.Profiling;

namespace Appalachia.Utility.Algorithms.Graphs
{
    /// <summary>
    ///     Implements Cycles Detection in Graphs
    /// </summary>
    public static class CyclesDetector
    {
        /// <summary>
        ///     Returns true if Graph has cycle.
        /// </summary>
        public static bool IsCyclic<T>(IGraph<T> Graph)
            where T : IComparable<T>
        {
            using (_PRF_IsCyclic.Auto())
            {
                if (Graph == null)
                {
                    throw new ArgumentNullException();
                }

                var visited = new HashSet<T>();
                var recursionStack = new HashSet<T>();

                if (Graph.IsDirected)
                {
                    foreach (var vertex in Graph.Vertices)
                    {
                        if (IsDirectedCyclic(Graph, vertex, ref visited, ref recursionStack))
                        {
                            return true;
                        }
                    }
                }
                else
                {
                    foreach (var vertex in Graph.Vertices)
                    {
                        if (IsUndirectedCyclic(Graph, vertex, null, ref visited))
                        {
                            return true;
                        }
                    }
                }

                return false;
            }
        }

        /// <summary>
        ///     [Directed DFS Forest]
        ///     Helper function used to decide whether the graph explored from a specific vertex contains a cycle.
        /// </summary>
        /// <param name="graph">The graph to explore.</param>
        /// <param name="source">The vertex to explore graph from.</param>
        /// <param name="parent">The predecessor node to the vertex we are exploring the graph from.</param>
        /// <param name="visited">A hash set of the explored nodes so far.</param>
        /// <param name="recursionStack">A set of element that are currently being processed.</param>
        /// <returns>True if there is a cycle; otherwise, false.</returns>
        private static bool IsDirectedCyclic<T>(
            IGraph<T> graph,
            T source,
            ref HashSet<T> visited,
            ref HashSet<T> recursionStack)
            where T : IComparable<T>
        {
            using (_PRF_IsDirectedCyclic.Auto())
            {
                if (!visited.Contains(source))
                {
                    // Mark the current node as visited and add it to the recursion stack
                    visited.Add(source);
                    recursionStack.Add(source);

                    // Recur for all the vertices adjacent to this vertex
                    foreach (var adjacent in graph.Neighbours(source))
                    {
                        // If an adjacent node was not visited, then check the DFS forest of the adjacent for directed cycles.
                        if (!visited.Contains(adjacent) &&
                            IsDirectedCyclic(graph, adjacent, ref visited, ref recursionStack))
                        {
                            return true;
                        }

                        // If an adjacent is visited and is on the recursion stack then there is a cycle.
                        if (recursionStack.Contains(adjacent))
                        {
                            return true;
                        }
                    }
                }

                // Remove the source vertex from the recursion stack
                recursionStack.Remove(source);
                return false;
            }
        }

        /// <summary>
        ///     [Undirected DFS Forest].
        ///     Helper function used to decide whether the graph explored from a specific vertex contains a cycle.
        /// </summary>
        /// <param name="graph">The graph to explore.</param>
        /// <param name="source">The vertex to explore graph from.</param>
        /// <param name="parent">The predecessor node to the vertex we are exploring the graph from.</param>
        /// <param name="visited">A hash set of the explored nodes so far.</param>
        /// <returns>True if there is a cycle; otherwise, false.</returns>
        private static bool IsUndirectedCyclic<T>(
            IGraph<T> graph,
            T source,
            object parent,
            ref HashSet<T> visited)
            where T : IComparable<T>
        {
            using (_PRF_IsUndirectedCyclic.Auto())
            {
                if (!visited.Contains(source))
                {
                    // Mark the current node as visited
                    visited.Add(source);

                    // Recur for all the vertices adjacent to this vertex
                    foreach (var adjacent in graph.Neighbours(source))
                    {
                        // If an adjacent node was not visited, then check the DFS forest of the adjacent for UNdirected cycles.
                        if (!visited.Contains(adjacent) &&
                            IsUndirectedCyclic(graph, adjacent, source, ref visited))
                        {
                            return true;
                        }

                        // If an adjacent is visited and NOT parent of current vertex, then there is a cycle.
                        if ((parent != null) && !adjacent.IsEqualTo((T)parent))
                        {
                            return true;
                        }
                    }
                }

                return false;
            }
        }

        #region Profiling

        private const string _PRF_PFX = nameof(CyclesDetector) + ".";

        private static readonly ProfilerMarker _PRF_IsDirectedCyclic =
            new ProfilerMarker(_PRF_PFX + nameof(IsDirectedCyclic));

        private static readonly ProfilerMarker _PRF_IsUndirectedCyclic =
            new ProfilerMarker(_PRF_PFX + nameof(IsUndirectedCyclic));

        private static readonly ProfilerMarker
            _PRF_IsCyclic = new ProfilerMarker(_PRF_PFX + nameof(IsCyclic));

        #endregion
    }
}
