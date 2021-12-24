#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Appalachia.Utility.Async
{
    public static class CancellationTokenExtensions
    {
        private static readonly Action<object> cancellationTokenCallback = Callback;
        private static readonly Action<object> disposeCallback = DisposeCallback;

        public static CancellationToken ToCancellationToken(this AppaTask task)
        {
            var cts = new CancellationTokenSource();
            ToCancellationTokenCore(task, cts).Forget();
            return cts.Token;
        }

        public static CancellationToken ToCancellationToken(this AppaTask task, CancellationToken linkToken)
        {
            if (linkToken.IsCancellationRequested)
            {
                return linkToken;
            }

            if (!linkToken.CanBeCanceled)
            {
                return ToCancellationToken(task);
            }

            var cts = CancellationTokenSource.CreateLinkedTokenSource(linkToken);
            ToCancellationTokenCore(task, cts).Forget();

            return cts.Token;
        }

        public static CancellationToken ToCancellationToken<T>(this AppaTask<T> task)
        {
            return ToCancellationToken(task.AsAppaTask());
        }

        public static CancellationToken ToCancellationToken<T>(
            this AppaTask<T> task,
            CancellationToken linkToken)
        {
            return ToCancellationToken(task.AsAppaTask(), linkToken);
        }

        private static async AppaTaskVoid ToCancellationTokenCore(AppaTask task, CancellationTokenSource cts)
        {
            try
            {
                await task;
            }
            catch (Exception ex)
            {
                AppaTaskScheduler.PublishUnobservedTaskException(ex);
            }

            cts.Cancel();
            cts.Dispose();
        }

        public static (AppaTask, CancellationTokenRegistration) ToAppaTask(
            this CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return (AppaTask.FromCanceled(cancellationToken), default(CancellationTokenRegistration));
            }

            var promise = new AppaTaskCompletionSource();
            return (promise.Task,
                cancellationToken.RegisterWithoutCaptureExecutionContext(cancellationTokenCallback, promise));
        }

        private static void Callback(object state)
        {
            var promise = (AppaTaskCompletionSource)state;
            promise.TrySetResult();
        }

        public static CancellationTokenAwaitable WaitUntilCanceled(this CancellationToken cancellationToken)
        {
            return new CancellationTokenAwaitable(cancellationToken);
        }

        public static CancellationTokenRegistration RegisterWithoutCaptureExecutionContext(
            this CancellationToken cancellationToken,
            Action callback)
        {
            var restoreFlow = false;
            if (!ExecutionContext.IsFlowSuppressed())
            {
                ExecutionContext.SuppressFlow();
                restoreFlow = true;
            }

            try
            {
                return cancellationToken.Register(callback, false);
            }
            finally
            {
                if (restoreFlow)
                {
                    ExecutionContext.RestoreFlow();
                }
            }
        }

        public static CancellationTokenRegistration RegisterWithoutCaptureExecutionContext(
            this CancellationToken cancellationToken,
            Action<object> callback,
            object state)
        {
            var restoreFlow = false;
            if (!ExecutionContext.IsFlowSuppressed())
            {
                ExecutionContext.SuppressFlow();
                restoreFlow = true;
            }

            try
            {
                return cancellationToken.Register(callback, state, false);
            }
            finally
            {
                if (restoreFlow)
                {
                    ExecutionContext.RestoreFlow();
                }
            }
        }

        public static CancellationTokenRegistration AddTo(
            this IDisposable disposable,
            CancellationToken cancellationToken)
        {
            return cancellationToken.RegisterWithoutCaptureExecutionContext(disposeCallback, disposable);
        }

        private static void DisposeCallback(object state)
        {
            var d = (IDisposable)state;
            d.Dispose();
        }
    }

    public struct CancellationTokenAwaitable
    {
        private CancellationToken cancellationToken;

        public CancellationTokenAwaitable(CancellationToken cancellationToken)
        {
            this.cancellationToken = cancellationToken;
        }

        public Awaiter GetAwaiter()
        {
            return new Awaiter(cancellationToken);
        }

        public struct Awaiter : ICriticalNotifyCompletion
        {
            private CancellationToken cancellationToken;

            public Awaiter(CancellationToken cancellationToken)
            {
                this.cancellationToken = cancellationToken;
            }

            public bool IsCompleted =>
                !cancellationToken.CanBeCanceled || cancellationToken.IsCancellationRequested;

            public void GetResult()
            {
            }

            public void OnCompleted(Action continuation)
            {
                UnsafeOnCompleted(continuation);
            }

            public void UnsafeOnCompleted(Action continuation)
            {
                cancellationToken.RegisterWithoutCaptureExecutionContext(continuation);
            }
        }
    }
}
