#pragma warning disable CS1591 // Missing XML comment for publicly visible type or 

using System;

namespace Appalachia.Utility.Async
{
    public readonly struct AsyncUnit : IEquatable<AsyncUnit>
    {
        #region Constants and Static Readonly

        public static readonly AsyncUnit Default = new AsyncUnit();

        #endregion

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return 0;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return "()";
        }

        #region IEquatable<AsyncUnit> Members

        public bool Equals(AsyncUnit other)
        {
            return true;
        }

        #endregion
    }
}
