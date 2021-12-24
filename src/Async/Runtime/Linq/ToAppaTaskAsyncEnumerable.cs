using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Appalachia.Utility.Async.Internal;

namespace Appalachia.Utility.Async.Linq
{
    public static partial class AppaTaskAsyncEnumerable
    {
        public static IAppaTaskAsyncEnumerable<TSource> ToAppaTaskAsyncEnumerable<TSource>(
            this IEnumerable<TSource> source)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return new ToAppaTaskAsyncEnumerable<TSource>(source);
        }

        public static IAppaTaskAsyncEnumerable<TSource> ToAppaTaskAsyncEnumerable<TSource>(
            this Task<TSource> source)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return new ToAppaTaskAsyncEnumerableTask<TSource>(source);
        }

        public static IAppaTaskAsyncEnumerable<TSource> ToAppaTaskAsyncEnumerable<TSource>(
            this AppaTask<TSource> source)
        {
            return new ToAppaTaskAsyncEnumerableAppaTask<TSource>(source);
        }

        public static IAppaTaskAsyncEnumerable<TSource> ToAppaTaskAsyncEnumerable<TSource>(
            this IObservable<TSource> source)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return new ToAppaTaskAsyncEnumerableObservable<TSource>(source);
        }
    }

    internal class ToAppaTaskAsyncEnumerable<T> : IAppaTaskAsyncEnumerable<T>
    {
        private readonly IEnumerable<T> source;

        public ToAppaTaskAsyncEnumerable(IEnumerable<T> source)
        {
            this.source = source;
        }

        public IAppaTaskAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _ToAppaTaskAsyncEnumerable(source, cancellationToken);
        }

        private class _ToAppaTaskAsyncEnumerable : IAppaTaskAsyncEnumerator<T>
        {
            private readonly IEnumerable<T> source;
            private CancellationToken cancellationToken;

            private IEnumerator<T> enumerator;

            public _ToAppaTaskAsyncEnumerable(IEnumerable<T> source, CancellationToken cancellationToken)
            {
                this.source = source;
                this.cancellationToken = cancellationToken;
            }

            public T Current => enumerator.Current;

            public AppaTask<bool> MoveNextAsync()
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (enumerator == null)
                {
                    enumerator = source.GetEnumerator();
                }

                if (enumerator.MoveNext())
                {
                    return CompletedTasks.True;
                }

                return CompletedTasks.False;
            }

            public AppaTask DisposeAsync()
            {
                enumerator.Dispose();
                return default;
            }
        }
    }

    internal class ToAppaTaskAsyncEnumerableTask<T> : IAppaTaskAsyncEnumerable<T>
    {
        private readonly Task<T> source;

        public ToAppaTaskAsyncEnumerableTask(Task<T> source)
        {
            this.source = source;
        }

        public IAppaTaskAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _ToAppaTaskAsyncEnumerableTask(source, cancellationToken);
        }

        private class _ToAppaTaskAsyncEnumerableTask : IAppaTaskAsyncEnumerator<T>
        {
            private readonly Task<T> source;
            private CancellationToken cancellationToken;

            private T current;
            private bool called;

            public _ToAppaTaskAsyncEnumerableTask(Task<T> source, CancellationToken cancellationToken)
            {
                this.source = source;
                this.cancellationToken = cancellationToken;

                called = false;
            }

            public T Current => current;

            public async AppaTask<bool> MoveNextAsync()
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (called)
                {
                    return false;
                }

                called = true;

                current = await source;
                return true;
            }

            public AppaTask DisposeAsync()
            {
                return default;
            }
        }
    }

    internal class ToAppaTaskAsyncEnumerableAppaTask<T> : IAppaTaskAsyncEnumerable<T>
    {
        private readonly AppaTask<T> source;

        public ToAppaTaskAsyncEnumerableAppaTask(AppaTask<T> source)
        {
            this.source = source;
        }

        public IAppaTaskAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _ToAppaTaskAsyncEnumerableAppaTask(source, cancellationToken);
        }

        private class _ToAppaTaskAsyncEnumerableAppaTask : IAppaTaskAsyncEnumerator<T>
        {
            private readonly AppaTask<T> source;
            private CancellationToken cancellationToken;

            private T current;
            private bool called;

            public _ToAppaTaskAsyncEnumerableAppaTask(AppaTask<T> source, CancellationToken cancellationToken)
            {
                this.source = source;
                this.cancellationToken = cancellationToken;

                called = false;
            }

            public T Current => current;

            public async AppaTask<bool> MoveNextAsync()
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (called)
                {
                    return false;
                }

                called = true;

                current = await source;
                return true;
            }

            public AppaTask DisposeAsync()
            {
                return default;
            }
        }
    }

    internal class ToAppaTaskAsyncEnumerableObservable<T> : IAppaTaskAsyncEnumerable<T>
    {
        private readonly IObservable<T> source;

        public ToAppaTaskAsyncEnumerableObservable(IObservable<T> source)
        {
            this.source = source;
        }

        public IAppaTaskAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _ToAppaTaskAsyncEnumerableObservable(source, cancellationToken);
        }

        private class _ToAppaTaskAsyncEnumerableObservable : MoveNextSource,
                                                             IAppaTaskAsyncEnumerator<T>,
                                                             IObserver<T>
        {
            private static readonly Action<object> OnCanceledDelegate = OnCanceled;

            private readonly IObservable<T> source;
            private CancellationToken cancellationToken;

            private bool useCachedCurrent;
            private T current;
            private bool subscribeCompleted;
            private readonly Queue<T> queuedResult;
            private Exception error;
            private IDisposable subscription;
            private CancellationTokenRegistration cancellationTokenRegistration;

            public _ToAppaTaskAsyncEnumerableObservable(
                IObservable<T> source,
                CancellationToken cancellationToken)
            {
                this.source = source;
                this.cancellationToken = cancellationToken;
                queuedResult = new Queue<T>();

                if (cancellationToken.CanBeCanceled)
                {
                    cancellationTokenRegistration =
                        cancellationToken.RegisterWithoutCaptureExecutionContext(OnCanceledDelegate, this);
                }
            }

            public T Current
            {
                get
                {
                    if (useCachedCurrent)
                    {
                        return current;
                    }

                    lock (queuedResult)
                    {
                        if (queuedResult.Count != 0)
                        {
                            current = queuedResult.Dequeue();
                            useCachedCurrent = true;
                            return current;
                        }

                        return default; // undefined.
                    }
                }
            }

            public AppaTask<bool> MoveNextAsync()
            {
                lock (queuedResult)
                {
                    useCachedCurrent = false;

                    if (cancellationToken.IsCancellationRequested)
                    {
                        return AppaTask.FromCanceled<bool>(cancellationToken);
                    }

                    if (subscription == null)
                    {
                        subscription = source.Subscribe(this);
                    }

                    if (error != null)
                    {
                        return AppaTask.FromException<bool>(error);
                    }

                    if (queuedResult.Count != 0)
                    {
                        return CompletedTasks.True;
                    }

                    if (subscribeCompleted)
                    {
                        return CompletedTasks.False;
                    }

                    completionSource.Reset();
                    return new AppaTask<bool>(this, completionSource.Version);
                }
            }

            public AppaTask DisposeAsync()
            {
                subscription.Dispose();
                cancellationTokenRegistration.Dispose();
                completionSource.Reset();
                return default;
            }

            public void OnCompleted()
            {
                lock (queuedResult)
                {
                    subscribeCompleted = true;
                    completionSource.TrySetResult(false);
                }
            }

            public void OnError(Exception error)
            {
                lock (queuedResult)
                {
                    this.error = error;
                    completionSource.TrySetException(error);
                }
            }

            public void OnNext(T value)
            {
                lock (queuedResult)
                {
                    queuedResult.Enqueue(value);
                    completionSource.TrySetResult(true); // include callback execution, too long lock?
                }
            }

            private static void OnCanceled(object state)
            {
                var self = (_ToAppaTaskAsyncEnumerableObservable)state;
                lock (self.queuedResult)
                {
                    self.completionSource.TrySetCanceled(self.cancellationToken);
                }
            }
        }
    }
}
