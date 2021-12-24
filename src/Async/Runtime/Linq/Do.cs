using System;
using System.Threading;
using Appalachia.Utility.Async.Internal;

namespace Appalachia.Utility.Async.Linq
{
    public static partial class AppaTaskAsyncEnumerable
    {
        public static IAppaTaskAsyncEnumerable<TSource> Do<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Action<TSource> onNext)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            return source.Do(onNext, null, null);
        }

        public static IAppaTaskAsyncEnumerable<TSource> Do<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Action<TSource> onNext,
            Action<Exception> onError)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            return source.Do(onNext, onError, null);
        }

        public static IAppaTaskAsyncEnumerable<TSource> Do<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Action<TSource> onNext,
            Action onCompleted)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            return source.Do(onNext, null, onCompleted);
        }

        public static IAppaTaskAsyncEnumerable<TSource> Do<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Action<TSource> onNext,
            Action<Exception> onError,
            Action onCompleted)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            return new Do<TSource>(source, onNext, onError, onCompleted);
        }

        public static IAppaTaskAsyncEnumerable<TSource> Do<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            IObserver<TSource> observer)
        {
            Error.ThrowArgumentNullException(source,   nameof(source));
            Error.ThrowArgumentNullException(observer, nameof(observer));

            return source.Do(observer.OnNext, observer.OnError, observer.OnCompleted); // alloc delegate.
        }

        // not yet impl.

        //public static IAppaTaskAsyncEnumerable<TSource> DoAwait<TSource>(this IAppaTaskAsyncEnumerable<TSource> source, Func<TSource, AppaTask> onNext)
        //{
        //    throw new NotImplementedException();
        //}

        //public static IAppaTaskAsyncEnumerable<TSource> DoAwait<TSource>(this IAppaTaskAsyncEnumerable<TSource> source, Func<TSource, AppaTask> onNext, Func<Exception, AppaTask> onError)
        //{
        //    throw new NotImplementedException();
        //}

        //public static IAppaTaskAsyncEnumerable<TSource> DoAwait<TSource>(this IAppaTaskAsyncEnumerable<TSource> source, Func<TSource, AppaTask> onNext, Func<AppaTask> onCompleted)
        //{
        //    throw new NotImplementedException();
        //}

        //public static IAppaTaskAsyncEnumerable<TSource> DoAwait<TSource>(this IAppaTaskAsyncEnumerable<TSource> source, Func<TSource, AppaTask> onNext, Func<Exception, AppaTask> onError, Func<AppaTask> onCompleted)
        //{
        //    throw new NotImplementedException();
        //}

        //public static IAppaTaskAsyncEnumerable<TSource> DoAwaitWithCancellation<TSource>(this IAppaTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, AppaTask> onNext)
        //{
        //    throw new NotImplementedException();
        //}

        //public static IAppaTaskAsyncEnumerable<TSource> DoAwaitWithCancellation<TSource>(this IAppaTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, AppaTask> onNext, Func<Exception, CancellationToken, AppaTask> onError)
        //{
        //    throw new NotImplementedException();
        //}

        //public static IAppaTaskAsyncEnumerable<TSource> DoAwaitWithCancellation<TSource>(this IAppaTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, AppaTask> onNext, Func<CancellationToken, AppaTask> onCompleted)
        //{
        //    throw new NotImplementedException();
        //}

        //public static IAppaTaskAsyncEnumerable<TSource> DoAwaitWithCancellation<TSource>(this IAppaTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, AppaTask> onNext, Func<Exception, CancellationToken, AppaTask> onError, Func<CancellationToken, AppaTask> onCompleted)
        //{
        //    throw new NotImplementedException();
        //}
    }

    internal sealed class Do<TSource> : IAppaTaskAsyncEnumerable<TSource>
    {
        private readonly IAppaTaskAsyncEnumerable<TSource> source;
        private readonly Action<TSource> onNext;
        private readonly Action<Exception> onError;
        private readonly Action onCompleted;

        public Do(
            IAppaTaskAsyncEnumerable<TSource> source,
            Action<TSource> onNext,
            Action<Exception> onError,
            Action onCompleted)
        {
            this.source = source;
            this.onNext = onNext;
            this.onError = onError;
            this.onCompleted = onCompleted;
        }

        public IAppaTaskAsyncEnumerator<TSource> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            return new _Do(source, onNext, onError, onCompleted, cancellationToken);
        }

        private sealed class _Do : MoveNextSource, IAppaTaskAsyncEnumerator<TSource>
        {
            private static readonly Action<object> MoveNextCoreDelegate = MoveNextCore;

            private readonly IAppaTaskAsyncEnumerable<TSource> source;
            private readonly Action<TSource> onNext;
            private readonly Action<Exception> onError;
            private readonly Action onCompleted;
            private CancellationToken cancellationToken;

            private IAppaTaskAsyncEnumerator<TSource> enumerator;
            private AppaTask<bool>.Awaiter awaiter;

            public _Do(
                IAppaTaskAsyncEnumerable<TSource> source,
                Action<TSource> onNext,
                Action<Exception> onError,
                Action onCompleted,
                CancellationToken cancellationToken)
            {
                this.source = source;
                this.onNext = onNext;
                this.onError = onError;
                this.onCompleted = onCompleted;
                this.cancellationToken = cancellationToken;
                TaskTracker.TrackActiveTask(this, 3);
            }

            public TSource Current { get; private set; }

            public AppaTask<bool> MoveNextAsync()
            {
                cancellationToken.ThrowIfCancellationRequested();
                completionSource.Reset();

                var isCompleted = false;
                try
                {
                    if (enumerator == null)
                    {
                        enumerator = source.GetAsyncEnumerator(cancellationToken);
                    }

                    awaiter = enumerator.MoveNextAsync().GetAwaiter();
                    isCompleted = awaiter.IsCompleted;
                }
                catch (Exception ex)
                {
                    CallTrySetExceptionAfterNotification(ex);
                    return new AppaTask<bool>(this, completionSource.Version);
                }

                if (isCompleted)
                {
                    MoveNextCore(this);
                }
                else
                {
                    awaiter.SourceOnCompleted(MoveNextCoreDelegate, this);
                }

                return new AppaTask<bool>(this, completionSource.Version);
            }

            private void CallTrySetExceptionAfterNotification(Exception ex)
            {
                if (onError != null)
                {
                    try
                    {
                        onError(ex);
                    }
                    catch (Exception ex2)
                    {
                        completionSource.TrySetException(ex2);
                        return;
                    }
                }

                completionSource.TrySetException(ex);
            }

            private bool TryGetResultWithNotification<T>(AppaTask<T>.Awaiter awaiter, out T result)
            {
                try
                {
                    result = awaiter.GetResult();
                    return true;
                }
                catch (Exception ex)
                {
                    CallTrySetExceptionAfterNotification(ex);
                    result = default;
                    return false;
                }
            }

            private static void MoveNextCore(object state)
            {
                var self = (_Do)state;

                if (self.TryGetResultWithNotification(self.awaiter, out var result))
                {
                    if (result)
                    {
                        var v = self.enumerator.Current;

                        if (self.onNext != null)
                        {
                            try
                            {
                                self.onNext(v);
                            }
                            catch (Exception ex)
                            {
                                self.CallTrySetExceptionAfterNotification(ex);
                            }
                        }

                        self.Current = v;
                        self.completionSource.TrySetResult(true);
                    }
                    else
                    {
                        if (self.onCompleted != null)
                        {
                            try
                            {
                                self.onCompleted();
                            }
                            catch (Exception ex)
                            {
                                self.CallTrySetExceptionAfterNotification(ex);
                                return;
                            }
                        }

                        self.completionSource.TrySetResult(false);
                    }
                }
            }

            public AppaTask DisposeAsync()
            {
                TaskTracker.RemoveTracking(this);
                if (enumerator != null)
                {
                    return enumerator.DisposeAsync();
                }

                return default;
            }
        }
    }
}
