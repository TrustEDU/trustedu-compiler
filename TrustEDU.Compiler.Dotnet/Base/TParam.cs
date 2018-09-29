namespace TrustEDU.Compiler.Dotnet.Base
{
    public class TParam
    {
        public TParam(string name, string type)
        {
            this.Name = name;
            this.Type = type;
        }
        public string Name
        {
            get;
            private set;
        }
        public string Type
        {
            get;
            private set;
        }
        public override string ToString()
        {
            return $"{Type} {Name}";
        }
    }
}