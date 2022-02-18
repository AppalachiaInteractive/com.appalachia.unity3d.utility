using System.Diagnostics;
using UnityEngine;

namespace Appalachia.Utility.TextEditor.Core.GUICache
{
    public class GUIColorCache : GUICache<Color>
    {
        public GUIColorCache(Color initial) : base(initial)
        {
        }

        [DebuggerStepThrough]
        public static implicit operator Color(GUIColorCache value)
        {
            return value.Value;
        }

        /// <inheritdoc />
        public override Color Default()
        {
            return Color.clear;
        }
    }
}
