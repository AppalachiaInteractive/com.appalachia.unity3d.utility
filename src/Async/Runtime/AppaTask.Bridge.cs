#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Collections;

namespace Appalachia.Utility.Async
{
    // UnityEngine Bridges.

    public partial struct AppaTask
    {
        public static IEnumerator ToCoroutine(Func<AppaTask> taskFactory)
        {
            return taskFactory().ToCoroutine();
        }
    }
}
