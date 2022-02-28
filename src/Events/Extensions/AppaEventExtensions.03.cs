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
        /// <param name="value1">The current value 1.</param>
        /// <param name="value2">The current value 2.</param>
        /// <param name="value3">The current value 3.</param>
        /// <typeparam name="T1">The type of value 1.</typeparam>
        /// <typeparam name="T2">The type of value 2.</typeparam>
        /// <typeparam name="T3">The type of value 3.</typeparam>
        /// <param name="callerFilePath">Do not provide a value for this argument.  It will be populated by the compiler.</param>
        /// <param name="callerMemberName">Do not provide a value for this argument.  It will be populated by the compiler.</param>
        /// <param name="callerLineNumber">Do not provide a value for this argument.  It will be populated by the compiler.</param>
        public static void RaiseEvent<T1, T2, T3>(
            this ValueEvent<T1, T2, T3>.Data handler,
            T1 value1,
            T2 value2,
            T3 value3,
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

                var args = ToArgs(value1, value2, value3);
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
        /// <param name="value1">The current value 1.</param>
        /// <param name="value2">The current value 2.</param>
        /// <param name="value3">The current value 3.</param>
        /// <typeparam name="T1">The value 1 type.</typeparam>
        /// <typeparam name="T2">The value 2 type.</typeparam>
        /// <typeparam name="T3">The value 3 type.</typeparam>
        /// <returns>The wrapper.  Remember to dispose!</returns>
        public static ValueEvent<T1, T2, T3>.Args ToArgs<T1, T2, T3>(T1 value1, T2 value2, T3 value3)
        {
            using (_PRF_ToArgs.Auto())
            {
                var instance = ValueEvent<T1, T2, T3>.Args.Get();
                instance.value1 = value1;
                instance.value2 = value2;
                instance.value3 = value3;

                return instance;
            }
        }
    }
}
