using System;
using System.Collections.Generic;
using Appalachia.Utility.Algorithms.Common;

// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace Appalachia.Utility.Algorithms.Sorting
{
    public static class BinarySearchTreeSorter
    {
        /// <summary>
        ///     Unbalanced Binary Search Tree sort.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        public static void UnbalancedBSTSort<T>(this List<T> collection)
            where T : IComparable<T>
        {
            if (collection.Count == 0)
            {
                return;
            }

            var treeRoot = new Node<T> { Value = collection[0] };

            // Get a handle on root.
            for (var i = 1; i < collection.Count; ++i)
            {
                var currentNode = treeRoot;
                var newNode = new Node<T> { Value = collection[i] };

                while (true)
                {
                    // Go left
                    if (newNode.Value.IsLessThan(currentNode.Value))
                    {
                        if (currentNode.Left == null)
                        {
                            newNode.Parent = currentNode;
                            currentNode.Left = newNode;
                            break;
                        }

                        currentNode = currentNode.Left;
                    }

                    // Go right
                    else
                    {
                        if (currentNode.Right == null)
                        {
                            newNode.Parent = currentNode;
                            currentNode.Right = newNode;
                            break;
                        }

                        currentNode = currentNode.Right;
                    }
                } //end-while
            }     //end-for

            // Reference handle to root again.
            collection.Clear();
            var treeRootReference = treeRoot;
            _inOrderTravelAndAdd(treeRootReference, ref collection);

            treeRootReference = treeRoot = null;
        }

        /// <summary>
        ///     Used to travel a node's subtrees and add the elements to the collection.
        /// </summary>
        /// <typeparam name="T">Type of tree elements.</typeparam>
        /// <param name="currentNode">Node to start from.</param>
        /// <param name="collection">Collection to add elements to.</param>
        private static void _inOrderTravelAndAdd<T>(Node<T> currentNode, ref List<T> collection)
            where T : IComparable<T>
        {
            if (currentNode == null)
            {
                return;
            }

            _inOrderTravelAndAdd(currentNode.Left, ref collection);
            collection.Add(currentNode.Value);
            _inOrderTravelAndAdd(currentNode.Right, ref collection);
        }

        #region Nested type: Node

        /// <summary>
        ///     Minimal BST Node class, used only for unbalanced binary search tree sort.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        private class Node<T> : IComparable<Node<T>>
            where T : IComparable<T>
        {
            public Node()
            {
                Value = default(T);
                Parent = null;
                Left = null;
                Right = null;
            }

            #region Fields and Autoproperties

            public Node<T> Left { get; set; }
            public Node<T> Parent { get; set; }
            public Node<T> Right { get; set; }
            public T Value { get; set; }

            #endregion

            #region IComparable<Node<T>> Members

            public int CompareTo(Node<T> other)
            {
                if (other == null)
                {
                    return -1;
                }

                return Value.CompareTo(other.Value);
            }

            #endregion
        }

        #endregion
    }
}
