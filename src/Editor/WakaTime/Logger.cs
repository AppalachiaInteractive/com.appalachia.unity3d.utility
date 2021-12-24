#if UNITY_EDITOR

using Appalachia.Utility.Strings;
using UnityEngine;

namespace Appalachia.Utility.WakaTime
{
    internal class Logger
    {
        internal static string GetLogPrefix()
        {
            return ZString.Format("{0} ", Constants.Logger.LogPrefix);
        }

        internal static void Log(string message)
        {
            Debug.Log(ZString.Format("{0}{1}", GetLogPrefix(), message));
        }

        internal static void LogWarning(string message)
        {
            Debug.LogWarning(ZString.Format("{0}{1}", GetLogPrefix(), message));
        }

        internal static void LogError(string message)
        {
            Debug.LogError(ZString.Format("{0}{1}", GetLogPrefix(), message));
        }

        internal static void DebugLog(string message)
        {
            if (Configuration.Debugging)
            {
                Debug.Log(ZString.Format("{0}{1}", GetLogPrefix(), message));
            }
        }

        internal static void DebugWarn(string message)
        {
            if (Configuration.Debugging)
            {
                Debug.LogWarning(ZString.Format("{0}{1}", GetLogPrefix(), message));
            }
        }
    }
}
#endif
