#region

using System;
using Unity.Profiling;

#endregion

namespace Appalachia.Utility.Pooling.Objects
{
    public class DisposingObjectPool<T> : ObjectPool<T>
        where T : class, IDisposable
    {
        public DisposingObjectPool(Func<T> customAdd) : base(customAdd)
        {
            using (_PRF_DisposingObjectPool_DisposingObjectPool.Auto())
            {
                if (typeof(IDisposable).IsAssignableFrom(typeof(T)))
                {
                    _disposeElements = true;
                }
            }
        }

        public DisposingObjectPool(Func<T> customAdd, Action<T> customReset) : base(customAdd, customReset)
        {
            using (_PRF_DisposingObjectPool_DisposingObjectPool.Auto())
            {
                if (typeof(IDisposable).IsAssignableFrom(typeof(T)))
                {
                    _disposeElements = true;
                }
            }
        }

        public DisposingObjectPool(Func<T> customAdd, Action<T> customReset, Action<T> customPreGet) : base(
            customAdd,
            customReset,
            customPreGet
        )
        {
            using (_PRF_DisposingObjectPool_DisposingObjectPool.Auto())
            {
                if (typeof(IDisposable).IsAssignableFrom(typeof(T)))
                {
                    _disposeElements = true;
                }
            }
        }

        #region Fields and Autoproperties

        protected readonly bool _disposeElements;

        #endregion

        /// <inheritdoc />
        protected override void OnDispose()
        {
            using (_PRF_DisposingObjectPool_OnDispose.Auto())
            {
                if (_disposeElements)
                {
                    for (var i = _list.Count - 1; i >= 0; i--)
                    {
                        var item = _list[i];
                        DisposeItem(item);

                        _list.RemoveAt(i);
                    }
                }
            }
        }

        /// <inheritdoc />
        protected override void OnReset(T obj)
        {
            using (_PRF_DisposingObjectPool_OnReset.Auto())
            {
                if (_disposeElements && _isDisposed)
                {
                    DisposeItem(obj);
                }
                else
                {
                    _list.Add(obj);
                }
            }
        }

        private void DisposeItem(T item)
        {
            using (_PRF_DisposingObjectPool_DisposeItem.Auto())
            {
                if (item is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }
        }

        #region Profiling

        private static readonly ProfilerMarker _PRF_DisposingObjectPool_DisposeItem =
            new("DisposingObjectPool.DisposeItem");

        private static readonly ProfilerMarker _PRF_DisposingObjectPool_DisposingObjectPool =
            new("DisposingObjectPool.DisposingObjectPool");

        private static readonly ProfilerMarker _PRF_DisposingObjectPool_OnDispose =
            new("DisposingObjectPool.OnDispose");

        private static readonly ProfilerMarker _PRF_DisposingObjectPool_OnReset =
            new("DisposingObjectPool.OnReset");

        #endregion
    }
}
