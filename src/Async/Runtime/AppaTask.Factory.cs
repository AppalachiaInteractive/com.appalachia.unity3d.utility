#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Runtime.ExceptionServices;
using System.Threading;

namespace Appalachia.Utility.Async
{
    public partial struct AppaTask
    {
        private static readonly AppaTask CanceledAppaTask = new Func<AppaTask>(
            () => { return new AppaTask(new CanceledResultSource(CancellationToken.None), 0); }
        )();

        private static class CanceledAppaTaskCache<T>
        {
            public static readonly AppaTask<T> Task;

            static CanceledAppaTaskCache()
            {
                Task = new AppaTask<T>(new CanceledResultSource<T>(CancellationToken.None), 0);
            }
        }

        public static readonly AppaTask CompletedTask;

        public static AppaTask FromException(Exception ex)
        {
            if (ex is OperationCanceledException oce)
            {
                return FromCanceled(oce.CancellationToken);
            }

            return new AppaTask(new ExceptionResultSource(ex), 0);
        }

        public static AppaTask<T> FromException<T>(Exception ex)
        {
            if (ex is OperationCanceledException oce)
            {
                return FromCanceled<T>(oce.CancellationToken);
            }

            return new AppaTask<T>(new ExceptionResultSource<T>(ex), 0);
        }

        public static AppaTask<T> FromResult<T>(T value)
        {
            return new AppaTask<T>(value);
        }

        public static AppaTask FromCanceled(CancellationToken cancellationToken = default)
        {
            if (cancellationToken == CancellationToken.None)
            {
                return CanceledAppaTask;
            }

            return new AppaTask(new CanceledResultSource(cancellationToken), 0);
        }

        public static AppaTask<T> FromCanceled<T>(CancellationToken cancellationToken = default)
        {
            if (cancellationToken == CancellationToken.None)
            {
                return CanceledAppaTaskCache<T>.Task;
            }

            return new AppaTask<T>(new CanceledResultSource<T>(cancellationToken), 0);
        }

        public static AppaTask Create(Func<AppaTask> factory)
        {
            return factory();
        }

        public static AppaTask<T> Create<T>(Func<AppaTask<T>> factory)
        {
            return factory();
        }

        public static AsyncLazy Lazy(Func<AppaTask> factory)
        {
            return new AsyncLazy(factory);
        }

        public static AsyncLazy<T> Lazy<T>(Func<AppaTask<T>> factory)
        {
            return new AsyncLazy<T>(factory);
        }

        /// <summary>
        ///     helper of fire and forget void action.
        /// </summary>
        public static void Void(Func<AppaTaskVoid> asyncAction)
        {
            asyncAction().Forget();
        }

        /// <summary>
        ///     helper of fire and forget void action.
        /// </summary>
        public static void Void(
            Func<CancellationToken, AppaTaskVoid> asyncAction,
            CancellationToken cancellationToken)
        {
            asyncAction(cancellationToken).Forget();
        }

        /// <summary>
        ///     helper of fire and forget void action.
        /// </summary>
        public static void Void<T>(Func<T, AppaTaskVoid> asyncAction, T state)
        {
            asyncAction(state).Forget();
        }

        /// <summary>
        ///     helper of create add AppaTaskVoid to delegate.
        ///     For example: FooAction = AppaTask.Action(async () => { /* */ })
        /// </summary>
        public static Action Action(Func<AppaTaskVoid> asyncAction)
        {
            return () => asyncAction().Forget();
        }

        /// <summary>
        ///     helper of create add AppaTaskVoid to delegate.
        /// </summary>
        public static Action Action(
            Func<CancellationToken, AppaTaskVoid> asyncAction,
            CancellationToken cancellationToken)
        {
            return () => asyncAction(cancellationToken).Forget();
        }

#if UNITY_2018_3_OR_NEWER

        /// <summary>
        ///     Create async void(AppaTaskVoid) UnityAction.
        ///     For exampe: onClick.AddListener(AppaTask.UnityAction(async () => { /* */ } ))
        /// </summary>
        public static UnityEngine.Events.UnityAction UnityAction(Func<AppaTaskVoid> asyncAction)
        {
            return () => asyncAction().Forget();
        }

        /// <summary>
        ///     Create async void(AppaTaskVoid) UnityAction.
        ///     For exampe: onClick.AddListener(AppaTask.UnityAction(FooAsync, this.GetCancellationTokenOnDestroy()))
        /// </summary>
        public static UnityEngine.Events.UnityAction UnityAction(
            Func<CancellationToken, AppaTaskVoid> asyncAction,
            CancellationToken cancellationToken)
        {
            return () => asyncAction(cancellationToken).Forget();
        }

#endif

        /// <summary>
        ///     Defer the task creation just before call await.
        /// </summary>
        public static AppaTask Defer(Func<AppaTask> factory)
        {
            return new AppaTask(new DeferPromise(factory), 0);
        }

        /// <summary>
        ///     Defer the task creation just before call await.
        /// </summary>
        public static AppaTask<T> Defer<T>(Func<AppaTask<T>> factory)
        {
            return new AppaTask<T>(new DeferPromise<T>(factory), 0);
        }

        /// <summary>
        ///     Never complete.
        /// </summary>
        public static AppaTask Never(CancellationToken cancellationToken)
        {
            return new AppaTask<AsyncUnit>(new NeverPromise<AsyncUnit>(cancellationToken), 0);
        }

        /// <summary>
        ///     Never complete.
        /// </summary>
        public static AppaTask<T> Never<T>(CancellationToken cancellationToken)
        {
            return new AppaTask<T>(new NeverPromise<T>(cancellationToken), 0);
        }

        private sealed class ExceptionResultSource : IAppaTaskSource
        {
            private readonly ExceptionDispatchInfo exception;

            public ExceptionResultSource(Exception exception)
            {
                this.exception = ExceptionDispatchInfo.Capture(exception);
            }

            public void GetResult(short token)
            {
                exception.Throw();
            }

            public AppaTaskStatus GetStatus(short token)
            {
                return AppaTaskStatus.Faulted;
            }

            public AppaTaskStatus UnsafeGetStatus()
            {
                return AppaTaskStatus.Faulted;
            }

            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                continuation(state);
            }
        }

        private sealed class ExceptionResultSource<T> : IAppaTaskSource<T>
        {
            private readonly ExceptionDispatchInfo exception;

            public ExceptionResultSource(Exception exception)
            {
                this.exception = ExceptionDispatchInfo.Capture(exception);
            }

            public T GetResult(short token)
            {
                exception.Throw();
                return default;
            }

            void IAppaTaskSource.GetResult(short token)
            {
                exception.Throw();
            }

            public AppaTaskStatus GetStatus(short token)
            {
                return AppaTaskStatus.Faulted;
            }

            public AppaTaskStatus UnsafeGetStatus()
            {
                return AppaTaskStatus.Faulted;
            }

            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                continuation(state);
            }
        }

        private sealed class CanceledResultSource : IAppaTaskSource
        {
            private readonly CancellationToken cancellationToken;

            public CanceledResultSource(CancellationToken cancellationToken)
            {
                this.cancellationToken = cancellationToken;
            }

            public void GetResult(short token)
            {
                throw new OperationCanceledException(cancellationToken);
            }

            public AppaTaskStatus GetStatus(short token)
            {
                return AppaTaskStatus.Canceled;
            }

            public AppaTaskStatus UnsafeGetStatus()
            {
                return AppaTaskStatus.Canceled;
            }

            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                continuation(state);
            }
        }

        private sealed class CanceledResultSource<T> : IAppaTaskSource<T>
        {
            private readonly CancellationToken cancellationToken;

            public CanceledResultSource(CancellationToken cancellationToken)
            {
                this.cancellationToken = cancellationToken;
            }

            public T GetResult(short token)
            {
                throw new OperationCanceledException(cancellationToken);
            }

            void IAppaTaskSource.GetResult(short token)
            {
                throw new OperationCanceledException(cancellationToken);
            }

            public AppaTaskStatus GetStatus(short token)
            {
                return AppaTaskStatus.Canceled;
            }

            public AppaTaskStatus UnsafeGetStatus()
            {
                return AppaTaskStatus.Canceled;
            }

            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                continuation(state);
            }
        }

        private sealed class DeferPromise : IAppaTaskSource
        {
            private Func<AppaTask> factory;
            private AppaTask task;
            private Awaiter awaiter;

            public DeferPromise(Func<AppaTask> factory)
            {
                this.factory = factory;
            }

            public void GetResult(short token)
            {
                awaiter.GetResult();
            }

            public AppaTaskStatus GetStatus(short token)
            {
                var f = Interlocked.Exchange(ref factory, null);
                if (f != null)
                {
                    task = f();
                    awaiter = task.GetAwaiter();
                }

                return task.Status;
            }

            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                awaiter.SourceOnCompleted(continuation, state);
            }

            public AppaTaskStatus UnsafeGetStatus()
            {
                return task.Status;
            }
        }

        private sealed class DeferPromise<T> : IAppaTaskSource<T>
        {
            private Func<AppaTask<T>> factory;
            private AppaTask<T> task;
            private AppaTask<T>.Awaiter awaiter;

            public DeferPromise(Func<AppaTask<T>> factory)
            {
                this.factory = factory;
            }

            public T GetResult(short token)
            {
                return awaiter.GetResult();
            }

            void IAppaTaskSource.GetResult(short token)
            {
                awaiter.GetResult();
            }

            public AppaTaskStatus GetStatus(short token)
            {
                var f = Interlocked.Exchange(ref factory, null);
                if (f != null)
                {
                    task = f();
                    awaiter = task.GetAwaiter();
                }

                return task.Status;
            }

            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                awaiter.SourceOnCompleted(continuation, state);
            }

            public AppaTaskStatus UnsafeGetStatus()
            {
                return task.Status;
            }
        }

        private sealed class NeverPromise<T> : IAppaTaskSource<T>
        {
            private static readonly Action<object> cancellationCallback = CancellationCallback;

            private CancellationToken cancellationToken;
            private AppaTaskCompletionSourceCore<T> core;

            public NeverPromise(CancellationToken cancellationToken)
            {
                this.cancellationToken = cancellationToken;
                if (this.cancellationToken.CanBeCanceled)
                {
                    this.cancellationToken.RegisterWithoutCaptureExecutionContext(cancellationCallback, this);
                }
            }

            private static void CancellationCallback(object state)
            {
                var self = (NeverPromise<T>)state;
                self.core.TrySetCanceled(self.cancellationToken);
            }

            public T GetResult(short token)
            {
                return core.GetResult(token);
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

            void IAppaTaskSource.GetResult(short token)
            {
                core.GetResult(token);
            }
        }
    }

    internal static class CompletedTasks
    {
        public static readonly AppaTask<AsyncUnit> AsyncUnit =
            AppaTask.FromResult(Appalachia.Utility.Async.AsyncUnit.Default);

        public static readonly AppaTask<bool> True = AppaTask.FromResult(true);
        public static readonly AppaTask<bool> False = AppaTask.FromResult(false);
        public static readonly AppaTask<int> Zero = AppaTask.FromResult(0);
        public static readonly AppaTask<int> MinusOne = AppaTask.FromResult(-1);
        public static readonly AppaTask<int> One = AppaTask.FromResult(1);
    }
}
