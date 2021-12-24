using Appalachia.Utility.Strings;

#if UNITY_EDITOR

namespace Appalachia.Utility.AutoSave
{
    internal static class AutoSaveExtensions
    {
        public static string Format(this string formatString, params object[] args)
        {
            return ZString.Format(formatString, args);
        }
    }
}

#endif
