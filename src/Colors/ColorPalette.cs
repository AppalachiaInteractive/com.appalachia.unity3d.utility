using System;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Appalachia.Utility.Colors
{
    [Serializable]
    public class ColorPalette
    {
        private const int SUBSETS = 6;
        private const string _prefixSpace = "                        ";

        public static ColorPalette Default
        {
            get
            {
                if (_default == null)
                {
                    _default = CreateDefault();
                }

                return _default;
            }
        }

        public ColorPalette()
        {
            SetDefaults(this);
        }

        private static ColorPalette _default;

        private static StringBuilder _logBuilder;
        public ColorPaletteSubset bad;
        public ColorPaletteSubset disabled;
        public ColorPaletteSubset good;
        public ColorPaletteSubset highlight;
        public ColorPaletteSubset notable;
        public ColorPaletteSubset ui;
        public ColorPaletteSubset neutral;
        public ColorPaletteSubset badToGood;
        public ColorPaletteSubset badToNeutral;
        public ColorPaletteSubset neutralToGood;
        public ColorPaletteSubset neutralToBad;
        public ColorPaletteSubset goodToNeutral;
        public ColorPaletteSubset goodToBad;

        private IEnumerable<ColorPaletteSubset> subsets =>
            new[]
            {
                disabled,
                ui,
                bad,
                good,
                notable,
                highlight,
                neutral,
                badToGood,
                badToNeutral,
                neutralToGood,
                neutralToBad,
                goodToNeutral,
                goodToBad,
            };

        public bool Draw()
        {
            var changed = false;

            for (var index = 0; index < labels.Length; index++)
            {
                var label = labels[index];
                var getter = getters[index];

                var initialValue = getter();
                var temp = EditorGUILayout.ColorField(label, initialValue);

                var thisChanged = temp != initialValue;

                changed = changed || thisChanged;

                if (thisChanged)
                {
                    var setter = setters[index];
                    setter(temp);
                }
            }

            return changed;
        }

        public ColorPalette Copy()
        {
            var newPalette = new ColorPalette();

            foreach (var label in labels)
            {
                newPalette[label] = this[label];
            }

            return newPalette;
        }

        public void CopyFrom(ColorPalette other)
        {
            foreach (var label in labels)
            {
                this[label] = other[label];
            }
        }

        public void CopyTo(ColorPalette other)
        {
            foreach (var label in labels)
            {
                other[label] = this[label];
            }
        }

        public void Log()
        {
            if (_logBuilder == null)
            {
                _logBuilder = new StringBuilder();
            }

            _logBuilder.Clear();

            foreach (var label in labels)
            {
                var color = this[label];

                var formatted =
                    $"{_prefixSpace}{label} = new Color({color.r}f, {color.g}f, {color.b}f, {color.a}f),";

                _logBuilder.AppendLine(formatted);
            }

            var logMessage = _logBuilder.ToString();

            Debug.Log(logMessage);
        }

        private Action<Color>[] GetSettersInternal()
        {
            var output = new Action<Color>[SUBSETS * ColorPaletteSubset.SIZE];

            var fullIndex = 0;

            foreach (var subset in subsets)
            {
                var g = subset.GetSettersInternal();

                for (var i = 0; i < ColorPaletteSubset.SIZE; i++)
                {
                    fullIndex += i;

                    output[fullIndex] = g[i];
                }
            }

            return output;
        }

        private Func<Color>[] GetGettersInternal()
        {
            var output = new Func<Color>[SUBSETS * ColorPaletteSubset.SIZE];

            var fullIndex = 0;

            foreach (var subset in subsets)
            {
                var g = subset.GetGettersInternal();

                for (var i = 0; i < ColorPaletteSubset.SIZE; i++)
                {
                    fullIndex += i;

                    output[fullIndex] = g[i];
                }
            }

            return output;
        }

        private string[] GetLabelsInternal()
        {
            var output = new string[SUBSETS * ColorPaletteSubset.SIZE];

            var fullIndex = 0;

            foreach (var subset in subsets)
            {
                var g = subset.GetLabelsInternal();

                for (var i = 0; i < ColorPaletteSubset.SIZE; i++)
                {
                    fullIndex += i;

                    output[fullIndex] = g[i];
                }
            }

            return output;
        }

        public static ColorPalette CreateDefault()
        {
            var newPalette = new ColorPalette();

            SetDefaults(newPalette);

            return newPalette;
        }

        private static void SetDefaults(ColorPalette p)
        {
            p.disabled = new ColorPaletteSubset(
                "Disabled",
                new Color(050.0f / 255.0f, 050.0f / 255.0f, 050.0f / 255.0f, 1.0f),
                new Color(150.0f / 255.0f, 150.0f / 255.0f, 150.0f / 255.0f, 1.0f)
            );

            p.ui = new ColorPaletteSubset(
                "UI",
                new Color(025.0f / 255.0f, 025.0f / 255.0f, 025.0f / 255.0f, 1.0f),
                new Color(225.0f / 255.0f, 225.0f / 255.0f, 225.0f / 255.0f, 1.0f)
            );

            p.bad = new ColorPaletteSubset(
                "Bad",
                new Color(255.0f / 255.0f, 010.0f / 255.0f, 000.0f / 255.0f, 1.0f),
                new Color(255.0f / 255.0f, 220.0f / 255.0f, 030.0f / 255.0f, 1.0f)
            );

            p.good = new ColorPaletteSubset(
                "Good",
                new Color(017.0f / 255.0f, 029.0f / 255.0f, 019.0f / 255.0f, 1.0f),
                new Color(171.0f / 255.0f, 204.0f / 255.0f, 175.0f / 255.0f, 1.0f)
            );

            p.notable = new ColorPaletteSubset(
                "Notable",
                new Color(013.0f / 255.0f, 071.0f / 255.0f, 161.0f / 255.0f, 1.0f),
                new Color(227.0f / 255.0f, 242.0f / 255.0f, 253.0f / 255.0f, 1.0f)
            );

            p.highlight = new ColorPaletteSubset(
                "Highlight",
                new Color(116.0f / 255.0f, 070.0f / 255.0f, 184.0f / 255.0f, 1.0f),
                new Color(128.0f / 255.0f, 255.0f / 255.0f, 219.0f / 255.0f, 1.0f)
            );
            
            p.neutral = new ColorPaletteSubset(
                "Neutral",
                new Color(181.6f / 255.0f, 179.2f / 255.0f, 172.0f / 255.0f, 1.0f),
                new Color(249.7f / 255.0f, 246.4f / 255.0f, 236.5f / 255.0f, 1.0f)
            );
            
            p.badToGood = new ColorPaletteSubset("Bad To Good",     p.bad.Middle, p.good.Middle);
            p.badToNeutral = new ColorPaletteSubset("Bad To Neutral",  p.bad.Middle, p.neutral.Middle);
            p.goodToBad = new ColorPaletteSubset("Good To Bad",     p.good.Middle, p.bad.Middle);
            p.goodToNeutral = new ColorPaletteSubset("Good To Neutral", p.good.Middle, p.neutral.Middle);
            p.neutralToBad = new ColorPaletteSubset("Neutral To Bad",  p.neutral.Middle, p.bad.Middle);
            p.neutralToGood = new ColorPaletteSubset("Neutral To Good", p.neutral.Middle, p.good.Middle);
        }

#region Plumbing

        private string[] _labels;
        private Func<Color>[] _getters;
        private Action<Color>[] _setters;

        private Func<Color>[] getters
        {
            get
            {
                if (_getters == null)
                {
                    _getters = GetGettersInternal();
                }

                return _getters;
            }
        }

        private Action<Color>[] setters
        {
            get
            {
                if (_setters == null)
                {
                    _setters = GetSettersInternal();
                }

                return _setters;
            }
        }

        private string[] labels
        {
            get
            {
                if (_labels == null)
                {
                    _labels = GetLabelsInternal();

                    for (var index = 0; index < _labels.Length; index++)
                    {
                        var label = _labels[index];
                        _labels[index] = char.ToUpper(label[0]) + label.Substring(1);
                    }
                }

                return _labels;
            }
        }

        private Color this[string key]
        {
            get
            {
                for (var index = 0; index < labels.Length; index++)
                {
                    var label = labels[index];

                    if (label != key)
                    {
                        continue;
                    }

                    var getter = getters[index];
                    return getter();
                }

                return default;
            }
            set
            {
                for (var index = 0; index < labels.Length; index++)
                {
                    var label = labels[index];
                    if (label != key)
                    {
                        continue;
                    }

                    var setter = setters[index];
                    setter(value);
                    return;
                }
            }
        }

#endregion
    }
}
