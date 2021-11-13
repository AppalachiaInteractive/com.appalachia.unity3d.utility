using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Appalachia.Utility.Execution
{
    public static class IEnumeratorExtensions
    {
        [SuppressMessage("ReSharper", "ExplicitCallerInfoArgument")]
        public static SafeCoroutineWrapper AsNonCancellable(this SafeCoroutineWrapper request)
        {
            request.SetNonCancellable();

            return request;
        }

        [SuppressMessage("ReSharper", "ExplicitCallerInfoArgument")]
        public static SafeCoroutineWrapper AsSafe(
            this IEnumerator enumerator,
            string processKey = null,
            SafeCoroutineWrapper.OnSuccess onSuccess = null,
            SafeCoroutineWrapper.OnError onError = null,
            double timeoutSeconds = 0.0,
            [CallerMemberName] string callerMemberName = null,
            [CallerFilePath] string callerFilePath = null,
            [CallerLineNumber] int callerLineNumber = 0)
        {
            return new SafeCoroutineWrapper(
                enumerator,
                processKey,
                onSuccess,
                onError,
                timeoutSeconds,
                callerMemberName,
                callerFilePath,
                callerLineNumber
            );
        }

#if UNITY_EDITOR

        [SuppressMessage("ReSharper", "ExplicitCallerInfoArgument")]
        public static SafeCoroutineWrapper AsSafe(
            this UnityEditor.PackageManager.Requests.Request request,
            string processKey = null,
            SafeCoroutineWrapper.OnSuccess onSuccess = null,
            SafeCoroutineWrapper.OnError onError = null,
            double timeoutSeconds = 0.0,
            [CallerMemberName] string callerMemberName = null,
            [CallerFilePath] string callerFilePath = null,
            [CallerLineNumber] int callerLineNumber = 0)
        {
            var enumerator = RequestToEnumerator(request);

            return new SafeCoroutineWrapper(
                enumerator,
                processKey,
                onSuccess,
                onError,
                timeoutSeconds,
                callerMemberName,
                callerFilePath,
                callerLineNumber
            );
        }

        private static IEnumerator RequestToEnumerator(UnityEditor.PackageManager.Requests.Request request)
        {
            while (!request.IsCompleted)
            {
                yield return null;
            }
        }
#endif
    }
}
