using System.Diagnostics;
using UnityEngine;

namespace Appalachia.Utility.TextEditor.Core.GUICache
{
    public class GUILayoutOptionsCache : GUICache<GUILayoutOption[]>
    {
        public GUILayoutOptionsCache(GUILayoutOption[] initial) : base(initial)
        {
        }

        [DebuggerStepThrough]
        public static implicit operator GUILayoutOption[](GUILayoutOptionsCache value)
        {
            return value.Value;
        }

        /// <inheritdoc />
        public override GUILayoutOption[] Default()
        {
            return new GUILayoutOption[0];
        }
    }
}
