using System;
using System.Buffers;
using System.Runtime.CompilerServices;
using Unity.Profiling;

namespace Appalachia.Utility.Strings
{
    public partial struct Utf16ValueStringBuilder : IDisposable,
                                                    IBufferWriter<char>,
                                                    IResettableBufferWriter<char>
    {
        public delegate bool TryFormat<T>(
            T value,
            Span<char> destination,
            out int charsWritten,
            ReadOnlySpan<char> format);

        #region Constants and Static Readonly

        private const int DefaultBufferSize = 32768; // use 32K default buffer.

        private const int ThreadStaticBufferSize = 31111;

        #endregion

        static Utf16ValueStringBuilder()
        {
            var newLine = Environment.NewLine.ToCharArray();
            if (newLine.Length == 1)
            {
                // cr or lf
                newLine1 = newLine[0];
                crlf = false;
            }
            else
            {
                // crlf(windows)
                newLine1 = newLine[0];
                newLine2 = newLine[1];
                crlf = true;
            }
        }

        /// <summary>
        ///     Initializes a new instance
        /// </summary>
        /// <param name="disposeImmediately">
        ///     If true uses thread-static buffer that is faster but must return immediately.
        /// </param>
        /// <exception cref="InvalidOperationException">
        ///     This exception is thrown when <c>new StringBuilder(disposeImmediately: true)</c> or <c>ZString.CreateStringBuilder(notNested: true)</c> is
        ///     nested.
        ///     See the README.md
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Utf16ValueStringBuilder(bool disposeImmediately)
        {
            using (_PRF_Utf16ValueStringBuilder.Auto())
            {
                if (disposeImmediately && scratchBufferUsed)
                {
                    ThrowNestedException();
                }

                char[] buf;
                if (disposeImmediately)
                {
                    buf = scratchBuffer;
                    if (buf == null)
                    {
                        buf = scratchBuffer = new char[ThreadStaticBufferSize];
                    }

                    scratchBufferUsed = true;
                }
                else
                {
                    buf = ArrayPool<char>.Shared.Rent(DefaultBufferSize);
                }

                buffer = buf;
                index = 0;
                this.disposeImmediately = disposeImmediately;
            }
        }

        #region Static Fields and Autoproperties

        [ThreadStatic] internal static bool scratchBufferUsed;

        private static bool crlf;

        private static char newLine1;
        private static char newLine2;

        [ThreadStatic] private static char[] scratchBuffer;

        #endregion

        #region Fields and Autoproperties

        private bool disposeImmediately;

        private char[] buffer;
        private int index;

        #endregion

        public char this[int i]
        {
            get
            {
                if ((uint)i > (uint)Length)
                {
                    ExceptionUtil.ThrowArgumentOutOfRangeException(nameof(i));
                }

                if (i < 0)
                {
                    ExceptionUtil.ThrowArgumentOutOfRangeException(nameof(i));
                }

                return buffer[i];
            }
            set
            {
                if ((uint)i > (uint)Length)
                {
                    ExceptionUtil.ThrowArgumentOutOfRangeException(nameof(i));
                }

                if (i < 0)
                {
                    ExceptionUtil.ThrowArgumentOutOfRangeException(nameof(i));
                }

                buffer[i] = value;
            }
        }

        /// <summary>Length of written buffer.</summary>
        public int Length => index;

        /// <summary>
        ///     Supports the Nullable type for a given struct type.
        /// </summary>
        public static void EnableNullableFormat<T>()
            where T : struct
        {
            using (_PRF_EnableNullableFormat.Auto())
            {
                RegisterTryFormat(CreateNullableFormatter<T>());
            }
        }

        /// <summary>
        ///     Register custom formatter
        /// </summary>
        public static void RegisterTryFormat<T>(TryFormat<T> formatMethod)
        {
            using (_PRF_RegisterTryFormat.Auto())
            {
                FormatterCache<T>.TryFormatDelegate = formatMethod;
            }
        }

        /// <summary>Converts the value of this instance to a System.String.</summary>
        public override string ToString()
        {
            using (_PRF_ToString.Auto())
            {
                if (index == 0)
                {
                    return string.Empty;
                }

                return new string(buffer, 0, index);
            }
        }

        /// <summary>Appends the string representation of a specified value to this instance.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(char value)
        {
            using (_PRF_Append.Auto())
            {
                if ((buffer.Length - index) < 1)
                {
                    Grow(1);
                }

                buffer[index++] = value;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(char value, int repeatCount)
        {
            using (_PRF_Append.Auto())
            {
                if (repeatCount < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(repeatCount));
                }

                GetSpan(repeatCount).Fill(value);
                Advance(repeatCount);
            }
        }

        /// <summary>Appends the string representation of a specified value to this instance.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(string value)
        {
            using (_PRF_Append.Auto())
            {
                if ((buffer.Length - index) < value.Length)
                {
                    Grow(value.Length);
                }

                value.CopyTo(0, buffer, index, value.Length);
                index += value.Length;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(string value, int startIndex, int count)
        {
            using (_PRF_Append.Auto())
            {
                if (value == null)
                {
                    if ((startIndex == 0) && (count == 0))
                    {
                        return;
                    }

                    throw new ArgumentNullException(nameof(value));
                }

#if UNITY_2018_3_OR_NEWER || NETSTANDARD2_0
                if ((buffer.Length - index) < count)
                {
                    Grow(count);
                }

                value.CopyTo(startIndex, buffer, index, count);
                index += count;
#else
            Append(value.AsSpan(startIndex, count));
#endif
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(char[] value, int startIndex, int charCount)
        {
            using (_PRF_Append.Auto())
            {
                if ((buffer.Length - index) < charCount)
                {
                    Grow(charCount);
                }

                Array.Copy(value, startIndex, buffer, index, charCount);
                index += charCount;
            }
        }

        /// <summary>Appends a contiguous region of arbitrary memory to this instance.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(ReadOnlySpan<char> value)
        {
            using (_PRF_Append.Auto())
            {
                if ((buffer.Length - index) < value.Length)
                {
                    Grow(value.Length);
                }

                value.CopyTo(buffer.AsSpan(index));
                index += value.Length;
            }
        }

        /// <summary>Appends the string representation of a specified value to this instance.</summary>
        public void Append<T>(T value)
        {
            using (_PRF_Append.Auto())
            {
                if (!FormatterCache<T>.TryFormatDelegate(
                        value,
                        buffer.AsSpan(index),
                        out var written,
                        default
                    ))
                {
                    Grow(written);
                    if (!FormatterCache<T>.TryFormatDelegate(
                            value,
                            buffer.AsSpan(index),
                            out written,
                            default
                        ))
                    {
                        ThrowArgumentException(nameof(value));
                    }
                }

                index += written;
            }
        }

        /// <summary>Appends the default line terminator to the end of this instance.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AppendLine()
        {
            using (_PRF_AppendLine.Auto())
            {
                if (crlf)
                {
                    if ((buffer.Length - index) < 2)
                    {
                        Grow(2);
                    }

                    buffer[index] = newLine1;
                    buffer[index + 1] = newLine2;
                    index += 2;
                }
                else
                {
                    if ((buffer.Length - index) < 1)
                    {
                        Grow(1);
                    }

                    buffer[index] = newLine1;
                    index += 1;
                }
            }
        }

        /// <summary>Appends the string representation of a specified value followed by the default line terminator to the end of this instance.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AppendLine(char value)
        {
            using (_PRF_AppendLine.Auto())
            {
                Append(value);
                AppendLine();
            }
        }

        /// <summary>Appends the string representation of a specified value followed by the default line terminator to the end of this instance.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AppendLine(string value)
        {
            using (_PRF_AppendLine.Auto())
            {
                Append(value);
                AppendLine();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AppendLine(ReadOnlySpan<char> value)
        {
            using (_PRF_AppendLine.Auto())
            {
                Append(value);
                AppendLine();
            }
        }

        /// <summary>Appends the string representation of a specified value followed by the default line terminator to the end of this instance.</summary>
        public void AppendLine<T>(T value)
        {
            using (_PRF_AppendLine.Auto())
            {
                Append(value);
                AppendLine();
            }
        }

        /// <summary>Get the written buffer data.</summary>
        public ArraySegment<char> AsArraySegment()
        {
            using (_PRF_AsArraySegment.Auto())
            {
                return new ArraySegment<char>(buffer, 0, index);
            }
        }

        /// <summary>Get the written buffer data.</summary>
        public ReadOnlyMemory<char> AsMemory()
        {
            using (_PRF_AsMemory.Auto())
            {
                return buffer.AsMemory(0, index);
            }
        }

        /// <summary>Get the written buffer data.</summary>
        public ReadOnlySpan<char> AsSpan()
        {
            using (_PRF_AsSpan.Auto())
            {
                return buffer.AsSpan(0, index);
            }
        }

        public void Clear()
        {
            using (_PRF_Clear.Auto())
            {
                index = 0;
            }
        }

        public void Grow(int sizeHint)
        {
            using (_PRF_Grow.Auto())
            {
                var nextSize = buffer.Length * 2;
                if (sizeHint != 0)
                {
                    nextSize = Math.Max(nextSize, index + sizeHint);
                }

                var newBuffer = ArrayPool<char>.Shared.Rent(nextSize);

                buffer.CopyTo(newBuffer, 0);
                if (buffer.Length != ThreadStaticBufferSize)
                {
                    ArrayPool<char>.Shared.Return(buffer);
                }

                buffer = newBuffer;
            }
        }

        /// <summary>
        ///     Inserts a string 0 or more times into this builder at the specified position.
        /// </summary>
        /// <param name="index">The index to insert in this builder.</param>
        /// <param name="value">The string to insert.</param>
        /// <param name="count">The number of times to insert the string.</param>
        public void Insert(int index, string value, int count)
        {
            using (_PRF_Insert.Auto())
            {
                Insert(index, value.AsSpan(), count);
            }
        }

        public void Insert(int index, string value)
        {
            using (_PRF_Insert.Auto())
            {
                Insert(index, value.AsSpan(), 1);
            }
        }

        public void Insert(int index, ReadOnlySpan<char> value, int count)
        {
            using (_PRF_Insert.Auto())
            {
                if (count < 0)
                {
                    ExceptionUtil.ThrowArgumentOutOfRangeException(nameof(count));
                }

                var currentLength = Length;
                if ((uint)index > (uint)currentLength)
                {
                    ExceptionUtil.ThrowArgumentOutOfRangeException(nameof(index));
                }

                if ((value.Length == 0) || (count == 0))
                {
                    return;
                }

                var newSize = index + (value.Length * count);
                var newBuffer = ArrayPool<char>.Shared.Rent(Math.Max(DefaultBufferSize, newSize));

                buffer.AsSpan(0, index).CopyTo(newBuffer);
                var newBufferIndex = index;

                for (var i = 0; i < count; i++)
                {
                    value.CopyTo(newBuffer.AsSpan(newBufferIndex));
                    newBufferIndex += value.Length;
                }

                var remainLnegth = this.index - index;
                buffer.AsSpan(index, remainLnegth).CopyTo(newBuffer.AsSpan(newBufferIndex));

                if (buffer.Length != ThreadStaticBufferSize)
                {
                    if (buffer != null)
                    {
                        ArrayPool<char>.Shared.Return(buffer);
                    }
                }

                buffer = newBuffer;
                this.index = newBufferIndex + remainLnegth;
            }
        }

        /// <summary>
        ///     Removes a range of characters from this builder.
        /// </summary>
        /// <remarks>
        ///     This method does not reduce the capacity of this builder.
        /// </remarks>
        public void Remove(int startIndex, int length)
        {
            using (_PRF_Remove.Auto())
            {
                if (length < 0)
                {
                    ExceptionUtil.ThrowArgumentOutOfRangeException(nameof(length));
                }

                if (startIndex < 0)
                {
                    ExceptionUtil.ThrowArgumentOutOfRangeException(nameof(startIndex));
                }

                if (length > (Length - startIndex))
                {
                    ExceptionUtil.ThrowArgumentOutOfRangeException(nameof(length));
                }

                if ((Length == length) && (startIndex == 0))
                {
                    index = 0;
                    return;
                }

                if (length == 0)
                {
                    return;
                }

                var remain = startIndex + length;
                buffer.AsSpan(remain, Length - remain).CopyTo(buffer.AsSpan(startIndex));
                index -= length;
            }
        }

        /// <summary>
        ///     Replaces all instances of one character with another in this builder.
        /// </summary>
        /// <param name="oldChar">The character to replace.</param>
        /// <param name="newChar">The character to replace <paramref name="oldChar" /> with.</param>
        public void Replace(char oldChar, char newChar)
        {
            Replace(oldChar, newChar, 0, Length);
        }

        /// <summary>
        ///     Replaces all instances of one character with another in this builder.
        /// </summary>
        /// <param name="oldChar">The character to replace.</param>
        /// <param name="newChar">The character to replace <paramref name="oldChar" /> with.</param>
        /// <param name="startIndex">The index to start in this builder.</param>
        /// <param name="count">The number of characters to read in this builder.</param>
        public void Replace(char oldChar, char newChar, int startIndex, int count)
        {
            using (_PRF_Replace.Auto())
            {
                var currentLength = Length;
                if ((uint)startIndex > (uint)currentLength)
                {
                    ExceptionUtil.ThrowArgumentOutOfRangeException(nameof(startIndex));
                }

                if ((count < 0) || (startIndex > (currentLength - count)))
                {
                    ExceptionUtil.ThrowArgumentOutOfRangeException(nameof(count));
                }

                var endIndex = startIndex + count;

                for (var i = startIndex; i < endIndex; i++)
                {
                    if (buffer[i] == oldChar)
                    {
                        buffer[i] = newChar;
                    }
                }
            }
        }

        /// <summary>
        ///     Replaces all instances of one string with another in this builder.
        /// </summary>
        /// <param name="oldValue">The string to replace.</param>
        /// <param name="newValue">The string to replace <paramref name="oldValue" /> with.</param>
        /// <remarks>
        ///     If <paramref name="newValue" /> is <c>null</c>, instances of <paramref name="oldValue" />
        ///     are removed from this builder.
        /// </remarks>
        public void Replace(string oldValue, string newValue)
        {
            using (_PRF_Replace.Auto())
            {
                Replace(oldValue, newValue, 0, Length);
            }
        }

        public void Replace(ReadOnlySpan<char> oldValue, ReadOnlySpan<char> newValue)
        {
            using (_PRF_Replace.Auto())
            {
                Replace(oldValue, newValue, 0, Length);
            }
        }

        /// <summary>
        ///     Replaces all instances of one string with another in part of this builder.
        /// </summary>
        /// <param name="oldValue">The string to replace.</param>
        /// <param name="newValue">The string to replace <paramref name="oldValue" /> with.</param>
        /// <param name="startIndex">The index to start in this builder.</param>
        /// <param name="count">The number of characters to read in this builder.</param>
        /// <remarks>
        ///     If <paramref name="newValue" /> is <c>null</c>, instances of <paramref name="oldValue" />
        ///     are removed from this builder.
        /// </remarks>
        public void Replace(string oldValue, string newValue, int startIndex, int count)
        {
            using (_PRF_Replace.Auto())
            {
                if (oldValue == null)
                {
                    throw new ArgumentNullException(nameof(oldValue));
                }

                Replace(oldValue.AsSpan(), newValue.AsSpan(), startIndex, count);
            }
        }

        public void Replace(
            ReadOnlySpan<char> oldValue,
            ReadOnlySpan<char> newValue,
            int startIndex,
            int count)
        {
            using (_PRF_Replace.Auto())
            {
                var currentLength = Length;

                if ((uint)startIndex > (uint)currentLength)
                {
                    ExceptionUtil.ThrowArgumentOutOfRangeException(nameof(startIndex));
                }

                if ((count < 0) || (startIndex > (currentLength - count)))
                {
                    ExceptionUtil.ThrowArgumentOutOfRangeException(nameof(count));
                }

                if (oldValue.Length == 0)
                {
                    throw new ArgumentException("oldValue.Length is 0", nameof(oldValue));
                }

                var readOnlySpan = AsSpan();
                var endIndex = startIndex + count;
                var matchCount = 0;

                for (var i = startIndex; i < endIndex; i += oldValue.Length)
                {
                    var span = readOnlySpan.Slice(i, endIndex - i);
                    var pos = span.IndexOf(oldValue, StringComparison.Ordinal);
                    if (pos == -1)
                    {
                        break;
                    }

                    i += pos;
                    matchCount++;
                }

                if (matchCount == 0)
                {
                    return;
                }

                var newBuffer = ArrayPool<char>.Shared.Rent(
                    Math.Max(DefaultBufferSize, Length + ((newValue.Length - oldValue.Length) * matchCount))
                );

                buffer.AsSpan(0, startIndex).CopyTo(newBuffer);
                var newBufferIndex = startIndex;

                for (var i = startIndex; i < endIndex; i += oldValue.Length)
                {
                    var span = readOnlySpan.Slice(i, endIndex - i);
                    var pos = span.IndexOf(oldValue, StringComparison.Ordinal);
                    if (pos == -1)
                    {
                        var remain = readOnlySpan.Slice(i);
                        remain.CopyTo(newBuffer.AsSpan(newBufferIndex));
                        newBufferIndex += remain.Length;
                        break;
                    }

                    readOnlySpan.Slice(i, pos).CopyTo(newBuffer.AsSpan(newBufferIndex));
                    newValue.CopyTo(newBuffer.AsSpan(newBufferIndex + pos));
                    newBufferIndex += pos + newValue.Length;
                    i += pos;
                }

                if (buffer.Length != ThreadStaticBufferSize)
                {
                    ArrayPool<char>.Shared.Return(buffer);
                }

                buffer = newBuffer;
                index = newBufferIndex;
            }
        }

        /// <summary>
        ///     Replaces the contents of a single position within the builder.
        /// </summary>
        /// <param name="newChar">The character to use at the position.</param>
        /// <param name="replaceIndex">The index to replace.</param>
        public void ReplaceAt(char newChar, int replaceIndex)
        {
            using (_PRF_ReplaceAt.Auto())
            {
                var currentLength = Length;
                if ((uint)replaceIndex > (uint)currentLength)
                {
                    ExceptionUtil.ThrowArgumentOutOfRangeException(nameof(replaceIndex));
                }

                buffer[replaceIndex] = newChar;
            }
        }

        // Output

        /// <summary>Copy inner buffer to the destination span.</summary>
        public bool TryCopyTo(Span<char> destination, out int charsWritten)
        {
            using (_PRF_TryCopyTo.Auto())
            {
                if (destination.Length < index)
                {
                    charsWritten = 0;
                    return false;
                }

                charsWritten = index;
                buffer.AsSpan(0, index).CopyTo(destination);
                return true;
            }
        }

        public void TryGrow(int sizeHint)
        {
            using (_PRF_TryGrow.Auto())
            {
                if (buffer.Length < (index + sizeHint))
                {
                    Grow(sizeHint);
                }
            }
        }

        private static TryFormat<T?> CreateNullableFormatter<T>()
            where T : struct
        {
            using (_PRF_CreateNullableFormatter.Auto())
            {
                return (T? x, Span<char> dest, out int written, ReadOnlySpan<char> format) =>
                {
                    if (x == null)
                    {
                        written = 0;
                        return true;
                    }

                    return FormatterCache<T>.TryFormatDelegate(x.Value, dest, out written, format);
                };
            }
        }

        private static void ThrowFormatException()
        {
            throw new FormatException(
                "Index (zero based) must be greater than or equal to zero and less than the size of the argument list."
            );
        }

        private static void ThrowNestedException()
        {
            throw new NestedStringBuilderCreationException(nameof(Utf16ValueStringBuilder));
        }

        private void AppendFormatInternal<T>(T arg, int width, ReadOnlySpan<char> format, string argName)
        {
            using (_PRF_AppendFormatInternal.Auto())
            {
                if (width <= 0) // leftJustify
                {
                    width *= -1;

                    if (!FormatterCache<T>.TryFormatDelegate(
                            arg,
                            buffer.AsSpan(index),
                            out var charsWritten,
                            format
                        ))
                    {
                        Grow(charsWritten);
                        if (!FormatterCache<T>.TryFormatDelegate(
                                arg,
                                buffer.AsSpan(index),
                                out charsWritten,
                                format
                            ))
                        {
                            ThrowArgumentException(argName);
                        }
                    }

                    index += charsWritten;

                    var padding = width - charsWritten;
                    if ((width > 0) && (padding > 0))
                    {
                        Append(' ', padding); // TODO Fill Method is too slow.
                    }
                }
                else // rightJustify
                {
                    if (typeof(T) == typeof(string))
                    {
                        var s = Unsafe.As<string>(arg);
                        var padding = width - s.Length;
                        if (padding > 0)
                        {
                            Append(' ', padding); // TODO Fill Method is too slow.
                        }

                        Append(s);
                    }
                    else
                    {
                        Span<char> s = stackalloc char[typeof(T).IsValueType ? Unsafe.SizeOf<T>() * 8 : 1024];

                        if (!FormatterCache<T>.TryFormatDelegate(arg, s, out var charsWritten, format))
                        {
                            s = stackalloc char[s.Length * 2];
                            if (!FormatterCache<T>.TryFormatDelegate(arg, s, out charsWritten, format))
                            {
                                ThrowArgumentException(argName);
                            }
                        }

                        var padding = width - charsWritten;
                        if (padding > 0)
                        {
                            Append(' ', padding); // TODO Fill Method is too slow.
                        }

                        Append(s.Slice(0, charsWritten));
                    }
                }
            }
        }

        private void ThrowArgumentException(string paramName)
        {
            throw new ArgumentException("Can't format argument.", paramName);
        }

        #region IBufferWriter<char> Members

        // IBufferWriter

        /// <summary>IBufferWriter.GetMemory.</summary>
        public Memory<char> GetMemory(int sizeHint)
        {
            using (_PRF_GetMemory.Auto())
            {
                if ((buffer.Length - index) < sizeHint)
                {
                    Grow(sizeHint);
                }

                return buffer.AsMemory(index);
            }
        }

        /// <summary>IBufferWriter.GetSpan.</summary>
        public Span<char> GetSpan(int sizeHint)
        {
            using (_PRF_GetSpan.Auto())
            {
                if ((buffer.Length - index) < sizeHint)
                {
                    Grow(sizeHint);
                }

                return buffer.AsSpan(index);
            }
        }

        /// <summary>IBufferWriter.Advance.</summary>
        public void Advance(int count)
        {
            using (_PRF_Advance.Auto())
            {
                index += count;
            }
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        ///     Return the inner buffer to pool.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose()
        {
            using (_PRF_Dispose.Auto())
            {
                if (buffer != null)
                {
                    if (buffer.Length != ThreadStaticBufferSize)
                    {
                        ArrayPool<char>.Shared.Return(buffer);
                    }

                    buffer = null;
                    index = 0;
                    if (disposeImmediately)
                    {
                        scratchBufferUsed = false;
                    }
                }
            }
        }

        #endregion

        #region IResettableBufferWriter<char> Members

        void IResettableBufferWriter<char>.Reset()
        {
            using (_PRF_Reset.Auto())
            {
                index = 0;
            }
        }

        #endregion

        #region Nested type: ExceptionUtil

        private static class ExceptionUtil
        {
            public static void ThrowArgumentOutOfRangeException(string paramName)
            {
                throw new ArgumentOutOfRangeException(paramName);
            }
        }

        #endregion

        #region Nested type: FormatterCache

        public static class FormatterCache<T>
        {
            static FormatterCache()
            {
                using (_PRF_FormatterCache.Auto())
                {
                    var formatter = (TryFormat<T>)CreateFormatter(typeof(T));
                    if (formatter == null)
                    {
                        if (typeof(T).IsEnum)
                        {
                            formatter = EnumUtil<T>.TryFormatUtf16;
                        }
                        else if (typeof(T) == typeof(string))
                        {
                            formatter = TryFormatString;
                        }
                        else
                        {
                            formatter = TryFormatDefault;
                        }
                    }

                    TryFormatDelegate = formatter;
                }
            }

            #region Static Fields and Autoproperties

            private static readonly ProfilerMarker _PRF_FormatterCache =
                new ProfilerMarker(_PRF_PFX + nameof(FormatterCache<T>));

            public static TryFormat<T> TryFormatDelegate;

            #endregion

            private static bool TryFormatDefault(
                T value,
                Span<char> dest,
                out int written,
                ReadOnlySpan<char> format)
            {
                using (_PRF_TryFormatDefault.Auto())
                {
                    if (value == null)
                    {
                        written = 0;
                        return true;
                    }

                    var s = value is IFormattable formattable && (format.Length != 0)
                        ? formattable.ToString(format.ToString(), null)
                        : value.ToString();

                    // also use this length when result is false.
                    written = s.Length;
                    return s.AsSpan().TryCopyTo(dest);
                }
            }

            private static bool TryFormatString(
                T value,
                Span<char> dest,
                out int written,
                ReadOnlySpan<char> format)
            {
                using (_PRF_TryFormatString.Auto())
                {
                    var s = value as string;

                    if (s == null)
                    {
                        written = 0;
                        return true;
                    }

                    // also use this length when result is false.
                    written = s.Length;
                    return s.AsSpan().TryCopyTo(dest);
                }
            }

            #region Profiling

            private const string _PRF_PFX = nameof(FormatterCache<T>) + ".";

            private static readonly ProfilerMarker _PRF_TryFormatString =
                new ProfilerMarker(_PRF_PFX + nameof(TryFormatString));

            private static readonly ProfilerMarker _PRF_TryFormatDefault =
                new ProfilerMarker(_PRF_PFX + nameof(TryFormatDefault));

            #endregion
        }

        #endregion

        #region Profiling

        private const string _PRF_PFX = nameof(Utf16ValueStringBuilder) + ".";

        private static readonly ProfilerMarker _PRF_EnableNullableFormat =
            new ProfilerMarker(_PRF_PFX + nameof(EnableNullableFormat));

        private static readonly ProfilerMarker _PRF_RegisterTryFormat =
            new ProfilerMarker(_PRF_PFX + nameof(RegisterTryFormat));

        private static readonly ProfilerMarker
            _PRF_ToString = new ProfilerMarker(_PRF_PFX + nameof(ToString));

        private static readonly ProfilerMarker _PRF_AppendLine =
            new ProfilerMarker(_PRF_PFX + nameof(AppendLine));

        private static readonly ProfilerMarker _PRF_AsArraySegment =
            new ProfilerMarker(_PRF_PFX + nameof(AsArraySegment));

        private static readonly ProfilerMarker
            _PRF_AsMemory = new ProfilerMarker(_PRF_PFX + nameof(AsMemory));

        private static readonly ProfilerMarker _PRF_AsSpan = new ProfilerMarker(_PRF_PFX + nameof(AsSpan));
        private static readonly ProfilerMarker _PRF_Clear = new ProfilerMarker(_PRF_PFX + nameof(Clear));
        private static readonly ProfilerMarker _PRF_Grow = new ProfilerMarker(_PRF_PFX + nameof(Grow));
        private static readonly ProfilerMarker _PRF_Insert = new ProfilerMarker(_PRF_PFX + nameof(Insert));
        private static readonly ProfilerMarker _PRF_Remove = new ProfilerMarker(_PRF_PFX + nameof(Remove));
        private static readonly ProfilerMarker _PRF_Replace = new ProfilerMarker(_PRF_PFX + nameof(Replace));

        private static readonly ProfilerMarker _PRF_ReplaceAt =
            new ProfilerMarker(_PRF_PFX + nameof(ReplaceAt));

        private static readonly ProfilerMarker _PRF_TryCopyTo =
            new ProfilerMarker(_PRF_PFX + nameof(TryCopyTo));

        private static readonly ProfilerMarker _PRF_TryGrow = new ProfilerMarker(_PRF_PFX + nameof(TryGrow));

        private static readonly ProfilerMarker _PRF_CreateNullableFormatter =
            new ProfilerMarker(_PRF_PFX + nameof(CreateNullableFormatter));

        private static readonly ProfilerMarker _PRF_AppendFormatInternal =
            new ProfilerMarker(_PRF_PFX + nameof(AppendFormatInternal));

        private static readonly ProfilerMarker _PRF_GetMemory =
            new ProfilerMarker(_PRF_PFX + nameof(GetMemory));

        private static readonly ProfilerMarker _PRF_GetSpan = new ProfilerMarker(_PRF_PFX + nameof(GetSpan));
        private static readonly ProfilerMarker _PRF_Advance = new ProfilerMarker(_PRF_PFX + nameof(Advance));

        private static readonly ProfilerMarker _PRF_Dispose = new ProfilerMarker(_PRF_PFX + nameof(Dispose));

        private static readonly ProfilerMarker _PRF_Reset =
            new ProfilerMarker(_PRF_PFX + nameof(IResettableBufferWriter<char>.Reset));

        private static readonly ProfilerMarker _PRF_Utf16ValueStringBuilder =
            new ProfilerMarker(_PRF_PFX + nameof(Utf16ValueStringBuilder));

        private static readonly ProfilerMarker _PRF_Append = new ProfilerMarker(_PRF_PFX + nameof(Append));

        #endregion
    }
}
