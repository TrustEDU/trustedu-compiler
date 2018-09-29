using System;
using System.Collections.Generic;
using TrustEDU.Compiler.Dotnet.Base;

namespace TrustEDU.Compiler.Dotnet.MSIL
{
    public class ILMethod
    {
        public ILMethod(ILType type, Mono.Cecil.MethodDefinition method)
        {
            this.Method = method;
            if (method != null)
            {
                ReturnType = method.ReturnType.FullName;
                if (method.HasParameters)
                {
                    HasParam = true;
                    foreach (var p in method.Parameters)
                    {
                        string paramtype = p.ParameterType.FullName;
                        try
                        {
                            var _type = p.ParameterType.Resolve();
                            foreach (var i in _type.Interfaces)
                            {
                                if (i.InterfaceType.Name == "IApiInterface")
                                {
                                    paramtype = "IInteropInterface";
                                }
                            }
                        }
                        catch (Exception)
                        {

                        }
                        this.ParamTypes.Add(new TParam(p.Name, paramtype));
                    }
                }
                if (method.HasBody)
                {
                    var bodyNative = method.Body;
                    if (bodyNative.HasVariables)
                    {
                        foreach (var v in bodyNative.Variables)
                        {
                            var indexname = v.VariableType.Name + ":" + v.Index;
                            this.BodyVariables.Add(new TParam(indexname, v.VariableType.FullName));
                        }
                    }
                    for (int i = 0; i < bodyNative.Instructions.Count; i++)
                    {
                        var code = bodyNative.Instructions[i];
                        OpCode c = new OpCode
                        {
                            code = (CodeEx)(int)code.OpCode.Code,
                            addr = code.Offset
                        };

                        var sp = method.DebugInformation.GetSequencePoint(code);
                        if (sp != null)
                        {
                            c.debugcode = sp.Document.Url;
                            c.debugline = sp.StartLine;
                        }
                        c.InitToken(code.Operand);
                        this.BodyCodes.Add(c.addr, c);
                    }
                }
            }
        }

        public string ReturnType;
        public List<TParam> ParamTypes = new List<TParam>();
        public bool HasParam = false;
        public Mono.Cecil.MethodDefinition Method;
        public List<TParam> BodyVariables = new List<TParam>();
        public SortedDictionary<int, OpCode> BodyCodes = new SortedDictionary<int, OpCode>();
        public string Fail = null;
        public int GetNextCodeAddr(int srcaddr)
        {
            bool bskip = false;
            foreach (var key in this.BodyCodes.Keys)
            {
                if (key == srcaddr)
                {
                    bskip = true;
                    continue;
                }
                if (bskip)
                {
                    return key;
                }

            }
            return -1;
        }
    }
}
