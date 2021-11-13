using Appalachia.Utility.Logging.Contexts.Base;
using UnityEngine;

namespace Appalachia.Utility.Logging.Contexts
{
    public class SubArea : AppaLogContext<SubArea>
    {
        protected override AppaLogFormats.LogFormat GetPrefixFormat()
        {
            return formats.contexts.subArea;
        }
    }
}
