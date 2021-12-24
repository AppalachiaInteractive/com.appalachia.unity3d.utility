using System;
using System.Collections.Generic;

namespace Appalachia.Utility.Algorithms.Strings
{
    public static class Permutations
    {
        /// <summary>
        ///     Computes the permutations of a string.
        /// </summary>
        public static HashSet<string> ComputeDistinct(string source)
        {
            return _permutations(source);
        }

        /// <summary>
        ///     Determines if the Other string is an anargram of the Source string.
        /// </summary>
        public static bool IsAnargram(string source, string other)
        {
            if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(other))
            {
                return false;
            }

            if (source.Length != other.Length)
            {
                return false;
            }

            if (source.Equals(other, StringComparison.Ordinal))
            {
                return true;
            }

            var len = source.Length;

            // Hash set which will contains all the characters present in input source.
            var hashSetSourceChars = new HashSet<char>();
            var hashSetOtherChars = new HashSet<char>();
            for (var i = 0; i < len; i++)
            {
                hashSetSourceChars.Add(source[i]);
                hashSetOtherChars.Add(other[i]);
            }

            for (var i = 0; i < len; i++)
            {
                // Inputs are not Anargram if characers from *other are not present in *source.
                if (!hashSetSourceChars.Contains(other[i]))
                {
                    return false;
                }

                if (!hashSetOtherChars.Contains(source[i]))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        ///     Private helper. Merges a set of permutations with a character.
        /// </summary>
        private static HashSet<string> _mergePermutations(HashSet<string> permutations, string character)
        {
            var merges = new HashSet<string>();

            foreach (var perm in permutations)
            {
                for (var i = 0; i < perm.Length; ++i)
                {
                    var newMerge = perm.Insert(i, character);

                    if (!merges.Contains(newMerge))
                    {
                        merges.Add(newMerge);
                    }
                }
            }

            return merges;
        }

        /// <summary>
        ///     Private Helper. Computes permutations recursively for string source.
        /// </summary>
        private static HashSet<string> _permutations(string source)
        {
            var perms = new HashSet<string>();

            if (source.Length == 1)
            {
                perms.Add(source);
            }
            else if (source.Length > 1)
            {
                var lastIndex = source.Length - 1;
                var lastChar = Convert.ToString(source[lastIndex]);
                var substring = source.Substring(0, lastIndex);
                perms = _mergePermutations(_permutations(substring), lastChar);
            }

            return perms;
        }
    }
}
