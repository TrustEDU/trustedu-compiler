using System;
using System.Collections.Generic;
using System.Linq;

namespace TrustEDU.Compiler.ASM.Base
{
    internal abstract class Semanteme
    {
        public uint LineNumber;
        public uint BaseAddress;

        public static IEnumerable<Semanteme> ProcessLines(IEnumerable<string> lines)
        {
            bool isInComment = false;
            uint lineNumber = 0;
            foreach (string line in lines)
            {
                string pline = line;
                ++lineNumber;
                int index;
                if (isInComment)
                {
                    index = pline.IndexOf("*/", StringComparison.Ordinal);
                    if (index == -1) continue;
                    pline = pline.Substring(index + 2);
                }
                index = 0;
                while (true)
                {
                    index = pline.IndexOf("/*", index, StringComparison.Ordinal);
                    if (index == -1) break;
                    int index2 = pline.IndexOf("*/", index + 2, StringComparison.Ordinal);
                    if (index2 >= 0)
                    {
                        pline = pline.Substring(0, index) + pline.Substring(index2 + 2);
                    }
                    else
                    {
                        pline = pline.Substring(0, index);
                        isInComment = true;
                        break;
                    }
                }
                index = pline.IndexOf("//", StringComparison.Ordinal);
                if (index >= 0) pline = pline.Substring(0, index);
                pline = pline.Trim();
                index = pline.IndexOf(':');
                if (index >= 0)
                {
                    yield return new Label
                    {
                        LineNumber = lineNumber,
                        Name = pline.Substring(0, index)
                    };
                    pline = pline.Substring(index + 1).Trim();
                }
                if (!string.IsNullOrEmpty(pline))
                {
                    string[] words = pline.Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                    if (!Enum.TryParse(words[0], true, out InstructionName name))
                        throw new CompilerException(lineNumber, "syntax error");
                    yield return new Instruction
                    {
                        LineNumber = lineNumber,
                        Name = name,
                        Arguments = words.Skip(1).ToArray()
                    };
                }
            }
        }
    }
}
