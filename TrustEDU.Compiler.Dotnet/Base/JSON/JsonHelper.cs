using System;

namespace TrustEDU.Compiler.Dotnet.Base.Json
{
    public static class JsonHelper
    {
        public static IJsonNode Parse(string json)
        {
            try
            {
                ScanObj obj = new ScanObj
                {
                    Json = json,
                    Seed = 0
                };
                IJsonNode node = Scan(obj);
                return node;
            }
            catch (Exception err)
            {
                throw new Exception("parse err:" + json, err);
            }
        }

        internal static IJsonNode ScanFirst(char c)
        {
            if (c == ' ' || c == '\n' || c == '\r' || c == '\t')
            {
                return null;
            }
            if (c == '{')
            {
                return new JsonNodeObject();
            }
            else if (c == '[')
            {
                return new JsonNodeArray();
            }
            else if (c == '"')
            {
                return new JsonNodeString();
            }
            else
            {
                return new JsonNodeNumber();
            }
        }

        internal static IJsonNode Scan(ScanObj scan)
        {
            for (int i = 0; i < scan.Json.Length; i++)
            {
                IJsonNode node = ScanFirst(scan.Json[i]);
                if (node != null)
                {
                    scan.Seed = i;
                    node.Scan(scan);
                    return node;
                }
            }
            return null;
        }


    }
}
