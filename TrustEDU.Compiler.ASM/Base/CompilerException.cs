using System;

namespace TrustEDU.Compiler.ASM.Base
{
    public class CompilerException : Exception
    {
        public CompilerException(uint lineNumber, string message)
            : base($"ERROR: {message} in line {lineNumber}.")
        {
        }
    }
}
