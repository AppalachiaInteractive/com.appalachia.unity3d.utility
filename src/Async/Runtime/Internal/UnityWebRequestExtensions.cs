using UnityEngine.Networking;

namespace Appalachia.Utility.Async.Internal
{
#if ENABLE_UNITYWEBREQUEST && (!UNITY_2019_1_OR_NEWER || APPATASK_WEBREQUEST_SUPPORT)

    internal static class UnityWebRequestResultExtensions
    {
        public static bool IsError(this UnityWebRequest unityWebRequest)
        {
#if UNITY_2020_2_OR_NEWER
            var result = unityWebRequest.result;
            return (result == UnityWebRequest.Result.ConnectionError) ||
                   (result == UnityWebRequest.Result.DataProcessingError) ||
                   (result == UnityWebRequest.Result.ProtocolError);
#else
            return unityWebRequest.isHttpError || unityWebRequest.isNetworkError;
#endif
        }
    }

#endif
}
