using System;
using System.Collections.Generic;
using System.Threading;

// ReSharper disable All

namespace Appalachia.Utility.Async
{
    public static class Channel
    {
        public static Channel<T> CreateSingleConsumerUnbounded<T>()
        {
            return new SingleConsumerUnboundedChannel<T>();
        }
    }

    public abstract class Channel<TWrite, TRead>
    {
        #region Fields and Autoproperties

        public ChannelReader<TRead> Reader { get; protected set; }
        public ChannelWriter<TWrite> Writer { get; protected set; }

        #endregion

        public static implicit operator ChannelReader<TRead>(Channel<TWrite, TRead> channel) =>
            channel.Reader;

        public static implicit operator ChannelWriter<TWrite>(Channel<TWrite, TRead> channel) =>
            channel.Writer;
    }

    public abstract class Channel<T> : Channel<T, T>
    {
    }

    public abstract class ChannelReader<T>
    {
        public abstract AppaTask Completion { get; }

        public abstract IAppaTaskAsyncEnumerable<T> ReadAllAsync(
            CancellationToken cancellationToken = default(CancellationToken));

        public abstract bool TryRead(out T item);

        public abstract AppaTask<bool> WaitToReadAsync(
            CancellationToken cancellationToken = default(CancellationToken));

        public virtual AppaTask<T> ReadAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            if (this.TryRead(out var item))
            {
                return AppaTask.FromResult(item);
            }

            return ReadAsyncCore(cancellationToken);
        }

        async AppaTask<T> ReadAsyncCore(CancellationToken cancellationToken = default(CancellationToken))
        {
            if (await WaitToReadAsync(cancellationToken))
            {
                if (TryRead(out var item))
                {
                    return item;
                }
            }

            throw new ChannelClosedException();
        }
    }

    public abstract class ChannelWriter<T>
    {
        public abstract bool TryComplete(Exception error = null);
        public abstract bool TryWrite(T item);

        public void Complete(Exception error = null)
        {
            if (!TryComplete(error))
            {
                throw new ChannelClosedException();
            }
        }
    }

    public partial class ChannelClosedException : InvalidOperationException
    {
        public ChannelClosedException() : base("Channel is already closed.")
        {
        }

        public ChannelClosedException(string message) : base(message)
        {
        }

        public ChannelClosedException(Exception innerException) : base(
            "Channel is already closed",
            innerException
        )
        {
        }

        public ChannelClosedException(string message, Exception innerException) : base(
            message,
            innerException
        )
        {
        }
    }

    internal class SingleConsumerUnboundedChannel<T> : Channel<T>
    {
        public SingleConsumerUnboundedChannel()
        {
            items = new Queue<T>();
            Writer = new SingleConsumerUnboundedChannelWriter(this);
            readerSource = new SingleConsumerUnboundedChannelReader(this);
            Reader = readerSource;
        }

        #region Fields and Autoproperties

        readonly Queue<T> items;
        readonly SingleConsumerUnboundedChannelReader readerSource;
        AppaTask completedTask;
        AppaTaskCompletionSource completedTaskSource;
        bool closed;

        Exception completionError;

        #endregion

        #region Nested type: SingleConsumerUnboundedChannelReader

        sealed class SingleConsumerUnboundedChannelReader : ChannelReader<T>, IAppaTaskSource<bool>
        {
            public SingleConsumerUnboundedChannelReader(SingleConsumerUnboundedChannel<T> parent)
            {
                this.parent = parent;

                TaskTracker.TrackActiveTask(this, 4);
            }

            #region Fields and Autoproperties

            internal bool isWaiting;
            readonly Action<object> CancellationCallbackDelegate = CancellationCallback;
            readonly SingleConsumerUnboundedChannel<T> parent;
            AppaTaskCompletionSourceCore<bool> core;

            CancellationToken cancellationToken;
            CancellationTokenRegistration cancellationTokenRegistration;

            #endregion

            /// <inheritdoc />
            public override AppaTask Completion
            {
                get
                {
                    if (parent.completedTaskSource != null) return parent.completedTaskSource.Task;

                    if (parent.closed)
                    {
                        return parent.completedTask;
                    }

                    parent.completedTaskSource = new AppaTaskCompletionSource();
                    return parent.completedTaskSource.Task;
                }
            }

            /// <inheritdoc />
            public override IAppaTaskAsyncEnumerable<T> ReadAllAsync(
                CancellationToken cancellationToken = default)
            {
                return new ReadAllAsyncEnumerable(this, cancellationToken);
            }

            /// <inheritdoc />
            public override bool TryRead(out T item)
            {
                lock (parent.items)
                {
                    if (parent.items.Count != 0)
                    {
                        item = parent.items.Dequeue();

                        // complete when all value was consumed.
                        if (parent.closed && parent.items.Count == 0)
                        {
                            if (parent.completionError != null)
                            {
                                if (parent.completedTaskSource != null)
                                {
                                    parent.completedTaskSource.TrySetException(parent.completionError);
                                }
                                else
                                {
                                    parent.completedTask = AppaTask.FromException(parent.completionError);
                                }
                            }
                            else
                            {
                                if (parent.completedTaskSource != null)
                                {
                                    parent.completedTaskSource.TrySetResult();
                                }
                                else
                                {
                                    parent.completedTask = AppaTask.CompletedTask;
                                }
                            }
                        }
                    }
                    else
                    {
                        item = default;
                        return false;
                    }
                }

                return true;
            }

            /// <inheritdoc />
            public override AppaTask<bool> WaitToReadAsync(CancellationToken cancellationToken)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return AppaTask.FromCanceled<bool>(cancellationToken);
                }

                lock (parent.items)
                {
                    if (parent.items.Count != 0)
                    {
                        return CompletedTasks.True;
                    }

                    if (parent.closed)
                    {
                        if (parent.completionError == null)
                        {
                            return CompletedTasks.False;
                        }
                        else
                        {
                            return AppaTask.FromException<bool>(parent.completionError);
                        }
                    }

                    cancellationTokenRegistration.Dispose();

                    core.Reset();
                    isWaiting = true;

                    this.cancellationToken = cancellationToken;
                    if (this.cancellationToken.CanBeCanceled)
                    {
                        cancellationTokenRegistration =
                            this.cancellationToken.RegisterWithoutCaptureExecutionContext(
                                CancellationCallbackDelegate,
                                this
                            );
                    }

                    return new AppaTask<bool>(this, core.Version);
                }
            }

            public void SingalCancellation(CancellationToken cancellationToken)
            {
                TaskTracker.RemoveTracking(this);
                core.TrySetCanceled(cancellationToken);
            }

            public void SingalCompleted(Exception error)
            {
                if (error != null)
                {
                    TaskTracker.RemoveTracking(this);
                    core.TrySetException(error);
                }
                else
                {
                    TaskTracker.RemoveTracking(this);
                    core.TrySetResult(false);
                }
            }

            public void SingalContinuation()
            {
                core.TrySetResult(true);
            }

            static void CancellationCallback(object state)
            {
                var self = (SingleConsumerUnboundedChannelReader)state;
                self.SingalCancellation(self.cancellationToken);
            }

            #region IAppaTaskSource<bool> Members

            bool IAppaTaskSource<bool>.GetResult(short token)
            {
                return core.GetResult(token);
            }

            void IAppaTaskSource.GetResult(short token)
            {
                core.GetResult(token);
            }

            AppaTaskStatus IAppaTaskSource.GetStatus(short token)
            {
                return core.GetStatus(token);
            }

            void IAppaTaskSource.OnCompleted(Action<object> continuation, object state, short token)
            {
                core.OnCompleted(continuation, state, token);
            }

            AppaTaskStatus IAppaTaskSource.UnsafeGetStatus()
            {
                return core.UnsafeGetStatus();
            }

            #endregion

            #region Nested type: ReadAllAsyncEnumerable

            sealed class ReadAllAsyncEnumerable : IAppaTaskAsyncEnumerable<T>, IAppaTaskAsyncEnumerator<T>
            {
                public ReadAllAsyncEnumerable(
                    SingleConsumerUnboundedChannelReader parent,
                    CancellationToken cancellationToken)
                {
                    this.parent = parent;
                    this.cancellationToken1 = cancellationToken;
                }

                #region Fields and Autoproperties

                readonly Action<object> CancellationCallback1Delegate = CancellationCallback1;
                readonly Action<object> CancellationCallback2Delegate = CancellationCallback2;

                readonly SingleConsumerUnboundedChannelReader parent;
                bool cacheValue;
                bool running;
                CancellationToken cancellationToken1;
                CancellationToken cancellationToken2;
                CancellationTokenRegistration cancellationTokenRegistration1;
                CancellationTokenRegistration cancellationTokenRegistration2;

                T current;

                #endregion

                static void CancellationCallback1(object state)
                {
                    var self = (ReadAllAsyncEnumerable)state;
                    self.parent.SingalCancellation(self.cancellationToken1);
                }

                static void CancellationCallback2(object state)
                {
                    var self = (ReadAllAsyncEnumerable)state;
                    self.parent.SingalCancellation(self.cancellationToken2);
                }

                #region IAppaTaskAsyncEnumerable<T> Members

                public IAppaTaskAsyncEnumerator<T> GetAsyncEnumerator(
                    CancellationToken cancellationToken = default)
                {
                    if (running)
                    {
                        throw new InvalidOperationException(
                            "Enumerator is already running, does not allow call GetAsyncEnumerator twice."
                        );
                    }

                    if (this.cancellationToken1 != cancellationToken)
                    {
                        this.cancellationToken2 = cancellationToken;
                    }

                    if (this.cancellationToken1.CanBeCanceled)
                    {
                        this.cancellationTokenRegistration1 =
                            this.cancellationToken1.RegisterWithoutCaptureExecutionContext(
                                CancellationCallback1Delegate,
                                this
                            );
                    }

                    if (this.cancellationToken2.CanBeCanceled)
                    {
                        this.cancellationTokenRegistration2 =
                            this.cancellationToken2.RegisterWithoutCaptureExecutionContext(
                                CancellationCallback2Delegate,
                                this
                            );
                    }

                    running = true;
                    return this;
                }

                #endregion

                #region IAppaTaskAsyncEnumerator<T> Members

                public T Current
                {
                    get
                    {
                        if (cacheValue)
                        {
                            return current;
                        }

                        parent.TryRead(out current);
                        return current;
                    }
                }

                public AppaTask<bool> MoveNextAsync()
                {
                    cacheValue = false;
                    return parent.WaitToReadAsync(
                        CancellationToken.None
                    ); // ok to use None, registered in ctor.
                }

                public AppaTask DisposeAsync()
                {
                    cancellationTokenRegistration1.Dispose();
                    cancellationTokenRegistration2.Dispose();
                    return default;
                }

                #endregion
            }

            #endregion
        }

        #endregion

        #region Nested type: SingleConsumerUnboundedChannelWriter

        sealed class SingleConsumerUnboundedChannelWriter : ChannelWriter<T>
        {
            public SingleConsumerUnboundedChannelWriter(SingleConsumerUnboundedChannel<T> parent)
            {
                this.parent = parent;
            }

            #region Fields and Autoproperties

            readonly SingleConsumerUnboundedChannel<T> parent;

            #endregion

            /// <inheritdoc />
            public override bool TryComplete(Exception error = null)
            {
                bool waiting;
                lock (parent.items)
                {
                    if (parent.closed) return false;
                    parent.closed = true;
                    waiting = parent.readerSource.isWaiting;

                    if (parent.items.Count == 0)
                    {
                        if (error == null)
                        {
                            if (parent.completedTaskSource != null)
                            {
                                parent.completedTaskSource.TrySetResult();
                            }
                            else
                            {
                                parent.completedTask = AppaTask.CompletedTask;
                            }
                        }
                        else
                        {
                            if (parent.completedTaskSource != null)
                            {
                                parent.completedTaskSource.TrySetException(error);
                            }
                            else
                            {
                                parent.completedTask = AppaTask.FromException(error);
                            }
                        }

                        if (waiting)
                        {
                            parent.readerSource.SingalCompleted(error);
                        }
                    }

                    parent.completionError = error;
                }

                return true;
            }

            /// <inheritdoc />
            public override bool TryWrite(T item)
            {
                bool waiting;
                lock (parent.items)
                {
                    if (parent.closed) return false;

                    parent.items.Enqueue(item);
                    waiting = parent.readerSource.isWaiting;
                }

                if (waiting)
                {
                    parent.readerSource.SingalContinuation();
                }

                return true;
            }
        }

        #endregion
    }
}
