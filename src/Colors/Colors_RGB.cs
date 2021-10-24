using UnityEngine;

namespace Appalachia.Utility.Colors
{
    public static partial class Colors
    {
    
        public static Color UpdateR(this Color color, float value)
        {
            return UpdateRed(color, value);
        }

        public static Color UpdateG(this Color color, float value)
        {
            return UpdateGreen(color, value);
        }

        public static Color UpdateB(this Color color, float value)
        {
            return UpdateBlue(color, value);
        }

        public static Color UpdateA(this Color color, float value)
        {
            return UpdateAlpha(color, value);
        }

        public static Color UpdateRed(this Color color, float value)
        {
            return new(value, color.g, color.b, color.a);
        }

        public static Color UpdateGreen(this Color color, float value)
        {
            return new(color.r, value, color.b, color.a);
        }

        public static Color UpdateBlue(this Color color, float value)
        {
            return new(color.r, color.g, value, color.a);
        }

        public static Color UpdateAlpha(this Color color, float value)
        {
            return new(color.r, color.g, color.b, value);
        }
        
        public static Color ScaleR(this Color color, float factor)
        {
            return ScaleRed(color, factor);
        }

        public static Color ScaleG(this Color color, float factor)
        {
            return ScaleGreen(color, factor);
        }

        public static Color ScaleB(this Color color, float factor)
        {
            return ScaleBlue(color, factor);
        }

        public static Color ScaleA(this Color color, float factor)
        {
            return ScaleAlpha(color, factor);
        }

        public static Color ScaleRed(this Color color, float factor)
        {
            return new(color.r * factor, color.g, color.b, color.a);
        }

        public static Color ScaleGreen(this Color color, float factor)
        {
            return new(color.r, color.g * factor, color.b, color.a);
        }

        public static Color ScaleBlue(this Color color, float factor)
        {
            return new(color.r, color.g, color.b * factor, color.a);
        }

        public static Color ScaleAlpha(this Color color, float factor)
        {
            return new(color.r, color.g, color.b, color.a * factor);
        }
    }
}
