using System;
using System.Collections.Generic;
using TrustEDU.Compiler.Dotnet.Base;

namespace TrustEDU.Compiler.Dotnet.MSIL
{
    /// <summary>
    /// Parsing IL Class Library using Mono.Cecil
    /// </summary>
    public class ILModule
    {
        public Mono.Cecil.ModuleDefinition module = null;
        public List<string> moduleref = new List<string>();
        public Dictionary<string, ILType> mapType = new Dictionary<string, ILType>();
        public ILModule()
        {

        }
        public void LoadModule(System.IO.Stream dllStream, System.IO.Stream pdbStream)
        {
            this.module = Mono.Cecil.ModuleDefinition.ReadModule(dllStream);
            //#if WITHPDB
            if (pdbStream != null)
            {
                var debugInfoLoader = new Mono.Cecil.Pdb.PdbReaderProvider();

                module.ReadSymbols(debugInfoLoader.GetSymbolReader(module, pdbStream));
            }
            //#endif
            if (module.HasAssemblyReferences)
            {
                foreach (var ar in module.AssemblyReferences)
                {
                    if (moduleref.Contains(ar.Name) == false)
                        moduleref.Add(ar.Name);
                    if (moduleref.Contains(ar.FullName) == false)
                        moduleref.Add(ar.FullName);
                }
            }
            //mapModule[module.Name] = module;
            if (module.HasTypes)
            {
                foreach (var t in module.Types)
                {
                    if (t.FullName.Contains(".My."))
                        continue;

                    mapType[t.FullName] = new ILType(this, t);
                    if (t.HasNestedTypes)
                    {
                        foreach (var nt in t.NestedTypes)
                        {
                            mapType[nt.FullName] = new ILType(this, nt);

                        }
                    }

                }
            }
        }

    }
    public class ILType
    {
        Mono.Cecil.TypeDefinition type;
        public Dictionary<string, ILField> fields = new Dictionary<string, ILField>();
        public Dictionary<string, ILMethod> methods = new Dictionary<string, ILMethod>();
        public ILType(ILModule module, Mono.Cecil.TypeDefinition type)
        {
            this.type = type;
            foreach (Mono.Cecil.FieldDefinition f in type.Fields)
            {
                this.fields.Add(f.Name, new ILField(this, f));
            }
            foreach (Mono.Cecil.MethodDefinition m in type.Methods)
            {
                if (m.IsStatic == false)
                {
                    var method = new ILMethod(this, null);
                    method.Fail = "Cannot export static functions";
                    methods[m.FullName] = method;
                }
                else
                {
                    var method = new ILMethod(this, m);
                    if (methods.ContainsKey(m.FullName))
                    {
                        throw new Exception("already have a func named:" + type.FullName + "::" + m.Name);
                    }
                    methods[m.FullName] = method;
                }
            }
        }

    }

    public class ILField
    {
        public ILField(ILType type, Mono.Cecil.FieldDefinition field)
        {
            this.type = field.FieldType.FullName;
            this.name = field.Name;
            this.displayName = this.name;
            this.field = field;
            foreach (var ev in field.DeclaringType.Events)
            {
                if (ev.Name == field.Name && ev.EventType.FullName == field.FieldType.FullName)
                {
                    this.isEvent = true;
                    Mono.Collections.Generic.Collection<Mono.Cecil.CustomAttribute> ca = ev.CustomAttributes;
                    foreach (var attr in ca)
                    {
                        if (attr.AttributeType.Name == "DisplayNameAttribute")
                        {
                            this.displayName = (string)attr.ConstructorArguments[0].Value;
                        }
                    }
                    var eventtype = field.FieldType as Mono.Cecil.TypeDefinition;
                    if (eventtype == null)
                    {
                        try
                        {
                            eventtype = field.FieldType.Resolve();
                        }
                        catch
                        {
                            throw new Exception("can't parese event type from:" + field.FieldType.FullName + ".maybe it is System.Action<xxx> which is defined in mscorlib.dll，copy this dll in.");
                        }
                    }
                    if (eventtype != null)
                    {
                        foreach (var m in eventtype.Methods)
                        {
                            if (m.Name == "Invoke")
                            {
                                this.returntype = m.ReturnType.FullName;
                                try
                                {
                                    var _type = m.ReturnType.Resolve();
                                    foreach (var i in _type.Interfaces)
                                    {
                                        if (i.InterfaceType.Name == "IApiInterface")
                                        {
                                            this.returntype = "IInteropInterface";
                                        }
                                    }
                                }
                                catch (Exception)
                                {

                                }
                                foreach (var src in m.Parameters)
                                {
                                    string paramtype = src.ParameterType.FullName;
                                    if (src.ParameterType.IsGenericParameter)
                                    {
                                        var gtype = src.ParameterType as Mono.Cecil.GenericParameter;

                                        var srcgtype = field.FieldType as Mono.Cecil.GenericInstanceType;
                                        var rtype = srcgtype.GenericArguments[gtype.Position];
                                        paramtype = rtype.FullName;
                                        try
                                        {
                                            var _type = rtype.Resolve();
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
                                    }
                                    this.paramtypes.Add(new TParam(src.Name, paramtype));

                                }
                            }
                        }
                    }
                    break;
                }
            }
        }
        public bool isEvent = false;
        public string type;
        public string name;
        public string displayName;
        public string returntype;
        public List<TParam> paramtypes = new List<TParam>();
        public override string ToString()
        {
            return type;
        }
        public Mono.Cecil.FieldDefinition field;
    }
}
