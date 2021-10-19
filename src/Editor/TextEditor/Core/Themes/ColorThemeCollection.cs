/*
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Appalachia.CI.TextEditor.Core.Themes
{
    [Serializable]
    public class ColorThemeCollection : ScriptableObject
    {
        public List<ColorTheme> themes;

        public ColorTheme defaultTheme => ColorTheme.Default();
        
        public static ColorThemeCollection GetInstance()
        {
            var assetPath = AssetDatabaseManager.FindAssets("t: ColorThemeCollection")
                                      .Select(AssetDatabaseManager.GUIDToAssetPath)
                                      .FirstOrDefault();

            if (string.IsNullOrWhiteSpace(assetPath))
            {
                var instance = ScriptableObject.CreateInstance<ColorThemeCollection>();
            }
        }
        
        private void OnEnable()
        {
            if (themes == null)
            {
                themes = new List<ColorTheme> {defaultTheme};
            }
        }
        
        public void Register(ColorTheme theme)
        {
            themes.Add(theme);
        }

        public ColorTheme Get(string name)
        {
            foreach (var theme in themes)
            {
                if (string.Equals(name, theme.themeName))
                {
                    return theme;
                }
            }

            return defaultTheme;
        }
    }
}
*/


