namespace Appalachia.Utility.Strings.Number
{
    internal static class BufferEx
    {
        internal static unsafe void ZeroMemory(byte* dest, uint len)
        {
            if (len == 0)
            {
                return;
            }

            for (var i = 0; i < len; i++)
            {
                dest[i] = 0;
            }
        }

        internal static unsafe void Memcpy(byte* dest, byte* src, int len)
        {
            if (len == 0)
            {
                return;
            }

            for (var i = 0; i < len; i++)
            {
                dest[i] = src[i];
            }
        }
    }
}
