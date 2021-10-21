using System;
using UnityEngine;

namespace Appalachia.Utility.TextEditor.Core.Themes
{
    [Serializable]
    public class ColorTheme : ScriptableObject
    {
        public string themeName;
        public Color colorMetadata;
        public Color colorObject;
        public Color colorArray;
        public Color colorProperty;
        public Color buttonHighlight;
        public Color buttonOn;
        public Color error;
        public Color menu;
        public Color navigate;
        public Color delete;

        public static ColorTheme Default()
        {
            var theme = CreateInstance<ColorTheme>();
            theme.themeName = "default";
            theme.colorMetadata = new Color(077f / 255f,   144f / 255f, 142f / 255f, 1.0f);
            theme.colorObject = new Color(249f / 255f,     132f / 255f, 074f / 255f, 1.0f);
            theme.colorArray = new Color(087f / 255f,      117f / 255f, 144f / 255f, 1.0f);
            theme.colorProperty = new Color(067f / 255f,   170f / 255f, 139f / 255f, 1.0f);
            theme.buttonHighlight = new Color(248f / 255f, 150f / 255f, 030f / 255f, 1.0f);
            theme.buttonOn = new Color(144f / 255f,        190f / 255f, 109f / 255f, 1.0f);
            theme.error = new Color(249f / 255f,           065f / 255f, 068f / 255f, 1.0f);
            theme.menu = new Color(039f / 255f,            125f / 255f, 161f / 255f, 1.0f);
            theme.navigate = new Color(249f / 255f,        199f / 255f, 079f / 255f, 1.0f);
            theme.delete = new Color(243f / 255f,          114f / 255f, 044f / 255f, 1.0f);

            return theme;
        }
    }
}
