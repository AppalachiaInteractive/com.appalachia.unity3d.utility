﻿#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Runtime.CompilerServices;

namespace Appalachia.Utility.Async.Internal
{
    // optimized version of Standard Queue<T>.
    internal class MinimumQueue<T>
    {
        private const int MinimumGrow = 4;
        private const int GrowFactor = 200;

        private T[] array;
        private int head;
        private int tail;
        private int size;

        public MinimumQueue(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException("capacity");
            }

            array = new T[capacity];
            head = tail = size = 0;
        }

        public int Count
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => size;
        }

        public T Peek()
        {
            if (size == 0)
            {
                ThrowForEmptyQueue();
            }

            return array[head];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Enqueue(T item)
        {
            if (size == array.Length)
            {
                Grow();
            }

            array[tail] = item;
            MoveNext(ref tail);
            size++;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Dequeue()
        {
            if (size == 0)
            {
                ThrowForEmptyQueue();
            }

            var head = this.head;
            var array = this.array;
            var removed = array[head];
            array[head] = default(T);
            MoveNext(ref this.head);
            size--;
            return removed;
        }

        private void Grow()
        {
            var newcapacity = (int)((array.Length * (long)GrowFactor) / 100);
            if (newcapacity < (array.Length + MinimumGrow))
            {
                newcapacity = array.Length + MinimumGrow;
            }

            SetCapacity(newcapacity);
        }

        private void SetCapacity(int capacity)
        {
            var newarray = new T[capacity];
            if (size > 0)
            {
                if (head < tail)
                {
                    Array.Copy(array, head, newarray, 0, size);
                }
                else
                {
                    Array.Copy(array, head, newarray, 0,                   array.Length - head);
                    Array.Copy(array, 0,    newarray, array.Length - head, tail);
                }
            }

            array = newarray;
            head = 0;
            tail = size == capacity ? 0 : size;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void MoveNext(ref int index)
        {
            var tmp = index + 1;
            if (tmp == array.Length)
            {
                tmp = 0;
            }

            index = tmp;
        }

        private void ThrowForEmptyQueue()
        {
            throw new InvalidOperationException("EmptyQueue");
        }
    }
}
