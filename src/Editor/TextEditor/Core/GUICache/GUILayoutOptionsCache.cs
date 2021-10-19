using UnityEngine;

namespace Appalachia.CI.TextEditor.Core.GUICache
{
    public class GUILayoutOptionsCache : GUICache<GUILayoutOption[]>
    {
        public GUILayoutOptionsCache(GUILayoutOption[] initial) : base(initial)
        {
        }

        public override GUILayoutOption[] Default()
        {
            return new GUILayoutOption[0];
        }

        public static implicit operator GUILayoutOption[](GUILayoutOptionsCache value)
        {
            return value.Value;
        }
    }
}
