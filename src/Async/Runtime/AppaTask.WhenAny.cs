#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Collections.Generic;
using System.Threading;
using Appalachia.Utility.Async.Internal;

// ReSharper disable GCSuppressFinalizeForTypeWithoutDestructor

namespace Appalachia.Utility.Async
{
    public partial struct AppaTask
    {
        public static AppaTask<(bool hasResultLeft, T result)> WhenAny<T>(
            AppaTask<T> leftTask,
            AppaTask rightTask)
        {
            return new AppaTask<(bool, T)>(new WhenAnyLRPromise<T>(leftTask, rightTask), 0);
        }

        public static AppaTask<(int winArgumentIndex, T result)> WhenAny<T>(params AppaTask<T>[] tasks)
        {
            return new AppaTask<(int, T)>(new WhenAnyPromise<T>(tasks, tasks.Length), 0);
        }

        public static AppaTask<(int winArgumentIndex, T result)> WhenAny<T>(IEnumerable<AppaTask<T>> tasks)
        {
            using (var span = ArrayPoolUtil.Materialize(tasks))
            {
                return new AppaTask<(int, T)>(new WhenAnyPromise<T>(span.Array, span.Length), 0);
            }
        }

        /// <summary>Return value is winArgumentIndex</summary>
        public static AppaTask<int> WhenAny(params AppaTask[] tasks)
        {
            return new AppaTask<int>(new WhenAnyPromise(tasks, tasks.Length), 0);
        }

        /// <summary>Return value is winArgumentIndex</summary>
        public static AppaTask<int> WhenAny(IEnumerable<AppaTask> tasks)
        {
            using (var span = ArrayPoolUtil.Materialize(tasks))
            {
                return new AppaTask<int>(new WhenAnyPromise(span.Array, span.Length), 0);
            }
        }

        private sealed class WhenAnyLRPromise<T> : IAppaTaskSource<(bool, T)>
        {
            private int completedCount;
            private AppaTaskCompletionSourceCore<(bool, T)> core;

            public WhenAnyLRPromise(AppaTask<T> leftTask, AppaTask rightTask)
            {
                TaskTracker.TrackActiveTask(this, 3);

                {
                    AppaTask<T>.Awaiter awaiter;
                    try
                    {
                        awaiter = leftTask.GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        core.TrySetException(ex);
                        goto RIGHT;
                    }

                    if (awaiter.IsCompleted)
                    {
                        TryLeftInvokeContinuation(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(
                            state =>
                            {
                                using (var t = (StateTuple<WhenAnyLRPromise<T>, AppaTask<T>.Awaiter>)state)
                                {
                                    TryLeftInvokeContinuation(t.Item1, t.Item2);
                                }
                            },
                            StateTuple.Create(this, awaiter)
                        );
                    }
                }
                RIGHT:
                {
                    Awaiter awaiter;
                    try
                    {
                        awaiter = rightTask.GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        core.TrySetException(ex);
                        return;
                    }

                    if (awaiter.IsCompleted)
                    {
                        TryRightInvokeContinuation(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(
                            state =>
                            {
                                using (var t = (StateTuple<WhenAnyLRPromise<T>, Awaiter>)state)
                                {
                                    TryRightInvokeContinuation(t.Item1, t.Item2);
                                }
                            },
                            StateTuple.Create(this, awaiter)
                        );
                    }
                }
            }

            private static void TryLeftInvokeContinuation(
                WhenAnyLRPromise<T> self,
                in AppaTask<T>.Awaiter awaiter)
            {
                T result;
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
                    self.core.TrySetResult((true, result));
                }
            }

            private static void TryRightInvokeContinuation(WhenAnyLRPromise<T> self, in Awaiter awaiter)
            {
                try
                {
                    awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((false, default));
                }
            }

            public (bool, T) GetResult(short token)
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

        private sealed class WhenAnyPromise<T> : IAppaTaskSource<(int, T)>
        {
            private int completedCount;
            private AppaTaskCompletionSourceCore<(int, T)> core;

            public WhenAnyPromise(AppaTask<T>[] tasks, int tasksLength)
            {
                if (tasksLength == 0)
                {
                    throw new ArgumentException("The tasks argument contains no tasks.");
                }

                TaskTracker.TrackActiveTask(this, 3);

                for (var i = 0; i < tasksLength; i++)
                {
                    AppaTask<T>.Awaiter awaiter;
                    try
                    {
                        awaiter = tasks[i].GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        core.TrySetException(ex);
                        continue; // consume others.
                    }

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuation(this, awaiter, i);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(
                            state =>
                            {
                                using (var t = (StateTuple<WhenAnyPromise<T>, AppaTask<T>.Awaiter, int>)state)
                                {
                                    TryInvokeContinuation(t.Item1, t.Item2, t.Item3);
                                }
                            },
                            StateTuple.Create(this, awaiter, i)
                        );
                    }
                }
            }

            private static void TryInvokeContinuation(
                WhenAnyPromise<T> self,
                in AppaTask<T>.Awaiter awaiter,
                int i)
            {
                T result;
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
                    self.core.TrySetResult((i, result));
                }
            }

            public (int, T) GetResult(short token)
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

        private sealed class WhenAnyPromise : IAppaTaskSource<int>
        {
            private int completedCount;
            private AppaTaskCompletionSourceCore<int> core;

            public WhenAnyPromise(AppaTask[] tasks, int tasksLength)
            {
                if (tasksLength == 0)
                {
                    throw new ArgumentException("The tasks argument contains no tasks.");
                }

                TaskTracker.TrackActiveTask(this, 3);

                for (var i = 0; i < tasksLength; i++)
                {
                    Awaiter awaiter;
                    try
                    {
                        awaiter = tasks[i].GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        core.TrySetException(ex);
                        continue; // consume others.
                    }

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuation(this, awaiter, i);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(
                            state =>
                            {
                                using (var t = (StateTuple<WhenAnyPromise, Awaiter, int>)state)
                                {
                                    TryInvokeContinuation(t.Item1, t.Item2, t.Item3);
                                }
                            },
                            StateTuple.Create(this, awaiter, i)
                        );
                    }
                }
            }

            private static void TryInvokeContinuation(WhenAnyPromise self, in Awaiter awaiter, int i)
            {
                try
                {
                    awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult(i);
                }
            }

            public int GetResult(short token)
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
