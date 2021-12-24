using System;
using System.Threading;
using Appalachia.Utility.Async.Internal;

namespace Appalachia.Utility.Async.Linq
{
    public static partial class AppaTaskAsyncEnumerable
    {
        public static IAppaTaskAsyncEnumerable<TResult> Select<TSource, TResult>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, TResult> selector)
        {
            Error.ThrowArgumentNullException(source,   nameof(source));
            Error.ThrowArgumentNullException(selector, nameof(selector));

            return new Select<TSource, TResult>(source, selector);
        }

        public static IAppaTaskAsyncEnumerable<TResult> Select<TSource, TResult>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, int, TResult> selector)
        {
            Error.ThrowArgumentNullException(source,   nameof(source));
            Error.ThrowArgumentNullException(selector, nameof(selector));

            return new SelectInt<TSource, TResult>(source, selector);
        }

        public static IAppaTaskAsyncEnumerable<TResult> SelectAwait<TSource, TResult>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<TResult>> selector)
        {
            Error.ThrowArgumentNullException(source,   nameof(source));
            Error.ThrowArgumentNullException(selector, nameof(selector));

            return new SelectAwait<TSource, TResult>(source, selector);
        }

        public static IAppaTaskAsyncEnumerable<TResult> SelectAwait<TSource, TResult>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, int, AppaTask<TResult>> selector)
        {
            Error.ThrowArgumentNullException(source,   nameof(source));
            Error.ThrowArgumentNullException(selector, nameof(selector));

            return new SelectIntAwait<TSource, TResult>(source, selector);
        }

        public static IAppaTaskAsyncEnumerable<TResult> SelectAwaitWithCancellation<TSource, TResult>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<TResult>> selector)
        {
            Error.ThrowArgumentNullException(source,   nameof(source));
            Error.ThrowArgumentNullException(selector, nameof(selector));

            return new SelectAwaitWithCancellation<TSource, TResult>(source, selector);
        }

        public static IAppaTaskAsyncEnumerable<TResult> SelectAwaitWithCancellation<TSource, TResult>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, int, CancellationToken, AppaTask<TResult>> selector)
        {
            Error.ThrowArgumentNullException(source,   nameof(source));
            Error.ThrowArgumentNullException(selector, nameof(selector));

            return new SelectIntAwaitWithCancellation<TSource, TResult>(source, selector);
        }
    }

    internal sealed class Select<TSource, TResult> : IAppaTaskAsyncEnumerable<TResult>
    {
        private readonly IAppaTaskAsyncEnumerable<TSource> source;
        private readonly Func<TSource, TResult> selector;

        public Select(IAppaTaskAsyncEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            this.source = source;
            this.selector = selector;
        }

        public IAppaTaskAsyncEnumerator<TResult> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            return new _Select(source, selector, cancellationToken);
        }

        private sealed class _Select : MoveNextSource, IAppaTaskAsyncEnumerator<TResult>
        {
            private readonly IAppaTaskAsyncEnumerable<TSource> source;
            private readonly Func<TSource, TResult> selector;
            private readonly CancellationToken cancellationToken;

            private int state = -1;
            private IAppaTaskAsyncEnumerator<TSource> enumerator;
            private AppaTask<bool>.Awaiter awaiter;
            private Action moveNextAction;

            public _Select(
                IAppaTaskAsyncEnumerable<TSource> source,
                Func<TSource, TResult> selector,
                CancellationToken cancellationToken)
            {
                this.source = source;
                this.selector = selector;
                this.cancellationToken = cancellationToken;
                moveNextAction = MoveNext;
                TaskTracker.TrackActiveTask(this, 3);
            }

            public TResult Current { get; private set; }

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
                            enumerator = source.GetAsyncEnumerator(cancellationToken);
                            goto case 0;
                        case 0:
                            awaiter = enumerator.MoveNextAsync().GetAwaiter();
                            if (awaiter.IsCompleted)
                            {
                                goto case 1;
                            }
                            else
                            {
                                state = 1;
                                awaiter.UnsafeOnCompleted(moveNextAction);
                                return;
                            }
                        case 1:
                            if (awaiter.GetResult())
                            {
                                Current = selector(enumerator.Current);
                                goto CONTINUE;
                            }
                            else
                            {
                                goto DONE;
                            }
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
                return;

                CONTINUE:
                state = 0;
                completionSource.TrySetResult(true);
            }

            public AppaTask DisposeAsync()
            {
                TaskTracker.RemoveTracking(this);
                return enumerator.DisposeAsync();
            }
        }
    }

    internal sealed class SelectInt<TSource, TResult> : IAppaTaskAsyncEnumerable<TResult>
    {
        private readonly IAppaTaskAsyncEnumerable<TSource> source;
        private readonly Func<TSource, int, TResult> selector;

        public SelectInt(IAppaTaskAsyncEnumerable<TSource> source, Func<TSource, int, TResult> selector)
        {
            this.source = source;
            this.selector = selector;
        }

        public IAppaTaskAsyncEnumerator<TResult> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            return new _Select(source, selector, cancellationToken);
        }

        private sealed class _Select : MoveNextSource, IAppaTaskAsyncEnumerator<TResult>
        {
            private readonly IAppaTaskAsyncEnumerable<TSource> source;
            private readonly Func<TSource, int, TResult> selector;
            private readonly CancellationToken cancellationToken;

            private int state = -1;
            private IAppaTaskAsyncEnumerator<TSource> enumerator;
            private AppaTask<bool>.Awaiter awaiter;
            private Action moveNextAction;
            private int index;

            public _Select(
                IAppaTaskAsyncEnumerable<TSource> source,
                Func<TSource, int, TResult> selector,
                CancellationToken cancellationToken)
            {
                this.source = source;
                this.selector = selector;
                this.cancellationToken = cancellationToken;
                moveNextAction = MoveNext;
                TaskTracker.TrackActiveTask(this, 3);
            }

            public TResult Current { get; private set; }

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
                            enumerator = source.GetAsyncEnumerator(cancellationToken);
                            goto case 0;
                        case 0:
                            awaiter = enumerator.MoveNextAsync().GetAwaiter();
                            if (awaiter.IsCompleted)
                            {
                                goto case 1;
                            }
                            else
                            {
                                state = 1;
                                awaiter.UnsafeOnCompleted(moveNextAction);
                                return;
                            }
                        case 1:
                            if (awaiter.GetResult())
                            {
                                Current = selector(enumerator.Current, checked(index++));
                                goto CONTINUE;
                            }
                            else
                            {
                                goto DONE;
                            }
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
                return;

                CONTINUE:
                state = 0;
                completionSource.TrySetResult(true);
            }

            public AppaTask DisposeAsync()
            {
                TaskTracker.RemoveTracking(this);
                return enumerator.DisposeAsync();
            }
        }
    }

    internal sealed class SelectAwait<TSource, TResult> : IAppaTaskAsyncEnumerable<TResult>
    {
        private readonly IAppaTaskAsyncEnumerable<TSource> source;
        private readonly Func<TSource, AppaTask<TResult>> selector;

        public SelectAwait(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<TResult>> selector)
        {
            this.source = source;
            this.selector = selector;
        }

        public IAppaTaskAsyncEnumerator<TResult> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            return new _SelectAwait(source, selector, cancellationToken);
        }

        private sealed class _SelectAwait : MoveNextSource, IAppaTaskAsyncEnumerator<TResult>
        {
            private readonly IAppaTaskAsyncEnumerable<TSource> source;
            private readonly Func<TSource, AppaTask<TResult>> selector;
            private readonly CancellationToken cancellationToken;

            private int state = -1;
            private IAppaTaskAsyncEnumerator<TSource> enumerator;
            private AppaTask<bool>.Awaiter awaiter;
            private AppaTask<TResult>.Awaiter awaiter2;
            private Action moveNextAction;

            public _SelectAwait(
                IAppaTaskAsyncEnumerable<TSource> source,
                Func<TSource, AppaTask<TResult>> selector,
                CancellationToken cancellationToken)
            {
                this.source = source;
                this.selector = selector;
                this.cancellationToken = cancellationToken;
                moveNextAction = MoveNext;
                TaskTracker.TrackActiveTask(this, 3);
            }

            public TResult Current { get; private set; }

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
                            enumerator = source.GetAsyncEnumerator(cancellationToken);
                            goto case 0;
                        case 0:
                            awaiter = enumerator.MoveNextAsync().GetAwaiter();
                            if (awaiter.IsCompleted)
                            {
                                goto case 1;
                            }
                            else
                            {
                                state = 1;
                                awaiter.UnsafeOnCompleted(moveNextAction);
                                return;
                            }
                        case 1:
                            if (awaiter.GetResult())
                            {
                                awaiter2 = selector(enumerator.Current).GetAwaiter();
                                if (awaiter2.IsCompleted)
                                {
                                    goto case 2;
                                }

                                state = 2;
                                awaiter2.UnsafeOnCompleted(moveNextAction);
                                return;
                            }
                            else
                            {
                                goto DONE;
                            }
                        case 2:
                            Current = awaiter2.GetResult();
                            goto CONTINUE;
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
                return;

                CONTINUE:
                state = 0;
                completionSource.TrySetResult(true);
            }

            public AppaTask DisposeAsync()
            {
                TaskTracker.RemoveTracking(this);
                return enumerator.DisposeAsync();
            }
        }
    }

    internal sealed class SelectIntAwait<TSource, TResult> : IAppaTaskAsyncEnumerable<TResult>
    {
        private readonly IAppaTaskAsyncEnumerable<TSource> source;
        private readonly Func<TSource, int, AppaTask<TResult>> selector;

        public SelectIntAwait(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, int, AppaTask<TResult>> selector)
        {
            this.source = source;
            this.selector = selector;
        }

        public IAppaTaskAsyncEnumerator<TResult> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            return new _SelectAwait(source, selector, cancellationToken);
        }

        private sealed class _SelectAwait : MoveNextSource, IAppaTaskAsyncEnumerator<TResult>
        {
            private readonly IAppaTaskAsyncEnumerable<TSource> source;
            private readonly Func<TSource, int, AppaTask<TResult>> selector;
            private readonly CancellationToken cancellationToken;

            private int state = -1;
            private IAppaTaskAsyncEnumerator<TSource> enumerator;
            private AppaTask<bool>.Awaiter awaiter;
            private AppaTask<TResult>.Awaiter awaiter2;
            private Action moveNextAction;
            private int index;

            public _SelectAwait(
                IAppaTaskAsyncEnumerable<TSource> source,
                Func<TSource, int, AppaTask<TResult>> selector,
                CancellationToken cancellationToken)
            {
                this.source = source;
                this.selector = selector;
                this.cancellationToken = cancellationToken;
                moveNextAction = MoveNext;
                TaskTracker.TrackActiveTask(this, 3);
            }

            public TResult Current { get; private set; }

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
                            enumerator = source.GetAsyncEnumerator(cancellationToken);
                            goto case 0;
                        case 0:
                            awaiter = enumerator.MoveNextAsync().GetAwaiter();
                            if (awaiter.IsCompleted)
                            {
                                goto case 1;
                            }
                            else
                            {
                                state = 1;
                                awaiter.UnsafeOnCompleted(moveNextAction);
                                return;
                            }
                        case 1:
                            if (awaiter.GetResult())
                            {
                                awaiter2 = selector(enumerator.Current, checked(index++)).GetAwaiter();
                                if (awaiter2.IsCompleted)
                                {
                                    goto case 2;
                                }

                                state = 2;
                                awaiter2.UnsafeOnCompleted(moveNextAction);
                                return;
                            }
                            else
                            {
                                goto DONE;
                            }
                        case 2:
                            Current = awaiter2.GetResult();
                            goto CONTINUE;
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
                return;

                CONTINUE:
                state = 0;
                completionSource.TrySetResult(true);
            }

            public AppaTask DisposeAsync()
            {
                TaskTracker.RemoveTracking(this);
                return enumerator.DisposeAsync();
            }
        }
    }

    internal sealed class SelectAwaitWithCancellation<TSource, TResult> : IAppaTaskAsyncEnumerable<TResult>
    {
        private readonly IAppaTaskAsyncEnumerable<TSource> source;
        private readonly Func<TSource, CancellationToken, AppaTask<TResult>> selector;

        public SelectAwaitWithCancellation(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<TResult>> selector)
        {
            this.source = source;
            this.selector = selector;
        }

        public IAppaTaskAsyncEnumerator<TResult> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            return new _SelectAwaitWithCancellation(source, selector, cancellationToken);
        }

        private sealed class _SelectAwaitWithCancellation : MoveNextSource, IAppaTaskAsyncEnumerator<TResult>
        {
            private readonly IAppaTaskAsyncEnumerable<TSource> source;
            private readonly Func<TSource, CancellationToken, AppaTask<TResult>> selector;
            private readonly CancellationToken cancellationToken;

            private int state = -1;
            private IAppaTaskAsyncEnumerator<TSource> enumerator;
            private AppaTask<bool>.Awaiter awaiter;
            private AppaTask<TResult>.Awaiter awaiter2;
            private Action moveNextAction;

            public _SelectAwaitWithCancellation(
                IAppaTaskAsyncEnumerable<TSource> source,
                Func<TSource, CancellationToken, AppaTask<TResult>> selector,
                CancellationToken cancellationToken)
            {
                this.source = source;
                this.selector = selector;
                this.cancellationToken = cancellationToken;
                moveNextAction = MoveNext;
                TaskTracker.TrackActiveTask(this, 3);
            }

            public TResult Current { get; private set; }

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
                            enumerator = source.GetAsyncEnumerator(cancellationToken);
                            goto case 0;
                        case 0:
                            awaiter = enumerator.MoveNextAsync().GetAwaiter();
                            if (awaiter.IsCompleted)
                            {
                                goto case 1;
                            }
                            else
                            {
                                state = 1;
                                awaiter.UnsafeOnCompleted(moveNextAction);
                                return;
                            }
                        case 1:
                            if (awaiter.GetResult())
                            {
                                awaiter2 = selector(enumerator.Current, cancellationToken).GetAwaiter();
                                if (awaiter2.IsCompleted)
                                {
                                    goto case 2;
                                }

                                state = 2;
                                awaiter2.UnsafeOnCompleted(moveNextAction);
                                return;
                            }
                            else
                            {
                                goto DONE;
                            }
                        case 2:
                            Current = awaiter2.GetResult();
                            goto CONTINUE;
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
                return;

                CONTINUE:
                state = 0;
                completionSource.TrySetResult(true);
            }

            public AppaTask DisposeAsync()
            {
                TaskTracker.RemoveTracking(this);
                return enumerator.DisposeAsync();
            }
        }
    }

    internal sealed class SelectIntAwaitWithCancellation<TSource, TResult> : IAppaTaskAsyncEnumerable<TResult>
    {
        private readonly IAppaTaskAsyncEnumerable<TSource> source;
        private readonly Func<TSource, int, CancellationToken, AppaTask<TResult>> selector;

        public SelectIntAwaitWithCancellation(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, int, CancellationToken, AppaTask<TResult>> selector)
        {
            this.source = source;
            this.selector = selector;
        }

        public IAppaTaskAsyncEnumerator<TResult> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            return new _SelectAwaitWithCancellation(source, selector, cancellationToken);
        }

        private sealed class _SelectAwaitWithCancellation : MoveNextSource, IAppaTaskAsyncEnumerator<TResult>
        {
            private readonly IAppaTaskAsyncEnumerable<TSource> source;
            private readonly Func<TSource, int, CancellationToken, AppaTask<TResult>> selector;
            private readonly CancellationToken cancellationToken;

            private int state = -1;
            private IAppaTaskAsyncEnumerator<TSource> enumerator;
            private AppaTask<bool>.Awaiter awaiter;
            private AppaTask<TResult>.Awaiter awaiter2;
            private Action moveNextAction;
            private int index;

            public _SelectAwaitWithCancellation(
                IAppaTaskAsyncEnumerable<TSource> source,
                Func<TSource, int, CancellationToken, AppaTask<TResult>> selector,
                CancellationToken cancellationToken)
            {
                this.source = source;
                this.selector = selector;
                this.cancellationToken = cancellationToken;
                moveNextAction = MoveNext;
                TaskTracker.TrackActiveTask(this, 3);
            }

            public TResult Current { get; private set; }

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
                            enumerator = source.GetAsyncEnumerator(cancellationToken);
                            goto case 0;
                        case 0:
                            awaiter = enumerator.MoveNextAsync().GetAwaiter();
                            if (awaiter.IsCompleted)
                            {
                                goto case 1;
                            }
                            else
                            {
                                state = 1;
                                awaiter.UnsafeOnCompleted(moveNextAction);
                                return;
                            }
                        case 1:
                            if (awaiter.GetResult())
                            {
                                awaiter2 = selector(enumerator.Current, checked(index++), cancellationToken)
                                   .GetAwaiter();
                                if (awaiter2.IsCompleted)
                                {
                                    goto case 2;
                                }

                                state = 2;
                                awaiter2.UnsafeOnCompleted(moveNextAction);
                                return;
                            }
                            else
                            {
                                goto DONE;
                            }
                        case 2:
                            Current = awaiter2.GetResult();
                            goto CONTINUE;
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
                return;

                CONTINUE:
                state = 0;
                completionSource.TrySetResult(true);
            }

            public AppaTask DisposeAsync()
            {
                TaskTracker.RemoveTracking(this);
                return enumerator.DisposeAsync();
            }
        }
    }
}
