using Appalachia.Utility.Events.Base;
using Appalachia.Utility.Events.Collections;
using Appalachia.Utility.Events.Extensions;

namespace Appalachia.Utility.Events
{
    public static class AppaEvent<T>
    {
        public delegate void Handler(Args args);

        #region Nested type: Args

        public sealed class Args : ValueBaseArgs<Args, T>
        {
            public static implicit operator T(Args o)
            {
                return o.value;
            }
        }

        #endregion

        #region Nested type: Data

        public struct Data
        {
            public event Handler Event
            {
                add
                {
                    _subscribers ??= new();
                    _subscribers.Add(value);
                }
                remove
                {
                    _subscribers ??= new();
                    _subscribers.Remove(value);
                }
            }

            #region Fields and Autoproperties

            private Subscribers _subscribers;

            #endregion

            public int SubscriberCount => _subscribers.SubscriberCountSafe();

            internal Subscribers Subscribers => _subscribers;
        }

        #endregion

        #region Nested type: Subscribers

        public sealed class Subscribers : EventSubscribersCollection<Handler>
        {
        }

        #endregion
    }
}
