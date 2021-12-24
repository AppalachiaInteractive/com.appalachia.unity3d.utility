using System;
using System.Threading;
using Appalachia.Utility.Async.Internal;

namespace Appalachia.Utility.Async.Linq
{
    public static partial class AppaTaskAsyncEnumerable
    {
        public static IAppaTaskAsyncEnumerable<TSource> Where<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, bool> predicate)
        {
            Error.ThrowArgumentNullException(source,    nameof(source));
            Error.ThrowArgumentNullException(predicate, nameof(predicate));

            return new Where<TSource>(source, predicate);
        }

        public static IAppaTaskAsyncEnumerable<TSource> Where<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, int, bool> predicate)
        {
            Error.ThrowArgumentNullException(source,    nameof(source));
            Error.ThrowArgumentNullException(predicate, nameof(predicate));

            return new WhereInt<TSource>(source, predicate);
        }

        public static IAppaTaskAsyncEnumerable<TSource> WhereAwait<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<bool>> predicate)
        {
            Error.ThrowArgumentNullException(source,    nameof(source));
            Error.ThrowArgumentNullException(predicate, nameof(predicate));

            return new WhereAwait<TSource>(source, predicate);
        }

        public static IAppaTaskAsyncEnumerable<TSource> WhereAwait<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, int, AppaTask<bool>> predicate)
        {
            Error.ThrowArgumentNullException(source,    nameof(source));
            Error.ThrowArgumentNullException(predicate, nameof(predicate));

            return new WhereIntAwait<TSource>(source, predicate);
        }

        public static IAppaTaskAsyncEnumerable<TSource> WhereAwaitWithCancellation<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<bool>> predicate)
        {
            Error.ThrowArgumentNullException(source,    nameof(source));
            Error.ThrowArgumentNullException(predicate, nameof(predicate));

            return new WhereAwaitWithCancellation<TSource>(source, predicate);
        }

        public static IAppaTaskAsyncEnumerable<TSource> WhereAwaitWithCancellation<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, int, CancellationToken, AppaTask<bool>> predicate)
        {
            Error.ThrowArgumentNullException(source,    nameof(source));
            Error.ThrowArgumentNullException(predicate, nameof(predicate));

            return new WhereIntAwaitWithCancellation<TSource>(source, predicate);
        }
    }

    internal sealed class Where<TSource> : IAppaTaskAsyncEnumerable<TSource>
    {
        private readonly IAppaTaskAsyncEnumerable<TSource> source;
        private readonly Func<TSource, bool> predicate;

        public Where(IAppaTaskAsyncEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            this.source = source;
            this.predicate = predicate;
        }

        public IAppaTaskAsyncEnumerator<TSource> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            return new _Where(source, predicate, cancellationToken);
        }

        private sealed class _Where : MoveNextSource, IAppaTaskAsyncEnumerator<TSource>
        {
            private readonly IAppaTaskAsyncEnumerable<TSource> source;
            private readonly Func<TSource, bool> predicate;
            private readonly CancellationToken cancellationToken;

            private int state = -1;
            private IAppaTaskAsyncEnumerator<TSource> enumerator;
            private AppaTask<bool>.Awaiter awaiter;
            private Action moveNextAction;

            public _Where(
                IAppaTaskAsyncEnumerable<TSource> source,
                Func<TSource, bool> predicate,
                CancellationToken cancellationToken)
            {
                this.source = source;
                this.predicate = predicate;
                this.cancellationToken = cancellationToken;
                moveNextAction = MoveNext;
                TaskTracker.TrackActiveTask(this, 3);
            }

            public TSource Current { get; private set; }

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
                REPEAT:
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
                                Current = enumerator.Current;
                                if (predicate(Current))
                                {
                                    goto CONTINUE;
                                }

                                state = 0;
                                goto REPEAT;
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

    internal sealed class WhereInt<TSource> : IAppaTaskAsyncEnumerable<TSource>
    {
        private readonly IAppaTaskAsyncEnumerable<TSource> source;
        private readonly Func<TSource, int, bool> predicate;

        public WhereInt(IAppaTaskAsyncEnumerable<TSource> source, Func<TSource, int, bool> predicate)
        {
            this.source = source;
            this.predicate = predicate;
        }

        public IAppaTaskAsyncEnumerator<TSource> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            return new _Where(source, predicate, cancellationToken);
        }

        private sealed class _Where : MoveNextSource, IAppaTaskAsyncEnumerator<TSource>
        {
            private readonly IAppaTaskAsyncEnumerable<TSource> source;
            private readonly Func<TSource, int, bool> predicate;
            private readonly CancellationToken cancellationToken;

            private int state = -1;
            private IAppaTaskAsyncEnumerator<TSource> enumerator;
            private AppaTask<bool>.Awaiter awaiter;
            private Action moveNextAction;
            private int index;

            public _Where(
                IAppaTaskAsyncEnumerable<TSource> source,
                Func<TSource, int, bool> predicate,
                CancellationToken cancellationToken)
            {
                this.source = source;
                this.predicate = predicate;
                this.cancellationToken = cancellationToken;
                moveNextAction = MoveNext;
                TaskTracker.TrackActiveTask(this, 3);
            }

            public TSource Current { get; private set; }

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
                REPEAT:
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
                                Current = enumerator.Current;
                                if (predicate(Current, checked(index++)))
                                {
                                    goto CONTINUE;
                                }

                                state = 0;
                                goto REPEAT;
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

    internal sealed class WhereAwait<TSource> : IAppaTaskAsyncEnumerable<TSource>
    {
        private readonly IAppaTaskAsyncEnumerable<TSource> source;
        private readonly Func<TSource, AppaTask<bool>> predicate;

        public WhereAwait(IAppaTaskAsyncEnumerable<TSource> source, Func<TSource, AppaTask<bool>> predicate)
        {
            this.source = source;
            this.predicate = predicate;
        }

        public IAppaTaskAsyncEnumerator<TSource> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            return new _WhereAwait(source, predicate, cancellationToken);
        }

        private sealed class _WhereAwait : MoveNextSource, IAppaTaskAsyncEnumerator<TSource>
        {
            private readonly IAppaTaskAsyncEnumerable<TSource> source;
            private readonly Func<TSource, AppaTask<bool>> predicate;
            private readonly CancellationToken cancellationToken;

            private int state = -1;
            private IAppaTaskAsyncEnumerator<TSource> enumerator;
            private AppaTask<bool>.Awaiter awaiter;
            private AppaTask<bool>.Awaiter awaiter2;
            private Action moveNextAction;

            public _WhereAwait(
                IAppaTaskAsyncEnumerable<TSource> source,
                Func<TSource, AppaTask<bool>> predicate,
                CancellationToken cancellationToken)
            {
                this.source = source;
                this.predicate = predicate;
                this.cancellationToken = cancellationToken;
                moveNextAction = MoveNext;
                TaskTracker.TrackActiveTask(this, 3);
            }

            public TSource Current { get; private set; }

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
                REPEAT:
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
                                Current = enumerator.Current;

                                awaiter2 = predicate(Current).GetAwaiter();
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
                            if (awaiter2.GetResult())
                            {
                                goto CONTINUE;
                            }
                            else
                            {
                                state = 0;
                                goto REPEAT;
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

    internal sealed class WhereIntAwait<TSource> : IAppaTaskAsyncEnumerable<TSource>
    {
        private readonly IAppaTaskAsyncEnumerable<TSource> source;
        private readonly Func<TSource, int, AppaTask<bool>> predicate;

        public WhereIntAwait(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, int, AppaTask<bool>> predicate)
        {
            this.source = source;
            this.predicate = predicate;
        }

        public IAppaTaskAsyncEnumerator<TSource> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            return new _WhereAwait(source, predicate, cancellationToken);
        }

        private sealed class _WhereAwait : MoveNextSource, IAppaTaskAsyncEnumerator<TSource>
        {
            private readonly IAppaTaskAsyncEnumerable<TSource> source;
            private readonly Func<TSource, int, AppaTask<bool>> predicate;
            private readonly CancellationToken cancellationToken;

            private int state = -1;
            private IAppaTaskAsyncEnumerator<TSource> enumerator;
            private AppaTask<bool>.Awaiter awaiter;
            private AppaTask<bool>.Awaiter awaiter2;
            private Action moveNextAction;
            private int index;

            public _WhereAwait(
                IAppaTaskAsyncEnumerable<TSource> source,
                Func<TSource, int, AppaTask<bool>> predicate,
                CancellationToken cancellationToken)
            {
                this.source = source;
                this.predicate = predicate;
                this.cancellationToken = cancellationToken;
                moveNextAction = MoveNext;
                TaskTracker.TrackActiveTask(this, 3);
            }

            public TSource Current { get; private set; }

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
                REPEAT:
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
                                Current = enumerator.Current;

                                awaiter2 = predicate(Current, checked(index++)).GetAwaiter();
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
                            if (awaiter2.GetResult())
                            {
                                goto CONTINUE;
                            }
                            else
                            {
                                state = 0;
                                goto REPEAT;
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

    internal sealed class WhereAwaitWithCancellation<TSource> : IAppaTaskAsyncEnumerable<TSource>
    {
        private readonly IAppaTaskAsyncEnumerable<TSource> source;
        private readonly Func<TSource, CancellationToken, AppaTask<bool>> predicate;

        public WhereAwaitWithCancellation(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<bool>> predicate)
        {
            this.source = source;
            this.predicate = predicate;
        }

        public IAppaTaskAsyncEnumerator<TSource> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            return new _WhereAwaitWithCancellation(source, predicate, cancellationToken);
        }

        private sealed class _WhereAwaitWithCancellation : MoveNextSource, IAppaTaskAsyncEnumerator<TSource>
        {
            private readonly IAppaTaskAsyncEnumerable<TSource> source;
            private readonly Func<TSource, CancellationToken, AppaTask<bool>> predicate;
            private readonly CancellationToken cancellationToken;

            private int state = -1;
            private IAppaTaskAsyncEnumerator<TSource> enumerator;
            private AppaTask<bool>.Awaiter awaiter;
            private AppaTask<bool>.Awaiter awaiter2;
            private Action moveNextAction;

            public _WhereAwaitWithCancellation(
                IAppaTaskAsyncEnumerable<TSource> source,
                Func<TSource, CancellationToken, AppaTask<bool>> predicate,
                CancellationToken cancellationToken)
            {
                this.source = source;
                this.predicate = predicate;
                this.cancellationToken = cancellationToken;
                moveNextAction = MoveNext;
                TaskTracker.TrackActiveTask(this, 3);
            }

            public TSource Current { get; private set; }

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
                REPEAT:
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
                                Current = enumerator.Current;

                                awaiter2 = predicate(Current, cancellationToken).GetAwaiter();
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
                            if (awaiter2.GetResult())
                            {
                                goto CONTINUE;
                            }
                            else
                            {
                                state = 0;
                                goto REPEAT;
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

    internal sealed class WhereIntAwaitWithCancellation<TSource> : IAppaTaskAsyncEnumerable<TSource>
    {
        private readonly IAppaTaskAsyncEnumerable<TSource> source;
        private readonly Func<TSource, int, CancellationToken, AppaTask<bool>> predicate;

        public WhereIntAwaitWithCancellation(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, int, CancellationToken, AppaTask<bool>> predicate)
        {
            this.source = source;
            this.predicate = predicate;
        }

        public IAppaTaskAsyncEnumerator<TSource> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            return new _WhereAwaitWithCancellation(source, predicate, cancellationToken);
        }

        private sealed class _WhereAwaitWithCancellation : MoveNextSource, IAppaTaskAsyncEnumerator<TSource>
        {
            private readonly IAppaTaskAsyncEnumerable<TSource> source;
            private readonly Func<TSource, int, CancellationToken, AppaTask<bool>> predicate;
            private readonly CancellationToken cancellationToken;

            private int state = -1;
            private IAppaTaskAsyncEnumerator<TSource> enumerator;
            private AppaTask<bool>.Awaiter awaiter;
            private AppaTask<bool>.Awaiter awaiter2;
            private Action moveNextAction;
            private int index;

            public _WhereAwaitWithCancellation(
                IAppaTaskAsyncEnumerable<TSource> source,
                Func<TSource, int, CancellationToken, AppaTask<bool>> predicate,
                CancellationToken cancellationToken)
            {
                this.source = source;
                this.predicate = predicate;
                this.cancellationToken = cancellationToken;
                moveNextAction = MoveNext;
                TaskTracker.TrackActiveTask(this, 3);
            }

            public TSource Current { get; private set; }

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
                REPEAT:
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
                                Current = enumerator.Current;

                                awaiter2 = predicate(Current, checked(index++), cancellationToken)
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
                            if (awaiter2.GetResult())
                            {
                                goto CONTINUE;
                            }
                            else
                            {
                                state = 0;
                                goto REPEAT;
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
}
