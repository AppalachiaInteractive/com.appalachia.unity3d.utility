using UnityEngine;

namespace Appalachia.Utility.Colors
{
    public static partial class Colors
    {
        public static Color HSVToRGB(float H, float S, float V, float A, bool hdr)
        {
            var shouldNormalizeH = H > 1.5f;
            var shouldNormalizeS = S > 1.5f;
            var shouldNormalizeV = V > 1.5f;

            var shouldNormalizeExtra = (H > 3.0f) || (S > 3.0f) || (V > 3.0f);

            var normalizationCount = shouldNormalizeH ? 1 : 0;
            normalizationCount += shouldNormalizeS ? 1 : 0;
            normalizationCount += shouldNormalizeV ? 1 : 0;
            normalizationCount += shouldNormalizeExtra ? 3 : 0;

            if (normalizationCount >= 2)
            {
                H /= 360f;
                S /= 100f;
                V /= 100f;
            }

            var c = Color.HSVToRGB(H, S, V, hdr);
            c.a = A;

            return c;
        }

        public static void RGBToHSV(this Color rgbColor, out float H, out float S, out float V)
        {
            Color.RGBToHSV(rgbColor, out H, out S, out V);
        }

        public static Color ScaleH(this Color color, float factor, bool allowHdr = true)
        {
            return ScaleHue(color, factor, allowHdr);
        }

        public static Color ScaleHue(this Color color, float factor, bool allowHdr = true)
        {
            color.RGBToHSV(out var hue, out var sat, out var value);
            return HSVToRGB(hue * factor, sat, value, color.a, allowHdr);
        }

        public static Color ScaleS(this Color color, float factor, bool allowHdr = true)
        {
            return ScaleSaturation(color, factor, allowHdr);
        }

        public static Color ScaleSaturation(this Color color, float factor, bool allowHdr = true)
        {
            color.RGBToHSV(out var hue, out var sat, out var value);
            return HSVToRGB(hue, sat * factor, value, color.a, allowHdr);
        }

        public static Color ScaleV(this Color color, float factor, bool allowHdr = true)
        {
            return ScaleValue(color, factor, allowHdr);
        }

        public static Color ScaleValue(this Color color, float factor, bool allowHdr = true)
        {
            color.RGBToHSV(out var hue, out var sat, out var value);
            return HSVToRGB(hue, sat, value * factor, color.a, allowHdr);
        }

        public static Color UpdateH(this Color color, float value, bool allowHdr = true)
        {
            return UpdateHue(color, value, allowHdr);
        }

        public static Color UpdateHue(this Color color, float newHue, bool allowHdr = true)
        {
            color.RGBToHSV(out _, out var sat, out var value);
            return HSVToRGB(newHue, sat, value, color.a, allowHdr);
        }

        public static Color UpdateS(this Color color, float value, bool allowHdr = true)
        {
            return UpdateSaturation(color, value, allowHdr);
        }

        public static Color UpdateSaturation(this Color color, float newSaturation, bool allowHdr = true)
        {
            color.RGBToHSV(out var hue, out _, out var value);
            return HSVToRGB(hue, newSaturation, value, color.a, allowHdr);
        }

        public static Color UpdateV(this Color color, float value, bool allowHdr = true)
        {
            return UpdateValue(color, value, allowHdr);
        }

        public static Color UpdateValue(this Color color, float newValue, bool allowHdr = true)
        {
            Color.RGBToHSV(color, out var hue, out var sat, out _);
            return HSVToRGB(hue, sat, newValue, color.a, allowHdr);
        }
    }
}
