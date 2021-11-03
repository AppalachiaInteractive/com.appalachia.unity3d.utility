using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace Appalachia.Utility.Extensions
{
    public static class IEnumeratorExtensions
    {
        public static IEnumerator OnComplete(this IEnumerator enumerator, string processKey, Action action, float timeout = 60f)
        {
            var start = Time.time;

            while (enumerator.MoveNext())
            {
                CheckTimeout(processKey, start, Time.time, timeout);

                yield return enumerator.Current;
            }

            action();
        }

        public static void CheckTimeout(string processKey, float start, float current, float iterationTimeout)
        {
            CheckTimeout(processKey, current - start, iterationTimeout);
        }
        
        public static void CheckTimeout(string processKey, float elapsed, float iterationTimeout)
        {
            if (elapsed > iterationTimeout)
            {
                if (!Debugger.IsAttached)
                {
                    var message = $"| {elapsed:F2} / {iterationTimeout:F2} | {processKey}";
                    throw new TimeoutException(message);
                }
            }
        }

        public static void Complete(this IEnumerator enumerator, string processKey, float timeout = 60f)
        {
            var start = Time.time;

            while (enumerator.MoveNext())
            {
                CheckTimeout(processKey, start, Time.time, timeout);
            }
        }
    }
}
