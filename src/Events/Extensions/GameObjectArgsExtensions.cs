using System.Runtime.CompilerServices;
using Unity.Profiling;
using UnityEngine;

namespace Appalachia.Utility.Events.Extensions
{
    public static class GameObjectArgsExtensions
    {
        /// <summary>
        ///     Invokes the event, using the provided arguments to generate the necessary delegate handler.
        /// </summary>
        /// <param name="eventHandler">The event to invoke.</param>
        /// <param name="gameObject">The <see cref="GameObject" /> invoking the event.</param>
        /// <param name="callerFilePath">Do not provide a value for this argument.  It will be populated by the compiler.</param>
        /// <param name="callerMemberName">Do not provide a value for this argument.  It will be populated by the compiler.</param>
        /// <param name="callerLineNumber">Do not provide a value for this argument.  It will be populated by the compiler.</param>
        public static void RaiseEvent(
            this GameObjectEvent.Data eventHandler,
            GameObject gameObject,
            [CallerFilePath] string callerFilePath = null,
            [CallerMemberName] string callerMemberName = null,
            [CallerLineNumber] int callerLineNumber = 0)
        {
            using (_PRF_RaiseEvent.Auto())
            {
                if (eventHandler.Subscribers == null)
                {
                    return;
                }

                var args = ToArgs(gameObject);
                eventHandler.Subscribers.InvokeSafe(
                    subscriber => subscriber.Invoke(args),
                    callerFilePath,
                    callerMemberName,
                    callerLineNumber,
                    args
                );
            }
        }

        /// <summary>
        ///     Provides a disposable delegate wrapper for the <see cref="GameObject" />.
        /// </summary>
        /// <param name="gameObject">The <see cref="GameObject" /> instance.</param>
        /// <returns>The wrapper.  Remember to dispose!</returns>
        public static GameObjectEvent.Args ToArgs(this GameObject gameObject)
        {
            using (_PRF_ToArgs.Auto())
            {
                var instance = GameObjectEvent.Args.Get();
                instance.gameObject = gameObject;

                return instance;
            }
        }

        #region Profiling

        private const string _PRF_PFX = nameof(GameObjectArgsExtensions) + ".";

        private static readonly ProfilerMarker _PRF_RaiseEvent =
            new ProfilerMarker(_PRF_PFX + nameof(RaiseEvent));

        private static readonly ProfilerMarker _PRF_ToArgs = new ProfilerMarker(_PRF_PFX + nameof(ToArgs));

        #endregion
    }
}
