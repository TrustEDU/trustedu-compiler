﻿namespace TrustEDU.Compiler.Java.Base.Java.Cecil.Loader
{
    public enum NormalizedByteCode : byte
    {
        __nop = 0,
        __aconst_null = 1,
        __lconst_0 = 9,
        __lconst_1 = 10,
        __fconst_0 = 11,
        __fconst_1 = 12,
        __fconst_2 = 13,
        __dconst_0 = 14,
        __dconst_1 = 15,
        __ldc = 18,
        __iload = 21,
        __lload = 22,
        __fload = 23,
        __dload = 24,
        __aload = 25,
        __iaload = 46,
        __laload = 47,
        __faload = 48,
        __daload = 49,
        __aaload = 50,
        __baload = 51,
        __caload = 52,
        __saload = 53,
        __istore = 54,
        __lstore = 55,
        __fstore = 56,
        __dstore = 57,
        __astore = 58,
        __iastore = 79,
        __lastore = 80,
        __fastore = 81,
        __dastore = 82,
        __aastore = 83,
        __bastore = 84,
        __castore = 85,
        __sastore = 86,
        __pop = 87,
        __pop2 = 88,
        __dup = 89,
        __dup_x1 = 90,
        __dup_x2 = 91,
        __dup2 = 92,
        __dup2_x1 = 93,
        __dup2_x2 = 94,
        __swap = 95,
        __iadd = 96,
        __ladd = 97,
        __fadd = 98,
        __dadd = 99,
        __isub = 100,
        __lsub = 101,
        __fsub = 102,
        __dsub = 103,
        __imul = 104,
        __lmul = 105,
        __fmul = 106,
        __dmul = 107,
        __idiv = 108,
        __ldiv = 109,
        __fdiv = 110,
        __ddiv = 111,
        __irem = 112,
        __lrem = 113,
        __frem = 114,
        __drem = 115,
        __ineg = 116,
        __lneg = 117,
        __fneg = 118,
        __dneg = 119,
        __ishl = 120,
        __lshl = 121,
        __ishr = 122,
        __lshr = 123,
        __iushr = 124,
        __lushr = 125,
        __iand = 126,
        __land = 127,
        __ior = 128,
        __lor = 129,
        __ixor = 130,
        __lxor = 131,
        __iinc = 132,
        __i2l = 133,
        __i2f = 134,
        __i2d = 135,
        __l2i = 136,
        __l2f = 137,
        __l2d = 138,
        __f2i = 139,
        __f2l = 140,
        __f2d = 141,
        __d2i = 142,
        __d2l = 143,
        __d2f = 144,
        __i2b = 145,
        __i2c = 146,
        __i2s = 147,
        __lcmp = 148,
        __fcmpl = 149,
        __fcmpg = 150,
        __dcmpl = 151,
        __dcmpg = 152,
        __ifeq = 153,
        __ifne = 154,
        __iflt = 155,
        __ifge = 156,
        __ifgt = 157,
        __ifle = 158,
        __if_icmpeq = 159,
        __if_icmpne = 160,
        __if_icmplt = 161,
        __if_icmpge = 162,
        __if_icmpgt = 163,
        __if_icmple = 164,
        __if_acmpeq = 165,
        __if_acmpne = 166,
        __goto = 167,
        __jsr = 168,
        __ret = 169,
        __tableswitch = 170,
        __lookupswitch = 171,
        __ireturn = 172,
        __lreturn = 173,
        __freturn = 174,
        __dreturn = 175,
        __areturn = 176,
        __return = 177,
        __getstatic = 178,
        __putstatic = 179,
        __getfield = 180,
        __putfield = 181,
        __invokevirtual = 182,
        __invokespecial = 183,
        __invokestatic = 184,
        __invokeinterface = 185,
        __invokedynamic = 186,
        __new = 187,
        __newarray = 188,
        __anewarray = 189,
        __arraylength = 190,
        __athrow = 191,
        __checkcast = 192,
        __instanceof = 193,
        __monitorenter = 194,
        __monitorexit = 195,
        __multianewarray = 197,
        __ifnull = 198,
        __ifnonnull = 199,
        // This is where the pseudo-bytecodes start
        __ldc_nothrow = 239,
        __methodhandle_invoke = 240,
        __methodhandle_invokeexact = 241,
        __goto_finally = 242,
        __intrinsic_gettype = 243,
        __athrow_no_unmap = 244,
        __dynamic_getstatic = 245,
        __dynamic_putstatic = 246,
        __dynamic_getfield = 247,
        __dynamic_putfield = 248,
        __dynamic_invokeinterface = 249,
        __dynamic_invokestatic = 250,
        __dynamic_invokevirtual = 251,
        __dynamic_invokespecial = 252,
        __clone_array = 253,
        __static_error = 254,   // not a real instruction, this signals an instruction that is compiled as an exception
        __iconst = 255
    }
}
