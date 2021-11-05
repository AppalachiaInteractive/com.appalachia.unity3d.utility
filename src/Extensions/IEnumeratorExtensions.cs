using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace Appalachia.Utility.Extensions
{
    public static class IEnumeratorExtensions
    {
        public static void CheckTimeout(string processKey, float start, float current, float iterationTimeout)
        {
            CheckTimeout(processKey, current - start, iterationTimeout);
        }

        public static void CheckTimeout(string processKey, float elapsed, float iterationTimeout)
        {
            if (ShouldTimeout(elapsed, iterationTimeout))
            {
                var message = $"| {elapsed:F2} / {iterationTimeout:F2} | {processKey}";
                throw new TimeoutException(message);
            }
        }

        public static void Complete(this IEnumerator enumerator, string processKey, float timeout = 60f)
        {
            var start = Time.realtimeSinceStartup;

            while (enumerator.MoveNext())
            {
                CheckTimeout(processKey, start, Time.realtimeSinceStartup, timeout);
            }
        }

        public static IEnumerator OnComplete(
            this IEnumerator enumerator,
            string processKey,
            Action action,
            float timeout = 60f)
        {
            var start = Time.time;

            while (enumerator.MoveNext())
            {
                CheckTimeout(processKey, start, Time.time, timeout);

                yield return enumerator.Current;
            }

            action();
        }
        
        public static bool ShouldTimeout(double start, float current, float iterationTimeout)
        {
            return ShouldTimeout(current - (float)start, iterationTimeout);
        }

        
        public static bool ShouldTimeout(double start, double current, float iterationTimeout)
        {
            return ShouldTimeout((float)current - (float)start, iterationTimeout);
        }
        
        public static bool ShouldTimeout(float start, float current, float iterationTimeout)
        {
            return ShouldTimeout(current - start, iterationTimeout);
        }

        public static bool ShouldTimeout(double elapsed, float iterationTimeout)
        {
            return ShouldTimeout((float)elapsed, iterationTimeout);
        }

        public static bool ShouldTimeout(float elapsed, float iterationTimeout)
        {
            if (elapsed > iterationTimeout)
            {
                if (!Debugger.IsAttached)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
