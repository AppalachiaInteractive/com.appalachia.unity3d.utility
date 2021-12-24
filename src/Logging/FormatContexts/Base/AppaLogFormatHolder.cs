using Appalachia.Utility.Constants;
using UnityEngine;

namespace Appalachia.Utility.Logging
{
    internal static class AppaLogFormatHolder
    {
        #region Static Fields and Autoproperties

        private static AppaLogFormats _formats;

        private static AppaLogFormats.Contexts? _dummyContext;
        private static AppaLogFormats.LogLevel? _dummyLogLevel;
        private static AppaLogFormats.Specials? _dummySpecials;

        #endregion

        public static bool HasFormat => _formats != null;

        internal static AppaLogFormats formats
        {
            get
            {
                if (_formats == null)
                {
                    if (APPASERIALIZE.InSerializationWindow)
                    {
                        Debug.LogWarning("Need to resolve this logging call!");
                        return null;
                    }

                    _formats = Resources.Load<AppaLogFormats>(AppaLogFormats.ADDRESS);

                    foreach (var context in Contexts.AllContexts)
                    {
                        context.Reset();
                    }
                }

                return _formats;
            }
            set => _formats = value;
        }

        internal static AppaLogFormats.Contexts contexts
        {
            get
            {
                if ((_formats == null) && APPASERIALIZE.InSerializationWindow)
                {
                    if (!_dummyContext.HasValue)
                    {
                        _dummyContext = new AppaLogFormats.Contexts();
                        _dummyContext.Value.DefaultAll();
                    }

                    return _dummyContext.Value;
                }

                return formats.contexts;
            }
        }

        internal static AppaLogFormats.LogLevel levels
        {
            get
            {
                if ((_formats == null) && APPASERIALIZE.InSerializationWindow)
                {
                    if (!_dummyLogLevel.HasValue)
                    {
                        _dummyLogLevel = new AppaLogFormats.LogLevel();
                        _dummyLogLevel.Value.DefaultAll();
                    }

                    return _dummyLogLevel.Value;
                }

                return formats.levels;
            }
        }

        internal static AppaLogFormats.Specials specials
        {
            get
            {
                if ((_formats == null) && APPASERIALIZE.InSerializationWindow)
                {
                    if (!_dummySpecials.HasValue)
                    {
                        _dummySpecials = new AppaLogFormats.Specials();
                        _dummySpecials.Value.DefaultAll();
                    }

                    return _dummySpecials.Value;
                }

                return formats.specials;
            }
        }
    }
}
