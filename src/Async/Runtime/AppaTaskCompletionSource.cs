#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace Appalachia.Utility.Async
{
    public interface IResolvePromise
    {
        bool TrySetResult();
    }

    public interface IResolvePromise<T>
    {
        bool TrySetResult(T value);
    }

    public interface IRejectPromise
    {
        bool TrySetException(Exception exception);
    }

    public interface ICancelPromise
    {
        bool TrySetCanceled(CancellationToken cancellationToken = default);
    }

    public interface IPromise<T> : IResolvePromise<T>, IRejectPromise, ICancelPromise
    {
    }

    public interface IPromise : IResolvePromise, IRejectPromise, ICancelPromise
    {
    }

    internal class ExceptionHolder
    {
        private ExceptionDispatchInfo exception;
        private bool calledGet;

        public ExceptionHolder(ExceptionDispatchInfo exception)
        {
            this.exception = exception;
        }

        public ExceptionDispatchInfo GetException()
        {
            if (!calledGet)
            {
                calledGet = true;
                GC.SuppressFinalize(this);
            }

            return exception;
        }

        ~ExceptionHolder()
        {
            if (!calledGet)
            {
                AppaTaskScheduler.PublishUnobservedTaskException(exception.SourceException);
            }
        }
    }

    [StructLayout(LayoutKind.Auto)]
    public struct AppaTaskCompletionSourceCore<TResult>
    {
        // Struct Size: TResult + (8 + 2 + 1 + 1 + 8 + 8)

        private TResult result;
        private object error; // ExceptionHolder or OperationCanceledException
        private short version;
        private bool hasUnhandledError;
        private int completedCount; // 0: completed == false
        private Action<object> continuation;
        private object continuationState;

        [DebuggerHidden]
        public void Reset()
        {
            ReportUnhandledError();

            unchecked
            {
                version += 1; // incr version.
            }

            completedCount = 0;
            result = default;
            error = null;
            hasUnhandledError = false;
            continuation = null;
            continuationState = null;
        }

        private void ReportUnhandledError()
        {
            if (hasUnhandledError)
            {
                try
                {
                    if (error is OperationCanceledException oc)
                    {
                        AppaTaskScheduler.PublishUnobservedTaskException(oc);
                    }
                    else if (error is ExceptionHolder e)
                    {
                        AppaTaskScheduler.PublishUnobservedTaskException(e.GetException().SourceException);
                    }
                }
                catch
                {
                    // ignored
                }
            }
        }

        internal void MarkHandled()
        {
            hasUnhandledError = false;
        }

        /// <summary>Completes with a successful result.</summary>
        /// <param name="result">The result.</param>
        [DebuggerHidden]
        public bool TrySetResult(TResult result)
        {
            if (Interlocked.Increment(ref completedCount) == 1)
            {
                // setup result
                this.result = result;

                if ((continuation != null) ||
                    (Interlocked.CompareExchange(
                         ref continuation,
                         AppaTaskCompletionSourceCoreShared.s_sentinel,
                         null
                     ) !=
                     null))
                {
                    continuation(continuationState);
                    return true;
                }
            }

            return false;
        }

        /// <summary>Completes with an error.</summary>
        /// <param name="error">The exception.</param>
        [DebuggerHidden]
        public bool TrySetException(Exception error)
        {
            if (Interlocked.Increment(ref completedCount) == 1)
            {
                // setup result
                hasUnhandledError = true;
                if (error is OperationCanceledException)
                {
                    this.error = error;
                }
                else
                {
                    this.error = new ExceptionHolder(ExceptionDispatchInfo.Capture(error));
                }

                if ((continuation != null) ||
                    (Interlocked.CompareExchange(
                         ref continuation,
                         AppaTaskCompletionSourceCoreShared.s_sentinel,
                         null
                     ) !=
                     null))
                {
                    continuation(continuationState);
                    return true;
                }
            }

            return false;
        }

        [DebuggerHidden]
        public bool TrySetCanceled(CancellationToken cancellationToken = default)
        {
            if (Interlocked.Increment(ref completedCount) == 1)
            {
                // setup result
                hasUnhandledError = true;
                error = new OperationCanceledException(cancellationToken);

                if ((continuation != null) ||
                    (Interlocked.CompareExchange(
                         ref continuation,
                         AppaTaskCompletionSourceCoreShared.s_sentinel,
                         null
                     ) !=
                     null))
                {
                    continuation(continuationState);
                    return true;
                }
            }

            return false;
        }

        /// <summary>Gets the operation version.</summary>
        [DebuggerHidden]
        public short Version => version;

        /// <summary>Gets the status of the operation.</summary>
        /// <param name="token">Opaque value that was provided to the <see cref="AppaTask" />'s constructor.</param>
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AppaTaskStatus GetStatus(short token)
        {
            ValidateToken(token);
            return (continuation == null) || (completedCount == 0)
                ? AppaTaskStatus.Pending
                : error == null
                    ? AppaTaskStatus.Succeeded
                    : error is OperationCanceledException
                        ? AppaTaskStatus.Canceled
                        : AppaTaskStatus.Faulted;
        }

        /// <summary>Gets the status of the operation without token validation.</summary>
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AppaTaskStatus UnsafeGetStatus()
        {
            return (continuation == null) || (completedCount == 0)
                ? AppaTaskStatus.Pending
                : error == null
                    ? AppaTaskStatus.Succeeded
                    : error is OperationCanceledException
                        ? AppaTaskStatus.Canceled
                        : AppaTaskStatus.Faulted;
        }

        /// <summary>Gets the result of the operation.</summary>
        /// <param name="token">Opaque value that was provided to the <see cref="AppaTask" />'s constructor.</param>

        // [StackTraceHidden]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TResult GetResult(short token)
        {
            ValidateToken(token);
            if (completedCount == 0)
            {
                throw new InvalidOperationException("Not yet completed, AppaTask only allow to use await.");
            }

            if (error != null)
            {
                hasUnhandledError = false;
                if (error is OperationCanceledException oce)
                {
                    throw oce;
                }

                if (error is ExceptionHolder eh)
                {
                    eh.GetException().Throw();
                }

                throw new InvalidOperationException("Critical: invalid exception type was held.");
            }

            return result;
        }

        /// <summary>Schedules the continuation action for this operation.</summary>
        /// <param name="continuation">The continuation to invoke when the operation has completed.</param>
        /// <param name="state">The state object to pass to <paramref name="continuation" /> when it's invoked.</param>
        /// <param name="token">Opaque value that was provided to the <see cref="AppaTask" />'s constructor.</param>
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void OnCompleted(
            Action<object> continuation,
            object state,
            short token /*, ValueTaskSourceOnCompletedFlags flags */)
        {
            if (continuation == null)
            {
                throw new ArgumentNullException(nameof(continuation));
            }

            ValidateToken(token);

            /* no use ValueTaskSourceOnCOmpletedFlags, always no capture ExecutionContext and SynchronizationContext. */

            /*
                PatternA: GetStatus=Pending => OnCompleted => TrySet*** => GetResult
                PatternB: TrySet*** => GetStatus=!Pending => GetResult
                PatternC: GetStatus=Pending => TrySet/OnCompleted(race condition) => GetResult
                C.1: win OnCompleted -> TrySet invoke saved continuation
                C.2: win TrySet -> should invoke continuation here.
            */

            // not set continuation yet.
            object oldContinuation = this.continuation;
            if (oldContinuation == null)
            {
                continuationState = state;
                oldContinuation = Interlocked.CompareExchange(ref this.continuation, continuation, null);
            }

            if (oldContinuation != null)
            {
                // already running continuation in TrySet.
                // It will cause call OnCompleted multiple time, invalid.
                if (!ReferenceEquals(oldContinuation, AppaTaskCompletionSourceCoreShared.s_sentinel))
                {
                    throw new InvalidOperationException(
                        "Already continuation registered, can not await twice or get Status after await."
                    );
                }

                continuation(state);
            }
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ValidateToken(short token)
        {
            if (token != version)
            {
                throw new InvalidOperationException(
                    "Token version is not matched, can not await twice or get Status after await."
                );
            }
        }
    }

    internal static class
        AppaTaskCompletionSourceCoreShared // separated out of generic to avoid unnecessary duplication
    {
        internal static readonly Action<object> s_sentinel = CompletionSentinel;

        private static void CompletionSentinel(object _) // named method to aid debugging
        {
            throw new InvalidOperationException("The sentinel delegate should never be invoked.");
        }
    }

    public class AutoResetAppaTaskCompletionSource : IAppaTaskSource,
                                                     ITaskPoolNode<AutoResetAppaTaskCompletionSource>,
                                                     IPromise
    {
        private static TaskPool<AutoResetAppaTaskCompletionSource> pool;
        private AutoResetAppaTaskCompletionSource nextNode;
        public ref AutoResetAppaTaskCompletionSource NextNode => ref nextNode;

        static AutoResetAppaTaskCompletionSource()
        {
            TaskPool.RegisterSizeGetter(typeof(AutoResetAppaTaskCompletionSource), () => pool.Size);
        }

        private AppaTaskCompletionSourceCore<AsyncUnit> core;

        private AutoResetAppaTaskCompletionSource()
        {
        }

        [DebuggerHidden]
        public static AutoResetAppaTaskCompletionSource Create()
        {
            if (!pool.TryPop(out var result))
            {
                result = new AutoResetAppaTaskCompletionSource();
            }

            TaskTracker.TrackActiveTask(result, 2);
            return result;
        }

        [DebuggerHidden]
        public static AutoResetAppaTaskCompletionSource CreateFromCanceled(
            CancellationToken cancellationToken,
            out short token)
        {
            var source = Create();
            source.TrySetCanceled(cancellationToken);
            token = source.core.Version;
            return source;
        }

        [DebuggerHidden]
        public static AutoResetAppaTaskCompletionSource CreateFromException(
            Exception exception,
            out short token)
        {
            var source = Create();
            source.TrySetException(exception);
            token = source.core.Version;
            return source;
        }

        [DebuggerHidden]
        public static AutoResetAppaTaskCompletionSource CreateCompleted(out short token)
        {
            var source = Create();
            source.TrySetResult();
            token = source.core.Version;
            return source;
        }

        public AppaTask Task
        {
            [DebuggerHidden] get => new AppaTask(this, core.Version);
        }

        [DebuggerHidden]
        public bool TrySetResult()
        {
            return core.TrySetResult(AsyncUnit.Default);
        }

        [DebuggerHidden]
        public bool TrySetCanceled(CancellationToken cancellationToken = default)
        {
            return core.TrySetCanceled(cancellationToken);
        }

        [DebuggerHidden]
        public bool TrySetException(Exception exception)
        {
            return core.TrySetException(exception);
        }

        [DebuggerHidden]
        public void GetResult(short token)
        {
            try
            {
                core.GetResult(token);
            }
            finally
            {
                TryReturn();
            }
        }

        [DebuggerHidden]
        public AppaTaskStatus GetStatus(short token)
        {
            return core.GetStatus(token);
        }

        [DebuggerHidden]
        public AppaTaskStatus UnsafeGetStatus()
        {
            return core.UnsafeGetStatus();
        }

        [DebuggerHidden]
        public void OnCompleted(Action<object> continuation, object state, short token)
        {
            core.OnCompleted(continuation, state, token);
        }

        [DebuggerHidden]
        private bool TryReturn()
        {
            TaskTracker.RemoveTracking(this);
            core.Reset();
            return pool.TryPush(this);
        }
    }

    public class AutoResetAppaTaskCompletionSource<T> : IAppaTaskSource<T>,
                                                        ITaskPoolNode<AutoResetAppaTaskCompletionSource<T>>,
                                                        IPromise<T>
    {
        private static TaskPool<AutoResetAppaTaskCompletionSource<T>> pool;
        private AutoResetAppaTaskCompletionSource<T> nextNode;
        public ref AutoResetAppaTaskCompletionSource<T> NextNode => ref nextNode;

        static AutoResetAppaTaskCompletionSource()
        {
            TaskPool.RegisterSizeGetter(typeof(AutoResetAppaTaskCompletionSource<T>), () => pool.Size);
        }

        private AppaTaskCompletionSourceCore<T> core;

        private AutoResetAppaTaskCompletionSource()
        {
        }

        [DebuggerHidden]
        public static AutoResetAppaTaskCompletionSource<T> Create()
        {
            if (!pool.TryPop(out var result))
            {
                result = new AutoResetAppaTaskCompletionSource<T>();
            }

            TaskTracker.TrackActiveTask(result, 2);
            return result;
        }

        [DebuggerHidden]
        public static AutoResetAppaTaskCompletionSource<T> CreateFromCanceled(
            CancellationToken cancellationToken,
            out short token)
        {
            var source = Create();
            source.TrySetCanceled(cancellationToken);
            token = source.core.Version;
            return source;
        }

        [DebuggerHidden]
        public static AutoResetAppaTaskCompletionSource<T> CreateFromException(
            Exception exception,
            out short token)
        {
            var source = Create();
            source.TrySetException(exception);
            token = source.core.Version;
            return source;
        }

        [DebuggerHidden]
        public static AutoResetAppaTaskCompletionSource<T> CreateFromResult(T result, out short token)
        {
            var source = Create();
            source.TrySetResult(result);
            token = source.core.Version;
            return source;
        }

        public AppaTask<T> Task
        {
            [DebuggerHidden] get => new AppaTask<T>(this, core.Version);
        }

        [DebuggerHidden]
        public bool TrySetResult(T result)
        {
            return core.TrySetResult(result);
        }

        [DebuggerHidden]
        public bool TrySetCanceled(CancellationToken cancellationToken = default)
        {
            return core.TrySetCanceled(cancellationToken);
        }

        [DebuggerHidden]
        public bool TrySetException(Exception exception)
        {
            return core.TrySetException(exception);
        }

        [DebuggerHidden]
        public T GetResult(short token)
        {
            try
            {
                return core.GetResult(token);
            }
            finally
            {
                TryReturn();
            }
        }

        [DebuggerHidden]
        void IAppaTaskSource.GetResult(short token)
        {
            GetResult(token);
        }

        [DebuggerHidden]
        public AppaTaskStatus GetStatus(short token)
        {
            return core.GetStatus(token);
        }

        [DebuggerHidden]
        public AppaTaskStatus UnsafeGetStatus()
        {
            return core.UnsafeGetStatus();
        }

        [DebuggerHidden]
        public void OnCompleted(Action<object> continuation, object state, short token)
        {
            core.OnCompleted(continuation, state, token);
        }

        [DebuggerHidden]
        private bool TryReturn()
        {
            TaskTracker.RemoveTracking(this);
            core.Reset();
            return pool.TryPush(this);
        }
    }

    public class AppaTaskCompletionSource : IAppaTaskSource, IPromise
    {
        private CancellationToken cancellationToken;
        private ExceptionHolder exception;
        private object gate;
        private Action<object> singleContinuation;
        private object singleState;
        private List<(Action<object>, object)> secondaryContinuationList;

        private int intStatus; // AppaTaskStatus
        private bool handled;

        public AppaTaskCompletionSource()
        {
            TaskTracker.TrackActiveTask(this, 2);
        }

        [DebuggerHidden]
        internal void MarkHandled()
        {
            if (!handled)
            {
                handled = true;
                TaskTracker.RemoveTracking(this);
            }
        }

        public AppaTask Task
        {
            [DebuggerHidden] get => new AppaTask(this, 0);
        }

        [DebuggerHidden]
        public bool TrySetResult()
        {
            return TrySignalCompletion(AppaTaskStatus.Succeeded);
        }

        [DebuggerHidden]
        public bool TrySetCanceled(CancellationToken cancellationToken = default)
        {
            if (UnsafeGetStatus() != AppaTaskStatus.Pending)
            {
                return false;
            }

            this.cancellationToken = cancellationToken;
            return TrySignalCompletion(AppaTaskStatus.Canceled);
        }

        [DebuggerHidden]
        public bool TrySetException(Exception exception)
        {
            if (exception is OperationCanceledException oce)
            {
                return TrySetCanceled(oce.CancellationToken);
            }

            if (UnsafeGetStatus() != AppaTaskStatus.Pending)
            {
                return false;
            }

            this.exception = new ExceptionHolder(ExceptionDispatchInfo.Capture(exception));
            return TrySignalCompletion(AppaTaskStatus.Faulted);
        }

        [DebuggerHidden]
        public void GetResult(short token)
        {
            MarkHandled();

            var status = (AppaTaskStatus)intStatus;
            switch (status)
            {
                case AppaTaskStatus.Succeeded:
                    return;
                case AppaTaskStatus.Faulted:
                    exception.GetException().Throw();
                    return;
                case AppaTaskStatus.Canceled:
                    throw new OperationCanceledException(cancellationToken);
                default:
                case AppaTaskStatus.Pending:
                    throw new InvalidOperationException("not yet completed.");
            }
        }

        [DebuggerHidden]
        public AppaTaskStatus GetStatus(short token)
        {
            return (AppaTaskStatus)intStatus;
        }

        [DebuggerHidden]
        public AppaTaskStatus UnsafeGetStatus()
        {
            return (AppaTaskStatus)intStatus;
        }

        [DebuggerHidden]
        public void OnCompleted(Action<object> continuation, object state, short token)
        {
            if (gate == null)
            {
                Interlocked.CompareExchange(ref gate, new object(), null);
            }

            var lockGate = Thread.VolatileRead(ref gate);
            lock (lockGate) // wait TrySignalCompletion, after status is not pending.
            {
                if ((AppaTaskStatus)intStatus != AppaTaskStatus.Pending)
                {
                    continuation(state);
                    return;
                }

                if (singleContinuation == null)
                {
                    singleContinuation = continuation;
                    singleState = state;
                }
                else
                {
                    if (secondaryContinuationList == null)
                    {
                        secondaryContinuationList = new List<(Action<object>, object)>();
                    }

                    secondaryContinuationList.Add((continuation, state));
                }
            }
        }

        [DebuggerHidden]
        private bool TrySignalCompletion(AppaTaskStatus status)
        {
            if (Interlocked.CompareExchange(ref intStatus, (int)status, (int)AppaTaskStatus.Pending) ==
                (int)AppaTaskStatus.Pending)
            {
                if (gate == null)
                {
                    Interlocked.CompareExchange(ref gate, new object(), null);
                }

                var lockGate = Thread.VolatileRead(ref gate);
                lock (lockGate) // wait OnCompleted.
                {
                    if (singleContinuation != null)
                    {
                        try
                        {
                            singleContinuation(singleState);
                        }
                        catch (Exception ex)
                        {
                            AppaTaskScheduler.PublishUnobservedTaskException(ex);
                        }
                    }

                    if (secondaryContinuationList != null)
                    {
                        foreach (var (c, state) in secondaryContinuationList)
                        {
                            try
                            {
                                c(state);
                            }
                            catch (Exception ex)
                            {
                                AppaTaskScheduler.PublishUnobservedTaskException(ex);
                            }
                        }
                    }

                    singleContinuation = null;
                    singleState = null;
                    secondaryContinuationList = null;
                }

                return true;
            }

            return false;
        }
    }

    public class AppaTaskCompletionSource<T> : IAppaTaskSource<T>, IPromise<T>
    {
        private CancellationToken cancellationToken;
        private T result;
        private ExceptionHolder exception;
        private object gate;
        private Action<object> singleContinuation;
        private object singleState;
        private List<(Action<object>, object)> secondaryContinuationList;

        private int intStatus; // AppaTaskStatus
        private bool handled;

        public AppaTaskCompletionSource()
        {
            TaskTracker.TrackActiveTask(this, 2);
        }

        [DebuggerHidden]
        internal void MarkHandled()
        {
            if (!handled)
            {
                handled = true;
                TaskTracker.RemoveTracking(this);
            }
        }

        public AppaTask<T> Task
        {
            [DebuggerHidden] get => new AppaTask<T>(this, 0);
        }

        [DebuggerHidden]
        public bool TrySetResult(T result)
        {
            if (UnsafeGetStatus() != AppaTaskStatus.Pending)
            {
                return false;
            }

            this.result = result;
            return TrySignalCompletion(AppaTaskStatus.Succeeded);
        }

        [DebuggerHidden]
        public bool TrySetCanceled(CancellationToken cancellationToken = default)
        {
            if (UnsafeGetStatus() != AppaTaskStatus.Pending)
            {
                return false;
            }

            this.cancellationToken = cancellationToken;
            return TrySignalCompletion(AppaTaskStatus.Canceled);
        }

        [DebuggerHidden]
        public bool TrySetException(Exception exception)
        {
            if (exception is OperationCanceledException oce)
            {
                return TrySetCanceled(oce.CancellationToken);
            }

            if (UnsafeGetStatus() != AppaTaskStatus.Pending)
            {
                return false;
            }

            this.exception = new ExceptionHolder(ExceptionDispatchInfo.Capture(exception));
            return TrySignalCompletion(AppaTaskStatus.Faulted);
        }

        [DebuggerHidden]
        public T GetResult(short token)
        {
            MarkHandled();

            var status = (AppaTaskStatus)intStatus;
            switch (status)
            {
                case AppaTaskStatus.Succeeded:
                    return result;
                case AppaTaskStatus.Faulted:
                    exception.GetException().Throw();
                    return default;
                case AppaTaskStatus.Canceled:
                    throw new OperationCanceledException(cancellationToken);
                default:
                case AppaTaskStatus.Pending:
                    throw new InvalidOperationException("not yet completed.");
            }
        }

        [DebuggerHidden]
        void IAppaTaskSource.GetResult(short token)
        {
            GetResult(token);
        }

        [DebuggerHidden]
        public AppaTaskStatus GetStatus(short token)
        {
            return (AppaTaskStatus)intStatus;
        }

        [DebuggerHidden]
        public AppaTaskStatus UnsafeGetStatus()
        {
            return (AppaTaskStatus)intStatus;
        }

        [DebuggerHidden]
        public void OnCompleted(Action<object> continuation, object state, short token)
        {
            if (gate == null)
            {
                Interlocked.CompareExchange(ref gate, new object(), null);
            }

            var lockGate = Thread.VolatileRead(ref gate);
            lock (lockGate) // wait TrySignalCompletion, after status is not pending.
            {
                if ((AppaTaskStatus)intStatus != AppaTaskStatus.Pending)
                {
                    continuation(state);
                    return;
                }

                if (singleContinuation == null)
                {
                    singleContinuation = continuation;
                    singleState = state;
                }
                else
                {
                    if (secondaryContinuationList == null)
                    {
                        secondaryContinuationList = new List<(Action<object>, object)>();
                    }

                    secondaryContinuationList.Add((continuation, state));
                }
            }
        }

        [DebuggerHidden]
        private bool TrySignalCompletion(AppaTaskStatus status)
        {
            if (Interlocked.CompareExchange(ref intStatus, (int)status, (int)AppaTaskStatus.Pending) ==
                (int)AppaTaskStatus.Pending)
            {
                if (gate == null)
                {
                    Interlocked.CompareExchange(ref gate, new object(), null);
                }

                var lockGate = Thread.VolatileRead(ref gate);
                lock (lockGate) // wait OnCompleted.
                {
                    if (singleContinuation != null)
                    {
                        try
                        {
                            singleContinuation(singleState);
                        }
                        catch (Exception ex)
                        {
                            AppaTaskScheduler.PublishUnobservedTaskException(ex);
                        }
                    }

                    if (secondaryContinuationList != null)
                    {
                        foreach (var (c, state) in secondaryContinuationList)
                        {
                            try
                            {
                                c(state);
                            }
                            catch (Exception ex)
                            {
                                AppaTaskScheduler.PublishUnobservedTaskException(ex);
                            }
                        }
                    }

                    singleContinuation = null;
                    singleState = null;
                    secondaryContinuationList = null;
                }

                return true;
            }

            return false;
        }
    }
}
