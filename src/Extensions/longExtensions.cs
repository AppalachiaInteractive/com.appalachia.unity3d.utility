using Unity.Profiling;

namespace Appalachia.Utility.Extensions
{
    public static class longExtensions
    {
        #region Profiling

        private const string _PRF_PFX = nameof(longExtensions) + ".";

        private static readonly ProfilerMarker _PRF_ToFileSize =
            new ProfilerMarker(_PRF_PFX + nameof(ToFileSize));

        #endregion

        public static string ToFileSize(this long length, int decimals = 2)
        {
            using (_PRF_ToFileSize.Auto())
            {
                var stringFormat = "0." + new string('0', decimals);

                long kb = 1024;
                var mb = kb * 1024;
                var gb = mb * 1024;
                var tb = gb * 1024;

                if (length < kb)
                {
                    return $"{length} B";
                }

                if (length < mb)
                {
                    var major = length / kb;
                    var minor = (length % kb) / (double) kb;
                    var result = major + minor;
                    var stringFormatted = result.ToString(stringFormat);

                    return $"{stringFormatted} KB";
                }

                if (length < gb)
                {
                    var major = length / mb;
                    var minor = (length % mb) / (double) mb;
                    var result = major + minor;
                    var stringFormatted = result.ToString(stringFormat);

                    return $"{stringFormatted} MB";
                }

                if (length < tb)
                {
                    var major = length / gb;
                    var minor = (length % gb) / (double) gb;
                    var result = major + minor;
                    var stringFormatted = result.ToString(stringFormat);

                    return $"{stringFormatted} GB";
                }
                else
                {
                    var major = length / tb;
                    var minor = (length % tb) / (double) tb;
                    var result = major + minor;
                    var stringFormatted = result.ToString(stringFormat);

                    return $"{stringFormatted} TB";
                }
            }
        }
    }
}
