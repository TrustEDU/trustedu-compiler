using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Logging;
using TrustEDU.Compiler.Dotnet.Base.Json;

namespace TrustEDU.Compiler.Dotnet.Base
{
    public class TModule
    {
        public TModule(ILogger logger)
        {
            this.logger = logger;
        }

        ILogger logger;

        public SortedDictionary<int, TCode> total_Codes = new SortedDictionary<int, TCode>();
        public byte[] Build()
        {
            List<byte> bytes = new List<byte>();
            foreach (var c in this.total_Codes.Values)
            {
                bytes.Add((byte)c.code);
                if (c.bytes != null)
                    for (var i = 0; i < c.bytes.Length; i++)
                    {
                        bytes.Add(c.bytes[i]);
                    }
            }
            return bytes.ToArray();
        }
        public string mainMethod;
        public ConvertOption option;
        public Dictionary<string, TMethod> mapMethods = new Dictionary<string, TMethod>();
        public Dictionary<string, TEvent> mapEvents = new Dictionary<string, TEvent>();
        //public Dictionary<string, byte[]> codes = new Dictionary<string, byte[]>();
        //public byte[] GetScript(byte[] script_hash)
        //{
        //    string strhash = "";
        //    foreach (var b in script_hash)
        //    {
        //        strhash += b.ToString("X02");
        //    }
        //    return codes[strhash];
        //}
        public string GenJson()
        {
            JsonNodeObject json = new JsonNodeObject();
            json["__name__"] = new JsonNodeString("neomodule.");

            //code
            var jsoncode = new JsonNodeArray();
            json["code"] = jsoncode;
            foreach (var c in this.total_Codes.Values)
            {
                jsoncode.Add(c.GenJson());
            }
            //code bytes
            var code = this.Build();
            var codestr = "";
            foreach (var c in code)
            {
                codestr += c.ToString("X02");
            }
            json.SetDictValue("codebin", codestr);

            //calls
            JsonNodeObject methodinfo = new JsonNodeObject();
            json["call"] = methodinfo;
            foreach (var m in this.mapMethods)
            {
                methodinfo[m.Key] = m.Value.GenJson();
            }


            StringBuilder sb = new StringBuilder();
            json.ConvertToStringWithFormat(sb, 4);
            return sb.ToString();
        }
        public void FromJson(string json)
        {

        }
        SHA1 sha1 = SHA1.Create();
        public Dictionary<string, object> staticfields = new Dictionary<string, object>();

    }
}
