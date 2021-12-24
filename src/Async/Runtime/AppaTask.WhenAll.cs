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
        public static AppaTask<T[]> WhenAll<T>(params AppaTask<T>[] tasks)
        {
            if (tasks.Length == 0)
            {
                return FromResult(Array.Empty<T>());
            }

            return new AppaTask<T[]>(new WhenAllPromise<T>(tasks, tasks.Length), 0);
        }

        public static AppaTask<T[]> WhenAll<T>(IEnumerable<AppaTask<T>> tasks)
        {
            using (var span = ArrayPoolUtil.Materialize(tasks))
            {
                var promise = new WhenAllPromise<T>(
                    span.Array,
                    span.Length
                ); // consumed array in constructor.
                return new AppaTask<T[]>(promise, 0);
            }
        }

        public static AppaTask WhenAll(params AppaTask[] tasks)
        {
            if (tasks.Length == 0)
            {
                return CompletedTask;
            }

            return new AppaTask(new WhenAllPromise(tasks, tasks.Length), 0);
        }

        public static AppaTask WhenAll(IEnumerable<AppaTask> tasks)
        {
            using (var span = ArrayPoolUtil.Materialize(tasks))
            {
                var promise = new WhenAllPromise(span.Array, span.Length); // consumed array in constructor.
                return new AppaTask(promise, 0);
            }
        }

        private sealed class WhenAllPromise<T> : IAppaTaskSource<T[]>
        {
            private T[] result;
            private int completeCount;

            private AppaTaskCompletionSourceCore<T[]>
                core; // don't reset(called after GetResult, will invoke TrySetException.)

            public WhenAllPromise(AppaTask<T>[] tasks, int tasksLength)
            {
                TaskTracker.TrackActiveTask(this, 3);

                completeCount = 0;

                if (tasksLength == 0)
                {
                    result = Array.Empty<T>();
                    core.TrySetResult(result);
                    return;
                }

                result = new T[tasksLength];

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
                        continue;
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
                                using (var t = (StateTuple<WhenAllPromise<T>, AppaTask<T>.Awaiter, int>)state)
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
                WhenAllPromise<T> self,
                in AppaTask<T>.Awaiter awaiter,
                int i)
            {
                try
                {
                    self.result[i] = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completeCount) == self.result.Length)
                {
                    self.core.TrySetResult(self.result);
                }
            }

            public T[] GetResult(short token)
            {
                TaskTracker.RemoveTracking(this);

                // ReSharper disable once GCSuppressFinalizeForTypeWithoutDestructor
                GC.SuppressFinalize(this);
                return core.GetResult(token);
            }

            void IAppaTaskSource.GetResult(short token)
            {
                GetResult(token);
            }

            public AppaTaskStatus GetStatus(short token)
            {
                return core.GetStatus(token);
            }

            public AppaTaskStatus UnsafeGetStatus()
            {
                return core.UnsafeGetStatus();
            }

            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                core.OnCompleted(continuation, state, token);
            }
        }

        private sealed class WhenAllPromise : IAppaTaskSource
        {
            private int completeCount;
            private int tasksLength;

            private AppaTaskCompletionSourceCore<AsyncUnit>
                core; // don't reset(called after GetResult, will invoke TrySetException.)

            public WhenAllPromise(AppaTask[] tasks, int tasksLength)
            {
                TaskTracker.TrackActiveTask(this, 3);

                this.tasksLength = tasksLength;
                completeCount = 0;

                if (tasksLength == 0)
                {
                    core.TrySetResult(AsyncUnit.Default);
                    return;
                }

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
                        continue;
                    }

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuation(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(
                            state =>
                            {
                                using (var t = (StateTuple<WhenAllPromise, Awaiter>)state)
                                {
                                    TryInvokeContinuation(t.Item1, t.Item2);
                                }
                            },
                            StateTuple.Create(this, awaiter)
                        );
                    }
                }
            }

            private static void TryInvokeContinuation(WhenAllPromise self, in Awaiter awaiter)
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

                if (Interlocked.Increment(ref self.completeCount) == self.tasksLength)
                {
                    self.core.TrySetResult(AsyncUnit.Default);
                }
            }

            public void GetResult(short token)
            {
                TaskTracker.RemoveTracking(this);
                GC.SuppressFinalize(this);
                core.GetResult(token);
            }

            public AppaTaskStatus GetStatus(short token)
            {
                return core.GetStatus(token);
            }

            public AppaTaskStatus UnsafeGetStatus()
            {
                return core.UnsafeGetStatus();
            }

            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                core.OnCompleted(continuation, state, token);
            }
        }
    }
}
