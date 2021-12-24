#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Threading;

namespace Appalachia.Utility.Async
{
    public class AsyncLazy
    {
        private static Action<object> continuation = SetCompletionSource;

        private Func<AppaTask> taskFactory;
        private AppaTaskCompletionSource completionSource;
        private AppaTask.Awaiter awaiter;

        private object syncLock;
        private bool initialized;

        public AsyncLazy(Func<AppaTask> taskFactory)
        {
            this.taskFactory = taskFactory;
            completionSource = new AppaTaskCompletionSource();
            syncLock = new object();
            initialized = false;
        }

        internal AsyncLazy(AppaTask task)
        {
            taskFactory = null;
            completionSource = new AppaTaskCompletionSource();
            syncLock = null;
            initialized = true;

            var awaiter = task.GetAwaiter();
            if (awaiter.IsCompleted)
            {
                SetCompletionSource(awaiter);
            }
            else
            {
                this.awaiter = awaiter;
                awaiter.SourceOnCompleted(continuation, this);
            }
        }

        public AppaTask Task
        {
            get
            {
                EnsureInitialized();
                return completionSource.Task;
            }
        }

        public AppaTask.Awaiter GetAwaiter()
        {
            return Task.GetAwaiter();
        }

        private void EnsureInitialized()
        {
            if (Volatile.Read(ref initialized))
            {
                return;
            }

            EnsureInitializedCore();
        }

        private void EnsureInitializedCore()
        {
            lock (syncLock)
            {
                if (!Volatile.Read(ref initialized))
                {
                    var f = Interlocked.Exchange(ref taskFactory, null);
                    if (f != null)
                    {
                        var task = f();
                        var awaiter = task.GetAwaiter();
                        if (awaiter.IsCompleted)
                        {
                            SetCompletionSource(awaiter);
                        }
                        else
                        {
                            this.awaiter = awaiter;
                            awaiter.SourceOnCompleted(continuation, this);
                        }

                        Volatile.Write(ref initialized, true);
                    }
                }
            }
        }

        private void SetCompletionSource(in AppaTask.Awaiter awaiter)
        {
            try
            {
                awaiter.GetResult();
                completionSource.TrySetResult();
            }
            catch (Exception ex)
            {
                completionSource.TrySetException(ex);
            }
        }

        private static void SetCompletionSource(object state)
        {
            var self = (AsyncLazy)state;
            try
            {
                self.awaiter.GetResult();
                self.completionSource.TrySetResult();
            }
            catch (Exception ex)
            {
                self.completionSource.TrySetException(ex);
            }
            finally
            {
                self.awaiter = default;
            }
        }
    }

    public class AsyncLazy<T>
    {
        private static Action<object> continuation = SetCompletionSource;

        private Func<AppaTask<T>> taskFactory;
        private AppaTaskCompletionSource<T> completionSource;
        private AppaTask<T>.Awaiter awaiter;

        private object syncLock;
        private bool initialized;

        public AsyncLazy(Func<AppaTask<T>> taskFactory)
        {
            this.taskFactory = taskFactory;
            completionSource = new AppaTaskCompletionSource<T>();
            syncLock = new object();
            initialized = false;
        }

        internal AsyncLazy(AppaTask<T> task)
        {
            taskFactory = null;
            completionSource = new AppaTaskCompletionSource<T>();
            syncLock = null;
            initialized = true;

            var awaiter = task.GetAwaiter();
            if (awaiter.IsCompleted)
            {
                SetCompletionSource(awaiter);
            }
            else
            {
                this.awaiter = awaiter;
                awaiter.SourceOnCompleted(continuation, this);
            }
        }

        public AppaTask<T> Task
        {
            get
            {
                EnsureInitialized();
                return completionSource.Task;
            }
        }

        public AppaTask<T>.Awaiter GetAwaiter()
        {
            return Task.GetAwaiter();
        }

        private void EnsureInitialized()
        {
            if (Volatile.Read(ref initialized))
            {
                return;
            }

            EnsureInitializedCore();
        }

        private void EnsureInitializedCore()
        {
            lock (syncLock)
            {
                if (!Volatile.Read(ref initialized))
                {
                    var f = Interlocked.Exchange(ref taskFactory, null);
                    if (f != null)
                    {
                        var task = f();
                        var awaiter = task.GetAwaiter();
                        if (awaiter.IsCompleted)
                        {
                            SetCompletionSource(awaiter);
                        }
                        else
                        {
                            this.awaiter = awaiter;
                            awaiter.SourceOnCompleted(continuation, this);
                        }

                        Volatile.Write(ref initialized, true);
                    }
                }
            }
        }

        private void SetCompletionSource(in AppaTask<T>.Awaiter awaiter)
        {
            try
            {
                var result = awaiter.GetResult();
                completionSource.TrySetResult(result);
            }
            catch (Exception ex)
            {
                completionSource.TrySetException(ex);
            }
        }

        private static void SetCompletionSource(object state)
        {
            var self = (AsyncLazy<T>)state;
            try
            {
                var result = self.awaiter.GetResult();
                self.completionSource.TrySetResult(result);
            }
            catch (Exception ex)
            {
                self.completionSource.TrySetException(ex);
            }
            finally
            {
                self.awaiter = default;
            }
        }
    }
}
