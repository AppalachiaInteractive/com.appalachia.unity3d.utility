using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Profiling;

// ReSharper disable NotResolvedInText

namespace Appalachia.Utility.DataStructures.Lists
{
    public class CircularBuffer<T> : IEnumerable<T>, ICollection<T>
        where T : IComparable<T>
    {
        #region Constants and Static Readonly

        protected static readonly int _defaultBufferLength = 10;

        #endregion

        /// <summary>
        ///     Initializes a circular buffer with initial length of 10
        /// </summary>
        public CircularBuffer(bool canOverride = true) : this(_defaultBufferLength, canOverride)
        {
        }

        /// <summary>
        ///     Initializes a circular buffer with given length
        /// </summary>
        /// <param name="length">The length of the buffer</param>
        public CircularBuffer(int length, bool canOverride = true)
        {
            using (_PRF_CircularBuffer.Auto())
            {
                if (length < 1)
                {
                    throw new ArgumentOutOfRangeException("length can not be zero or negative");
                }

                _circularBuffer = new T[length + 1];
                _end = 0;
                _start = 0;
                CanOverride = canOverride;
            }
        }

        #region Fields and Autoproperties

        /// <summary>
        ///     Controls whether data should be overridden when it is continously inserted without reading
        /// </summary>
        public bool CanOverride { get; }

        protected int _end;
        protected int _start;
        protected T[] _circularBuffer;

        private int _count;

        #endregion

        /// <summary>
        ///     Checks if no element is inserted into the buffer
        /// </summary>
        public bool IsEmpty => _count == 0;

        /// <summary>
        ///     Checks if the buffer is filled up
        /// </summary>
        public bool IsFilledUp
        {
            get
            {
                using (_PRF_IsFilledUp.Auto())
                {
                    return (((_end + 1) % _circularBuffer.Length) == _start) &&
                           !_circularBuffer[_start].Equals(_circularBuffer[_end]);
                }
            }
        }

        /// <summary>
        ///     Returns the length of the buffer
        /// </summary>
        public int Length => _circularBuffer.Length - 1;

        public T this[int index]
        {
            get => _circularBuffer[(_start + index) % _circularBuffer.Length];
            set => _circularBuffer[(_start + index) % _circularBuffer.Length] = value;
        }

        /// <summary>
        ///     Reads and removes the value in front of the buffer, and places the next value in front.
        /// </summary>
        public T Pop()
        {
            using (_PRF_Pop.Auto())
            {
                var result = _circularBuffer[_start];
                _circularBuffer[_start] = _circularBuffer[_end];
                _start = (_start + 1) % _circularBuffer.Length;

                //Count should not go below Zero when poping an empty buffer.
                _count = _count > 0 ? --_count : _count;
                return result;
            }
        }

        public T RemoveFirst()
        {
            using (_PRF_RemoveFirst.Auto())
            {
                var element = _circularBuffer[_start];

                Remove(element);

                return element;
            }
        }

        public T RemoveLast()
        {
            using (_PRF_RemoveLast.Auto())
            {
                var element = _circularBuffer[Count - 1];

                Remove(element);

                return element;
            }
        }

        protected virtual void AddInternal(T value)
        {
            using (_PRF_AddInternal.Auto())
            {
                if ((CanOverride == false) && IsFilledUp)
                {
                    throw new CircularBufferFullException(
                        $"Circular Buffer is filled up. {value} can not be inserted"
                    );
                }

                InsertInternal(value);
            }
        }

        // Inserts data into the buffer without checking if it is full
        protected void InsertInternal(T value)
        {
            using (_PRF_InsertInternal.Auto())
            {
                _circularBuffer[_end] = value;
                _end = (_end + 1) % _circularBuffer.Length;
                if (_end == _start)
                {
                    _start = (_start + 1) % _circularBuffer.Length;
                }

                // Count should not be greater than the length of the buffer when overriding 
                _count = _count < Length ? ++_count : _count;
            }
        }

        #region ICollection<T> Members

        /// <summary>
        ///     Writes value to the back of the buffer
        /// </summary>
        /// <param name="value">value to be added to the buffer</param>
        public void Add(T value)
        {
            using (_PRF_Add.Auto())
            {
                AddInternal(value);
            }
        }

        /// <summary>
        ///     Returns the number of elements.
        /// </summary>
        public int Count => _count;

        /// <summary>
        ///     Checks whether this collection is readonly
        /// </summary>
        public bool IsReadOnly => false;

        /// <summary>
        ///     Clears this instance
        /// </summary>
        public void Clear()
        {
            using (_PRF_Clear.Auto())
            {
                _count = 0;
                _start = 0;
                _end = 0;
                _circularBuffer = new T[Length + 1];
            }
        }

        /// <summary>
        ///     Checks whether the buffer contains an item
        /// </summary>
        public bool Contains(T item)
        {
            using (_PRF_Contains.Auto())
            {
                return _circularBuffer.Contains(item);
            }
        }

        /// <summary>
        ///     Copies this buffer to an array
        /// </summary>
        public void CopyTo(T[] array, int arrayIndex)
        {
            using (_PRF_CopyTo.Auto())
            {
                if (array == null)
                {
                    throw new ArgumentNullException("array can not be null");
                }

                if ((array.Length == 0) || (arrayIndex >= array.Length) || (arrayIndex < 0))
                {
                    throw new IndexOutOfRangeException();
                }

                // Get enumerator
                using var enumarator = GetEnumerator();

                // Copy elements if there is any in the buffer and if the index is within the valid range
                while (arrayIndex < array.Length)
                {
                    if (enumarator.MoveNext())
                    {
                        array[arrayIndex] = enumarator.Current;
                        arrayIndex++;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        /// <summary>
        ///     Removes an item from the buffer
        /// </summary>
        public bool Remove(T item)
        {
            using (_PRF_Remove.Auto())
            {
                if (!IsEmpty && Contains(item))
                {
                    var sourceArray = _circularBuffer.Except(new[] { item }).ToArray();
                    _circularBuffer = new T[Length + 1];
                    Array.Copy(sourceArray, _circularBuffer, sourceArray.Length);

                    if (!Equals(item, default(T)))
                    {
                        _end = sourceArray.Length - 1;
                        _count = sourceArray.Length - 1;
                    }
                    else
                    {
                        _end = sourceArray.Length;
                        _count = sourceArray.Length;
                    }

                    return true;
                }

                return false;
            }
        }

        #endregion

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            for (var i = _start; i < Count; i++)
            {
                yield return _circularBuffer[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Profiling

        private const string _PRF_PFX = nameof(CircularBuffer<T>) + ".";

        private static readonly ProfilerMarker _PRF_IsFilledUp =
            new ProfilerMarker(_PRF_PFX + nameof(IsFilledUp));

        private static readonly ProfilerMarker _PRF_CircularBuffer =
            new ProfilerMarker(_PRF_PFX + nameof(CircularBuffer<T>));

        private static readonly ProfilerMarker _PRF_AddInternal =
            new ProfilerMarker(_PRF_PFX + nameof(AddInternal));

        private static readonly ProfilerMarker _PRF_Pop = new ProfilerMarker(_PRF_PFX + nameof(Pop));

        private static readonly ProfilerMarker _PRF_InsertInternal =
            new ProfilerMarker(_PRF_PFX + nameof(InsertInternal));

        private static readonly ProfilerMarker _PRF_Add = new ProfilerMarker(_PRF_PFX + nameof(Add));

        private static readonly ProfilerMarker _PRF_Remove = new ProfilerMarker(_PRF_PFX + nameof(Remove));

        private static readonly ProfilerMarker _PRF_RemoveFirst =
            new ProfilerMarker(_PRF_PFX + nameof(RemoveFirst));

        private static readonly ProfilerMarker _PRF_RemoveLast =
            new ProfilerMarker(_PRF_PFX + nameof(RemoveLast));

        private static readonly ProfilerMarker _PRF_Clear = new ProfilerMarker(_PRF_PFX + nameof(Clear));

        private static readonly ProfilerMarker _PRF_Contains =
            new ProfilerMarker(_PRF_PFX + nameof(Contains));

        private static readonly ProfilerMarker _PRF_CopyTo = new ProfilerMarker(_PRF_PFX + nameof(CopyTo));

        #endregion
    }

    public class CircularBufferFullException : Exception
    {
        public CircularBufferFullException(string message) : base(message)
        {
        }
    }
}
