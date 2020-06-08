using RoboticsProgramCompiler.Executors;
using RoboticsProgramCompiler.Miscs;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RoboticsProgramCompiler.Symbols.KUKA
{
    /// <summary>
    /// 点到点运动
    /// </summary>
    public class PTP : Instruction, IParser
    {
        private const string regex = @"^PTP ([\S]*) ([\S]*) ";

        public bool C_DIS { get; set; }

        public override object Execute(Executor executor)
        {
            Tracker.LogD($"execute > {GetType().Name}: {Text} ({referenceSymbols})");
            return null;
        }

        public Symbol[] Parse(Dictionary<string, object> arguments)
        {
            var text = (arguments["text"] as string).ToUpper();
            var mc = Regex.Match(text, regex.Replace(" ", @"[\s]*"));
            if (!mc.Success) {
                return null;
            }

            return new Symbol[] { new PTP() {
                Namespace = arguments["namespace"] as string,
                Name = UUID.Generate(arguments["namespace"] as string),
                File = arguments["file"] as string,
                Line = (int)arguments["line"],
                Column = (int)arguments["column"],
                Text = arguments["text"] as string,
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
