using System.Collections.Generic;
using System.Numerics;

namespace Appalachia.Utility.Algorithms.Numeric
{
    public static class BinomialCoefficients
    {
        #region Constants and Static Readonly

        private static readonly Dictionary<uint, BigInteger> Cache = new Dictionary<uint, BigInteger>();

        #endregion

        /// <summary>
        ///     Calculate binomial coefficients, C(n, k).
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static BigInteger Calculate(uint n)
        {
            return Factorial(2 * n) / (Factorial(n) * Factorial(n + 1));
        }

        private static BigInteger Factorial(uint n)
        {
            if (n <= 1)
            {
                return 1;
            }

            if (Cache.TryGetValue(n, out var result)) return result;

            var value = n * Factorial(n - 1);
            Cache[n] = value;
            return value;
        }
    }
}
