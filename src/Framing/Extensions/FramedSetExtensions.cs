using System.Collections.Generic;
using UnityEngine;

namespace Appalachia.Utility.Framing.Extensions
{
    internal static class FramedSetExtensions
    {
        public static FramedSet ToFramedSet(this IEnumerable<GameObject> gameObjects)
        {
            return new FramedSet(gameObjects);
        }
        public static FramedSet ToFramedSet(this GameObject gameObject)
        {
            return new FramedSet(gameObject);
        }
    }
}
