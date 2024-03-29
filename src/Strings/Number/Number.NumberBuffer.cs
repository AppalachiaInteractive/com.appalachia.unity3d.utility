﻿using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace Appalachia.Utility.Strings.Number
{
    internal static partial class Number
    {
        internal enum NumberBufferKind : byte
        {
            Unknown = 0,
            Integer = 1,
            Decimal = 2,
            FloatingPoint = 3,
        }

        #region Constants and Static Readonly

        // We need 1 additional byte, per length, for the terminating null
        internal const int
            DecimalNumberBufferLength = 29 + 1 + 1; // 29 for the longest input + 1 for rounding

        internal const int
            DoubleNumberBufferLength =
                767 + 1 + 1; // 767 for the longest input + 1 for rounding: 4.9406564584124654E-324

        internal const int Int32NumberBufferLength = 10 + 1; // 10 for the longest input: 2,147,483,647

        internal const int
            Int64NumberBufferLength = 19 + 1; // 19 for the longest input: 9,223,372,036,854,775,807

        internal const int
            SingleNumberBufferLength =
                112 + 1 + 1; // 112 for the longest input + 1 for rounding: 1.40129846E-45

        internal const int UInt32NumberBufferLength = 10 + 1; // 10 for the longest input: 4,294,967,295

        internal const int
            UInt64NumberBufferLength = 20 + 1; // 20 for the longest input: 18,446,744,073,709,551,615

        #endregion

        #region Nested type: NumberBuffer

        internal unsafe ref struct NumberBuffer
        {
            public NumberBuffer(NumberBufferKind kind, byte* digits, int digitsLength)
            {
                Debug.Assert(digits != null);
                Debug.Assert(digitsLength > 0);

                DigitsCount = 0;
                Scale = 0;
                IsNegative = false;
                HasNonZeroTail = false;
                Kind = kind;
                Digits = new Span<byte>(digits, digitsLength);

#if DEBUG
                Digits.Fill(0xCC);
#endif

                Digits[0] = (byte)'\0';
                CheckConsistency();
            }

            #region Fields and Autoproperties

            public bool HasNonZeroTail;
            public bool IsNegative;
            public int DigitsCount;
            public int Scale;
            public NumberBufferKind Kind;
            public Span<byte> Digits;

            #endregion

            //
            // Code coverage note: This only exists so that Number displays nicely in the VS watch window. So yes, I know it works.
            //
            /// <inheritdoc />
            public override string ToString()
            {
                var sb = new StringBuilder();

                sb.Append('[');
                sb.Append('"');

                for (var i = 0; i < Digits.Length; i++)
                {
                    var digit = Digits[i];

                    if (digit == 0)
                    {
                        break;
                    }

                    sb.Append((char)digit);
                }

                sb.Append('"');
                sb.Append(", Length = ").Append(DigitsCount);
                sb.Append(", Scale = ").Append(Scale);
                sb.Append(", IsNegative = ").Append(IsNegative);
                sb.Append(", HasNonZeroTail = ").Append(HasNonZeroTail);
                sb.Append(", Kind = ").Append(Kind);
                sb.Append(']');

                return sb.ToString();
            }

            [Conditional("DEBUG")]
            public void CheckConsistency()
            {
#if DEBUG
                Debug.Assert(
                    (Kind == NumberBufferKind.Integer) ||
                    (Kind == NumberBufferKind.Decimal) ||
                    (Kind == NumberBufferKind.FloatingPoint)
                );
                Debug.Assert(Digits[0] != '0', "Leading zeros should never be stored in a Number");

                int numDigits;
                for (numDigits = 0; numDigits < Digits.Length; numDigits++)
                {
                    var digit = Digits[numDigits];

                    if (digit == 0)
                    {
                        break;
                    }

                    Debug.Assert((digit >= '0') && (digit <= '9'), "Unexpected character found in Number");
                }

                Debug.Assert(
                    numDigits == DigitsCount,
                    "Null terminator found in unexpected location in Number"
                );
                Debug.Assert(numDigits < Digits.Length, "Null terminator not found in Number");
#endif // DEBUG
            }

            public byte* GetDigitsPointer()
            {
                // This is safe to do since we are a ref struct
                return (byte*)Unsafe.AsPointer(ref Digits[0]);
            }
        }

        #endregion
    }
}
