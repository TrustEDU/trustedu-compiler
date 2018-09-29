using System;

namespace TrustEDU.Compiler.Java.Base.Java.Cecil.Loader
{
    [Flags]
    internal enum ByteCodeFlags : byte
    {
        None = 0,
        FixedArg = 1,
        CannotThrow = 2
    }
}
