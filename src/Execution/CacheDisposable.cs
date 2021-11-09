using System;

namespace Appalachia.Utility.Execution
{
    public class CacheDisposable<T> : IDisposable
    {
        public CacheDisposable(T initial, Action now, Action<T> onDispose)
        {
            now?.Invoke();
            _initial = initial;
            _onDispose = onDispose;
        }

        private readonly Action<T> _onDispose;

        private readonly T _initial;

        public void Dispose()
        {
            _onDispose?.Invoke(_initial);
        }
    }
}
