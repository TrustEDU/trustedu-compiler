using System;
using System.Collections.Generic;
using System.Text;

namespace TrustEDU.Compiler.Dotnet.Base.Json
{
    public class JsonNodeArray : List<IJsonNode>, IJsonNode
    {
        public JsonType Type
        {
            get { return JsonType.Array; }
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            ConvertToString(sb);
            return sb.ToString();
        }
        public void ConvertToString(StringBuilder sb)
        {
            sb.Append('[');
            for (int i = 0; i < this.Count; i++)
            {
                this[i].ConvertToString(sb);
                if (i != this.Count - 1)
                    sb.Append(',');
            }
            sb.Append(']');
        }
        public void ConvertToStringWithFormat(StringBuilder sb, int spacesub)
        {
            for (int _i = 0; _i < spacesub; _i++)
                sb.Append(' ');
            sb.Append("[\n");

            for (int i = 0; i < this.Count; i++)
            {
                if (this[i] is JsonNodeObject || this[i] is JsonNodeArray)
                {

                }
                else
                {
                    for (int _i = 0; _i < spacesub; _i++)
                        sb.Append(' ');
                }
                this[i].ConvertToStringWithFormat(sb, spacesub + 4);
                if (i != this.Count - 1)
                    sb.Append(',');
                sb.Append('\n');
            }
            //for (int _i = 0; _i < space; _i++)
            //    sb.Append(' ');
            for (int _i = 0; _i < spacesub; _i++)
                sb.Append(' ');
            sb.Append(']');
        }
        public void ConvertToStringPhp(StringBuilder sb)
        {
            sb.Append("Array(");
            for (int i = 0; i < this.Count; i++)
            {
                this[i].ConvertToStringPhp(sb);
                if (i != this.Count - 1)
                    sb.Append(',');
            }
            sb.Append(')');
        }
        public void ConvertToStringPhpWithFormat(StringBuilder sb, int spacesub)
        {
            //for (int _i = 0; _i < space; _i++)
            //    sb.Append(' ');
            sb.Append("Array(\n");
            for (int i = 0; i < this.Count; i++)
            {
                for (int _i = 0; _i < spacesub; _i++)
                    sb.Append(' ');

                this[i].ConvertToStringPhpWithFormat(sb, spacesub + 4);
                if (i != this.Count - 1)
                    sb.Append(',');
                sb.Append('\n');
            }
            //for (int _i = 0; _i < space; _i++)
            //    sb.Append(' ');
            for (int _i = 0; _i < spacesub; _i++)
                sb.Append(' ');
            sb.Append(')');
        }
        public void Scan(ScanObj scan)
        {
            for (int i = scan.Seed + 1; i < scan.Json.Length; i++)
            {
                char c = scan.Json[i];
                if (c == ',')
                    continue;
                if (c == ']')
                {
                    scan.Seed = i + 1;
                    break;
                }
                IJsonNode node = JsonHelper.ScanFirst(c);
                if (node != null)
                {
                    scan.Seed = i;
                    node.Scan(scan);
                    i = scan.Seed - 1;
                    this.Add(node);
                }

            }
        }

        public int GetFirstKey02(string path, int start, out string nextpath)
        {
            int _path = -1;
            for (int i = start + 1; i < path.Length; i++)
            {
                if (path[i] == '[')
                {
                    _path = GetFirstKey02(path, i, out nextpath);
                }
                if (path[i] == ']')
                {
                    nextpath = path.Substring(i + 1);
                    if (_path == -1)
                    {
                        _path = int.Parse(path.Substring(start + 1, i - start - 1));
                    }
                    return _path;
                }
            }
            nextpath = null;
            return -1;
        }

        public int GetFirstKey(string path, out string nextpath)
        {
            nextpath = null;
            int istart = 0;
            for (int i = 0; i < path.Length; i++)
            {
                if (path[i] == '.' || path[i] == ' ')
                {
                    istart++;
                    continue;
                }
                if (path[i] == '[')
                {
                    return GetFirstKey02(path, i, out nextpath);
                }

            }

            return -1;


        }


        public IJsonNode Get(string path)
        {
            if (path.Length == 0) return this;
            string nextpath;
            int key = GetFirstKey(path, out nextpath);
            if (key >= 0 && key < this.Count)
            {
                return this[key];
            }
            else
            {
                return null;
            }
        }

        public IJsonNode GetArrayItem(int index)
        {
            return this[index];
        }

        public IJsonNode GetDictItem(string key)
        {
            throw new NotImplementedException();
        }

        public void AddArrayValue(IJsonNode node)
        {
            this.Add(node);
        }

        public void AddArrayValue(double value)
        {
            this.Add(new JsonNodeNumber(value));
        }

        public void AddArrayValue(bool value)
        {
            this.Add(new JsonNodeNumber(value));
        }

        public void AddArrayValue(string value)
        {
            this.Add(new JsonNodeString(value));
        }

        public void SetArrayValue(int index, IJsonNode node)
        {
            this[index] = node;
        }

        public void SetArrayValue(int index, double value)
        {
            this[index] = new JsonNodeNumber(value);
        }

        public void SetArrayValue(int index, bool value)
        {
            this[index] = new JsonNodeNumber(value);
        }

        public void SetArrayValue(int index, string value)
        {
            this[index] = new JsonNodeString(value);
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public IList<IJsonNode> AsList()
        {
            return this;
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
            return this.Count;
        }
    }
}
