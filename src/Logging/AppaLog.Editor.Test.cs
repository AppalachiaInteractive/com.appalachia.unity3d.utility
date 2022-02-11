#if UNITY_EDITOR
using System;
using System.Linq;
using Appalachia.Utility.Logging.Formatters;
using Unity.Profiling;
using UnityEditor;

namespace Appalachia.Utility.Logging
{
    public static partial class AppaLog
    {
        #region Menu Items

        [MenuItem(MENU_BASE, false, MENU_PRIORITY + 0)]
        internal static void Test()
        {
            using (_PRF_Test.Auto())
            {
                _consoleLogFormatter = new UnityConsoleFormatter();
                _traceLogFormatter = new FileLogFormatter();

                var line = 436;
                var step = 11;

                LogInternal(
                    LogLevel.Fatal,
                    null,
                    null,
                    "Testing 1 2 3...",
                    null,
                    null,
                    true,
                    nameof(Test),
                    "AppaLog.cs",
                    line + (step * 1)
                );

                LogInternal(
                    LogLevel.Critical,
                    null,
                    null,
                    "Testing 1 2 3...",
                    null,
                    null,
                    true,
                    nameof(Test),
                    "AppaLog.cs",
                    line + (step * 2)
                );

                LogInternal(
                    LogLevel.Exception,
                    null,
                    null,
                    "Testing 1 2 3...",
                    null,
                    new NotSupportedException("testing 123"),
                    true,
                    nameof(Test),
                    "AppaLog.cs",
                    line + (step * 3)
                );

                LogInternal(
                    LogLevel.Error,
                    null,
                    null,
                    "Testing 1 2 3...",
                    null,
                    null,
                    true,
                    nameof(Test),
                    "AppaLog.cs",
                    line + (step * 4)
                );

                LogInternal(
                    LogLevel.Warn,
                    null,
                    null,
                    "Testing 1 2 3...",
                    null,
                    null,
                    true,
                    nameof(Test),
                    "AppaLog.cs",
                    line + (step * 5)
                );

                LogInternal(
                    LogLevel.Info,
                    null,
                    null,
                    "Testing 1 2 3...",
                    null,
                    null,
                    true,
                    nameof(Test),
                    "AppaLog.cs",
                    line + (step * 6)
                );

                LogInternal(
                    LogLevel.Debug,
                    null,
                    null,
                    "Testing 1 2 3...",
                    null,
                    null,
                    true,
                    nameof(Test),
                    "AppaLog.cs",
                    line + (step * 7)
                );

                LogInternal(
                    LogLevel.Trace,
                    null,
                    null,
                    "Testing 1 2 3...",
                    null,
                    null,
                    true,
                    nameof(Test),
                    "AppaLog.cs",
                    line + (step * 8)
                );

                var contextProperties = typeof(Context).GetProperties()
                                                       .Where(p => p.PropertyType == typeof(AppaLogContext));

                foreach (var contextProperty in contextProperties)
                {
                    var contextValue = contextProperty.GetValue(null) as AppaLogContext;

                    contextValue?.Test();
                }
            }
        }

        #endregion

        #region Profiling

        private static readonly ProfilerMarker _PRF_Test = new ProfilerMarker(_PRF_PFX + nameof(Test));

        #endregion
    }
}

#endif
