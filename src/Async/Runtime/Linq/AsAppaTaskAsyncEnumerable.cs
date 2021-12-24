namespace Appalachia.Utility.Async.Linq
{
    public static partial class AppaTaskAsyncEnumerable
    {
        public static IAppaTaskAsyncEnumerable<TSource> AsAppaTaskAsyncEnumerable<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source)
        {
            return source;
        }
    }
}
