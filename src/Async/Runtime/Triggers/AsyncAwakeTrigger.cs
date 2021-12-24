#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using UnityEngine;

namespace Appalachia.Utility.Async.Triggers
{
    public static partial class AsyncTriggerExtensions
    {
        public static AsyncAwakeTrigger GetAsyncAwakeTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncAwakeTrigger>(gameObject);
        }

        public static AsyncAwakeTrigger GetAsyncAwakeTrigger(this Component component)
        {
            return component.gameObject.GetAsyncAwakeTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncAwakeTrigger : AsyncTriggerBase<AsyncUnit>
    {
        public AppaTask AwakeAsync()
        {
            if (calledAwake)
            {
                return AppaTask.CompletedTask;
            }

            return ((IAsyncOneShotTrigger)new AsyncTriggerHandler<AsyncUnit>(this, true)).OneShotAsync();
        }
    }
}
