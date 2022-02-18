using System;
using System.Threading;

namespace Appalachia.Utility.Async
{
    public interface IReadOnlyAsyncReactiveProperty<T> : IAppaTaskAsyncEnumerable<T>
    {
        T Value { get; }
        AppaTask<T> WaitAsync(CancellationToken cancellationToken = default);
        IAppaTaskAsyncEnumerable<T> WithoutCurrent();
    }

    public interface IAsyncReactiveProperty<T> : IReadOnlyAsyncReactiveProperty<T>
    {
        new T Value { get; set; }
    }

    [Serializable]
    public class AsyncReactiveProperty<T> : IAsyncReactiveProperty<T>, IDisposable
    {
        static AsyncReactiveProperty()
        {
            isValueType = typeof(T).IsValueType;
        }

        public AsyncReactiveProperty(T value)
        {
            latestValue = value;
            triggerEvent = default;
        }

        #region Static Fields and Autoproperties

        private static bool isValueType;

        #endregion

        #region Fields and Autoproperties

#if UNITY_2018_3_OR_NEWER
        [UnityEngine.SerializeField]
#endif
        private T latestValue;

        private TriggerEvent<T> triggerEvent;

        #endregion

        public static implicit operator T(AsyncReactiveProperty<T> value)
        {
            return value.Value;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            if (isValueType)
            {
                return latestValue.ToString();
            }

            return latestValue?.ToString();
        }

        #region IAsyncReactiveProperty<T> Members

        public T Value
        {
            get => latestValue;
            set
            {
                latestValue = value;
                triggerEvent.SetResult(value);
            }
        }

        public IAppaTaskAsyncEnumerable<T> WithoutCurrent()
        {
            return new WithoutCurrentEnumerable(this);
        }

        public IAppaTaskAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken)
        {
            return new Enumerator(this, cancellationToken, true);
        }

        public AppaTask<T> WaitAsync(CancellationToken cancellationToken = default)
        {
            return new AppaTask<T>(WaitAsyncSource.Create(this, cancellationToken, out var token), token);
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            triggerEvent.SetCompleted();
        }

        #endregion

        #region Nested type: Enumerator

        private sealed class Enumerator : MoveNextSource, IAppaTaskAsyncEnumerator<T>, ITriggerHandler<T>
        {
            public Enumerator(
                AsyncReactiveProperty<T> parent,
                CancellationToken cancellationToken,
                bool publishCurrentValue)
            {
                this.parent = parent;
                this.cancellationToken = cancellationToken;
                firstCall = publishCurrentValue;

                parent.triggerEvent.Add(this);
                TaskTracker.TrackActiveTask(this, 3);

                if (cancellationToken.CanBeCanceled)
                {
                    cancellationTokenRegistration =
                        cancellationToken.RegisterWithoutCaptureExecutionContext(cancellationCallback, this);
                }
            }

            #region Static Fields and Autoproperties

            private static Action<object> cancellationCallback = CancellationCallback;

            #endregion

            #region Fields and Autoproperties

            private readonly AsyncReactiveProperty<T> parent;
            private readonly CancellationToken cancellationToken;
            private readonly CancellationTokenRegistration cancellationTokenRegistration;
            private bool firstCall;
            private bool isDisposed;
            private T value;

            #endregion

            private static void CancellationCallback(object state)
            {
                var self = (Enumerator)state;
                self.DisposeAsync().Forget();
            }

            #region IAppaTaskAsyncEnumerator<T> Members

            public T Current => value;

            public AppaTask<bool> MoveNextAsync()
            {
                // raise latest value on first call.
                if (firstCall)
                {
                    firstCall = false;
                    value = parent.Value;
                    return CompletedTasks.True;
                }

                completionSource.Reset();
                return new AppaTask<bool>(this, completionSource.Version);
            }

            public AppaTask DisposeAsync()
            {
                if (!isDisposed)
                {
                    isDisposed = true;
                    TaskTracker.RemoveTracking(this);
                    completionSource.TrySetCanceled(cancellationToken);
                    parent.triggerEvent.Remove(this);
                }

                return default;
            }

            #endregion

            #region ITriggerHandler<T> Members

            ITriggerHandler<T> ITriggerHandler<T>.Prev { get; set; }
            ITriggerHandler<T> ITriggerHandler<T>.Next { get; set; }

            public void OnNext(T value)
            {
                this.value = value;
                completionSource.TrySetResult(true);
            }

            public void OnCanceled(CancellationToken cancellationToken)
            {
                DisposeAsync().Forget();
            }

            public void OnCompleted()
            {
                completionSource.TrySetResult(false);
            }

            public void OnError(Exception ex)
            {
                completionSource.TrySetException(ex);
            }

            #endregion
        }

        #endregion

        #region Nested type: WaitAsyncSource

        private sealed class WaitAsyncSource : IAppaTaskSource<T>,
                                               ITriggerHandler<T>,
                                               ITaskPoolNode<WaitAsyncSource>
        {
            static WaitAsyncSource()
            {
                TaskPool.RegisterSizeGetter(typeof(WaitAsyncSource), () => pool.Size);
            }

            private WaitAsyncSource()
            {
            }

            #region Static Fields and Autoproperties

            private static Action<object> cancellationCallback = CancellationCallback;

            private static TaskPool<WaitAsyncSource> pool;

            #endregion

            #region Fields and Autoproperties

            private AppaTaskCompletionSourceCore<T> core;

            private AsyncReactiveProperty<T> parent;
            private WaitAsyncSource nextNode;
            private CancellationToken cancellationToken;
            private CancellationTokenRegistration cancellationTokenRegistration;

            #endregion

            public static IAppaTaskSource<T> Create(
                AsyncReactiveProperty<T> parent,
                CancellationToken cancellationToken,
                out short token)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return AutoResetAppaTaskCompletionSource<T>.CreateFromCanceled(
                        cancellationToken,
                        out token
                    );
                }

                if (!pool.TryPop(out var result))
                {
                    result = new WaitAsyncSource();
                }

                result.parent = parent;
                result.cancellationToken = cancellationToken;

                if (cancellationToken.CanBeCanceled)
                {
                    result.cancellationTokenRegistration =
                        cancellationToken.RegisterWithoutCaptureExecutionContext(
                            cancellationCallback,
                            result
                        );
                }

                result.parent.triggerEvent.Add(result);

                TaskTracker.TrackActiveTask(result, 3);

                token = result.core.Version;
                return result;
            }

            private static void CancellationCallback(object state)
            {
                var self = (WaitAsyncSource)state;
                self.OnCanceled(self.cancellationToken);
            }

            private bool TryReturn()
            {
                TaskTracker.RemoveTracking(this);
                core.Reset();
                cancellationTokenRegistration.Dispose();
                cancellationTokenRegistration = default;
                parent.triggerEvent.Remove(this);
                parent = null;
                cancellationToken = default;
                return pool.TryPush(this);
            }

            #region IAppaTaskSource<T> Members

            // IAppaTaskSource

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

            void IAppaTaskSource.GetResult(short token)
            {
                GetResult(token);
            }

            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                core.OnCompleted(continuation, state, token);
            }

            public AppaTaskStatus GetStatus(short token)
            {
                return core.GetStatus(token);
            }

            public AppaTaskStatus UnsafeGetStatus()
            {
                return core.UnsafeGetStatus();
            }

            #endregion

            #region ITaskPoolNode<AsyncReactiveProperty<T>.WaitAsyncSource> Members

            ref WaitAsyncSource ITaskPoolNode<WaitAsyncSource>.NextNode => ref nextNode;

            #endregion

            #region ITriggerHandler<T> Members

            // ITriggerHandler

            ITriggerHandler<T> ITriggerHandler<T>.Prev { get; set; }
            ITriggerHandler<T> ITriggerHandler<T>.Next { get; set; }

            public void OnCanceled(CancellationToken cancellationToken)
            {
                core.TrySetCanceled(cancellationToken);
            }

            public void OnCompleted()
            {
                // Complete as Cancel.
                core.TrySetCanceled(CancellationToken.None);
            }

            public void OnError(Exception ex)
            {
                core.TrySetException(ex);
            }

            public void OnNext(T value)
            {
                core.TrySetResult(value);
            }

            #endregion
        }

        #endregion

        #region Nested type: WithoutCurrentEnumerable

        private sealed class WithoutCurrentEnumerable : IAppaTaskAsyncEnumerable<T>
        {
            public WithoutCurrentEnumerable(AsyncReactiveProperty<T> parent)
            {
                this.parent = parent;
            }

            #region Fields and Autoproperties

            private readonly AsyncReactiveProperty<T> parent;

            #endregion

            #region IAppaTaskAsyncEnumerable<T> Members

            public IAppaTaskAsyncEnumerator<T> GetAsyncEnumerator(
                CancellationToken cancellationToken = default)
            {
                return new Enumerator(parent, cancellationToken, false);
            }

            #endregion
        }

        #endregion
    }

    public class ReadOnlyAsyncReactiveProperty<T> : IReadOnlyAsyncReactiveProperty<T>, IDisposable
    {
        static ReadOnlyAsyncReactiveProperty()
        {
            isValueType = typeof(T).IsValueType;
        }

        public ReadOnlyAsyncReactiveProperty(
            T initialValue,
            IAppaTaskAsyncEnumerable<T> source,
            CancellationToken cancellationToken)
        {
            latestValue = initialValue;
            ConsumeEnumerator(source, cancellationToken).Forget();
        }

        public ReadOnlyAsyncReactiveProperty(
            IAppaTaskAsyncEnumerable<T> source,
            CancellationToken cancellationToken)
        {
            ConsumeEnumerator(source, cancellationToken).Forget();
        }

        #region Static Fields and Autoproperties

        private static bool isValueType;

        #endregion

        #region Fields and Autoproperties

        private IAppaTaskAsyncEnumerator<T> enumerator;

        private T latestValue;
        private TriggerEvent<T> triggerEvent;

        #endregion

        public static implicit operator T(ReadOnlyAsyncReactiveProperty<T> value)
        {
            return value.Value;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            if (isValueType)
            {
                return latestValue.ToString();
            }

            return latestValue?.ToString();
        }

        private async AppaTaskVoid ConsumeEnumerator(
            IAppaTaskAsyncEnumerable<T> source,
            CancellationToken cancellationToken)
        {
            enumerator = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await enumerator.MoveNextAsync())
                {
                    var value = enumerator.Current;
                    latestValue = value;
                    triggerEvent.SetResult(value);
                }
            }
            finally
            {
                await enumerator.DisposeAsync();
                enumerator = null;
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (enumerator != null)
            {
                enumerator.DisposeAsync().Forget();
            }

            triggerEvent.SetCompleted();
        }

        #endregion

        #region IReadOnlyAsyncReactiveProperty<T> Members

        public T Value => latestValue;

        public IAppaTaskAsyncEnumerable<T> WithoutCurrent()
        {
            return new WithoutCurrentEnumerable(this);
        }

        public IAppaTaskAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken)
        {
            return new Enumerator(this, cancellationToken, true);
        }

        public AppaTask<T> WaitAsync(CancellationToken cancellationToken = default)
        {
            return new AppaTask<T>(WaitAsyncSource.Create(this, cancellationToken, out var token), token);
        }

        #endregion

        #region Nested type: Enumerator

        private sealed class Enumerator : MoveNextSource, IAppaTaskAsyncEnumerator<T>, ITriggerHandler<T>
        {
            public Enumerator(
                ReadOnlyAsyncReactiveProperty<T> parent,
                CancellationToken cancellationToken,
                bool publishCurrentValue)
            {
                this.parent = parent;
                this.cancellationToken = cancellationToken;
                firstCall = publishCurrentValue;

                parent.triggerEvent.Add(this);
                TaskTracker.TrackActiveTask(this, 3);

                if (cancellationToken.CanBeCanceled)
                {
                    cancellationTokenRegistration =
                        cancellationToken.RegisterWithoutCaptureExecutionContext(cancellationCallback, this);
                }
            }

            #region Static Fields and Autoproperties

            private static Action<object> cancellationCallback = CancellationCallback;

            #endregion

            #region Fields and Autoproperties

            private readonly CancellationToken cancellationToken;
            private readonly CancellationTokenRegistration cancellationTokenRegistration;

            private readonly ReadOnlyAsyncReactiveProperty<T> parent;
            private bool firstCall;
            private bool isDisposed;
            private T value;

            #endregion

            private static void CancellationCallback(object state)
            {
                var self = (Enumerator)state;
                self.DisposeAsync().Forget();
            }

            #region IAppaTaskAsyncEnumerator<T> Members

            public T Current => value;

            public AppaTask<bool> MoveNextAsync()
            {
                // raise latest value on first call.
                if (firstCall)
                {
                    firstCall = false;
                    value = parent.Value;
                    return CompletedTasks.True;
                }

                completionSource.Reset();
                return new AppaTask<bool>(this, completionSource.Version);
            }

            public AppaTask DisposeAsync()
            {
                if (!isDisposed)
                {
                    isDisposed = true;
                    TaskTracker.RemoveTracking(this);
                    completionSource.TrySetCanceled(cancellationToken);
                    parent.triggerEvent.Remove(this);
                }

                return default;
            }

            #endregion

            #region ITriggerHandler<T> Members

            ITriggerHandler<T> ITriggerHandler<T>.Prev { get; set; }
            ITriggerHandler<T> ITriggerHandler<T>.Next { get; set; }

            public void OnNext(T value)
            {
                this.value = value;
                completionSource.TrySetResult(true);
            }

            public void OnCanceled(CancellationToken cancellationToken)
            {
                DisposeAsync().Forget();
            }

            public void OnCompleted()
            {
                completionSource.TrySetResult(false);
            }

            public void OnError(Exception ex)
            {
                completionSource.TrySetException(ex);
            }

            #endregion
        }

        #endregion

        #region Nested type: WaitAsyncSource

        private sealed class WaitAsyncSource : IAppaTaskSource<T>,
                                               ITriggerHandler<T>,
                                               ITaskPoolNode<WaitAsyncSource>
        {
            static WaitAsyncSource()
            {
                TaskPool.RegisterSizeGetter(typeof(WaitAsyncSource), () => pool.Size);
            }

            private WaitAsyncSource()
            {
            }

            #region Static Fields and Autoproperties

            private static Action<object> cancellationCallback = CancellationCallback;

            private static TaskPool<WaitAsyncSource> pool;

            #endregion

            #region Fields and Autoproperties

            private AppaTaskCompletionSourceCore<T> core;
            private CancellationToken cancellationToken;
            private CancellationTokenRegistration cancellationTokenRegistration;

            private ReadOnlyAsyncReactiveProperty<T> parent;
            private WaitAsyncSource nextNode;

            #endregion

            public static IAppaTaskSource<T> Create(
                ReadOnlyAsyncReactiveProperty<T> parent,
                CancellationToken cancellationToken,
                out short token)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return AutoResetAppaTaskCompletionSource<T>.CreateFromCanceled(
                        cancellationToken,
                        out token
                    );
                }

                if (!pool.TryPop(out var result))
                {
                    result = new WaitAsyncSource();
                }

                result.parent = parent;
                result.cancellationToken = cancellationToken;

                if (cancellationToken.CanBeCanceled)
                {
                    result.cancellationTokenRegistration =
                        cancellationToken.RegisterWithoutCaptureExecutionContext(
                            cancellationCallback,
                            result
                        );
                }

                result.parent.triggerEvent.Add(result);

                TaskTracker.TrackActiveTask(result, 3);

                token = result.core.Version;
                return result;
            }

            private static void CancellationCallback(object state)
            {
                var self = (WaitAsyncSource)state;
                self.OnCanceled(self.cancellationToken);
            }

            private bool TryReturn()
            {
                TaskTracker.RemoveTracking(this);
                core.Reset();
                cancellationTokenRegistration.Dispose();
                cancellationTokenRegistration = default;
                parent.triggerEvent.Remove(this);
                parent = null;
                cancellationToken = default;
                return pool.TryPush(this);
            }

            #region IAppaTaskSource<T> Members

            // IAppaTaskSource

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

            void IAppaTaskSource.GetResult(short token)
            {
                GetResult(token);
            }

            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                core.OnCompleted(continuation, state, token);
            }

            public AppaTaskStatus GetStatus(short token)
            {
                return core.GetStatus(token);
            }

            public AppaTaskStatus UnsafeGetStatus()
            {
                return core.UnsafeGetStatus();
            }

            #endregion

            #region ITaskPoolNode<ReadOnlyAsyncReactiveProperty<T>.WaitAsyncSource> Members

            ref WaitAsyncSource ITaskPoolNode<WaitAsyncSource>.NextNode => ref nextNode;

            #endregion

            #region ITriggerHandler<T> Members

            // ITriggerHandler

            ITriggerHandler<T> ITriggerHandler<T>.Prev { get; set; }
            ITriggerHandler<T> ITriggerHandler<T>.Next { get; set; }

            public void OnCanceled(CancellationToken cancellationToken)
            {
                core.TrySetCanceled(cancellationToken);
            }

            public void OnCompleted()
            {
                // Complete as Cancel.
                core.TrySetCanceled(CancellationToken.None);
            }

            public void OnError(Exception ex)
            {
                core.TrySetException(ex);
            }

            public void OnNext(T value)
            {
                core.TrySetResult(value);
            }

            #endregion
        }

        #endregion

        #region Nested type: WithoutCurrentEnumerable

        private sealed class WithoutCurrentEnumerable : IAppaTaskAsyncEnumerable<T>
        {
            public WithoutCurrentEnumerable(ReadOnlyAsyncReactiveProperty<T> parent)
            {
                this.parent = parent;
            }

            #region Fields and Autoproperties

            private readonly ReadOnlyAsyncReactiveProperty<T> parent;

            #endregion

            #region IAppaTaskAsyncEnumerable<T> Members

            public IAppaTaskAsyncEnumerator<T> GetAsyncEnumerator(
                CancellationToken cancellationToken = default)
            {
                return new Enumerator(parent, cancellationToken, false);
            }

            #endregion
        }

        #endregion
    }

    public static class StateExtensions
    {
        public static ReadOnlyAsyncReactiveProperty<T> ToReadOnlyAsyncReactiveProperty<T>(
            this IAppaTaskAsyncEnumerable<T> source,
            CancellationToken cancellationToken)
        {
            return new ReadOnlyAsyncReactiveProperty<T>(source, cancellationToken);
        }

        public static ReadOnlyAsyncReactiveProperty<T> ToReadOnlyAsyncReactiveProperty<T>(
            this IAppaTaskAsyncEnumerable<T> source,
            T initialValue,
            CancellationToken cancellationToken)
        {
            return new ReadOnlyAsyncReactiveProperty<T>(initialValue, source, cancellationToken);
        }
    }
}
