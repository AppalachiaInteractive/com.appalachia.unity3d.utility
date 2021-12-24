using System;
using System.Collections.Generic;

namespace Appalachia.Utility.Enums
{
    public static class DictionaryExtensions
    {
        public static void PopulateEnumKeys<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary,
            Func<TKey, TValue> creator,
            Func<TKey, bool> doInclude = null,
            bool clear = false)
            where TKey : Enum
        {
            var keys = EnumValueManager.GetAllValues<TKey>();

            if (clear)
            {
                dictionary.Clear();
            }

            for (var i = 0; i < keys.Length; i++)
            {
                var key = keys[i];

                if (!dictionary.ContainsKey(key))
                {
                    if ((doInclude == null) || doInclude(key))
                    {
                        dictionary.Add(key, creator(key));
                    }
                }
            }
        }

        public static void PopulateEnumKeys<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
            where TKey : Enum
            where TValue : new()
        {
            var keys = EnumValueManager.GetAllValues<TKey>();

            for (var i = 0; i < keys.Length; i++)
            {
                var key = keys[i];

                if (!dictionary.ContainsKey(key))
                {
                    dictionary.Add(key, new TValue());
                }
            }
        }
    }
}
