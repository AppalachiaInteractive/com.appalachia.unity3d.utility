using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace Appalachia.Utility.Async
{
    public class AppaTaskSynchronizationContext : SynchronizationContext
    {
        #region Constants and Static Readonly

        private const int InitialSize = 16;
        private const int MaxArrayLength = 0X7FEFFFFF;

        #endregion

        #region Static Fields and Autoproperties

        private static bool dequing;
        private static int actionListCount;
        private static SpinLock gate = new SpinLock(false);
        private static Callback[] actionList = new Callback[InitialSize];
        private static int waitingListCount;
        private static Callback[] waitingList = new Callback[InitialSize];

        private static int opCount;

        #endregion

        /// <inheritdoc />
        public override SynchronizationContext CreateCopy()
        {
            return this;
        }

        /// <inheritdoc />
        public override void OperationCompleted()
        {
            Interlocked.Decrement(ref opCount);
        }

        /// <inheritdoc />
        public override void OperationStarted()
        {
            Interlocked.Increment(ref opCount);
        }

        /// <inheritdoc />
        public override void Post(SendOrPostCallback d, object state)
        {
            var lockTaken = false;
            try
            {
                gate.Enter(ref lockTaken);

                if (dequing)
                {
                    // Ensure Capacity
                    if (waitingList.Length == waitingListCount)
                    {
                        var newLength = waitingListCount * 2;
                        if ((uint)newLength > MaxArrayLength)
                        {
                            newLength = MaxArrayLength;
                        }

                        var newArray = new Callback[newLength];
                        Array.Copy(waitingList, newArray, waitingListCount);
                        waitingList = newArray;
                    }

                    waitingList[waitingListCount] = new Callback(d, state);
                    waitingListCount++;
                }
                else
                {
                    // Ensure Capacity
                    if (actionList.Length == actionListCount)
                    {
                        var newLength = actionListCount * 2;
                        if ((uint)newLength > MaxArrayLength)
                        {
                            newLength = MaxArrayLength;
                        }

                        var newArray = new Callback[newLength];
                        Array.Copy(actionList, newArray, actionListCount);
                        actionList = newArray;
                    }

                    actionList[actionListCount] = new Callback(d, state);
                    actionListCount++;
                }
            }
            finally
            {
                if (lockTaken)
                {
                    gate.Exit(false);
                }
            }
        }

        /// <inheritdoc />
        public override void Send(SendOrPostCallback d, object state)
        {
            d(state);
        }

        // delegate entrypoint.
        internal static void Run()
        {
            {
                var lockTaken = false;
                try
                {
                    gate.Enter(ref lockTaken);
                    if (actionListCount == 0)
                    {
                        return;
                    }

                    dequing = true;
                }
                finally
                {
                    if (lockTaken)
                    {
                        gate.Exit(false);
                    }
                }
            }

            for (var i = 0; i < actionListCount; i++)
            {
                var action = actionList[i];
                actionList[i] = default;
                action.Invoke();
            }

            {
                var lockTaken = false;
                try
                {
                    gate.Enter(ref lockTaken);
                    dequing = false;

                    var swapTempActionList = actionList;

                    actionListCount = waitingListCount;
                    actionList = waitingList;

                    waitingListCount = 0;
                    waitingList = swapTempActionList;
                }
                finally
                {
                    if (lockTaken)
                    {
                        gate.Exit(false);
                    }
                }
            }
        }

        #region Nested type: Callback

        [StructLayout(LayoutKind.Auto)]
        private readonly struct Callback
        {
            public Callback(SendOrPostCallback callback, object state)
            {
                this.callback = callback;
                this.state = state;
            }

            #region Fields and Autoproperties

            private readonly object state;
            private readonly SendOrPostCallback callback;

            #endregion

            public void Invoke()
            {
                try
                {
                    callback(state);
                }
                catch (Exception ex)
                {
                    UnityEngine.Debug.LogException(ex);
                }
            }
        }

        #endregion
    }
}
