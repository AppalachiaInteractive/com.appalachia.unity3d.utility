using System;

namespace Appalachia.Utility.Execution
{
    public class CustomDisposable : IDisposable
    {
        public CustomDisposable(Action now, Action onDispose)
        {
            now?.Invoke();
            _onDispose = onDispose;
        }

        private readonly Action _onDispose;

        public void Dispose()
        {
            _onDispose?.Invoke();
        }
    }
}
