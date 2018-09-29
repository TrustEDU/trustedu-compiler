using System;

namespace TrustEDU.Compiler.Dotnet.MSIL
{
    public class OpCode
    {
        public override string ToString()
        {
            var info = "IL_" + addr.ToString("X04") + " " + code + " ";
            if (this.tokenValueType == TokenValueType.Method)
                info += tokenMethod;
            if (this.tokenValueType == TokenValueType.String)
                info += tokenStr;

            if (debugline >= 0)
            {
                info += "(" + debugline + ")";
            }
            return info;
        }
        public enum TokenValueType
        {
            Nothing,
            Addr,//地址
            AddrArray,
            String,
            Type,
            Field,
            Method,
            I32,
            I64,
            OTher,
        }
        public TokenValueType tokenValueType = TokenValueType.Nothing;
        public int addr;
        public CodeEx code;
        public int debugline = -1;
        public string debugcode;
        public object tokenUnknown;
        public int tokenAddr_Index;
        //public int tokenAddr;
        public int[] tokenAddr_Switch;
        public string tokenType;
        public string tokenField;
        public string tokenMethod;
        public int tokenI32;
        public Int64 tokenI64;
        public float tokenR32;
        public double tokenR64;
        public string tokenStr;
        public void InitToken(object _p)
        {
            this.tokenUnknown = _p;
            switch (code)
            {
                case CodeEx.Leave:
                case CodeEx.Leave_S:
                case CodeEx.Br:
                case CodeEx.Br_S:
                case CodeEx.Brtrue:
                case CodeEx.Brtrue_S:
                case CodeEx.Brfalse:
                case CodeEx.Brfalse_S:
                //比较流程控制
                case CodeEx.Beq:
                case CodeEx.Beq_S:
                case CodeEx.Bne_Un:
                case CodeEx.Bne_Un_S:
                case CodeEx.Bge:
                case CodeEx.Bge_S:
                case CodeEx.Bge_Un:
                case CodeEx.Bge_Un_S:
                case CodeEx.Bgt:
                case CodeEx.Bgt_S:
                case CodeEx.Bgt_Un:
                case CodeEx.Bgt_Un_S:
                case CodeEx.Ble:
                case CodeEx.Ble_S:
                case CodeEx.Ble_Un:
                case CodeEx.Ble_Un_S:
                case CodeEx.Blt:
                case CodeEx.Blt_S:
                case CodeEx.Blt_Un:
                case CodeEx.Blt_Un_S:
                    //this.tokenAddr = ((Mono.Cecil.Cil.Instruction)_p).Offset;
                    this.tokenAddr_Index = ((Mono.Cecil.Cil.Instruction)_p).Offset;
                    this.tokenValueType = TokenValueType.Addr;
                    break;
                case CodeEx.Isinst:
                case CodeEx.Constrained:
                case CodeEx.Box:
                case CodeEx.Initobj:
                case CodeEx.Castclass:
                case CodeEx.Newarr:
                    this.tokenType = (_p as Mono.Cecil.TypeReference).FullName;
                    this.tokenValueType = TokenValueType.Type;
                    this.tokenUnknown = _p;
                    break;
                case CodeEx.Ldfld:
                case CodeEx.Ldflda:
                case CodeEx.Ldsfld:
                case CodeEx.Ldsflda:
                case CodeEx.Stfld:
                case CodeEx.Stsfld:
                    this.tokenField = (_p as Mono.Cecil.FieldReference).FullName;
                    this.tokenUnknown = _p;
                    this.tokenValueType = TokenValueType.Field;
                    break;
                case CodeEx.Call:
                case CodeEx.Callvirt:
                case CodeEx.Newobj:
                case CodeEx.Ldftn:
                case CodeEx.Ldvirtftn:

                    this.tokenMethod = (_p as Mono.Cecil.MethodReference).FullName;
                    this.tokenUnknown = _p;
                    this.tokenValueType = TokenValueType.Method;
                    break;
                case CodeEx.Ldc_I4:
                    this.tokenI32 = (int)_p;
                    this.tokenValueType = TokenValueType.I32;
                    break;
                case CodeEx.Ldc_I4_S:
                    this.tokenI32 = (int)Convert.ToDecimal(_p);
                    this.tokenValueType = TokenValueType.I32;
                    break;
                case CodeEx.Ldc_I4_M1:
                    this.tokenI32 = -1;
                    this.tokenValueType = TokenValueType.I32;
                    break;
                case CodeEx.Ldc_I4_0:
                    this.tokenI32 = 0;
                    this.tokenValueType = TokenValueType.I32;
                    break;
                case CodeEx.Ldc_I4_1:
                    this.tokenI32 = 1;
                    this.tokenValueType = TokenValueType.I32;
                    break;
                case CodeEx.Ldc_I4_2:
                    this.tokenI32 = 2;
                    this.tokenValueType = TokenValueType.I32;
                    break;
                case CodeEx.Ldc_I4_3:
                    this.tokenI32 = 3;
                    this.tokenValueType = TokenValueType.I32;
                    break;
                case CodeEx.Ldc_I4_4:
                    this.tokenI32 = 4;
                    this.tokenValueType = TokenValueType.I32;
                    break;
                case CodeEx.Ldc_I4_5:
                    this.tokenI32 = 5;
                    this.tokenValueType = TokenValueType.I32;
                    break;
                case CodeEx.Ldc_I4_6:
                    this.tokenI32 = 6;
                    this.tokenValueType = TokenValueType.I32;
                    break;
                case CodeEx.Ldc_I4_7:
                    this.tokenI32 = 7;
                    this.tokenValueType = TokenValueType.I32;
                    break;
                case CodeEx.Ldc_I4_8:
                    this.tokenI32 = 8;
                    this.tokenValueType = TokenValueType.I32;
                    break;
                case CodeEx.Ldc_I8:
                    this.tokenI64 = (Int64)_p;
                    this.tokenValueType = TokenValueType.I64;
                    break;
                case CodeEx.Ldc_R4:
                    this.tokenR32 = (float)_p;
                    this.tokenValueType = TokenValueType.OTher;
                    break;
                case CodeEx.Ldc_R8:
                    this.tokenR64 = (double)_p;
                    this.tokenValueType = TokenValueType.OTher;
                    break;

                case CodeEx.Ldstr:
                    this.tokenStr = _p as string;
                    this.tokenValueType = TokenValueType.String;
                    break;

                case CodeEx.Stloc_0:

                    break;
                case CodeEx.Ldloca:
                case CodeEx.Ldloca_S:
                case CodeEx.Ldloc_S:
                case CodeEx.Stloc_S:
                    this.tokenI32 = ((Mono.Cecil.Cil.VariableDefinition)_p).Index;
                    this.tokenValueType = TokenValueType.I32;

                    //this.tokenUnknown = _p;
                    break;
                case CodeEx.Ldloc:
                case CodeEx.Stloc:
                    this.tokenI32 = (int)_p;
                    this.tokenValueType = TokenValueType.I32;

                    break;
                case CodeEx.Ldloc_0:
                    this.tokenI32 = 0;
                    this.tokenValueType = TokenValueType.I32;

                    break;
                case CodeEx.Ldloc_1:
                    this.tokenI32 = 1;
                    this.tokenValueType = TokenValueType.I32;

                    break;
                case CodeEx.Ldloc_2:
                    this.tokenI32 = 2;
                    this.tokenValueType = TokenValueType.I32;

                    break;
                case CodeEx.Ldloc_3:
                    this.tokenI32 = 3;
                    this.tokenValueType = TokenValueType.I32;

                    break;

                case CodeEx.Ldarga:
                case CodeEx.Ldarga_S:
                case CodeEx.Starg:
                case CodeEx.Starg_S:
                    this.tokenI32 = (_p as Mono.Cecil.ParameterDefinition).Index;
                    this.tokenValueType = TokenValueType.I32;

                    break;
                case CodeEx.Switch:
                    {
                        Mono.Cecil.Cil.Instruction[] e = _p as Mono.Cecil.Cil.Instruction[];
                        tokenAddr_Switch = new int[e.Length];
                        for (int i = 0; i < e.Length; i++)
                        {
                            tokenAddr_Switch[i] = (e[i].Offset);
                        }
                        this.tokenValueType = TokenValueType.AddrArray;

                    }
                    break;
                case CodeEx.Ldarg:
                    this.tokenI32 = (int)_p;
                    this.tokenValueType = TokenValueType.I32;

                    break;
                case CodeEx.Ldarg_S:
                    this.tokenI32 = (_p as Mono.Cecil.ParameterReference).Index;
                    this.tokenValueType = TokenValueType.I32;

                    break;
                case CodeEx.Volatile:
                case CodeEx.Ldind_I1:
                case CodeEx.Ldind_U1:
                case CodeEx.Ldind_I2:
                case CodeEx.Ldind_U2:
                case CodeEx.Ldind_I4:
                case CodeEx.Ldind_U4:
                case CodeEx.Ldind_I8:
                case CodeEx.Ldind_I:
                case CodeEx.Ldind_R4:
                case CodeEx.Ldind_R8:
                case CodeEx.Ldind_Ref:
                    this.tokenValueType = TokenValueType.Nothing;
                    break;

                case CodeEx.Ldtoken:
                    var def = (_p as Mono.Cecil.FieldDefinition);
                    this.tokenUnknown = def.InitialValue;
                    this.tokenValueType = TokenValueType.Nothing;
                    break;
                default:
                    this.tokenUnknown = _p;
                    this.tokenValueType = TokenValueType.Nothing;
                    break;
            }
        }
    }
}
