using System;
using UnityEngine;

namespace Appalachia.Utility.Constants
{
#if UNITY_EDITOR
    [UnityEditor.InitializeOnLoad]
#endif
    public static class APPASERIALIZE
    {
        static APPASERIALIZE()
        {
            Initialize();
        }

        #region Static Fields and Autoproperties

        [NonSerialized] private static int _isDeserializing;

        [NonSerialized] private static int _isSerializing;

        [NonSerialized] private static uint _frame;

        #endregion

        public static bool CouldBeInSerializationWindow => (_frame == 0) || InSerializationWindowGuaranteed;
        public static bool InSerializationWindowGuaranteed => IsSerializing || IsDeserializing;
        public static bool IsDeserializing => _isDeserializing > 0;

        public static bool IsSerializing => _isSerializing > 0;

        public static void FrameIncrement()
        {
            _frame += 1;
        }

        public static IDisposable OnAfterDeserialize()
        {
            return new SerializationScope(false);
        }

        public static IDisposable OnAssetEditing()
        {
            return new SerializationScope(false);
        }

        public static IDisposable OnBeforeSerialize()
        {
            return new SerializationScope(true);
        }

#if UNITY_EDITOR
        [UnityEditor.Callbacks.DidReloadScripts]
#endif
        public static void ResetFrameCount()
        {
            _frame = 0;
        }

        [RuntimeInitializeOnLoadMethod]
        private static void Initialize()
        {
            _isSerializing = 0;
            _isDeserializing = 0;
        }

        #region Nested type: SerializationScope

        private class SerializationScope : IDisposable
        {
            public SerializationScope(bool serializing)
            {
                _serializing = serializing;

                if (_serializing)
                {
                    _isSerializing += 1;
                }
                else
                {
                    _isDeserializing += 1;
                }
            }

            #region Fields and Autoproperties

            private readonly bool _serializing;

            #endregion

            #region IDisposable Members

            public void Dispose()
            {
                if (_serializing)
                {
                    _isSerializing -= 1;
                }
                else
                {
                    _isDeserializing -= 1;
                }
            }

            #endregion
        }

        #endregion
    }
}
