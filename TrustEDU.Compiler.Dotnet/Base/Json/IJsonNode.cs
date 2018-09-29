using System;
using System.Collections.Generic;
using System.Text;

namespace TrustEDU.Compiler.Dotnet.Base.Json
{
    public interface IJsonNode
    {
        JsonType Type { get; }
        void ConvertToString(StringBuilder sb);
        void ConvertToStringPhp(StringBuilder sb);
        void ConvertToStringWithFormat(StringBuilder sb, int spacesub);
        void ConvertToStringPhpWithFormat(StringBuilder sb, int spacesub);
        void Scan(ScanObj scan);

        IJsonNode Get(string path);
        IJsonNode GetArrayItem(int index);
        IJsonNode GetDictItem(string key);

        void AddArrayValue(IJsonNode node);
        void AddArrayValue(double value);
        void AddArrayValue(bool value);
        void AddArrayValue(string value);

        void SetArrayValue(int index, IJsonNode node);
        void SetArrayValue(int index, double value);
        void SetArrayValue(int index, bool value);
        void SetArrayValue(int index, string value);

        void SetDictValue(string key, IJsonNode node);
        void SetDictValue(string key, double value);
        void SetDictValue(string key, bool value);
        void SetDictValue(string key, string value);

        void SetValue(double value);
        void SetValue(string value);
        void SetValue(bool value);

        double AsDouble();
        int AsInt();
        bool AsBool();
        bool IsNull();

        String AsString();
        IList<IJsonNode> AsList();
        IDictionary<string, IJsonNode> asDict();

        bool HaveDictItem(string key);
        int GetListCount();
    }
}