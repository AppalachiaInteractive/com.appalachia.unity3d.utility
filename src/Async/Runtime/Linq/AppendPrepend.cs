using System;
using System.Threading;
using Appalachia.Utility.Async.Internal;

namespace Appalachia.Utility.Async.Linq
{
    public static partial class AppaTaskAsyncEnumerable
    {
        public static IAppaTaskAsyncEnumerable<TSource> Append<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            TSource element)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return new AppendPrepend<TSource>(source, element, true);
        }

        public static IAppaTaskAsyncEnumerable<TSource> Prepend<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source,
            TSource element)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return new AppendPrepend<TSource>(source, element, false);
        }
    }

    internal sealed class AppendPrepend<TSource> : IAppaTaskAsyncEnumerable<TSource>
    {
        private readonly IAppaTaskAsyncEnumerable<TSource> source;
        private readonly TSource element;
        private readonly bool append; // or prepend

        public AppendPrepend(IAppaTaskAsyncEnumerable<TSource> source, TSource element, bool append)
        {
            this.source = source;
            this.element = element;
            this.append = append;
        }

        public IAppaTaskAsyncEnumerator<TSource> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            return new _AppendPrepend(source, element, append, cancellationToken);
        }

        private sealed class _AppendPrepend : MoveNextSource, IAppaTaskAsyncEnumerator<TSource>
        {
            private enum State : byte
            {
                None,
                RequirePrepend,
                RequireAppend,
                Completed
            }

            private static readonly Action<object> MoveNextCoreDelegate = MoveNextCore;

            private readonly IAppaTaskAsyncEnumerable<TSource> source;
            private readonly TSource element;
            private CancellationToken cancellationToken;

            private State state;
            private IAppaTaskAsyncEnumerator<TSource> enumerator;
            private AppaTask<bool>.Awaiter awaiter;

            public _AppendPrepend(
                IAppaTaskAsyncEnumerable<TSource> source,
                TSource element,
                bool append,
                CancellationToken cancellationToken)
            {
                this.source = source;
                this.element = element;
                state = append ? State.RequireAppend : State.RequirePrepend;
                this.cancellationToken = cancellationToken;

                TaskTracker.TrackActiveTask(this, 3);
            }

            public TSource Current { get; private set; }

            public AppaTask<bool> MoveNextAsync()
            {
                cancellationToken.ThrowIfCancellationRequested();
                completionSource.Reset();

                if (enumerator == null)
                {
                    if (state == State.RequirePrepend)
                    {
                        Current = element;
                        state = State.None;
                        return CompletedTasks.True;
                    }

                    enumerator = source.GetAsyncEnumerator(cancellationToken);
                }

                if (state == State.Completed)
                {
                    return CompletedTasks.False;
                }

                awaiter = enumerator.MoveNextAsync().GetAwaiter();

                if (awaiter.IsCompleted)
                {
                    MoveNextCoreDelegate(this);
                }
                else
                {
                    awaiter.SourceOnCompleted(MoveNextCoreDelegate, this);
                }

                return new AppaTask<bool>(this, completionSource.Version);
            }

            private static void MoveNextCore(object state)
            {
                var self = (_AppendPrepend)state;

                if (self.TryGetResult(self.awaiter, out var result))
                {
                    if (result)
                    {
                        self.Current = self.enumerator.Current;
                        self.completionSource.TrySetResult(true);
                    }
                    else
                    {
                        if (self.state == State.RequireAppend)
                        {
                            self.state = State.Completed;
                            self.Current = self.element;
                            self.completionSource.TrySetResult(true);
                        }
                        else
                        {
                            self.state = State.Completed;
                            self.completionSource.TrySetResult(false);
                        }
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
