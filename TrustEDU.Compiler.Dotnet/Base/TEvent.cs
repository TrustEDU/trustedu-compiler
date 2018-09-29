using System.Collections.Generic;

namespace TrustEDU.Compiler.Dotnet.Base
{
    public class TEvent
    {
        public string Namespace;
        public string Name;
        public string DisplayName;
        public List<TParam> ParamTypes = new List<TParam>();
        public string ReturnType;
    }
}
