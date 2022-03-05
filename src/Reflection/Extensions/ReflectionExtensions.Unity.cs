using System;
using System.Collections.Generic;
using Unity.Profiling;

namespace Appalachia.Utility.Reflection.Extensions
{
    public static partial class ReflectionExtensions
    {
        private static Dictionary<Type, bool> _unityAssetTypeCache;

        private static readonly ProfilerMarker _PRF_IsUnityAssetType =
            new ProfilerMarker(_PRF_PFX + nameof(IsUnityAssetType));

        public static bool IsUnityAssetType<T>()
        {
            using (_PRF_IsUnityAssetType.Auto())
            {
                return typeof(T).IsUnityAssetType();
            }
        }

        public static bool IsUnityAssetType(this Type t)
        {
            using (_PRF_IsUnityAssetType.Auto())
            {
                _unityAssetTypeCache ??= new();

                if (_unityAssetTypeCache.TryGetValue(t, out var result)) return result;

                if (t.InheritsFrom(typeof(UnityEngine.Object)))
                {
                    if (t.Namespace.StartsWith("Unity"))
                    {
                        result = true;
                    }
                }

                _unityAssetTypeCache.Add(t, result);

                return result;
            }
        }
    }
}
