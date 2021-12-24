#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Collections.Generic;

namespace Appalachia.Utility.Async
{
    public static class EnumerableAsyncExtensions
    {
        // overload resolver - .Select(async x => { }) : IEnumerable<AppaTask<T>>

        public static IEnumerable<AppaTask> Select<T>(this IEnumerable<T> source, Func<T, AppaTask> selector)
        {
            return System.Linq.Enumerable.Select(source, selector);
        }

        public static IEnumerable<AppaTask<TR>> Select<T, TR>(
            this IEnumerable<T> source,
            Func<T, AppaTask<TR>> selector)
        {
            return System.Linq.Enumerable.Select(source, selector);
        }

        public static IEnumerable<AppaTask> Select<T>(
            this IEnumerable<T> source,
            Func<T, int, AppaTask> selector)
        {
            return System.Linq.Enumerable.Select(source, selector);
        }

        public static IEnumerable<AppaTask<TR>> Select<T, TR>(
            this IEnumerable<T> source,
            Func<T, int, AppaTask<TR>> selector)
        {
            return System.Linq.Enumerable.Select(source, selector);
        }
    }
}
