using System;
using JetBrains.Annotations;

namespace Appalachia.Utility.Enums
{
    public static class FlagExtensions
    {
        [Pure]
        public static T ClearFlag<T>(this T value, T flag)
            where T : Enum
        {
            var underlyingType = Enum.GetUnderlyingType(value.GetType());

            var valueAsInt = (int) Convert.ChangeType(value, underlyingType);
            var flagAsInt = (int) Convert.ChangeType(flag,   underlyingType);

            valueAsInt &= ~flagAsInt;

            return (T) (valueAsInt as object);
        }

        [Pure]
        public static bool HasAny<T>(this T value, T flags)
            where T : Enum
        {
            var underlyingType = Enum.GetUnderlyingType(value.GetType());

            var valueAsInt = (int) Convert.ChangeType(value, underlyingType);
            var flagAsInt = (int) Convert.ChangeType(flags,  underlyingType);

            return (valueAsInt & flagAsInt) == 0;
        }

        [Pure]
        public static bool HasFlag<T>(this T value, T flag)
            where T : Enum
        {
            var underlyingType = Enum.GetUnderlyingType(value.GetType());

            var valueAsInt = (int) Convert.ChangeType(value, underlyingType);
            var flagAsInt = (int) Convert.ChangeType(flag,   underlyingType);

            return (valueAsInt & flagAsInt) == flagAsInt;
        }

        [Pure]
        public static T SetFlag<T>(this T value, T flag)
            where T : Enum
        {
            var underlyingType = Enum.GetUnderlyingType(value.GetType());

            var valueAsInt = (int) Convert.ChangeType(value, underlyingType);
            var flagAsInt = (int) Convert.ChangeType(flag,   underlyingType);

            valueAsInt |= flagAsInt;

            return (T) (valueAsInt as object);
        }
    }
}
