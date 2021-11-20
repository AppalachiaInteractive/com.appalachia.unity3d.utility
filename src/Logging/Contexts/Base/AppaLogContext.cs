using Unity.Profiling;

namespace Appalachia.Utility.Logging.Contexts.Base
{
    public abstract class AppaLogContext<T> : AppaLogContextBase
        where T : AppaLogContext<T>, new()
    {
        #region Static Fields and Autoproperties

        private static string _formattedLogPrefix;

        private static string _logPrefix;
        private static T _instance;

        #endregion

        public static T Instance
        {
            get
            {
                using (_PRF_Instance.Auto())
                {
                    if (_instance == null)
                    {
                        _instance = new T();
                    }

                    return _instance;
                }
            }
        }

        protected override string LogPrefix
        {
            get
            {
                using (_PRF_LogPrefix.Auto())
                {
                    if (_logPrefix == null)
                    {
                        _logPrefix = typeof(T).Name;
                    }

                    return _logPrefix;
                }
            }
        }

        protected override string LogPrefixFormatted
        {
            get
            {
                using (_PRF_LogPrefixFormatted.Auto())
                {
                    if (_formattedLogPrefix == null)
                    {
                        var format = GetPrefixFormat();
                        _formattedLogPrefix = format.Format(_logPrefix);
                    }

                    return _formattedLogPrefix;
                }
            }
        }

        internal void Reset()
        {
            _instance = null;
            _logPrefix = null;
            _formattedLogPrefix = null;
        }

        #region Profiling

        private const string _PRF_PFX = nameof(AppaLogContext<T>) + ".";

        private static readonly ProfilerMarker
            _PRF_Instance = new ProfilerMarker(_PRF_PFX + nameof(Instance));

        private static readonly ProfilerMarker _PRF_LogPrefix =
            new ProfilerMarker(_PRF_PFX + nameof(LogPrefix));

        private static readonly ProfilerMarker _PRF_LogPrefixFormatted =
            new ProfilerMarker(_PRF_PFX + nameof(LogPrefixFormatted));

        #endregion
    }
}
