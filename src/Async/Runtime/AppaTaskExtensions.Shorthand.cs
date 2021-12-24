#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using System.Collections.Generic;

namespace Appalachia.Utility.Async
{
    public static partial class AppaTaskExtensions
    {
        // shorthand of WhenAll

        public static AppaTask.Awaiter GetAwaiter(this AppaTask[] tasks)
        {
            return AppaTask.WhenAll(tasks).GetAwaiter();
        }

        public static AppaTask.Awaiter GetAwaiter(this IEnumerable<AppaTask> tasks)
        {
            return AppaTask.WhenAll(tasks).GetAwaiter();
        }

        public static AppaTask<T[]>.Awaiter GetAwaiter<T>(this AppaTask<T>[] tasks)
        {
            return AppaTask.WhenAll(tasks).GetAwaiter();
        }

        public static AppaTask<T[]>.Awaiter GetAwaiter<T>(this IEnumerable<AppaTask<T>> tasks)
        {
            return AppaTask.WhenAll(tasks).GetAwaiter();
        }

        public static AppaTask<(T1, T2)>.Awaiter GetAwaiter<T1, T2>(
            this (AppaTask<T1> task1, AppaTask<T2> task2) tasks)
        {
            return AppaTask.WhenAll(tasks.Item1, tasks.Item2).GetAwaiter();
        }

        public static AppaTask<(T1, T2, T3)>.Awaiter GetAwaiter<T1, T2, T3>(
            this (AppaTask<T1> task1, AppaTask<T2> task2, AppaTask<T3> task3) tasks)
        {
            return AppaTask.WhenAll(tasks.Item1, tasks.Item2, tasks.Item3).GetAwaiter();
        }

        public static AppaTask<(T1, T2, T3, T4)>.Awaiter GetAwaiter<T1, T2, T3, T4>(
            this (AppaTask<T1> task1, AppaTask<T2> task2, AppaTask<T3> task3, AppaTask<T4> task4) tasks)
        {
            return AppaTask.WhenAll(tasks.Item1, tasks.Item2, tasks.Item3, tasks.Item4).GetAwaiter();
        }

        public static AppaTask<(T1, T2, T3, T4, T5)>.Awaiter GetAwaiter<T1, T2, T3, T4, T5>(
            this (AppaTask<T1> task1, AppaTask<T2> task2, AppaTask<T3> task3, AppaTask<T4> task4, AppaTask<T5>
                task5) tasks)
        {
            return AppaTask.WhenAll(tasks.Item1, tasks.Item2, tasks.Item3, tasks.Item4, tasks.Item5)
                           .GetAwaiter();
        }

        public static AppaTask<(T1, T2, T3, T4, T5, T6)>.Awaiter GetAwaiter<T1, T2, T3, T4, T5, T6>(
            this (AppaTask<T1> task1, AppaTask<T2> task2, AppaTask<T3> task3, AppaTask<T4> task4, AppaTask<T5>
                task5, AppaTask<T6> task6) tasks)
        {
            return AppaTask.WhenAll(
                                tasks.Item1,
                                tasks.Item2,
                                tasks.Item3,
                                tasks.Item4,
                                tasks.Item5,
                                tasks.Item6
                            )
                           .GetAwaiter();
        }

        public static AppaTask<(T1, T2, T3, T4, T5, T6, T7)>.Awaiter GetAwaiter<T1, T2, T3, T4, T5, T6, T7>(
            this (AppaTask<T1> task1, AppaTask<T2> task2, AppaTask<T3> task3, AppaTask<T4> task4, AppaTask<T5>
                task5, AppaTask<T6> task6, AppaTask<T7> task7) tasks)
        {
            return AppaTask.WhenAll(
                                tasks.Item1,
                                tasks.Item2,
                                tasks.Item3,
                                tasks.Item4,
                                tasks.Item5,
                                tasks.Item6,
                                tasks.Item7
                            )
                           .GetAwaiter();
        }

        public static AppaTask<(T1, T2, T3, T4, T5, T6, T7, T8)>.Awaiter
            GetAwaiter<T1, T2, T3, T4, T5, T6, T7, T8>(
                this (AppaTask<T1> task1, AppaTask<T2> task2, AppaTask<T3> task3, AppaTask<T4> task4,
                    AppaTask<T5> task5, AppaTask<T6> task6, AppaTask<T7> task7, AppaTask<T8> task8) tasks)
        {
            return AppaTask.WhenAll(
                                tasks.Item1,
                                tasks.Item2,
                                tasks.Item3,
                                tasks.Item4,
                                tasks.Item5,
                                tasks.Item6,
                                tasks.Item7,
                                tasks.Item8
                            )
                           .GetAwaiter();
        }

        public static AppaTask<(T1, T2, T3, T4, T5, T6, T7, T8, T9)>.Awaiter
            GetAwaiter<T1, T2, T3, T4, T5, T6, T7, T8, T9>(
                this (AppaTask<T1> task1, AppaTask<T2> task2, AppaTask<T3> task3, AppaTask<T4> task4,
                    AppaTask<T5> task5, AppaTask<T6> task6, AppaTask<T7> task7, AppaTask<T8> task8,
                    AppaTask<T9> task9) tasks)
        {
            return AppaTask.WhenAll(
                                tasks.Item1,
                                tasks.Item2,
                                tasks.Item3,
                                tasks.Item4,
                                tasks.Item5,
                                tasks.Item6,
                                tasks.Item7,
                                tasks.Item8,
                                tasks.Item9
                            )
                           .GetAwaiter();
        }

        public static AppaTask<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)>.Awaiter
            GetAwaiter<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(
                this (AppaTask<T1> task1, AppaTask<T2> task2, AppaTask<T3> task3, AppaTask<T4> task4,
                    AppaTask<T5> task5, AppaTask<T6> task6, AppaTask<T7> task7, AppaTask<T8> task8,
                    AppaTask<T9> task9, AppaTask<T10> task10) tasks)
        {
            return AppaTask.WhenAll(
                                tasks.Item1,
                                tasks.Item2,
                                tasks.Item3,
                                tasks.Item4,
                                tasks.Item5,
                                tasks.Item6,
                                tasks.Item7,
                                tasks.Item8,
                                tasks.Item9,
                                tasks.Item10
                            )
                           .GetAwaiter();
        }

        public static AppaTask<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11)>.Awaiter
            GetAwaiter<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(
                this (AppaTask<T1> task1, AppaTask<T2> task2, AppaTask<T3> task3, AppaTask<T4> task4,
                    AppaTask<T5> task5, AppaTask<T6> task6, AppaTask<T7> task7, AppaTask<T8> task8,
                    AppaTask<T9> task9, AppaTask<T10> task10, AppaTask<T11> task11) tasks)
        {
            return AppaTask.WhenAll(
                                tasks.Item1,
                                tasks.Item2,
                                tasks.Item3,
                                tasks.Item4,
                                tasks.Item5,
                                tasks.Item6,
                                tasks.Item7,
                                tasks.Item8,
                                tasks.Item9,
                                tasks.Item10,
                                tasks.Item11
                            )
                           .GetAwaiter();
        }

        public static AppaTask<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12)>.Awaiter
            GetAwaiter<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(
                this (AppaTask<T1> task1, AppaTask<T2> task2, AppaTask<T3> task3, AppaTask<T4> task4,
                    AppaTask<T5> task5, AppaTask<T6> task6, AppaTask<T7> task7, AppaTask<T8> task8,
                    AppaTask<T9> task9, AppaTask<T10> task10, AppaTask<T11> task11, AppaTask<T12> task12)
                    tasks)
        {
            return AppaTask.WhenAll(
                                tasks.Item1,
                                tasks.Item2,
                                tasks.Item3,
                                tasks.Item4,
                                tasks.Item5,
                                tasks.Item6,
                                tasks.Item7,
                                tasks.Item8,
                                tasks.Item9,
                                tasks.Item10,
                                tasks.Item11,
                                tasks.Item12
                            )
                           .GetAwaiter();
        }

        public static AppaTask<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13)>.Awaiter
            GetAwaiter<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(
                this (AppaTask<T1> task1, AppaTask<T2> task2, AppaTask<T3> task3, AppaTask<T4> task4,
                    AppaTask<T5> task5, AppaTask<T6> task6, AppaTask<T7> task7, AppaTask<T8> task8,
                    AppaTask<T9> task9, AppaTask<T10> task10, AppaTask<T11> task11, AppaTask<T12> task12,
                    AppaTask<T13> task13) tasks)
        {
            return AppaTask.WhenAll(
                                tasks.Item1,
                                tasks.Item2,
                                tasks.Item3,
                                tasks.Item4,
                                tasks.Item5,
                                tasks.Item6,
                                tasks.Item7,
                                tasks.Item8,
                                tasks.Item9,
                                tasks.Item10,
                                tasks.Item11,
                                tasks.Item12,
                                tasks.Item13
                            )
                           .GetAwaiter();
        }

        public static AppaTask<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)>.Awaiter
            GetAwaiter<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(
                this (AppaTask<T1> task1, AppaTask<T2> task2, AppaTask<T3> task3, AppaTask<T4> task4,
                    AppaTask<T5> task5, AppaTask<T6> task6, AppaTask<T7> task7, AppaTask<T8> task8,
                    AppaTask<T9> task9, AppaTask<T10> task10, AppaTask<T11> task11, AppaTask<T12> task12,
                    AppaTask<T13> task13, AppaTask<T14> task14) tasks)
        {
            return AppaTask.WhenAll(
                                tasks.Item1,
                                tasks.Item2,
                                tasks.Item3,
                                tasks.Item4,
                                tasks.Item5,
                                tasks.Item6,
                                tasks.Item7,
                                tasks.Item8,
                                tasks.Item9,
                                tasks.Item10,
                                tasks.Item11,
                                tasks.Item12,
                                tasks.Item13,
                                tasks.Item14
                            )
                           .GetAwaiter();
        }

        public static AppaTask<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15)>.Awaiter
            GetAwaiter<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
                this (AppaTask<T1> task1, AppaTask<T2> task2, AppaTask<T3> task3, AppaTask<T4> task4,
                    AppaTask<T5> task5, AppaTask<T6> task6, AppaTask<T7> task7, AppaTask<T8> task8,
                    AppaTask<T9> task9, AppaTask<T10> task10, AppaTask<T11> task11, AppaTask<T12> task12,
                    AppaTask<T13> task13, AppaTask<T14> task14, AppaTask<T15> task15) tasks)
        {
            return AppaTask.WhenAll(
                                tasks.Item1,
                                tasks.Item2,
                                tasks.Item3,
                                tasks.Item4,
                                tasks.Item5,
                                tasks.Item6,
                                tasks.Item7,
                                tasks.Item8,
                                tasks.Item9,
                                tasks.Item10,
                                tasks.Item11,
                                tasks.Item12,
                                tasks.Item13,
                                tasks.Item14,
                                tasks.Item15
                            )
                           .GetAwaiter();
        }

        public static AppaTask.Awaiter GetAwaiter(this (AppaTask task1, AppaTask task2) tasks)
        {
            return AppaTask.WhenAll(tasks.Item1, tasks.Item2).GetAwaiter();
        }

        public static AppaTask.Awaiter GetAwaiter(this (AppaTask task1, AppaTask task2, AppaTask task3) tasks)
        {
            return AppaTask.WhenAll(tasks.Item1, tasks.Item2, tasks.Item3).GetAwaiter();
        }

        public static AppaTask.Awaiter GetAwaiter(
            this (AppaTask task1, AppaTask task2, AppaTask task3, AppaTask task4) tasks)
        {
            return AppaTask.WhenAll(tasks.Item1, tasks.Item2, tasks.Item3, tasks.Item4).GetAwaiter();
        }

        public static AppaTask.Awaiter GetAwaiter(
            this (AppaTask task1, AppaTask task2, AppaTask task3, AppaTask task4, AppaTask task5) tasks)
        {
            return AppaTask.WhenAll(tasks.Item1, tasks.Item2, tasks.Item3, tasks.Item4, tasks.Item5)
                           .GetAwaiter();
        }

        public static AppaTask.Awaiter GetAwaiter(
            this (AppaTask task1, AppaTask task2, AppaTask task3, AppaTask task4, AppaTask task5, AppaTask
                task6) tasks)
        {
            return AppaTask.WhenAll(
                                tasks.Item1,
                                tasks.Item2,
                                tasks.Item3,
                                tasks.Item4,
                                tasks.Item5,
                                tasks.Item6
                            )
                           .GetAwaiter();
        }

        public static AppaTask.Awaiter GetAwaiter(
            this (AppaTask task1, AppaTask task2, AppaTask task3, AppaTask task4, AppaTask task5, AppaTask
                task6, AppaTask task7) tasks)
        {
            return AppaTask.WhenAll(
                                tasks.Item1,
                                tasks.Item2,
                                tasks.Item3,
                                tasks.Item4,
                                tasks.Item5,
                                tasks.Item6,
                                tasks.Item7
                            )
                           .GetAwaiter();
        }

        public static AppaTask.Awaiter GetAwaiter(
            this (AppaTask task1, AppaTask task2, AppaTask task3, AppaTask task4, AppaTask task5, AppaTask
                task6, AppaTask task7, AppaTask task8) tasks)
        {
            return AppaTask.WhenAll(
                                tasks.Item1,
                                tasks.Item2,
                                tasks.Item3,
                                tasks.Item4,
                                tasks.Item5,
                                tasks.Item6,
                                tasks.Item7,
                                tasks.Item8
                            )
                           .GetAwaiter();
        }

        public static AppaTask.Awaiter GetAwaiter(
            this (AppaTask task1, AppaTask task2, AppaTask task3, AppaTask task4, AppaTask task5, AppaTask
                task6, AppaTask task7, AppaTask task8, AppaTask task9) tasks)
        {
            return AppaTask.WhenAll(
                                tasks.Item1,
                                tasks.Item2,
                                tasks.Item3,
                                tasks.Item4,
                                tasks.Item5,
                                tasks.Item6,
                                tasks.Item7,
                                tasks.Item8,
                                tasks.Item9
                            )
                           .GetAwaiter();
        }

        public static AppaTask.Awaiter GetAwaiter(
            this (AppaTask task1, AppaTask task2, AppaTask task3, AppaTask task4, AppaTask task5, AppaTask
                task6, AppaTask task7, AppaTask task8, AppaTask task9, AppaTask task10) tasks)
        {
            return AppaTask.WhenAll(
                                tasks.Item1,
                                tasks.Item2,
                                tasks.Item3,
                                tasks.Item4,
                                tasks.Item5,
                                tasks.Item6,
                                tasks.Item7,
                                tasks.Item8,
                                tasks.Item9,
                                tasks.Item10
                            )
                           .GetAwaiter();
        }

        public static AppaTask.Awaiter GetAwaiter(
            this (AppaTask task1, AppaTask task2, AppaTask task3, AppaTask task4, AppaTask task5, AppaTask
                task6, AppaTask task7, AppaTask task8, AppaTask task9, AppaTask task10, AppaTask task11)
                tasks)
        {
            return AppaTask.WhenAll(
                                tasks.Item1,
                                tasks.Item2,
                                tasks.Item3,
                                tasks.Item4,
                                tasks.Item5,
                                tasks.Item6,
                                tasks.Item7,
                                tasks.Item8,
                                tasks.Item9,
                                tasks.Item10,
                                tasks.Item11
                            )
                           .GetAwaiter();
        }

        public static AppaTask.Awaiter GetAwaiter(
            this (AppaTask task1, AppaTask task2, AppaTask task3, AppaTask task4, AppaTask task5, AppaTask
                task6, AppaTask task7, AppaTask task8, AppaTask task9, AppaTask task10, AppaTask task11,
                AppaTask task12) tasks)
        {
            return AppaTask.WhenAll(
                                tasks.Item1,
                                tasks.Item2,
                                tasks.Item3,
                                tasks.Item4,
                                tasks.Item5,
                                tasks.Item6,
                                tasks.Item7,
                                tasks.Item8,
                                tasks.Item9,
                                tasks.Item10,
                                tasks.Item11,
                                tasks.Item12
                            )
                           .GetAwaiter();
        }

        public static AppaTask.Awaiter GetAwaiter(
            this (AppaTask task1, AppaTask task2, AppaTask task3, AppaTask task4, AppaTask task5, AppaTask
                task6, AppaTask task7, AppaTask task8, AppaTask task9, AppaTask task10, AppaTask task11,
                AppaTask task12, AppaTask task13) tasks)
        {
            return AppaTask.WhenAll(
                                tasks.Item1,
                                tasks.Item2,
                                tasks.Item3,
                                tasks.Item4,
                                tasks.Item5,
                                tasks.Item6,
                                tasks.Item7,
                                tasks.Item8,
                                tasks.Item9,
                                tasks.Item10,
                                tasks.Item11,
                                tasks.Item12,
                                tasks.Item13
                            )
                           .GetAwaiter();
        }

        public static AppaTask.Awaiter GetAwaiter(
            this (AppaTask task1, AppaTask task2, AppaTask task3, AppaTask task4, AppaTask task5, AppaTask
                task6, AppaTask task7, AppaTask task8, AppaTask task9, AppaTask task10, AppaTask task11,
                AppaTask task12, AppaTask task13, AppaTask task14) tasks)
        {
            return AppaTask.WhenAll(
                                tasks.Item1,
                                tasks.Item2,
                                tasks.Item3,
                                tasks.Item4,
                                tasks.Item5,
                                tasks.Item6,
                                tasks.Item7,
                                tasks.Item8,
                                tasks.Item9,
                                tasks.Item10,
                                tasks.Item11,
                                tasks.Item12,
                                tasks.Item13,
                                tasks.Item14
                            )
                           .GetAwaiter();
        }

        public static AppaTask.Awaiter GetAwaiter(
            this (AppaTask task1, AppaTask task2, AppaTask task3, AppaTask task4, AppaTask task5, AppaTask
                task6, AppaTask task7, AppaTask task8, AppaTask task9, AppaTask task10, AppaTask task11,
                AppaTask task12, AppaTask task13, AppaTask task14, AppaTask task15) tasks)
        {
            return AppaTask.WhenAll(
                                tasks.Item1,
                                tasks.Item2,
                                tasks.Item3,
                                tasks.Item4,
                                tasks.Item5,
                                tasks.Item6,
                                tasks.Item7,
                                tasks.Item8,
                                tasks.Item9,
                                tasks.Item10,
                                tasks.Item11,
                                tasks.Item12,
                                tasks.Item13,
                                tasks.Item14,
                                tasks.Item15
                            )
                           .GetAwaiter();
        }
    }
}
