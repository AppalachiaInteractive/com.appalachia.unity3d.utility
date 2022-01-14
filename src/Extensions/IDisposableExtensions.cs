using System;
using System.Runtime.CompilerServices;
using Unity.Profiling;

namespace Appalachia.Utility.Extensions
{
    public static class IDisposableExtensions
    {
        #region Constants and Static Readonly

        private static readonly ProfilerMarker _PRF_SafeDispose =
            new ProfilerMarker(_PRF_PFX + nameof(SafeDispose));

        private static readonly ConditionalWeakTable<IDisposable, ReferenceCount> RefCounts = new();

        #endregion

        /// <summary>
        ///     Extension method for IDisposable.
        ///     Decrements the refCount for the given disposable.
        /// </summary>
        /// <remarks>This method is thread-safe.</remarks>
        /// <param name="disposable">The disposable to release.</param>
        public static void Release(this IDisposable disposable)
        {
            using (_PRF_Release.Auto())
            {
                lock (RefCounts)
                {
                    var refCount = RefCounts.GetOrCreateValue(disposable);
                    if (refCount.refCount > 0)
                    {
                        refCount.refCount--;

                        if (refCount.refCount != 0)
                        {
                            return;
                        }

                        RefCounts.Remove(disposable);
                        disposable.Dispose();
                    }
                    else
                    {
                        // Retain() was never called, so assume there is only
                        // one reference, which is now calling Release()
                        disposable.Dispose();
                    }
                }
            }
        }

        /// <summary>
        ///     Extension method for IDisposable.
        ///     Increments the refCount for the given IDisposable object.
        ///     Note: newly instantiated objects don't automatically have a refCount of 1!
        ///     If you wish to use ref-counting, always call retain() whenever you want
        ///     to take ownership of an object.
        /// </summary>
        /// <remarks>This method is thread-safe.</remarks>
        /// <param name="disposable">The disposable that should be retained.</param>
        public static void Retain(this IDisposable disposable)
        {
            using (_PRF_Retain.Auto())
            {
                lock (RefCounts)
                {
                    var refCount = RefCounts.GetOrCreateValue(disposable);
                    refCount.refCount++;
                }
            }
        }

        public static void SafeDispose<T>(ref T disposable)
            where T : IDisposable
        {
            using (_PRF_SafeDispose.Auto())
            {
                try
                {
                    disposable?.Dispose();
                }
                catch (InvalidOperationException)
                {
                }
            }
        }

        public static void SafeDispose<T0, T1>(ref T0 d0, ref T1 d1)
            where T0 : IDisposable
            where T1 : IDisposable
        {
            SafeDispose(ref d0);
            SafeDispose(ref d1);
        }

        public static void SafeDispose<T0, T1, T2>(ref T0 d0, ref T1 d1, ref T2 d2)
            where T0 : IDisposable
            where T1 : IDisposable
            where T2 : IDisposable
        {
            SafeDispose(ref d0);
            SafeDispose(ref d1);
            SafeDispose(ref d2);
        }

        public static void SafeDispose<T0, T1, T2, T3>(ref T0 d0, ref T1 d1, ref T2 d2, ref T3 d3)
            where T0 : IDisposable
            where T1 : IDisposable
            where T2 : IDisposable
            where T3 : IDisposable
        {
            SafeDispose(ref d0);
            SafeDispose(ref d1);
            SafeDispose(ref d2);
            SafeDispose(ref d3);
        }

        public static void SafeDispose<T0, T1, T2, T3, T4>(
            ref T0 d0,
            ref T1 d1,
            ref T2 d2,
            ref T3 d3,
            ref T4 d4)
            where T0 : IDisposable
            where T1 : IDisposable
            where T2 : IDisposable
            where T3 : IDisposable
            where T4 : IDisposable
        {
            SafeDispose(ref d0);
            SafeDispose(ref d1);
            SafeDispose(ref d2);
            SafeDispose(ref d3);
            SafeDispose(ref d4);
        }

        public static void SafeDispose<T0, T1, T2, T3, T4, T5>(
            ref T0 d0,
            ref T1 d1,
            ref T2 d2,
            ref T3 d3,
            ref T4 d4,
            ref T5 d5)
            where T0 : IDisposable
            where T1 : IDisposable
            where T2 : IDisposable
            where T3 : IDisposable
            where T4 : IDisposable
            where T5 : IDisposable
        {
            SafeDispose(ref d0);
            SafeDispose(ref d1);
            SafeDispose(ref d2);
            SafeDispose(ref d3);
            SafeDispose(ref d4);
            SafeDispose(ref d5);
        }

        public static void SafeDispose<T0, T1, T2, T3, T4, T5, T6>(
            ref T0 d0,
            ref T1 d1,
            ref T2 d2,
            ref T3 d3,
            ref T4 d4,
            ref T5 d5,
            ref T6 d6)
            where T0 : IDisposable
            where T1 : IDisposable
            where T2 : IDisposable
            where T3 : IDisposable
            where T4 : IDisposable
            where T5 : IDisposable
            where T6 : IDisposable
        {
            SafeDispose(ref d0);
            SafeDispose(ref d1);
            SafeDispose(ref d2);
            SafeDispose(ref d3);
            SafeDispose(ref d4);
            SafeDispose(ref d5);
            SafeDispose(ref d6);
        }

        public static void SafeDispose<T0, T1, T2, T3, T4, T5, T6, T7>(
            ref T0 d0,
            ref T1 d1,
            ref T2 d2,
            ref T3 d3,
            ref T4 d4,
            ref T5 d5,
            ref T6 d6,
            ref T7 d7)
            where T0 : IDisposable
            where T1 : IDisposable
            where T2 : IDisposable
            where T3 : IDisposable
            where T4 : IDisposable
            where T5 : IDisposable
            where T6 : IDisposable
            where T7 : IDisposable
        {
            SafeDispose(ref d0);
            SafeDispose(ref d1);
            SafeDispose(ref d2);
            SafeDispose(ref d3);
            SafeDispose(ref d4);
            SafeDispose(ref d5);
            SafeDispose(ref d6);
            SafeDispose(ref d7);
        }

        public static void SafeDispose<T0, T1, T2, T3, T4, T5, T6, T7, T8>(
            ref T0 d0,
            ref T1 d1,
            ref T2 d2,
            ref T3 d3,
            ref T4 d4,
            ref T5 d5,
            ref T6 d6,
            ref T7 d7,
            ref T8 d8)
            where T0 : IDisposable
            where T1 : IDisposable
            where T2 : IDisposable
            where T3 : IDisposable
            where T4 : IDisposable
            where T5 : IDisposable
            where T6 : IDisposable
            where T7 : IDisposable
            where T8 : IDisposable
        {
            SafeDispose(ref d0);
            SafeDispose(ref d1);
            SafeDispose(ref d2);
            SafeDispose(ref d3);
            SafeDispose(ref d4);
            SafeDispose(ref d5);
            SafeDispose(ref d6);
            SafeDispose(ref d7);
            SafeDispose(ref d8);
        }

        public static void SafeDispose<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(
            ref T0 d0,
            ref T1 d1,
            ref T2 d2,
            ref T3 d3,
            ref T4 d4,
            ref T5 d5,
            ref T6 d6,
            ref T7 d7,
            ref T8 d8,
            ref T9 d9)
            where T0 : IDisposable
            where T1 : IDisposable
            where T2 : IDisposable
            where T3 : IDisposable
            where T4 : IDisposable
            where T5 : IDisposable
            where T6 : IDisposable
            where T7 : IDisposable
            where T8 : IDisposable
            where T9 : IDisposable
        {
            SafeDispose(ref d0);
            SafeDispose(ref d1);
            SafeDispose(ref d2);
            SafeDispose(ref d3);
            SafeDispose(ref d4);
            SafeDispose(ref d5);
            SafeDispose(ref d6);
            SafeDispose(ref d7);
            SafeDispose(ref d8);
            SafeDispose(ref d9);
        }

        public static void SafeDispose<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(
            ref T0 d0,
            ref T1 d1,
            ref T2 d2,
            ref T3 d3,
            ref T4 d4,
            ref T5 d5,
            ref T6 d6,
            ref T7 d7,
            ref T8 d8,
            ref T9 d9,
            ref T10 d10)
            where T0 : IDisposable
            where T1 : IDisposable
            where T2 : IDisposable
            where T3 : IDisposable
            where T4 : IDisposable
            where T5 : IDisposable
            where T6 : IDisposable
            where T7 : IDisposable
            where T8 : IDisposable
            where T9 : IDisposable
            where T10 : IDisposable
        {
            SafeDispose(ref d0);
            SafeDispose(ref d1);
            SafeDispose(ref d2);
            SafeDispose(ref d3);
            SafeDispose(ref d4);
            SafeDispose(ref d5);
            SafeDispose(ref d6);
            SafeDispose(ref d7);
            SafeDispose(ref d8);
            SafeDispose(ref d9);
            SafeDispose(ref d10);
        }

        public static void SafeDispose<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(
            ref T0 d0,
            ref T1 d1,
            ref T2 d2,
            ref T3 d3,
            ref T4 d4,
            ref T5 d5,
            ref T6 d6,
            ref T7 d7,
            ref T8 d8,
            ref T9 d9,
            ref T10 d10,
            ref T11 d11)
            where T0 : IDisposable
            where T1 : IDisposable
            where T2 : IDisposable
            where T3 : IDisposable
            where T4 : IDisposable
            where T5 : IDisposable
            where T6 : IDisposable
            where T7 : IDisposable
            where T8 : IDisposable
            where T9 : IDisposable
            where T10 : IDisposable
            where T11 : IDisposable
        {
            SafeDispose(ref d0);
            SafeDispose(ref d1);
            SafeDispose(ref d2);
            SafeDispose(ref d3);
            SafeDispose(ref d4);
            SafeDispose(ref d5);
            SafeDispose(ref d6);
            SafeDispose(ref d7);
            SafeDispose(ref d8);
            SafeDispose(ref d9);
            SafeDispose(ref d10);
            SafeDispose(ref d11);
        }

        public static void SafeDispose<T00, T01, T02, T03, T04, T05, T06, T07, T08, T09, T10, T11, T12>(
            ref T00 d00,
            ref T01 d01,
            ref T02 d02,
            ref T03 d03,
            ref T04 d04,
            ref T05 d05,
            ref T06 d06,
            ref T07 d07,
            ref T08 d08,
            ref T09 d09,
            ref T10 d10,
            ref T11 d11,
            ref T12 d12)
            where T00 : IDisposable
            where T01 : IDisposable
            where T02 : IDisposable
            where T03 : IDisposable
            where T04 : IDisposable
            where T05 : IDisposable
            where T06 : IDisposable
            where T07 : IDisposable
            where T08 : IDisposable
            where T09 : IDisposable
            where T10 : IDisposable
            where T11 : IDisposable
            where T12 : IDisposable
        {
            SafeDispose(ref d00);
            SafeDispose(ref d01);
            SafeDispose(ref d02);
            SafeDispose(ref d03);
            SafeDispose(ref d04);
            SafeDispose(ref d05);
            SafeDispose(ref d06);
            SafeDispose(ref d07);
            SafeDispose(ref d08);
            SafeDispose(ref d09);
            SafeDispose(ref d10);
            SafeDispose(ref d11);
            SafeDispose(ref d12);
        }

        public static void SafeDispose<T00, T01, T02, T03, T04, T05, T06, T07, T08, T09, T10, T11, T12, T13>(
            ref T00 d00,
            ref T01 d01,
            ref T02 d02,
            ref T03 d03,
            ref T04 d04,
            ref T05 d05,
            ref T06 d06,
            ref T07 d07,
            ref T08 d08,
            ref T09 d09,
            ref T10 d10,
            ref T11 d11,
            ref T12 d12,
            ref T13 d13)
            where T00 : IDisposable
            where T01 : IDisposable
            where T02 : IDisposable
            where T03 : IDisposable
            where T04 : IDisposable
            where T05 : IDisposable
            where T06 : IDisposable
            where T07 : IDisposable
            where T08 : IDisposable
            where T09 : IDisposable
            where T10 : IDisposable
            where T11 : IDisposable
            where T12 : IDisposable
            where T13 : IDisposable
        {
            SafeDispose(ref d00);
            SafeDispose(ref d01);
            SafeDispose(ref d02);
            SafeDispose(ref d03);
            SafeDispose(ref d04);
            SafeDispose(ref d05);
            SafeDispose(ref d06);
            SafeDispose(ref d07);
            SafeDispose(ref d08);
            SafeDispose(ref d09);
            SafeDispose(ref d10);
            SafeDispose(ref d11);
            SafeDispose(ref d12);
            SafeDispose(ref d13);
        }

        public static void
            SafeDispose<T00, T01, T02, T03, T04, T05, T06, T07, T08, T09, T10, T11, T12, T13, T14>(
                ref T00 d00,
                ref T01 d01,
                ref T02 d02,
                ref T03 d03,
                ref T04 d04,
                ref T05 d05,
                ref T06 d06,
                ref T07 d07,
                ref T08 d08,
                ref T09 d09,
                ref T10 d10,
                ref T11 d11,
                ref T12 d12,
                ref T13 d13,
                ref T14 d14)
            where T00 : IDisposable
            where T01 : IDisposable
            where T02 : IDisposable
            where T03 : IDisposable
            where T04 : IDisposable
            where T05 : IDisposable
            where T06 : IDisposable
            where T07 : IDisposable
            where T08 : IDisposable
            where T09 : IDisposable
            where T10 : IDisposable
            where T11 : IDisposable
            where T12 : IDisposable
            where T13 : IDisposable
            where T14 : IDisposable
        {
            SafeDispose(ref d00);
            SafeDispose(ref d01);
            SafeDispose(ref d02);
            SafeDispose(ref d03);
            SafeDispose(ref d04);
            SafeDispose(ref d05);
            SafeDispose(ref d06);
            SafeDispose(ref d07);
            SafeDispose(ref d08);
            SafeDispose(ref d09);
            SafeDispose(ref d10);
            SafeDispose(ref d11);
            SafeDispose(ref d12);
            SafeDispose(ref d13);
            SafeDispose(ref d14);
        }

        public static void
            SafeDispose<T00, T01, T02, T03, T04, T05, T06, T07, T08, T09, T10, T11, T12, T13, T14, T15>(
                ref T00 d00,
                ref T01 d01,
                ref T02 d02,
                ref T03 d03,
                ref T04 d04,
                ref T05 d05,
                ref T06 d06,
                ref T07 d07,
                ref T08 d08,
                ref T09 d09,
                ref T10 d10,
                ref T11 d11,
                ref T12 d12,
                ref T13 d13,
                ref T14 d14,
                ref T15 d15)
            where T00 : IDisposable
            where T01 : IDisposable
            where T02 : IDisposable
            where T03 : IDisposable
            where T04 : IDisposable
            where T05 : IDisposable
            where T06 : IDisposable
            where T07 : IDisposable
            where T08 : IDisposable
            where T09 : IDisposable
            where T10 : IDisposable
            where T11 : IDisposable
            where T12 : IDisposable
            where T13 : IDisposable
            where T14 : IDisposable
            where T15 : IDisposable
        {
            SafeDispose(ref d00);
            SafeDispose(ref d01);
            SafeDispose(ref d02);
            SafeDispose(ref d03);
            SafeDispose(ref d04);
            SafeDispose(ref d05);
            SafeDispose(ref d06);
            SafeDispose(ref d07);
            SafeDispose(ref d08);
            SafeDispose(ref d09);
            SafeDispose(ref d10);
            SafeDispose(ref d11);
            SafeDispose(ref d12);
            SafeDispose(ref d13);
            SafeDispose(ref d14);
            SafeDispose(ref d15);
        }

        public static void
            SafeDispose<T00, T01, T02, T03, T04, T05, T06, T07, T08, T09, T10, T11, T12, T13, T14, T15, T16>(
                ref T00 d00,
                ref T01 d01,
                ref T02 d02,
                ref T03 d03,
                ref T04 d04,
                ref T05 d05,
                ref T06 d06,
                ref T07 d07,
                ref T08 d08,
                ref T09 d09,
                ref T10 d10,
                ref T11 d11,
                ref T12 d12,
                ref T13 d13,
                ref T14 d14,
                ref T15 d15,
                ref T16 d16)
            where T00 : IDisposable
            where T01 : IDisposable
            where T02 : IDisposable
            where T03 : IDisposable
            where T04 : IDisposable
            where T05 : IDisposable
            where T06 : IDisposable
            where T07 : IDisposable
            where T08 : IDisposable
            where T09 : IDisposable
            where T10 : IDisposable
            where T11 : IDisposable
            where T12 : IDisposable
            where T13 : IDisposable
            where T14 : IDisposable
            where T15 : IDisposable
            where T16 : IDisposable
        {
            SafeDispose(ref d00);
            SafeDispose(ref d01);
            SafeDispose(ref d02);
            SafeDispose(ref d03);
            SafeDispose(ref d04);
            SafeDispose(ref d05);
            SafeDispose(ref d06);
            SafeDispose(ref d07);
            SafeDispose(ref d08);
            SafeDispose(ref d09);
            SafeDispose(ref d10);
            SafeDispose(ref d11);
            SafeDispose(ref d12);
            SafeDispose(ref d13);
            SafeDispose(ref d14);
            SafeDispose(ref d15);
            SafeDispose(ref d16);
        }

        public static void
            SafeDispose<T00, T01, T02, T03, T04, T05, T06, T07, T08, T09, T10, T11, T12, T13, T14, T15, T16,
                        T17>(
                ref T00 d00,
                ref T01 d01,
                ref T02 d02,
                ref T03 d03,
                ref T04 d04,
                ref T05 d05,
                ref T06 d06,
                ref T07 d07,
                ref T08 d08,
                ref T09 d09,
                ref T10 d10,
                ref T11 d11,
                ref T12 d12,
                ref T13 d13,
                ref T14 d14,
                ref T15 d15,
                ref T16 d16,
                ref T17 d17)
            where T00 : IDisposable
            where T01 : IDisposable
            where T02 : IDisposable
            where T03 : IDisposable
            where T04 : IDisposable
            where T05 : IDisposable
            where T06 : IDisposable
            where T07 : IDisposable
            where T08 : IDisposable
            where T09 : IDisposable
            where T10 : IDisposable
            where T11 : IDisposable
            where T12 : IDisposable
            where T13 : IDisposable
            where T14 : IDisposable
            where T15 : IDisposable
            where T16 : IDisposable
            where T17 : IDisposable
        {
            SafeDispose(ref d00);
            SafeDispose(ref d01);
            SafeDispose(ref d02);
            SafeDispose(ref d03);
            SafeDispose(ref d04);
            SafeDispose(ref d05);
            SafeDispose(ref d06);
            SafeDispose(ref d07);
            SafeDispose(ref d08);
            SafeDispose(ref d09);
            SafeDispose(ref d10);
            SafeDispose(ref d11);
            SafeDispose(ref d12);
            SafeDispose(ref d13);
            SafeDispose(ref d14);
            SafeDispose(ref d15);
            SafeDispose(ref d16);
            SafeDispose(ref d17);
        }

        public static void
            SafeDispose<T00, T01, T02, T03, T04, T05, T06, T07, T08, T09, T10, T11, T12, T13, T14, T15, T16,
                        T17, T18>(
                ref T00 d00,
                ref T01 d01,
                ref T02 d02,
                ref T03 d03,
                ref T04 d04,
                ref T05 d05,
                ref T06 d06,
                ref T07 d07,
                ref T08 d08,
                ref T09 d09,
                ref T10 d10,
                ref T11 d11,
                ref T12 d12,
                ref T13 d13,
                ref T14 d14,
                ref T15 d15,
                ref T16 d16,
                ref T17 d17,
                ref T18 d18)
            where T00 : IDisposable
            where T01 : IDisposable
            where T02 : IDisposable
            where T03 : IDisposable
            where T04 : IDisposable
            where T05 : IDisposable
            where T06 : IDisposable
            where T07 : IDisposable
            where T08 : IDisposable
            where T09 : IDisposable
            where T10 : IDisposable
            where T11 : IDisposable
            where T12 : IDisposable
            where T13 : IDisposable
            where T14 : IDisposable
            where T15 : IDisposable
            where T16 : IDisposable
            where T17 : IDisposable
            where T18 : IDisposable
        {
            SafeDispose(ref d00);
            SafeDispose(ref d01);
            SafeDispose(ref d02);
            SafeDispose(ref d03);
            SafeDispose(ref d04);
            SafeDispose(ref d05);
            SafeDispose(ref d06);
            SafeDispose(ref d07);
            SafeDispose(ref d08);
            SafeDispose(ref d09);
            SafeDispose(ref d10);
            SafeDispose(ref d11);
            SafeDispose(ref d12);
            SafeDispose(ref d13);
            SafeDispose(ref d14);
            SafeDispose(ref d15);
            SafeDispose(ref d16);
            SafeDispose(ref d17);
            SafeDispose(ref d18);
        }

        public static void
            SafeDispose<T00, T01, T02, T03, T04, T05, T06, T07, T08, T09, T10, T11, T12, T13, T14, T15, T16,
                        T17, T18, T19>(
                ref T00 d00,
                ref T01 d01,
                ref T02 d02,
                ref T03 d03,
                ref T04 d04,
                ref T05 d05,
                ref T06 d06,
                ref T07 d07,
                ref T08 d08,
                ref T09 d09,
                ref T10 d10,
                ref T11 d11,
                ref T12 d12,
                ref T13 d13,
                ref T14 d14,
                ref T15 d15,
                ref T16 d16,
                ref T17 d17,
                ref T18 d18,
                ref T19 d19)
            where T00 : IDisposable
            where T01 : IDisposable
            where T02 : IDisposable
            where T03 : IDisposable
            where T04 : IDisposable
            where T05 : IDisposable
            where T06 : IDisposable
            where T07 : IDisposable
            where T08 : IDisposable
            where T09 : IDisposable
            where T10 : IDisposable
            where T11 : IDisposable
            where T12 : IDisposable
            where T13 : IDisposable
            where T14 : IDisposable
            where T15 : IDisposable
            where T16 : IDisposable
            where T17 : IDisposable
            where T18 : IDisposable
            where T19 : IDisposable
        {
            SafeDispose(ref d00);
            SafeDispose(ref d01);
            SafeDispose(ref d02);
            SafeDispose(ref d03);
            SafeDispose(ref d04);
            SafeDispose(ref d05);
            SafeDispose(ref d06);
            SafeDispose(ref d07);
            SafeDispose(ref d08);
            SafeDispose(ref d09);
            SafeDispose(ref d10);
            SafeDispose(ref d11);
            SafeDispose(ref d12);
            SafeDispose(ref d13);
            SafeDispose(ref d14);
            SafeDispose(ref d15);
            SafeDispose(ref d16);
            SafeDispose(ref d17);
            SafeDispose(ref d18);
            SafeDispose(ref d19);
        }

        public static void
            SafeDispose<T00, T01, T02, T03, T04, T05, T06, T07, T08, T09, T10, T11, T12, T13, T14, T15, T16,
                        T17, T18, T19, T20>(
                ref T00 d00,
                ref T01 d01,
                ref T02 d02,
                ref T03 d03,
                ref T04 d04,
                ref T05 d05,
                ref T06 d06,
                ref T07 d07,
                ref T08 d08,
                ref T09 d09,
                ref T10 d10,
                ref T11 d11,
                ref T12 d12,
                ref T13 d13,
                ref T14 d14,
                ref T15 d15,
                ref T16 d16,
                ref T17 d17,
                ref T18 d18,
                ref T19 d19,
                ref T20 d20)
            where T00 : IDisposable
            where T01 : IDisposable
            where T02 : IDisposable
            where T03 : IDisposable
            where T04 : IDisposable
            where T05 : IDisposable
            where T06 : IDisposable
            where T07 : IDisposable
            where T08 : IDisposable
            where T09 : IDisposable
            where T10 : IDisposable
            where T11 : IDisposable
            where T12 : IDisposable
            where T13 : IDisposable
            where T14 : IDisposable
            where T15 : IDisposable
            where T16 : IDisposable
            where T17 : IDisposable
            where T18 : IDisposable
            where T19 : IDisposable
            where T20 : IDisposable
        {
            SafeDispose(ref d00);
            SafeDispose(ref d01);
            SafeDispose(ref d02);
            SafeDispose(ref d03);
            SafeDispose(ref d04);
            SafeDispose(ref d05);
            SafeDispose(ref d06);
            SafeDispose(ref d07);
            SafeDispose(ref d08);
            SafeDispose(ref d09);
            SafeDispose(ref d10);
            SafeDispose(ref d11);
            SafeDispose(ref d12);
            SafeDispose(ref d13);
            SafeDispose(ref d14);
            SafeDispose(ref d15);
            SafeDispose(ref d16);
            SafeDispose(ref d17);
            SafeDispose(ref d18);
            SafeDispose(ref d19);
            SafeDispose(ref d20);
        }

        public static void
            SafeDispose<T00, T01, T02, T03, T04, T05, T06, T07, T08, T09, T10, T11, T12, T13, T14, T15, T16,
                        T17, T18, T19, T20, T21>(
                ref T00 d00,
                ref T01 d01,
                ref T02 d02,
                ref T03 d03,
                ref T04 d04,
                ref T05 d05,
                ref T06 d06,
                ref T07 d07,
                ref T08 d08,
                ref T09 d09,
                ref T10 d10,
                ref T11 d11,
                ref T12 d12,
                ref T13 d13,
                ref T14 d14,
                ref T15 d15,
                ref T16 d16,
                ref T17 d17,
                ref T18 d18,
                ref T19 d19,
                ref T20 d20,
                ref T21 d21)
            where T00 : IDisposable
            where T01 : IDisposable
            where T02 : IDisposable
            where T03 : IDisposable
            where T04 : IDisposable
            where T05 : IDisposable
            where T06 : IDisposable
            where T07 : IDisposable
            where T08 : IDisposable
            where T09 : IDisposable
            where T10 : IDisposable
            where T11 : IDisposable
            where T12 : IDisposable
            where T13 : IDisposable
            where T14 : IDisposable
            where T15 : IDisposable
            where T16 : IDisposable
            where T17 : IDisposable
            where T18 : IDisposable
            where T19 : IDisposable
            where T20 : IDisposable
            where T21 : IDisposable
        {
            SafeDispose(ref d00);
            SafeDispose(ref d01);
            SafeDispose(ref d02);
            SafeDispose(ref d03);
            SafeDispose(ref d04);
            SafeDispose(ref d05);
            SafeDispose(ref d06);
            SafeDispose(ref d07);
            SafeDispose(ref d08);
            SafeDispose(ref d09);
            SafeDispose(ref d10);
            SafeDispose(ref d11);
            SafeDispose(ref d12);
            SafeDispose(ref d13);
            SafeDispose(ref d14);
            SafeDispose(ref d15);
            SafeDispose(ref d16);
            SafeDispose(ref d17);
            SafeDispose(ref d18);
            SafeDispose(ref d19);
            SafeDispose(ref d20);
            SafeDispose(ref d21);
        }

        public static void
            SafeDispose<T00, T01, T02, T03, T04, T05, T06, T07, T08, T09, T10, T11, T12, T13, T14, T15, T16,
                        T17, T18, T19, T20, T21, T22>(
                ref T00 d00,
                ref T01 d01,
                ref T02 d02,
                ref T03 d03,
                ref T04 d04,
                ref T05 d05,
                ref T06 d06,
                ref T07 d07,
                ref T08 d08,
                ref T09 d09,
                ref T10 d10,
                ref T11 d11,
                ref T12 d12,
                ref T13 d13,
                ref T14 d14,
                ref T15 d15,
                ref T16 d16,
                ref T17 d17,
                ref T18 d18,
                ref T19 d19,
                ref T20 d20,
                ref T21 d21,
                ref T22 d22)
            where T00 : IDisposable
            where T01 : IDisposable
            where T02 : IDisposable
            where T03 : IDisposable
            where T04 : IDisposable
            where T05 : IDisposable
            where T06 : IDisposable
            where T07 : IDisposable
            where T08 : IDisposable
            where T09 : IDisposable
            where T10 : IDisposable
            where T11 : IDisposable
            where T12 : IDisposable
            where T13 : IDisposable
            where T14 : IDisposable
            where T15 : IDisposable
            where T16 : IDisposable
            where T17 : IDisposable
            where T18 : IDisposable
            where T19 : IDisposable
            where T20 : IDisposable
            where T21 : IDisposable
            where T22 : IDisposable
        {
            SafeDispose(ref d00);
            SafeDispose(ref d01);
            SafeDispose(ref d02);
            SafeDispose(ref d03);
            SafeDispose(ref d04);
            SafeDispose(ref d05);
            SafeDispose(ref d06);
            SafeDispose(ref d07);
            SafeDispose(ref d08);
            SafeDispose(ref d09);
            SafeDispose(ref d10);
            SafeDispose(ref d11);
            SafeDispose(ref d12);
            SafeDispose(ref d13);
            SafeDispose(ref d14);
            SafeDispose(ref d15);
            SafeDispose(ref d16);
            SafeDispose(ref d17);
            SafeDispose(ref d18);
            SafeDispose(ref d19);
            SafeDispose(ref d20);
            SafeDispose(ref d21);
            SafeDispose(ref d22);
        }

        public static void SafeDispose<
            T00, T01, T02, T03, T04, T05, T06, T07, T08, T09, T10, T11, T12, T13, T14, T15, T16, T17, T18,
            T19, T20, T21, T22, T23>(
            ref T00 d00,
            ref T01 d01,
            ref T02 d02,
            ref T03 d03,
            ref T04 d04,
            ref T05 d05,
            ref T06 d06,
            ref T07 d07,
            ref T08 d08,
            ref T09 d09,
            ref T10 d10,
            ref T11 d11,
            ref T12 d12,
            ref T13 d13,
            ref T14 d14,
            ref T15 d15,
            ref T16 d16,
            ref T17 d17,
            ref T18 d18,
            ref T19 d19,
            ref T20 d20,
            ref T21 d21,
            ref T22 d22,
            ref T23 d23)
            where T00 : IDisposable
            where T01 : IDisposable
            where T02 : IDisposable
            where T03 : IDisposable
            where T04 : IDisposable
            where T05 : IDisposable
            where T06 : IDisposable
            where T07 : IDisposable
            where T08 : IDisposable
            where T09 : IDisposable
            where T10 : IDisposable
            where T11 : IDisposable
            where T12 : IDisposable
            where T13 : IDisposable
            where T14 : IDisposable
            where T15 : IDisposable
            where T16 : IDisposable
            where T17 : IDisposable
            where T18 : IDisposable
            where T19 : IDisposable
            where T20 : IDisposable
            where T21 : IDisposable
            where T22 : IDisposable
            where T23 : IDisposable
        {
            SafeDispose(ref d00);
            SafeDispose(ref d01);
            SafeDispose(ref d02);
            SafeDispose(ref d03);
            SafeDispose(ref d04);
            SafeDispose(ref d05);
            SafeDispose(ref d06);
            SafeDispose(ref d07);
            SafeDispose(ref d08);
            SafeDispose(ref d09);
            SafeDispose(ref d10);
            SafeDispose(ref d11);
            SafeDispose(ref d12);
            SafeDispose(ref d13);
            SafeDispose(ref d14);
            SafeDispose(ref d15);
            SafeDispose(ref d16);
            SafeDispose(ref d17);
            SafeDispose(ref d18);
            SafeDispose(ref d19);
            SafeDispose(ref d20);
            SafeDispose(ref d21);
            SafeDispose(ref d22);
            SafeDispose(ref d23);
        }

        public static void SafeDispose<
            T00, T01, T02, T03, T04, T05, T06, T07, T08, T09, T10, T11, T12, T13, T14, T15, T16, T17, T18,
            T19, T20, T21, T22, T23, T24>(
            ref T00 d00,
            ref T01 d01,
            ref T02 d02,
            ref T03 d03,
            ref T04 d04,
            ref T05 d05,
            ref T06 d06,
            ref T07 d07,
            ref T08 d08,
            ref T09 d09,
            ref T10 d10,
            ref T11 d11,
            ref T12 d12,
            ref T13 d13,
            ref T14 d14,
            ref T15 d15,
            ref T16 d16,
            ref T17 d17,
            ref T18 d18,
            ref T19 d19,
            ref T20 d20,
            ref T21 d21,
            ref T22 d22,
            ref T23 d23,
            ref T24 d24)
            where T00 : IDisposable
            where T01 : IDisposable
            where T02 : IDisposable
            where T03 : IDisposable
            where T04 : IDisposable
            where T05 : IDisposable
            where T06 : IDisposable
            where T07 : IDisposable
            where T08 : IDisposable
            where T09 : IDisposable
            where T10 : IDisposable
            where T11 : IDisposable
            where T12 : IDisposable
            where T13 : IDisposable
            where T14 : IDisposable
            where T15 : IDisposable
            where T16 : IDisposable
            where T17 : IDisposable
            where T18 : IDisposable
            where T19 : IDisposable
            where T20 : IDisposable
            where T21 : IDisposable
            where T22 : IDisposable
            where T23 : IDisposable
            where T24 : IDisposable
        {
            SafeDispose(ref d00);
            SafeDispose(ref d01);
            SafeDispose(ref d02);
            SafeDispose(ref d03);
            SafeDispose(ref d04);
            SafeDispose(ref d05);
            SafeDispose(ref d06);
            SafeDispose(ref d07);
            SafeDispose(ref d08);
            SafeDispose(ref d09);
            SafeDispose(ref d10);
            SafeDispose(ref d11);
            SafeDispose(ref d12);
            SafeDispose(ref d13);
            SafeDispose(ref d14);
            SafeDispose(ref d15);
            SafeDispose(ref d16);
            SafeDispose(ref d17);
            SafeDispose(ref d18);
            SafeDispose(ref d19);
            SafeDispose(ref d20);
            SafeDispose(ref d21);
            SafeDispose(ref d22);
            SafeDispose(ref d23);
            SafeDispose(ref d24);
        }

        public static void
            SafeDispose<T00, T01, T02, T03, T04, T05, T06, T07, T08, T09, T10, T11, T12, T13, T14, T15, T16,
                        T17, T18, T19, T20, T21, T22, T23, T24, T25>(
                ref T00 d00,
                ref T01 d01,
                ref T02 d02,
                ref T03 d03,
                ref T04 d04,
                ref T05 d05,
                ref T06 d06,
                ref T07 d07,
                ref T08 d08,
                ref T09 d09,
                ref T10 d10,
                ref T11 d11,
                ref T12 d12,
                ref T13 d13,
                ref T14 d14,
                ref T15 d15,
                ref T16 d16,
                ref T17 d17,
                ref T18 d18,
                ref T19 d19,
                ref T20 d20,
                ref T21 d21,
                ref T22 d22,
                ref T23 d23,
                ref T24 d24,
                ref T25 d25)
            where T00 : IDisposable
            where T01 : IDisposable
            where T02 : IDisposable
            where T03 : IDisposable
            where T04 : IDisposable
            where T05 : IDisposable
            where T06 : IDisposable
            where T07 : IDisposable
            where T08 : IDisposable
            where T09 : IDisposable
            where T10 : IDisposable
            where T11 : IDisposable
            where T12 : IDisposable
            where T13 : IDisposable
            where T14 : IDisposable
            where T15 : IDisposable
            where T16 : IDisposable
            where T17 : IDisposable
            where T18 : IDisposable
            where T19 : IDisposable
            where T20 : IDisposable
            where T21 : IDisposable
            where T22 : IDisposable
            where T23 : IDisposable
            where T24 : IDisposable
            where T25 : IDisposable
        {
            SafeDispose(ref d00);
            SafeDispose(ref d01);
            SafeDispose(ref d02);
            SafeDispose(ref d03);
            SafeDispose(ref d04);
            SafeDispose(ref d05);
            SafeDispose(ref d06);
            SafeDispose(ref d07);
            SafeDispose(ref d08);
            SafeDispose(ref d09);
            SafeDispose(ref d10);
            SafeDispose(ref d11);
            SafeDispose(ref d12);
            SafeDispose(ref d13);
            SafeDispose(ref d14);
            SafeDispose(ref d15);
            SafeDispose(ref d16);
            SafeDispose(ref d17);
            SafeDispose(ref d18);
            SafeDispose(ref d19);
            SafeDispose(ref d20);
            SafeDispose(ref d21);
            SafeDispose(ref d22);
            SafeDispose(ref d23);
            SafeDispose(ref d24);
            SafeDispose(ref d25);
        }

        public static void
            SafeDispose<T00, T01, T02, T03, T04, T05, T06, T07, T08, T09, T10, T11, T12, T13, T14, T15, T16,
                        T17, T18, T19, T20, T21, T22, T23, T24, T25, T26>(
                ref T00 d00,
                ref T01 d01,
                ref T02 d02,
                ref T03 d03,
                ref T04 d04,
                ref T05 d05,
                ref T06 d06,
                ref T07 d07,
                ref T08 d08,
                ref T09 d09,
                ref T10 d10,
                ref T11 d11,
                ref T12 d12,
                ref T13 d13,
                ref T14 d14,
                ref T15 d15,
                ref T16 d16,
                ref T17 d17,
                ref T18 d18,
                ref T19 d19,
                ref T20 d20,
                ref T21 d21,
                ref T22 d22,
                ref T23 d23,
                ref T24 d24,
                ref T25 d25,
                ref T26 d26)
            where T00 : IDisposable
            where T01 : IDisposable
            where T02 : IDisposable
            where T03 : IDisposable
            where T04 : IDisposable
            where T05 : IDisposable
            where T06 : IDisposable
            where T07 : IDisposable
            where T08 : IDisposable
            where T09 : IDisposable
            where T10 : IDisposable
            where T11 : IDisposable
            where T12 : IDisposable
            where T13 : IDisposable
            where T14 : IDisposable
            where T15 : IDisposable
            where T16 : IDisposable
            where T17 : IDisposable
            where T18 : IDisposable
            where T19 : IDisposable
            where T20 : IDisposable
            where T21 : IDisposable
            where T22 : IDisposable
            where T23 : IDisposable
            where T24 : IDisposable
            where T25 : IDisposable
            where T26 : IDisposable
        {
            SafeDispose(ref d00);
            SafeDispose(ref d01);
            SafeDispose(ref d02);
            SafeDispose(ref d03);
            SafeDispose(ref d04);
            SafeDispose(ref d05);
            SafeDispose(ref d06);
            SafeDispose(ref d07);
            SafeDispose(ref d08);
            SafeDispose(ref d09);
            SafeDispose(ref d10);
            SafeDispose(ref d11);
            SafeDispose(ref d12);
            SafeDispose(ref d13);
            SafeDispose(ref d14);
            SafeDispose(ref d15);
            SafeDispose(ref d16);
            SafeDispose(ref d17);
            SafeDispose(ref d18);
            SafeDispose(ref d19);
            SafeDispose(ref d20);
            SafeDispose(ref d21);
            SafeDispose(ref d22);
            SafeDispose(ref d23);
            SafeDispose(ref d24);
            SafeDispose(ref d25);
            SafeDispose(ref d26);
        }

        public static void
            SafeDispose<T00, T01, T02, T03, T04, T05, T06, T07, T08, T09, T10, T11, T12, T13, T14, T15, T16,
                        T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27>(
                ref T00 d00,
                ref T01 d01,
                ref T02 d02,
                ref T03 d03,
                ref T04 d04,
                ref T05 d05,
                ref T06 d06,
                ref T07 d07,
                ref T08 d08,
                ref T09 d09,
                ref T10 d10,
                ref T11 d11,
                ref T12 d12,
                ref T13 d13,
                ref T14 d14,
                ref T15 d15,
                ref T16 d16,
                ref T17 d17,
                ref T18 d18,
                ref T19 d19,
                ref T20 d20,
                ref T21 d21,
                ref T22 d22,
                ref T23 d23,
                ref T24 d24,
                ref T25 d25,
                ref T26 d26,
                ref T27 d27)
            where T00 : IDisposable
            where T01 : IDisposable
            where T02 : IDisposable
            where T03 : IDisposable
            where T04 : IDisposable
            where T05 : IDisposable
            where T06 : IDisposable
            where T07 : IDisposable
            where T08 : IDisposable
            where T09 : IDisposable
            where T10 : IDisposable
            where T11 : IDisposable
            where T12 : IDisposable
            where T13 : IDisposable
            where T14 : IDisposable
            where T15 : IDisposable
            where T16 : IDisposable
            where T17 : IDisposable
            where T18 : IDisposable
            where T19 : IDisposable
            where T20 : IDisposable
            where T21 : IDisposable
            where T22 : IDisposable
            where T23 : IDisposable
            where T24 : IDisposable
            where T25 : IDisposable
            where T26 : IDisposable
            where T27 : IDisposable
        {
            SafeDispose(ref d00);
            SafeDispose(ref d01);
            SafeDispose(ref d02);
            SafeDispose(ref d03);
            SafeDispose(ref d04);
            SafeDispose(ref d05);
            SafeDispose(ref d06);
            SafeDispose(ref d07);
            SafeDispose(ref d08);
            SafeDispose(ref d09);
            SafeDispose(ref d10);
            SafeDispose(ref d11);
            SafeDispose(ref d12);
            SafeDispose(ref d13);
            SafeDispose(ref d14);
            SafeDispose(ref d15);
            SafeDispose(ref d16);
            SafeDispose(ref d17);
            SafeDispose(ref d18);
            SafeDispose(ref d19);
            SafeDispose(ref d20);
            SafeDispose(ref d21);
            SafeDispose(ref d22);
            SafeDispose(ref d23);
            SafeDispose(ref d24);
            SafeDispose(ref d25);
            SafeDispose(ref d26);
            SafeDispose(ref d27);
        }

        public static void
            SafeDispose<T00, T01, T02, T03, T04, T05, T06, T07, T08, T09, T10, T11, T12, T13, T14, T15, T16,
                        T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28>(
                ref T00 d00,
                ref T01 d01,
                ref T02 d02,
                ref T03 d03,
                ref T04 d04,
                ref T05 d05,
                ref T06 d06,
                ref T07 d07,
                ref T08 d08,
                ref T09 d09,
                ref T10 d10,
                ref T11 d11,
                ref T12 d12,
                ref T13 d13,
                ref T14 d14,
                ref T15 d15,
                ref T16 d16,
                ref T17 d17,
                ref T18 d18,
                ref T19 d19,
                ref T20 d20,
                ref T21 d21,
                ref T22 d22,
                ref T23 d23,
                ref T24 d24,
                ref T25 d25,
                ref T26 d26,
                ref T27 d27,
                ref T28 d28)
            where T00 : IDisposable
            where T01 : IDisposable
            where T02 : IDisposable
            where T03 : IDisposable
            where T04 : IDisposable
            where T05 : IDisposable
            where T06 : IDisposable
            where T07 : IDisposable
            where T08 : IDisposable
            where T09 : IDisposable
            where T10 : IDisposable
            where T11 : IDisposable
            where T12 : IDisposable
            where T13 : IDisposable
            where T14 : IDisposable
            where T15 : IDisposable
            where T16 : IDisposable
            where T17 : IDisposable
            where T18 : IDisposable
            where T19 : IDisposable
            where T20 : IDisposable
            where T21 : IDisposable
            where T22 : IDisposable
            where T23 : IDisposable
            where T24 : IDisposable
            where T25 : IDisposable
            where T26 : IDisposable
            where T27 : IDisposable
            where T28 : IDisposable
        {
            SafeDispose(ref d00);
            SafeDispose(ref d01);
            SafeDispose(ref d02);
            SafeDispose(ref d03);
            SafeDispose(ref d04);
            SafeDispose(ref d05);
            SafeDispose(ref d06);
            SafeDispose(ref d07);
            SafeDispose(ref d08);
            SafeDispose(ref d09);
            SafeDispose(ref d10);
            SafeDispose(ref d11);
            SafeDispose(ref d12);
            SafeDispose(ref d13);
            SafeDispose(ref d14);
            SafeDispose(ref d15);
            SafeDispose(ref d16);
            SafeDispose(ref d17);
            SafeDispose(ref d18);
            SafeDispose(ref d19);
            SafeDispose(ref d20);
            SafeDispose(ref d21);
            SafeDispose(ref d22);
            SafeDispose(ref d23);
            SafeDispose(ref d24);
            SafeDispose(ref d25);
            SafeDispose(ref d26);
            SafeDispose(ref d27);
            SafeDispose(ref d28);
        }

        public static void
            SafeDispose<T00, T01, T02, T03, T04, T05, T06, T07, T08, T09, T10, T11, T12, T13, T14, T15, T16,
                        T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29>(
                ref T00 d00,
                ref T01 d01,
                ref T02 d02,
                ref T03 d03,
                ref T04 d04,
                ref T05 d05,
                ref T06 d06,
                ref T07 d07,
                ref T08 d08,
                ref T09 d09,
                ref T10 d10,
                ref T11 d11,
                ref T12 d12,
                ref T13 d13,
                ref T14 d14,
                ref T15 d15,
                ref T16 d16,
                ref T17 d17,
                ref T18 d18,
                ref T19 d19,
                ref T20 d20,
                ref T21 d21,
                ref T22 d22,
                ref T23 d23,
                ref T24 d24,
                ref T25 d25,
                ref T26 d26,
                ref T27 d27,
                ref T28 d28,
                ref T29 d29)
            where T00 : IDisposable
            where T01 : IDisposable
            where T02 : IDisposable
            where T03 : IDisposable
            where T04 : IDisposable
            where T05 : IDisposable
            where T06 : IDisposable
            where T07 : IDisposable
            where T08 : IDisposable
            where T09 : IDisposable
            where T10 : IDisposable
            where T11 : IDisposable
            where T12 : IDisposable
            where T13 : IDisposable
            where T14 : IDisposable
            where T15 : IDisposable
            where T16 : IDisposable
            where T17 : IDisposable
            where T18 : IDisposable
            where T19 : IDisposable
            where T20 : IDisposable
            where T21 : IDisposable
            where T22 : IDisposable
            where T23 : IDisposable
            where T24 : IDisposable
            where T25 : IDisposable
            where T26 : IDisposable
            where T27 : IDisposable
            where T28 : IDisposable
            where T29 : IDisposable
        {
            SafeDispose(ref d00);
            SafeDispose(ref d01);
            SafeDispose(ref d02);
            SafeDispose(ref d03);
            SafeDispose(ref d04);
            SafeDispose(ref d05);
            SafeDispose(ref d06);
            SafeDispose(ref d07);
            SafeDispose(ref d08);
            SafeDispose(ref d09);
            SafeDispose(ref d10);
            SafeDispose(ref d11);
            SafeDispose(ref d12);
            SafeDispose(ref d13);
            SafeDispose(ref d14);
            SafeDispose(ref d15);
            SafeDispose(ref d16);
            SafeDispose(ref d17);
            SafeDispose(ref d18);
            SafeDispose(ref d19);
            SafeDispose(ref d20);
            SafeDispose(ref d21);
            SafeDispose(ref d22);
            SafeDispose(ref d23);
            SafeDispose(ref d24);
            SafeDispose(ref d25);
            SafeDispose(ref d26);
            SafeDispose(ref d27);
            SafeDispose(ref d28);
            SafeDispose(ref d29);
        }

        #region Nested type: ReferenceCount

        #region Nested Types

        /// <summary>
        ///     Values in a ConditionalWeakTable need to be a reference type,
        ///     so box the refcount int in a class.
        /// </summary>
        private class ReferenceCount
        {
            public int refCount;

            #endregion
        }

        #endregion

        #region Profiling

        private const string _PRF_PFX = nameof(IDisposableExtensions) + ".";

        private static readonly ProfilerMarker _PRF_Release = new ProfilerMarker(_PRF_PFX + nameof(Release));

        private static readonly ProfilerMarker _PRF_Retain = new ProfilerMarker(_PRF_PFX + nameof(Retain));

        #endregion
    }
}
