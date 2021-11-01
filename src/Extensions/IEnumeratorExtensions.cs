using System.Collections;

namespace Appalachia.Utility.Extensions
{
    public static class IEnumeratorExtensions
    {
        public static void Complete(this IEnumerator enumerator)
        {
            while (enumerator.MoveNext())
            {
                
            }    
        }
    }
}
