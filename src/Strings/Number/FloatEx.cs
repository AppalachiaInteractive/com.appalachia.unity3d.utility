using System;
using System.Runtime.CompilerServices;

namespace Appalachia.Utility.Strings.Number
{
    internal static class FloatEx
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsFinite(double d)
        {
            var bits = BitConverter.DoubleToInt64Bits(d);
            return (bits & 0x7FFFFFFFFFFFFFFF) < 0x7FF0000000000000;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNegative(double d)
        {
            return BitConverter.DoubleToInt64Bits(d) < 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsFinite(float f)
        {
            var bits = SingleToInt32Bits(f);
            return (bits & 0x7FFFFFFF) < 0x7F800000;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNegative(float f)
        {
            return SingleToInt32Bits(f) < 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe int SingleToInt32Bits(float value)
        {
            return *(int*)&value;
        }
    }
}
