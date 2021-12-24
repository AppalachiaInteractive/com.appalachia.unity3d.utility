using System;
using System.Buffers;
using System.Buffers.Text;

namespace Appalachia.Utility.Strings
{
    public partial struct Utf8ValueStringBuilder
    {
        private static object CreateFormatter(Type type)
        {
            if (type == typeof(byte))
            {
                return new TryFormat<byte>(
                    (byte x, Span<byte> dest, out int written, StandardFormat format) =>
                        Utf8Formatter.TryFormat(x, dest, out written, format)
                );
            }

            if (type == typeof(DateTime))
            {
                return new TryFormat<DateTime>(
                    (DateTime x, Span<byte> dest, out int written, StandardFormat format) =>
                        Utf8Formatter.TryFormat(x, dest, out written, format)
                );
            }

            if (type == typeof(DateTimeOffset))
            {
                return new TryFormat<DateTimeOffset>(
                    (DateTimeOffset x, Span<byte> dest, out int written, StandardFormat format) =>
                        Utf8Formatter.TryFormat(x, dest, out written, format)
                );
            }

            if (type == typeof(decimal))
            {
                return new TryFormat<decimal>(
                    (decimal x, Span<byte> dest, out int written, StandardFormat format) =>
                        Utf8Formatter.TryFormat(x, dest, out written, format)
                );
            }

            if (type == typeof(double))
            {
                return new TryFormat<double>(
                    (double x, Span<byte> dest, out int written, StandardFormat format) =>
                        Utf8Formatter.TryFormat(x, dest, out written, format)
                );
            }

            if (type == typeof(short))
            {
                return new TryFormat<short>(
                    (short x, Span<byte> dest, out int written, StandardFormat format) =>
                        Utf8Formatter.TryFormat(x, dest, out written, format)
                );
            }

            if (type == typeof(int))
            {
                return new TryFormat<int>(
                    (int x, Span<byte> dest, out int written, StandardFormat format) =>
                        Utf8Formatter.TryFormat(x, dest, out written, format)
                );
            }

            if (type == typeof(long))
            {
                return new TryFormat<long>(
                    (long x, Span<byte> dest, out int written, StandardFormat format) =>
                        Utf8Formatter.TryFormat(x, dest, out written, format)
                );
            }

            if (type == typeof(sbyte))
            {
                return new TryFormat<sbyte>(
                    (sbyte x, Span<byte> dest, out int written, StandardFormat format) =>
                        Utf8Formatter.TryFormat(x, dest, out written, format)
                );
            }

            if (type == typeof(float))
            {
                return new TryFormat<float>(
                    (float x, Span<byte> dest, out int written, StandardFormat format) =>
                        Utf8Formatter.TryFormat(x, dest, out written, format)
                );
            }

            if (type == typeof(TimeSpan))
            {
                return new TryFormat<TimeSpan>(
                    (TimeSpan x, Span<byte> dest, out int written, StandardFormat format) =>
                        Utf8Formatter.TryFormat(x, dest, out written, format)
                );
            }

            if (type == typeof(ushort))
            {
                return new TryFormat<ushort>(
                    (ushort x, Span<byte> dest, out int written, StandardFormat format) =>
                        Utf8Formatter.TryFormat(x, dest, out written, format)
                );
            }

            if (type == typeof(uint))
            {
                return new TryFormat<uint>(
                    (uint x, Span<byte> dest, out int written, StandardFormat format) =>
                        Utf8Formatter.TryFormat(x, dest, out written, format)
                );
            }

            if (type == typeof(ulong))
            {
                return new TryFormat<ulong>(
                    (ulong x, Span<byte> dest, out int written, StandardFormat format) =>
                        Utf8Formatter.TryFormat(x, dest, out written, format)
                );
            }

            if (type == typeof(Guid))
            {
                return new TryFormat<Guid>(
                    (Guid x, Span<byte> dest, out int written, StandardFormat format) =>
                        Utf8Formatter.TryFormat(x, dest, out written, format)
                );
            }

            if (type == typeof(bool))
            {
                return new TryFormat<bool>(
                    (bool x, Span<byte> dest, out int written, StandardFormat format) =>
                        Utf8Formatter.TryFormat(x, dest, out written, format)
                );
            }

            if (type == typeof(byte?))
            {
                return CreateNullableFormatter<byte>();
            }

            if (type == typeof(DateTime?))
            {
                return CreateNullableFormatter<DateTime>();
            }

            if (type == typeof(DateTimeOffset?))
            {
                return CreateNullableFormatter<DateTimeOffset>();
            }

            if (type == typeof(decimal?))
            {
                return CreateNullableFormatter<decimal>();
            }

            if (type == typeof(double?))
            {
                return CreateNullableFormatter<double>();
            }

            if (type == typeof(short?))
            {
                return CreateNullableFormatter<short>();
            }

            if (type == typeof(int?))
            {
                return CreateNullableFormatter<int>();
            }

            if (type == typeof(long?))
            {
                return CreateNullableFormatter<long>();
            }

            if (type == typeof(sbyte?))
            {
                return CreateNullableFormatter<sbyte>();
            }

            if (type == typeof(float?))
            {
                return CreateNullableFormatter<float>();
            }

            if (type == typeof(TimeSpan?))
            {
                return CreateNullableFormatter<TimeSpan>();
            }

            if (type == typeof(ushort?))
            {
                return CreateNullableFormatter<ushort>();
            }

            if (type == typeof(uint?))
            {
                return CreateNullableFormatter<uint>();
            }

            if (type == typeof(ulong?))
            {
                return CreateNullableFormatter<ulong>();
            }

            if (type == typeof(Guid?))
            {
                return CreateNullableFormatter<Guid>();
            }

            if (type == typeof(bool?))
            {
                return CreateNullableFormatter<bool>();
            }

            if (type == typeof(IntPtr))
            {
                // ignore format
                return new TryFormat<IntPtr>(
                    (IntPtr x, Span<byte> dest, out int written, StandardFormat _) => IntPtr.Size == 4
                        ? Utf8Formatter.TryFormat(x.ToInt32(), dest, out written)
                        : Utf8Formatter.TryFormat(x.ToInt64(), dest, out written)
                );
            }

            if (type == typeof(UIntPtr))
            {
                // ignore format
                return new TryFormat<UIntPtr>(
                    (UIntPtr x, Span<byte> dest, out int written, StandardFormat _) => UIntPtr.Size == 4
                        ? Utf8Formatter.TryFormat(x.ToUInt32(), dest, out written)
                        : Utf8Formatter.TryFormat(x.ToUInt64(), dest, out written)
                );
            }

            return null;
        }
    }
}
