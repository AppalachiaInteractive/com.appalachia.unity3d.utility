#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using System;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Threading;
using Appalachia.Utility.Async.Internal;

namespace Appalachia.Utility.Async
{
    public partial struct AppaTask
    {
        public static AppaTask<(int winArgumentIndex, T1 result1, T2 result2)> WhenAny<T1, T2>(AppaTask<T1> task1, AppaTask<T2> task2)
        {
            return new AppaTask<(int winArgumentIndex, T1 result1, T2 result2)>(new WhenAnyPromise<T1, T2>(task1, task2), 0);
        }

        sealed class WhenAnyPromise<T1, T2> : IAppaTaskSource<(int, T1 result1, T2 result2)>
        {
            int completedCount;
            AppaTaskCompletionSourceCore<(int, T1 result1, T2 result2)> core;

            public WhenAnyPromise(AppaTask<T1> task1, AppaTask<T2> task2)
            {
                TaskTracker.TrackActiveTask(this, 3);

                this.completedCount = 0;
                {
                    var awaiter = task1.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT1(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2>, AppaTask<T1>.Awaiter>)state)
                            {
                                TryInvokeContinuationT1(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task2.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT2(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2>, AppaTask<T2>.Awaiter>)state)
                            {
                                TryInvokeContinuationT2(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
            }

            static void TryInvokeContinuationT1(WhenAnyPromise<T1, T2> self, in AppaTask<T1>.Awaiter awaiter)
            {
                T1 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((0, result, default));
                }
            }

            static void TryInvokeContinuationT2(WhenAnyPromise<T1, T2> self, in AppaTask<T2>.Awaiter awaiter)
            {
                T2 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((1, default, result));
                }
            }


            public (int, T1 result1, T2 result2) GetResult(short token)
            {
                TaskTracker.RemoveTracking(this);
                GC.SuppressFinalize(this);
                return core.GetResult(token);
            }

            public AppaTaskStatus GetStatus(short token)
            {
                return core.GetStatus(token);
            }

            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                core.OnCompleted(continuation, state, token);
            }

            public AppaTaskStatus UnsafeGetStatus()
            {
                return core.UnsafeGetStatus();
            }

            void IAppaTaskSource.GetResult(short token)
            {
                GetResult(token);
            }
        }

        public static AppaTask<(int winArgumentIndex, T1 result1, T2 result2, T3 result3)> WhenAny<T1, T2, T3>(AppaTask<T1> task1, AppaTask<T2> task2, AppaTask<T3> task3)
        {
            return new AppaTask<(int winArgumentIndex, T1 result1, T2 result2, T3 result3)>(new WhenAnyPromise<T1, T2, T3>(task1, task2, task3), 0);
        }

        sealed class WhenAnyPromise<T1, T2, T3> : IAppaTaskSource<(int, T1 result1, T2 result2, T3 result3)>
        {
            int completedCount;
            AppaTaskCompletionSourceCore<(int, T1 result1, T2 result2, T3 result3)> core;

            public WhenAnyPromise(AppaTask<T1> task1, AppaTask<T2> task2, AppaTask<T3> task3)
            {
                TaskTracker.TrackActiveTask(this, 3);

                this.completedCount = 0;
                {
                    var awaiter = task1.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT1(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3>, AppaTask<T1>.Awaiter>)state)
                            {
                                TryInvokeContinuationT1(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task2.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT2(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3>, AppaTask<T2>.Awaiter>)state)
                            {
                                TryInvokeContinuationT2(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task3.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT3(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3>, AppaTask<T3>.Awaiter>)state)
                            {
                                TryInvokeContinuationT3(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
            }

            static void TryInvokeContinuationT1(WhenAnyPromise<T1, T2, T3> self, in AppaTask<T1>.Awaiter awaiter)
            {
                T1 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((0, result, default, default));
                }
            }

            static void TryInvokeContinuationT2(WhenAnyPromise<T1, T2, T3> self, in AppaTask<T2>.Awaiter awaiter)
            {
                T2 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((1, default, result, default));
                }
            }

            static void TryInvokeContinuationT3(WhenAnyPromise<T1, T2, T3> self, in AppaTask<T3>.Awaiter awaiter)
            {
                T3 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((2, default, default, result));
                }
            }


            public (int, T1 result1, T2 result2, T3 result3) GetResult(short token)
            {
                TaskTracker.RemoveTracking(this);
                GC.SuppressFinalize(this);
                return core.GetResult(token);
            }

            public AppaTaskStatus GetStatus(short token)
            {
                return core.GetStatus(token);
            }

            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                core.OnCompleted(continuation, state, token);
            }

            public AppaTaskStatus UnsafeGetStatus()
            {
                return core.UnsafeGetStatus();
            }

            void IAppaTaskSource.GetResult(short token)
            {
                GetResult(token);
            }
        }

        public static AppaTask<(int winArgumentIndex, T1 result1, T2 result2, T3 result3, T4 result4)> WhenAny<T1, T2, T3, T4>(AppaTask<T1> task1, AppaTask<T2> task2, AppaTask<T3> task3, AppaTask<T4> task4)
        {
            return new AppaTask<(int winArgumentIndex, T1 result1, T2 result2, T3 result3, T4 result4)>(new WhenAnyPromise<T1, T2, T3, T4>(task1, task2, task3, task4), 0);
        }

        sealed class WhenAnyPromise<T1, T2, T3, T4> : IAppaTaskSource<(int, T1 result1, T2 result2, T3 result3, T4 result4)>
        {
            int completedCount;
            AppaTaskCompletionSourceCore<(int, T1 result1, T2 result2, T3 result3, T4 result4)> core;

            public WhenAnyPromise(AppaTask<T1> task1, AppaTask<T2> task2, AppaTask<T3> task3, AppaTask<T4> task4)
            {
                TaskTracker.TrackActiveTask(this, 3);

                this.completedCount = 0;
                {
                    var awaiter = task1.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT1(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4>, AppaTask<T1>.Awaiter>)state)
                            {
                                TryInvokeContinuationT1(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task2.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT2(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4>, AppaTask<T2>.Awaiter>)state)
                            {
                                TryInvokeContinuationT2(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task3.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT3(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4>, AppaTask<T3>.Awaiter>)state)
                            {
                                TryInvokeContinuationT3(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task4.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT4(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4>, AppaTask<T4>.Awaiter>)state)
                            {
                                TryInvokeContinuationT4(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
            }

            static void TryInvokeContinuationT1(WhenAnyPromise<T1, T2, T3, T4> self, in AppaTask<T1>.Awaiter awaiter)
            {
                T1 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((0, result, default, default, default));
                }
            }

            static void TryInvokeContinuationT2(WhenAnyPromise<T1, T2, T3, T4> self, in AppaTask<T2>.Awaiter awaiter)
            {
                T2 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((1, default, result, default, default));
                }
            }

            static void TryInvokeContinuationT3(WhenAnyPromise<T1, T2, T3, T4> self, in AppaTask<T3>.Awaiter awaiter)
            {
                T3 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((2, default, default, result, default));
                }
            }

            static void TryInvokeContinuationT4(WhenAnyPromise<T1, T2, T3, T4> self, in AppaTask<T4>.Awaiter awaiter)
            {
                T4 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((3, default, default, default, result));
                }
            }


            public (int, T1 result1, T2 result2, T3 result3, T4 result4) GetResult(short token)
            {
                TaskTracker.RemoveTracking(this);
                GC.SuppressFinalize(this);
                return core.GetResult(token);
            }

            public AppaTaskStatus GetStatus(short token)
            {
                return core.GetStatus(token);
            }

            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                core.OnCompleted(continuation, state, token);
            }

            public AppaTaskStatus UnsafeGetStatus()
            {
                return core.UnsafeGetStatus();
            }

            void IAppaTaskSource.GetResult(short token)
            {
                GetResult(token);
            }
        }

        public static AppaTask<(int winArgumentIndex, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5)> WhenAny<T1, T2, T3, T4, T5>(AppaTask<T1> task1, AppaTask<T2> task2, AppaTask<T3> task3, AppaTask<T4> task4, AppaTask<T5> task5)
        {
            return new AppaTask<(int winArgumentIndex, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5)>(new WhenAnyPromise<T1, T2, T3, T4, T5>(task1, task2, task3, task4, task5), 0);
        }

        sealed class WhenAnyPromise<T1, T2, T3, T4, T5> : IAppaTaskSource<(int, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5)>
        {
            int completedCount;
            AppaTaskCompletionSourceCore<(int, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5)> core;

            public WhenAnyPromise(AppaTask<T1> task1, AppaTask<T2> task2, AppaTask<T3> task3, AppaTask<T4> task4, AppaTask<T5> task5)
            {
                TaskTracker.TrackActiveTask(this, 3);

                this.completedCount = 0;
                {
                    var awaiter = task1.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT1(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5>, AppaTask<T1>.Awaiter>)state)
                            {
                                TryInvokeContinuationT1(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task2.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT2(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5>, AppaTask<T2>.Awaiter>)state)
                            {
                                TryInvokeContinuationT2(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task3.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT3(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5>, AppaTask<T3>.Awaiter>)state)
                            {
                                TryInvokeContinuationT3(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task4.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT4(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5>, AppaTask<T4>.Awaiter>)state)
                            {
                                TryInvokeContinuationT4(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task5.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT5(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5>, AppaTask<T5>.Awaiter>)state)
                            {
                                TryInvokeContinuationT5(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
            }

            static void TryInvokeContinuationT1(WhenAnyPromise<T1, T2, T3, T4, T5> self, in AppaTask<T1>.Awaiter awaiter)
            {
                T1 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((0, result, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT2(WhenAnyPromise<T1, T2, T3, T4, T5> self, in AppaTask<T2>.Awaiter awaiter)
            {
                T2 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((1, default, result, default, default, default));
                }
            }

            static void TryInvokeContinuationT3(WhenAnyPromise<T1, T2, T3, T4, T5> self, in AppaTask<T3>.Awaiter awaiter)
            {
                T3 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((2, default, default, result, default, default));
                }
            }

            static void TryInvokeContinuationT4(WhenAnyPromise<T1, T2, T3, T4, T5> self, in AppaTask<T4>.Awaiter awaiter)
            {
                T4 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((3, default, default, default, result, default));
                }
            }

            static void TryInvokeContinuationT5(WhenAnyPromise<T1, T2, T3, T4, T5> self, in AppaTask<T5>.Awaiter awaiter)
            {
                T5 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((4, default, default, default, default, result));
                }
            }


            public (int, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5) GetResult(short token)
            {
                TaskTracker.RemoveTracking(this);
                GC.SuppressFinalize(this);
                return core.GetResult(token);
            }

            public AppaTaskStatus GetStatus(short token)
            {
                return core.GetStatus(token);
            }

            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                core.OnCompleted(continuation, state, token);
            }

            public AppaTaskStatus UnsafeGetStatus()
            {
                return core.UnsafeGetStatus();
            }

            void IAppaTaskSource.GetResult(short token)
            {
                GetResult(token);
            }
        }

        public static AppaTask<(int winArgumentIndex, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6)> WhenAny<T1, T2, T3, T4, T5, T6>(AppaTask<T1> task1, AppaTask<T2> task2, AppaTask<T3> task3, AppaTask<T4> task4, AppaTask<T5> task5, AppaTask<T6> task6)
        {
            return new AppaTask<(int winArgumentIndex, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6)>(new WhenAnyPromise<T1, T2, T3, T4, T5, T6>(task1, task2, task3, task4, task5, task6), 0);
        }

        sealed class WhenAnyPromise<T1, T2, T3, T4, T5, T6> : IAppaTaskSource<(int, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6)>
        {
            int completedCount;
            AppaTaskCompletionSourceCore<(int, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6)> core;

            public WhenAnyPromise(AppaTask<T1> task1, AppaTask<T2> task2, AppaTask<T3> task3, AppaTask<T4> task4, AppaTask<T5> task5, AppaTask<T6> task6)
            {
                TaskTracker.TrackActiveTask(this, 3);

                this.completedCount = 0;
                {
                    var awaiter = task1.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT1(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6>, AppaTask<T1>.Awaiter>)state)
                            {
                                TryInvokeContinuationT1(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task2.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT2(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6>, AppaTask<T2>.Awaiter>)state)
                            {
                                TryInvokeContinuationT2(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task3.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT3(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6>, AppaTask<T3>.Awaiter>)state)
                            {
                                TryInvokeContinuationT3(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task4.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT4(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6>, AppaTask<T4>.Awaiter>)state)
                            {
                                TryInvokeContinuationT4(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task5.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT5(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6>, AppaTask<T5>.Awaiter>)state)
                            {
                                TryInvokeContinuationT5(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task6.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT6(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6>, AppaTask<T6>.Awaiter>)state)
                            {
                                TryInvokeContinuationT6(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
            }

            static void TryInvokeContinuationT1(WhenAnyPromise<T1, T2, T3, T4, T5, T6> self, in AppaTask<T1>.Awaiter awaiter)
            {
                T1 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((0, result, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT2(WhenAnyPromise<T1, T2, T3, T4, T5, T6> self, in AppaTask<T2>.Awaiter awaiter)
            {
                T2 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((1, default, result, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT3(WhenAnyPromise<T1, T2, T3, T4, T5, T6> self, in AppaTask<T3>.Awaiter awaiter)
            {
                T3 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((2, default, default, result, default, default, default));
                }
            }

            static void TryInvokeContinuationT4(WhenAnyPromise<T1, T2, T3, T4, T5, T6> self, in AppaTask<T4>.Awaiter awaiter)
            {
                T4 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((3, default, default, default, result, default, default));
                }
            }

            static void TryInvokeContinuationT5(WhenAnyPromise<T1, T2, T3, T4, T5, T6> self, in AppaTask<T5>.Awaiter awaiter)
            {
                T5 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((4, default, default, default, default, result, default));
                }
            }

            static void TryInvokeContinuationT6(WhenAnyPromise<T1, T2, T3, T4, T5, T6> self, in AppaTask<T6>.Awaiter awaiter)
            {
                T6 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((5, default, default, default, default, default, result));
                }
            }


            public (int, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6) GetResult(short token)
            {
                TaskTracker.RemoveTracking(this);
                GC.SuppressFinalize(this);
                return core.GetResult(token);
            }

            public AppaTaskStatus GetStatus(short token)
            {
                return core.GetStatus(token);
            }

            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                core.OnCompleted(continuation, state, token);
            }

            public AppaTaskStatus UnsafeGetStatus()
            {
                return core.UnsafeGetStatus();
            }

            void IAppaTaskSource.GetResult(short token)
            {
                GetResult(token);
            }
        }

        public static AppaTask<(int winArgumentIndex, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7)> WhenAny<T1, T2, T3, T4, T5, T6, T7>(AppaTask<T1> task1, AppaTask<T2> task2, AppaTask<T3> task3, AppaTask<T4> task4, AppaTask<T5> task5, AppaTask<T6> task6, AppaTask<T7> task7)
        {
            return new AppaTask<(int winArgumentIndex, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7)>(new WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>(task1, task2, task3, task4, task5, task6, task7), 0);
        }

        sealed class WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7> : IAppaTaskSource<(int, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7)>
        {
            int completedCount;
            AppaTaskCompletionSourceCore<(int, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7)> core;

            public WhenAnyPromise(AppaTask<T1> task1, AppaTask<T2> task2, AppaTask<T3> task3, AppaTask<T4> task4, AppaTask<T5> task5, AppaTask<T6> task6, AppaTask<T7> task7)
            {
                TaskTracker.TrackActiveTask(this, 3);

                this.completedCount = 0;
                {
                    var awaiter = task1.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT1(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>, AppaTask<T1>.Awaiter>)state)
                            {
                                TryInvokeContinuationT1(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task2.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT2(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>, AppaTask<T2>.Awaiter>)state)
                            {
                                TryInvokeContinuationT2(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task3.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT3(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>, AppaTask<T3>.Awaiter>)state)
                            {
                                TryInvokeContinuationT3(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task4.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT4(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>, AppaTask<T4>.Awaiter>)state)
                            {
                                TryInvokeContinuationT4(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task5.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT5(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>, AppaTask<T5>.Awaiter>)state)
                            {
                                TryInvokeContinuationT5(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task6.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT6(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>, AppaTask<T6>.Awaiter>)state)
                            {
                                TryInvokeContinuationT6(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task7.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT7(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>, AppaTask<T7>.Awaiter>)state)
                            {
                                TryInvokeContinuationT7(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
            }

            static void TryInvokeContinuationT1(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7> self, in AppaTask<T1>.Awaiter awaiter)
            {
                T1 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((0, result, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT2(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7> self, in AppaTask<T2>.Awaiter awaiter)
            {
                T2 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((1, default, result, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT3(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7> self, in AppaTask<T3>.Awaiter awaiter)
            {
                T3 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((2, default, default, result, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT4(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7> self, in AppaTask<T4>.Awaiter awaiter)
            {
                T4 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((3, default, default, default, result, default, default, default));
                }
            }

            static void TryInvokeContinuationT5(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7> self, in AppaTask<T5>.Awaiter awaiter)
            {
                T5 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((4, default, default, default, default, result, default, default));
                }
            }

            static void TryInvokeContinuationT6(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7> self, in AppaTask<T6>.Awaiter awaiter)
            {
                T6 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((5, default, default, default, default, default, result, default));
                }
            }

            static void TryInvokeContinuationT7(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7> self, in AppaTask<T7>.Awaiter awaiter)
            {
                T7 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((6, default, default, default, default, default, default, result));
                }
            }


            public (int, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7) GetResult(short token)
            {
                TaskTracker.RemoveTracking(this);
                GC.SuppressFinalize(this);
                return core.GetResult(token);
            }

            public AppaTaskStatus GetStatus(short token)
            {
                return core.GetStatus(token);
            }

            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                core.OnCompleted(continuation, state, token);
            }

            public AppaTaskStatus UnsafeGetStatus()
            {
                return core.UnsafeGetStatus();
            }

            void IAppaTaskSource.GetResult(short token)
            {
                GetResult(token);
            }
        }

        public static AppaTask<(int winArgumentIndex, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8)> WhenAny<T1, T2, T3, T4, T5, T6, T7, T8>(AppaTask<T1> task1, AppaTask<T2> task2, AppaTask<T3> task3, AppaTask<T4> task4, AppaTask<T5> task5, AppaTask<T6> task6, AppaTask<T7> task7, AppaTask<T8> task8)
        {
            return new AppaTask<(int winArgumentIndex, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8)>(new WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>(task1, task2, task3, task4, task5, task6, task7, task8), 0);
        }

        sealed class WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8> : IAppaTaskSource<(int, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8)>
        {
            int completedCount;
            AppaTaskCompletionSourceCore<(int, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8)> core;

            public WhenAnyPromise(AppaTask<T1> task1, AppaTask<T2> task2, AppaTask<T3> task3, AppaTask<T4> task4, AppaTask<T5> task5, AppaTask<T6> task6, AppaTask<T7> task7, AppaTask<T8> task8)
            {
                TaskTracker.TrackActiveTask(this, 3);

                this.completedCount = 0;
                {
                    var awaiter = task1.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT1(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>, AppaTask<T1>.Awaiter>)state)
                            {
                                TryInvokeContinuationT1(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task2.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT2(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>, AppaTask<T2>.Awaiter>)state)
                            {
                                TryInvokeContinuationT2(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task3.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT3(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>, AppaTask<T3>.Awaiter>)state)
                            {
                                TryInvokeContinuationT3(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task4.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT4(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>, AppaTask<T4>.Awaiter>)state)
                            {
                                TryInvokeContinuationT4(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task5.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT5(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>, AppaTask<T5>.Awaiter>)state)
                            {
                                TryInvokeContinuationT5(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task6.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT6(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>, AppaTask<T6>.Awaiter>)state)
                            {
                                TryInvokeContinuationT6(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task7.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT7(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>, AppaTask<T7>.Awaiter>)state)
                            {
                                TryInvokeContinuationT7(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task8.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT8(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>, AppaTask<T8>.Awaiter>)state)
                            {
                                TryInvokeContinuationT8(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
            }

            static void TryInvokeContinuationT1(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8> self, in AppaTask<T1>.Awaiter awaiter)
            {
                T1 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((0, result, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT2(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8> self, in AppaTask<T2>.Awaiter awaiter)
            {
                T2 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((1, default, result, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT3(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8> self, in AppaTask<T3>.Awaiter awaiter)
            {
                T3 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((2, default, default, result, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT4(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8> self, in AppaTask<T4>.Awaiter awaiter)
            {
                T4 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((3, default, default, default, result, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT5(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8> self, in AppaTask<T5>.Awaiter awaiter)
            {
                T5 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((4, default, default, default, default, result, default, default, default));
                }
            }

            static void TryInvokeContinuationT6(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8> self, in AppaTask<T6>.Awaiter awaiter)
            {
                T6 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((5, default, default, default, default, default, result, default, default));
                }
            }

            static void TryInvokeContinuationT7(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8> self, in AppaTask<T7>.Awaiter awaiter)
            {
                T7 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((6, default, default, default, default, default, default, result, default));
                }
            }

            static void TryInvokeContinuationT8(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8> self, in AppaTask<T8>.Awaiter awaiter)
            {
                T8 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((7, default, default, default, default, default, default, default, result));
                }
            }


            public (int, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8) GetResult(short token)
            {
                TaskTracker.RemoveTracking(this);
                GC.SuppressFinalize(this);
                return core.GetResult(token);
            }

            public AppaTaskStatus GetStatus(short token)
            {
                return core.GetStatus(token);
            }

            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                core.OnCompleted(continuation, state, token);
            }

            public AppaTaskStatus UnsafeGetStatus()
            {
                return core.UnsafeGetStatus();
            }

            void IAppaTaskSource.GetResult(short token)
            {
                GetResult(token);
            }
        }

        public static AppaTask<(int winArgumentIndex, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8, T9 result9)> WhenAny<T1, T2, T3, T4, T5, T6, T7, T8, T9>(AppaTask<T1> task1, AppaTask<T2> task2, AppaTask<T3> task3, AppaTask<T4> task4, AppaTask<T5> task5, AppaTask<T6> task6, AppaTask<T7> task7, AppaTask<T8> task8, AppaTask<T9> task9)
        {
            return new AppaTask<(int winArgumentIndex, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8, T9 result9)>(new WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>(task1, task2, task3, task4, task5, task6, task7, task8, task9), 0);
        }

        sealed class WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9> : IAppaTaskSource<(int, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8, T9 result9)>
        {
            int completedCount;
            AppaTaskCompletionSourceCore<(int, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8, T9 result9)> core;

            public WhenAnyPromise(AppaTask<T1> task1, AppaTask<T2> task2, AppaTask<T3> task3, AppaTask<T4> task4, AppaTask<T5> task5, AppaTask<T6> task6, AppaTask<T7> task7, AppaTask<T8> task8, AppaTask<T9> task9)
            {
                TaskTracker.TrackActiveTask(this, 3);

                this.completedCount = 0;
                {
                    var awaiter = task1.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT1(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, AppaTask<T1>.Awaiter>)state)
                            {
                                TryInvokeContinuationT1(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task2.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT2(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, AppaTask<T2>.Awaiter>)state)
                            {
                                TryInvokeContinuationT2(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task3.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT3(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, AppaTask<T3>.Awaiter>)state)
                            {
                                TryInvokeContinuationT3(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task4.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT4(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, AppaTask<T4>.Awaiter>)state)
                            {
                                TryInvokeContinuationT4(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task5.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT5(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, AppaTask<T5>.Awaiter>)state)
                            {
                                TryInvokeContinuationT5(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task6.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT6(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, AppaTask<T6>.Awaiter>)state)
                            {
                                TryInvokeContinuationT6(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task7.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT7(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, AppaTask<T7>.Awaiter>)state)
                            {
                                TryInvokeContinuationT7(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task8.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT8(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, AppaTask<T8>.Awaiter>)state)
                            {
                                TryInvokeContinuationT8(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task9.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT9(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, AppaTask<T9>.Awaiter>)state)
                            {
                                TryInvokeContinuationT9(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
            }

            static void TryInvokeContinuationT1(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9> self, in AppaTask<T1>.Awaiter awaiter)
            {
                T1 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((0, result, default, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT2(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9> self, in AppaTask<T2>.Awaiter awaiter)
            {
                T2 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((1, default, result, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT3(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9> self, in AppaTask<T3>.Awaiter awaiter)
            {
                T3 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((2, default, default, result, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT4(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9> self, in AppaTask<T4>.Awaiter awaiter)
            {
                T4 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((3, default, default, default, result, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT5(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9> self, in AppaTask<T5>.Awaiter awaiter)
            {
                T5 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((4, default, default, default, default, result, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT6(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9> self, in AppaTask<T6>.Awaiter awaiter)
            {
                T6 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((5, default, default, default, default, default, result, default, default, default));
                }
            }

            static void TryInvokeContinuationT7(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9> self, in AppaTask<T7>.Awaiter awaiter)
            {
                T7 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((6, default, default, default, default, default, default, result, default, default));
                }
            }

            static void TryInvokeContinuationT8(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9> self, in AppaTask<T8>.Awaiter awaiter)
            {
                T8 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((7, default, default, default, default, default, default, default, result, default));
                }
            }

            static void TryInvokeContinuationT9(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9> self, in AppaTask<T9>.Awaiter awaiter)
            {
                T9 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((8, default, default, default, default, default, default, default, default, result));
                }
            }


            public (int, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8, T9 result9) GetResult(short token)
            {
                TaskTracker.RemoveTracking(this);
                GC.SuppressFinalize(this);
                return core.GetResult(token);
            }

            public AppaTaskStatus GetStatus(short token)
            {
                return core.GetStatus(token);
            }

            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                core.OnCompleted(continuation, state, token);
            }

            public AppaTaskStatus UnsafeGetStatus()
            {
                return core.UnsafeGetStatus();
            }

            void IAppaTaskSource.GetResult(short token)
            {
                GetResult(token);
            }
        }

        public static AppaTask<(int winArgumentIndex, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8, T9 result9, T10 result10)> WhenAny<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(AppaTask<T1> task1, AppaTask<T2> task2, AppaTask<T3> task3, AppaTask<T4> task4, AppaTask<T5> task5, AppaTask<T6> task6, AppaTask<T7> task7, AppaTask<T8> task8, AppaTask<T9> task9, AppaTask<T10> task10)
        {
            return new AppaTask<(int winArgumentIndex, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8, T9 result9, T10 result10)>(new WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(task1, task2, task3, task4, task5, task6, task7, task8, task9, task10), 0);
        }

        sealed class WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> : IAppaTaskSource<(int, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8, T9 result9, T10 result10)>
        {
            int completedCount;
            AppaTaskCompletionSourceCore<(int, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8, T9 result9, T10 result10)> core;

            public WhenAnyPromise(AppaTask<T1> task1, AppaTask<T2> task2, AppaTask<T3> task3, AppaTask<T4> task4, AppaTask<T5> task5, AppaTask<T6> task6, AppaTask<T7> task7, AppaTask<T8> task8, AppaTask<T9> task9, AppaTask<T10> task10)
            {
                TaskTracker.TrackActiveTask(this, 3);

                this.completedCount = 0;
                {
                    var awaiter = task1.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT1(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, AppaTask<T1>.Awaiter>)state)
                            {
                                TryInvokeContinuationT1(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task2.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT2(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, AppaTask<T2>.Awaiter>)state)
                            {
                                TryInvokeContinuationT2(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task3.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT3(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, AppaTask<T3>.Awaiter>)state)
                            {
                                TryInvokeContinuationT3(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task4.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT4(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, AppaTask<T4>.Awaiter>)state)
                            {
                                TryInvokeContinuationT4(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task5.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT5(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, AppaTask<T5>.Awaiter>)state)
                            {
                                TryInvokeContinuationT5(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task6.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT6(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, AppaTask<T6>.Awaiter>)state)
                            {
                                TryInvokeContinuationT6(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task7.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT7(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, AppaTask<T7>.Awaiter>)state)
                            {
                                TryInvokeContinuationT7(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task8.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT8(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, AppaTask<T8>.Awaiter>)state)
                            {
                                TryInvokeContinuationT8(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task9.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT9(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, AppaTask<T9>.Awaiter>)state)
                            {
                                TryInvokeContinuationT9(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task10.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT10(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, AppaTask<T10>.Awaiter>)state)
                            {
                                TryInvokeContinuationT10(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
            }

            static void TryInvokeContinuationT1(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> self, in AppaTask<T1>.Awaiter awaiter)
            {
                T1 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((0, result, default, default, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT2(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> self, in AppaTask<T2>.Awaiter awaiter)
            {
                T2 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((1, default, result, default, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT3(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> self, in AppaTask<T3>.Awaiter awaiter)
            {
                T3 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((2, default, default, result, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT4(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> self, in AppaTask<T4>.Awaiter awaiter)
            {
                T4 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((3, default, default, default, result, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT5(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> self, in AppaTask<T5>.Awaiter awaiter)
            {
                T5 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((4, default, default, default, default, result, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT6(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> self, in AppaTask<T6>.Awaiter awaiter)
            {
                T6 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((5, default, default, default, default, default, result, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT7(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> self, in AppaTask<T7>.Awaiter awaiter)
            {
                T7 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((6, default, default, default, default, default, default, result, default, default, default));
                }
            }

            static void TryInvokeContinuationT8(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> self, in AppaTask<T8>.Awaiter awaiter)
            {
                T8 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((7, default, default, default, default, default, default, default, result, default, default));
                }
            }

            static void TryInvokeContinuationT9(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> self, in AppaTask<T9>.Awaiter awaiter)
            {
                T9 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((8, default, default, default, default, default, default, default, default, result, default));
                }
            }

            static void TryInvokeContinuationT10(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> self, in AppaTask<T10>.Awaiter awaiter)
            {
                T10 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((9, default, default, default, default, default, default, default, default, default, result));
                }
            }


            public (int, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8, T9 result9, T10 result10) GetResult(short token)
            {
                TaskTracker.RemoveTracking(this);
                GC.SuppressFinalize(this);
                return core.GetResult(token);
            }

            public AppaTaskStatus GetStatus(short token)
            {
                return core.GetStatus(token);
            }

            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                core.OnCompleted(continuation, state, token);
            }

            public AppaTaskStatus UnsafeGetStatus()
            {
                return core.UnsafeGetStatus();
            }

            void IAppaTaskSource.GetResult(short token)
            {
                GetResult(token);
            }
        }

        public static AppaTask<(int winArgumentIndex, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8, T9 result9, T10 result10, T11 result11)> WhenAny<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(AppaTask<T1> task1, AppaTask<T2> task2, AppaTask<T3> task3, AppaTask<T4> task4, AppaTask<T5> task5, AppaTask<T6> task6, AppaTask<T7> task7, AppaTask<T8> task8, AppaTask<T9> task9, AppaTask<T10> task10, AppaTask<T11> task11)
        {
            return new AppaTask<(int winArgumentIndex, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8, T9 result9, T10 result10, T11 result11)>(new WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(task1, task2, task3, task4, task5, task6, task7, task8, task9, task10, task11), 0);
        }

        sealed class WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> : IAppaTaskSource<(int, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8, T9 result9, T10 result10, T11 result11)>
        {
            int completedCount;
            AppaTaskCompletionSourceCore<(int, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8, T9 result9, T10 result10, T11 result11)> core;

            public WhenAnyPromise(AppaTask<T1> task1, AppaTask<T2> task2, AppaTask<T3> task3, AppaTask<T4> task4, AppaTask<T5> task5, AppaTask<T6> task6, AppaTask<T7> task7, AppaTask<T8> task8, AppaTask<T9> task9, AppaTask<T10> task10, AppaTask<T11> task11)
            {
                TaskTracker.TrackActiveTask(this, 3);

                this.completedCount = 0;
                {
                    var awaiter = task1.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT1(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, AppaTask<T1>.Awaiter>)state)
                            {
                                TryInvokeContinuationT1(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task2.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT2(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, AppaTask<T2>.Awaiter>)state)
                            {
                                TryInvokeContinuationT2(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task3.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT3(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, AppaTask<T3>.Awaiter>)state)
                            {
                                TryInvokeContinuationT3(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task4.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT4(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, AppaTask<T4>.Awaiter>)state)
                            {
                                TryInvokeContinuationT4(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task5.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT5(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, AppaTask<T5>.Awaiter>)state)
                            {
                                TryInvokeContinuationT5(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task6.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT6(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, AppaTask<T6>.Awaiter>)state)
                            {
                                TryInvokeContinuationT6(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task7.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT7(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, AppaTask<T7>.Awaiter>)state)
                            {
                                TryInvokeContinuationT7(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task8.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT8(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, AppaTask<T8>.Awaiter>)state)
                            {
                                TryInvokeContinuationT8(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task9.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT9(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, AppaTask<T9>.Awaiter>)state)
                            {
                                TryInvokeContinuationT9(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task10.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT10(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, AppaTask<T10>.Awaiter>)state)
                            {
                                TryInvokeContinuationT10(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task11.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT11(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, AppaTask<T11>.Awaiter>)state)
                            {
                                TryInvokeContinuationT11(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
            }

            static void TryInvokeContinuationT1(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> self, in AppaTask<T1>.Awaiter awaiter)
            {
                T1 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((0, result, default, default, default, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT2(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> self, in AppaTask<T2>.Awaiter awaiter)
            {
                T2 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((1, default, result, default, default, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT3(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> self, in AppaTask<T3>.Awaiter awaiter)
            {
                T3 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((2, default, default, result, default, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT4(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> self, in AppaTask<T4>.Awaiter awaiter)
            {
                T4 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((3, default, default, default, result, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT5(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> self, in AppaTask<T5>.Awaiter awaiter)
            {
                T5 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((4, default, default, default, default, result, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT6(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> self, in AppaTask<T6>.Awaiter awaiter)
            {
                T6 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((5, default, default, default, default, default, result, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT7(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> self, in AppaTask<T7>.Awaiter awaiter)
            {
                T7 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((6, default, default, default, default, default, default, result, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT8(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> self, in AppaTask<T8>.Awaiter awaiter)
            {
                T8 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((7, default, default, default, default, default, default, default, result, default, default, default));
                }
            }

            static void TryInvokeContinuationT9(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> self, in AppaTask<T9>.Awaiter awaiter)
            {
                T9 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((8, default, default, default, default, default, default, default, default, result, default, default));
                }
            }

            static void TryInvokeContinuationT10(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> self, in AppaTask<T10>.Awaiter awaiter)
            {
                T10 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((9, default, default, default, default, default, default, default, default, default, result, default));
                }
            }

            static void TryInvokeContinuationT11(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> self, in AppaTask<T11>.Awaiter awaiter)
            {
                T11 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((10, default, default, default, default, default, default, default, default, default, default, result));
                }
            }


            public (int, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8, T9 result9, T10 result10, T11 result11) GetResult(short token)
            {
                TaskTracker.RemoveTracking(this);
                GC.SuppressFinalize(this);
                return core.GetResult(token);
            }

            public AppaTaskStatus GetStatus(short token)
            {
                return core.GetStatus(token);
            }

            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                core.OnCompleted(continuation, state, token);
            }

            public AppaTaskStatus UnsafeGetStatus()
            {
                return core.UnsafeGetStatus();
            }

            void IAppaTaskSource.GetResult(short token)
            {
                GetResult(token);
            }
        }

        public static AppaTask<(int winArgumentIndex, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8, T9 result9, T10 result10, T11 result11, T12 result12)> WhenAny<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(AppaTask<T1> task1, AppaTask<T2> task2, AppaTask<T3> task3, AppaTask<T4> task4, AppaTask<T5> task5, AppaTask<T6> task6, AppaTask<T7> task7, AppaTask<T8> task8, AppaTask<T9> task9, AppaTask<T10> task10, AppaTask<T11> task11, AppaTask<T12> task12)
        {
            return new AppaTask<(int winArgumentIndex, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8, T9 result9, T10 result10, T11 result11, T12 result12)>(new WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(task1, task2, task3, task4, task5, task6, task7, task8, task9, task10, task11, task12), 0);
        }

        sealed class WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> : IAppaTaskSource<(int, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8, T9 result9, T10 result10, T11 result11, T12 result12)>
        {
            int completedCount;
            AppaTaskCompletionSourceCore<(int, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8, T9 result9, T10 result10, T11 result11, T12 result12)> core;

            public WhenAnyPromise(AppaTask<T1> task1, AppaTask<T2> task2, AppaTask<T3> task3, AppaTask<T4> task4, AppaTask<T5> task5, AppaTask<T6> task6, AppaTask<T7> task7, AppaTask<T8> task8, AppaTask<T9> task9, AppaTask<T10> task10, AppaTask<T11> task11, AppaTask<T12> task12)
            {
                TaskTracker.TrackActiveTask(this, 3);

                this.completedCount = 0;
                {
                    var awaiter = task1.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT1(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, AppaTask<T1>.Awaiter>)state)
                            {
                                TryInvokeContinuationT1(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task2.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT2(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, AppaTask<T2>.Awaiter>)state)
                            {
                                TryInvokeContinuationT2(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task3.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT3(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, AppaTask<T3>.Awaiter>)state)
                            {
                                TryInvokeContinuationT3(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task4.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT4(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, AppaTask<T4>.Awaiter>)state)
                            {
                                TryInvokeContinuationT4(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task5.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT5(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, AppaTask<T5>.Awaiter>)state)
                            {
                                TryInvokeContinuationT5(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task6.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT6(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, AppaTask<T6>.Awaiter>)state)
                            {
                                TryInvokeContinuationT6(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task7.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT7(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, AppaTask<T7>.Awaiter>)state)
                            {
                                TryInvokeContinuationT7(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task8.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT8(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, AppaTask<T8>.Awaiter>)state)
                            {
                                TryInvokeContinuationT8(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task9.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT9(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, AppaTask<T9>.Awaiter>)state)
                            {
                                TryInvokeContinuationT9(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task10.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT10(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, AppaTask<T10>.Awaiter>)state)
                            {
                                TryInvokeContinuationT10(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task11.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT11(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, AppaTask<T11>.Awaiter>)state)
                            {
                                TryInvokeContinuationT11(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task12.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT12(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, AppaTask<T12>.Awaiter>)state)
                            {
                                TryInvokeContinuationT12(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
            }

            static void TryInvokeContinuationT1(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in AppaTask<T1>.Awaiter awaiter)
            {
                T1 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((0, result, default, default, default, default, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT2(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in AppaTask<T2>.Awaiter awaiter)
            {
                T2 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((1, default, result, default, default, default, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT3(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in AppaTask<T3>.Awaiter awaiter)
            {
                T3 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((2, default, default, result, default, default, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT4(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in AppaTask<T4>.Awaiter awaiter)
            {
                T4 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((3, default, default, default, result, default, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT5(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in AppaTask<T5>.Awaiter awaiter)
            {
                T5 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((4, default, default, default, default, result, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT6(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in AppaTask<T6>.Awaiter awaiter)
            {
                T6 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((5, default, default, default, default, default, result, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT7(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in AppaTask<T7>.Awaiter awaiter)
            {
                T7 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((6, default, default, default, default, default, default, result, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT8(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in AppaTask<T8>.Awaiter awaiter)
            {
                T8 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((7, default, default, default, default, default, default, default, result, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT9(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in AppaTask<T9>.Awaiter awaiter)
            {
                T9 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((8, default, default, default, default, default, default, default, default, result, default, default, default));
                }
            }

            static void TryInvokeContinuationT10(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in AppaTask<T10>.Awaiter awaiter)
            {
                T10 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((9, default, default, default, default, default, default, default, default, default, result, default, default));
                }
            }

            static void TryInvokeContinuationT11(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in AppaTask<T11>.Awaiter awaiter)
            {
                T11 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((10, default, default, default, default, default, default, default, default, default, default, result, default));
                }
            }

            static void TryInvokeContinuationT12(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in AppaTask<T12>.Awaiter awaiter)
            {
                T12 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((11, default, default, default, default, default, default, default, default, default, default, default, result));
                }
            }


            public (int, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8, T9 result9, T10 result10, T11 result11, T12 result12) GetResult(short token)
            {
                TaskTracker.RemoveTracking(this);
                GC.SuppressFinalize(this);
                return core.GetResult(token);
            }

            public AppaTaskStatus GetStatus(short token)
            {
                return core.GetStatus(token);
            }

            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                core.OnCompleted(continuation, state, token);
            }

            public AppaTaskStatus UnsafeGetStatus()
            {
                return core.UnsafeGetStatus();
            }

            void IAppaTaskSource.GetResult(short token)
            {
                GetResult(token);
            }
        }

        public static AppaTask<(int winArgumentIndex, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8, T9 result9, T10 result10, T11 result11, T12 result12, T13 result13)> WhenAny<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(AppaTask<T1> task1, AppaTask<T2> task2, AppaTask<T3> task3, AppaTask<T4> task4, AppaTask<T5> task5, AppaTask<T6> task6, AppaTask<T7> task7, AppaTask<T8> task8, AppaTask<T9> task9, AppaTask<T10> task10, AppaTask<T11> task11, AppaTask<T12> task12, AppaTask<T13> task13)
        {
            return new AppaTask<(int winArgumentIndex, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8, T9 result9, T10 result10, T11 result11, T12 result12, T13 result13)>(new WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(task1, task2, task3, task4, task5, task6, task7, task8, task9, task10, task11, task12, task13), 0);
        }

        sealed class WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> : IAppaTaskSource<(int, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8, T9 result9, T10 result10, T11 result11, T12 result12, T13 result13)>
        {
            int completedCount;
            AppaTaskCompletionSourceCore<(int, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8, T9 result9, T10 result10, T11 result11, T12 result12, T13 result13)> core;

            public WhenAnyPromise(AppaTask<T1> task1, AppaTask<T2> task2, AppaTask<T3> task3, AppaTask<T4> task4, AppaTask<T5> task5, AppaTask<T6> task6, AppaTask<T7> task7, AppaTask<T8> task8, AppaTask<T9> task9, AppaTask<T10> task10, AppaTask<T11> task11, AppaTask<T12> task12, AppaTask<T13> task13)
            {
                TaskTracker.TrackActiveTask(this, 3);

                this.completedCount = 0;
                {
                    var awaiter = task1.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT1(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, AppaTask<T1>.Awaiter>)state)
                            {
                                TryInvokeContinuationT1(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task2.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT2(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, AppaTask<T2>.Awaiter>)state)
                            {
                                TryInvokeContinuationT2(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task3.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT3(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, AppaTask<T3>.Awaiter>)state)
                            {
                                TryInvokeContinuationT3(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task4.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT4(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, AppaTask<T4>.Awaiter>)state)
                            {
                                TryInvokeContinuationT4(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task5.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT5(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, AppaTask<T5>.Awaiter>)state)
                            {
                                TryInvokeContinuationT5(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task6.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT6(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, AppaTask<T6>.Awaiter>)state)
                            {
                                TryInvokeContinuationT6(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task7.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT7(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, AppaTask<T7>.Awaiter>)state)
                            {
                                TryInvokeContinuationT7(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task8.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT8(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, AppaTask<T8>.Awaiter>)state)
                            {
                                TryInvokeContinuationT8(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task9.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT9(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, AppaTask<T9>.Awaiter>)state)
                            {
                                TryInvokeContinuationT9(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task10.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT10(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, AppaTask<T10>.Awaiter>)state)
                            {
                                TryInvokeContinuationT10(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task11.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT11(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, AppaTask<T11>.Awaiter>)state)
                            {
                                TryInvokeContinuationT11(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task12.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT12(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, AppaTask<T12>.Awaiter>)state)
                            {
                                TryInvokeContinuationT12(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task13.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT13(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, AppaTask<T13>.Awaiter>)state)
                            {
                                TryInvokeContinuationT13(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
            }

            static void TryInvokeContinuationT1(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in AppaTask<T1>.Awaiter awaiter)
            {
                T1 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((0, result, default, default, default, default, default, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT2(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in AppaTask<T2>.Awaiter awaiter)
            {
                T2 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((1, default, result, default, default, default, default, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT3(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in AppaTask<T3>.Awaiter awaiter)
            {
                T3 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((2, default, default, result, default, default, default, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT4(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in AppaTask<T4>.Awaiter awaiter)
            {
                T4 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((3, default, default, default, result, default, default, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT5(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in AppaTask<T5>.Awaiter awaiter)
            {
                T5 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((4, default, default, default, default, result, default, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT6(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in AppaTask<T6>.Awaiter awaiter)
            {
                T6 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((5, default, default, default, default, default, result, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT7(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in AppaTask<T7>.Awaiter awaiter)
            {
                T7 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((6, default, default, default, default, default, default, result, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT8(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in AppaTask<T8>.Awaiter awaiter)
            {
                T8 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((7, default, default, default, default, default, default, default, result, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT9(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in AppaTask<T9>.Awaiter awaiter)
            {
                T9 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((8, default, default, default, default, default, default, default, default, result, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT10(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in AppaTask<T10>.Awaiter awaiter)
            {
                T10 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((9, default, default, default, default, default, default, default, default, default, result, default, default, default));
                }
            }

            static void TryInvokeContinuationT11(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in AppaTask<T11>.Awaiter awaiter)
            {
                T11 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((10, default, default, default, default, default, default, default, default, default, default, result, default, default));
                }
            }

            static void TryInvokeContinuationT12(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in AppaTask<T12>.Awaiter awaiter)
            {
                T12 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((11, default, default, default, default, default, default, default, default, default, default, default, result, default));
                }
            }

            static void TryInvokeContinuationT13(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in AppaTask<T13>.Awaiter awaiter)
            {
                T13 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((12, default, default, default, default, default, default, default, default, default, default, default, default, result));
                }
            }


            public (int, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8, T9 result9, T10 result10, T11 result11, T12 result12, T13 result13) GetResult(short token)
            {
                TaskTracker.RemoveTracking(this);
                GC.SuppressFinalize(this);
                return core.GetResult(token);
            }

            public AppaTaskStatus GetStatus(short token)
            {
                return core.GetStatus(token);
            }

            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                core.OnCompleted(continuation, state, token);
            }

            public AppaTaskStatus UnsafeGetStatus()
            {
                return core.UnsafeGetStatus();
            }

            void IAppaTaskSource.GetResult(short token)
            {
                GetResult(token);
            }
        }

        public static AppaTask<(int winArgumentIndex, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8, T9 result9, T10 result10, T11 result11, T12 result12, T13 result13, T14 result14)> WhenAny<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(AppaTask<T1> task1, AppaTask<T2> task2, AppaTask<T3> task3, AppaTask<T4> task4, AppaTask<T5> task5, AppaTask<T6> task6, AppaTask<T7> task7, AppaTask<T8> task8, AppaTask<T9> task9, AppaTask<T10> task10, AppaTask<T11> task11, AppaTask<T12> task12, AppaTask<T13> task13, AppaTask<T14> task14)
        {
            return new AppaTask<(int winArgumentIndex, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8, T9 result9, T10 result10, T11 result11, T12 result12, T13 result13, T14 result14)>(new WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(task1, task2, task3, task4, task5, task6, task7, task8, task9, task10, task11, task12, task13, task14), 0);
        }

        sealed class WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> : IAppaTaskSource<(int, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8, T9 result9, T10 result10, T11 result11, T12 result12, T13 result13, T14 result14)>
        {
            int completedCount;
            AppaTaskCompletionSourceCore<(int, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8, T9 result9, T10 result10, T11 result11, T12 result12, T13 result13, T14 result14)> core;

            public WhenAnyPromise(AppaTask<T1> task1, AppaTask<T2> task2, AppaTask<T3> task3, AppaTask<T4> task4, AppaTask<T5> task5, AppaTask<T6> task6, AppaTask<T7> task7, AppaTask<T8> task8, AppaTask<T9> task9, AppaTask<T10> task10, AppaTask<T11> task11, AppaTask<T12> task12, AppaTask<T13> task13, AppaTask<T14> task14)
            {
                TaskTracker.TrackActiveTask(this, 3);

                this.completedCount = 0;
                {
                    var awaiter = task1.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT1(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, AppaTask<T1>.Awaiter>)state)
                            {
                                TryInvokeContinuationT1(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task2.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT2(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, AppaTask<T2>.Awaiter>)state)
                            {
                                TryInvokeContinuationT2(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task3.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT3(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, AppaTask<T3>.Awaiter>)state)
                            {
                                TryInvokeContinuationT3(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task4.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT4(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, AppaTask<T4>.Awaiter>)state)
                            {
                                TryInvokeContinuationT4(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task5.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT5(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, AppaTask<T5>.Awaiter>)state)
                            {
                                TryInvokeContinuationT5(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task6.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT6(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, AppaTask<T6>.Awaiter>)state)
                            {
                                TryInvokeContinuationT6(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task7.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT7(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, AppaTask<T7>.Awaiter>)state)
                            {
                                TryInvokeContinuationT7(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task8.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT8(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, AppaTask<T8>.Awaiter>)state)
                            {
                                TryInvokeContinuationT8(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task9.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT9(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, AppaTask<T9>.Awaiter>)state)
                            {
                                TryInvokeContinuationT9(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task10.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT10(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, AppaTask<T10>.Awaiter>)state)
                            {
                                TryInvokeContinuationT10(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task11.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT11(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, AppaTask<T11>.Awaiter>)state)
                            {
                                TryInvokeContinuationT11(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task12.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT12(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, AppaTask<T12>.Awaiter>)state)
                            {
                                TryInvokeContinuationT12(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task13.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT13(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, AppaTask<T13>.Awaiter>)state)
                            {
                                TryInvokeContinuationT13(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task14.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT14(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, AppaTask<T14>.Awaiter>)state)
                            {
                                TryInvokeContinuationT14(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
            }

            static void TryInvokeContinuationT1(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in AppaTask<T1>.Awaiter awaiter)
            {
                T1 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((0, result, default, default, default, default, default, default, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT2(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in AppaTask<T2>.Awaiter awaiter)
            {
                T2 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((1, default, result, default, default, default, default, default, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT3(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in AppaTask<T3>.Awaiter awaiter)
            {
                T3 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((2, default, default, result, default, default, default, default, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT4(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in AppaTask<T4>.Awaiter awaiter)
            {
                T4 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((3, default, default, default, result, default, default, default, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT5(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in AppaTask<T5>.Awaiter awaiter)
            {
                T5 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((4, default, default, default, default, result, default, default, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT6(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in AppaTask<T6>.Awaiter awaiter)
            {
                T6 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((5, default, default, default, default, default, result, default, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT7(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in AppaTask<T7>.Awaiter awaiter)
            {
                T7 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((6, default, default, default, default, default, default, result, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT8(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in AppaTask<T8>.Awaiter awaiter)
            {
                T8 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((7, default, default, default, default, default, default, default, result, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT9(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in AppaTask<T9>.Awaiter awaiter)
            {
                T9 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((8, default, default, default, default, default, default, default, default, result, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT10(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in AppaTask<T10>.Awaiter awaiter)
            {
                T10 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((9, default, default, default, default, default, default, default, default, default, result, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT11(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in AppaTask<T11>.Awaiter awaiter)
            {
                T11 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((10, default, default, default, default, default, default, default, default, default, default, result, default, default, default));
                }
            }

            static void TryInvokeContinuationT12(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in AppaTask<T12>.Awaiter awaiter)
            {
                T12 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((11, default, default, default, default, default, default, default, default, default, default, default, result, default, default));
                }
            }

            static void TryInvokeContinuationT13(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in AppaTask<T13>.Awaiter awaiter)
            {
                T13 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((12, default, default, default, default, default, default, default, default, default, default, default, default, result, default));
                }
            }

            static void TryInvokeContinuationT14(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in AppaTask<T14>.Awaiter awaiter)
            {
                T14 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((13, default, default, default, default, default, default, default, default, default, default, default, default, default, result));
                }
            }


            public (int, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8, T9 result9, T10 result10, T11 result11, T12 result12, T13 result13, T14 result14) GetResult(short token)
            {
                TaskTracker.RemoveTracking(this);
                GC.SuppressFinalize(this);
                return core.GetResult(token);
            }

            public AppaTaskStatus GetStatus(short token)
            {
                return core.GetStatus(token);
            }

            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                core.OnCompleted(continuation, state, token);
            }

            public AppaTaskStatus UnsafeGetStatus()
            {
                return core.UnsafeGetStatus();
            }

            void IAppaTaskSource.GetResult(short token)
            {
                GetResult(token);
            }
        }

        public static AppaTask<(int winArgumentIndex, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8, T9 result9, T10 result10, T11 result11, T12 result12, T13 result13, T14 result14, T15 result15)> WhenAny<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(AppaTask<T1> task1, AppaTask<T2> task2, AppaTask<T3> task3, AppaTask<T4> task4, AppaTask<T5> task5, AppaTask<T6> task6, AppaTask<T7> task7, AppaTask<T8> task8, AppaTask<T9> task9, AppaTask<T10> task10, AppaTask<T11> task11, AppaTask<T12> task12, AppaTask<T13> task13, AppaTask<T14> task14, AppaTask<T15> task15)
        {
            return new AppaTask<(int winArgumentIndex, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8, T9 result9, T10 result10, T11 result11, T12 result12, T13 result13, T14 result14, T15 result15)>(new WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(task1, task2, task3, task4, task5, task6, task7, task8, task9, task10, task11, task12, task13, task14, task15), 0);
        }

        sealed class WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> : IAppaTaskSource<(int, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8, T9 result9, T10 result10, T11 result11, T12 result12, T13 result13, T14 result14, T15 result15)>
        {
            int completedCount;
            AppaTaskCompletionSourceCore<(int, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8, T9 result9, T10 result10, T11 result11, T12 result12, T13 result13, T14 result14, T15 result15)> core;

            public WhenAnyPromise(AppaTask<T1> task1, AppaTask<T2> task2, AppaTask<T3> task3, AppaTask<T4> task4, AppaTask<T5> task5, AppaTask<T6> task6, AppaTask<T7> task7, AppaTask<T8> task8, AppaTask<T9> task9, AppaTask<T10> task10, AppaTask<T11> task11, AppaTask<T12> task12, AppaTask<T13> task13, AppaTask<T14> task14, AppaTask<T15> task15)
            {
                TaskTracker.TrackActiveTask(this, 3);

                this.completedCount = 0;
                {
                    var awaiter = task1.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT1(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, AppaTask<T1>.Awaiter>)state)
                            {
                                TryInvokeContinuationT1(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task2.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT2(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, AppaTask<T2>.Awaiter>)state)
                            {
                                TryInvokeContinuationT2(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task3.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT3(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, AppaTask<T3>.Awaiter>)state)
                            {
                                TryInvokeContinuationT3(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task4.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT4(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, AppaTask<T4>.Awaiter>)state)
                            {
                                TryInvokeContinuationT4(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task5.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT5(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, AppaTask<T5>.Awaiter>)state)
                            {
                                TryInvokeContinuationT5(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task6.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT6(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, AppaTask<T6>.Awaiter>)state)
                            {
                                TryInvokeContinuationT6(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task7.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT7(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, AppaTask<T7>.Awaiter>)state)
                            {
                                TryInvokeContinuationT7(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task8.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT8(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, AppaTask<T8>.Awaiter>)state)
                            {
                                TryInvokeContinuationT8(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task9.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT9(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, AppaTask<T9>.Awaiter>)state)
                            {
                                TryInvokeContinuationT9(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task10.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT10(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, AppaTask<T10>.Awaiter>)state)
                            {
                                TryInvokeContinuationT10(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task11.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT11(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, AppaTask<T11>.Awaiter>)state)
                            {
                                TryInvokeContinuationT11(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task12.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT12(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, AppaTask<T12>.Awaiter>)state)
                            {
                                TryInvokeContinuationT12(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task13.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT13(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, AppaTask<T13>.Awaiter>)state)
                            {
                                TryInvokeContinuationT13(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task14.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT14(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, AppaTask<T14>.Awaiter>)state)
                            {
                                TryInvokeContinuationT14(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task15.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT15(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, AppaTask<T15>.Awaiter>)state)
                            {
                                TryInvokeContinuationT15(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
            }

            static void TryInvokeContinuationT1(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in AppaTask<T1>.Awaiter awaiter)
            {
                T1 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((0, result, default, default, default, default, default, default, default, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT2(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in AppaTask<T2>.Awaiter awaiter)
            {
                T2 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((1, default, result, default, default, default, default, default, default, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT3(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in AppaTask<T3>.Awaiter awaiter)
            {
                T3 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((2, default, default, result, default, default, default, default, default, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT4(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in AppaTask<T4>.Awaiter awaiter)
            {
                T4 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((3, default, default, default, result, default, default, default, default, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT5(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in AppaTask<T5>.Awaiter awaiter)
            {
                T5 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((4, default, default, default, default, result, default, default, default, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT6(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in AppaTask<T6>.Awaiter awaiter)
            {
                T6 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((5, default, default, default, default, default, result, default, default, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT7(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in AppaTask<T7>.Awaiter awaiter)
            {
                T7 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((6, default, default, default, default, default, default, result, default, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT8(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in AppaTask<T8>.Awaiter awaiter)
            {
                T8 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((7, default, default, default, default, default, default, default, result, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT9(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in AppaTask<T9>.Awaiter awaiter)
            {
                T9 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((8, default, default, default, default, default, default, default, default, result, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT10(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in AppaTask<T10>.Awaiter awaiter)
            {
                T10 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((9, default, default, default, default, default, default, default, default, default, result, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT11(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in AppaTask<T11>.Awaiter awaiter)
            {
                T11 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((10, default, default, default, default, default, default, default, default, default, default, result, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT12(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in AppaTask<T12>.Awaiter awaiter)
            {
                T12 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((11, default, default, default, default, default, default, default, default, default, default, default, result, default, default, default));
                }
            }

            static void TryInvokeContinuationT13(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in AppaTask<T13>.Awaiter awaiter)
            {
                T13 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((12, default, default, default, default, default, default, default, default, default, default, default, default, result, default, default));
                }
            }

            static void TryInvokeContinuationT14(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in AppaTask<T14>.Awaiter awaiter)
            {
                T14 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((13, default, default, default, default, default, default, default, default, default, default, default, default, default, result, default));
                }
            }

            static void TryInvokeContinuationT15(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in AppaTask<T15>.Awaiter awaiter)
            {
                T15 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((14, default, default, default, default, default, default, default, default, default, default, default, default, default, default, result));
                }
            }


            public (int, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8, T9 result9, T10 result10, T11 result11, T12 result12, T13 result13, T14 result14, T15 result15) GetResult(short token)
            {
                TaskTracker.RemoveTracking(this);
                GC.SuppressFinalize(this);
                return core.GetResult(token);
            }

            public AppaTaskStatus GetStatus(short token)
            {
                return core.GetStatus(token);
            }

            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                core.OnCompleted(continuation, state, token);
            }

            public AppaTaskStatus UnsafeGetStatus()
            {
                return core.UnsafeGetStatus();
            }

            void IAppaTaskSource.GetResult(short token)
            {
                GetResult(token);
            }
        }

    }
}