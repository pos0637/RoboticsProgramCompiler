using RoboticsProgramCompiler.Miscs;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RoboticsProgramCompiler.Symbols.KUKA
{
    /// <summary>
    /// 设置参数
    /// </summary>
    public class SetPDAT : Instruction, IParser
    {
        private const string regex = @"PDAT_ACT = ([\S]*)";

        public override object[] Execute(params object[] arguments)
        {
            throw new System.NotImplementedException();
        }

        public Symbol[] Parse(Dictionary<string, object> arguments)
        {
            var text = arguments["text"] as string;
            var mc = Regex.Match(text, regex.Replace(" ", @"[\s]*"));
            if (!mc.Success) {
                return null;
            }

            return new Symbol[] { new SetPDAT() {
                Namespace = arguments["namespace"] as string,
                Name = UUID.Generate(arguments["file"] as string),
                File = arguments["file"] as string,
                Line = (int)arguments["line"],
                Column = (int)arguments["column"],
                Text = text,
                symbols = new List<string>() { UUID.Generate(arguments["file"] as string, mc.Groups[1].Value) }
            } };
        }
    }
}
