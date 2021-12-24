using System;
using System.Threading;
using Appalachia.Utility.Async.Internal;

namespace Appalachia.Utility.Async.Linq
{
    public static partial class AppaTaskAsyncEnumerable
    {
        public static IAppaTaskAsyncEnumerable<TSource> TakeWhile<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, bool> predicate)
        {
            Error.ThrowArgumentNullException(source,    nameof(source));
            Error.ThrowArgumentNullException(predicate, nameof(predicate));

            return new TakeWhile<TSource>(source, predicate);
        }

        public static IAppaTaskAsyncEnumerable<TSource> TakeWhile<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, int, bool> predicate)
        {
            Error.ThrowArgumentNullException(source,    nameof(source));
            Error.ThrowArgumentNullException(predicate, nameof(predicate));

            return new TakeWhileInt<TSource>(source, predicate);
        }

        public static IAppaTaskAsyncEnumerable<TSource> TakeWhileAwait<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<bool>> predicate)
        {
            Error.ThrowArgumentNullException(source,    nameof(source));
            Error.ThrowArgumentNullException(predicate, nameof(predicate));

            return new TakeWhileAwait<TSource>(source, predicate);
        }

        public static IAppaTaskAsyncEnumerable<TSource> TakeWhileAwait<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, int, AppaTask<bool>> predicate)
        {
            Error.ThrowArgumentNullException(source,    nameof(source));
            Error.ThrowArgumentNullException(predicate, nameof(predicate));

            return new TakeWhileIntAwait<TSource>(source, predicate);
        }

        public static IAppaTaskAsyncEnumerable<TSource> TakeWhileAwaitWithCancellation<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<bool>> predicate)
        {
            Error.ThrowArgumentNullException(source,    nameof(source));
            Error.ThrowArgumentNullException(predicate, nameof(predicate));

            return new TakeWhileAwaitWithCancellation<TSource>(source, predicate);
        }

        public static IAppaTaskAsyncEnumerable<TSource> TakeWhileAwaitWithCancellation<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, int, CancellationToken, AppaTask<bool>> predicate)
        {
            Error.ThrowArgumentNullException(source,    nameof(source));
            Error.ThrowArgumentNullException(predicate, nameof(predicate));

            return new TakeWhileIntAwaitWithCancellation<TSource>(source, predicate);
        }
    }

    internal sealed class TakeWhile<TSource> : IAppaTaskAsyncEnumerable<TSource>
    {
        private readonly IAppaTaskAsyncEnumerable<TSource> source;
        private readonly Func<TSource, bool> predicate;

        public TakeWhile(IAppaTaskAsyncEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            this.source = source;
            this.predicate = predicate;
        }

        public IAppaTaskAsyncEnumerator<TSource> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            return new _TakeWhile(source, predicate, cancellationToken);
        }

        private class _TakeWhile : AsyncEnumeratorBase<TSource, TSource>
        {
            private Func<TSource, bool> predicate;

            public _TakeWhile(
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
                    if (predicate(SourceCurrent))
                    {
                        Current = SourceCurrent;
                        result = true;
                        return true;
                    }
                }

                result = false;
                return true;
            }
        }
    }

    internal sealed class TakeWhileInt<TSource> : IAppaTaskAsyncEnumerable<TSource>
    {
        private readonly IAppaTaskAsyncEnumerable<TSource> source;
        private readonly Func<TSource, int, bool> predicate;

        public TakeWhileInt(IAppaTaskAsyncEnumerable<TSource> source, Func<TSource, int, bool> predicate)
        {
            this.source = source;
            this.predicate = predicate;
        }

        public IAppaTaskAsyncEnumerator<TSource> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            return new _TakeWhileInt(source, predicate, cancellationToken);
        }

        private class _TakeWhileInt : AsyncEnumeratorBase<TSource, TSource>
        {
            private readonly Func<TSource, int, bool> predicate;
            private int index;

            public _TakeWhileInt(
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
                    if (predicate(SourceCurrent, checked(index++)))
                    {
                        Current = SourceCurrent;
                        result = true;
                        return true;
                    }
                }

                result = false;
                return true;
            }
        }
    }

    internal sealed class TakeWhileAwait<TSource> : IAppaTaskAsyncEnumerable<TSource>
    {
        private readonly IAppaTaskAsyncEnumerable<TSource> source;
        private readonly Func<TSource, AppaTask<bool>> predicate;

        public TakeWhileAwait(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<bool>> predicate)
        {
            this.source = source;
            this.predicate = predicate;
        }

        public IAppaTaskAsyncEnumerator<TSource> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            return new _TakeWhileAwait(source, predicate, cancellationToken);
        }

        private class _TakeWhileAwait : AsyncEnumeratorAwaitSelectorBase<TSource, TSource, bool>
        {
            private Func<TSource, AppaTask<bool>> predicate;

            public _TakeWhileAwait(
                IAppaTaskAsyncEnumerable<TSource> source,
                Func<TSource, AppaTask<bool>> predicate,
                CancellationToken cancellationToken) : base(source, cancellationToken)
            {
                this.predicate = predicate;
            }

            protected override AppaTask<bool> TransformAsync(TSource sourceCurrent)
            {
                return predicate(sourceCurrent);
            }

            protected override bool TrySetCurrentCore(bool awaitResult, out bool terminateIteration)
            {
                if (awaitResult)
                {
                    Current = SourceCurrent;
                    terminateIteration = false;
                    return true;
                }

                terminateIteration = true;
                return false;
            }
        }
    }

    internal sealed class TakeWhileIntAwait<TSource> : IAppaTaskAsyncEnumerable<TSource>
    {
        private readonly IAppaTaskAsyncEnumerable<TSource> source;
        private readonly Func<TSource, int, AppaTask<bool>> predicate;

        public TakeWhileIntAwait(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, int, AppaTask<bool>> predicate)
        {
            this.source = source;
            this.predicate = predicate;
        }

        public IAppaTaskAsyncEnumerator<TSource> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            return new _TakeWhileIntAwait(source, predicate, cancellationToken);
        }

        private class _TakeWhileIntAwait : AsyncEnumeratorAwaitSelectorBase<TSource, TSource, bool>
        {
            private readonly Func<TSource, int, AppaTask<bool>> predicate;
            private int index;

            public _TakeWhileIntAwait(
                IAppaTaskAsyncEnumerable<TSource> source,
                Func<TSource, int, AppaTask<bool>> predicate,
                CancellationToken cancellationToken) : base(source, cancellationToken)
            {
                this.predicate = predicate;
            }

            protected override AppaTask<bool> TransformAsync(TSource sourceCurrent)
            {
                return predicate(sourceCurrent, checked(index++));
            }

            protected override bool TrySetCurrentCore(bool awaitResult, out bool terminateIteration)
            {
                if (awaitResult)
                {
                    Current = SourceCurrent;
                    terminateIteration = false;
                    return true;
                }

                terminateIteration = true;
                return false;
            }
        }
    }

    internal sealed class TakeWhileAwaitWithCancellation<TSource> : IAppaTaskAsyncEnumerable<TSource>
    {
        private readonly IAppaTaskAsyncEnumerable<TSource> source;
        private readonly Func<TSource, CancellationToken, AppaTask<bool>> predicate;

        public TakeWhileAwaitWithCancellation(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<bool>> predicate)
        {
            this.source = source;
            this.predicate = predicate;
        }

        public IAppaTaskAsyncEnumerator<TSource> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            return new _TakeWhileAwaitWithCancellation(source, predicate, cancellationToken);
        }

        private class
            _TakeWhileAwaitWithCancellation : AsyncEnumeratorAwaitSelectorBase<TSource, TSource, bool>
        {
            private Func<TSource, CancellationToken, AppaTask<bool>> predicate;

            public _TakeWhileAwaitWithCancellation(
                IAppaTaskAsyncEnumerable<TSource> source,
                Func<TSource, CancellationToken, AppaTask<bool>> predicate,
                CancellationToken cancellationToken) : base(source, cancellationToken)
            {
                this.predicate = predicate;
            }

            protected override AppaTask<bool> TransformAsync(TSource sourceCurrent)
            {
                return predicate(sourceCurrent, cancellationToken);
            }

            protected override bool TrySetCurrentCore(bool awaitResult, out bool terminateIteration)
            {
                if (awaitResult)
                {
                    Current = SourceCurrent;
                    terminateIteration = false;
                    return true;
                }

                terminateIteration = true;
                return false;
            }
        }
    }

    internal sealed class TakeWhileIntAwaitWithCancellation<TSource> : IAppaTaskAsyncEnumerable<TSource>
    {
        private readonly IAppaTaskAsyncEnumerable<TSource> source;
        private readonly Func<TSource, int, CancellationToken, AppaTask<bool>> predicate;

        public TakeWhileIntAwaitWithCancellation(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, int, CancellationToken, AppaTask<bool>> predicate)
        {
            this.source = source;
            this.predicate = predicate;
        }

        public IAppaTaskAsyncEnumerator<TSource> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            return new _TakeWhileIntAwaitWithCancellation(source, predicate, cancellationToken);
        }

        private class
            _TakeWhileIntAwaitWithCancellation : AsyncEnumeratorAwaitSelectorBase<TSource, TSource, bool>
        {
            private readonly Func<TSource, int, CancellationToken, AppaTask<bool>> predicate;
            private int index;

            public _TakeWhileIntAwaitWithCancellation(
                IAppaTaskAsyncEnumerable<TSource> source,
                Func<TSource, int, CancellationToken, AppaTask<bool>> predicate,
                CancellationToken cancellationToken) : base(source, cancellationToken)
            {
                this.predicate = predicate;
            }

            protected override AppaTask<bool> TransformAsync(TSource sourceCurrent)
            {
                return predicate(sourceCurrent, checked(index++), cancellationToken);
            }

            protected override bool TrySetCurrentCore(bool awaitResult, out bool terminateIteration)
            {
                if (awaitResult)
                {
                    Current = SourceCurrent;
                    terminateIteration = false;
                    return true;
                }

                terminateIteration = true;
                return false;
            }
        }
    }
}
