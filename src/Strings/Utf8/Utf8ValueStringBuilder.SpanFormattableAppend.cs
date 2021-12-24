using System;
using System.Buffers;
using System.Buffers.Text;
using System.Runtime.CompilerServices;

namespace Appalachia.Utility.Strings
{
    public partial struct Utf8ValueStringBuilder
    {
        /// <summary>Appends the string representation of a specified value to this instance.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(byte value)
        {
            if (!Utf8Formatter.TryFormat(value, buffer.AsSpan(index), out var written))
            {
                Grow(written);
                if (!Utf8Formatter.TryFormat(value, buffer.AsSpan(index), out written))
                {
                    ThrowArgumentException(nameof(value));
                }
            }

            index += written;
        }

        /// <summary>Appends the string representation of a specified value to this instance with numeric format strings.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(byte value, StandardFormat format)
        {
            if (!Utf8Formatter.TryFormat(value, buffer.AsSpan(index), out var written, format))
            {
                Grow(written);
                if (!Utf8Formatter.TryFormat(value, buffer.AsSpan(index), out written, format))
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
        public void AppendLine(byte value, StandardFormat format)
        {
            Append(value, format);
            AppendLine();
        }

        /// <summary>Appends the string representation of a specified value to this instance.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(DateTime value)
        {
            if (!Utf8Formatter.TryFormat(value, buffer.AsSpan(index), out var written))
            {
                Grow(written);
                if (!Utf8Formatter.TryFormat(value, buffer.AsSpan(index), out written))
                {
                    ThrowArgumentException(nameof(value));
                }
            }

            index += written;
        }

        /// <summary>Appends the string representation of a specified value to this instance with numeric format strings.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(DateTime value, StandardFormat format)
        {
            if (!Utf8Formatter.TryFormat(value, buffer.AsSpan(index), out var written, format))
            {
                Grow(written);
                if (!Utf8Formatter.TryFormat(value, buffer.AsSpan(index), out written, format))
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
        public void AppendLine(DateTime value, StandardFormat format)
        {
            Append(value, format);
            AppendLine();
        }

        /// <summary>Appends the string representation of a specified value to this instance.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(DateTimeOffset value)
        {
            if (!Utf8Formatter.TryFormat(value, buffer.AsSpan(index), out var written))
            {
                Grow(written);
                if (!Utf8Formatter.TryFormat(value, buffer.AsSpan(index), out written))
                {
                    ThrowArgumentException(nameof(value));
                }
            }

            index += written;
        }

        /// <summary>Appends the string representation of a specified value to this instance with numeric format strings.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(DateTimeOffset value, StandardFormat format)
        {
            if (!Utf8Formatter.TryFormat(value, buffer.AsSpan(index), out var written, format))
            {
                Grow(written);
                if (!Utf8Formatter.TryFormat(value, buffer.AsSpan(index), out written, format))
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
        public void AppendLine(DateTimeOffset value, StandardFormat format)
        {
            Append(value, format);
            AppendLine();
        }

        /// <summary>Appends the string representation of a specified value to this instance.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(decimal value)
        {
            if (!Utf8Formatter.TryFormat(value, buffer.AsSpan(index), out var written))
            {
                Grow(written);
                if (!Utf8Formatter.TryFormat(value, buffer.AsSpan(index), out written))
                {
                    ThrowArgumentException(nameof(value));
                }
            }

            index += written;
        }

        /// <summary>Appends the string representation of a specified value to this instance with numeric format strings.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(decimal value, StandardFormat format)
        {
            if (!Utf8Formatter.TryFormat(value, buffer.AsSpan(index), out var written, format))
            {
                Grow(written);
                if (!Utf8Formatter.TryFormat(value, buffer.AsSpan(index), out written, format))
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
        public void AppendLine(decimal value, StandardFormat format)
        {
            Append(value, format);
            AppendLine();
        }

        /// <summary>Appends the string representation of a specified value to this instance.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(double value)
        {
            if (!Utf8Formatter.TryFormat(value, buffer.AsSpan(index), out var written))
            {
                Grow(written);
                if (!Utf8Formatter.TryFormat(value, buffer.AsSpan(index), out written))
                {
                    ThrowArgumentException(nameof(value));
                }
            }

            index += written;
        }

        /// <summary>Appends the string representation of a specified value to this instance with numeric format strings.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(double value, StandardFormat format)
        {
            if (!Utf8Formatter.TryFormat(value, buffer.AsSpan(index), out var written, format))
            {
                Grow(written);
                if (!Utf8Formatter.TryFormat(value, buffer.AsSpan(index), out written, format))
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
        public void AppendLine(double value, StandardFormat format)
        {
            Append(value, format);
            AppendLine();
        }

        /// <summary>Appends the string representation of a specified value to this instance.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(short value)
        {
            if (!Utf8Formatter.TryFormat(value, buffer.AsSpan(index), out var written))
            {
                Grow(written);
                if (!Utf8Formatter.TryFormat(value, buffer.AsSpan(index), out written))
                {
                    ThrowArgumentException(nameof(value));
                }
            }

            index += written;
        }

        /// <summary>Appends the string representation of a specified value to this instance with numeric format strings.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(short value, StandardFormat format)
        {
            if (!Utf8Formatter.TryFormat(value, buffer.AsSpan(index), out var written, format))
            {
                Grow(written);
                if (!Utf8Formatter.TryFormat(value, buffer.AsSpan(index), out written, format))
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
        public void AppendLine(short value, StandardFormat format)
        {
            Append(value, format);
            AppendLine();
        }

        /// <summary>Appends the string representation of a specified value to this instance.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(int value)
        {
            if (!Utf8Formatter.TryFormat(value, buffer.AsSpan(index), out var written))
            {
                Grow(written);
                if (!Utf8Formatter.TryFormat(value, buffer.AsSpan(index), out written))
                {
                    ThrowArgumentException(nameof(value));
                }
            }

            index += written;
        }

        /// <summary>Appends the string representation of a specified value to this instance with numeric format strings.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(int value, StandardFormat format)
        {
            if (!Utf8Formatter.TryFormat(value, buffer.AsSpan(index), out var written, format))
            {
                Grow(written);
                if (!Utf8Formatter.TryFormat(value, buffer.AsSpan(index), out written, format))
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
        public void AppendLine(int value, StandardFormat format)
        {
            Append(value, format);
            AppendLine();
        }

        /// <summary>Appends the string representation of a specified value to this instance.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(long value)
        {
            if (!Utf8Formatter.TryFormat(value, buffer.AsSpan(index), out var written))
            {
                Grow(written);
                if (!Utf8Formatter.TryFormat(value, buffer.AsSpan(index), out written))
                {
                    ThrowArgumentException(nameof(value));
                }
            }

            index += written;
        }

        /// <summary>Appends the string representation of a specified value to this instance with numeric format strings.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(long value, StandardFormat format)
        {
            if (!Utf8Formatter.TryFormat(value, buffer.AsSpan(index), out var written, format))
            {
                Grow(written);
                if (!Utf8Formatter.TryFormat(value, buffer.AsSpan(index), out written, format))
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
        public void AppendLine(long value, StandardFormat format)
        {
            Append(value, format);
            AppendLine();
        }

        /// <summary>Appends the string representation of a specified value to this instance.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(sbyte value)
        {
            if (!Utf8Formatter.TryFormat(value, buffer.AsSpan(index), out var written))
            {
                Grow(written);
                if (!Utf8Formatter.TryFormat(value, buffer.AsSpan(index), out written))
                {
                    ThrowArgumentException(nameof(value));
                }
            }

            index += written;
        }

        /// <summary>Appends the string representation of a specified value to this instance with numeric format strings.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(sbyte value, StandardFormat format)
        {
            if (!Utf8Formatter.TryFormat(value, buffer.AsSpan(index), out var written, format))
            {
                Grow(written);
                if (!Utf8Formatter.TryFormat(value, buffer.AsSpan(index), out written, format))
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
        public void AppendLine(sbyte value, StandardFormat format)
        {
            Append(value, format);
            AppendLine();
        }

        /// <summary>Appends the string representation of a specified value to this instance.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(float value)
        {
            if (!Utf8Formatter.TryFormat(value, buffer.AsSpan(index), out var written))
            {
                Grow(written);
                if (!Utf8Formatter.TryFormat(value, buffer.AsSpan(index), out written))
                {
                    ThrowArgumentException(nameof(value));
                }
            }

            index += written;
        }

        /// <summary>Appends the string representation of a specified value to this instance with numeric format strings.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(float value, StandardFormat format)
        {
            if (!Utf8Formatter.TryFormat(value, buffer.AsSpan(index), out var written, format))
            {
                Grow(written);
                if (!Utf8Formatter.TryFormat(value, buffer.AsSpan(index), out written, format))
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
        public void AppendLine(float value, StandardFormat format)
        {
            Append(value, format);
            AppendLine();
        }

        /// <summary>Appends the string representation of a specified value to this instance.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(TimeSpan value)
        {
            if (!Utf8Formatter.TryFormat(value, buffer.AsSpan(index), out var written))
            {
                Grow(written);
                if (!Utf8Formatter.TryFormat(value, buffer.AsSpan(index), out written))
                {
                    ThrowArgumentException(nameof(value));
                }
            }

            index += written;
        }

        /// <summary>Appends the string representation of a specified value to this instance with numeric format strings.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(TimeSpan value, StandardFormat format)
        {
            if (!Utf8Formatter.TryFormat(value, buffer.AsSpan(index), out var written, format))
            {
                Grow(written);
                if (!Utf8Formatter.TryFormat(value, buffer.AsSpan(index), out written, format))
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
        public void AppendLine(TimeSpan value, StandardFormat format)
        {
            Append(value, format);
            AppendLine();
        }

        /// <summary>Appends the string representation of a specified value to this instance.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(ushort value)
        {
            if (!Utf8Formatter.TryFormat(value, buffer.AsSpan(index), out var written))
            {
                Grow(written);
                if (!Utf8Formatter.TryFormat(value, buffer.AsSpan(index), out written))
                {
                    ThrowArgumentException(nameof(value));
                }
            }

            index += written;
        }

        /// <summary>Appends the string representation of a specified value to this instance with numeric format strings.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(ushort value, StandardFormat format)
        {
            if (!Utf8Formatter.TryFormat(value, buffer.AsSpan(index), out var written, format))
            {
                Grow(written);
                if (!Utf8Formatter.TryFormat(value, buffer.AsSpan(index), out written, format))
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
        public void AppendLine(ushort value, StandardFormat format)
        {
            Append(value, format);
            AppendLine();
        }

        /// <summary>Appends the string representation of a specified value to this instance.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(uint value)
        {
            if (!Utf8Formatter.TryFormat(value, buffer.AsSpan(index), out var written))
            {
                Grow(written);
                if (!Utf8Formatter.TryFormat(value, buffer.AsSpan(index), out written))
                {
                    ThrowArgumentException(nameof(value));
                }
            }

            index += written;
        }

        /// <summary>Appends the string representation of a specified value to this instance with numeric format strings.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(uint value, StandardFormat format)
        {
            if (!Utf8Formatter.TryFormat(value, buffer.AsSpan(index), out var written, format))
            {
                Grow(written);
                if (!Utf8Formatter.TryFormat(value, buffer.AsSpan(index), out written, format))
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
        public void AppendLine(uint value, StandardFormat format)
        {
            Append(value, format);
            AppendLine();
        }

        /// <summary>Appends the string representation of a specified value to this instance.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(ulong value)
        {
            if (!Utf8Formatter.TryFormat(value, buffer.AsSpan(index), out var written))
            {
                Grow(written);
                if (!Utf8Formatter.TryFormat(value, buffer.AsSpan(index), out written))
                {
                    ThrowArgumentException(nameof(value));
                }
            }

            index += written;
        }

        /// <summary>Appends the string representation of a specified value to this instance with numeric format strings.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(ulong value, StandardFormat format)
        {
            if (!Utf8Formatter.TryFormat(value, buffer.AsSpan(index), out var written, format))
            {
                Grow(written);
                if (!Utf8Formatter.TryFormat(value, buffer.AsSpan(index), out written, format))
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
        public void AppendLine(ulong value, StandardFormat format)
        {
            Append(value, format);
            AppendLine();
        }

        /// <summary>Appends the string representation of a specified value to this instance.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(Guid value)
        {
            if (!Utf8Formatter.TryFormat(value, buffer.AsSpan(index), out var written))
            {
                Grow(written);
                if (!Utf8Formatter.TryFormat(value, buffer.AsSpan(index), out written))
                {
                    ThrowArgumentException(nameof(value));
                }
            }

            index += written;
        }

        /// <summary>Appends the string representation of a specified value to this instance with numeric format strings.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(Guid value, StandardFormat format)
        {
            if (!Utf8Formatter.TryFormat(value, buffer.AsSpan(index), out var written, format))
            {
                Grow(written);
                if (!Utf8Formatter.TryFormat(value, buffer.AsSpan(index), out written, format))
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
        public void AppendLine(Guid value, StandardFormat format)
        {
            Append(value, format);
            AppendLine();
        }

        /// <summary>Appends the string representation of a specified value to this instance.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(bool value)
        {
            if (!Utf8Formatter.TryFormat(value, buffer.AsSpan(index), out var written))
            {
                Grow(written);
                if (!Utf8Formatter.TryFormat(value, buffer.AsSpan(index), out written))
                {
                    ThrowArgumentException(nameof(value));
                }
            }

            index += written;
        }

        /// <summary>Appends the string representation of a specified value to this instance with numeric format strings.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(bool value, StandardFormat format)
        {
            if (!Utf8Formatter.TryFormat(value, buffer.AsSpan(index), out var written, format))
            {
                Grow(written);
                if (!Utf8Formatter.TryFormat(value, buffer.AsSpan(index), out written, format))
                {
                    ThrowArgumentException(nameof(value));
                }
            }

            index += written;
        }

        /// <summary>Appends the string representation of a specified value followed by the default line terminator to the end of this instance.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AppendLine(bool value)
        {
            Append(value);
            AppendLine();
        }

        /// <summary>
        ///     Appends the string representation of a specified value with numeric format strings followed by the default line terminator to the end of
        ///     this instance.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AppendLine(bool value, StandardFormat format)
        {
            Append(value, format);
            AppendLine();
        }
    }
}
