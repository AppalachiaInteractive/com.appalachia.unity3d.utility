using System;
using System.Collections.Generic;
using Appalachia.Utility.DataStructures.Common;
using Appalachia.Utility.DataStructures.Extensions;

// ReSharper disable NotResolvedInText

namespace Appalachia.Utility.DataStructures.Heaps
{
    /// <summary>
    ///     Maximum Heap Data Structure.
    /// </summary>
    public class BinaryMaxHeap<T> : IMaxHeap<T>
        where T : IComparable<T>
    {
        /// <summary>
        ///     CONSTRUCTORS
        /// </summary>
        public BinaryMaxHeap() : this(0, null)
        {
        }

        public BinaryMaxHeap(Comparer<T> comparer) : this(0, comparer)
        {
        }

        public BinaryMaxHeap(int capacity, Comparer<T> comparer)
        {
            _collection = new List<T>(capacity);
            _heapComparer = Comparer<T>.Default;
            _heapComparer = comparer ?? Comparer<T>.Default;
        }

        #region Fields and Autoproperties

        private Comparer<T> _heapComparer;

        /// <summary>
        ///     Instance Variables.
        ///     _collection: The list of elements. Implemented as an array-based list with auto-resizing.
        /// </summary>
        private List<T> _collection { get; set; }

        #endregion

        /// <summary>
        ///     Gets or sets the at the specified index.
        /// </summary>
        public T this[int index]
        {
            get
            {
                if ((index < 0) || (index > Count) || (Count == 0))
                {
                    throw new IndexOutOfRangeException();
                }

                return _collection[index];
            }
            set
            {
                if ((index < 0) || (index >= Count))
                {
                    throw new IndexOutOfRangeException();
                }

                _collection[index] = value;

                if ((index != 0) &&
                    (_heapComparer.Compare(
                         _collection[index],
                         _collection[(index - 1) / 2]
                     ) >
                     0)) // greater than or equal to max
                {
                    _siftUp(index);
                }
                else
                {
                    _maxHeapify(index, _collection.Count - 1);
                }
            }
        }

        /// <summary>
        ///     Union two heaps together, returns a new min-heap of both heaps' elements,
        ///     ... and then destroys the original ones.
        /// </summary>
        public BinaryMaxHeap<T> Union(ref BinaryMaxHeap<T> firstMaxHeap, ref BinaryMaxHeap<T> secondMaxHeap)
        {
            if ((firstMaxHeap == null) || (secondMaxHeap == null))
            {
                throw new ArgumentNullException("Null heaps are not allowed.");
            }

            // Create a new heap with reserved size.
            var size = firstMaxHeap.Count + secondMaxHeap.Count;
            var newHeap = new BinaryMaxHeap<T>(size, Comparer<T>.Default);

            // Insert into the new heap.
            while (firstMaxHeap.IsEmpty == false)
            {
                newHeap.Add(firstMaxHeap.ExtractMax());
            }

            while (secondMaxHeap.IsEmpty == false)
            {
                newHeap.Add(secondMaxHeap.ExtractMax());
            }

            // Destroy the two heaps.
            firstMaxHeap = secondMaxHeap = null;

            return newHeap;
        }

        /// <summary>
        ///     Private Method. Builds a max heap from the inner array-list _collection.
        /// </summary>
        private void _buildMaxHeap()
        {
            var lastIndex = _collection.Count - 1;
            var lastNodeWithChildren = lastIndex / 2;

            for (var node = lastNodeWithChildren; node >= 0; node--)
            {
                _maxHeapify(node, lastIndex);
            }
        }

        /// <summary>
        ///     Private Method. Used in Building a Max Heap.
        /// </summary>
        private void _maxHeapify(int nodeIndex, int lastIndex)
        {
            // assume that the subtrees left(node) and right(node) are max-heaps
            var left = (nodeIndex * 2) + 1;
            var right = left + 1;
            var largest = nodeIndex;

            // If collection[left] > collection[nodeIndex]
            if ((left <= lastIndex) && (_heapComparer.Compare(_collection[left], _collection[nodeIndex]) > 0))
            {
                largest = left;
            }

            // If collection[right] > collection[largest]
            if ((right <= lastIndex) && (_heapComparer.Compare(_collection[right], _collection[largest]) > 0))
            {
                largest = right;
            }

            // Swap and heapify
            if (largest != nodeIndex)
            {
                _collection.Swap(nodeIndex, largest);
                _maxHeapify(largest, lastIndex);
            }
        }

        /// <summary>
        ///     Private Method. Used to restore heap condition after insertion
        /// </summary>
        private void _siftUp(int nodeIndex)
        {
            var parent = (nodeIndex - 1) / 2;
            while (_heapComparer.Compare(_collection[nodeIndex], _collection[parent]) > 0)
            {
                _collection.Swap(parent, nodeIndex);
                nodeIndex = parent;
                parent = (nodeIndex - 1) / 2;
            }
        }

        #region IMaxHeap<T> Members

        /// <summary>
        ///     Returns the number of elements in heap
        /// </summary>
        public int Count => _collection.Count;

        /// <summary>
        ///     Checks whether this heap is empty
        /// </summary>
        public bool IsEmpty => _collection.Count == 0;

        /// <summary>
        ///     Heapifies the specified newCollection. Overrides the current heap.
        /// </summary>
        public void Initialize(IList<T> newCollection)
        {
            if (newCollection.Count > 0)
            {
                // Reset and reserve the size of the newCollection
                _collection = new List<T>(newCollection.Count);

                // Copy the elements from the newCollection to the inner collection
                for (var i = 0; i < newCollection.Count; ++i)
                {
                    _collection.Insert(i, newCollection[i]);
                }

                // Build the heap
                _buildMaxHeap();
            }
        }

        /// <summary>
        ///     Adding a new key to the heap.
        /// </summary>
        public void Add(T heapKey)
        {
            _collection.Add(heapKey);
            if (!IsEmpty)
            {
                _siftUp(_collection.Count - 1);
            }
        }

        /// <summary>
        ///     Find the maximum node of a max heap.
        /// </summary>
        public T Peek()
        {
            if (IsEmpty)
            {
                throw new Exception("Heap is empty.");
            }

            return _collection.GetFirst();
        }

        /// <summary>
        ///     Removes the node of minimum value from a min heap.
        /// </summary>
        public void RemoveMax()
        {
            if (IsEmpty)
            {
                throw new Exception("Heap is empty.");
            }

            var max = 0;
            var last = _collection.Count - 1;
            _collection.Swap(max, last);

            _collection.RemoveAt(last);
            last--;

            _maxHeapify(0, last);
        }

        /// <summary>
        ///     Returns the node of maximum value from a max heap after removing it from the heap.
        /// </summary>
        public T ExtractMax()
        {
            var max = Peek();
            RemoveMax();
            return max;
        }

        /// <summary>
        ///     Clear this heap.
        /// </summary>
        public void Clear()
        {
            if (IsEmpty)
            {
                throw new Exception("Heap is empty.");
            }

            _collection.Clear();
        }

        /// <summary>
        ///     Rebuilds the heap.
        /// </summary>
        public void RebuildHeap()
        {
            _buildMaxHeap();
        }

        /// <summary>
        ///     Returns an array version of this heap.
        /// </summary>
        public T[] ToArray()
        {
            return _collection.ToArray();
        }

        /// <summary>
        ///     Returns a list version of this heap.
        /// </summary>
        public List<T> ToList()
        {
            return _collection;
        }

        /// <summary>
        ///     Returns a new min heap that contains all elements of this heap.
        /// </summary>
        public IMinHeap<T> ToMinHeap()
        {
            var newMinHeap = new BinaryMinHeap<T>(Count, _heapComparer);
            newMinHeap.Initialize(_collection.ToArray());
            return newMinHeap;
        }

        #endregion
    }
}
