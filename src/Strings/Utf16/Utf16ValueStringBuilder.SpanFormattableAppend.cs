using System;
using System.Runtime.CompilerServices;

namespace Appalachia.Utility.Strings
{
    public partial struct Utf16ValueStringBuilder
    {
        /// <summary>Appends the string representation of a specified value to this instance.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(byte value)
        {
            if (!value.TryFormat(buffer.AsSpan(index), out var written))
            {
                Grow(written);
                if (!value.TryFormat(buffer.AsSpan(index), out written))
                {
                    ThrowArgumentException(nameof(value));
                }
            }

            index += written;
        }

        /// <summary>Appends the string representation of a specified value to this instance with numeric format strings.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(byte value, string format)
        {
            if (!value.TryFormat(buffer.AsSpan(index), out var written, format.AsSpan()))
            {
                Grow(written);
                if (!value.TryFormat(buffer.AsSpan(index), out written, format.AsSpan()))
                {
                    ThrowArgumentException(nameof(value));
                }
            }

            index += written;
        }

        /// <summary>Appends the string representation of a specified value followed by the default line terminator to the end of this instance.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AppendLine(byte value)
        {
            Append(value);
            AppendLine();
        }

        /// <summary>
        ///     Appends the string representation of a specified value with numeric format strings followed by the default line terminator to the end of
        ///     this instance.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AppendLine(byte value, string format)
        {
            Append(value, format);
            AppendLine();
        }

        /// <summary>Appends the string representation of a specified value to this instance.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(DateTime value)
        {
            if (!value.TryFormat(buffer.AsSpan(index), out var written))
            {
                Grow(written);
                if (!value.TryFormat(buffer.AsSpan(index), out written))
                {
                    ThrowArgumentException(nameof(value));
                }
            }

            index += written;
        }

        /// <summary>Appends the string representation of a specified value to this instance with numeric format strings.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(DateTime value, string format)
        {
            if (!value.TryFormat(buffer.AsSpan(index), out var written, format.AsSpan()))
            {
                Grow(written);
                if (!value.TryFormat(buffer.AsSpan(index), out written, format.AsSpan()))
                {
                    ThrowArgumentException(nameof(value));
                }
            }

            index += written;
        }

        /// <summary>Appends the string representation of a specified value followed by the default line terminator to the end of this instance.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AppendLine(DateTime value)
        {
            Append(value);
            AppendLine();
        }

        /// <summary>
        ///     Appends the string representation of a specified value with numeric format strings followed by the default line terminator to the end of
        ///     this instance.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AppendLine(DateTime value, string format)
        {
            Append(value, format);
            AppendLine();
        }

        /// <summary>Appends the string representation of a specified value to this instance.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(DateTimeOffset value)
        {
            if (!value.TryFormat(buffer.AsSpan(index), out var written))
            {
                Grow(written);
                if (!value.TryFormat(buffer.AsSpan(index), out written))
                {
                    ThrowArgumentException(nameof(value));
                }
            }

            index += written;
        }

        /// <summary>Appends the string representation of a specified value to this instance with numeric format strings.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(DateTimeOffset value, string format)
        {
            if (!value.TryFormat(buffer.AsSpan(index), out var written, format.AsSpan()))
            {
                Grow(written);
                if (!value.TryFormat(buffer.AsSpan(index), out written, format.AsSpan()))
                {
                    ThrowArgumentException(nameof(value));
                }
            }

            index += written;
        }

        /// <summary>Appends the string representation of a specified value followed by the default line terminator to the end of this instance.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AppendLine(DateTimeOffset value)
        {
            Append(value);
            AppendLine();
        }

        /// <summary>
        ///     Appends the string representation of a specified value with numeric format strings followed by the default line terminator to the end of
        ///     this instance.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AppendLine(DateTimeOffset value, string format)
        {
            Append(value, format);
            AppendLine();
        }

        /// <summary>Appends the string representation of a specified value to this instance.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(decimal value)
        {
            if (!value.TryFormat(buffer.AsSpan(index), out var written))
            {
                Grow(written);
                if (!value.TryFormat(buffer.AsSpan(index), out written))
                {
                    ThrowArgumentException(nameof(value));
                }
            }

            index += written;
        }

        /// <summary>Appends the string representation of a specified value to this instance with numeric format strings.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(decimal value, string format)
        {
            if (!value.TryFormat(buffer.AsSpan(index), out var written, format.AsSpan()))
            {
                Grow(written);
                if (!value.TryFormat(buffer.AsSpan(index), out written, format.AsSpan()))
                {
                    ThrowArgumentException(nameof(value));
                }
            }

            index += written;
        }

        /// <summary>Appends the string representation of a specified value followed by the default line terminator to the end of this instance.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AppendLine(decimal value)
        {
            Append(value);
            AppendLine();
        }

        /// <summary>
        ///     Appends the string representation of a specified value with numeric format strings followed by the default line terminator to the end of
        ///     this instance.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AppendLine(decimal value, string format)
        {
            Append(value, format);
            AppendLine();
        }

        /// <summary>Appends the string representation of a specified value to this instance.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(double value)
        {
            if (!value.TryFormat(buffer.AsSpan(index), out var written))
            {
                Grow(written);
                if (!value.TryFormat(buffer.AsSpan(index), out written))
                {
                    ThrowArgumentException(nameof(value));
                }
            }

            index += written;
        }

        /// <summary>Appends the string representation of a specified value to this instance with numeric format strings.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(double value, string format)
        {
            if (!value.TryFormat(buffer.AsSpan(index), out var written, format.AsSpan()))
            {
                Grow(written);
                if (!value.TryFormat(buffer.AsSpan(index), out written, format.AsSpan()))
                {
                    ThrowArgumentException(nameof(value));
                }
            }

            index += written;
        }

        /// <summary>Appends the string representation of a specified value followed by the default line terminator to the end of this instance.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AppendLine(double value)
        {
            Append(value);
            AppendLine();
        }

        /// <summary>
        ///     Appends the string representation of a specified value with numeric format strings followed by the default line terminator to the end of
        ///     this instance.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AppendLine(double value, string format)
        {
            Append(value, format);
            AppendLine();
        }

        /// <summary>Appends the string representation of a specified value to this instance.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(short value)
        {
            if (!value.TryFormat(buffer.AsSpan(index), out var written))
            {
                Grow(written);
                if (!value.TryFormat(buffer.AsSpan(index), out written))
                {
                    ThrowArgumentException(nameof(value));
                }
            }

            index += written;
        }

        /// <summary>Appends the string representation of a specified value to this instance with numeric format strings.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(short value, string format)
        {
            if (!value.TryFormat(buffer.AsSpan(index), out var written, format.AsSpan()))
            {
                Grow(written);
                if (!value.TryFormat(buffer.AsSpan(index), out written, format.AsSpan()))
                {
                    ThrowArgumentException(nameof(value));
                }
            }

            index += written;
        }

        /// <summary>Appends the string representation of a specified value followed by the default line terminator to the end of this instance.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AppendLine(short value)
        {
            Append(value);
            AppendLine();
        }

        /// <summary>
        ///     Appends the string representation of a specified value with numeric format strings followed by the default line terminator to the end of
        ///     this instance.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AppendLine(short value, string format)
        {
            Append(value, format);
            AppendLine();
        }

        /// <summary>Appends the string representation of a specified value to this instance.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(int value)
        {
            if (!value.TryFormat(buffer.AsSpan(index), out var written))
            {
                Grow(written);
                if (!value.TryFormat(buffer.AsSpan(index), out written))
                {
                    ThrowArgumentException(nameof(value));
                }
            }

            index += written;
        }

        /// <summary>Appends the string representation of a specified value to this instance with numeric format strings.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(int value, string format)
        {
            if (!value.TryFormat(buffer.AsSpan(index), out var written, format.AsSpan()))
            {
                Grow(written);
                if (!value.TryFormat(buffer.AsSpan(index), out written, format.AsSpan()))
                {
                    ThrowArgumentException(nameof(value));
                }
            }

            index += written;
        }

        /// <summary>Appends the string representation of a specified value followed by the default line terminator to the end of this instance.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AppendLine(int value)
        {
            Append(value);
            AppendLine();
        }

        /// <summary>
        ///     Appends the string representation of a specified value with numeric format strings followed by the default line terminator to the end of
        ///     this instance.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AppendLine(int value, string format)
        {
            Append(value, format);
            AppendLine();
        }

        /// <summary>Appends the string representation of a specified value to this instance.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(long value)
        {
            if (!value.TryFormat(buffer.AsSpan(index), out var written))
            {
                Grow(written);
                if (!value.TryFormat(buffer.AsSpan(index), out written))
                {
                    ThrowArgumentException(nameof(value));
                }
            }

            index += written;
        }

        /// <summary>Appends the string representation of a specified value to this instance with numeric format strings.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(long value, string format)
        {
            if (!value.TryFormat(buffer.AsSpan(index), out var written, format.AsSpan()))
            {
                Grow(written);
                if (!value.TryFormat(buffer.AsSpan(index), out written, format.AsSpan()))
                {
                    ThrowArgumentException(nameof(value));
                }
            }

            index += written;
        }

        /// <summary>Appends the string representation of a specified value followed by the default line terminator to the end of this instance.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AppendLine(long value)
        {
            Append(value);
            AppendLine();
        }

        /// <summary>
        ///     Appends the string representation of a specified value with numeric format strings followed by the default line terminator to the end of
        ///     this instance.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AppendLine(long value, string format)
        {
            Append(value, format);
            AppendLine();
        }

        /// <summary>Appends the string representation of a specified value to this instance.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(sbyte value)
        {
            if (!value.TryFormat(buffer.AsSpan(index), out var written))
            {
                Grow(written);
                if (!value.TryFormat(buffer.AsSpan(index), out written))
                {
                    ThrowArgumentException(nameof(value));
                }
            }

            index += written;
        }

        /// <summary>Appends the string representation of a specified value to this instance with numeric format strings.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(sbyte value, string format)
        {
            if (!value.TryFormat(buffer.AsSpan(index), out var written, format.AsSpan()))
            {
                Grow(written);
                if (!value.TryFormat(buffer.AsSpan(index), out written, format.AsSpan()))
                {
                    ThrowArgumentException(nameof(value));
                }
            }

            index += written;
        }

        /// <summary>Appends the string representation of a specified value followed by the default line terminator to the end of this instance.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AppendLine(sbyte value)
        {
            Append(value);
            AppendLine();
        }

        /// <summary>
        ///     Appends the string representation of a specified value with numeric format strings followed by the default line terminator to the end of
        ///     this instance.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AppendLine(sbyte value, string format)
        {
            Append(value, format);
            AppendLine();
        }

        /// <summary>Appends the string representation of a specified value to this instance.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(float value)
        {
            if (!value.TryFormat(buffer.AsSpan(index), out var written))
            {
                Grow(written);
                if (!value.TryFormat(buffer.AsSpan(index), out written))
                {
                    ThrowArgumentException(nameof(value));
                }
            }

            index += written;
        }

        /// <summary>Appends the string representation of a specified value to this instance with numeric format strings.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(float value, string format)
        {
            if (!value.TryFormat(buffer.AsSpan(index), out var written, format.AsSpan()))
            {
                Grow(written);
                if (!value.TryFormat(buffer.AsSpan(index), out written, format.AsSpan()))
                {
                    ThrowArgumentException(nameof(value));
                }
            }

            index += written;
        }

        /// <summary>Appends the string representation of a specified value followed by the default line terminator to the end of this instance.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AppendLine(float value)
        {
            Append(value);
            AppendLine();
        }

        /// <summary>
        ///     Appends the string representation of a specified value with numeric format strings followed by the default line terminator to the end of
        ///     this instance.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AppendLine(float value, string format)
        {
            Append(value, format);
            AppendLine();
        }

        /// <summary>Appends the string representation of a specified value to this instance.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(TimeSpan value)
        {
            if (!value.TryFormat(buffer.AsSpan(index), out var written))
            {
                Grow(written);
                if (!value.TryFormat(buffer.AsSpan(index), out written))
                {
                    ThrowArgumentException(nameof(value));
                }
            }

            index += written;
        }

        /// <summary>Appends the string representation of a specified value to this instance with numeric format strings.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(TimeSpan value, string format)
        {
            if (!value.TryFormat(buffer.AsSpan(index), out var written, format.AsSpan()))
            {
                Grow(written);
                if (!value.TryFormat(buffer.AsSpan(index), out written, format.AsSpan()))
                {
                    ThrowArgumentException(nameof(value));
                }
            }

            index += written;
        }

        /// <summary>Appends the string representation of a specified value followed by the default line terminator to the end of this instance.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AppendLine(TimeSpan value)
        {
            Append(value);
            AppendLine();
        }

        /// <summary>
        ///     Appends the string representation of a specified value with numeric format strings followed by the default line terminator to the end of
        ///     this instance.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AppendLine(TimeSpan value, string format)
        {
            Append(value, format);
            AppendLine();
        }

        /// <summary>Appends the string representation of a specified value to this instance.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(ushort value)
        {
            if (!value.TryFormat(buffer.AsSpan(index), out var written))
            {
                Grow(written);
                if (!value.TryFormat(buffer.AsSpan(index), out written))
                {
                    ThrowArgumentException(nameof(value));
                }
            }

            index += written;
        }

        /// <summary>Appends the string representation of a specified value to this instance with numeric format strings.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(ushort value, string format)
        {
            if (!value.TryFormat(buffer.AsSpan(index), out var written, format.AsSpan()))
            {
                Grow(written);
                if (!value.TryFormat(buffer.AsSpan(index), out written, format.AsSpan()))
                {
                    ThrowArgumentException(nameof(value));
                }
            }

            index += written;
        }

        /// <summary>Appends the string representation of a specified value followed by the default line terminator to the end of this instance.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AppendLine(ushort value)
        {
            Append(value);
            AppendLine();
        }

        /// <summary>
        ///     Appends the string representation of a specified value with numeric format strings followed by the default line terminator to the end of
        ///     this instance.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AppendLine(ushort value, string format)
        {
            Append(value, format);
            AppendLine();
        }

        /// <summary>Appends the string representation of a specified value to this instance.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(uint value)
        {
            if (!value.TryFormat(buffer.AsSpan(index), out var written))
            {
                Grow(written);
                if (!value.TryFormat(buffer.AsSpan(index), out written))
                {
                    ThrowArgumentException(nameof(value));
                }
            }

            index += written;
        }

        /// <summary>Appends the string representation of a specified value to this instance with numeric format strings.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(uint value, string format)
        {
            if (!value.TryFormat(buffer.AsSpan(index), out var written, format.AsSpan()))
            {
                Grow(written);
                if (!value.TryFormat(buffer.AsSpan(index), out written, format.AsSpan()))
                {
                    ThrowArgumentException(nameof(value));
                }
            }

            index += written;
        }

        /// <summary>Appends the string representation of a specified value followed by the default line terminator to the end of this instance.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AppendLine(uint value)
        {
            Append(value);
            AppendLine();
        }

        /// <summary>
        ///     Appends the string representation of a specified value with numeric format strings followed by the default line terminator to the end of
        ///     this instance.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AppendLine(uint value, string format)
        {
            Append(value, format);
            AppendLine();
        }

        /// <summary>Appends the string representation of a specified value to this instance.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(ulong value)
        {
            if (!value.TryFormat(buffer.AsSpan(index), out var written))
            {
                Grow(written);
                if (!value.TryFormat(buffer.AsSpan(index), out written))
                {
                    ThrowArgumentException(nameof(value));
                }
            }

            index += written;
        }

        /// <summary>Appends the string representation of a specified value to this instance with numeric format strings.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(ulong value, string format)
        {
            if (!value.TryFormat(buffer.AsSpan(index), out var written, format.AsSpan()))
            {
                Grow(written);
                if (!value.TryFormat(buffer.AsSpan(index), out written, format.AsSpan()))
                {
                    ThrowArgumentException(nameof(value));
                }
            }

            index += written;
        }

        /// <summary>Appends the string representation of a specified value followed by the default line terminator to the end of this instance.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AppendLine(ulong value)
        {
            Append(value);
            AppendLine();
        }

        /// <summary>
        ///     Appends the string representation of a specified value with numeric format strings followed by the default line terminator to the end of
        ///     this instance.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AppendLine(ulong value, string format)
        {
            Append(value, format);
            AppendLine();
        }

        /// <summary>Appends the string representation of a specified value to this instance.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(Guid value)
        {
            if (!value.TryFormat(buffer.AsSpan(index), out var written))
            {
                Grow(written);
                if (!value.TryFormat(buffer.AsSpan(index), out written))
                {
                    ThrowArgumentException(nameof(value));
                }
            }

            index += written;
        }

        /// <summary>Appends the string representation of a specified value to this instance with numeric format strings.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(Guid value, string format)
        {
            if (!value.TryFormat(buffer.AsSpan(index), out var written, format.AsSpan()))
            {
                Grow(written);
                if (!value.TryFormat(buffer.AsSpan(index), out written, format.AsSpan()))
                {
                    ThrowArgumentException(nameof(value));
                }
            }

            index += written;
        }

        /// <summary>Appends the string representation of a specified value followed by the default line terminator to the end of this instance.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AppendLine(Guid value)
        {
            Append(value);
            AppendLine();
        }

        /// <summary>
        ///     Appends the string representation of a specified value with numeric format strings followed by the default line terminator to the end of
        ///     this instance.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AppendLine(Guid value, string format)
        {
            Append(value, format);
            AppendLine();
        }
    }
}
