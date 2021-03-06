using System.Diagnostics;
using UnityEngine;

namespace Appalachia.Utility.TextEditor.Core.GUICache
{
    public class GUIContentCache : GUICache<GUIContent>
    {
        public GUIContentCache(GUIContent initial) : base(initial)
        {
        }

        [DebuggerStepThrough]
        public static implicit operator GUIContent(GUIContentCache value)
        {
            return value.Value;
        }

        /// <inheritdoc />
        public override GUIContent Default()
        {
            return GUIContent.none;
        }
    }
}
