using System;

// ReSharper disable NotResolvedInText

namespace Appalachia.Utility.DataStructures.Lists
{
    public class SkipListNode<T> : IComparable<SkipListNode<T>>
        where T : IComparable<T>
    {
        /// <summary>
        ///     CONSTRUCTORS
        /// </summary>
        public SkipListNode(T value, int level)
        {
            if (level < 0)
            {
                throw new ArgumentOutOfRangeException("Invalid value for level.");
            }

            Value = value;
            Forwards = new SkipListNode<T>[level];
        }

        #region Fields and Autoproperties

        private SkipListNode<T>[] _forwards;

        /// <summary>
        ///     Instance variables
        /// </summary>
        private T _value;

        #endregion

        /// <summary>
        ///     Return level of node.
        /// </summary>
        public virtual int Level => Forwards.Length;

        /// <summary>
        ///     Get and set node's forwards links
        /// </summary>
        public virtual SkipListNode<T>[] Forwards
        {
            get => _forwards;
            private set => _forwards = value;
        }

        /// <summary>
        ///     Get and set node's value
        /// </summary>
        public virtual T Value
        {
            get => _value;
            private set => _value = value;
        }

        #region IComparable<SkipListNode<T>> Members

        /// <summary>
        ///     IComparable method implementation
        /// </summary>
        public int CompareTo(SkipListNode<T> other)
        {
            if (other == null)
            {
                return -1;
            }

            return Value.CompareTo(other.Value);
        }

        #endregion
    }
}
