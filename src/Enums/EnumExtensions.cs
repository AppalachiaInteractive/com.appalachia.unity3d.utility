using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Appalachia.Utility.Enums
{
    public static class EnumExtensions
    {
        private static Dictionary<Type, Dictionary<object, string>> _enumNameLookup;

        public static T[] GetValuesAsInstances<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>().ToArray();
        }

        public static T Parse<T>(this string name)
            where T : struct, IConvertible
        {
            try
            {
                return (T) Enum.Parse(typeof(T), name);
            }
            catch
            {
                return default;
            }
        }

        public static string ToDisplayName<T>(this T value)
            where T : Enum
        {
            if (_enumNameLookup == null)
            {
                _enumNameLookup = new Dictionary<Type, Dictionary<object, string>>();
            }

            var enumType = typeof(T);

            if (!_enumNameLookup.ContainsKey(enumType))
            {
                _enumNameLookup.Add(enumType, new Dictionary<object, string>());
            }

            if (_enumNameLookup[enumType].ContainsKey(value))
            {
                return _enumNameLookup[enumType][value];
            }

            var stringValue = value.ToString();

            var newString = new StringBuilder();

            for (var i = 0; i < stringValue.Length; i++)
            {
                var character = stringValue[i];

                if (i > 0)
                {
                    if (char.IsUpper(character))
                    {
                        newString.Append(' ');
                    }
                }

                if (!char.IsLetterOrDigit(character))
                {
                    newString.Append(' ');
                }
                else
                {
                    newString.Append(character);
                }
            }

            var result = newString.ToString();

            _enumNameLookup[enumType].Add(value, result);

            return result;
        }
    }
}
