using System.Runtime.CompilerServices;
using Unity.Profiling;

namespace Appalachia.Utility.Events.Extensions
{
    public static partial class AppaEventExtensions
    {
        /// <summary>
        ///     Invokes the event, using the provided arguments to generate the necessary delegate handler.
        /// </summary>
        /// <param name="handler">The event to invoke.</param>
        /// <param name="value">The current value.</param>
        /// <typeparam name="T">The type of component.</typeparam>
        /// <param name="callerFilePath">Do not provide a value for this argument.  It will be populated by the compiler.</param>
        /// <param name="callerMemberName">Do not provide a value for this argument.  It will be populated by the compiler.</param>
        /// <param name="callerLineNumber">Do not provide a value for this argument.  It will be populated by the compiler.</param>
        public static void RaiseEvent<T>(
            this AppaEvent<T>.Data handler,
            T value,
            [CallerFilePath] string callerFilePath = null,
            [CallerMemberName] string callerMemberName = null,
            [CallerLineNumber] int callerLineNumber = 0)
        {
            using (_PRF_RaiseEvent.Auto())
            {
                if (handler.Subscribers == null)
                {
                    return;
                }

                var args = ToArgs(value);
                handler.Subscribers.InvokeSafe(
                    subscriber => subscriber.Invoke(args),
                    callerFilePath,
                    callerMemberName,
                    callerLineNumber,
                    args
                );
            }
        }

        /// <summary>
        ///     Provides a disposable delegate wrapper for the value.
        /// </summary>
        /// <param name="value">The current value.</param>
        /// <typeparam name="T">The value type.</typeparam>
        /// <returns>The wrapper.  Remember to dispose!</returns>
        public static AppaEvent<T>.Args ToArgs<T>(T value)
        {
            using (_PRF_ToArgs.Auto())
            {
                var instance = AppaEvent<T>.Args.Get();
                instance.value = value;

                return instance;
            }
        }
    }
}
