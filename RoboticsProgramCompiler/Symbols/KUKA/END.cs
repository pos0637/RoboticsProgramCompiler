using RoboticsProgramCompiler.Executors;
using RoboticsProgramCompiler.Miscs;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RoboticsProgramCompiler.Symbols.KUKA
{
    /// <summary>
    /// 标签
    /// </summary>
    public class END : Instruction, IParser
    {
        private const string regex = @"^END$";

        public override object Execute(Executor executor)
        {
            Tracker.LogD($"execute > {GetType().Name}: {Text} ({referenceSymbols})");
            executor.Return();
            return null;
        }

        public Symbol[] Parse(Dictionary<string, object> arguments)
        {
            var text = (arguments["text"] as string).ToUpper();
            var mc = Regex.Match(text, regex.Replace(" ", @"[\s]*"));
            if (!mc.Success) {
                return null;
            }

            return new Symbol[] { new END() {
                Namespace = arguments["namespace"] as string,
                Name = UUID.Generate(arguments["namespace"] as string, mc.Groups[1].Value),
                Type = SymbolType.Return,
                File = arguments["file"] as string,
                Line = (int)arguments["line"],
                Column = (int)arguments["column"],
                Text = arguments["text"] as string
            } };
        }
    }
}
