using System;
using UnityEngine;

namespace Appalachia.Utility.Colors
{
    public class ColorPaletteSubset
    {
        public const int SIZE = 20;

        public ColorPaletteSubset(string name)
        {
            this.name = name;
        }

        public ColorPaletteSubset(string name, Color low, Color high) : this(name)
        {
            var range = new Color(
                Mathf.Clamp01(high.r - low.r),
                Mathf.Clamp01(high.g - low.g),
                Mathf.Clamp01(high.b - low.b),
                Mathf.Clamp01(high.a - low.a)
            );

            var step = 1f / SIZE;

            range *= step;

            c00 = low;
            c01 = c00 + range;
            c02 = c01 + range;
            c03 = c02 + range;
            c04 = c03 + range;
            c05 = c04 + range;
            c06 = c05 + range;
            c07 = c06 + range;
            c08 = c07 + range;
            c09 = c08 + range;
            c10 = c09 + range;
            c11 = c10 + range;
            c12 = c11 + range;
            c13 = c12 + range;
            c14 = c13 + range;
            c15 = c14 + range;
            c16 = c15 + range;
            c17 = c16 + range;
            c18 = c17 + range;
            c19 = c18 + range;
        }

        public Color c00;
        public Color c01;
        public Color c02;
        public Color c03;
        public Color c04;
        public Color c05;
        public Color c06;
        public Color c07;
        public Color c08;
        public Color c09;
        public Color c10;
        public Color c11;
        public Color c12;
        public Color c13;
        public Color c14;
        public Color c15;
        public Color c16;
        public Color c17;
        public Color c18;
        public Color c19;

        public string name;

        private Action<Color>[] _setters;

        private Func<Color>[] _getters;

        private string[] _labels;

        public Color First => Get(0);
        public Color Last => Get((int)SIZE - 1);
        public Color Middle => Get(((int)SIZE / 2) - 1);
        public Color Quarter => Get((int) (1f * (SIZE / 4f)) - 1);
        public Color ThreeQuarters => Get((int) (3f * (SIZE / 4f)) - 1);
        public Color Third => Get((int) (1f * (SIZE / 3f)) - 1);
        public Color TwoThirds => Get((int) (2f * (SIZE / 3f)) - 1);

        public Color Gradient(float time, bool clamp = true)
        {
            if (clamp)
            {
                time = Mathf.Clamp01(time);
            }
            
            var index = (int) ((time * SIZE) % SIZE);

            return Get(index);
        }
        
        public Color Get(int index)
        {
            index %= SIZE;

            if (index == 0)
            {
                return c00;
            }

            if (index == 1)
            {
                return c01;
            }

            if (index == 2)
            {
                return c02;
            }

            if (index == 3)
            {
                return c03;
            }

            if (index == 4)
            {
                return c04;
            }

            if (index == 5)
            {
                return c05;
            }

            if (index == 6)
            {
                return c06;
            }

            if (index == 7)
            {
                return c07;
            }

            if (index == 8)
            {
                return c08;
            }

            if (index == 9)
            {
                return c09;
            }

            if (index == 10)
            {
                return c10;
            }

            if (index == 11)
            {
                return c11;
            }

            if (index == 12)
            {
                return c12;
            }

            if (index == 13)
            {
                return c13;
            }

            if (index == 14)
            {
                return c14;
            }

            if (index == 15)
            {
                return c15;
            }

            if (index == 16)
            {
                return c16;
            }

            if (index == 17)
            {
                return c17;
            }

            if (index == 18)
            {
                return c18;
            }

            return c19;
        }

        public Color[] Multiple(int count)
        {
            count = Mathf.Clamp(count, 1, 20);

            var current = 0.0f;
            var step = SIZE / (float) count;

            var results = new Color[count];

            for (var i = 0; i < count; i++)
            {
                var castIndex = (int) current;

                results[i] = Get(castIndex);

                current += step;
            }

            return results;
        }

        public void Set(int index, Color c)
        {
            index %= SIZE;

            if (index == 0)
            {
                c00 = c;
            }

            if (index == 1)
            {
                c01 = c;
            }

            if (index == 2)
            {
                c02 = c;
            }

            if (index == 3)
            {
                c03 = c;
            }

            if (index == 4)
            {
                c04 = c;
            }

            if (index == 5)
            {
                c05 = c;
            }

            if (index == 6)
            {
                c06 = c;
            }

            if (index == 7)
            {
                c07 = c;
            }

            if (index == 8)
            {
                c08 = c;
            }

            if (index == 9)
            {
                c09 = c;
            }

            if (index == 10)
            {
                c10 = c;
            }

            if (index == 11)
            {
                c11 = c;
            }

            if (index == 12)
            {
                c12 = c;
            }

            if (index == 13)
            {
                c13 = c;
            }

            if (index == 14)
            {
                c14 = c;
            }

            if (index == 15)
            {
                c15 = c;
            }

            if (index == 16)
            {
                c16 = c;
            }

            if (index == 17)
            {
                c17 = c;
            }

            if (index == 18)
            {
                c18 = c;
            }

            c19 = c;
        }

        internal Action<Color>[] GetSettersInternal()
        {
            if (_setters == null)
            {
                _setters = new Action<Color>[SIZE];

                for (var i = 0; i < SIZE; i++)
                {
                    var i1 = i;
                    _setters[i] = c => Set(i1, c);
                }
            }

            return _setters;
        }

        internal Func<Color>[] GetGettersInternal()
        {
            if (_getters == null)
            {
                _getters = new Func<Color>[SIZE];

                for (var i = 0; i < SIZE; i++)
                {
                    _getters[i] = () => Get(i);
                }
            }

            return _getters;
        }

        internal string[] GetLabelsInternal()
        {
            if (_labels == null)
            {
                _labels = new string[SIZE];

                for (var i = 0; i < SIZE; i++)
                {
                    _labels[i] = $"{name} - {i:00}";
                }
            }

            return _labels;
        }
    }
}
