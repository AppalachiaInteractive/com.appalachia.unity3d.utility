using System;
using System.Collections;
using System.Collections.Generic;
using Appalachia.Utility.Algorithms.Sorting;

namespace Appalachia.Utility.Algorithms.Search
{
    public class BinarySearcher<T> : IEnumerator<T>
    {
        /// <summary>
        ///     Class constructor
        /// </summary>
        /// <param name="collection">A list</param>
        /// <param name="comparer">A comparer</param>
        public BinarySearcher(IList<T> collection, Comparer<T> comparer)
        {
            if (collection == null)
            {
                throw new NullReferenceException("List is null");
            }

            _collection = collection;
            _comparer = comparer;
            _collection.HeapSort();
        }

        #region Fields and Autoproperties

        private readonly Comparer<T> _comparer;
        private readonly IList<T> _collection;
        private int _currentItemIndex;
        private int _leftIndex;
        private int _rightIndex;
        private T _item;

        #endregion

        /// <summary>
        ///     Apply Binary Search in a list.
        /// </summary>
        /// <param name="item">The item we search</param>
        /// <returns>If item found, its' index, -1 otherwise</returns>
        public int BinarySearch(T item)
        {
            var notFound = true;

            if (item == null)
            {
                throw new NullReferenceException("Item to search for is not set");
            }

            Reset();
            _item = item;

            while ((_leftIndex <= _rightIndex) && notFound)
            {
                notFound = MoveNext();
            }

            if (notFound)
            {
                Reset();
            }

            return _currentItemIndex;
        }

        #region IEnumerator<T> Members

        /// <summary>
        ///     The value of the current item
        /// </summary>
        public T Current => _collection[_currentItemIndex];

        object IEnumerator.Current => Current;

        /// <summary>
        ///     An implementation of IEnumerator's MoveNext method.
        /// </summary>
        /// <returns>true if iteration can proceed to the next item, false otherwise</returns>
        public bool MoveNext()
        {
            _currentItemIndex = _leftIndex + ((_rightIndex - _leftIndex) / 2);

            if (_comparer.Compare(_item, Current) < 0)
            {
                _rightIndex = _currentItemIndex - 1;
            }
            else if (_comparer.Compare(_item, Current) > 0)
            {
                _leftIndex = _currentItemIndex + 1;
            }
            else
            {
                return false;
            }

            return true;
        }

        public void Reset()
        {
            _currentItemIndex = -1;
            _leftIndex = 0;
            _rightIndex = _collection.Count - 1;
        }

        public void Dispose()
        {
            //not implementing this, since there are no managed resources to release 
        }

        #endregion
    }
}
