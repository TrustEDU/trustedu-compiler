using System;
using System.Collections.Generic;
using System.Text;


namespace TrustEDU.Compiler.Dotnet.Base.Json
{
    public class JsonNodeObject : Dictionary<string, IJsonNode>, IJsonNode
    {
        public JsonType Type
        {
            get { return JsonType.Object; }
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            ConvertToString(sb);
            return sb.ToString();
        }
        public void ConvertToString(StringBuilder sb)
        {
            sb.Append('{');
            int i = Count;
            foreach (var item in this)
            {
                sb.Append('\"');
                sb.Append(item.Key);
                sb.Append("\":");
                item.Value.ConvertToString(sb);
                i--;
                if (i != 0) sb.Append(',');
            }
            sb.Append('}');
        }
        public void ConvertToStringWithFormat(StringBuilder sb, int spacesub)
        {
            for (int _i = 0; _i < spacesub; _i++)
                sb.Append(' ');
            sb.Append("{\n");
            int i = Count;
            foreach (var item in this)
            {
                for (int _i = 0; _i < spacesub + 4; _i++)
                    sb.Append(' ');

                sb.Append('\"');
                sb.Append(item.Key);
                sb.Append("\":");
                if (item.Value is JsonNodeArray || item.Value is JsonNodeObject)
                    sb.Append('\n');
                item.Value.ConvertToStringWithFormat(sb, spacesub + 4);
                i--;
                if (i != 0) sb.Append(',');
                sb.Append('\n');
            }
            //for (int _i = 0; _i < space; _i++)
            //    sb.Append(' ');
            for (int _i = 0; _i < spacesub; _i++)
                sb.Append(' ');
            sb.Append('}');
        }
        public void ConvertToStringPhp(StringBuilder sb)
        {
            sb.Append("Array(");
            int i = Count;
            foreach (var item in this)
            {
                sb.Append('\"');
                sb.Append(item.Key);
                sb.Append("\"=>");
                item.Value.ConvertToStringPhp(sb);
                i--;
                if (i != 0) sb.Append(',');
            }
            sb.Append(')');
        }
        public void ConvertToStringPhpWithFormat(StringBuilder sb, int spacesub)
        {
            //for (int _i = 0; _i < space; _i++)
            //    sb.Append(' ');
            sb.Append("Array(\n");
            int i = Count;
            foreach (var item in this)
            {
                for (int _i = 0; _i < spacesub; _i++)
                    sb.Append(' ');

                sb.Append('\"');
                sb.Append(item.Key);
                sb.Append("\"=>");
                item.Value.ConvertToStringPhpWithFormat(sb, spacesub + 4);
                i--;
                if (i != 0) sb.Append(',');
                sb.Append('\n');
            }
            //for (int _i = 0; _i < space; _i++)
            //    sb.Append(' ');
            for (int _i = 0; _i < spacesub; _i++)
                sb.Append(' ');
            sb.Append(')');
        }
        //public IJsonNode  this[string key]
        //{
        //    get
        //    {
        //        if (this.ContainsKey(key))
        //        {
        //            return base[key];
        //        }

        //        throw new Exception("key not exist");

        //    }
        //    set
        //    {
        //        if (value == null)
        //        {

        //            throw new Exception("value is null. key:"+key);
        //        }
        //        base[key] = value;
        //    }
        //}

        public void Scan(ScanObj scan)
        {
            string key = null;
            int keystate = 0;//0 nokey 1scankey 2gotkey
            for (int i = scan.Seed + 1; i < scan.Json.Length; i++)
            {
                char c = scan.Json[i];
                if (keystate != 1 && (c == ',' || c == ':'))
                    continue;
                if (c == '}')
                {
                    scan.Seed = i + 1;
                    break;
                }
                if (keystate == 0)
                {
                    if (c == '\"')
                    {
                        keystate = 1;
                        key = "";
                    }
                }
                else if (keystate == 1)
                {
                    if (c == '\"')
                    {
                        keystate = 2;
                        //scan.Seed = i + 1;
                        continue;
                    }
                    else
                    {
                        key += c;
                    }
                }
                else
                {
                    IJsonNode node = JsonHelper.ScanFirst(c);
                    if (node != null)
                    {
                        scan.Seed = i;
                        node.Scan(scan);
                        i = scan.Seed - 1;
                        this.Add(key, node);
                        keystate = 0;
                    }
                }

            }
        }
        public string GetFirstKey01(string path, int start, out string nextpath)
        {
            for (int i = start + 1; i < path.Length; i++)
            {
                if (path[i] == '\\') continue;
                if (path[i] == '\"')
                {
                    nextpath = path.Substring(i + 1);
                    var _path = path.Substring(start + 1, i - start - 1);
                    return _path;
                }
            }
            nextpath = null;
            return null;
        }
        public string GetFirstKey02(string path, int start, out string nextpath)
        {
            string _path = null;
            for (int i = start + 1; i < path.Length; i++)
            {
                if (path[i] == '[')
                {
                    _path = GetFirstKey02(path, i, out nextpath);
                }
                if (path[i] == '\"')
                {
                    _path = GetFirstKey01(path, i, out nextpath);
                    i += _path.Length + 2;
                }
                if (path[i] == ']')
                {
                    nextpath = path.Substring(i + 1);
                    if (_path == null)
                    {
                        _path = path.Substring(start + 1, i - start - 1);
                    }
                    return _path;
                }
            }
            nextpath = null;
            return null;
        }
        public string GetFirstKey(string path, out string nextpath)
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
                else if (path[i] == '\"')
                {
                    return GetFirstKey01(path, i, out nextpath);
                }
                else
                {

                    int iend1 = path.IndexOf('[', i + 1);
                    if (iend1 == -1) iend1 = path.Length;
                    int iend2 = path.IndexOf('.', i + 1);
                    if (iend2 == -1) iend2 = path.Length;
                    int iss = Math.Min(iend1, iend2);

                    var _path = path.Substring(istart, iss - istart);
                    nextpath = path.Substring(iss);
                    return _path;
                }

            }

            return null;


        }
        public IJsonNode Get(string path)
        {
            if (path.Length == 0) return this;
            string nextpath;
            string key = GetFirstKey(path, out nextpath);
            if (this.ContainsKey(key))
            {
                return this[key].Get(nextpath);
            }
            else
            {
                return null;
            }

        }

        public IJsonNode GetArrayItem(int index)
        {
            throw new NotImplementedException();
        }

        public IJsonNode GetDictItem(string key)
        {
            return this[key];
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
            this[key] = node;
        }

        public void SetDictValue(string key, double value)
        {
            this[key] = new JsonNodeNumber(value);
        }

        public void SetDictValue(string key, bool value)
        {
            this[key] = new JsonNodeNumber(value);
        }

        public void SetDictValue(string key, string value)
        {
            this[key] = new JsonNodeString(value);
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
            throw new NotImplementedException();
        }

        public IDictionary<string, IJsonNode> asDict()
        {
            return this;
        }


        public bool HaveDictItem(string key)
        {
            return ContainsKey(key);
        }

        public int GetListCount()
        {
            throw new NotImplementedException();
        }
    }
}
