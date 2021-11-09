using System.Diagnostics;
using UnityEngine;

namespace Appalachia.Utility.TextEditor.Core.GUICache
{
    public class GUIColorCache : GUICache<Color>
    {
        public GUIColorCache(Color initial) : base(initial)
        {
        }

        public override Color Default()
        {
            return Color.clear;
        }

        [DebuggerStepThrough] public static implicit operator Color(GUIColorCache value)
        {
            return value.Value;
        }
    }
}
