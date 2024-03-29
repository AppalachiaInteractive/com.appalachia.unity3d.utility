﻿using System;
using System.Threading;
using Appalachia.Utility.Async.Internal;

namespace Appalachia.Utility.Async.Linq
{
    public static partial class AppaTaskAsyncEnumerable
    {
        public static IConnectableAppaTaskAsyncEnumerable<TSource> Publish<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return new Publish<TSource>(source);
        }
    }

    internal sealed class Publish<TSource> : IConnectableAppaTaskAsyncEnumerable<TSource>
    {
        private readonly IAppaTaskAsyncEnumerable<TSource> source;
        private readonly CancellationTokenSource cancellationTokenSource;

        private TriggerEvent<TSource> trigger;
        private IAppaTaskAsyncEnumerator<TSource> enumerator;
        private IDisposable connectedDisposable;
        private bool isCompleted;

        public Publish(IAppaTaskAsyncEnumerable<TSource> source)
        {
            this.source = source;
            cancellationTokenSource = new CancellationTokenSource();
        }

        public IDisposable Connect()
        {
            if (connectedDisposable != null)
            {
                return connectedDisposable;
            }

            if (enumerator == null)
            {
                enumerator = source.GetAsyncEnumerator(cancellationTokenSource.Token);
            }

            ConsumeEnumerator().Forget();

            connectedDisposable = new ConnectDisposable(cancellationTokenSource);
            return connectedDisposable;
        }

        private async AppaTaskVoid ConsumeEnumerator()
        {
            try
            {
                try
                {
                    while (await enumerator.MoveNextAsync())
                    {
                        trigger.SetResult(enumerator.Current);
                    }

                    trigger.SetCompleted();
                }
                catch (Exception ex)
                {
                    trigger.SetError(ex);
                }
            }
            finally
            {
                isCompleted = true;
                await enumerator.DisposeAsync();
            }
        }

        public IAppaTaskAsyncEnumerator<TSource> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            return new _Publish(this, cancellationToken);
        }

        private sealed class ConnectDisposable : IDisposable
        {
            private readonly CancellationTokenSource cancellationTokenSource;

            public ConnectDisposable(CancellationTokenSource cancellationTokenSource)
            {
                this.cancellationTokenSource = cancellationTokenSource;
            }

            public void Dispose()
            {
                cancellationTokenSource.Cancel();
            }
        }

        private sealed class _Publish : MoveNextSource,
                                        IAppaTaskAsyncEnumerator<TSource>,
                                        ITriggerHandler<TSource>
        {
            private static readonly Action<object> CancelDelegate = OnCanceled;

            private readonly Publish<TSource> parent;
            private CancellationToken cancellationToken;
            private CancellationTokenRegistration cancellationTokenRegistration;
            private bool isDisposed;

            public _Publish(Publish<TSource> parent, CancellationToken cancellationToken)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }

                this.parent = parent;
                this.cancellationToken = cancellationToken;

                if (cancellationToken.CanBeCanceled)
                {
                    cancellationTokenRegistration =
                        cancellationToken.RegisterWithoutCaptureExecutionContext(CancelDelegate, this);
                }

                parent.trigger.Add(this);
                TaskTracker.TrackActiveTask(this, 3);
            }

            public TSource Current { get; private set; }
            ITriggerHandler<TSource> ITriggerHandler<TSource>.Prev { get; set; }
            ITriggerHandler<TSource> ITriggerHandler<TSource>.Next { get; set; }

            public AppaTask<bool> MoveNextAsync()
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (parent.isCompleted)
                {
                    return CompletedTasks.False;
                }

                completionSource.Reset();
                return new AppaTask<bool>(this, completionSource.Version);
            }

            private static void OnCanceled(object state)
            {
                var self = (_Publish)state;
                self.completionSource.TrySetCanceled(self.cancellationToken);
                self.DisposeAsync().Forget();
            }

            public AppaTask DisposeAsync()
            {
                if (!isDisposed)
                {
                    isDisposed = true;
                    TaskTracker.RemoveTracking(this);
                    cancellationTokenRegistration.Dispose();
                    parent.trigger.Remove(this);
                }

                return default;
            }

            public void OnNext(TSource value)
            {
                Current = value;
                completionSource.TrySetResult(true);
            }

            public void OnCanceled(CancellationToken cancellationToken)
            {
                completionSource.TrySetCanceled(cancellationToken);
            }

            public void OnCompleted()
            {
                completionSource.TrySetResult(false);
            }

            public void OnError(Exception ex)
            {
                completionSource.TrySetException(ex);
            }
        }
    }
}
