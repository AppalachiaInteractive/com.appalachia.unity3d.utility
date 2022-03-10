using System;
using Appalachia.Utility.Events.Collections;
using Appalachia.Utility.Events.Extensions;
using Appalachia.Utility.Standards;

namespace Appalachia.Utility.Events
{
    public static class AppaEvent
    {
        public delegate void Handler();

        #region Nested type: Data

        public struct Data : IEquatable<Data>
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

            private ObjectID _objectId;

            private Subscribers _subscribers;

            #endregion

            public int SubscriberCount
            {
                get
                {
                    _subscribers ??= new();
                    return _subscribers.SubscriberCountSafe();
                }
            }

            public ObjectID ObjectID
            {
                get
                {
                    if ((_objectId == null) || (_objectId == ObjectID.Empty))
                    {
                        _objectId = ObjectID.NewObjectID();
                    }

                    return _objectId;
                }
            }

            internal Subscribers Subscribers
            {
                get
                {
                    _subscribers ??= new();
                    return _subscribers;
                }
            }

            public static bool operator ==(Data left, Data right)
            {
                return left.Equals(right);
            }

            public static bool operator !=(Data left, Data right)
            {
                return !left.Equals(right);
            }

            public override bool Equals(object obj)
            {
                return obj is Data other && Equals(other);
            }

            public override int GetHashCode()
            {
                return ObjectID.GetHashCode();
            }

            public void UnsubscribeAll()
            {
                _subscribers.Clear();
            }

            #region IEquatable<Data> Members

            public bool Equals(Data other)
            {
                return Equals(_objectId, other._objectId);
            }

            #endregion
        }

        #endregion

        #region Nested type: Subscribers

        public sealed class Subscribers : EventSubscribersCollection<Handler>
        {
        }

        #endregion
    }
}
