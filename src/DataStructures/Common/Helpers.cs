using System;
using System.Collections.Generic;

namespace Appalachia.Utility.DataStructures.Common
{
    public static class Helpers
    {
        #region Constants and Static Readonly

        public const int MAXIMUM_ARRAY_LENGTH_x64 = 0X7FEFFFFF; //x64
        public const int MAXIMUM_ARRAY_LENGTH_x86 = 0x8000000;  //x86

        #endregion

        /// <summary>
        ///     Centralize a text.
        /// </summary>
        public static string PadCenter(this string text, int newWidth, char fillerCharacter = ' ')
        {
            if (string.IsNullOrEmpty(text))
            {
                return text;
            }

            var length = text.Length;
            var charactersToPad = newWidth - length;
            if (charactersToPad < 0)
            {
                throw new ArgumentException("New width must be greater than string length.", "newWidth");
            }

            var padLeft = (charactersToPad / 2) + (charactersToPad % 2);

            //add a space to the left if the string is an odd number
            var padRight = charactersToPad / 2;

            return new string(fillerCharacter, padLeft) + text + new string(fillerCharacter, padRight);
        }

        /// <summary>
        ///     Populates the specified two-dimensional with a default value.
        /// </summary>
        public static void Populate<T>(this T[,] array, int rows, int columns, T defaultValue = default(T))
        {
            for (var i = 0; i < rows; ++i)
            {
                for (var j = 0; j < columns; ++j)
                {
                    array[i, j] = defaultValue;
                }
            }
        }

        /// <summary>
        ///     Swap two values in a collection given their indexes.
        /// </summary>
        public static void Swap<T>(this IList<T> list, int firstIndex, int secondIndex)
        {
            if ((list.Count < 2) ||
                (firstIndex ==
                 secondIndex)) //This check is not required but Partition function may make many calls so its for perf reason
            {
                return;
            }

            var temp = list[firstIndex];
            list[firstIndex] = list[secondIndex];
            list[secondIndex] = temp;
        }

        /// <summary>
        ///     Swap two values in a collection given their indexes.
        /// </summary>
        public static void Swap<T>(this List<T> list, int firstIndex, int secondIndex)
        {
            if ((list.Count < 2) ||
                (firstIndex ==
                 secondIndex)) //This check is not required but Partition function may make many calls so its for perf reason
            {
                return;
            }

            var temp = list[firstIndex];
            list[firstIndex] = list[secondIndex];
            list[secondIndex] = temp;
        }
    }
}
