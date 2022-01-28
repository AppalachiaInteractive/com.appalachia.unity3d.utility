using System;
using System.Threading;
using Appalachia.Utility.Async.Internal;

namespace Appalachia.Utility.Async.Linq
{
    public static partial class AppaTaskAsyncEnumerable
    {
        public static IAppaTaskAsyncEnumerable<T> Create<T>(
            Func<IAsyncWriter<T>, CancellationToken, AppaTask> create)
        {
            Error.ThrowArgumentNullException(create, nameof(create));
            return new Create<T>(create);
        }
    }

    public interface IAsyncWriter<T>
    {
        AppaTask YieldAsync(T value);
    }

    internal sealed class Create<T> : IAppaTaskAsyncEnumerable<T>
    {
        private readonly Func<IAsyncWriter<T>, CancellationToken, AppaTask> create;

        public Create(Func<IAsyncWriter<T>, CancellationToken, AppaTask> create)
        {
            this.create = create;
        }

        public IAppaTaskAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _Create(create, cancellationToken);
        }

        private sealed class _Create : MoveNextSource, IAppaTaskAsyncEnumerator<T>
        {
            private readonly Func<IAsyncWriter<T>, CancellationToken, AppaTask> create;
            private readonly CancellationToken cancellationToken;

            private int state = -1;
            private AsyncWriter writer;

            public _Create(
                Func<IAsyncWriter<T>, CancellationToken, AppaTask> create,
                CancellationToken cancellationToken)
            {
                this.create = create;
                this.cancellationToken = cancellationToken;
                TaskTracker.TrackActiveTask(this, 3);
            }

            public T Current { get; private set; }

            public AppaTask DisposeAsync()
            {
                TaskTracker.RemoveTracking(this);
                return default;
            }

            public AppaTask<bool> MoveNextAsync()
            {
                if (state == -2)
                {
                    return default;
                }

                completionSource.Reset();
                MoveNext();
                return new AppaTask<bool>(this, completionSource.Version);
            }

            private void MoveNext()
            {
                try
                {
                    switch (state)
                    {
                        case -1: // init
                        {
                            writer = new AsyncWriter(this);
                            RunWriterTask(create(writer, cancellationToken)).Forget();
                            if (Volatile.Read(ref state) == -2)
                            {
                                return; // complete synchronously
                            }

                            state = 0; // wait YieldAsync, it set TrySetResult(true)
                            return;
                        }
                        case 0:
                            writer.SignalWriter();
                            return;
                        default:
                            goto DONE;
                    }
                }
                catch (Exception ex)
                {
                    state = -2;
                    completionSource.TrySetException(ex);
                    return;
                }

                DONE:
                state = -2;
                completionSource.TrySetResult(false);
            }

            private async AppaTaskVoid RunWriterTask(AppaTask task)
            {
                try
                {
                    await task;
                }
                catch (Exception ex)
                {
                    Volatile.Write(ref state, -2);
                    completionSource.TrySetException(ex);
                    return;
                }

#pragma warning disable CS0164
                DONE:
#pragma warning restore CS0164
                Volatile.Write(ref state, -2);
                completionSource.TrySetResult(false);
            }

            public void SetResult(T value)
            {
                Current = value;
                completionSource.TrySetResult(true);
            }
        }

        private sealed class AsyncWriter : IAppaTaskSource, IAsyncWriter<T>
        {
            private readonly _Create enumerator;

            private AppaTaskCompletionSourceCore<AsyncUnit> core;

            public AsyncWriter(_Create enumerator)
            {
                this.enumerator = enumerator;
            }

            public void GetResult(short token)
            {
                core.GetResult(token);
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

            public AppaTask YieldAsync(T value)
            {
                core.Reset();
                enumerator.SetResult(value);
                return new AppaTask(this, core.Version);
            }

            public void SignalWriter()
            {
                core.TrySetResult(AsyncUnit.Default);
            }
        }
    }
}
