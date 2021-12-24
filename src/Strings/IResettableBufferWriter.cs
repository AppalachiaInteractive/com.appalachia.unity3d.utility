using System.Buffers;

namespace Appalachia.Utility.Strings
{
    public interface IResettableBufferWriter<T> : IBufferWriter<T>
    {
        void Reset();
    }
}
