using System.Collections.Generic;
using System.Security.Cryptography;
using TrustEDU.Compiler.Dotnet.Base.Json;

namespace TrustEDU.Compiler.Dotnet.Base
{
    public class TMethod
    {
        public string Namespace;
        public string Name;
        public string DisplayName;
        public List<TParam> ParamTypes = new List<TParam>();
        public string ReturnType;
        public bool IsPublic = true;
        public bool InSmartContract;
        public List<TParam> BodyVariables = new List<TParam>();
        public SortedDictionary<int, TCode> BodyCodes = new SortedDictionary<int, TCode>();
        public int funcaddr;
        public JsonNodeObject GenJson()
        {
            JsonNodeObject json = new JsonNodeObject();
            json.SetDictValue("name", this.Name);
            var sha1 = SHA1.Create();

            json.SetDictValue("returntype", this.ReturnType);

            json.SetDictValue("paramcount", this.ParamTypes.Count);
            JsonNodeArray jsonparams = new JsonNodeArray();
            json.SetDictValue("params", jsonparams);
            for (var i = 0; i < this.ParamTypes.Count; i++)
            {
                JsonNodeObject item = new JsonNodeObject();
                item.SetDictValue("name", this.ParamTypes[i].Name);
                item.SetDictValue("type", this.ParamTypes[i].Type);
                jsonparams.Add(item);
            }
            return json;
        }
        public void FromJson(JsonNodeObject json)
        {

        }
        //public byte[] Build()
        //{
        //    List<byte> bytes = new List<byte>();
        //    foreach (var c in this.BodyCodes.Values)
        //    {
        //        bytes.Add((byte)c.code);
        //        if (c.bytes != null)
        //            for (var i = 0; i < c.bytes.Length; i++)
        //            {
        //                bytes.Add(c.bytes[i]);
        //            }
        //    }
        //    return bytes.ToArray();
        //}
        public string lastsfieldname = null;//The name of the last loaded static member, only used by event

        public int lastparam = -1;//The last loaded parameter corresponds to
        public int lastCast = -1;
    }
}
