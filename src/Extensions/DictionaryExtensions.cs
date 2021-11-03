using System;
using System.Collections.Generic;

namespace Appalachia.Utility.Extensions
{
    public static class DictionaryExtensions
    {
        public static void AddOrExecute<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary,
            TKey key,
            Func<TValue> addFunction,
            Action<TValue> action)
        {
            if (dictionary.ContainsKey(key))
            {
                var current = dictionary[key];
                action(current);
            }
            else
            {
                dictionary.Add(key, addFunction());
            }
        }

        public static void AddOrUpdate<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary,
            TKey key,
            Func<TValue> addFunction,
            Func<TValue, TValue> action)
        {
            if (dictionary.ContainsKey(key))
            {
                var current = dictionary[key];
                dictionary[key] = action(current);
            }
            else
            {
                dictionary.Add(key, addFunction());
            }
        }

        public static void AddOrUpdate<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary,
            TKey key,
            TValue value)
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary[key] = value;
            }
            else
            {
                dictionary.Add(key, value);
            }
        }
    }
}
