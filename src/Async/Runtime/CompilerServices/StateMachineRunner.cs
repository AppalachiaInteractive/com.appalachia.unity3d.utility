#pragma warning disable CS1591

using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Appalachia.Utility.Async.CompilerServices
{
    // #ENABLE_IL2CPP in this file is to avoid bug of IL2CPP VM.
    // Issue is tracked on https://issuetracker.unity3d.com/issues/il2cpp-incorrect-results-when-calling-a-method-from-outside-class-in-a-struct
    // but currently it is labeled `Won't Fix`.

    internal interface IStateMachineRunner
    {
        Action MoveNext { get; }
        void Return();

#if ENABLE_IL2CPP
        Action ReturnAction { get; }
#endif
    }

    internal interface IStateMachineRunnerPromise : IAppaTaskSource
    {
        Action MoveNext { get; }
        AppaTask Task { get; }
        void SetResult();
        void SetException(Exception exception);
    }

    internal interface IStateMachineRunnerPromise<T> : IAppaTaskSource<T>
    {
        Action MoveNext { get; }
        AppaTask<T> Task { get; }
        void SetResult(T result);
        void SetException(Exception exception);
    }

    internal static class StateMachineUtility
    {
        // Get AsyncStateMachine internal state to check IL2CPP bug
        public static int GetState(IAsyncStateMachine stateMachine)
        {
            var info = stateMachine.GetType()
                                   .GetFields(
                                        System.Reflection.BindingFlags.Public |
                                        System.Reflection.BindingFlags.NonPublic |
                                        System.Reflection.BindingFlags.Instance
                                    )
                                   .First(x => x.Name.EndsWith("__state"));
            return (int)info.GetValue(stateMachine);
        }
    }

    internal sealed class AsyncAppaTaskVoid<TStateMachine> : IStateMachineRunner,
                                                             ITaskPoolNode<AsyncAppaTaskVoid<TStateMachine>>,
                                                             IAppaTaskSource
        where TStateMachine : IAsyncStateMachine
    {
        private static TaskPool<AsyncAppaTaskVoid<TStateMachine>> pool;

#if ENABLE_IL2CPP
        public Action ReturnAction { get; }
#endif

        private TStateMachine stateMachine;

        public Action MoveNext { get; }

        public AsyncAppaTaskVoid()
        {
            MoveNext = Run;
#if ENABLE_IL2CPP
            ReturnAction = Return;
#endif
        }

        public static void SetStateMachine(
            ref TStateMachine stateMachine,
            ref IStateMachineRunner runnerFieldRef)
        {
            if (!pool.TryPop(out var result))
            {
                result = new AsyncAppaTaskVoid<TStateMachine>();
            }

            TaskTracker.TrackActiveTask(result, 3);

            runnerFieldRef = result;            // set runner before copied.
            result.stateMachine = stateMachine; // copy struct StateMachine(in release build).
        }

        static AsyncAppaTaskVoid()
        {
            TaskPool.RegisterSizeGetter(typeof(AsyncAppaTaskVoid<TStateMachine>), () => pool.Size);
        }

        private AsyncAppaTaskVoid<TStateMachine> nextNode;
        public ref AsyncAppaTaskVoid<TStateMachine> NextNode => ref nextNode;

        public void Return()
        {
            TaskTracker.RemoveTracking(this);
            stateMachine = default;
            pool.TryPush(this);
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Run()
        {
            stateMachine.MoveNext();
        }

        // dummy interface implementation for TaskTracker.

        AppaTaskStatus IAppaTaskSource.GetStatus(short token)
        {
            return AppaTaskStatus.Pending;
        }

        AppaTaskStatus IAppaTaskSource.UnsafeGetStatus()
        {
            return AppaTaskStatus.Pending;
        }

        void IAppaTaskSource.OnCompleted(Action<object> continuation, object state, short token)
        {
        }

        void IAppaTaskSource.GetResult(short token)
        {
        }
    }

    internal sealed class AsyncAppaTask<TStateMachine> : IStateMachineRunnerPromise,
                                                         IAppaTaskSource,
                                                         ITaskPoolNode<AsyncAppaTask<TStateMachine>>
        where TStateMachine : IAsyncStateMachine
    {
        private static TaskPool<AsyncAppaTask<TStateMachine>> pool;

#if ENABLE_IL2CPP
        readonly Action returnDelegate;
#endif
        public Action MoveNext { get; }

        private TStateMachine stateMachine;
        private AppaTaskCompletionSourceCore<AsyncUnit> core;

        private AsyncAppaTask()
        {
            MoveNext = Run;
#if ENABLE_IL2CPP
            returnDelegate = Return;
#endif
        }

        public static void SetStateMachine(
            ref TStateMachine stateMachine,
            ref IStateMachineRunnerPromise runnerPromiseFieldRef)
        {
            if (!pool.TryPop(out var result))
            {
                result = new AsyncAppaTask<TStateMachine>();
            }

            TaskTracker.TrackActiveTask(result, 3);

            runnerPromiseFieldRef = result;     // set runner before copied.
            result.stateMachine = stateMachine; // copy struct StateMachine(in release build).
        }

        private AsyncAppaTask<TStateMachine> nextNode;
        public ref AsyncAppaTask<TStateMachine> NextNode => ref nextNode;

        static AsyncAppaTask()
        {
            TaskPool.RegisterSizeGetter(typeof(AsyncAppaTask<TStateMachine>), () => pool.Size);
        }

        private void Return()
        {
            TaskTracker.RemoveTracking(this);
            core.Reset();
            stateMachine = default;
            pool.TryPush(this);
        }

        private bool TryReturn()
        {
            TaskTracker.RemoveTracking(this);
            core.Reset();
            stateMachine = default;
            return pool.TryPush(this);
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Run()
        {
            stateMachine.MoveNext();
        }

        public AppaTask Task
        {
            [DebuggerHidden] get => new AppaTask(this, core.Version);
        }

        [DebuggerHidden]
        public void SetResult()
        {
            core.TrySetResult(AsyncUnit.Default);
        }

        [DebuggerHidden]
        public void SetException(Exception exception)
        {
            core.TrySetException(exception);
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
#if ENABLE_IL2CPP
                // workaround for IL2CPP bug.
                PlayerLoopHelper.AddContinuation(PlayerLoopTiming.LastPostLateUpdate, returnDelegate);
#else
                TryReturn();
#endif
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
    }

    internal sealed class AsyncAppaTask<TStateMachine, T> : IStateMachineRunnerPromise<T>,
                                                            IAppaTaskSource<T>,
                                                            ITaskPoolNode<AsyncAppaTask<TStateMachine, T>>
        where TStateMachine : IAsyncStateMachine
    {
        private static TaskPool<AsyncAppaTask<TStateMachine, T>> pool;

#if ENABLE_IL2CPP
        readonly Action returnDelegate;
#endif

        public Action MoveNext { get; }

        private TStateMachine stateMachine;
        private AppaTaskCompletionSourceCore<T> core;

        private AsyncAppaTask()
        {
            MoveNext = Run;
#if ENABLE_IL2CPP
            returnDelegate = Return;
#endif
        }

        public static void SetStateMachine(
            ref TStateMachine stateMachine,
            ref IStateMachineRunnerPromise<T> runnerPromiseFieldRef)
        {
            if (!pool.TryPop(out var result))
            {
                result = new AsyncAppaTask<TStateMachine, T>();
            }

            TaskTracker.TrackActiveTask(result, 3);

            runnerPromiseFieldRef = result;     // set runner before copied.
            result.stateMachine = stateMachine; // copy struct StateMachine(in release build).
        }

        private AsyncAppaTask<TStateMachine, T> nextNode;
        public ref AsyncAppaTask<TStateMachine, T> NextNode => ref nextNode;

        static AsyncAppaTask()
        {
            TaskPool.RegisterSizeGetter(typeof(AsyncAppaTask<TStateMachine, T>), () => pool.Size);
        }

        private void Return()
        {
            TaskTracker.RemoveTracking(this);
            core.Reset();
            stateMachine = default;
            pool.TryPush(this);
        }

        private bool TryReturn()
        {
            TaskTracker.RemoveTracking(this);
            core.Reset();
            stateMachine = default;
            return pool.TryPush(this);
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Run()
        {
            // UnityEngine.Debug.Log($"MoveNext State:" + StateMachineUtility.GetState(stateMachine));
            stateMachine.MoveNext();
        }

        public AppaTask<T> Task
        {
            [DebuggerHidden] get => new AppaTask<T>(this, core.Version);
        }

        [DebuggerHidden]
        public void SetResult(T result)
        {
            core.TrySetResult(result);
        }

        [DebuggerHidden]
        public void SetException(Exception exception)
        {
            core.TrySetException(exception);
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
#if ENABLE_IL2CPP
                // workaround for IL2CPP bug.
                PlayerLoopHelper.AddContinuation(PlayerLoopTiming.LastPostLateUpdate, returnDelegate);
#else
                TryReturn();
#endif
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
    }
}
