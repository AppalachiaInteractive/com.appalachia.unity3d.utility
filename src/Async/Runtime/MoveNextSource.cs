using System;

namespace Appalachia.Utility.Async
{
    public abstract class MoveNextSource : IAppaTaskSource<bool>
    {
        protected AppaTaskCompletionSourceCore<bool> completionSource;

        public bool GetResult(short token)
        {
            return completionSource.GetResult(token);
        }

        public AppaTaskStatus GetStatus(short token)
        {
            return completionSource.GetStatus(token);
        }

        public void OnCompleted(Action<object> continuation, object state, short token)
        {
            completionSource.OnCompleted(continuation, state, token);
        }

        public AppaTaskStatus UnsafeGetStatus()
        {
            return completionSource.UnsafeGetStatus();
        }

        void IAppaTaskSource.GetResult(short token)
        {
            completionSource.GetResult(token);
        }

        protected bool TryGetResult<T>(AppaTask<T>.Awaiter awaiter, out T result)
        {
            try
            {
                result = awaiter.GetResult();
                return true;
            }
            catch (Exception ex)
            {
                completionSource.TrySetException(ex);
                result = default;
                return false;
            }
        }

        protected bool TryGetResult(AppaTask.Awaiter awaiter)
        {
            try
            {
                awaiter.GetResult();
                return true;
            }
            catch (Exception ex)
            {
                completionSource.TrySetException(ex);
                return false;
            }
        }
    }
}
