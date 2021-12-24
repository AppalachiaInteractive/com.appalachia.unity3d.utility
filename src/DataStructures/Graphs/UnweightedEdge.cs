using System;
using Appalachia.Utility.DataStructures.Common;

namespace Appalachia.Utility.DataStructures.Graphs
{
    /// <summary>
    ///     The graph edge class.
    /// </summary>
    public class UnweightedEdge<TVertex> : IEdge<TVertex>
        where TVertex : IComparable<TVertex>
    {
        #region Constants and Static Readonly

        private const int _edgeWeight = 0;

        #endregion

        /// <summary>
        ///     CONSTRUCTOR
        /// </summary>
        public UnweightedEdge(TVertex src, TVertex dst)
        {
            Source = src;
            Destination = dst;
        }

        #region IEdge<TVertex> Members

        /// <summary>
        ///     Gets or sets the source vertex.
        /// </summary>
        /// <value>The source.</value>
        public TVertex Source { get; set; }

        /// <summary>
        ///     Gets or sets the destination vertex.
        /// </summary>
        /// <value>The destination.</value>
        public TVertex Destination { get; set; }

        /// <summary>
        ///     [PRIVATE MEMBER] Gets or sets the weight.
        /// </summary>
        /// <value>The weight.</value>
        public long Weight
        {
            get => throw new NotImplementedException("Unweighted edges don't have weights.");
            set => throw new NotImplementedException("Unweighted edges can't have weights.");
        }

        /// <summary>
        ///     Gets a value indicating whether this edge is weighted.
        /// </summary>
        public bool IsWeighted => false;

        #region IComparable implementation

        public int CompareTo(IEdge<TVertex> other)
        {
            if (other == null)
            {
                return -1;
            }

            var areNodesEqual = Source.IsEqualTo(other.Source) && Destination.IsEqualTo(other.Destination);

            if (!areNodesEqual)
            {
                return -1;
            }

            return 0;
        }

        #endregion

        #endregion
    }
}
