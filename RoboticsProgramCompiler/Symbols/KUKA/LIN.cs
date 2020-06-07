using RoboticsProgramCompiler.Miscs;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RoboticsProgramCompiler.Symbols.KUKA
{
    /// <summary>
    /// 直线运动
    /// </summary>
    public class LIN : Instruction, IParser
    {
        private const string regex = @"^LIN ([\S]*) ([\S]*) ";

        public bool C_DIS { get; set; }

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

            return new Symbol[] { new LIN() {
                Namespace = arguments["namespace"] as string,
                Name = UUID.Generate(arguments["file"] as string),
                File = arguments["file"] as string,
                Line = (int)arguments["line"],
                Column = (int)arguments["column"],
                Text = text,
                C_DIS = !string.IsNullOrEmpty(mc.Groups[2].Value),
                referenceSymbols = new List<Symbol>() {
                    new Reference() {
                        Namespace = arguments["namespace"] as string,
                        Name = mc.Groups[1].Value
                    }
                }
            } };
        }
    }
}
