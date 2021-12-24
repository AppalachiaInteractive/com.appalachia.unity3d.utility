using System;

// ReSharper disable NotResolvedInText

namespace Appalachia.Utility.Algorithms.Strings
{
    /// <summary>
    ///     The Edit Distance computation algorithm.
    ///     Uses a custom class for receiving the costs.
    /// </summary>
    public static class EditDistance
    {
        /// <summary>
        ///     Computes the Minimum Edit Distance between two strings.
        /// </summary>
        public static long GetMinDistance(
            string source,
            string destination,
            EditDistanceCostsMap<long> distances)
        {
            // Validate parameters and TCost.
            if ((source == null) || (destination == null) || (distances == null))
            {
                throw new ArgumentNullException("Some of the parameters are null.");
            }

            if (source == destination)
            {
                return 0;
            }

            // Dynamic Programming 3D Table
            var dynamicTable = new long[source.Length + 1, destination.Length + 1];

            // Initialize table
            for (var i = 0; i <= source.Length; ++i)
            {
                dynamicTable[i, 0] = i;
            }

            for (var i = 0; i <= destination.Length; ++i)
            {
                dynamicTable[0, i] = i;
            }

            // Compute min edit distance cost
            for (var i = 1; i <= source.Length; ++i)
            {
                for (var j = 1; j <= destination.Length; ++j)
                {
                    if (source[i - 1] == destination[j - 1])
                    {
                        dynamicTable[i, j] = dynamicTable[i - 1, j - 1];
                    }
                    else
                    {
                        var insert = dynamicTable[i, j - 1] + distances.InsertionCost;
                        var delete = dynamicTable[i - 1, j] + distances.DeletionCost;
                        var substitute = dynamicTable[i - 1, j - 1] + distances.SubstitutionCost;

                        dynamicTable[i, j] = Math.Min(insert, Math.Min(delete, substitute));
                    }
                }
            }

            // Get min edit distance cost
            return dynamicTable[source.Length, destination.Length];
        }

        /// <summary>
        ///     Overloaded method for 32-bits Integer Distances
        /// </summary>
        public static int GetMinDistance(
            string source,
            string destination,
            EditDistanceCostsMap<int> distances)
        {
            // Validate parameters and TCost.
            if ((source == null) || (destination == null) || (distances == null))
            {
                throw new ArgumentNullException("Some of the parameters are null.");
            }

            var longDistance = new EditDistanceCostsMap<long>(
                Convert.ToInt64(distances.InsertionCost),
                Convert.ToInt64(distances.DeletionCost),
                Convert.ToInt64(distances.InsertionCost)
            );

            return Convert.ToInt32(GetMinDistance(source, destination, longDistance));
        }

        /// <summary>
        ///     Overloaded method for 16-bits Integer Distances
        /// </summary>
        public static short GetMinDistance(
            string source,
            string destination,
            EditDistanceCostsMap<short> distances)
        {
            // Validate parameters and TCost.
            if ((source == null) || (destination == null) || (distances == null))
            {
                throw new ArgumentNullException("Some of the parameters are null.");
            }

            var longDistance = new EditDistanceCostsMap<long>(
                Convert.ToInt64(distances.InsertionCost),
                Convert.ToInt64(distances.DeletionCost),
                Convert.ToInt64(distances.InsertionCost)
            );

            return Convert.ToInt16(GetMinDistance(source, destination, longDistance));
        }
    }
}
