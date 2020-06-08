using RoboticsProgramCompiler.Executors;
using RoboticsProgramCompiler.Miscs;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RoboticsProgramCompiler.Symbols.KUKA
{
    /// <summary>
    /// 标签
    /// </summary>
    public class CALL : Instruction, IParser
    {
        private const string regex = @"^([a-zA-Z0-9_]*) \( \) ";

        public override object Execute(Executor executor)
        {
            Tracker.LogD($"execute > {GetType().Name}: {Text} ({referenceSymbols})");
            if (!referenceSymbols[0].GetType().IsSubclassOf(typeof(Label))) {
                Tracker.LogE($"CALL label error: {referenceSymbols[0]}");
                return null;
            }

            executor.Call((referenceSymbols[0] as Label).Address);
            return null;
        }

        public Symbol[] Parse(Dictionary<string, object> arguments)
        {
            var text = (arguments["text"] as string).ToUpper();
            var mc = Regex.Match(text, regex.Replace(" ", @"[\s]*"));
            if (!mc.Success) {
                return null;
            }

            return new Symbol[] { new CALL() {
                Namespace = arguments["namespace"] as string,
                Name = UUID.Generate(arguments["namespace"] as string),
                Type = SymbolType.Call,
                File = arguments["file"] as string,
                Line = (int)arguments["line"],
                Column = (int)arguments["column"],
                Text = arguments["text"] as string,
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
