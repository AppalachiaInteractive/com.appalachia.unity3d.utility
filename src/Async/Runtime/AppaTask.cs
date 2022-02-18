#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS0436

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Appalachia.Utility.Async.CompilerServices;

namespace Appalachia.Utility.Async
{
    internal static class AwaiterActions
    {
        #region Constants and Static Readonly

        internal static readonly Action<object> InvokeContinuationDelegate = Continuation;

        #endregion

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void Continuation(object state)
        {
            ((Action)state).Invoke();
        }
    }

    /// <summary>
    ///     Lightweight unity specified task-like object.
    /// </summary>
    [AsyncMethodBuilder(typeof(AsyncAppaTaskMethodBuilder))]
    [StructLayout(LayoutKind.Auto)]
    public readonly partial struct AppaTask
    {
        private readonly IAppaTaskSource source;
        private readonly short token;

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AppaTask(IAppaTaskSource source, short token)
        {
            this.source = source;
            this.token = token;
        }

        public AppaTaskStatus Status
        {
            [DebuggerHidden]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                if (source == null)
                {
                    return AppaTaskStatus.Succeeded;
                }

                return source.GetStatus(token);
            }
        }

        public static bool ExecutionIsAllowed => PlayerLoopHelper.IsInitialized;

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Awaiter GetAwaiter()
        {
            return new Awaiter(this);
        }

        /// <summary>
        ///     returns (bool IsCanceled) instead of throws OperationCanceledException.
        /// </summary>
        public AppaTask<bool> SuppressCancellationThrow()
        {
            var status = Status;
            if (status == AppaTaskStatus.Succeeded)
            {
                return CompletedTasks.False;
            }

            if (status == AppaTaskStatus.Canceled)
            {
                return CompletedTasks.True;
            }

            return new AppaTask<bool>(new IsCanceledSource(source), token);
        }

#if !UNITY_2018_3_OR_NEWER
        public static implicit operator System.Threading.Tasks.ValueTask(in AppaTask self)
        {
            if (self.source == null)
            {
                return default;
            }

#if NETSTANDARD2_0
            return self.AsValueTask();
#else
            return new System.Threading.Tasks.ValueTask(self.source, self.token);
#endif
        }

#endif

        /// <inheritdoc />
        public override string ToString()
        {
            if (source == null)
            {
                return "()";
            }

            return "(" + source.UnsafeGetStatus() + ")";
        }

        /// <summary>
        ///     Memoizing inner IValueTaskSource. The result AppaTask can await multiple.
        /// </summary>
        public AppaTask Preserve()
        {
            if (source == null)
            {
                return this;
            }

            return new AppaTask(new MemoizeSource(source), token);
        }

        public AppaTask<AsyncUnit> AsAsyncUnitAppaTask()
        {
            if (source == null)
            {
                return CompletedTasks.AsyncUnit;
            }

            var status = source.GetStatus(token);
            if (status.IsCompletedSuccessfully())
            {
                source.GetResult(token);
                return CompletedTasks.AsyncUnit;
            }

            if (source is IAppaTaskSource<AsyncUnit> asyncUnitSource)
            {
                return new AppaTask<AsyncUnit>(asyncUnitSource, token);
            }

            return new AppaTask<AsyncUnit>(new AsyncUnitSource(source), token);
        }

        private sealed class AsyncUnitSource : IAppaTaskSource<AsyncUnit>
        {
            public AsyncUnitSource(IAppaTaskSource source)
            {
                this.source = source;
            }

            #region Fields and Autoproperties

            private readonly IAppaTaskSource source;

            #endregion

            #region IAppaTaskSource<AsyncUnit> Members

            public AsyncUnit GetResult(short token)
            {
                source.GetResult(token);
                return AsyncUnit.Default;
            }

            public AppaTaskStatus GetStatus(short token)
            {
                return source.GetStatus(token);
            }

            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                source.OnCompleted(continuation, state, token);
            }

            public AppaTaskStatus UnsafeGetStatus()
            {
                return source.UnsafeGetStatus();
            }

            void IAppaTaskSource.GetResult(short token)
            {
                GetResult(token);
            }

            #endregion
        }

        private sealed class IsCanceledSource : IAppaTaskSource<bool>
        {
            public IsCanceledSource(IAppaTaskSource source)
            {
                this.source = source;
            }

            #region Fields and Autoproperties

            private readonly IAppaTaskSource source;

            #endregion

            #region IAppaTaskSource<bool> Members

            public bool GetResult(short token)
            {
                if (source.GetStatus(token) == AppaTaskStatus.Canceled)
                {
                    return true;
                }

                source.GetResult(token);
                return false;
            }

            void IAppaTaskSource.GetResult(short token)
            {
                GetResult(token);
            }

            public AppaTaskStatus GetStatus(short token)
            {
                return source.GetStatus(token);
            }

            public AppaTaskStatus UnsafeGetStatus()
            {
                return source.UnsafeGetStatus();
            }

            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                source.OnCompleted(continuation, state, token);
            }

            #endregion
        }

        private sealed class MemoizeSource : IAppaTaskSource
        {
            public MemoizeSource(IAppaTaskSource source)
            {
                this.source = source;
            }

            #region Fields and Autoproperties

            private AppaTaskStatus status;
            private ExceptionDispatchInfo exception;
            private IAppaTaskSource source;

            #endregion

            #region IAppaTaskSource Members

            public void GetResult(short token)
            {
                if (source == null)
                {
                    if (exception != null)
                    {
                        exception.Throw();
                    }
                }
                else
                {
                    try
                    {
                        source.GetResult(token);
                        status = AppaTaskStatus.Succeeded;
                    }
                    catch (Exception ex)
                    {
                        exception = ExceptionDispatchInfo.Capture(ex);
                        if (ex is OperationCanceledException)
                        {
                            status = AppaTaskStatus.Canceled;
                        }
                        else
                        {
                            status = AppaTaskStatus.Faulted;
                        }

                        throw;
                    }
                    finally
                    {
                        source = null;
                    }
                }
            }

            public AppaTaskStatus GetStatus(short token)
            {
                if (source == null)
                {
                    return status;
                }

                return source.GetStatus(token);
            }

            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                if (source == null)
                {
                    continuation(state);
                }
                else
                {
                    source.OnCompleted(continuation, state, token);
                }
            }

            public AppaTaskStatus UnsafeGetStatus()
            {
                if (source == null)
                {
                    return status;
                }

                return source.UnsafeGetStatus();
            }

            #endregion
        }

        public readonly struct Awaiter : ICriticalNotifyCompletion
        {
            [DebuggerHidden]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Awaiter(in AppaTask task)
            {
                this.task = task;
            }

            #region Fields and Autoproperties

            private readonly AppaTask task;

            #endregion

            public bool IsCompleted
            {
                [DebuggerHidden]
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => task.Status.IsCompleted();
            }

            [DebuggerHidden]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void GetResult()
            {
                if (task.source == null)
                {
                    return;
                }

                task.source.GetResult(task.token);
            }

            /// <summary>
            ///     If register manually continuation, you can use it instead of for compiler OnCompleted methods.
            /// </summary>
            [DebuggerHidden]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void SourceOnCompleted(Action<object> continuation, object state)
            {
                if (task.source == null)
                {
                    continuation(state);
                }
                else
                {
                    task.source.OnCompleted(continuation, state, task.token);
                }
            }

            #region ICriticalNotifyCompletion Members

            [DebuggerHidden]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void OnCompleted(Action continuation)
            {
                if (task.source == null)
                {
                    continuation();
                }
                else
                {
                    task.source.OnCompleted(
                        AwaiterActions.InvokeContinuationDelegate,
                        continuation,
                        task.token
                    );
                }
            }

            [DebuggerHidden]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void UnsafeOnCompleted(Action continuation)
            {
                if (task.source == null)
                {
                    continuation();
                }
                else
                {
                    task.source.OnCompleted(
                        AwaiterActions.InvokeContinuationDelegate,
                        continuation,
                        task.token
                    );
                }
            }

            #endregion
        }
    }

    /// <summary>
    ///     Lightweight unity specified task-like object.
    /// </summary>
    [AsyncMethodBuilder(typeof(AsyncAppaTaskMethodBuilder<>))]
    [StructLayout(LayoutKind.Auto)]
    public readonly struct AppaTask<T>
    {
        private readonly IAppaTaskSource<T> source;
        private readonly T result;
        private readonly short token;

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AppaTask(T result)
        {
            source = default;
            token = default;
            this.result = result;
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AppaTask(IAppaTaskSource<T> source, short token)
        {
            this.source = source;
            this.token = token;
            result = default;
        }

        public AppaTaskStatus Status
        {
            [DebuggerHidden]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => source == null ? AppaTaskStatus.Succeeded : source.GetStatus(token);
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Awaiter GetAwaiter()
        {
            return new Awaiter(this);
        }

        /// <summary>
        ///     Memoizing inner IValueTaskSource. The result AppaTask can await multiple.
        /// </summary>
        public AppaTask<T> Preserve()
        {
            if (source == null)
            {
                return this;
            }

            return new AppaTask<T>(new MemoizeSource(source), token);
        }

        public AppaTask AsAppaTask()
        {
            if (source == null)
            {
                return AppaTask.CompletedTask;
            }

            var status = source.GetStatus(token);
            if (status.IsCompletedSuccessfully())
            {
                source.GetResult(token);
                return AppaTask.CompletedTask;
            }

            // Converting AppaTask<T> -> AppaTask is zero overhead.
            return new AppaTask(source, token);
        }

        public static implicit operator AppaTask(AppaTask<T> self)
        {
            return self.AsAppaTask();
        }

#if !UNITY_2018_3_OR_NEWER
        public static implicit operator System.Threading.Tasks.ValueTask<T>(in AppaTask<T> self)
        {
            if (self.source == null)
            {
                return new System.Threading.Tasks.ValueTask<T>(self.result);
            }

#if NETSTANDARD2_0
            return self.AsValueTask();
#else
            return new System.Threading.Tasks.ValueTask<T>(self.source, self.token);
#endif
        }

#endif

        /// <summary>
        ///     returns (bool IsCanceled, T Result) instead of throws OperationCanceledException.
        /// </summary>
        public AppaTask<(bool IsCanceled, T Result)> SuppressCancellationThrow()
        {
            if (source == null)
            {
                return new AppaTask<(bool IsCanceled, T Result)>((false, result));
            }

            return new AppaTask<(bool, T)>(new IsCanceledSource(source), token);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return source == null ? result?.ToString() : "(" + source.UnsafeGetStatus() + ")";
        }

        private sealed class IsCanceledSource : IAppaTaskSource<(bool, T)>
        {
            [DebuggerHidden]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public IsCanceledSource(IAppaTaskSource<T> source)
            {
                this.source = source;
            }

            #region Fields and Autoproperties

            private readonly IAppaTaskSource<T> source;

            #endregion

            #region IAppaTaskSource<(bool, T)> Members

            [DebuggerHidden]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public (bool, T) GetResult(short token)
            {
                if (source.GetStatus(token) == AppaTaskStatus.Canceled)
                {
                    return (true, default);
                }

                var result = source.GetResult(token);
                return (false, result);
            }

            [DebuggerHidden]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            void IAppaTaskSource.GetResult(short token)
            {
                GetResult(token);
            }

            [DebuggerHidden]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public AppaTaskStatus GetStatus(short token)
            {
                return source.GetStatus(token);
            }

            [DebuggerHidden]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public AppaTaskStatus UnsafeGetStatus()
            {
                return source.UnsafeGetStatus();
            }

            [DebuggerHidden]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                source.OnCompleted(continuation, state, token);
            }

            #endregion
        }

        private sealed class MemoizeSource : IAppaTaskSource<T>
        {
            public MemoizeSource(IAppaTaskSource<T> source)
            {
                this.source = source;
            }

            #region Fields and Autoproperties

            private AppaTaskStatus status;
            private ExceptionDispatchInfo exception;
            private IAppaTaskSource<T> source;
            private T result;

            #endregion

            #region IAppaTaskSource<T> Members

            public T GetResult(short token)
            {
                if (source == null)
                {
                    if (exception != null)
                    {
                        exception.Throw();
                    }

                    return result;
                }

                try
                {
                    result = source.GetResult(token);
                    status = AppaTaskStatus.Succeeded;
                    return result;
                }
                catch (Exception ex)
                {
                    exception = ExceptionDispatchInfo.Capture(ex);
                    if (ex is OperationCanceledException)
                    {
                        status = AppaTaskStatus.Canceled;
                    }
                    else
                    {
                        status = AppaTaskStatus.Faulted;
                    }

                    throw;
                }
                finally
                {
                    source = null;
                }
            }

            void IAppaTaskSource.GetResult(short token)
            {
                GetResult(token);
            }

            public AppaTaskStatus GetStatus(short token)
            {
                if (source == null)
                {
                    return status;
                }

                return source.GetStatus(token);
            }

            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                if (source == null)
                {
                    continuation(state);
                }
                else
                {
                    source.OnCompleted(continuation, state, token);
                }
            }

            public AppaTaskStatus UnsafeGetStatus()
            {
                if (source == null)
                {
                    return status;
                }

                return source.UnsafeGetStatus();
            }

            #endregion
        }

        public readonly struct Awaiter : ICriticalNotifyCompletion
        {
            [DebuggerHidden]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Awaiter(in AppaTask<T> task)
            {
                this.task = task;
            }

            #region Fields and Autoproperties

            private readonly AppaTask<T> task;

            #endregion

            public bool IsCompleted
            {
                [DebuggerHidden]
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => task.Status.IsCompleted();
            }

            [DebuggerHidden]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public T GetResult()
            {
                var s = task.source;
                if (s == null)
                {
                    return task.result;
                }

                return s.GetResult(task.token);
            }

            /// <summary>
            ///     If register manually continuation, you can use it instead of for compiler OnCompleted methods.
            /// </summary>
            [DebuggerHidden]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void SourceOnCompleted(Action<object> continuation, object state)
            {
                var s = task.source;
                if (s == null)
                {
                    continuation(state);
                }
                else
                {
                    s.OnCompleted(continuation, state, task.token);
                }
            }

            #region ICriticalNotifyCompletion Members

            [DebuggerHidden]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void OnCompleted(Action continuation)
            {
                var s = task.source;
                if (s == null)
                {
                    continuation();
                }
                else
                {
                    s.OnCompleted(AwaiterActions.InvokeContinuationDelegate, continuation, task.token);
                }
            }

            [DebuggerHidden]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void UnsafeOnCompleted(Action continuation)
            {
                var s = task.source;
                if (s == null)
                {
                    continuation();
                }
                else
                {
                    s.OnCompleted(AwaiterActions.InvokeContinuationDelegate, continuation, task.token);
                }
            }

            #endregion
        }
    }
}
