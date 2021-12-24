#pragma warning disable CS1591
#pragma warning disable CS0436

using System.Runtime.CompilerServices;
using Appalachia.Utility.Async.CompilerServices;

namespace Appalachia.Utility.Async
{
    [AsyncMethodBuilder(typeof(AsyncAppaTaskVoidMethodBuilder))]
    public readonly struct AppaTaskVoid
    {
        public void Forget()
        {
        }
    }
}
