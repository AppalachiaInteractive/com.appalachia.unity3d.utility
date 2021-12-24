﻿using System;
using System.Collections.Generic;
using System.Threading;
using Appalachia.Utility.Async.Internal;

namespace Appalachia.Utility.Async.Linq
{
    public static partial class AppaTaskAsyncEnumerable
    {
        public static IAppaTaskAsyncEnumerable<TSource> DistinctUntilChanged<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source)
        {
            return DistinctUntilChanged(source, EqualityComparer<TSource>.Default);
        }

        public static IAppaTaskAsyncEnumerable<TSource> DistinctUntilChanged<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            IEqualityComparer<TSource> comparer)
        {
            Error.ThrowArgumentNullException(source,   nameof(source));
            Error.ThrowArgumentNullException(comparer, nameof(comparer));

            return new DistinctUntilChanged<TSource>(source, comparer);
        }

        public static IAppaTaskAsyncEnumerable<TSource> DistinctUntilChanged<TSource, TKey>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, TKey> keySelector)
        {
            return DistinctUntilChanged(source, keySelector, EqualityComparer<TKey>.Default);
        }

        public static IAppaTaskAsyncEnumerable<TSource> DistinctUntilChanged<TSource, TKey>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            IEqualityComparer<TKey> comparer)
        {
            Error.ThrowArgumentNullException(source,      nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));
            Error.ThrowArgumentNullException(comparer,    nameof(comparer));

            return new DistinctUntilChanged<TSource, TKey>(source, keySelector, comparer);
        }

        public static IAppaTaskAsyncEnumerable<TSource> DistinctUntilChangedAwait<TSource, TKey>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<TKey>> keySelector)
        {
            return DistinctUntilChangedAwait(source, keySelector, EqualityComparer<TKey>.Default);
        }

        public static IAppaTaskAsyncEnumerable<TSource> DistinctUntilChangedAwait<TSource, TKey>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<TKey>> keySelector,
            IEqualityComparer<TKey> comparer)
        {
            Error.ThrowArgumentNullException(source,      nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));
            Error.ThrowArgumentNullException(comparer,    nameof(comparer));

            return new DistinctUntilChangedAwait<TSource, TKey>(source, keySelector, comparer);
        }

        public static IAppaTaskAsyncEnumerable<TSource>
            DistinctUntilChangedAwaitWithCancellation<TSource, TKey>(
                this IAppaTaskAsyncEnumerable<TSource> source,
                Func<TSource, CancellationToken, AppaTask<TKey>> keySelector)
        {
            return DistinctUntilChangedAwaitWithCancellation(
                source,
                keySelector,
                EqualityComparer<TKey>.Default
            );
        }

        public static IAppaTaskAsyncEnumerable<TSource>
            DistinctUntilChangedAwaitWithCancellation<TSource, TKey>(
                this IAppaTaskAsyncEnumerable<TSource> source,
                Func<TSource, CancellationToken, AppaTask<TKey>> keySelector,
                IEqualityComparer<TKey> comparer)
        {
            Error.ThrowArgumentNullException(source,      nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));
            Error.ThrowArgumentNullException(comparer,    nameof(comparer));

            return new DistinctUntilChangedAwaitWithCancellation<TSource, TKey>(
                source,
                keySelector,
                comparer
            );
        }
    }

    internal sealed class DistinctUntilChanged<TSource> : IAppaTaskAsyncEnumerable<TSource>
    {
        private readonly IAppaTaskAsyncEnumerable<TSource> source;
        private readonly IEqualityComparer<TSource> comparer;

        public DistinctUntilChanged(
            IAppaTaskAsyncEnumerable<TSource> source,
            IEqualityComparer<TSource> comparer)
        {
            this.source = source;
            this.comparer = comparer;
        }

        public IAppaTaskAsyncEnumerator<TSource> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            return new _DistinctUntilChanged(source, comparer, cancellationToken);
        }

        private sealed class _DistinctUntilChanged : MoveNextSource, IAppaTaskAsyncEnumerator<TSource>
        {
            private readonly IAppaTaskAsyncEnumerable<TSource> source;
            private readonly IEqualityComparer<TSource> comparer;
            private readonly CancellationToken cancellationToken;

            private int state = -1;
            private IAppaTaskAsyncEnumerator<TSource> enumerator;
            private AppaTask<bool>.Awaiter awaiter;
            private Action moveNextAction;

            public _DistinctUntilChanged(
                IAppaTaskAsyncEnumerable<TSource> source,
                IEqualityComparer<TSource> comparer,
                CancellationToken cancellationToken)
            {
                this.source = source;
                this.comparer = comparer;
                this.cancellationToken = cancellationToken;
                moveNextAction = MoveNext;
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
                            awaiter = enumerator.MoveNextAsync().GetAwaiter();
                            if (awaiter.IsCompleted)
                            {
                                goto case -3;
                            }
                            else
                            {
                                state = -3;
                                awaiter.UnsafeOnCompleted(moveNextAction);
                                return;
                            }
                        case -3: // first
                            if (awaiter.GetResult())
                            {
                                Current = enumerator.Current;
                                goto CONTINUE;
                            }
                            else
                            {
                                goto DONE;
                            }
                        case 0: // normal
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
                                var v = enumerator.Current;
                                if (!comparer.Equals(Current, v))
                                {
                                    Current = v;
                                    goto CONTINUE;
                                }

                                state = 0;
                                goto REPEAT;
                            }
                            else
                            {
                                goto DONE;
                            }
                        case -2:
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
                return enumerator.DisposeAsync();
            }
        }
    }

    internal sealed class DistinctUntilChanged<TSource, TKey> : IAppaTaskAsyncEnumerable<TSource>
    {
        private readonly IAppaTaskAsyncEnumerable<TSource> source;
        private readonly Func<TSource, TKey> keySelector;
        private readonly IEqualityComparer<TKey> comparer;

        public DistinctUntilChanged(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            IEqualityComparer<TKey> comparer)
        {
            this.source = source;
            this.keySelector = keySelector;
            this.comparer = comparer;
        }

        public IAppaTaskAsyncEnumerator<TSource> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            return new _DistinctUntilChanged(source, keySelector, comparer, cancellationToken);
        }

        private sealed class _DistinctUntilChanged : MoveNextSource, IAppaTaskAsyncEnumerator<TSource>
        {
            private readonly IAppaTaskAsyncEnumerable<TSource> source;
            private readonly Func<TSource, TKey> keySelector;
            private readonly IEqualityComparer<TKey> comparer;
            private readonly CancellationToken cancellationToken;

            private int state = -1;
            private IAppaTaskAsyncEnumerator<TSource> enumerator;
            private AppaTask<bool>.Awaiter awaiter;
            private Action moveNextAction;
            private TKey prev;

            public _DistinctUntilChanged(
                IAppaTaskAsyncEnumerable<TSource> source,
                Func<TSource, TKey> keySelector,
                IEqualityComparer<TKey> comparer,
                CancellationToken cancellationToken)
            {
                this.source = source;
                this.keySelector = keySelector;
                this.comparer = comparer;
                this.cancellationToken = cancellationToken;
                moveNextAction = MoveNext;
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
                            awaiter = enumerator.MoveNextAsync().GetAwaiter();
                            if (awaiter.IsCompleted)
                            {
                                goto case -3;
                            }
                            else
                            {
                                state = -3;
                                awaiter.UnsafeOnCompleted(moveNextAction);
                                return;
                            }
                        case -3: // first
                            if (awaiter.GetResult())
                            {
                                Current = enumerator.Current;
                                goto CONTINUE;
                            }
                            else
                            {
                                goto DONE;
                            }
                        case 0: // normal
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
                                var v = enumerator.Current;
                                var key = keySelector(v);
                                if (!comparer.Equals(prev, key))
                                {
                                    prev = key;
                                    Current = v;
                                    goto CONTINUE;
                                }

                                state = 0;
                                goto REPEAT;
                            }
                            else
                            {
                                goto DONE;
                            }
                        case -2:
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
                return enumerator.DisposeAsync();
            }
        }
    }

    internal sealed class DistinctUntilChangedAwait<TSource, TKey> : IAppaTaskAsyncEnumerable<TSource>
    {
        private readonly IAppaTaskAsyncEnumerable<TSource> source;
        private readonly Func<TSource, AppaTask<TKey>> keySelector;
        private readonly IEqualityComparer<TKey> comparer;

        public DistinctUntilChangedAwait(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<TKey>> keySelector,
            IEqualityComparer<TKey> comparer)
        {
            this.source = source;
            this.keySelector = keySelector;
            this.comparer = comparer;
        }

        public IAppaTaskAsyncEnumerator<TSource> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            return new _DistinctUntilChangedAwait(source, keySelector, comparer, cancellationToken);
        }

        private sealed class _DistinctUntilChangedAwait : MoveNextSource, IAppaTaskAsyncEnumerator<TSource>
        {
            private readonly IAppaTaskAsyncEnumerable<TSource> source;
            private readonly Func<TSource, AppaTask<TKey>> keySelector;
            private readonly IEqualityComparer<TKey> comparer;
            private readonly CancellationToken cancellationToken;

            private int state = -1;
            private IAppaTaskAsyncEnumerator<TSource> enumerator;
            private AppaTask<bool>.Awaiter awaiter;
            private AppaTask<TKey>.Awaiter awaiter2;
            private Action moveNextAction;
            private TSource enumeratorCurrent;
            private TKey prev;

            public _DistinctUntilChangedAwait(
                IAppaTaskAsyncEnumerable<TSource> source,
                Func<TSource, AppaTask<TKey>> keySelector,
                IEqualityComparer<TKey> comparer,
                CancellationToken cancellationToken)
            {
                this.source = source;
                this.keySelector = keySelector;
                this.comparer = comparer;
                this.cancellationToken = cancellationToken;
                moveNextAction = MoveNext;
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
                            awaiter = enumerator.MoveNextAsync().GetAwaiter();
                            if (awaiter.IsCompleted)
                            {
                                goto case -3;
                            }
                            else
                            {
                                state = -3;
                                awaiter.UnsafeOnCompleted(moveNextAction);
                                return;
                            }
                        case -3: // first
                            if (awaiter.GetResult())
                            {
                                Current = enumerator.Current;
                                goto CONTINUE;
                            }
                            else
                            {
                                goto DONE;
                            }
                        case 0: // normal
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
                                enumeratorCurrent = enumerator.Current;
                                awaiter2 = keySelector(enumeratorCurrent).GetAwaiter();
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
                            var key = awaiter2.GetResult();
                            if (!comparer.Equals(prev, key))
                            {
                                prev = key;
                                Current = enumeratorCurrent;
                                goto CONTINUE;
                            }
                            else
                            {
                                state = 0;
                                goto REPEAT;
                            }
                        case -2:
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
                return enumerator.DisposeAsync();
            }
        }
    }

    internal sealed class
        DistinctUntilChangedAwaitWithCancellation<TSource, TKey> : IAppaTaskAsyncEnumerable<TSource>
    {
        private readonly IAppaTaskAsyncEnumerable<TSource> source;
        private readonly Func<TSource, CancellationToken, AppaTask<TKey>> keySelector;
        private readonly IEqualityComparer<TKey> comparer;

        public DistinctUntilChangedAwaitWithCancellation(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<TKey>> keySelector,
            IEqualityComparer<TKey> comparer)
        {
            this.source = source;
            this.keySelector = keySelector;
            this.comparer = comparer;
        }

        public IAppaTaskAsyncEnumerator<TSource> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            return new _DistinctUntilChangedAwaitWithCancellation(
                source,
                keySelector,
                comparer,
                cancellationToken
            );
        }

        private sealed class _DistinctUntilChangedAwaitWithCancellation : MoveNextSource,
            IAppaTaskAsyncEnumerator<TSource>
        {
            private readonly IAppaTaskAsyncEnumerable<TSource> source;
            private readonly Func<TSource, CancellationToken, AppaTask<TKey>> keySelector;
            private readonly IEqualityComparer<TKey> comparer;
            private readonly CancellationToken cancellationToken;

            private int state = -1;
            private IAppaTaskAsyncEnumerator<TSource> enumerator;
            private AppaTask<bool>.Awaiter awaiter;
            private AppaTask<TKey>.Awaiter awaiter2;
            private Action moveNextAction;
            private TSource enumeratorCurrent;
            private TKey prev;

            public _DistinctUntilChangedAwaitWithCancellation(
                IAppaTaskAsyncEnumerable<TSource> source,
                Func<TSource, CancellationToken, AppaTask<TKey>> keySelector,
                IEqualityComparer<TKey> comparer,
                CancellationToken cancellationToken)
            {
                this.source = source;
                this.keySelector = keySelector;
                this.comparer = comparer;
                this.cancellationToken = cancellationToken;
                moveNextAction = MoveNext;
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
                            awaiter = enumerator.MoveNextAsync().GetAwaiter();
                            if (awaiter.IsCompleted)
                            {
                                goto case -3;
                            }
                            else
                            {
                                state = -3;
                                awaiter.UnsafeOnCompleted(moveNextAction);
                                return;
                            }
                        case -3: // first
                            if (awaiter.GetResult())
                            {
                                Current = enumerator.Current;
                                goto CONTINUE;
                            }
                            else
                            {
                                goto DONE;
                            }
                        case 0: // normal
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
                                enumeratorCurrent = enumerator.Current;
                                awaiter2 = keySelector(enumeratorCurrent, cancellationToken).GetAwaiter();
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
                            var key = awaiter2.GetResult();
                            if (!comparer.Equals(prev, key))
                            {
                                prev = key;
                                Current = enumeratorCurrent;
                                goto CONTINUE;
                            }
                            else
                            {
                                state = 0;
                                goto REPEAT;
                            }
                        case -2:
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
                return enumerator.DisposeAsync();
            }
        }
    }
}
