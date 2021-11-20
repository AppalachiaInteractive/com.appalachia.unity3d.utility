using System;

namespace Appalachia.Utility.Colors
{
    [Flags]
    public enum HexCodeFormat
    {
        Default = 0,
        IncludeNumberSign = 1 << 0,
        IncludeAlpha = 1 << 1,
        AlphaFirst = 1 << 2,
            
        RichText = IncludeNumberSign | IncludeAlpha,
    }
}
