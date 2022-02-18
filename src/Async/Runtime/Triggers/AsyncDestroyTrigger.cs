#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System.Threading;
using UnityEngine;

namespace Appalachia.Utility.Async.Triggers
{
    public static partial class AsyncTriggerExtensions
    {
        public static AsyncDestroyTrigger GetAsyncDestroyTrigger(this GameObject gameObject)
        {
            var result = GetOrAddComponent<AsyncDestroyTrigger>(gameObject);

            result.hideFlags = HideFlags.HideAndDontSave;

            return result;
        }

        public static AsyncDestroyTrigger GetAsyncDestroyTrigger(this Component component)
        {
            return component.gameObject.GetAsyncDestroyTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncDestroyTrigger : MonoBehaviour
    {
        #region Fields and Autoproperties

        private bool awakeCalled;
        private bool called;
        private CancellationTokenSource cancellationTokenSource;

        #endregion

        public CancellationToken CancellationToken
        {
            get
            {
                if (cancellationTokenSource == null)
                {
                    cancellationTokenSource = new CancellationTokenSource();
                }

                if (!awakeCalled)
                {
                    PlayerLoopHelper.AddAction(PlayerLoopTiming.Update, new AwakeMonitor(this));
                }

                return cancellationTokenSource.Token;
            }
        }

        #region Event Functions

        private void Awake()
        {
            awakeCalled = true;
        }

        private void OnDestroy()
        {
            called = true;

            cancellationTokenSource?.Cancel();
            cancellationTokenSource?.Dispose();
        }

        #endregion

        public AppaTask OnDestroyAsync()
        {
            if (called)
            {
                return AppaTask.CompletedTask;
            }

            var tcs = new AppaTaskCompletionSource();

            // OnDestroy = Called Cancel.
            CancellationToken.RegisterWithoutCaptureExecutionContext(
                state =>
                {
                    var tcs2 = (AppaTaskCompletionSource)state;
                    tcs2.TrySetResult();
                },
                tcs
            );

            return tcs.Task;
        }

        #region Nested type: AwakeMonitor

        private class AwakeMonitor : IPlayerLoopItem
        {
            public AwakeMonitor(AsyncDestroyTrigger trigger)
            {
                this.trigger = trigger;
            }

            #region Fields and Autoproperties

            private readonly AsyncDestroyTrigger trigger;

            #endregion

            #region IPlayerLoopItem Members

            public bool MoveNext()
            {
                if (trigger.called)
                {
                    return false;
                }

                if (trigger == null)
                {
                    trigger.OnDestroy();
                    return false;
                }

                return true;
            }

            #endregion
        }

        #endregion
    }
}
