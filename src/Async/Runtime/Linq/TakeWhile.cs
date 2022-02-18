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
        public TakeWhile(IAppaTaskAsyncEnumerable<TSource> source, Func<TSource, bool> predicate)
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
            return new _TakeWhile(source, predicate, cancellationToken);
        }

        #endregion

        #region Nested type: _TakeWhile

        private class _TakeWhile : AsyncEnumeratorBase<TSource, TSource>
        {
            public _TakeWhile(
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

        #endregion
    }

    internal sealed class TakeWhileInt<TSource> : IAppaTaskAsyncEnumerable<TSource>
    {
        public TakeWhileInt(IAppaTaskAsyncEnumerable<TSource> source, Func<TSource, int, bool> predicate)
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
            return new _TakeWhileInt(source, predicate, cancellationToken);
        }

        #endregion

        #region Nested type: _TakeWhileInt

        private class _TakeWhileInt : AsyncEnumeratorBase<TSource, TSource>
        {
            public _TakeWhileInt(
                IAppaTaskAsyncEnumerable<TSource> source,
                Func<TSource, int, bool> predicate,
                CancellationToken cancellationToken) : base(source, cancellationToken)
            {
                this.predicate = predicate;
            }

            #region Fields and Autoproperties

            private readonly Func<TSource, int, bool> predicate;
            private int index;

            #endregion

            /// <inheritdoc />
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

        #endregion
    }

    internal sealed class TakeWhileAwait<TSource> : IAppaTaskAsyncEnumerable<TSource>
    {
        public TakeWhileAwait(
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
            return new _TakeWhileAwait(source, predicate, cancellationToken);
        }

        #endregion

        #region Nested type: _TakeWhileAwait

        private class _TakeWhileAwait : AsyncEnumeratorAwaitSelectorBase<TSource, TSource, bool>
        {
            public _TakeWhileAwait(
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
                return predicate(sourceCurrent);
            }

            /// <inheritdoc />
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

        #endregion
    }

    internal sealed class TakeWhileIntAwait<TSource> : IAppaTaskAsyncEnumerable<TSource>
    {
        public TakeWhileIntAwait(
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
            return new _TakeWhileIntAwait(source, predicate, cancellationToken);
        }

        #endregion

        #region Nested type: _TakeWhileIntAwait

        private class _TakeWhileIntAwait : AsyncEnumeratorAwaitSelectorBase<TSource, TSource, bool>
        {
            public _TakeWhileIntAwait(
                IAppaTaskAsyncEnumerable<TSource> source,
                Func<TSource, int, AppaTask<bool>> predicate,
                CancellationToken cancellationToken) : base(source, cancellationToken)
            {
                this.predicate = predicate;
            }

            #region Fields and Autoproperties

            private readonly Func<TSource, int, AppaTask<bool>> predicate;
            private int index;

            #endregion

            /// <inheritdoc />
            protected override AppaTask<bool> TransformAsync(TSource sourceCurrent)
            {
                return predicate(sourceCurrent, checked(index++));
            }

            /// <inheritdoc />
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

        #endregion
    }

    internal sealed class TakeWhileAwaitWithCancellation<TSource> : IAppaTaskAsyncEnumerable<TSource>
    {
        public TakeWhileAwaitWithCancellation(
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
            return new _TakeWhileAwaitWithCancellation(source, predicate, cancellationToken);
        }

        #endregion

        #region Nested type: _TakeWhileAwaitWithCancellation

        private class
            _TakeWhileAwaitWithCancellation : AsyncEnumeratorAwaitSelectorBase<TSource, TSource, bool>
        {
            public _TakeWhileAwaitWithCancellation(
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
                return predicate(sourceCurrent, cancellationToken);
            }

            /// <inheritdoc />
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

        #endregion
    }

    internal sealed class TakeWhileIntAwaitWithCancellation<TSource> : IAppaTaskAsyncEnumerable<TSource>
    {
        public TakeWhileIntAwaitWithCancellation(
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
            return new _TakeWhileIntAwaitWithCancellation(source, predicate, cancellationToken);
        }

        #endregion

        #region Nested type: _TakeWhileIntAwaitWithCancellation

        private class
            _TakeWhileIntAwaitWithCancellation : AsyncEnumeratorAwaitSelectorBase<TSource, TSource, bool>
        {
            public _TakeWhileIntAwaitWithCancellation(
                IAppaTaskAsyncEnumerable<TSource> source,
                Func<TSource, int, CancellationToken, AppaTask<bool>> predicate,
                CancellationToken cancellationToken) : base(source, cancellationToken)
            {
                this.predicate = predicate;
            }

            #region Fields and Autoproperties

            private readonly Func<TSource, int, CancellationToken, AppaTask<bool>> predicate;
            private int index;

            #endregion

            /// <inheritdoc />
            protected override AppaTask<bool> TransformAsync(TSource sourceCurrent)
            {
                return predicate(sourceCurrent, checked(index++), cancellationToken);
            }

            /// <inheritdoc />
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

        #endregion
    }
}
