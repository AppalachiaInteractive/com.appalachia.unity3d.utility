using System;
using System.Threading;

namespace Appalachia.Utility.Async
{
    public static partial class UnityAsyncExtensions
    {
        public static AppaTask StartAsyncCoroutine(
            this UnityEngine.MonoBehaviour monoBehaviour,
            Func<CancellationToken, AppaTask> asyncCoroutine)
        {
            var token = monoBehaviour.GetCancellationTokenOnDestroy();
            return asyncCoroutine(token);
        }
    }
}
