using Appalachia.Utility.Logging.Contexts.Base;
using UnityEngine;

namespace Appalachia.Utility.Logging.Contexts
{
    /// <summary>
    /// 476a6f,519e8a,8a7090,052f5f,848fa5,297045,204e4a
    /// </summary>
    public class Application : AppaLogContext<Application>
    {
        protected override AppaLogFormats.LogFormat GetPrefixFormat()
        {
            return formats.contexts.application;
        }
    }
}
