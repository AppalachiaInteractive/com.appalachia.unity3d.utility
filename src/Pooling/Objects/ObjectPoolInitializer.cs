namespace Appalachia.Utility.Pooling.Objects
{
    public static class ObjectPoolInitializer
    {
        public static ObjectPool<T> Create<T>()
            where T : class, new()
        {
            return ObjectPoolProvider.Create<T>();
        }
    }
}
