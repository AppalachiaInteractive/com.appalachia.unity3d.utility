using System;
using System.Collections.Generic;
using Appalachia.Utility.Standards;
using Unity.Profiling;

namespace Appalachia.Utility.Events.Collections
{
    public class ReusableDelegateCollection<TKey1>
    {
        #region Fields and Autoproperties

        private Dictionary<ObjectID, Dictionary<TKey1, Action>> _updateDelegatesByData;

        private Dictionary<ObjectID, Dictionary<TKey1, AppaEvent.Handler>> _updateHandlersByData;

        #endregion

        /// <summary>
        ///     Subscribes the delegate to the provided event if it has not been previously subscribed, using the keys provided to ensure uniqueness.
        /// </summary>
        /// <param name="key1">The first delegate key.</param>
        /// <param name="targetEvent">The event to subscribe to (once).</param>
        /// <param name="delegateCreator">A delegate to create the update/subscribe delegate.</param>
        /// <exception cref="ArgumentNullException">Thrown whenever any key is null.</exception>
        public void Subscribe(TKey1 key1, ref AppaEvent.Data targetEvent, Func<Action> delegateCreator)
        {
            using (_PRF_Subscribe.Auto())
            {
                if (key1 == null)
                {
                    throw new ArgumentNullException(nameof(key1), "The key may not be null!");
                }

                _updateDelegatesByData ??= new();
                _updateHandlersByData ??= new();
                Action updateDelegate;
                AppaEvent.Handler updateHandler;

                // first
                if (!_updateDelegatesByData.ContainsKey(targetEvent.ObjectID))
                {
                    _updateDelegatesByData.Add(targetEvent.ObjectID, new());
                }

                if (!_updateDelegatesByData[targetEvent.ObjectID].ContainsKey(key1))
                {
                    _updateDelegatesByData[targetEvent.ObjectID].Add(key1, delegateCreator());
                }

                updateDelegate = _updateDelegatesByData[targetEvent.ObjectID][key1];

                if (!_updateHandlersByData.ContainsKey(targetEvent.ObjectID))
                {
                    _updateHandlersByData.Add(targetEvent.ObjectID, new());
                }

                if (!_updateHandlersByData[targetEvent.ObjectID].ContainsKey(key1))
                {
                    _updateHandlersByData[targetEvent.ObjectID].Add(key1, new AppaEvent.Handler(updateDelegate));
                }

                updateHandler = _updateHandlersByData[targetEvent.ObjectID][key1];

                targetEvent.Event += updateHandler;
            }
        }

        /// <summary>
        ///     Subscribes the delegate to the provided event if it has not been previously subscribed, using the keys provided to ensure uniqueness.
        /// </summary>
        /// <param name="key1">The first delegate key.</param>
        /// <param name="targetEvent">The event to subscribe to (once).</param>
        /// <param name="delegateCreator">A delegate to create the update/subscribe delegate.</param>
        /// <param name="doInvoke">Should we invoke the delegate the first time we are subscribed?</param>
        /// <exception cref="ArgumentNullException">Thrown whenever any key is null.</exception>
        public void SubscribeAndInvoke(
            TKey1 key1,
            ref AppaEvent.Data targetEvent,
            Func<Action> delegateCreator,
            bool doInvoke = true)
        {
            using (_PRF_SubscribeAndInvoke.Auto())
            {
                Subscribe(key1, ref targetEvent, delegateCreator);

                if (!doInvoke)
                {
                    return;
                }

                var updateDelegate = _updateDelegatesByData[targetEvent.ObjectID][key1];
                updateDelegate();
            }
        }

        /// <summary>
        ///     Unsubscribes from the provided event, using the keys provided to ensure the correct target.
        /// </summary>
        /// <param name="key1">The first delegate key.</param>
        /// <param name="targetEvent">The event to unsubscribe from.</param>
        /// <exception cref="ArgumentNullException">Thrown whenever any key is null.</exception>
        public void Unsubscribe(TKey1 key1, ref AppaEvent.Data targetEvent)
        {
            using (_PRF_Unsubscribe.Auto())
            {
                if (_updateDelegatesByData.ContainsKey(targetEvent.ObjectID))
                {
                    if (_updateDelegatesByData[targetEvent.ObjectID].ContainsKey(key1))
                    {
                        _updateDelegatesByData[targetEvent.ObjectID].Remove(key1);
                    }
                }

                if (_updateHandlersByData.ContainsKey(targetEvent.ObjectID))
                {
                    if (_updateHandlersByData[targetEvent.ObjectID].ContainsKey(key1))
                    {
                        var target = _updateHandlersByData[targetEvent.ObjectID][key1];

                        targetEvent.Event -= target;

                        _updateHandlersByData[targetEvent.ObjectID].Remove(key1);
                    }
                }
            }
        }

        #region Profiling

        private const string _PRF_PFX = nameof(ReusableDelegateCollection<TKey1>) + ".";

        private static readonly ProfilerMarker _PRF_Unsubscribe = new ProfilerMarker(_PRF_PFX + nameof(Unsubscribe));
        private static readonly ProfilerMarker _PRF_Subscribe = new ProfilerMarker(_PRF_PFX + nameof(Subscribe));

        private static readonly ProfilerMarker _PRF_SubscribeAndInvoke =
            new ProfilerMarker(_PRF_PFX + nameof(SubscribeAndInvoke));

        #endregion
    }
}
