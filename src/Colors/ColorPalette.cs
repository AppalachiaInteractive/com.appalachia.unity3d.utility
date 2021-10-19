using System;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Appalachia.Utility.Colors
{
    [Serializable]
    public class ColorPalette
    {
        private const int SIZE = 20;
        
        private const string _prefixSpace = "                        ";
        public Color bad0;
        public Color bad1;
        public Color bad2;
        public Color bad3;
        public Color bad4;
        public Color bad5;
        public Color bad6;
        public Color bad7;
        public Color bad8;
        public Color bad9;
        public Color bad10;
        public Color bad11;
        public Color bad12;
        public Color bad13;
        public Color bad14;
        public Color bad15;
        public Color bad16;
        public Color bad17;
        public Color bad18;
        public Color bad19;
        public Color disabled0;
        public Color disabled1;
        public Color disabled2;
        public Color disabled3;
        public Color disabled4;
        public Color disabled5;
        public Color disabled6;
        public Color disabled7;
        public Color disabled8;
        public Color disabled9;
        public Color disabled10;
        public Color disabled11;
        public Color disabled12;
        public Color disabled13;
        public Color disabled14;
        public Color disabled15;
        public Color disabled16;
        public Color disabled17;
        public Color disabled18;
        public Color disabled19;
        public Color good0;
        public Color good1;
        public Color good2;
        public Color good3;
        public Color good4;
        public Color good5;
        public Color good6;
        public Color good7;
        public Color good8;
        public Color good9;
        public Color good10;
        public Color good11;
        public Color good12;
        public Color good13;
        public Color good14;
        public Color good15;
        public Color good16;
        public Color good17;
        public Color good18;
        public Color good19;
        public Color highlight0;
        public Color highlight1;
        public Color highlight2;
        public Color highlight3;
        public Color highlight4;
        public Color highlight5;
        public Color highlight6;
        public Color highlight7;
        public Color highlight8;
        public Color highlight9;
        public Color highlight10;
        public Color highlight11;
        public Color highlight12;
        public Color highlight13;
        public Color highlight14;
        public Color highlight15;
        public Color highlight16;
        public Color highlight17;
        public Color highlight18;
        public Color highlight19;
        public Color notable0;
        public Color notable1;
        public Color notable2;
        public Color notable3;
        public Color notable4;
        public Color notable5;
        public Color notable6;
        public Color notable7;
        public Color notable8;
        public Color notable9;
        public Color notable10;
        public Color notable11;
        public Color notable12;
        public Color notable13;
        public Color notable14;
        public Color notable15;
        public Color notable16;
        public Color notable17;
        public Color notable18;
        public Color notable19;
        private static ColorPalette _default;

        private static StringBuilder _logBuilder;

        public ColorPalette()
        {
            SetDefaults(this);
        }

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

        private Func<Color>[] GetGettersInternal()
        {
            return new Func<Color>[]
            {
                () => disabled0,
                () => disabled1,
                () => disabled2,
                () => disabled3,
                () => disabled4,
                () => disabled5,
                () => disabled6,
                () => disabled7,
                () => disabled8,
                () => disabled9,
                () => disabled10,
                () => disabled11,
                () => disabled12,
                () => disabled13,
                () => disabled14,
                () => disabled15,
                () => disabled16,
                () => disabled17,
                () => disabled18,
                () => disabled19,
                () => bad0,
                () => bad1,
                () => bad2,
                () => bad3,
                () => bad4,
                () => bad5,
                () => bad6,
                () => bad7,
                () => bad8,
                () => bad9,
                () => bad10,
                () => bad11,
                () => bad12,
                () => bad13,
                () => bad14,
                () => bad15,
                () => bad16,
                () => bad17,
                () => bad18,
                () => bad19,
                () => good0,
                () => good1,
                () => good2,
                () => good3,
                () => good4,
                () => good5,
                () => good6,
                () => good7,
                () => good8,
                () => good9,
                () => good10,
                () => good11,
                () => good12,
                () => good13,
                () => good14,
                () => good15,
                () => good16,
                () => good17,
                () => good18,
                () => good19,
                () => highlight0,
                () => highlight1,
                () => highlight2,
                () => highlight3,
                () => highlight4,
                () => highlight5,
                () => highlight6,
                () => highlight7,
                () => highlight8,
                () => highlight9,
                () => highlight10,
                () => highlight11,
                () => highlight12,
                () => highlight13,
                () => highlight14,
                () => highlight15,
                () => highlight16,
                () => highlight17,
                () => highlight18,
                () => highlight19,
                () => notable0,
                () => notable1,
                () => notable2,
                () => notable3,
                () => notable4,
                () => notable5,
                () => notable6,
                () => notable7,
                () => notable8,
                () => notable9,
                () => notable10,
                () => notable11,
                () => notable12,
                () => notable13,
                () => notable14,
                () => notable15,
                () => notable16,
                () => notable17,
                () => notable18,
                () => notable19
            };
        }

        private string[] GetLabelsInternal()
        {
            return new[]
            {
                nameof(disabled0),
                nameof(disabled1),
                nameof(disabled2),
                nameof(disabled3),
                nameof(disabled4),
                nameof(disabled5),
                nameof(disabled6),
                nameof(disabled7),
                nameof(disabled8),
                nameof(disabled9),
                nameof(disabled10),
                nameof(disabled11),
                nameof(disabled12),
                nameof(disabled13),
                nameof(disabled14),
                nameof(disabled15),
                nameof(disabled16),
                nameof(disabled17),
                nameof(disabled18),
                nameof(disabled19),
                nameof(bad0),
                nameof(bad1),
                nameof(bad2),
                nameof(bad3),
                nameof(bad4),
                nameof(bad5),
                nameof(bad6),
                nameof(bad7),
                nameof(bad8),
                nameof(bad9),
                nameof(bad10),
                nameof(bad11),
                nameof(bad12),
                nameof(bad13),
                nameof(bad14),
                nameof(bad15),
                nameof(bad16),
                nameof(bad17),
                nameof(bad18),
                nameof(bad19),
                nameof(good0),
                nameof(good1),
                nameof(good2),
                nameof(good3),
                nameof(good4),
                nameof(good5),
                nameof(good6),
                nameof(good7),
                nameof(good8),
                nameof(good9),
                nameof(good10),
                nameof(good11),
                nameof(good12),
                nameof(good13),
                nameof(good14),
                nameof(good15),
                nameof(good16),
                nameof(good17),
                nameof(good18),
                nameof(good19),
                nameof(highlight0),
                nameof(highlight1),
                nameof(highlight2),
                nameof(highlight3),
                nameof(highlight4),
                nameof(highlight5),
                nameof(highlight6),
                nameof(highlight7),
                nameof(highlight8),
                nameof(highlight9),
                nameof(highlight10),
                nameof(highlight11),
                nameof(highlight12),
                nameof(highlight13),
                nameof(highlight14),
                nameof(highlight15),
                nameof(highlight16),
                nameof(highlight17),
                nameof(highlight18),
                nameof(highlight19),
                nameof(notable0),
                nameof(notable1),
                nameof(notable2),
                nameof(notable3),
                nameof(notable4),
                nameof(notable5),
                nameof(notable6),
                nameof(notable7),
                nameof(notable8),
                nameof(notable9),
                nameof(notable10),
                nameof(notable11),
                nameof(notable12),
                nameof(notable13),
                nameof(notable14),
                nameof(notable15),
                nameof(notable16),
                nameof(notable17),
                nameof(notable18),
                nameof(notable19)
            };
        }

        private Action<Color>[] GetSettersInternal()
        {
            return new Action<Color>[]
            {
                c => disabled0 = c,
                c => disabled1 = c,
                c => disabled2 = c,
                c => disabled3 = c,
                c => disabled4 = c,
                c => disabled5 = c,
                c => disabled6 = c,
                c => disabled7 = c,
                c => disabled8 = c,
                c => disabled9 = c,
                c => disabled10 = c,
                c => disabled11 = c,
                c => disabled12 = c,
                c => disabled13 = c,
                c => disabled14 = c,
                c => disabled15 = c,
                c => disabled16 = c,
                c => disabled17 = c,
                c => disabled18 = c,
                c => disabled19 = c,
                c => bad0 = c,
                c => bad1 = c,
                c => bad2 = c,
                c => bad3 = c,
                c => bad4 = c,
                c => bad5 = c,
                c => bad6 = c,
                c => bad7 = c,
                c => bad8 = c,
                c => bad9 = c,
                c => bad10 = c,
                c => bad11 = c,
                c => bad12 = c,
                c => bad13 = c,
                c => bad14 = c,
                c => bad15 = c,
                c => bad16 = c,
                c => bad17 = c,
                c => bad18 = c,
                c => bad19 = c,
                c => good0 = c,
                c => good1 = c,
                c => good2 = c,
                c => good3 = c,
                c => good4 = c,
                c => good5 = c,
                c => good6 = c,
                c => good7 = c,
                c => good8 = c,
                c => good9 = c,
                c => good10 = c,
                c => good11 = c,
                c => good12 = c,
                c => good13 = c,
                c => good14 = c,
                c => good15 = c,
                c => good16 = c,
                c => good17 = c,
                c => good18 = c,
                c => good19 = c,
                c => highlight0 = c,
                c => highlight1 = c,
                c => highlight2 = c,
                c => highlight3 = c,
                c => highlight4 = c,
                c => highlight5 = c,
                c => highlight6 = c,
                c => highlight7 = c,
                c => highlight8 = c,
                c => highlight9 = c,
                c => highlight10 = c,
                c => highlight11 = c,
                c => highlight12 = c,
                c => highlight13 = c,
                c => highlight14 = c,
                c => highlight15 = c,
                c => highlight16 = c,
                c => highlight17 = c,
                c => highlight18 = c,
                c => highlight19 = c,
                c => notable0 = c,
                c => notable1 = c,
                c => notable2 = c,
                c => notable3 = c,
                c => notable4 = c,
                c => notable5 = c,
                c => notable6 = c,
                c => notable7 = c,
                c => notable8 = c,
                c => notable9 = c,
                c => notable10 = c,
                c => notable11 = c,
                c => notable12 = c,
                c => notable13 = c,
                c => notable14 = c,
                c => notable15 = c,
                c => notable16 = c,
                c => notable17 = c,
                c => notable18 = c,
                c => notable19 = c
            };
        }

        public static ColorPalette CreateDefault()
        {
            var newPalette = new ColorPalette();

            SetDefaults(newPalette);

            return newPalette;
        }

        private static Color[] GetMultiple(int count, Func<int, Color> get)
        {
            count = Mathf.Clamp(count, 1, 20);
            
            float current = 0.0f;
            float step = (count / (float) SIZE);

            var results = new Color[count];

            for (var i = 0; i < count; i++)
            {
                var castIndex = (int) current;

                results[i] = get(castIndex);
                
                current += step;
            }

            return results;
        }

        public Color[] GetBadMultiple(int count)
        {
            return GetMultiple(count, GetBad);
        }

        public Color[] GetGoodMultiple(int count)
        {
            return GetMultiple(count, GetGood);
        }

        public Color[] GetHighlightMultiple(int count)
        {
            return GetMultiple(count, GetHighlight);
        }

        public Color[] GetNotableMultiple(int count)
        {
            return GetMultiple(count, GetNotable);
        }

        public Color[] GetDisabledMultiple(int count)
        {
            return GetMultiple(count, GetDisabled);
        }

        public Color GetBad(int index)
        {
            index %= SIZE;

            if (index == 0) return bad0;
            if (index == 1) return bad1;
            if (index == 2) return bad2;
            if (index == 3) return bad3;
            if (index == 4) return bad4;
            if (index == 5) return bad5;
            if (index == 6) return bad6;
            if (index == 7) return bad7;
            if (index == 8) return bad8;
            if (index == 9) return bad9;
            if (index == 10) return bad10;
            if (index == 11) return bad11;
            if (index == 12) return bad12;
            if (index == 13) return bad13;
            if (index == 14) return bad14;
            if (index == 15) return bad15;
            if (index == 16) return bad16;
            if (index == 17) return bad17;
            if (index == 18) return bad18;
            return bad19;
        }

        public Color GetGood(int index)
        {
            index %= SIZE;

            if (index == 0) return good0;
            if (index == 1) return good1;
            if (index == 2) return good2;
            if (index == 3) return good3;
            if (index == 4) return good4;
            if (index == 5) return good5;
            if (index == 6) return good6;
            if (index == 7) return good7;
            if (index == 8) return good8;
            if (index == 9) return good9;
            if (index == 10) return good10;
            if (index == 11) return good11;
            if (index == 12) return good12;
            if (index == 13) return good13;
            if (index == 14) return good14;
            if (index == 15) return good15;
            if (index == 16) return good16;
            if (index == 17) return good17;
            if (index == 18) return good18;
            return good19;
        }

        public Color GetHighlight(int index)
        {
            index %= SIZE;

            if (index == 0) return highlight0;
            if (index == 1) return highlight1;
            if (index == 2) return highlight2;
            if (index == 3) return highlight3;
            if (index == 4) return highlight4;
            if (index == 5) return highlight5;
            if (index == 6) return highlight6;
            if (index == 7) return highlight7;
            if (index == 8) return highlight8;
            if (index == 9) return highlight9;
            if (index == 10) return highlight10;
            if (index == 11) return highlight11;
            if (index == 12) return highlight12;
            if (index == 13) return highlight13;
            if (index == 14) return highlight14;
            if (index == 15) return highlight15;
            if (index == 16) return highlight16;
            if (index == 17) return highlight17;
            if (index == 18) return highlight18;
            return highlight19;
        }

        public Color GetNotable(int index)
        {
            index %= SIZE;

            if (index == 0) return notable0;
            if (index == 1) return notable1;
            if (index == 2) return notable2;
            if (index == 3) return notable3;
            if (index == 4) return notable4;
            if (index == 5) return notable5;
            if (index == 6) return notable6;
            if (index == 7) return notable7;
            if (index == 8) return notable8;
            if (index == 9) return notable9;
            if (index == 10) return notable10;
            if (index == 11) return notable11;
            if (index == 12) return notable12;
            if (index == 13) return notable13;
            if (index == 14) return notable14;
            if (index == 15) return notable15;
            if (index == 16) return notable16;
            if (index == 17) return notable17;
            if (index == 18) return notable18;
            return notable19;
        }

        public Color GetDisabled(int index)
        {
            index %= SIZE;

            if (index == 0) return disabled0;
            if (index == 1) return disabled1;
            if (index == 2) return disabled2;
            if (index == 3) return disabled3;
            if (index == 4) return disabled4;
            if (index == 5) return disabled5;
            if (index == 6) return disabled6;
            if (index == 7) return disabled7;
            if (index == 8) return disabled8;
            if (index == 9) return disabled9;
            if (index == 10) return disabled10;
            if (index == 11) return disabled11;
            if (index == 12) return disabled12;
            if (index == 13) return disabled13;
            if (index == 14) return disabled14;
            if (index == 15) return disabled15;
            if (index == 16) return disabled16;
            if (index == 17) return disabled17;
            if (index == 18) return disabled18;
            return disabled19;
        }
        
        private static void SetDefaults(ColorPalette p)
        {
            var d0 = new Color(050f / 255f, 050f / 255f, 050f / 255f, 1f);
            var d9 = new Color(150f / 255f, 150f / 255f, 150f / 255f, 1f);
            
            var b0 = new Color(255f / 255f, 010f / 255f, 000f / 255f, 1f);
            var b9 = new Color(255f / 255f, 220f / 255f, 030f / 255f, 1f);

            var g0 = new Color(017f / 255f, 029f / 255f, 019f / 255f, 1f);
            var g9 = new Color(171f / 255f, 204f / 255f, 175f / 255f, 1f);
            
            var h0 = new Color(013f / 255f, 071f / 255f, 161f / 255f, 1f);
            var h9 = new Color(227f / 255f, 242f / 255f, 253f / 255f, 1f);
            
            var n0 = new Color(116f / 255f, 000f / 255f, 184f / 255f, 1f);
            var n9 = new Color(128f / 255f, 255f / 255f, 219f / 255f, 1f);

            var disabledRange = new Color(d9.r - d0.r,       d9.g - d0.g, d9.b - d0.b, d9.a - d0.a);
            var badRange = new Color(b9.r - b0.r,       b9.g - b0.g, b9.b - b0.b, b9.a - b0.a);
            var goodRange = new Color(g9.r - g0.r,      g9.g - g0.g, g9.b - g0.b, g9.a - g0.a);
            var highlightRange = new Color(h9.r - h0.r, h9.g - h0.g, h9.b - h0.b, h9.a - h0.a);
            var notableRange = new Color(n9.r - n0.r,   n9.g - n0.g, n9.b - n0.b, n9.a - n0.a);
            
            disabledRange *= .05f;
            badRange *= .05f;
            goodRange *= .05f;
            highlightRange *= .05f;
            notableRange *= .05f;
                
            p.disabled0 = b0;
            p.disabled1 = p.disabled0 + disabledRange;
            p.disabled2 = p.disabled1 + disabledRange;
            p.disabled3 = p.disabled2 + disabledRange;
            p.disabled4 = p.disabled3 + disabledRange;
            p.disabled5 = p.disabled4 + disabledRange;
            p.disabled6 = p.disabled5 + disabledRange;
            p.disabled7 = p.disabled6 + disabledRange;
            p.disabled8 = p.disabled7 + disabledRange;
            p.disabled9 = p.disabled8 + disabledRange;
            p.disabled10 = p.disabled9 + disabledRange;
            p.disabled11 = p.disabled10 + disabledRange;
            p.disabled12 = p.disabled11 + disabledRange;
            p.disabled13 = p.disabled12 + disabledRange;
            p.disabled14 = p.disabled13 + disabledRange;
            p.disabled15 = p.disabled14 + disabledRange;
            p.disabled16 = p.disabled15 + disabledRange;
            p.disabled17 = p.disabled16 + disabledRange;
            p.disabled18 = p.disabled17 + disabledRange;
            p.disabled19 = p.disabled18 + disabledRange;
            
            p.bad0 = b0;
            p.bad1 = p.bad0 + badRange;
            p.bad2 = p.bad1 + badRange;
            p.bad3 = p.bad2 + badRange;
            p.bad4 = p.bad3 + badRange;
            p.bad5 = p.bad4 + badRange;
            p.bad6 = p.bad5 + badRange;
            p.bad7 = p.bad6 + badRange;
            p.bad8 = p.bad7 + badRange;
            p.bad9 = p.bad8 + badRange;
            p.bad10 = p.bad9 + badRange;
            p.bad11 = p.bad10 + badRange;
            p.bad12 = p.bad11 + badRange;
            p.bad13 = p.bad12 + badRange;
            p.bad14 = p.bad13 + badRange;
            p.bad15 = p.bad14 + badRange;
            p.bad16 = p.bad15 + badRange;
            p.bad17 = p.bad16 + badRange;
            p.bad18 = p.bad17 + badRange;
            p.bad19 = p.bad18 + badRange;

            p.good0 = g0;
            p.good1 = p.good0 + goodRange;
            p.good2 = p.good1 + goodRange;
            p.good3 = p.good2 + goodRange;
            p.good4 = p.good3 + goodRange;
            p.good5 = p.good4 + goodRange;
            p.good6 = p.good5 + goodRange;
            p.good7 = p.good6 + goodRange;
            p.good8 = p.good7 + goodRange;
            p.good9 = p.good8 + goodRange;
            p.good10 = p.good9 + goodRange;
            p.good11 = p.good10 + goodRange;
            p.good12 = p.good11 + goodRange;
            p.good13 = p.good12 + goodRange;
            p.good14 = p.good13 + goodRange;
            p.good15 = p.good14 + goodRange;
            p.good16 = p.good15 + goodRange;
            p.good17 = p.good16 + goodRange;
            p.good18 = p.good17 + goodRange;
            p.good19 = p.good18 + goodRange;

            p.notable0 = n0;
            p.notable1 = p.notable0 + notableRange;
            p.notable2 = p.notable1 + notableRange;
            p.notable3 = p.notable2 + notableRange;
            p.notable4 = p.notable3 + notableRange;
            p.notable5 = p.notable4 + notableRange;
            p.notable6 = p.notable5 + notableRange;
            p.notable7 = p.notable6 + notableRange;
            p.notable8 = p.notable7 + notableRange;
            p.notable9 = p.notable8 + notableRange;
            p.notable10 = p.notable9 + notableRange;
            p.notable11 = p.notable10 + notableRange;
            p.notable12 = p.notable11 + notableRange;
            p.notable13 = p.notable12 + notableRange;
            p.notable14 = p.notable13 + notableRange;
            p.notable15 = p.notable14 + notableRange;
            p.notable16 = p.notable15 + notableRange;
            p.notable17 = p.notable16 + notableRange;
            p.notable18 = p.notable17 + notableRange;
            p.notable19 = p.notable18 + notableRange;

            p.highlight0 = h0;
            p.highlight1 = p.highlight0 + highlightRange;
            p.highlight2 = p.highlight1 + highlightRange;
            p.highlight3 = p.highlight2 + highlightRange;
            p.highlight4 = p.highlight3 + highlightRange;
            p.highlight5 = p.highlight4 + highlightRange;
            p.highlight6 = p.highlight5 + highlightRange;
            p.highlight7 = p.highlight6 + highlightRange;
            p.highlight8 = p.highlight7 + highlightRange;
            p.highlight9 = p.highlight8 + highlightRange;
            p.highlight10 = p.highlight9 + highlightRange;
            p.highlight11 = p.highlight10 + highlightRange;
            p.highlight12 = p.highlight11 + highlightRange;
            p.highlight13 = p.highlight12 + highlightRange;
            p.highlight14 = p.highlight13 + highlightRange;
            p.highlight15 = p.highlight14 + highlightRange;
            p.highlight16 = p.highlight15 + highlightRange;
            p.highlight17 = p.highlight16 + highlightRange;
            p.highlight18 = p.highlight17 + highlightRange;
            p.highlight19 = p.highlight18 + highlightRange;
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
