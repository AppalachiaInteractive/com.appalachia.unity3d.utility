using System.Globalization;

namespace Appalachia.Utility.Strings.Number
{
    internal static class NumberFormatInfoEx
    {
        internal static bool HasInvariantNumberSigns(this NumberFormatInfo info)
        {
            return (info.PositiveSign == "+") && (info.NegativeSign == "-");
        }
    }
}
