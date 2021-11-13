using Appalachia.Utility.Logging.Contexts.Base;
using UnityEngine;

namespace Appalachia.Utility.Logging.Contexts
{
    public class Bootload : AppaLogContext<Bootload>
    {
        protected override AppaLogFormats.LogFormat GetPrefixFormat()
        {
            return formats.contexts.bootload;
        }
    }
}
