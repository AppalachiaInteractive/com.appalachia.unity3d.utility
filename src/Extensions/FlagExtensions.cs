using System;
using JetBrains.Annotations;
using Unity.Profiling;

namespace Appalachia.Utility.Enums
{
    public static class FlagExtensions
    {
        public static long GetSignedValue<T>(this T value)
            where T : Enum
        {
            using (_PRF_GetSignedValue.Auto())
            {
                var type = value.GetUnderlyingEnumType();

                if (type == typeof(short))
                {
                    return (short)Convert.ChangeType(value, type);
                }

                if (type == typeof(int))
                {
                    return (int)Convert.ChangeType(value, type);
                }

                return (long)Convert.ChangeType(value, type);
            }
        }

        public static Type GetUnderlyingEnumType<T>(this T value)
            where T : Enum
        {
            using (_PRF_GetUnderlyingEnumType.Auto())
            {
                var enumValueType = value.GetType();
                return Enum.GetUnderlyingType(enumValueType);
            }
        }

        public static ulong GetUnsignedValue<T>(this T value)
            where T : Enum
        {
            using (_PRF_GetUnsignedValue.Auto())
            {
                var type = value.GetUnderlyingEnumType();

                if (type == typeof(ushort))
                {
                    return (ushort)Convert.ChangeType(value, type);
                }

                if (type == typeof(uint))
                {
                    return (uint)Convert.ChangeType(value, type);
                }

                return (ulong)Convert.ChangeType(value, type);
            }
        }

        [Pure]
        public static bool Has<T>(this T value, T flag)
            where T : Enum
        {
            using (_PRF_Has.Auto())
            {
                // false          true
                // A: 000111000   A: 000111000
                // B: 001010000   B: 000010000* 
                // |: 001111000   |: 000111000
                // &: 000010000   &: 000010000* & == B
                if (value.IsUnsignedEnum())
                {
                    var valueNum = value.GetUnsignedValue();
                    var flagNum = flag.GetUnsignedValue();

                    return (valueNum & flagNum) == flagNum;
                }
                else
                {
                    var valueNum = value.GetSignedValue();
                    var flagNum = flag.GetSignedValue();

                    return (valueNum & flagNum) == flagNum;
                }
            }
        }

        [Pure]
        public static bool HasAny<T>(this T value, T flags)
            where T : Enum
        {
            using (_PRF_HasAny.Auto())
            {
                // false          true
                // A: 000111000   A: 000111000
                // B: 111000111   B: 111100111 
                // |: 111111111   |: 111111111
                // &: 000000111   &: 000100000 * != 0
                if (value.IsUnsignedEnum())
                {
                    var valueNum = value.GetUnsignedValue();
                    var flagNum = flags.GetUnsignedValue();

                    return (valueNum & flagNum) != 0;
                }
                else
                {
                    var valueNum = value.GetSignedValue();
                    var flagNum = flags.GetSignedValue();

                    return (valueNum & flagNum) != 0;
                }
            }
        }

        [Pure]
        public static bool HasNone<T>(this T value, T flags)
            where T : Enum
        {
            using (_PRF_HasNone.Auto())
            {
                // false          true
                // A: 000111000   A: 000111000
                // B: 111100111   B: 111000111 
                // |: 111111111   |: 111111111
                // &: 000100111   &: 000000000 * == 0
                if (value.IsUnsignedEnum())
                {
                    var valueNum = value.GetUnsignedValue();
                    var flagNum = flags.GetUnsignedValue();

                    return (valueNum & flagNum) == 0;
                }
                else
                {
                    var valueNum = value.GetSignedValue();
                    var flagNum = flags.GetSignedValue();

                    return (valueNum & flagNum) == 0;
                }
            }
        }

        public static bool IsUnsignedEnum<T>(this T value)
            where T : Enum
        {
            using (_PRF_IsUnsignedEnum.Auto())
            {
                var type = value.GetUnderlyingEnumType();

                if ((type == typeof(ulong)) || (type == typeof(uint)) || (type == typeof(ushort)))
                {
                    return true;
                }

                return false;
            }
        }

        [Pure]
        public static T SetFlag<T>(this T value, T flag)
            where T : Enum
        {
            using (_PRF_SetFlag.Auto())
            {
                if (value.IsUnsignedEnum())
                {
                    var valueNum = value.GetUnsignedValue();
                    var flagNum = flag.GetUnsignedValue();

                    valueNum |= flagNum;

                    return valueNum.ConvertBack<T>();
                }
                else
                {
                    var valueNum = value.GetSignedValue();
                    var flagNum = flag.GetSignedValue();

                    valueNum |= flagNum;

                    return valueNum.ConvertBack<T>();
                }
            }
        }

        [Pure]
        public static T SetFlags<T>(this T value, params T[] flags)
            where T : Enum
        {
            using (_PRF_SetFlags.Auto())
            {
                if (value.IsUnsignedEnum())
                {
                    var valueNum = value.GetUnsignedValue();

                    for (var i = 0; i < flags.Length; i++)
                    {
                        var flag = flags[i];

                        var flagNum = flag.GetUnsignedValue();

                        valueNum |= flagNum;
                    }

                    return valueNum.ConvertBack<T>();
                }
                else
                {
                    var valueNum = value.GetSignedValue();

                    for (var i = 0; i < flags.Length; i++)
                    {
                        var flag = flags[i];

                        var flagNum = flag.GetSignedValue();

                        valueNum |= flagNum;
                    }

                    return valueNum.ConvertBack<T>();
                }
            }
        }

        [Pure]
        public static T UnsetFlag<T>(this T value, T flag)
            where T : Enum
        {
            using (_PRF_UnsetFlag.Auto())
            {
                if (value.IsUnsignedEnum())
                {
                    var valueNum = value.GetUnsignedValue();
                    var flagNum = flag.GetUnsignedValue();

                    valueNum &= ~flagNum;

                    return valueNum.ConvertBack<T>();
                }
                else
                {
                    var valueNum = value.GetSignedValue();
                    var flagNum = flag.GetSignedValue();

                    valueNum &= ~flagNum;

                    return valueNum.ConvertBack<T>();
                }
            }
        }

        [Pure]
        public static T UnsetFlags<T>(this T value, params T[] flags)
            where T : Enum
        {
            using (_PRF_UnsetFlags.Auto())
            {
                foreach (var flag in flags)
                {
                    value = value.UnsetFlag(flag);
                }

                return value;
            }
        }

        private static T ConvertBack<T>(this long value)
            where T : Enum
        {
            using (_PRF_ConvertBack.Auto())
            {
                var type = default(T).GetUnderlyingEnumType();

                if (type == typeof(long))
                {
                    return (T)(object)value;
                }

                if (type == typeof(int))
                {
                    return (T)(object)(int)value;
                }

                return (T)(object)(short)value;
            }
        }

        private static T ConvertBack<T>(this ulong value)
            where T : Enum
        {
            using (_PRF_ConvertBack.Auto())
            {
                var type = default(T).GetUnderlyingEnumType();

                if (type == typeof(ulong))
                {
                    return (T)(object)value;
                }

                if (type == typeof(uint))
                {
                    return (T)(object)(uint)value;
                }

                return (T)(object)(ushort)value;
            }
        }

        #region Profiling

        private const string _PRF_PFX = nameof(FlagExtensions) + ".";

        private static readonly ProfilerMarker _PRF_SetFlags =
            new ProfilerMarker(_PRF_PFX + nameof(SetFlags));

        private static readonly ProfilerMarker _PRF_GetSignedValue =
            new ProfilerMarker(_PRF_PFX + nameof(GetSignedValue));

        private static readonly ProfilerMarker _PRF_UnsetFlag =
            new ProfilerMarker(_PRF_PFX + nameof(UnsetFlag));

        private static readonly ProfilerMarker _PRF_UnsetFlags =
            new ProfilerMarker(_PRF_PFX + nameof(UnsetFlags));

        private static readonly ProfilerMarker _PRF_IsUnsignedEnum =
            new ProfilerMarker(_PRF_PFX + nameof(IsUnsignedEnum));

        private static readonly ProfilerMarker _PRF_GetUnsignedValue =
            new ProfilerMarker(_PRF_PFX + nameof(GetUnsignedValue));

        private static readonly ProfilerMarker _PRF_GetUnderlyingEnumType =
            new ProfilerMarker(_PRF_PFX + nameof(GetUnderlyingEnumType));

        private static readonly ProfilerMarker _PRF_SetFlag = new ProfilerMarker(_PRF_PFX + nameof(SetFlag));

        private static readonly ProfilerMarker _PRF_ConvertBack =
            new ProfilerMarker(_PRF_PFX + nameof(ConvertBack));

        private static readonly ProfilerMarker _PRF_Has = new ProfilerMarker(_PRF_PFX + nameof(Has));

        private static readonly ProfilerMarker _PRF_HasAny = new ProfilerMarker(_PRF_PFX + nameof(HasAny));

        private static readonly ProfilerMarker _PRF_HasNone = new ProfilerMarker(_PRF_PFX + nameof(HasNone));

        #endregion
    }
}
