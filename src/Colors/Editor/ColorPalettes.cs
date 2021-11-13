#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace Appalachia.Utility.Colors
{
    [Serializable]
    public static class ColorPalettes
    {
        private static ColorPalette _default;
        private static ColorPalette _scratchPalette;

        public static ColorPalette Default
        {
            get
            {
                if (_default == null)
                {
                    _default = ColorPalette.CreateDefault();
                }

                return _default;
            }
        }

        internal static void DrawScratchPalette(ColorPalette assignTo)
        {
            if (_scratchPalette == null)
            {
                _scratchPalette = assignTo?.Copy() ?? ColorPalette.CreateDefault();
            }

            var paletteChanged = _scratchPalette.Draw();

            if (GUILayout.Button("Generate Color Code to Log"))
            {
                _scratchPalette.Log();
            }

            EditorGUILayout.Space(6f, false);

            if (paletteChanged)
            {
                if (assignTo != null)
                {
                    assignTo.CopyFrom(_scratchPalette);
                }
            }
        }
    }
}

#endif