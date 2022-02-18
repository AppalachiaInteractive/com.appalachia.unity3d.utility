using System;
using Unity.Profiling;

// ReSharper disable NotResolvedInText

namespace Appalachia.Utility.DataStructures.Lists
{
    public class DynamicCircularBuffer<T> : CircularBuffer<T>
        where T : IComparable<T>
    {
        public DynamicCircularBuffer(bool canOverride = true) : base(canOverride)
        {
        }

        public DynamicCircularBuffer(int length, bool canOverride = true) : base(length, canOverride)
        {
        }

        /// <inheritdoc />
        protected override void AddInternal(T value)
        {
            using (_PRF_AddInternal.Auto())
            {
                if (Count >= _circularBuffer.Length)
                {
                    var prevSize = _circularBuffer.Length;
                    var newSize =
                        prevSize > 0
                            ? prevSize * 2
                            : 2; // Size must be doubled (at least), or the shift operation below must consider IndexOutOfRange situations

                    Array.Resize(ref _circularBuffer, newSize);

                    if (_start > 0)
                    {
                        if (_start <= ((prevSize - 1) / 2))
                        {
                            // Move elements [0,_start) to the end
                            for (var i = 0; i < _start; i++)
                            {
                                _circularBuffer[i + prevSize] = _circularBuffer[i];
                                _circularBuffer[i] = default(T);
                            }
                        }
                        else
                        {
                            // Move elements [_start,prevSize) to the end
                            var delta = newSize - prevSize;
                            for (var i = prevSize - 1; i >= _start; i--)
                            {
                                _circularBuffer[i + delta] = _circularBuffer[i];
                                _circularBuffer[i] = default(T);
                            }

                            _start += delta;
                        }
                    }
                }

                InsertInternal(value);
            }
        }

        #region Profiling

        private const string _PRF_PFX = nameof(DynamicCircularBuffer<T>) + ".";

        private static readonly ProfilerMarker _PRF_AddInternal =
            new ProfilerMarker(_PRF_PFX + nameof(AddInternal));

        #endregion
    }
}
