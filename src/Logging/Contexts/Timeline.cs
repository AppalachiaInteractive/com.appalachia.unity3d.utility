using Appalachia.Utility.Logging.Contexts.Base;
using UnityEngine;

namespace Appalachia.Utility.Logging.Contexts
{
    public class Timeline : AppaLogContext<Timeline>
    {
        protected override AppaLogFormats.LogFormat GetPrefixFormat()
        {
            return formats.contexts.timeline;
        }
    }
}
