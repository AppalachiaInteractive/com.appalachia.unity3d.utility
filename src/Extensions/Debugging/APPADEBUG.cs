using System;
using System.Diagnostics;
using Appalachia.Utility.Logging;
using Appalachia.Utility.Strings;
using Unity.Profiling;

namespace Appalachia.Utility.Extensions.Debugging
{
    public static class APPADEBUG
    {
        public enum LOGIC
        {
            None,
            Any,
            All,
        }

        public static void BREAKPOINT(Func<string> message, UnityEngine.Object context)
        {
#if UNITY_EDITOR
            using (_PRF_BREAKPOINT.Auto())
            {
                var realMessage = message();

                if (!Debugger.IsAttached)
                {
                    AppaLog.Context.Extensions.Warn(
                        ZString.Format(
                            "Condition breakpoint [{0}] would be hit if the debugger was attached.",
                            realMessage
                        ),
                        context
                    );

                    return;
                }

                Debugger.Break();
            }
#endif
        }

        public static void BREAKPOINT(Func<string> message, UnityEngine.Object context, Func<bool> condition)
        {
#if UNITY_EDITOR
            using (_PRF_BREAKPOINT.Auto())
            {
                if (condition())
                {
                    BREAKPOINT(message, context);
                }
            }
#endif
        }

        public static void BREAKPOINT(
            Func<string> message,
            UnityEngine.Object context,
            LOGIC logic,
            params Func<bool>[] conditions)
        {
#if UNITY_EDITOR
            using (_PRF_BREAKPOINT.Auto())
            {
                var anyTrue = false;

                foreach (var condition in conditions)
                {
                    if (condition())
                    {
                        anyTrue = true;
                        if (logic == LOGIC.None)
                        {
                            return;
                        }

                        if (logic == LOGIC.Any)
                        {
                            break;
                        }
                    }
                    else
                    {
                        if (logic == LOGIC.All)
                        {
                            return;
                        }
                    }
                }

                if ((logic == LOGIC.Any) && !anyTrue)
                {
                    return;
                }

                BREAKPOINT(message, context);
            }
#endif
        }

        #region Profiling

        private const string _PRF_PFX = nameof(APPADEBUG) + ".";

        private static readonly ProfilerMarker _PRF_BREAKPOINT =
            new ProfilerMarker(_PRF_PFX + nameof(BREAKPOINT));

        #endregion
    }
}
