﻿using System;
using System.Collections.Generic;
using Appalachia.Utility.DataStructures.Common;
using Appalachia.Utility.DataStructures.Extensions;
using Appalachia.Utility.DataStructures.Lists;

namespace Appalachia.Utility.DataStructures.Graphs
{
    public class UndirectedDenseGraph<T> : IGraph<T>
        where T : IComparable<T>
    {
        #region Constants and Static Readonly

        /// <summary>
        ///     INSTANCE VARIABLES
        /// </summary>
        protected const object EMPTY_VERTEX_SLOT = null;

        #endregion

        /// <summary>
        ///     CONSTRUCTORS
        /// </summary>
        public UndirectedDenseGraph(uint capacity = 10)
        {
            _edgesCount = 0;
            _verticesCount = 0;
            _verticesCapacity = (int)capacity;

            _vertices = new List<object>(_verticesCapacity);
            _adjacencyMatrix = new bool[_verticesCapacity, _verticesCapacity];
            _adjacencyMatrix.Populate(_verticesCapacity, _verticesCapacity);
        }

        #region Fields and Autoproperties

        protected virtual List<object> _vertices { get; set; }
        protected virtual bool[,] _adjacencyMatrix { get; set; }

        protected virtual int _edgesCount { get; set; }
        protected virtual int _verticesCapacity { get; set; }
        protected virtual int _verticesCount { get; set; }
        protected virtual T _firstInsertedNode { get; set; }

        #endregion

        /// <summary>
        ///     An enumerable collection of edges.
        /// </summary>
        public virtual IEnumerable<UnweightedEdge<T>> Edges
        {
            get
            {
                var seen = new HashSet<KeyValuePair<T, T>>();

                foreach (var vertex in _vertices)
                {
                    var source = _vertices.IndexOf(vertex);

                    for (var adjacent = 0; adjacent < _vertices.Count; ++adjacent)
                    {
                        // Check existence of vertex
                        if ((_vertices[adjacent] != null) && _doesEdgeExist(source, adjacent))
                        {
                            var neighbor = (T)_vertices[adjacent];

                            var outgoingEdge = new KeyValuePair<T, T>((T)vertex, neighbor);
                            var incomingEdge = new KeyValuePair<T, T>(neighbor,  (T)vertex);

                            if (seen.Contains(incomingEdge) || seen.Contains(outgoingEdge))
                            {
                                continue;
                            }

                            seen.Add(outgoingEdge);

                            yield return new UnweightedEdge<T>(outgoingEdge.Key, outgoingEdge.Value);
                        }
                    }
                } //end-foreach
            }
        }

        /// <summary>
        ///     Get all incoming edges to a vertex
        /// </summary>
        public IEnumerable<UnweightedEdge<T>> IncomingEdges(T vertex)
        {
            if (!HasVertex(vertex))
            {
                throw new KeyNotFoundException("Vertex doesn't belong to graph.");
            }

            var source = _vertices.IndexOf(vertex);

            for (var adjacent = 0; adjacent < _vertices.Count; ++adjacent)
            {
                if ((_vertices[adjacent] != null) && _doesEdgeExist(source, adjacent))
                {
                    yield return new UnweightedEdge<T>(
                        (T)_vertices[adjacent], // from
                        vertex                  // to
                    );
                }
            } //end-for
        }

        /// <summary>
        ///     Get all outgoing edges from a vertex.
        /// </summary>
        public IEnumerable<UnweightedEdge<T>> OutgoingEdges(T vertex)
        {
            if (!HasVertex(vertex))
            {
                throw new KeyNotFoundException("Vertex doesn't belong to graph.");
            }

            var source = _vertices.IndexOf(vertex);

            for (var adjacent = 0; adjacent < _vertices.Count; ++adjacent)
            {
                if ((_vertices[adjacent] != null) && _doesEdgeExist(source, adjacent))
                {
                    yield return new UnweightedEdge<T>(
                        vertex,                // from
                        (T)_vertices[adjacent] // to
                    );
                }
            } //end-for
        }

        /// <summary>
        ///     Helper function. Checks if edge exist in graph.
        /// </summary>
        protected virtual bool _doesEdgeExist(int index1, int index2)
        {
            return _adjacencyMatrix[index1, index2] || _adjacencyMatrix[index2, index1];
        }

        /// <summary>
        ///     Helper function that checks whether a vertex exist.
        /// </summary>
        protected virtual bool _doesVertexExist(T vertex)
        {
            return _vertices.Contains(vertex);
        }

        #region IGraph<T> Members

        /// <summary>
        ///     Returns true, if graph is directed; false otherwise.
        /// </summary>
        public virtual bool IsDirected => false;

        /// <summary>
        ///     Returns true, if graph is weighted; false otherwise.
        /// </summary>
        public virtual bool IsWeighted => false;

        /// <summary>
        ///     Gets the count of vetices.
        /// </summary>
        public virtual int VerticesCount => _verticesCount;

        /// <summary>
        ///     Gets the count of edges.
        /// </summary>
        public virtual int EdgesCount => _edgesCount;

        /// <summary>
        ///     Returns the list of Vertices.
        /// </summary>
        public virtual IEnumerable<T> Vertices
        {
            get
            {
                foreach (var item in _vertices)
                {
                    if (item != null)
                    {
                        yield return (T)item;
                    }
                }
            }
        }

        IEnumerable<IEdge<T>> IGraph<T>.Edges => Edges;

        IEnumerable<IEdge<T>> IGraph<T>.IncomingEdges(T vertex)
        {
            return IncomingEdges(vertex);
        }

        IEnumerable<IEdge<T>> IGraph<T>.OutgoingEdges(T vertex)
        {
            return OutgoingEdges(vertex);
        }

        /// <summary>
        ///     Connects two vertices together.
        /// </summary>
        public virtual bool AddEdge(T firstVertex, T secondVertex)
        {
            var indexOfFirst = _vertices.IndexOf(firstVertex);
            var indexOfSecond = _vertices.IndexOf(secondVertex);

            if ((indexOfFirst == -1) || (indexOfSecond == -1))
            {
                return false;
            }

            if (_doesEdgeExist(indexOfFirst, indexOfSecond))
            {
                return false;
            }

            _adjacencyMatrix[indexOfFirst, indexOfSecond] = true;
            _adjacencyMatrix[indexOfSecond, indexOfFirst] = true;

            // Increment the edges count.
            ++_edgesCount;

            return true;
        }

        /// <summary>
        ///     Deletes an edge, if exists, between two vertices.
        /// </summary>
        public virtual bool RemoveEdge(T firstVertex, T secondVertex)
        {
            var indexOfFirst = _vertices.IndexOf(firstVertex);
            var indexOfSecond = _vertices.IndexOf(secondVertex);

            if ((indexOfFirst == -1) || (indexOfSecond == -1))
            {
                return false;
            }

            if (!_doesEdgeExist(indexOfFirst, indexOfSecond))
            {
                return false;
            }

            _adjacencyMatrix[indexOfFirst, indexOfSecond] = false;
            _adjacencyMatrix[indexOfSecond, indexOfFirst] = false;

            // Decrement the edges count.
            --_edgesCount;

            return true;
        }

        /// <summary>
        ///     Adds a list of vertices to the graph.
        /// </summary>
        public virtual void AddVertices(IList<T> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException();
            }

            foreach (var item in collection)
            {
                AddVertex(item);
            }
        }

        /// <summary>
        ///     Adds a new vertex to graph.
        /// </summary>
        public virtual bool AddVertex(T vertex)
        {
            // Return if graph reached it's maximum capacity
            if (_verticesCount >= _verticesCapacity)
            {
                return false;
            }

            // Return if vertex exists
            if (_doesVertexExist(vertex))
            {
                return false;
            }

            // Initialize first inserted node
            if (_verticesCount == 0)
            {
                _firstInsertedNode = vertex;
            }

            // Try inserting vertex at previously lazy-deleted slot
            var indexOfDeleted = _vertices.IndexOf(EMPTY_VERTEX_SLOT);

            if (indexOfDeleted != -1)
            {
                _vertices[indexOfDeleted] = vertex;
            }
            else
            {
                _vertices.Add(vertex);
            }

            // Increment the vertices count
            ++_verticesCount;

            return true;
        }

        /// <summary>
        ///     Removes the specified vertex from graph.
        /// </summary>
        public virtual bool RemoveVertex(T vertex)
        {
            // Return if graph is empty
            if (_verticesCount == 0)
            {
                return false;
            }

            // Get index of vertex
            var index = _vertices.IndexOf(vertex);

            // Return if vertex doesn't exists
            if (index == -1)
            {
                return false;
            }

            // Lazy-delete the vertex from graph
            //_vertices.Remove (vertex);
            _vertices[index] = EMPTY_VERTEX_SLOT;

            // Decrement the vertices count
            --_verticesCount;

            // Delete the edges
            for (var i = 0; i < _verticesCapacity; ++i)
            {
                if (_doesEdgeExist(index, i))
                {
                    _adjacencyMatrix[index, i] = false;
                    _adjacencyMatrix[i, index] = false;

                    // Decrement the edges count
                    --_edgesCount;
                }
            }

            return true;
        }

        /// <summary>
        ///     Checks whether two vertices are connected (there is an edge between firstVertex & secondVertex)
        /// </summary>
        public virtual bool HasEdge(T firstVertex, T secondVertex)
        {
            var indexOfFirst = _vertices.IndexOf(firstVertex);
            var indexOfSecond = _vertices.IndexOf(secondVertex);

            // Check the existence of vertices and the directed edge
            return (indexOfFirst != -1) &&
                   (indexOfSecond != -1) &&
                   _doesEdgeExist(indexOfFirst, indexOfSecond);
        }

        /// <summary>
        ///     Determines whether this graph has the specified vertex.
        /// </summary>
        public virtual bool HasVertex(T vertex)
        {
            return _vertices.Contains(vertex);
        }

        /// <summary>
        ///     Returns the neighbours doubly-linked list for the specified vertex.
        /// </summary>
        public virtual DLinkedList<T> Neighbours(T vertex)
        {
            var neighbours = new DLinkedList<T>();
            var source = _vertices.IndexOf(vertex);

            if (source != -1)
            {
                for (var adjacent = 0; adjacent < _vertices.Count; ++adjacent)
                {
                    if ((_vertices[adjacent] != null) && _doesEdgeExist(source, adjacent))
                    {
                        neighbours.Append((T)_vertices[adjacent]);
                    }
                }
            }

            return neighbours;
        }

        /// <summary>
        ///     Returns the degree of the specified vertex.
        /// </summary>
        public virtual int Degree(T vertex)
        {
            if (!HasVertex(vertex))
            {
                throw new KeyNotFoundException();
            }

            return Neighbours(vertex).Count;
        }

        /// <summary>
        ///     Returns a human-readable string of the graph.
        /// </summary>
        public virtual string ToReadable()
        {
            var output = string.Empty;

            for (var i = 0; i < _vertices.Count; ++i)
            {
                if (_vertices[i] == null)
                {
                    continue;
                }

                var node = (T)_vertices[i];
                var adjacents = string.Empty;

                output = string.Format("{0}\r\n{1}: [", output, node);

                foreach (var adjacentNode in Neighbours(node))
                {
                    adjacents = string.Format("{0}{1},", adjacents, adjacentNode);
                }

                if (adjacents.Length > 0)
                {
                    adjacents = adjacents.TrimEnd(',', ' ');
                }

                output = string.Format("{0}{1}]", output, adjacents);
            }

            return output;
        }

        /// <summary>
        ///     A depth first search traversal of the graph starting from the first inserted node.
        ///     Returns the visited vertices of the graph.
        /// </summary>
        public virtual IEnumerable<T> DepthFirstWalk()
        {
            return DepthFirstWalk(_firstInsertedNode);
        }

        /// <summary>
        ///     A depth first search traversal of the graph, starting from a specified vertex.
        ///     Returns the visited vertices of the graph.
        /// </summary>
        public virtual IEnumerable<T> DepthFirstWalk(T source)
        {
            if (_verticesCount == 0)
            {
                return new List<T>();
            }

            if (!HasVertex(source))
            {
                throw new Exception("The specified starting vertex doesn't exist.");
            }

            var stack = new Stack<T>(_verticesCount);
            var visited = new HashSet<T>();
            var listOfNodes = new List<T>(_verticesCount);

            stack.Push(source);

            while (!stack.IsEmpty())
            {
                var current = stack.Pop();

                if (!visited.Contains(current))
                {
                    listOfNodes.Add(current);
                    visited.Add(current);

                    foreach (var adjacent in Neighbours(current))
                    {
                        if (!visited.Contains(adjacent))
                        {
                            stack.Push(adjacent);
                        }
                    }
                }
            }

            return listOfNodes;
        }

        /// <summary>
        ///     A breadth first search traversal of the graphstarting from the first inserted node.
        ///     Returns the visited vertices of the graph.
        /// </summary>
        public virtual IEnumerable<T> BreadthFirstWalk()
        {
            return BreadthFirstWalk(_firstInsertedNode);
        }

        /// <summary>
        ///     A breadth first search traversal of the graph, starting from a specified vertex.
        ///     Returns the visited vertices of the graph.
        /// </summary>
        public virtual IEnumerable<T> BreadthFirstWalk(T source)
        {
            if (_verticesCount == 0)
            {
                return new List<T>();
            }

            if (!HasVertex(source))
            {
                throw new Exception("The specified starting vertex doesn't exist.");
            }

            var visited = new HashSet<T>();
            var queue = new Queue<T>(VerticesCount);
            var listOfNodes = new List<T>(VerticesCount);

            listOfNodes.Add(source);
            visited.Add(source);

            queue.Enqueue(source);

            while (!queue.IsEmpty())
            {
                var current = queue.Dequeue();
                var neighbors = Neighbours(current);

                foreach (var adjacent in neighbors)
                {
                    if (!visited.Contains(adjacent))
                    {
                        listOfNodes.Add(adjacent);
                        visited.Add(adjacent);
                        queue.Enqueue(adjacent);
                    }
                }
            }

            return listOfNodes;
        }

        /// <summary>
        ///     Clear this graph.
        /// </summary>
        public virtual void Clear()
        {
            _edgesCount = 0;
            _verticesCount = 0;
            _vertices.Clear();
            _adjacencyMatrix = new bool[_verticesCapacity, _verticesCapacity];
            _adjacencyMatrix.Populate(_verticesCapacity, _verticesCapacity);
        }

        #endregion
    }
}
