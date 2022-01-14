using System;
using System.Collections.Generic;
using System.Linq;
using Appalachia.Utility.DataStructures.Extensions;
using Appalachia.Utility.DataStructures.Lists;
using Appalachia.Utility.Strings;
using Unity.Profiling;

namespace Appalachia.Utility.DataStructures.Graphs
{
    public class DirectedSparseGraph<T> : IGraph<T>
        where T : IComparable<T>
    {
        /// <summary>
        ///     CONSTRUCTOR
        /// </summary>
        public DirectedSparseGraph() : this(10)
        {
        }

        public DirectedSparseGraph(uint initialCapacity)
        {
            _edgesCount = 0;
            _adjacencyList = new Dictionary<T, DLinkedList<T>>((int)initialCapacity);
        }

        #region Fields and Autoproperties

        protected virtual Dictionary<T, DLinkedList<T>> _adjacencyList { get; set; }

        /// <summary>
        ///     INSTANCE VARIABLES
        /// </summary>
        protected virtual int _edgesCount { get; set; }

        protected virtual T _firstInsertedNode { get; set; }

        #endregion

        /// <summary>
        ///     An enumerable collection of all directed unweighted edges in graph.
        /// </summary>
        public virtual IEnumerable<UnweightedEdge<T>> Edges
        {
            get
            {
                foreach (var vertex in _adjacencyList)
                foreach (var adjacent in vertex.Value)
                {
                    yield return new UnweightedEdge<T>(
                        vertex.Key, // from
                        adjacent    // to
                    );
                }
            }
        }

        /// <summary>
        ///     Get all incoming directed unweighted edges to a vertex.
        /// </summary>
        public virtual IEnumerable<UnweightedEdge<T>> IncomingEdges(T vertex)
        {
            if (!HasVertex(vertex))
            {
                throw new KeyNotFoundException("Vertex doesn't belong to graph.");
            }

            foreach (var adjacent in _adjacencyList.Keys)
            {
                if (_adjacencyList[adjacent].Contains(vertex))
                {
                    yield return new UnweightedEdge<T>(
                        adjacent, // from
                        vertex    // to
                    );
                }
            } //end-foreach
        }

        /// <summary>
        ///     Get all outgoing directed unweighted edges from a vertex.
        /// </summary>
        public virtual IEnumerable<UnweightedEdge<T>> OutgoingEdges(T vertex)
        {
            if (!HasVertex(vertex))
            {
                throw new KeyNotFoundException("Vertex doesn't belong to graph.");
            }

            foreach (var adjacent in _adjacencyList[vertex])
            {
                yield return new UnweightedEdge<T>(
                    vertex,  // from
                    adjacent // to
                );
            }
        }

        /// <summary>
        ///     Helper function. Checks if edge exist in graph.
        /// </summary>
        protected virtual bool DoesEdgeExist(T vertex1, T vertex2)
        {
            using (_PRF_DoesEdgeExist.Auto())
            {
                return _adjacencyList[vertex1].Contains(vertex2);
            }
        }

        #region IGraph<T> Members

        /// <summary>
        ///     Returns true, if graph is directed; false otherwise.
        /// </summary>
        public virtual bool IsDirected => true;

        /// <summary>
        ///     Returns true, if graph is weighted; false otherwise.
        /// </summary>
        public virtual bool IsWeighted => false;

        /// <summary>
        ///     Gets the count of vetices.
        /// </summary>
        public virtual int VerticesCount => _adjacencyList.Count;

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
                foreach (var vertex in _adjacencyList)
                {
                    yield return vertex.Key;
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
        ///     Connects two vertices together in the direction: first->second.
        /// </summary>
        public virtual bool AddEdge(T source, T destination)
        {
            using (_PRF_AddEdge.Auto())
            {
                // Check existence of nodes and non-existence of edge
                if (!HasVertex(source) || !HasVertex(destination))
                {
                    return false;
                }

                if (DoesEdgeExist(source, destination))
                {
                    return false;
                }

                // Add edge from source to destination
                _adjacencyList[source].Append(destination);

                // Increment edges count
                ++_edgesCount;

                return true;
            }
        }

        /// <summary>
        ///     Removes edge, if exists, from source to destination.
        /// </summary>
        public virtual bool RemoveEdge(T source, T destination)
        {
            using (_PRF_RemoveEdge.Auto())
            {
                // Check existence of nodes and non-existence of edge
                if (!HasVertex(source) || !HasVertex(destination))
                {
                    return false;
                }

                if (!DoesEdgeExist(source, destination))
                {
                    return false;
                }

                // Remove edge from source to destination
                _adjacencyList[source].Remove(destination);

                // Decrement the edges count
                --_edgesCount;

                return true;
            }
        }

        /// <summary>
        ///     Add a collection of vertices to the graph.
        /// </summary>
        public virtual void AddVertices(IList<T> collection)
        {
            using (_PRF_AddVertices.Auto())
            {
                if (collection == null)
                {
                    throw new ArgumentNullException();
                }

                foreach (var vertex in collection)
                {
                    AddVertex(vertex);
                }
            }
        }

        /// <summary>
        ///     Add vertex to the graph
        /// </summary>
        public virtual bool AddVertex(T vertex)
        {
            using (_PRF_AddVertex.Auto())
            {
                if (HasVertex(vertex))
                {
                    return false;
                }

                if (_adjacencyList.Count == 0)
                {
                    _firstInsertedNode = vertex;
                }

                _adjacencyList.Add(vertex, new DLinkedList<T>());

                return true;
            }
        }

        /// <summary>
        ///     Removes the specified vertex from graph.
        /// </summary>
        public virtual bool RemoveVertex(T vertex)
        {
            using (_PRF_RemoveVertex.Auto())
            {
                // Check existence of vertex
                if (!HasVertex(vertex))
                {
                    return false;
                }

                // Subtract the number of edges for this vertex from the total edges count
                _edgesCount = _edgesCount - _adjacencyList[vertex].Count;

                // Remove vertex from graph
                _adjacencyList.Remove(vertex);

                // Remove destination edges to this vertex
                foreach (var adjacent in _adjacencyList)
                {
                    if (adjacent.Value.Contains(vertex))
                    {
                        adjacent.Value.Remove(vertex);

                        // Decrement the edges count.
                        --_edgesCount;
                    }
                }

                return true;
            }
        }

        /// <summary>
        ///     Checks whether there is an edge from source to destination.
        /// </summary>
        public virtual bool HasEdge(T source, T destination)
        {
            using (_PRF_HasEdge.Auto())
            {
                return _adjacencyList.ContainsKey(source) &&
                       _adjacencyList.ContainsKey(destination) &&
                       DoesEdgeExist(source, destination);
            }
        }

        /// <summary>
        ///     Checks whether a vertex exists in the graph
        /// </summary>
        public virtual bool HasVertex(T vertex)
        {
            using (_PRF_HasVertex.Auto())
            {
                return _adjacencyList.ContainsKey(vertex);
            }
        }

        /// <summary>
        ///     Returns the neighbours doubly-linked list for the specified vertex.
        /// </summary>
        public virtual DLinkedList<T> Neighbours(T vertex)
        {
            using (_PRF_Neighbours.Auto())
            {
                if (!HasVertex(vertex))
                {
                    return null;
                }

                return _adjacencyList[vertex];
            }
        }

        /// <summary>
        ///     Returns the degree of the specified vertex.
        /// </summary>
        public virtual int Degree(T vertex)
        {
            using (_PRF_Degree.Auto())
            {
                if (!HasVertex(vertex))
                {
                    throw new KeyNotFoundException();
                }

                return _adjacencyList[vertex].Count;
            }
        }

        private const string FORMAT_1 = "{0}\r\n{1}: [";
        private const string FORMAT_2 = "{0}{1},";
        private const string FORMAT_3 = "{0}{1}]";

        public virtual string ToReadable()
        {
            return ToReadable(false);
        }

        /// <summary>
        ///     Returns a human-readable string of the graph.
        /// </summary>
        public virtual string ToReadable(bool reverse)
        {
            using (_PRF_ToReadable.Auto())
            {
                var output = string.Empty;

                foreach (var node in reverse ? _adjacencyList.Reverse() : _adjacencyList)
                {
                    var adjacents = string.Empty;

                    output = ZString.Format(FORMAT_1, output, node.Key);

                    foreach (var adjacentNode in node.Value)
                    {
                        adjacents = ZString.Format(FORMAT_2, adjacents, adjacentNode);
                    }

                    if (adjacents.Length > 0)
                    {
                        adjacents = adjacents.TrimEnd(',', ' ');
                    }

                    output = ZString.Format(FORMAT_3, output, adjacents);
                }

                return output;
            }
        }

        /// <summary>
        ///     A depth first search traversal of the graph starting from the first inserted node.
        ///     Returns the visited vertices of the graph.
        /// </summary>
        public virtual IEnumerable<T> DepthFirstWalk()
        {
            using (_PRF_DepthFirstWalk.Auto())
            {
                return DepthFirstWalk(_firstInsertedNode);
            }
        }

        /// <summary>
        ///     A depth first search traversal of the graph, starting from a specified vertex.
        ///     Returns the visited vertices of the graph.
        /// </summary>
        public virtual IEnumerable<T> DepthFirstWalk(T source)
        {
            using (_PRF_DepthFirstWalk.Auto())
            {
                // Check for existence of source
                if (VerticesCount == 0)
                {
                    return new List<T>(0);
                }

                if (!HasVertex(source))
                {
                    throw new KeyNotFoundException("The source vertex doesn't exist.");
                }

                var visited = new HashSet<T>();
                var stack = new Stack<T>();
                var listOfNodes = new List<T>(VerticesCount);

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
        }

        /// <summary>
        ///     A breadth first search traversal of the graphstarting from the first inserted node.
        ///     Returns the visited vertices of the graph.
        /// </summary>
        public virtual IEnumerable<T> BreadthFirstWalk()
        {
            using (_PRF_BreadthFirstWalk.Auto())
            {
                return BreadthFirstWalk(_firstInsertedNode);
            }
        }

        /// <summary>
        ///     A breadth first search traversal of the graph, starting from a specified vertex.
        ///     Returns the visited vertices of the graph.
        /// </summary>
        public virtual IEnumerable<T> BreadthFirstWalk(T source)
        {
            using (_PRF_BreadthFirstWalk.Auto())
            {
                // Check for existence of source
                if (VerticesCount == 0)
                {
                    return new List<T>(0);
                }

                if (!HasVertex(source))
                {
                    throw new KeyNotFoundException("The source vertex doesn't exist.");
                }

                var visited = new HashSet<T>();
                var queue = new Queue<T>();
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
        }

        /// <summary>
        ///     Clear this graph.
        /// </summary>
        public virtual void Clear()
        {
            using (_PRF_Clear.Auto())
            {
                _edgesCount = 0;
                _adjacencyList.Clear();
            }
        }

        #endregion

        #region Profiling

        private const string _PRF_PFX = nameof(DirectedSparseGraph<T>) + ".";

        private static readonly ProfilerMarker _PRF_DoesEdgeExist =
            new ProfilerMarker(_PRF_PFX + nameof(DoesEdgeExist));

        private static readonly ProfilerMarker _PRF_AddEdge = new ProfilerMarker(_PRF_PFX + nameof(AddEdge));

        private static readonly ProfilerMarker _PRF_RemoveEdge =
            new ProfilerMarker(_PRF_PFX + nameof(RemoveEdge));

        private static readonly ProfilerMarker _PRF_AddVertices =
            new ProfilerMarker(_PRF_PFX + nameof(AddVertices));

        private static readonly ProfilerMarker _PRF_AddVertex =
            new ProfilerMarker(_PRF_PFX + nameof(AddVertex));

        private static readonly ProfilerMarker _PRF_RemoveVertex =
            new ProfilerMarker(_PRF_PFX + nameof(RemoveVertex));

        private static readonly ProfilerMarker _PRF_HasVertex =
            new ProfilerMarker(_PRF_PFX + nameof(HasVertex));

        private static readonly ProfilerMarker _PRF_HasEdge = new ProfilerMarker(_PRF_PFX + nameof(HasEdge));

        private static readonly ProfilerMarker _PRF_Neighbours =
            new ProfilerMarker(_PRF_PFX + nameof(Neighbours));

        private static readonly ProfilerMarker _PRF_Degree = new ProfilerMarker(_PRF_PFX + nameof(Degree));

        private static readonly ProfilerMarker _PRF_ToReadable =
            new ProfilerMarker(_PRF_PFX + nameof(ToReadable));

        private static readonly ProfilerMarker _PRF_DepthFirstWalk =
            new ProfilerMarker(_PRF_PFX + nameof(DepthFirstWalk));

        private static readonly ProfilerMarker _PRF_Clear = new ProfilerMarker(_PRF_PFX + nameof(Clear));

        private static readonly ProfilerMarker _PRF_BreadthFirstWalk =
            new ProfilerMarker(_PRF_PFX + nameof(BreadthFirstWalk));

        #endregion
    }
}
