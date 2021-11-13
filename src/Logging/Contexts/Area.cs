using Appalachia.Utility.Logging.Contexts.Base;
using UnityEngine;

namespace Appalachia.Utility.Logging.Contexts
{
    public class Area : AppaLogContext<Area>
    {
        protected override AppaLogFormats.LogFormat GetPrefixFormat()
        {
            return formats.contexts.area;
        }
    }
}
