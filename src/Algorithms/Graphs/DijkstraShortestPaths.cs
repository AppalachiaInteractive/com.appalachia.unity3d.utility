﻿using System;
using System.Collections.Generic;
using System.Linq;
using Appalachia.Utility.DataStructures.Graphs;
using Appalachia.Utility.DataStructures.Heaps;

namespace Appalachia.Utility.Algorithms.Graphs
{
    /// <summary>
    ///     Computes Dijkstra's Shortest-Paths for Directed Weighted Graphs from a single-source to all destinations.
    /// </summary>
    public class DijkstraShortestPaths<TGraph, TVertex>
        where TGraph : IGraph<TVertex>, IWeightedGraph<TVertex>
        where TVertex : IComparable<TVertex>
    {
        #region Constants and Static Readonly

        private const int NilPredecessor = -1;
        private const long Infinity = long.MaxValue;

        #endregion

        public DijkstraShortestPaths(TGraph graph, TVertex source)
        {
            if (graph == null)
            {
                throw new ArgumentNullException(nameof(graph));
            }

            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (!graph.HasVertex(source))
            {
                throw new ArgumentException("The source vertex doesn't belong to graph.");
            }

            if (graph.Edges.Any(edge => edge.Weight < 0))
            {
                throw new ArgumentException("Negative edge weight detected.");
            }

            _graph = graph;
            _source = source;

            _initialize();
            _dijkstra();
        }

        #region Fields and Autoproperties

        private readonly TGraph _graph;
        private readonly TVertex _source;
        private Dictionary<int, TVertex> _indicesToNodes;

        private Dictionary<TVertex, int> _nodesToIndices;
        private int[] _predecessors;

        private long[] _distances;

        private MinPriorityQueue<TVertex, long> _minPriorityQueue;

        #endregion

        /// <summary>
        ///     Returns the distance between the source vertex and the specified vertex.
        /// </summary>
        public long DistanceTo(TVertex destination)
        {
            if (!_nodesToIndices.ContainsKey(destination))
            {
                throw new ArgumentException("Graph doesn't have the specified vertex.");
            }

            var index = _nodesToIndices[destination];
            return _distances[index];
        }

        /// <summary>
        ///     Determines whether there is a path from the source vertex to this specified vertex.
        /// </summary>
        public bool HasPathTo(TVertex destination)
        {
            if (!_nodesToIndices.ContainsKey(destination))
            {
                throw new ArgumentException("Graph doesn't have the specified vertex.");
            }

            var index = _nodesToIndices[destination];
            return _distances[index] != Infinity;
        }

        /// <summary>
        ///     Returns an enumerable collection of nodes that specify the shortest path from the source vertex to the destination vertex.
        /// </summary>
        public IEnumerable<TVertex> ShortestPathTo(TVertex destination)
        {
            if (!_nodesToIndices.ContainsKey(destination))
            {
                throw new ArgumentException("Graph doesn't have the specified vertex.");
            }

            if (!HasPathTo(destination))
            {
                return null;
            }

            var dstIndex = _nodesToIndices[destination];
            var stack = new Stack<TVertex>();

            int index;
            for (index = dstIndex; _distances[index] != 0; index = _predecessors[index])
            {
                stack.Push(_indicesToNodes[index]);
            }

            stack.Push(_indicesToNodes[index]);

            return stack;
        }

        /// <summary>
        ///     The Dijkstra's algorithm.
        /// </summary>
        private void _dijkstra()
        {
            while (!_minPriorityQueue.IsEmpty)
            {
                var currentVertex = _minPriorityQueue.DequeueMin();
                var currentVertexIndex = _nodesToIndices[currentVertex];

                var outgoingEdges = _graph.OutgoingEdges(currentVertex);
                foreach (var outgoingEdge in outgoingEdges)
                {
                    var adjacentIndex = _nodesToIndices[outgoingEdge.Destination];
                    var delta = _distances[currentVertexIndex] != Infinity
                        ? _distances[currentVertexIndex] + outgoingEdge.Weight
                        : Infinity;

                    if (delta < _distances[adjacentIndex])
                    {
                        _distances[adjacentIndex] = delta;
                        _predecessors[adjacentIndex] = currentVertexIndex;

                        if (_minPriorityQueue.Contains(outgoingEdge.Destination))
                        {
                            _minPriorityQueue.UpdatePriority(outgoingEdge.Destination, delta);
                        }
                        else
                        {
                            _minPriorityQueue.Enqueue(outgoingEdge.Destination, delta);
                        }
                    }
                }
            }
        }

        private void _initialize()
        {
            var verticesCount = _graph.VerticesCount;

            _distances = new long[verticesCount];
            _predecessors = new int[verticesCount];

            _nodesToIndices = new Dictionary<TVertex, int>();
            _indicesToNodes = new Dictionary<int, TVertex>();
            _minPriorityQueue = new MinPriorityQueue<TVertex, long>((uint)verticesCount);

            var vertices = _graph.Vertices.ToList();
            for (var i = 0; i < verticesCount; i++)
            {
                if (_source.Equals(vertices[i]))
                {
                    _distances[i] = 0;
                    _predecessors[i] = 0;
                }
                else
                {
                    _distances[i] = Infinity;
                    _predecessors[i] = NilPredecessor;
                }

                _minPriorityQueue.Enqueue(vertices[i], _distances[i]);

                _nodesToIndices.Add(vertices[i], i);
                _indicesToNodes.Add(i, vertices[i]);
            }
        }
    }
}
