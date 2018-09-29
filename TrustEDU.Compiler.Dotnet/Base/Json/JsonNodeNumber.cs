using System;
using System.Collections.Generic;
using System.Text;

namespace TrustEDU.Compiler.Dotnet.Base.Json
{
    public class JsonNodeNumber : IJsonNode
    {
        public JsonNodeNumber()
        {
        }
        public JsonNodeNumber(double Value)
        {
            this.Value = Value;
            this.isBool = false;
        }
        public JsonNodeNumber(bool Value)
        {
            this.Value = Value ? 1 : 0;
            this.isBool = true;
        }
        public double Value
        {
            get;
            set;
        }
        public bool isBool
        {
            get;
            private set;
        }
        public bool isNull
        {
            get;
            private set;
        }
        public void SetNull()
        {
            this.isNull = true;
            this.isBool = false;
        }
        public void SetBool(bool v)
        {
            this.Value = v ? 1 : 0;
            this.isBool = true;
        }
        public override string ToString()
        {
            if (isBool)
            {
                return ((bool)this) ? "true" : "false";
            }
            else if (isNull)
            {
                return "null";
            }
            else
            {
                return Value.ToString();
            }
        }

        public JsonType Type
        {
            get
            {
                return JsonType.Number;
            }
        }
        public void ConvertToString(StringBuilder sb)
        {
            sb.Append(ToString());
        }
        public void ConvertToStringWithFormat(StringBuilder sb, int spacesub)
        {
            //for (int i = 0; i < space; i++)
            //    sb.Append(' ');
            ConvertToString(sb);
        }
        public void ConvertToStringPhp(StringBuilder sb)
        {

            sb.Append(ToString());
        }
        public void ConvertToStringPhpWithFormat(StringBuilder sb, int spacesub)
        {
            //for (int i = 0; i < space; i++)
            //    sb.Append(' ');
            ConvertToStringPhp(sb);
        }
        public void Scan(ScanObj scan)
        {
            string number = "";
            for (int i = scan.Seed; i < scan.Json.Length; i++)
            {
                char c = scan.Json[i];
                if (c != ',' && c != ']' && c != '}' && c != ' ')
                {
                    if (c != '\n')
                        number += c;
                }
                else
                {
                    scan.Seed = i;
                    break;
                }
            }

            if (number.ToLower() == "true")
            {
                Value = 1;
                isBool = true;
            }
            else if (number.ToLower() == "false")
            {
                Value = 0;
                isBool = true;
            }
            else if (number.ToLower() == "null")
            {
                Value = 0;
                isBool = true;
            }
            else
            {
                Value = double.Parse(number);
                isBool = false;
            }
        }
        public static implicit operator double(JsonNodeNumber m)
        {
            return m.Value;
        }
        public static implicit operator float(JsonNodeNumber m)
        {
            return (float)m.Value;
        }
        public static implicit operator int(JsonNodeNumber m)
        {
            return (int)m.Value;
        }
        public static implicit operator uint(JsonNodeNumber m)
        {
            return (uint)m.Value;
        }

        public static implicit operator bool(JsonNodeNumber m)
        {
            return (uint)m.Value != 0;
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

        public void AddArrayValue(double Value)
        {
            throw new NotImplementedException();
        }

        public void AddArrayValue(bool Value)
        {
            throw new NotImplementedException();
        }

        public void AddArrayValue(string Value)
        {
            throw new NotImplementedException();
        }

        public void SetArrayValue(int index, IJsonNode node)
        {
            throw new NotImplementedException();
        }

        public void SetArrayValue(int index, double Value)
        {
            throw new NotImplementedException();
        }

        public void SetArrayValue(int index, bool Value)
        {
            throw new NotImplementedException();
        }

        public void SetArrayValue(int index, string Value)
        {
            throw new NotImplementedException();
        }

        public void SetDictValue(string key, IJsonNode node)
        {
            throw new NotImplementedException();
        }

        public void SetDictValue(string key, double Value)
        {
            throw new NotImplementedException();
        }

        public void SetDictValue(string key, bool Value)
        {
            throw new NotImplementedException();
        }

        public void SetDictValue(string key, string Value)
        {
            throw new NotImplementedException();
        }

        public void SetValue(double Value)
        {
            this.Value = Value;
            this.isBool = false;
            this.isNull = false;
        }

        public void SetValue(string Value)
        {
            throw new NotImplementedException();
        }

        public void SetValue(bool Value)
        {
            this.Value = Value ? 1 : 0;
            this.isBool = true;
            this.isNull = false;
        }

        public double AsDouble()
        {
            if (!this.isNull && !this.isBool)
                return this.Value;
            throw new Exception("Value type is different");
        }

        public int AsInt()
        {
            if (!this.isNull && !this.isBool)
                return (int)this.Value;
            throw new Exception("Value type is different");
        }

        public bool AsBool()
        {
            if (this.isBool)
            {
                return (uint)Value != 0;
            }
            throw new Exception("Value type is different");
        }

        public bool IsNull()
        {
            return isNull;
        }

        public string AsString()
        {
            throw new NotImplementedException();
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
