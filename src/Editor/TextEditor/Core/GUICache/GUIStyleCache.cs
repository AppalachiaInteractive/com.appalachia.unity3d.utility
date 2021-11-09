using System.Diagnostics;
using UnityEngine;

namespace Appalachia.Utility.TextEditor.Core.GUICache
{
    public class GUIStyleCache : GUICache<GUIStyle>
    {
        public GUIStyleCache(GUIStyle initial) : base(initial)
        {
        }

        public override GUIStyle Default()
        {
            return GUIStyle.none;
        }

        [DebuggerStepThrough] public static implicit operator GUIStyle(GUIStyleCache value)
        {
            return value.Value;
        }
    }
}
