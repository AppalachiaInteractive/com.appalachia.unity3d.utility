using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Appalachia.Utility.Execution
{
    public static class IEnumeratorExtensions
    {
        [SuppressMessage("ReSharper", "ExplicitCallerInfoArgument")]
        public static SafeCoroutineWrapper ToSafe(
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
    }
}
