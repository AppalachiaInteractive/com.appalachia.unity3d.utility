using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Profiling;

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
            using (_PRF_AddOrExecute.Auto())
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
        }

        public static void AddOrUpdate<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary,
            TKey key,
            Func<TValue> addFunction,
            Func<TValue, TValue> action)
        {
            using (_PRF_AddOrUpdate.Auto())
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
        }

        public static void AddOrUpdate<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary,
            TKey key,
            TValue value)
        {
            using (_PRF_AddOrUpdate.Auto())
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

        public static void UpdateAll<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary,
            Func<TKey, TValue, TValue> updater)
        {
            using (_PRF_UpdateAll.Auto())
            {
                var keys = dictionary.Keys.ToArray();

                for (var i = 0; i < keys.Length; i++)
                {
                    var key = keys[i];
                    var value = dictionary[key];

                    var newValue = updater(key, value);

                    dictionary[key] = newValue;
                }
            }
        }

        #region Profiling

        private const string _PRF_PFX = nameof(DictionaryExtensions) + ".";

        private static readonly ProfilerMarker _PRF_UpdateAll =
            new ProfilerMarker(_PRF_PFX + nameof(UpdateAll));

        private static readonly ProfilerMarker _PRF_AddOrExecute =
            new ProfilerMarker(_PRF_PFX + nameof(AddOrExecute));

        private static readonly ProfilerMarker _PRF_AddOrUpdate =
            new ProfilerMarker(_PRF_PFX + nameof(AddOrUpdate));

        #endregion
    }
}
