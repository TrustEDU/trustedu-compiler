using System;

namespace TrustEDU.Compiler.Java.Base.Java.Cecil.Loader
{
    [Flags]
    public enum ClassFileParseOptions
    {
        None = 0,
        LocalVariableTable = 1,
        LineNumberTable = 2,
        RelaxedClassNameValidation = 4,
    }
}
