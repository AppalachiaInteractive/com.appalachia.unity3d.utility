using System;
using System.Threading;
using Appalachia.Utility.Async.Internal;

namespace Appalachia.Utility.Async.Linq
{
    public static partial class AppaTaskAsyncEnumerable
    {
        public static IAppaTaskAsyncEnumerable<TSource> SkipWhile<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, bool> predicate)
        {
            Error.ThrowArgumentNullException(source,    nameof(source));
            Error.ThrowArgumentNullException(predicate, nameof(predicate));

            return new SkipWhile<TSource>(source, predicate);
        }

        public static IAppaTaskAsyncEnumerable<TSource> SkipWhile<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, int, bool> predicate)
        {
            Error.ThrowArgumentNullException(source,    nameof(source));
            Error.ThrowArgumentNullException(predicate, nameof(predicate));

            return new SkipWhileInt<TSource>(source, predicate);
        }

        public static IAppaTaskAsyncEnumerable<TSource> SkipWhileAwait<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<bool>> predicate)
        {
            Error.ThrowArgumentNullException(source,    nameof(source));
            Error.ThrowArgumentNullException(predicate, nameof(predicate));

            return new SkipWhileAwait<TSource>(source, predicate);
        }

        public static IAppaTaskAsyncEnumerable<TSource> SkipWhileAwait<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, int, AppaTask<bool>> predicate)
        {
            Error.ThrowArgumentNullException(source,    nameof(source));
            Error.ThrowArgumentNullException(predicate, nameof(predicate));

            return new SkipWhileIntAwait<TSource>(source, predicate);
        }

        public static IAppaTaskAsyncEnumerable<TSource> SkipWhileAwaitWithCancellation<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<bool>> predicate)
        {
            Error.ThrowArgumentNullException(source,    nameof(source));
            Error.ThrowArgumentNullException(predicate, nameof(predicate));

            return new SkipWhileAwaitWithCancellation<TSource>(source, predicate);
        }

        public static IAppaTaskAsyncEnumerable<TSource> SkipWhileAwaitWithCancellation<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, int, CancellationToken, AppaTask<bool>> predicate)
        {
            Error.ThrowArgumentNullException(source,    nameof(source));
            Error.ThrowArgumentNullException(predicate, nameof(predicate));

            return new SkipWhileIntAwaitWithCancellation<TSource>(source, predicate);
        }
    }

    internal sealed class SkipWhile<TSource> : IAppaTaskAsyncEnumerable<TSource>
    {
        private readonly IAppaTaskAsyncEnumerable<TSource> source;
        private readonly Func<TSource, bool> predicate;

        public SkipWhile(IAppaTaskAsyncEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            this.source = source;
            this.predicate = predicate;
        }

        public IAppaTaskAsyncEnumerator<TSource> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            return new _SkipWhile(source, predicate, cancellationToken);
        }

        private class _SkipWhile : AsyncEnumeratorBase<TSource, TSource>
        {
            private Func<TSource, bool> predicate;

            public _SkipWhile(
                IAppaTaskAsyncEnumerable<TSource> source,
                Func<TSource, bool> predicate,
                CancellationToken cancellationToken) : base(source, cancellationToken)
            {
                this.predicate = predicate;
            }

            protected override bool TryMoveNextCore(bool sourceHasCurrent, out bool result)
            {
                if (sourceHasCurrent)
                {
                    if ((predicate == null) || !predicate(SourceCurrent))
                    {
                        predicate = null;
                        Current = SourceCurrent;
                        result = true;
                        return true;
                    }

                    result = default;
                    return false;
                }

                result = false;
                return true;
            }
        }
    }

    internal sealed class SkipWhileInt<TSource> : IAppaTaskAsyncEnumerable<TSource>
    {
        private readonly IAppaTaskAsyncEnumerable<TSource> source;
        private readonly Func<TSource, int, bool> predicate;

        public SkipWhileInt(IAppaTaskAsyncEnumerable<TSource> source, Func<TSource, int, bool> predicate)
        {
            this.source = source;
            this.predicate = predicate;
        }

        public IAppaTaskAsyncEnumerator<TSource> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            return new _SkipWhileInt(source, predicate, cancellationToken);
        }

        private class _SkipWhileInt : AsyncEnumeratorBase<TSource, TSource>
        {
            private Func<TSource, int, bool> predicate;
            private int index;

            public _SkipWhileInt(
                IAppaTaskAsyncEnumerable<TSource> source,
                Func<TSource, int, bool> predicate,
                CancellationToken cancellationToken) : base(source, cancellationToken)
            {
                this.predicate = predicate;
            }

            protected override bool TryMoveNextCore(bool sourceHasCurrent, out bool result)
            {
                if (sourceHasCurrent)
                {
                    if ((predicate == null) || !predicate(SourceCurrent, checked(index++)))
                    {
                        predicate = null;
                        Current = SourceCurrent;
                        result = true;
                        return true;
                    }

                    result = default;
                    return false;
                }

                result = false;
                return true;
            }
        }
    }

    internal sealed class SkipWhileAwait<TSource> : IAppaTaskAsyncEnumerable<TSource>
    {
        private readonly IAppaTaskAsyncEnumerable<TSource> source;
        private readonly Func<TSource, AppaTask<bool>> predicate;

        public SkipWhileAwait(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<bool>> predicate)
        {
            this.source = source;
            this.predicate = predicate;
        }

        public IAppaTaskAsyncEnumerator<TSource> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            return new _SkipWhileAwait(source, predicate, cancellationToken);
        }

        private class _SkipWhileAwait : AsyncEnumeratorAwaitSelectorBase<TSource, TSource, bool>
        {
            private Func<TSource, AppaTask<bool>> predicate;

            public _SkipWhileAwait(
                IAppaTaskAsyncEnumerable<TSource> source,
                Func<TSource, AppaTask<bool>> predicate,
                CancellationToken cancellationToken) : base(source, cancellationToken)
            {
                this.predicate = predicate;
            }

            protected override AppaTask<bool> TransformAsync(TSource sourceCurrent)
            {
                if (predicate == null)
                {
                    return CompletedTasks.False;
                }

                return predicate(sourceCurrent);
            }

            protected override bool TrySetCurrentCore(bool awaitResult, out bool terminateIteration)
            {
                if (!awaitResult)
                {
                    predicate = null;
                    Current = SourceCurrent;
                    terminateIteration = false;
                    return true;
                }

                terminateIteration = false;
                return false;
            }
        }
    }

    internal sealed class SkipWhileIntAwait<TSource> : IAppaTaskAsyncEnumerable<TSource>
    {
        private readonly IAppaTaskAsyncEnumerable<TSource> source;
        private readonly Func<TSource, int, AppaTask<bool>> predicate;

        public SkipWhileIntAwait(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, int, AppaTask<bool>> predicate)
        {
            this.source = source;
            this.predicate = predicate;
        }

        public IAppaTaskAsyncEnumerator<TSource> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            return new _SkipWhileIntAwait(source, predicate, cancellationToken);
        }

        private class _SkipWhileIntAwait : AsyncEnumeratorAwaitSelectorBase<TSource, TSource, bool>
        {
            private Func<TSource, int, AppaTask<bool>> predicate;
            private int index;

            public _SkipWhileIntAwait(
                IAppaTaskAsyncEnumerable<TSource> source,
                Func<TSource, int, AppaTask<bool>> predicate,
                CancellationToken cancellationToken) : base(source, cancellationToken)
            {
                this.predicate = predicate;
            }

            protected override AppaTask<bool> TransformAsync(TSource sourceCurrent)
            {
                if (predicate == null)
                {
                    return CompletedTasks.False;
                }

                return predicate(sourceCurrent, checked(index++));
            }

            protected override bool TrySetCurrentCore(bool awaitResult, out bool terminateIteration)
            {
                terminateIteration = false;
                if (!awaitResult)
                {
                    predicate = null;
                    Current = SourceCurrent;
                    return true;
                }

                return false;
            }
        }
    }

    internal sealed class SkipWhileAwaitWithCancellation<TSource> : IAppaTaskAsyncEnumerable<TSource>
    {
        private readonly IAppaTaskAsyncEnumerable<TSource> source;
        private readonly Func<TSource, CancellationToken, AppaTask<bool>> predicate;

        public SkipWhileAwaitWithCancellation(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<bool>> predicate)
        {
            this.source = source;
            this.predicate = predicate;
        }

        public IAppaTaskAsyncEnumerator<TSource> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            return new _SkipWhileAwaitWithCancellation(source, predicate, cancellationToken);
        }

        private class
            _SkipWhileAwaitWithCancellation : AsyncEnumeratorAwaitSelectorBase<TSource, TSource, bool>
        {
            private Func<TSource, CancellationToken, AppaTask<bool>> predicate;

            public _SkipWhileAwaitWithCancellation(
                IAppaTaskAsyncEnumerable<TSource> source,
                Func<TSource, CancellationToken, AppaTask<bool>> predicate,
                CancellationToken cancellationToken) : base(source, cancellationToken)
            {
                this.predicate = predicate;
            }

            protected override AppaTask<bool> TransformAsync(TSource sourceCurrent)
            {
                if (predicate == null)
                {
                    return CompletedTasks.False;
                }

                return predicate(sourceCurrent, cancellationToken);
            }

            protected override bool TrySetCurrentCore(bool awaitResult, out bool terminateIteration)
            {
                terminateIteration = false;
                if (!awaitResult)
                {
                    predicate = null;
                    Current = SourceCurrent;
                    return true;
                }

                return false;
            }
        }
    }

    internal sealed class SkipWhileIntAwaitWithCancellation<TSource> : IAppaTaskAsyncEnumerable<TSource>
    {
        private readonly IAppaTaskAsyncEnumerable<TSource> source;
        private readonly Func<TSource, int, CancellationToken, AppaTask<bool>> predicate;

        public SkipWhileIntAwaitWithCancellation(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, int, CancellationToken, AppaTask<bool>> predicate)
        {
            this.source = source;
            this.predicate = predicate;
        }

        public IAppaTaskAsyncEnumerator<TSource> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            return new _SkipWhileIntAwaitWithCancellation(source, predicate, cancellationToken);
        }

        private class
            _SkipWhileIntAwaitWithCancellation : AsyncEnumeratorAwaitSelectorBase<TSource, TSource, bool>
        {
            private Func<TSource, int, CancellationToken, AppaTask<bool>> predicate;
            private int index;

            public _SkipWhileIntAwaitWithCancellation(
                IAppaTaskAsyncEnumerable<TSource> source,
                Func<TSource, int, CancellationToken, AppaTask<bool>> predicate,
                CancellationToken cancellationToken) : base(source, cancellationToken)
            {
                this.predicate = predicate;
            }

            protected override AppaTask<bool> TransformAsync(TSource sourceCurrent)
            {
                if (predicate == null)
                {
                    return CompletedTasks.False;
                }

                return predicate(sourceCurrent, checked(index++), cancellationToken);
            }

            protected override bool TrySetCurrentCore(bool awaitResult, out bool terminateIteration)
            {
                terminateIteration = false;
                if (!awaitResult)
                {
                    predicate = null;
                    Current = SourceCurrent;
                    return true;
                }

                return false;
            }
        }
    }
}
