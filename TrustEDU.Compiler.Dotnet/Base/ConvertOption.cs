namespace TrustEDU.Compiler.Dotnet.Base
{
    public class ConvertOption
    {
        public bool UseTERCx = false;
        public static ConvertOption Default
        {
            get
            {
                return new ConvertOption();
            }
        }
    }
}
