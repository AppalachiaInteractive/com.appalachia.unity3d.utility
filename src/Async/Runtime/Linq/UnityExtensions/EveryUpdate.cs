using System.Threading;

namespace Appalachia.Utility.Async.Linq.UnityExtensions
{
    public static partial class AppaTaskAsyncEnumerable
    {
        public static IAppaTaskAsyncEnumerable<AsyncUnit> EveryUpdate(
            PlayerLoopTiming updateTiming = PlayerLoopTiming.Update)
        {
            return new EveryUpdate(updateTiming);
        }
    }

    internal class EveryUpdate : IAppaTaskAsyncEnumerable<AsyncUnit>
    {
        private readonly PlayerLoopTiming updateTiming;

        public EveryUpdate(PlayerLoopTiming updateTiming)
        {
            this.updateTiming = updateTiming;
        }

        public IAppaTaskAsyncEnumerator<AsyncUnit> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            return new _EveryUpdate(updateTiming, cancellationToken);
        }

        private class _EveryUpdate : MoveNextSource, IAppaTaskAsyncEnumerator<AsyncUnit>, IPlayerLoopItem
        {
            private readonly PlayerLoopTiming updateTiming;
            private CancellationToken cancellationToken;

            private bool disposed;

            public _EveryUpdate(PlayerLoopTiming updateTiming, CancellationToken cancellationToken)
            {
                this.updateTiming = updateTiming;
                this.cancellationToken = cancellationToken;

                TaskTracker.TrackActiveTask(this, 2);
                PlayerLoopHelper.AddAction(updateTiming, this);
            }

            public AsyncUnit Current => default;

            public AppaTask<bool> MoveNextAsync()
            {
                // return false instead of throw
                if (disposed || cancellationToken.IsCancellationRequested)
                {
                    return CompletedTasks.False;
                }

                completionSource.Reset();
                return new AppaTask<bool>(this, completionSource.Version);
            }

            public AppaTask DisposeAsync()
            {
                if (!disposed)
                {
                    disposed = true;
                    TaskTracker.RemoveTracking(this);
                }

                return default;
            }

            public bool MoveNext()
            {
                if (disposed || cancellationToken.IsCancellationRequested)
                {
                    completionSource.TrySetResult(false);
                    return false;
                }

                completionSource.TrySetResult(true);
                return true;
            }
        }
    }
}
