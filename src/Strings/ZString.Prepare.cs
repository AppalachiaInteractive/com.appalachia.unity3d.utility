using JetBrains.Annotations;
using Unity.Profiling;

namespace Appalachia.Utility.Strings
{
    public static partial class ZString
    {
        /// <summary>Prepare string format to avoid parse template in each operation.</summary>
        [StringFormatMethod("format")]
        public static Utf16PreparedFormat<T1> PrepareUtf16<T1>(string format)
        {
            using (_PRF_PrepareUtf16.Auto())
            {
                return new Utf16PreparedFormat<T1>(format);
            }
        }

        /// <summary>Prepare string format to avoid parse template in each operation.</summary>
        [StringFormatMethod("format")]
        public static Utf16PreparedFormat<T1, T2> PrepareUtf16<T1, T2>(string format)
        {
            using (_PRF_PrepareUtf16.Auto())
            {
                return new Utf16PreparedFormat<T1, T2>(format);
            }
        }

        /// <summary>Prepare string format to avoid parse template in each operation.</summary>
        [StringFormatMethod("format")]
        public static Utf16PreparedFormat<T1, T2, T3> PrepareUtf16<T1, T2, T3>(string format)
        {
            using (_PRF_PrepareUtf16.Auto())
            {
                return new Utf16PreparedFormat<T1, T2, T3>(format);
            }
        }

        /// <summary>Prepare string format to avoid parse template in each operation.</summary>
        [StringFormatMethod("format")]
        public static Utf16PreparedFormat<T1, T2, T3, T4> PrepareUtf16<T1, T2, T3, T4>(string format)
        {
            using (_PRF_PrepareUtf16.Auto())
            {
                return new Utf16PreparedFormat<T1, T2, T3, T4>(format);
            }
        }

        /// <summary>Prepare string format to avoid parse template in each operation.</summary>
        [StringFormatMethod("format")]
        public static Utf16PreparedFormat<T1, T2, T3, T4, T5> PrepareUtf16<T1, T2, T3, T4, T5>(string format)
        {
            using (_PRF_PrepareUtf16.Auto())
            {
                return new Utf16PreparedFormat<T1, T2, T3, T4, T5>(format);
            }
        }

        /// <summary>Prepare string format to avoid parse template in each operation.</summary>
        [StringFormatMethod("format")]
        public static Utf16PreparedFormat<T1, T2, T3, T4, T5, T6> PrepareUtf16<T1, T2, T3, T4, T5, T6>(
            string format)
        {
            using (_PRF_PrepareUtf16.Auto())
            {
                return new Utf16PreparedFormat<T1, T2, T3, T4, T5, T6>(format);
            }
        }

        /// <summary>Prepare string format to avoid parse template in each operation.</summary>
        [StringFormatMethod("format")]
        public static Utf16PreparedFormat<T1, T2, T3, T4, T5, T6, T7>
            PrepareUtf16<T1, T2, T3, T4, T5, T6, T7>(string format)
        {
            using (_PRF_PrepareUtf16.Auto())
            {
                return new Utf16PreparedFormat<T1, T2, T3, T4, T5, T6, T7>(format);
            }
        }

        /// <summary>Prepare string format to avoid parse template in each operation.</summary>
        [StringFormatMethod("format")]
        public static Utf16PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8> PrepareUtf16<
            T1, T2, T3, T4, T5, T6, T7, T8>(string format)
        {
            using (_PRF_PrepareUtf16.Auto())
            {
                return new Utf16PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8>(format);
            }
        }

        /// <summary>Prepare string format to avoid parse template in each operation.</summary>
        [StringFormatMethod("format")]
        public static Utf16PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9> PrepareUtf16<
            T1, T2, T3, T4, T5, T6, T7, T8, T9>(string format)
        {
            using (_PRF_PrepareUtf16.Auto())
            {
                return new Utf16PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9>(format);
            }
        }

        /// <summary>Prepare string format to avoid parse template in each operation.</summary>
        [StringFormatMethod("format")]
        public static Utf16PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> PrepareUtf16<
            T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string format)
        {
            using (_PRF_PrepareUtf16.Auto())
            {
                return new Utf16PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(format);
            }
        }

        /// <summary>Prepare string format to avoid parse template in each operation.</summary>
        [StringFormatMethod("format")]
        public static Utf16PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> PrepareUtf16<
            T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string format)
        {
            using (_PRF_PrepareUtf16.Auto())
            {
                return new Utf16PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(format);
            }
        }

        /// <summary>Prepare string format to avoid parse template in each operation.</summary>
        [StringFormatMethod("format")]
        public static Utf16PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> PrepareUtf16<
            T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string format)
        {
            using (_PRF_PrepareUtf16.Auto())
            {
                return new Utf16PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(format);
            }
        }

        /// <summary>Prepare string format to avoid parse template in each operation.</summary>
        [StringFormatMethod("format")]
        public static Utf16PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>
            PrepareUtf16<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(string format)
        {
            using (_PRF_PrepareUtf16.Auto())
            {
                return new Utf16PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(
                    format
                );
            }
        }

        /// <summary>Prepare string format to avoid parse template in each operation.</summary>
        [StringFormatMethod("format")]
        public static Utf16PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>
            PrepareUtf16<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(string format)
        {
            using (_PRF_PrepareUtf16.Auto())
            {
                return new Utf16PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(
                    format
                );
            }
        }

        /// <summary>Prepare string format to avoid parse template in each operation.</summary>
        [StringFormatMethod("format")]
        public static Utf16PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>
            PrepareUtf16<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(string format)
        {
            using (_PRF_PrepareUtf16.Auto())
            {
                return new Utf16PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14,
                    T15>(format);
            }
        }

        /// <summary>Prepare string format to avoid parse template in each operation.</summary>
        [StringFormatMethod("format")]
        public static
            Utf16PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>
            PrepareUtf16<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(string format)
        {
            using (_PRF_PrepareUtf16.Auto())
            {
                return new Utf16PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14,
                    T15, T16>(format);
            }
        }

        /// <summary>Prepare string format to avoid parse template in each operation.</summary>
        [StringFormatMethod("format")]
        public static Utf8PreparedFormat<T1> PrepareUtf8<T1>(string format)
        {
            using (_PRF_PrepareUtf8.Auto())
            {
                return new Utf8PreparedFormat<T1>(format);
            }
        }

        /// <summary>Prepare string format to avoid parse template in each operation.</summary>
        [StringFormatMethod("format")]
        public static Utf8PreparedFormat<T1, T2> PrepareUtf8<T1, T2>(string format)
        {
            using (_PRF_PrepareUtf8.Auto())
            {
                return new Utf8PreparedFormat<T1, T2>(format);
            }
        }

        /// <summary>Prepare string format to avoid parse template in each operation.</summary>
        [StringFormatMethod("format")]
        public static Utf8PreparedFormat<T1, T2, T3> PrepareUtf8<T1, T2, T3>(string format)
        {
            using (_PRF_PrepareUtf8.Auto())
            {
                return new Utf8PreparedFormat<T1, T2, T3>(format);
            }
        }

        /// <summary>Prepare string format to avoid parse template in each operation.</summary>
        [StringFormatMethod("format")]
        public static Utf8PreparedFormat<T1, T2, T3, T4> PrepareUtf8<T1, T2, T3, T4>(string format)
        {
            using (_PRF_PrepareUtf8.Auto())
            {
                return new Utf8PreparedFormat<T1, T2, T3, T4>(format);
            }
        }

        /// <summary>Prepare string format to avoid parse template in each operation.</summary>
        [StringFormatMethod("format")]
        public static Utf8PreparedFormat<T1, T2, T3, T4, T5> PrepareUtf8<T1, T2, T3, T4, T5>(string format)
        {
            using (_PRF_PrepareUtf8.Auto())
            {
                return new Utf8PreparedFormat<T1, T2, T3, T4, T5>(format);
            }
        }

        /// <summary>Prepare string format to avoid parse template in each operation.</summary>
        [StringFormatMethod("format")]
        public static Utf8PreparedFormat<T1, T2, T3, T4, T5, T6> PrepareUtf8<T1, T2, T3, T4, T5, T6>(
            string format)
        {
            using (_PRF_PrepareUtf8.Auto())
            {
                return new Utf8PreparedFormat<T1, T2, T3, T4, T5, T6>(format);
            }
        }

        /// <summary>Prepare string format to avoid parse template in each operation.</summary>
        [StringFormatMethod("format")]
        public static Utf8PreparedFormat<T1, T2, T3, T4, T5, T6, T7> PrepareUtf8<T1, T2, T3, T4, T5, T6, T7>(
            string format)
        {
            using (_PRF_PrepareUtf8.Auto())
            {
                return new Utf8PreparedFormat<T1, T2, T3, T4, T5, T6, T7>(format);
            }
        }

        /// <summary>Prepare string format to avoid parse template in each operation.</summary>
        [StringFormatMethod("format")]
        public static Utf8PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8> PrepareUtf8<
            T1, T2, T3, T4, T5, T6, T7, T8>(string format)
        {
            using (_PRF_PrepareUtf8.Auto())
            {
                return new Utf8PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8>(format);
            }
        }

        /// <summary>Prepare string format to avoid parse template in each operation.</summary>
        [StringFormatMethod("format")]
        public static Utf8PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9> PrepareUtf8<
            T1, T2, T3, T4, T5, T6, T7, T8, T9>(string format)
        {
            using (_PRF_PrepareUtf8.Auto())
            {
                return new Utf8PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9>(format);
            }
        }

        /// <summary>Prepare string format to avoid parse template in each operation.</summary>
        [StringFormatMethod("format")]
        public static Utf8PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> PrepareUtf8<
            T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string format)
        {
            using (_PRF_PrepareUtf8.Auto())
            {
                return new Utf8PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(format);
            }
        }

        /// <summary>Prepare string format to avoid parse template in each operation.</summary>
        [StringFormatMethod("format")]
        public static Utf8PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> PrepareUtf8<
            T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string format)
        {
            using (_PRF_PrepareUtf8.Auto())
            {
                return new Utf8PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(format);
            }
        }

        /// <summary>Prepare string format to avoid parse template in each operation.</summary>
        [StringFormatMethod("format")]
        public static Utf8PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> PrepareUtf8<
            T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string format)
        {
            using (_PRF_PrepareUtf8.Auto())
            {
                return new Utf8PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(format);
            }
        }

        /// <summary>Prepare string format to avoid parse template in each operation.</summary>
        [StringFormatMethod("format")]
        public static Utf8PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> PrepareUtf8<
            T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(string format)
        {
            using (_PRF_PrepareUtf8.Auto())
            {
                return new Utf8PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(format);
            }
        }

        /// <summary>Prepare string format to avoid parse template in each operation.</summary>
        [StringFormatMethod("format")]
        public static Utf8PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>
            PrepareUtf8<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(string format)
        {
            using (_PRF_PrepareUtf8.Auto())
            {
                return new Utf8PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(
                    format
                );
            }
        }

        /// <summary>Prepare string format to avoid parse template in each operation.</summary>
        [StringFormatMethod("format")]
        public static Utf8PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>
            PrepareUtf8<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(string format)
        {
            using (_PRF_PrepareUtf8.Auto())
            {
                return new Utf8PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14,
                    T15>(format);
            }
        }

        /// <summary>Prepare string format to avoid parse template in each operation.</summary>
        [StringFormatMethod("format")]
        public static
            Utf8PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>
            PrepareUtf8<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(string format)
        {
            using (_PRF_PrepareUtf8.Auto())
            {
                return new Utf8PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15
                    , T16>(format);
            }
        }

        #region Profiling

        private static readonly ProfilerMarker _PRF_PrepareUtf8 =
            new ProfilerMarker(_PRF_PFX + nameof(PrepareUtf8));

        private static readonly ProfilerMarker _PRF_PrepareUtf16 =
            new ProfilerMarker(_PRF_PFX + nameof(PrepareUtf16));

        #endregion
    }
}
