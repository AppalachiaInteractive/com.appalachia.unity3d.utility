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
        public SkipWhile(IAppaTaskAsyncEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            this.source = source;
            this.predicate = predicate;
        }

        #region Fields and Autoproperties

        private readonly Func<TSource, bool> predicate;
        private readonly IAppaTaskAsyncEnumerable<TSource> source;

        #endregion

        #region IAppaTaskAsyncEnumerable<TSource> Members

        public IAppaTaskAsyncEnumerator<TSource> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            return new _SkipWhile(source, predicate, cancellationToken);
        }

        #endregion

        #region Nested type: _SkipWhile

        private class _SkipWhile : AsyncEnumeratorBase<TSource, TSource>
        {
            public _SkipWhile(
                IAppaTaskAsyncEnumerable<TSource> source,
                Func<TSource, bool> predicate,
                CancellationToken cancellationToken) : base(source, cancellationToken)
            {
                this.predicate = predicate;
            }

            #region Fields and Autoproperties

            private Func<TSource, bool> predicate;

            #endregion

            /// <inheritdoc />
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

        #endregion
    }

    internal sealed class SkipWhileInt<TSource> : IAppaTaskAsyncEnumerable<TSource>
    {
        public SkipWhileInt(IAppaTaskAsyncEnumerable<TSource> source, Func<TSource, int, bool> predicate)
        {
            this.source = source;
            this.predicate = predicate;
        }

        #region Fields and Autoproperties

        private readonly Func<TSource, int, bool> predicate;
        private readonly IAppaTaskAsyncEnumerable<TSource> source;

        #endregion

        #region IAppaTaskAsyncEnumerable<TSource> Members

        public IAppaTaskAsyncEnumerator<TSource> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            return new _SkipWhileInt(source, predicate, cancellationToken);
        }

        #endregion

        #region Nested type: _SkipWhileInt

        private class _SkipWhileInt : AsyncEnumeratorBase<TSource, TSource>
        {
            public _SkipWhileInt(
                IAppaTaskAsyncEnumerable<TSource> source,
                Func<TSource, int, bool> predicate,
                CancellationToken cancellationToken) : base(source, cancellationToken)
            {
                this.predicate = predicate;
            }

            #region Fields and Autoproperties

            private Func<TSource, int, bool> predicate;
            private int index;

            #endregion

            /// <inheritdoc />
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

        #endregion
    }

    internal sealed class SkipWhileAwait<TSource> : IAppaTaskAsyncEnumerable<TSource>
    {
        public SkipWhileAwait(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, AppaTask<bool>> predicate)
        {
            this.source = source;
            this.predicate = predicate;
        }

        #region Fields and Autoproperties

        private readonly Func<TSource, AppaTask<bool>> predicate;
        private readonly IAppaTaskAsyncEnumerable<TSource> source;

        #endregion

        #region IAppaTaskAsyncEnumerable<TSource> Members

        public IAppaTaskAsyncEnumerator<TSource> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            return new _SkipWhileAwait(source, predicate, cancellationToken);
        }

        #endregion

        #region Nested type: _SkipWhileAwait

        private class _SkipWhileAwait : AsyncEnumeratorAwaitSelectorBase<TSource, TSource, bool>
        {
            public _SkipWhileAwait(
                IAppaTaskAsyncEnumerable<TSource> source,
                Func<TSource, AppaTask<bool>> predicate,
                CancellationToken cancellationToken) : base(source, cancellationToken)
            {
                this.predicate = predicate;
            }

            #region Fields and Autoproperties

            private Func<TSource, AppaTask<bool>> predicate;

            #endregion

            /// <inheritdoc />
            protected override AppaTask<bool> TransformAsync(TSource sourceCurrent)
            {
                if (predicate == null)
                {
                    return CompletedTasks.False;
                }

                return predicate(sourceCurrent);
            }

            /// <inheritdoc />
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

        #endregion
    }

    internal sealed class SkipWhileIntAwait<TSource> : IAppaTaskAsyncEnumerable<TSource>
    {
        public SkipWhileIntAwait(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, int, AppaTask<bool>> predicate)
        {
            this.source = source;
            this.predicate = predicate;
        }

        #region Fields and Autoproperties

        private readonly Func<TSource, int, AppaTask<bool>> predicate;
        private readonly IAppaTaskAsyncEnumerable<TSource> source;

        #endregion

        #region IAppaTaskAsyncEnumerable<TSource> Members

        public IAppaTaskAsyncEnumerator<TSource> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            return new _SkipWhileIntAwait(source, predicate, cancellationToken);
        }

        #endregion

        #region Nested type: _SkipWhileIntAwait

        private class _SkipWhileIntAwait : AsyncEnumeratorAwaitSelectorBase<TSource, TSource, bool>
        {
            public _SkipWhileIntAwait(
                IAppaTaskAsyncEnumerable<TSource> source,
                Func<TSource, int, AppaTask<bool>> predicate,
                CancellationToken cancellationToken) : base(source, cancellationToken)
            {
                this.predicate = predicate;
            }

            #region Fields and Autoproperties

            private Func<TSource, int, AppaTask<bool>> predicate;
            private int index;

            #endregion

            /// <inheritdoc />
            protected override AppaTask<bool> TransformAsync(TSource sourceCurrent)
            {
                if (predicate == null)
                {
                    return CompletedTasks.False;
                }

                return predicate(sourceCurrent, checked(index++));
            }

            /// <inheritdoc />
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

        #endregion
    }

    internal sealed class SkipWhileAwaitWithCancellation<TSource> : IAppaTaskAsyncEnumerable<TSource>
    {
        public SkipWhileAwaitWithCancellation(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, AppaTask<bool>> predicate)
        {
            this.source = source;
            this.predicate = predicate;
        }

        #region Fields and Autoproperties

        private readonly Func<TSource, CancellationToken, AppaTask<bool>> predicate;
        private readonly IAppaTaskAsyncEnumerable<TSource> source;

        #endregion

        #region IAppaTaskAsyncEnumerable<TSource> Members

        public IAppaTaskAsyncEnumerator<TSource> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            return new _SkipWhileAwaitWithCancellation(source, predicate, cancellationToken);
        }

        #endregion

        #region Nested type: _SkipWhileAwaitWithCancellation

        private class
            _SkipWhileAwaitWithCancellation : AsyncEnumeratorAwaitSelectorBase<TSource, TSource, bool>
        {
            public _SkipWhileAwaitWithCancellation(
                IAppaTaskAsyncEnumerable<TSource> source,
                Func<TSource, CancellationToken, AppaTask<bool>> predicate,
                CancellationToken cancellationToken) : base(source, cancellationToken)
            {
                this.predicate = predicate;
            }

            #region Fields and Autoproperties

            private Func<TSource, CancellationToken, AppaTask<bool>> predicate;

            #endregion

            /// <inheritdoc />
            protected override AppaTask<bool> TransformAsync(TSource sourceCurrent)
            {
                if (predicate == null)
                {
                    return CompletedTasks.False;
                }

                return predicate(sourceCurrent, cancellationToken);
            }

            /// <inheritdoc />
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

        #endregion
    }

    internal sealed class SkipWhileIntAwaitWithCancellation<TSource> : IAppaTaskAsyncEnumerable<TSource>
    {
        public SkipWhileIntAwaitWithCancellation(
            IAppaTaskAsyncEnumerable<TSource> source,
            Func<TSource, int, CancellationToken, AppaTask<bool>> predicate)
        {
            this.source = source;
            this.predicate = predicate;
        }

        #region Fields and Autoproperties

        private readonly Func<TSource, int, CancellationToken, AppaTask<bool>> predicate;
        private readonly IAppaTaskAsyncEnumerable<TSource> source;

        #endregion

        #region IAppaTaskAsyncEnumerable<TSource> Members

        public IAppaTaskAsyncEnumerator<TSource> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            return new _SkipWhileIntAwaitWithCancellation(source, predicate, cancellationToken);
        }

        #endregion

        #region Nested type: _SkipWhileIntAwaitWithCancellation

        private class
            _SkipWhileIntAwaitWithCancellation : AsyncEnumeratorAwaitSelectorBase<TSource, TSource, bool>
        {
            public _SkipWhileIntAwaitWithCancellation(
                IAppaTaskAsyncEnumerable<TSource> source,
                Func<TSource, int, CancellationToken, AppaTask<bool>> predicate,
                CancellationToken cancellationToken) : base(source, cancellationToken)
            {
                this.predicate = predicate;
            }

            #region Fields and Autoproperties

            private Func<TSource, int, CancellationToken, AppaTask<bool>> predicate;
            private int index;

            #endregion

            /// <inheritdoc />
            protected override AppaTask<bool> TransformAsync(TSource sourceCurrent)
            {
                if (predicate == null)
                {
                    return CompletedTasks.False;
                }

                return predicate(sourceCurrent, checked(index++), cancellationToken);
            }

            /// <inheritdoc />
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

        #endregion
    }
}
