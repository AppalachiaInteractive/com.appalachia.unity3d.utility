using System;
using System.Collections.Generic;
using System.Diagnostics;
using Appalachia.Utility.Algorithms.Common;
using Appalachia.Utility.DataStructures.Graphs;

// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace Appalachia.Utility.Algorithms.Graphs
{
    public class BreadthFirstShortestPaths<T>
        where T : IComparable<T>
    {
        #region Constants and Static Readonly

        // A const that represent an infinite distance
        private const long INFINITY = long.MaxValue;

        #endregion

        /// <summary>
        ///     CONSTRUCTOR.
        ///     Breadth First Searcher from Single Source.
        /// </summary>
        public BreadthFirstShortestPaths(IGraph<T> Graph, T Source)
        {
            if (Graph == null)
            {
                throw new ArgumentNullException();
            }

            if (!Graph.HasVertex(Source))
            {
                throw new ArgumentException("The source vertex doesn't belong to graph.");
            }

            // Init
            _initializeDataMembers(Graph);

            // Single source BFS
            _breadthFirstSearch(Graph, Source);

            //bool optimalityConditionsSatisfied = checkOptimalityConditions (Graph, Source);
            Debug.Assert(checkOptimalityConditions(Graph, Source));
        }

        /// <summary>
        ///     CONSTRUCTOR.
        ///     Breadth First Searcher from Multiple Sources.
        /// </summary>
        public BreadthFirstShortestPaths(IGraph<T> Graph, IList<T> Sources)
        {
            if (Graph == null)
            {
                throw new ArgumentNullException();
            }

            if ((Sources == null) || (Sources.Count == 0))
            {
                throw new ArgumentException("Sources list is either null or empty.");
            }

            // Init
            _initializeDataMembers(Graph);

            // Multiple sources BFS
            _breadthFirstSearch(Graph, Sources);
        }

        #region Fields and Autoproperties

        private bool[] _visited { get; set; }

        // A dictionary that maps integer index to node-value
        private Dictionary<int, T> _indicesToNodes { get; set; }

        // A dictionary that maps node-values to integer indeces
        private Dictionary<T, int> _nodesToIndices { get; set; }
        private int _edgesCount { get; set; }
        private int _verticesCount { get; set; }
        private int[] _predecessors { get; set; }
        private long[] _distances { get; set; }

        #endregion

        /// <summary>
        ///     Returns the distance between the source vertex and the specified vertex.
        /// </summary>
        public long DistanceTo(T destination)
        {
            if (!_nodesToIndices.ContainsKey(destination))
            {
                throw new Exception("Graph doesn't have the specified vertex.");
            }

            var dstIndex = _nodesToIndices[destination];
            return _distances[dstIndex];
        }

        /************************************************************************************************************/

        /// <summary>
        ///     Determines whether there is a path from the source vertex to this specified vertex.
        /// </summary>
        public bool HasPathTo(T destination)
        {
            if (!_nodesToIndices.ContainsKey(destination))
            {
                throw new Exception("Graph doesn't have the specified vertex.");
            }

            var dstIndex = _nodesToIndices[destination];
            return _visited[dstIndex];
        }

        /// <summary>
        ///     Returns an enumerable collection of nodes that specify the shortest path from the source vertex to the destination vertex.
        /// </summary>
        public IEnumerable<T> ShortestPathTo(T destination)
        {
            if (!_nodesToIndices.ContainsKey(destination))
            {
                throw new Exception("Graph doesn't have the specified vertex.");
            }

            if (!HasPathTo(destination))
            {
                return null;
            }

            var dstIndex = _nodesToIndices[destination];
            var stack = new Stack<T>();

            int index;
            for (index = dstIndex; _distances[index] != 0; index = _predecessors[index])
            {
                stack.Push(_indicesToNodes[index]);
            }

            // Push the source vertex
            stack.Push(_indicesToNodes[index]);

            return stack;
        }

        /// <summary>
        ///     Privat helper. Breadth First Search from Single Source.
        /// </summary>
        private void _breadthFirstSearch(IGraph<T> graph, T source)
        {
            // Set distance to current to zero
            _distances[_nodesToIndices[source]] = 0;

            // Set current to visited: true.
            _visited[_nodesToIndices[source]] = true;

            var queue = new Queue<T>(_verticesCount);
            queue.Enqueue(source);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                var indexOfCurrent = _nodesToIndices[current];

                foreach (var adjacent in graph.Neighbours(current))
                {
                    var indexOfAdjacent = _nodesToIndices[adjacent];

                    if (!_visited[indexOfAdjacent])
                    {
                        _predecessors[indexOfAdjacent] = indexOfCurrent;
                        _distances[indexOfAdjacent] = _distances[indexOfCurrent] + 1;
                        _visited[indexOfAdjacent] = true;

                        queue.Enqueue(adjacent);
                    }
                } //end-foreach
            }     //end-while
        }

        /// <summary>
        ///     Privat helper. Breadth First Search from Multiple Sources.
        /// </summary>
        private void _breadthFirstSearch(IGraph<T> graph, IList<T> sources)
        {
            // Define helper variables.
            var queue = new Queue<T>(_verticesCount);

            foreach (var source in sources)
            {
                if (!graph.HasVertex(source))
                {
                    throw new Exception("Graph doesn't has a vertex '" + source + "'");
                }

                var index = _nodesToIndices[source];
                _distances[index] = 0;
                _visited[index] = true;
                queue.Enqueue(source);
            }

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                var indexOfCurrent = _nodesToIndices[current];

                foreach (var adjacent in graph.Neighbours(current))
                {
                    var indexOfAdjacent = _nodesToIndices[adjacent];

                    if (!_visited[indexOfAdjacent])
                    {
                        _predecessors[indexOfAdjacent] = indexOfCurrent;
                        _distances[indexOfAdjacent] = _distances[indexOfCurrent] + 1;
                        _visited[indexOfAdjacent] = true;

                        queue.Enqueue(adjacent);
                    }
                } //end-foreach
            }     //end-while
        }

        /************************************************************************************************************/

        /// <summary>
        ///     Constructors helper function. Initializes some of the data memebers.
        /// </summary>
        private void _initializeDataMembers(IGraph<T> Graph)
        {
            _edgesCount = Graph.EdgesCount;
            _verticesCount = Graph.VerticesCount;

            _visited = new bool[_verticesCount];
            _distances = new long[_verticesCount];
            _predecessors = new int[_verticesCount];

            _nodesToIndices = new Dictionary<T, int>();
            _indicesToNodes = new Dictionary<int, T>();

            // Reset the visited, distances and predeccessors arrays
            var i = 0;
            foreach (var node in Graph.Vertices)
            {
                if (i >= _verticesCount)
                {
                    break;
                }

                _visited[i] = false;
                _distances[i] = INFINITY;
                _predecessors[i] = -1;

                _nodesToIndices.Add(node, i);
                _indicesToNodes.Add(i, node);

                ++i;
            }
        }

        /// <summary>
        ///     Private helper. Checks optimality conditions for single source
        /// </summary>
        private bool checkOptimalityConditions(IGraph<T> graph, T source)
        {
            var indexOfSource = _nodesToIndices[source];

            // check that the distance of s = 0
            if (_distances[indexOfSource] != 0)
            {
                Console.WriteLine(
                    "Distance of source '" + source + "' to itself = " + _distances[indexOfSource]
                );
                return false;
            }

            // check that for each edge v-w dist[w] <= dist[v] + 1
            // provided v is reachable from s
            foreach (var node in graph.Vertices)
            {
                var v = _nodesToIndices[node];

                foreach (var adjacent in graph.Neighbours(node))
                {
                    var w = _nodesToIndices[adjacent];

                    if (HasPathTo(node) != HasPathTo(adjacent))
                    {
                        Console.WriteLine("edge " + node + "-" + adjacent);
                        Console.WriteLine("hasPathTo(" + node + ") = " + HasPathTo(node));
                        Console.WriteLine("hasPathTo(" + adjacent + ") = " + HasPathTo(adjacent));
                        return false;
                    }

                    if (HasPathTo(node) && (_distances[w] > (_distances[v] + 1)))
                    {
                        Console.WriteLine("edge " + node + "-" + adjacent);
                        Console.WriteLine("distanceTo[" + node + "] = " + _distances[v]);
                        Console.WriteLine("distanceTo[" + adjacent + "] = " + _distances[w]);
                        return false;
                    }
                }
            }

            // check that v = edgeTo[w] satisfies distTo[w] + distTo[v] + 1
            // provided v is reachable from source
            foreach (var node in graph.Vertices)
            {
                var w = _nodesToIndices[node];

                if (!HasPathTo(node) || node.IsEqualTo(source))
                {
                    continue;
                }

                var v = _predecessors[w];

                if (_distances[w] != (_distances[v] + 1))
                {
                    Console.WriteLine("shortest path edge " + v + "-" + w);
                    Console.WriteLine("distanceTo[" + v + "] = " + _distances[v]);
                    Console.WriteLine("distanceTo[" + w + "] = " + _distances[w]);
                    return false;
                }
            }

            return true;
        }
    }
}
