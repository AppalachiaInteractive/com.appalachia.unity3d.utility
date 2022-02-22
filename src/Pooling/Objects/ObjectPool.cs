using System;
using System.Collections.Generic;
using Unity.Profiling;

namespace Appalachia.Utility.Pooling.Objects
{
    public class ObjectPool<T> : IDisposable
        where T : class
    {
        public ObjectPool(Func<T> customAdd) : this(customAdd, null)
        {
        }

        public ObjectPool(Func<T> customAdd, Action<T> customReset) : this(customAdd, customReset, null)
        {
        }

        public ObjectPool(Func<T> customAdd, Action<T> customReset, Action<T> customPreGet)
        {
            using (_PRF_ObjectPool_ObjectPool.Auto())
            {
                _selfPooling = typeof(SelfPoolingObject).IsAssignableFrom(typeof(T));

                _list = new List<T>(32);
                _customReset = customReset;
                _customAdd = customAdd;
                _customPreGet = customPreGet;

                OnInitialize();
            }
        }

        #region Fields and Autoproperties

        protected volatile bool _isDisposed;

        private protected readonly List<T> _list;
        private readonly Action<T> _customPreGet;

        private readonly Action<T> _customReset;

        private readonly bool _selfPooling;

        private readonly Func<T> _customAdd;

        #endregion

        public bool IsDisposed => _isDisposed;

        public T Get()
        {
            using (_PRF_ObjectPool_Get.Auto())
            {
                using (_PRF_ObjectPool_Get_DisposalCheck.Auto())
                {
                    if (_isDisposed)
                    {
                        throw new ObjectDisposedException(GetType().Name);
                    }
                }

                T item;

                using (_PRF_ObjectPool_Get_ListCheck.Auto())
                {
                    if (_list.Count == 0)
                    {
                        using (_PRF_ObjectPool_Get_ListCheck_Add.Auto())
                        {
                            item = _customAdd();
                        }
                    }
                    else
                    {
                        using (_PRF_ObjectPool_Get_ListCheck_GetLast.Auto())
                        {
                            item = _list[_list.Count - 1];
                        }

                        using (_PRF_ObjectPool_Get_ListCheck_RemoveLast.Auto())
                        {
                            _list.RemoveAt(_list.Count - 1);
                        }
                    }
                }

                using (_PRF_ObjectPool_Get_CustomPreGet.Auto())
                {
                    _customPreGet?.Invoke(item);
                }

                return item;
            }
        }

        public void Return(T obj)
        {
            using (_PRF_ObjectPool_Return.Auto())
            {
                using (_PRF_ObjectPool_Return_SelfPoolReset.Auto())
                {
                    if (_selfPooling)
                    {
                        (obj as SelfPoolingObject)?.Reset();
                    }
                }

                using (_PRF_ObjectPool_Return_CustomReset.Auto())
                {
                    _customReset?.Invoke(obj);
                }

                using (_PRF_ObjectPool_Return_OnReset.Auto())
                {
                    OnReset(obj);
                }
            }
        }

        protected virtual void OnDispose()
        {
        }

        protected virtual void OnInitialize()
        {
        }

        protected virtual void OnReset(T obj)
        {
            _list.Add(obj);
        }

        #region IDisposable Members

        public void Dispose()
        {
            using (_PRF_ObjectPool_Dispose.Auto())
            {
                _isDisposed = true;

                OnDispose();
            }
        }

        #endregion

        #region Profiling

        private static readonly ProfilerMarker _PRF_ObjectPool_Dispose = new("ObjectPool.Dispose");
        private static readonly ProfilerMarker _PRF_ObjectPool_Get = new("ObjectPool.Get");

        private static readonly ProfilerMarker _PRF_ObjectPool_Get_CustomPreGet =
            new("ObjectPool.Get.CustomPreGet");

        private static readonly ProfilerMarker _PRF_ObjectPool_Get_DisposalCheck =
            new("ObjectPool.Get.DisposalCheck");

        private static readonly ProfilerMarker _PRF_ObjectPool_Get_ListCheck =
            new("ObjectPool.Get.ListCheck");

        private static readonly ProfilerMarker _PRF_ObjectPool_Get_ListCheck_Add =
            new("ObjectPool.Get.ListCheck.Add");

        private static readonly ProfilerMarker _PRF_ObjectPool_Get_ListCheck_GetLast =
            new("ObjectPool.Get.ListCheck.GetLast");

        private static readonly ProfilerMarker _PRF_ObjectPool_Get_ListCheck_RemoveLast =
            new("ObjectPool.Get.ListCheck.RemoveLast");

        private static readonly ProfilerMarker _PRF_ObjectPool_ObjectPool = new("ObjectPool.ObjectPool");

        private static readonly ProfilerMarker _PRF_ObjectPool_Return = new("ObjectPool.Return");

        private static readonly ProfilerMarker _PRF_ObjectPool_Return_CustomReset =
            new("ObjectPool.Return.CustomReset");

        private static readonly ProfilerMarker _PRF_ObjectPool_Return_OnReset =
            new("ObjectPool.Return.CustomReset");

        private static readonly ProfilerMarker _PRF_ObjectPool_Return_SelfPoolReset =
            new("ObjectPool.Return.SelfPoolReset");

        #endregion
    }
}
