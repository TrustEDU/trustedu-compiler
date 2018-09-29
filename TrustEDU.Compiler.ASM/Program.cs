using System.IO;
using TrustEDU.Compiler.ASM.Base;

namespace TrustEDU.Compiler.ASM
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length == 0) return;
            if (!File.Exists(args[0])) return;
            var lines = File.ReadAllLines(args[0]);
            var semantemes = Semanteme.ProcessLines(lines);
            var table = new AddressTable(semantemes);
            var script = table.ToScript();
            var outputPath = args.Length >= 2 ? args[1] : Path.ChangeExtension(args[0], "avm");
            File.WriteAllBytes(outputPath, script);
        }
    }
}
