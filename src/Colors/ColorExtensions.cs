using UnityEngine;

namespace Appalachia.Utility.Colors
{
    public static class ColorExtensions
    {
        public static Color ToGradient(this long current, long low, long high, ColorPaletteSubset palette)
        {
            var time = (current - low) / (double) (high - low);

            return palette.Gradient((float) time);
        }
    }
}
