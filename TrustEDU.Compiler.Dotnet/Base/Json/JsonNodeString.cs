using System;
using System.Collections.Generic;
using System.Text;

namespace TrustEDU.Compiler.Dotnet.Base.Json
{
    public class JsonNodeString : IJsonNode
    {
        public JsonNodeString()
        {

        }
        public JsonNodeString(string value)
        {
            this.value = value;
        }
        public string value
        {
            get;
            set;
        }
        public override string ToString()
        {
            return value;
        }

        public JsonType Type
        {
            get
            {
                return JsonType.String;
            }
        }

        public void ConvertToString(StringBuilder sb)
        {
            sb.Append('\"');
            if (value != null)
            {
                string v = value.Replace("\\", "\\\\");
                v = v.Replace("\"", "\\\"");
                sb.Append(v);
            }
            sb.Append('\"');
        }

        public void ConvertToStringWithFormat(StringBuilder sb, int spacesub)
        {
            //for (int i = 0; i < space; i++)
            //    sb.Append(' ');
            ConvertToString(sb);
        }

        public void ConvertToStringPhp(StringBuilder sb)
        {
            sb.Append('\"');
            if (value != null)
            {
                string v = value.Replace("\\", "\\\\");
                v = v.Replace("\"", "\\\"");
                sb.Append(v);
            }
            sb.Append('\"');
        }

        public void ConvertToStringPhpWithFormat(StringBuilder sb, int spacesub)
        {
            ConvertToStringPhp(sb);
        }
        public void Scan(ScanObj scan)
        {
            string _value = "";
            for (int i = scan.Seed + 1; i < scan.Json.Length; i++)
            {
                char c = scan.Json[i];
                if (c == '\\')
                {
                    i++;
                    c = scan.Json[i];
                    _value += c;
                }

                else if (c != '\"')
                {

                    _value += c;
                }

                else
                {
                    scan.Seed = i + 1;
                    break;
                }
            }
            value = _value;
        }

        public static implicit operator string(JsonNodeString m)
        {
            return m.value;
        }



        public IJsonNode Get(string path)
        {
            if (string.IsNullOrEmpty(path)) return this;

            return null;
        }


        public IJsonNode GetArrayItem(int index)
        {
            throw new NotImplementedException();
        }

        public IJsonNode GetDictItem(string key)
        {
            throw new NotImplementedException();
        }

        public void AddArrayValue(IJsonNode node)
        {
            throw new NotImplementedException();
        }

        public void AddArrayValue(double value)
        {
            throw new NotImplementedException();
        }

        public void AddArrayValue(bool value)
        {
            throw new NotImplementedException();
        }

        public void AddArrayValue(string value)
        {
            throw new NotImplementedException();
        }

        public void SetArrayValue(int index, IJsonNode node)
        {
            throw new NotImplementedException();
        }

        public void SetArrayValue(int index, double value)
        {
            throw new NotImplementedException();
        }

        public void SetArrayValue(int index, bool value)
        {
            throw new NotImplementedException();
        }

        public void SetArrayValue(int index, string value)
        {
            throw new NotImplementedException();
        }

        public void SetDictValue(string key, IJsonNode node)
        {
            throw new NotImplementedException();
        }

        public void SetDictValue(string key, double value)
        {
            throw new NotImplementedException();
        }

        public void SetDictValue(string key, bool value)
        {
            throw new NotImplementedException();
        }

        public void SetDictValue(string key, string value)
        {
            throw new NotImplementedException();
        }

        public void SetValue(double value)
        {
            throw new NotImplementedException();
        }

        public void SetValue(string value)
        {
            this.value = value;
        }

        public void SetValue(bool value)
        {
            throw new NotImplementedException();
        }

        public double AsDouble()
        {
            throw new NotImplementedException();
        }

        public int AsInt()
        {
            throw new NotImplementedException();
        }

        public bool AsBool()
        {
            throw new NotImplementedException();
        }

        public bool IsNull()
        {
            return false;
        }

        public string AsString()
        {
            return value;
        }

        public IList<IJsonNode> AsList()
        {
            throw new NotImplementedException();
        }

        public IDictionary<string, IJsonNode> asDict()
        {
            throw new NotImplementedException();
        }


        public bool HaveDictItem(string key)
        {
            throw new NotImplementedException();
        }

        public int GetListCount()
        {
            throw new NotImplementedException();
        }
    }
}
