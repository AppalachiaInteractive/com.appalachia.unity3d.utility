using UnityEngine;

namespace Appalachia.Utility.Colors
{
    public static partial class Colors
    {
        public static Color BlendHSV(this Color color, Color other)
        {
            Color.RGBToHSV(color, out var hue1, out var sat1, out var value1);
            Color.RGBToHSV(other, out var hue2, out var sat2, out var value2);

            var c = Color.HSVToRGB((hue1 + hue2) / 2f, (sat1 + sat2) / 2f, (value1 + value2) / 2f);

            c.a = (color.a + other.a) / 2f;

            return c;
        }

        public static Color BlendRGB(this Color color, Color other)
        {
            var c = new Color(
                (color.r + other.r) / 2f,
                (color.g + other.g) / 2f,
                (color.b + other.b) / 2f,
                color.a
            );
            return c;
        }

        public static Color BlendRGBA(this Color color, Color other)
        {
            var c = new Color(
                (color.r + other.r) / 2f,
                (color.g + other.g) / 2f,
                (color.b + other.b) / 2f,
                (color.a + other.a) / 2f
            );
            return c;
        }

        public static void ToHSV(this Color color, out float h, out float s, out float v)
        {
            Color.RGBToHSV(color, out h, out s, out v);
        }
    }
}
