using System;

namespace Appalachia.Utility.Strings
{
    // Currently, this class is internals.
    internal class NestedStringBuilderCreationException : InvalidOperationException
    {
        protected internal NestedStringBuilderCreationException(string typeName, string extraMessage = "") :
            base(
                ZString.Format(
                    "A nested call with `notNested: true`, or Either You forgot to call {0}.Dispose() of  in the past.{1}",
                    typeName,
                    extraMessage
                )
            )
        {
        }

        protected internal NestedStringBuilderCreationException(string message, Exception innerException) :
            base(message, innerException)
        {
        }
    }
}
