using System.Collections.Generic;
using UnityEngine;

namespace Appalachia.Utility.Execution
{
    public class SafeCoroutineManager: MonoBehaviour
    {
        private static SafeCoroutineManager _instance;
        // ReSharper disable once CollectionNeverQueried.Local
        private List<SafeCoroutineWrapper> _executing;

        public static SafeCoroutineManager instance
        {
            get
            {
                if (_instance == null)
                {
                    var managerObj = GameObject.Find(nameof(SafeCoroutineManager));

                    if (managerObj == null)
                    {
                        managerObj = new GameObject
                        {
                            name = nameof(SafeCoroutineManager), hideFlags = HideFlags.HideAndDontSave
                        };
                    }

                    _instance = managerObj.AddComponent<SafeCoroutineManager>();
                }

                return _instance;
            }
        }

        internal void Add(SafeCoroutineWrapper wrapper)
        {
            _executing ??= new List<SafeCoroutineWrapper>();
            _executing.Add(wrapper);
        }

        internal void Completed(SafeCoroutineWrapper wrapper)
        {
            _executing.Remove(wrapper);
        }

        public int Count => _executing?.Count ?? 0;

        internal List<SafeCoroutineWrapper> GetAll()
        {
            return _executing;
        }
    }
}
