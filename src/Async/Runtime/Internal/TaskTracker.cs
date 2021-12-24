#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using Appalachia.Utility.Async.Internal;

namespace Appalachia.Utility.Async
{
    // public for add user custom.

    public static class TaskTracker
    {
#if UNITY_EDITOR

        private static int trackingId;

        public const string EnableAutoReloadKey = "AppaTaskTrackerWindow_EnableAutoReloadKey";
        public const string EnableTrackingKey = "AppaTaskTrackerWindow_EnableTrackingKey";
        public const string EnableStackTraceKey = "AppaTaskTrackerWindow_EnableStackTraceKey";

        public static class EditorEnableState
        {
            private static bool enableAutoReload;

            public static bool EnableAutoReload
            {
                get => enableAutoReload;
                set
                {
                    enableAutoReload = value;
                    UnityEditor.EditorPrefs.SetBool(EnableAutoReloadKey, value);
                }
            }

            private static bool enableTracking;

            public static bool EnableTracking
            {
                get => enableTracking;
                set
                {
                    enableTracking = value;
                    UnityEditor.EditorPrefs.SetBool(EnableTrackingKey, value);
                }
            }

            private static bool enableStackTrace;

            public static bool EnableStackTrace
            {
                get => enableStackTrace;
                set
                {
                    enableStackTrace = value;
                    UnityEditor.EditorPrefs.SetBool(EnableStackTraceKey, value);
                }
            }
        }

#endif

        private static
            List<KeyValuePair<IAppaTaskSource, (string formattedType, int trackingId, DateTime addTime, string
                stackTrace)>> listPool =
                new List<KeyValuePair<IAppaTaskSource, (string formattedType, int trackingId, DateTime addTime
                    , string stackTrace)>>();

        private static readonly
            WeakDictionary<IAppaTaskSource, (string formattedType, int trackingId, DateTime addTime, string
                stackTrace)> tracking =
                new WeakDictionary<IAppaTaskSource, (string formattedType, int trackingId, DateTime addTime,
                    string stackTrace)>();

        [Conditional("UNITY_EDITOR")]
        public static void TrackActiveTask(IAppaTaskSource task, int skipFrame)
        {
#if UNITY_EDITOR
            dirty = true;
            if (!EditorEnableState.EnableTracking)
            {
                return;
            }

            var stackTrace = EditorEnableState.EnableStackTrace
                ? new StackTrace(skipFrame, true).CleanupAsyncStackTrace()
                : "";

            string typeName;
            if (EditorEnableState.EnableStackTrace)
            {
                var sb = new StringBuilder();
                TypeBeautify(task.GetType(), sb);
                typeName = sb.ToString();
            }
            else
            {
                typeName = task.GetType().Name;
            }

            tracking.TryAdd(
                task,
                (typeName, Interlocked.Increment(ref trackingId), DateTime.UtcNow, stackTrace)
            );
#endif
        }

        [Conditional("UNITY_EDITOR")]
        public static void RemoveTracking(IAppaTaskSource task)
        {
#if UNITY_EDITOR
            dirty = true;
            if (!EditorEnableState.EnableTracking)
            {
                return;
            }

            var success = tracking.TryRemove(task);
#endif
        }

        private static bool dirty;

        public static bool CheckAndResetDirty()
        {
            var current = dirty;
            dirty = false;
            return current;
        }

        /// <summary>(trackingId, awaiterType, awaiterStatus, createdTime, stackTrace)</summary>
        public static void ForEachActiveTask(Action<int, string, AppaTaskStatus, DateTime, string> action)
        {
            lock (listPool)
            {
                var count = tracking.ToList(ref listPool, false);
                try
                {
                    for (var i = 0; i < count; i++)
                    {
                        action(
                            listPool[i].Value.trackingId,
                            listPool[i].Value.formattedType,
                            listPool[i].Key.UnsafeGetStatus(),
                            listPool[i].Value.addTime,
                            listPool[i].Value.stackTrace
                        );
                        listPool[i] = default;
                    }
                }
                catch
                {
                    listPool.Clear();
                    throw;
                }
            }
        }

        private static void TypeBeautify(Type type, StringBuilder sb)
        {
            if (type.IsNested)
            {
                // TypeBeautify(type.DeclaringType, sb);
                sb.Append(type.DeclaringType.Name);
                sb.Append(".");
            }

            if (type.IsGenericType)
            {
                var genericsStart = type.Name.IndexOf("`");
                if (genericsStart != -1)
                {
                    sb.Append(type.Name.Substring(0, genericsStart));
                }
                else
                {
                    sb.Append(type.Name);
                }

                sb.Append("<");
                var first = true;
                foreach (var item in type.GetGenericArguments())
                {
                    if (!first)
                    {
                        sb.Append(", ");
                    }

                    first = false;
                    TypeBeautify(item, sb);
                }

                sb.Append(">");
            }
            else
            {
                sb.Append(type.Name);
            }
        }

        //static string RemoveAppaTaskNamespace(string str)
        //{
        //    return str.Replace("Appalachia.Utility.Async.CompilerServices", "")
        //        .Replace("Appalachia.Utility.Async.Linq", "")
        //        .Replace("Appalachia.Utility.Async", "");
        //}
    }
}
