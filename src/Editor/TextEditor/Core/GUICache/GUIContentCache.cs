using UnityEngine;

namespace Appalachia.Utility.TextEditor.Core.GUICache
{
    public class GUIContentCache : GUICache<GUIContent>
    {
        public GUIContentCache(GUIContent initial) : base(initial)
        {
        }

        public override GUIContent Default()
        {
            return GUIContent.none;
        }

        public static implicit operator GUIContent(GUIContentCache value)
        {
            return value.Value;
        }
    }
}
