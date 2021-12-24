#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#if !UNITY_2019_1_OR_NEWER || APPATASK_UGUI_SUPPORT
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Appalachia.Utility.Async
{
    public static partial class UnityAsyncExtensions
    {
        public static AsyncUnityEventHandler GetAsyncEventHandler(
            this UnityEvent unityEvent,
            CancellationToken cancellationToken)
        {
            return new AsyncUnityEventHandler(unityEvent, cancellationToken, false);
        }

        public static AppaTask OnInvokeAsync(this UnityEvent unityEvent, CancellationToken cancellationToken)
        {
            return new AsyncUnityEventHandler(unityEvent, cancellationToken, true).OnInvokeAsync();
        }

        public static IAppaTaskAsyncEnumerable<AsyncUnit> OnInvokeAsAsyncEnumerable(
            this UnityEvent unityEvent,
            CancellationToken cancellationToken)
        {
            return new UnityEventHandlerAsyncEnumerable(unityEvent, cancellationToken);
        }

        public static AsyncUnityEventHandler<T> GetAsyncEventHandler<T>(
            this UnityEvent<T> unityEvent,
            CancellationToken cancellationToken)
        {
            return new AsyncUnityEventHandler<T>(unityEvent, cancellationToken, false);
        }

        public static AppaTask<T> OnInvokeAsync<T>(
            this UnityEvent<T> unityEvent,
            CancellationToken cancellationToken)
        {
            return new AsyncUnityEventHandler<T>(unityEvent, cancellationToken, true).OnInvokeAsync();
        }

        public static IAppaTaskAsyncEnumerable<T> OnInvokeAsAsyncEnumerable<T>(
            this UnityEvent<T> unityEvent,
            CancellationToken cancellationToken)
        {
            return new UnityEventHandlerAsyncEnumerable<T>(unityEvent, cancellationToken);
        }

        public static IAsyncClickEventHandler GetAsyncClickEventHandler(this Button button)
        {
            return new AsyncUnityEventHandler(button.onClick, button.GetCancellationTokenOnDestroy(), false);
        }

        public static IAsyncClickEventHandler GetAsyncClickEventHandler(
            this Button button,
            CancellationToken cancellationToken)
        {
            return new AsyncUnityEventHandler(button.onClick, cancellationToken, false);
        }

        public static AppaTask OnClickAsync(this Button button)
        {
            return new AsyncUnityEventHandler(button.onClick, button.GetCancellationTokenOnDestroy(), true)
               .OnInvokeAsync();
        }

        public static AppaTask OnClickAsync(this Button button, CancellationToken cancellationToken)
        {
            return new AsyncUnityEventHandler(button.onClick, cancellationToken, true).OnInvokeAsync();
        }

        public static IAppaTaskAsyncEnumerable<AsyncUnit> OnClickAsAsyncEnumerable(this Button button)
        {
            return new UnityEventHandlerAsyncEnumerable(
                button.onClick,
                button.GetCancellationTokenOnDestroy()
            );
        }

        public static IAppaTaskAsyncEnumerable<AsyncUnit> OnClickAsAsyncEnumerable(
            this Button button,
            CancellationToken cancellationToken)
        {
            return new UnityEventHandlerAsyncEnumerable(button.onClick, cancellationToken);
        }

        public static IAsyncValueChangedEventHandler<bool> GetAsyncValueChangedEventHandler(
            this Toggle toggle)
        {
            return new AsyncUnityEventHandler<bool>(
                toggle.onValueChanged,
                toggle.GetCancellationTokenOnDestroy(),
                false
            );
        }

        public static IAsyncValueChangedEventHandler<bool> GetAsyncValueChangedEventHandler(
            this Toggle toggle,
            CancellationToken cancellationToken)
        {
            return new AsyncUnityEventHandler<bool>(toggle.onValueChanged, cancellationToken, false);
        }

        public static AppaTask<bool> OnValueChangedAsync(this Toggle toggle)
        {
            return new AsyncUnityEventHandler<bool>(
                toggle.onValueChanged,
                toggle.GetCancellationTokenOnDestroy(),
                true
            ).OnInvokeAsync();
        }

        public static AppaTask<bool> OnValueChangedAsync(
            this Toggle toggle,
            CancellationToken cancellationToken)
        {
            return new AsyncUnityEventHandler<bool>(toggle.onValueChanged, cancellationToken, true)
               .OnInvokeAsync();
        }

        public static IAppaTaskAsyncEnumerable<bool> OnValueChangedAsAsyncEnumerable(this Toggle toggle)
        {
            return new UnityEventHandlerAsyncEnumerable<bool>(
                toggle.onValueChanged,
                toggle.GetCancellationTokenOnDestroy()
            );
        }

        public static IAppaTaskAsyncEnumerable<bool> OnValueChangedAsAsyncEnumerable(
            this Toggle toggle,
            CancellationToken cancellationToken)
        {
            return new UnityEventHandlerAsyncEnumerable<bool>(toggle.onValueChanged, cancellationToken);
        }

        public static IAsyncValueChangedEventHandler<float> GetAsyncValueChangedEventHandler(
            this Scrollbar scrollbar)
        {
            return new AsyncUnityEventHandler<float>(
                scrollbar.onValueChanged,
                scrollbar.GetCancellationTokenOnDestroy(),
                false
            );
        }

        public static IAsyncValueChangedEventHandler<float> GetAsyncValueChangedEventHandler(
            this Scrollbar scrollbar,
            CancellationToken cancellationToken)
        {
            return new AsyncUnityEventHandler<float>(scrollbar.onValueChanged, cancellationToken, false);
        }

        public static AppaTask<float> OnValueChangedAsync(this Scrollbar scrollbar)
        {
            return new AsyncUnityEventHandler<float>(
                scrollbar.onValueChanged,
                scrollbar.GetCancellationTokenOnDestroy(),
                true
            ).OnInvokeAsync();
        }

        public static AppaTask<float> OnValueChangedAsync(
            this Scrollbar scrollbar,
            CancellationToken cancellationToken)
        {
            return new AsyncUnityEventHandler<float>(scrollbar.onValueChanged, cancellationToken, true)
               .OnInvokeAsync();
        }

        public static IAppaTaskAsyncEnumerable<float> OnValueChangedAsAsyncEnumerable(
            this Scrollbar scrollbar)
        {
            return new UnityEventHandlerAsyncEnumerable<float>(
                scrollbar.onValueChanged,
                scrollbar.GetCancellationTokenOnDestroy()
            );
        }

        public static IAppaTaskAsyncEnumerable<float> OnValueChangedAsAsyncEnumerable(
            this Scrollbar scrollbar,
            CancellationToken cancellationToken)
        {
            return new UnityEventHandlerAsyncEnumerable<float>(scrollbar.onValueChanged, cancellationToken);
        }

        public static IAsyncValueChangedEventHandler<Vector2> GetAsyncValueChangedEventHandler(
            this ScrollRect scrollRect)
        {
            return new AsyncUnityEventHandler<Vector2>(
                scrollRect.onValueChanged,
                scrollRect.GetCancellationTokenOnDestroy(),
                false
            );
        }

        public static IAsyncValueChangedEventHandler<Vector2> GetAsyncValueChangedEventHandler(
            this ScrollRect scrollRect,
            CancellationToken cancellationToken)
        {
            return new AsyncUnityEventHandler<Vector2>(scrollRect.onValueChanged, cancellationToken, false);
        }

        public static AppaTask<Vector2> OnValueChangedAsync(this ScrollRect scrollRect)
        {
            return new AsyncUnityEventHandler<Vector2>(
                scrollRect.onValueChanged,
                scrollRect.GetCancellationTokenOnDestroy(),
                true
            ).OnInvokeAsync();
        }

        public static AppaTask<Vector2> OnValueChangedAsync(
            this ScrollRect scrollRect,
            CancellationToken cancellationToken)
        {
            return new AsyncUnityEventHandler<Vector2>(scrollRect.onValueChanged, cancellationToken, true)
               .OnInvokeAsync();
        }

        public static IAppaTaskAsyncEnumerable<Vector2> OnValueChangedAsAsyncEnumerable(
            this ScrollRect scrollRect)
        {
            return new UnityEventHandlerAsyncEnumerable<Vector2>(
                scrollRect.onValueChanged,
                scrollRect.GetCancellationTokenOnDestroy()
            );
        }

        public static IAppaTaskAsyncEnumerable<Vector2> OnValueChangedAsAsyncEnumerable(
            this ScrollRect scrollRect,
            CancellationToken cancellationToken)
        {
            return new UnityEventHandlerAsyncEnumerable<Vector2>(
                scrollRect.onValueChanged,
                cancellationToken
            );
        }

        public static IAsyncValueChangedEventHandler<float> GetAsyncValueChangedEventHandler(
            this Slider slider)
        {
            return new AsyncUnityEventHandler<float>(
                slider.onValueChanged,
                slider.GetCancellationTokenOnDestroy(),
                false
            );
        }

        public static IAsyncValueChangedEventHandler<float> GetAsyncValueChangedEventHandler(
            this Slider slider,
            CancellationToken cancellationToken)
        {
            return new AsyncUnityEventHandler<float>(slider.onValueChanged, cancellationToken, false);
        }

        public static AppaTask<float> OnValueChangedAsync(this Slider slider)
        {
            return new AsyncUnityEventHandler<float>(
                slider.onValueChanged,
                slider.GetCancellationTokenOnDestroy(),
                true
            ).OnInvokeAsync();
        }

        public static AppaTask<float> OnValueChangedAsync(
            this Slider slider,
            CancellationToken cancellationToken)
        {
            return new AsyncUnityEventHandler<float>(slider.onValueChanged, cancellationToken, true)
               .OnInvokeAsync();
        }

        public static IAppaTaskAsyncEnumerable<float> OnValueChangedAsAsyncEnumerable(this Slider slider)
        {
            return new UnityEventHandlerAsyncEnumerable<float>(
                slider.onValueChanged,
                slider.GetCancellationTokenOnDestroy()
            );
        }

        public static IAppaTaskAsyncEnumerable<float> OnValueChangedAsAsyncEnumerable(
            this Slider slider,
            CancellationToken cancellationToken)
        {
            return new UnityEventHandlerAsyncEnumerable<float>(slider.onValueChanged, cancellationToken);
        }

        public static IAsyncEndEditEventHandler<string> GetAsyncEndEditEventHandler(
            this InputField inputField)
        {
            return new AsyncUnityEventHandler<string>(
                inputField.onEndEdit,
                inputField.GetCancellationTokenOnDestroy(),
                false
            );
        }

        public static IAsyncEndEditEventHandler<string> GetAsyncEndEditEventHandler(
            this InputField inputField,
            CancellationToken cancellationToken)
        {
            return new AsyncUnityEventHandler<string>(inputField.onEndEdit, cancellationToken, false);
        }

        public static AppaTask<string> OnEndEditAsync(this InputField inputField)
        {
            return new AsyncUnityEventHandler<string>(
                inputField.onEndEdit,
                inputField.GetCancellationTokenOnDestroy(),
                true
            ).OnInvokeAsync();
        }

        public static AppaTask<string> OnEndEditAsync(
            this InputField inputField,
            CancellationToken cancellationToken)
        {
            return new AsyncUnityEventHandler<string>(inputField.onEndEdit, cancellationToken, true)
               .OnInvokeAsync();
        }

        public static IAppaTaskAsyncEnumerable<string> OnEndEditAsAsyncEnumerable(this InputField inputField)
        {
            return new UnityEventHandlerAsyncEnumerable<string>(
                inputField.onEndEdit,
                inputField.GetCancellationTokenOnDestroy()
            );
        }

        public static IAppaTaskAsyncEnumerable<string> OnEndEditAsAsyncEnumerable(
            this InputField inputField,
            CancellationToken cancellationToken)
        {
            return new UnityEventHandlerAsyncEnumerable<string>(inputField.onEndEdit, cancellationToken);
        }

        public static IAsyncValueChangedEventHandler<string> GetAsyncValueChangedEventHandler(
            this InputField inputField)
        {
            return new AsyncUnityEventHandler<string>(
                inputField.onValueChanged,
                inputField.GetCancellationTokenOnDestroy(),
                false
            );
        }

        public static IAsyncValueChangedEventHandler<string> GetAsyncValueChangedEventHandler(
            this InputField inputField,
            CancellationToken cancellationToken)
        {
            return new AsyncUnityEventHandler<string>(inputField.onValueChanged, cancellationToken, false);
        }

        public static AppaTask<string> OnValueChangedAsync(this InputField inputField)
        {
            return new AsyncUnityEventHandler<string>(
                inputField.onValueChanged,
                inputField.GetCancellationTokenOnDestroy(),
                true
            ).OnInvokeAsync();
        }

        public static AppaTask<string> OnValueChangedAsync(
            this InputField inputField,
            CancellationToken cancellationToken)
        {
            return new AsyncUnityEventHandler<string>(inputField.onValueChanged, cancellationToken, true)
               .OnInvokeAsync();
        }

        public static IAppaTaskAsyncEnumerable<string> OnValueChangedAsAsyncEnumerable(
            this InputField inputField)
        {
            return new UnityEventHandlerAsyncEnumerable<string>(
                inputField.onValueChanged,
                inputField.GetCancellationTokenOnDestroy()
            );
        }

        public static IAppaTaskAsyncEnumerable<string> OnValueChangedAsAsyncEnumerable(
            this InputField inputField,
            CancellationToken cancellationToken)
        {
            return new UnityEventHandlerAsyncEnumerable<string>(inputField.onValueChanged, cancellationToken);
        }

        public static IAsyncValueChangedEventHandler<int> GetAsyncValueChangedEventHandler(
            this Dropdown dropdown)
        {
            return new AsyncUnityEventHandler<int>(
                dropdown.onValueChanged,
                dropdown.GetCancellationTokenOnDestroy(),
                false
            );
        }

        public static IAsyncValueChangedEventHandler<int> GetAsyncValueChangedEventHandler(
            this Dropdown dropdown,
            CancellationToken cancellationToken)
        {
            return new AsyncUnityEventHandler<int>(dropdown.onValueChanged, cancellationToken, false);
        }

        public static AppaTask<int> OnValueChangedAsync(this Dropdown dropdown)
        {
            return new AsyncUnityEventHandler<int>(
                dropdown.onValueChanged,
                dropdown.GetCancellationTokenOnDestroy(),
                true
            ).OnInvokeAsync();
        }

        public static AppaTask<int> OnValueChangedAsync(
            this Dropdown dropdown,
            CancellationToken cancellationToken)
        {
            return new AsyncUnityEventHandler<int>(dropdown.onValueChanged, cancellationToken, true)
               .OnInvokeAsync();
        }

        public static IAppaTaskAsyncEnumerable<int> OnValueChangedAsAsyncEnumerable(this Dropdown dropdown)
        {
            return new UnityEventHandlerAsyncEnumerable<int>(
                dropdown.onValueChanged,
                dropdown.GetCancellationTokenOnDestroy()
            );
        }

        public static IAppaTaskAsyncEnumerable<int> OnValueChangedAsAsyncEnumerable(
            this Dropdown dropdown,
            CancellationToken cancellationToken)
        {
            return new UnityEventHandlerAsyncEnumerable<int>(dropdown.onValueChanged, cancellationToken);
        }
    }

    public interface IAsyncClickEventHandler : IDisposable
    {
        AppaTask OnClickAsync();
    }

    public interface IAsyncValueChangedEventHandler<T> : IDisposable
    {
        AppaTask<T> OnValueChangedAsync();
    }

    public interface IAsyncEndEditEventHandler<T> : IDisposable
    {
        AppaTask<T> OnEndEditAsync();
    }

    // for TMP_PRO

    public interface IAsyncEndTextSelectionEventHandler<T> : IDisposable
    {
        AppaTask<T> OnEndTextSelectionAsync();
    }

    public interface IAsyncTextSelectionEventHandler<T> : IDisposable
    {
        AppaTask<T> OnTextSelectionAsync();
    }

    public interface IAsyncDeselectEventHandler<T> : IDisposable
    {
        AppaTask<T> OnDeselectAsync();
    }

    public interface IAsyncSelectEventHandler<T> : IDisposable
    {
        AppaTask<T> OnSelectAsync();
    }

    public interface IAsyncSubmitEventHandler<T> : IDisposable
    {
        AppaTask<T> OnSubmitAsync();
    }

    internal class TextSelectionEventConverter : UnityEvent<(string, int, int)>, IDisposable
    {
        private readonly UnityEvent<string, int, int> innerEvent;
        private readonly UnityAction<string, int, int> invokeDelegate;

        public TextSelectionEventConverter(UnityEvent<string, int, int> unityEvent)
        {
            innerEvent = unityEvent;
            invokeDelegate = InvokeCore;

            innerEvent.AddListener(invokeDelegate);
        }

        private void InvokeCore(string item1, int item2, int item3)
        {
            innerEvent.Invoke(item1, item2, item3);
        }

        public void Dispose()
        {
            innerEvent.RemoveListener(invokeDelegate);
        }
    }

    public class AsyncUnityEventHandler : IAppaTaskSource, IDisposable, IAsyncClickEventHandler
    {
        private static Action<object> cancellationCallback = CancellationCallback;

        private readonly UnityAction action;
        private readonly UnityEvent unityEvent;

        private CancellationToken cancellationToken;
        private CancellationTokenRegistration registration;
        private bool isDisposed;
        private bool callOnce;

        private AppaTaskCompletionSourceCore<AsyncUnit> core;

        public AsyncUnityEventHandler(
            UnityEvent unityEvent,
            CancellationToken cancellationToken,
            bool callOnce)
        {
            this.cancellationToken = cancellationToken;
            if (cancellationToken.IsCancellationRequested)
            {
                isDisposed = true;
                return;
            }

            action = Invoke;
            this.unityEvent = unityEvent;
            this.callOnce = callOnce;

            unityEvent.AddListener(action);

            if (cancellationToken.CanBeCanceled)
            {
                registration =
                    cancellationToken.RegisterWithoutCaptureExecutionContext(cancellationCallback, this);
            }

            TaskTracker.TrackActiveTask(this, 3);
        }

        public AppaTask OnInvokeAsync()
        {
            core.Reset();
            if (isDisposed)
            {
                core.TrySetCanceled(cancellationToken);
            }

            return new AppaTask(this, core.Version);
        }

        private void Invoke()
        {
            core.TrySetResult(AsyncUnit.Default);
        }

        private static void CancellationCallback(object state)
        {
            var self = (AsyncUnityEventHandler)state;
            self.Dispose();
        }

        public void Dispose()
        {
            if (!isDisposed)
            {
                isDisposed = true;
                TaskTracker.RemoveTracking(this);
                registration.Dispose();
                if (unityEvent != null)
                {
                    unityEvent.RemoveListener(action);
                }

                core.TrySetCanceled(cancellationToken);
            }
        }

        AppaTask IAsyncClickEventHandler.OnClickAsync()
        {
            return OnInvokeAsync();
        }

        void IAppaTaskSource.GetResult(short token)
        {
            try
            {
                core.GetResult(token);
            }
            finally
            {
                if (callOnce)
                {
                    Dispose();
                }
            }
        }

        AppaTaskStatus IAppaTaskSource.GetStatus(short token)
        {
            return core.GetStatus(token);
        }

        AppaTaskStatus IAppaTaskSource.UnsafeGetStatus()
        {
            return core.UnsafeGetStatus();
        }

        void IAppaTaskSource.OnCompleted(Action<object> continuation, object state, short token)
        {
            core.OnCompleted(continuation, state, token);
        }
    }

    public class AsyncUnityEventHandler<T> : IAppaTaskSource<T>,
                                             IDisposable,
                                             IAsyncValueChangedEventHandler<T>,
                                             IAsyncEndEditEventHandler<T>,
                                             IAsyncEndTextSelectionEventHandler<T>,
                                             IAsyncTextSelectionEventHandler<T>,
                                             IAsyncDeselectEventHandler<T>,
                                             IAsyncSelectEventHandler<T>,
                                             IAsyncSubmitEventHandler<T>
    {
        private static Action<object> cancellationCallback = CancellationCallback;

        private readonly UnityAction<T> action;
        private readonly UnityEvent<T> unityEvent;

        private CancellationToken cancellationToken;
        private CancellationTokenRegistration registration;
        private bool isDisposed;
        private bool callOnce;

        private AppaTaskCompletionSourceCore<T> core;

        public AsyncUnityEventHandler(
            UnityEvent<T> unityEvent,
            CancellationToken cancellationToken,
            bool callOnce)
        {
            this.cancellationToken = cancellationToken;
            if (cancellationToken.IsCancellationRequested)
            {
                isDisposed = true;
                return;
            }

            action = Invoke;
            this.unityEvent = unityEvent;
            this.callOnce = callOnce;

            unityEvent.AddListener(action);

            if (cancellationToken.CanBeCanceled)
            {
                registration =
                    cancellationToken.RegisterWithoutCaptureExecutionContext(cancellationCallback, this);
            }

            TaskTracker.TrackActiveTask(this, 3);
        }

        public AppaTask<T> OnInvokeAsync()
        {
            core.Reset();
            if (isDisposed)
            {
                core.TrySetCanceled(cancellationToken);
            }

            return new AppaTask<T>(this, core.Version);
        }

        private void Invoke(T result)
        {
            core.TrySetResult(result);
        }

        private static void CancellationCallback(object state)
        {
            var self = (AsyncUnityEventHandler<T>)state;
            self.Dispose();
        }

        public void Dispose()
        {
            if (!isDisposed)
            {
                isDisposed = true;
                TaskTracker.RemoveTracking(this);
                registration.Dispose();
                if (unityEvent != null)
                {
                    // Dispose inner delegate for TextSelectionEventConverter
                    if (unityEvent is IDisposable disp)
                    {
                        disp.Dispose();
                    }

                    unityEvent.RemoveListener(action);
                }

                core.TrySetCanceled();
            }
        }

        AppaTask<T> IAsyncValueChangedEventHandler<T>.OnValueChangedAsync()
        {
            return OnInvokeAsync();
        }

        AppaTask<T> IAsyncEndEditEventHandler<T>.OnEndEditAsync()
        {
            return OnInvokeAsync();
        }

        AppaTask<T> IAsyncEndTextSelectionEventHandler<T>.OnEndTextSelectionAsync()
        {
            return OnInvokeAsync();
        }

        AppaTask<T> IAsyncTextSelectionEventHandler<T>.OnTextSelectionAsync()
        {
            return OnInvokeAsync();
        }

        AppaTask<T> IAsyncDeselectEventHandler<T>.OnDeselectAsync()
        {
            return OnInvokeAsync();
        }

        AppaTask<T> IAsyncSelectEventHandler<T>.OnSelectAsync()
        {
            return OnInvokeAsync();
        }

        AppaTask<T> IAsyncSubmitEventHandler<T>.OnSubmitAsync()
        {
            return OnInvokeAsync();
        }

        T IAppaTaskSource<T>.GetResult(short token)
        {
            try
            {
                return core.GetResult(token);
            }
            finally
            {
                if (callOnce)
                {
                    Dispose();
                }
            }
        }

        void IAppaTaskSource.GetResult(short token)
        {
            ((IAppaTaskSource<T>)this).GetResult(token);
        }

        AppaTaskStatus IAppaTaskSource.GetStatus(short token)
        {
            return core.GetStatus(token);
        }

        AppaTaskStatus IAppaTaskSource.UnsafeGetStatus()
        {
            return core.UnsafeGetStatus();
        }

        void IAppaTaskSource.OnCompleted(Action<object> continuation, object state, short token)
        {
            core.OnCompleted(continuation, state, token);
        }
    }

    public class UnityEventHandlerAsyncEnumerable : IAppaTaskAsyncEnumerable<AsyncUnit>
    {
        private readonly UnityEvent unityEvent;
        private readonly CancellationToken cancellationToken1;

        public UnityEventHandlerAsyncEnumerable(UnityEvent unityEvent, CancellationToken cancellationToken)
        {
            this.unityEvent = unityEvent;
            cancellationToken1 = cancellationToken;
        }

        public IAppaTaskAsyncEnumerator<AsyncUnit> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            if (cancellationToken1 == cancellationToken)
            {
                return new UnityEventHandlerAsyncEnumerator(
                    unityEvent,
                    cancellationToken1,
                    CancellationToken.None
                );
            }

            return new UnityEventHandlerAsyncEnumerator(unityEvent, cancellationToken1, cancellationToken);
        }

        private class UnityEventHandlerAsyncEnumerator : MoveNextSource, IAppaTaskAsyncEnumerator<AsyncUnit>
        {
            private static readonly Action<object> cancel1 = OnCanceled1;
            private static readonly Action<object> cancel2 = OnCanceled2;

            private readonly UnityEvent unityEvent;
            private CancellationToken cancellationToken1;
            private CancellationToken cancellationToken2;

            private UnityAction unityAction;
            private CancellationTokenRegistration registration1;
            private CancellationTokenRegistration registration2;
            private bool isDisposed;

            public UnityEventHandlerAsyncEnumerator(
                UnityEvent unityEvent,
                CancellationToken cancellationToken1,
                CancellationToken cancellationToken2)
            {
                this.unityEvent = unityEvent;
                this.cancellationToken1 = cancellationToken1;
                this.cancellationToken2 = cancellationToken2;
            }

            public AsyncUnit Current => default;

            public AppaTask<bool> MoveNextAsync()
            {
                cancellationToken1.ThrowIfCancellationRequested();
                cancellationToken2.ThrowIfCancellationRequested();
                completionSource.Reset();

                if (unityAction == null)
                {
                    unityAction = Invoke;

                    TaskTracker.TrackActiveTask(this, 3);
                    unityEvent.AddListener(unityAction);
                    if (cancellationToken1.CanBeCanceled)
                    {
                        registration1 =
                            cancellationToken1.RegisterWithoutCaptureExecutionContext(cancel1, this);
                    }

                    if (cancellationToken2.CanBeCanceled)
                    {
                        registration2 =
                            cancellationToken1.RegisterWithoutCaptureExecutionContext(cancel2, this);
                    }
                }

                return new AppaTask<bool>(this, completionSource.Version);
            }

            private void Invoke()
            {
                completionSource.TrySetResult(true);
            }

            private static void OnCanceled1(object state)
            {
                var self = (UnityEventHandlerAsyncEnumerator)state;
                self.DisposeAsync().Forget();
            }

            private static void OnCanceled2(object state)
            {
                var self = (UnityEventHandlerAsyncEnumerator)state;
                self.DisposeAsync().Forget();
            }

            public AppaTask DisposeAsync()
            {
                if (!isDisposed)
                {
                    isDisposed = true;
                    TaskTracker.RemoveTracking(this);
                    registration1.Dispose();
                    registration2.Dispose();
                    unityEvent.RemoveListener(unityAction);
                }

                return default;
            }
        }
    }

    public class UnityEventHandlerAsyncEnumerable<T> : IAppaTaskAsyncEnumerable<T>
    {
        private readonly UnityEvent<T> unityEvent;
        private readonly CancellationToken cancellationToken1;

        public UnityEventHandlerAsyncEnumerable(UnityEvent<T> unityEvent, CancellationToken cancellationToken)
        {
            this.unityEvent = unityEvent;
            cancellationToken1 = cancellationToken;
        }

        public IAppaTaskAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            if (cancellationToken1 == cancellationToken)
            {
                return new UnityEventHandlerAsyncEnumerator(
                    unityEvent,
                    cancellationToken1,
                    CancellationToken.None
                );
            }

            return new UnityEventHandlerAsyncEnumerator(unityEvent, cancellationToken1, cancellationToken);
        }

        private class UnityEventHandlerAsyncEnumerator : MoveNextSource, IAppaTaskAsyncEnumerator<T>
        {
            private static readonly Action<object> cancel1 = OnCanceled1;
            private static readonly Action<object> cancel2 = OnCanceled2;

            private readonly UnityEvent<T> unityEvent;
            private CancellationToken cancellationToken1;
            private CancellationToken cancellationToken2;

            private UnityAction<T> unityAction;
            private CancellationTokenRegistration registration1;
            private CancellationTokenRegistration registration2;
            private bool isDisposed;

            public UnityEventHandlerAsyncEnumerator(
                UnityEvent<T> unityEvent,
                CancellationToken cancellationToken1,
                CancellationToken cancellationToken2)
            {
                this.unityEvent = unityEvent;
                this.cancellationToken1 = cancellationToken1;
                this.cancellationToken2 = cancellationToken2;
            }

            public T Current { get; private set; }

            public AppaTask<bool> MoveNextAsync()
            {
                cancellationToken1.ThrowIfCancellationRequested();
                cancellationToken2.ThrowIfCancellationRequested();
                completionSource.Reset();

                if (unityAction == null)
                {
                    unityAction = Invoke;

                    TaskTracker.TrackActiveTask(this, 3);
                    unityEvent.AddListener(unityAction);
                    if (cancellationToken1.CanBeCanceled)
                    {
                        registration1 =
                            cancellationToken1.RegisterWithoutCaptureExecutionContext(cancel1, this);
                    }

                    if (cancellationToken2.CanBeCanceled)
                    {
                        registration2 =
                            cancellationToken1.RegisterWithoutCaptureExecutionContext(cancel2, this);
                    }
                }

                return new AppaTask<bool>(this, completionSource.Version);
            }

            private void Invoke(T value)
            {
                Current = value;
                completionSource.TrySetResult(true);
            }

            private static void OnCanceled1(object state)
            {
                var self = (UnityEventHandlerAsyncEnumerator)state;
                self.DisposeAsync().Forget();
            }

            private static void OnCanceled2(object state)
            {
                var self = (UnityEventHandlerAsyncEnumerator)state;
                self.DisposeAsync().Forget();
            }

            public AppaTask DisposeAsync()
            {
                if (!isDisposed)
                {
                    isDisposed = true;
                    TaskTracker.RemoveTracking(this);
                    registration1.Dispose();
                    registration2.Dispose();
                    if (unityEvent is IDisposable disp)
                    {
                        disp.Dispose();
                    }

                    unityEvent.RemoveListener(unityAction);
                }

                return default;
            }
        }
    }
}

#endif
