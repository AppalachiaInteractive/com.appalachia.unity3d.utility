﻿using System;
using System.Collections.Generic;
using Appalachia.Utility.DataStructures.Lists;

namespace Appalachia.Utility.DataStructures.Graphs
{
    public interface IGraph<T>
        where T : IComparable<T>
    {
        /// <summary>
        ///     Returns true, if graph is directed; false otherwise.
        /// </summary>
        bool IsDirected { get; }

        /// <summary>
        ///     Returns true, if graph is weighted; false otherwise.
        /// </summary>
        bool IsWeighted { get; }

        /// <summary>
        ///     An enumerable collection of edges.
        /// </summary>
        IEnumerable<IEdge<T>> Edges { get; }

        /// <summary>
        ///     Returns the list of Vertices.
        /// </summary>
        IEnumerable<T> Vertices { get; }

        /// <summary>
        ///     Gets the count of edges.
        /// </summary>
        int EdgesCount { get; }

        /// <summary>
        ///     Gets the count of vetices.
        /// </summary>
        int VerticesCount { get; }

        /// <summary>
        ///     Connects two vertices together.
        /// </summary>
        bool AddEdge(T firstVertex, T secondVertex);

        /// <summary>
        ///     Adds a new vertex to graph.
        /// </summary>
        bool AddVertex(T vertex);

        /// <summary>
        ///     Adds a list of vertices to the graph.
        /// </summary>
        void AddVertices(IList<T> collection);

        /// <summary>
        ///     A breadth first search traversal of the graph. Prints nodes as they get visited.
        ///     It considers the first inserted vertex as the start-vertex for the walk.
        /// </summary>
        IEnumerable<T> BreadthFirstWalk();

        /// <summary>
        ///     A breadth first search traversal of the graph, starting from a specified vertex. Prints nodes as they get visited.
        /// </summary>
        IEnumerable<T> BreadthFirstWalk(T startingVertex);

        /// <summary>
        ///     Clear this graph.
        /// </summary>
        void Clear();

        /// <summary>
        ///     Returns the degree of the specified vertex.
        /// </summary>
        int Degree(T vertex);

        /// <summary>
        ///     A depth first search traversal of the graph. Prints nodes as they get visited.
        ///     It considers the first inserted vertex as the start-vertex for the walk.
        /// </summary>
        IEnumerable<T> DepthFirstWalk();

        /// <summary>
        ///     A depth first search traversal of the graph, starting from a specified vertex. Prints nodes as they get visited.
        /// </summary>
        IEnumerable<T> DepthFirstWalk(T startingVertex);

        /// <summary>
        ///     Checks whether two vertices are connected (there is an edge between firstVertex & secondVertex)
        /// </summary>
        bool HasEdge(T firstVertex, T secondVertex);

        /// <summary>
        ///     Determines whether this graph has the specified vertex.
        /// </summary>
        bool HasVertex(T vertex);

        /// <summary>
        ///     Get all incoming edges from vertex
        /// </summary>
        IEnumerable<IEdge<T>> IncomingEdges(T vertex);

        /// <summary>
        ///     Returns the neighbours doubly-linked list for the specified vertex.
        /// </summary>
        DLinkedList<T> Neighbours(T vertex);

        /// <summary>
        ///     Get all outgoing edges from vertex
        /// </summary>
        IEnumerable<IEdge<T>> OutgoingEdges(T vertex);

        /// <summary>
        ///     Deletes an edge, if exists, between two vertices.
        /// </summary>
        bool RemoveEdge(T firstVertex, T secondVertex);

        /// <summary>
        ///     Removes the specified vertex from graph.
        /// </summary>
        bool RemoveVertex(T vertex);

        /// <summary>
        ///     Returns a human-readable string of the graph.
        /// </summary>
        string ToReadable();
    }
}
