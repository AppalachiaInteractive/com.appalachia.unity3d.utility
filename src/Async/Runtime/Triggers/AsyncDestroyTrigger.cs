﻿#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System.Threading;
using UnityEngine;

namespace Appalachia.Utility.Async.Triggers
{
    public static partial class AsyncTriggerExtensions
    {
        public static AsyncDestroyTrigger GetAsyncDestroyTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncDestroyTrigger>(gameObject);
        }

        public static AsyncDestroyTrigger GetAsyncDestroyTrigger(this Component component)
        {
            return component.gameObject.GetAsyncDestroyTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncDestroyTrigger : MonoBehaviour
    {
        private bool awakeCalled;
        private bool called;
        private CancellationTokenSource cancellationTokenSource;

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

        private class AwakeMonitor : IPlayerLoopItem
        {
            private readonly AsyncDestroyTrigger trigger;

            public AwakeMonitor(AsyncDestroyTrigger trigger)
            {
                this.trigger = trigger;
            }

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
        }
    }
}
