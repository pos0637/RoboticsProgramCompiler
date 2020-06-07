using RoboticsProgramCompiler.Executors;
using RoboticsProgramCompiler.Miscs;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RoboticsProgramCompiler.Symbols.KUKA
{
    /// <summary>
    /// 标签
    /// </summary>
    public class DEF : Label, IParser
    {
        private const string regex = @"^DEF ([\S]*) \( \) ";

        public override object Execute(Executor executor)
        {
            Tracker.LogD($"DEF {referenceSymbols}");
            return null;
        }

        public Symbol[] Parse(Dictionary<string, object> arguments)
        {
            var text = arguments["text"] as string;
            var mc = Regex.Match(text, regex.Replace(" ", @"[\s]*"));
            if (!mc.Success) {
                return null;
            }

            return new Symbol[] { new DEF() {
                Namespace = arguments["namespace"] as string,
                Name = mc.Groups[1].Value,
                Type = SymbolType.Label,
                File = arguments["file"] as string,
                Line = (int)arguments["line"],
                Column = (int)arguments["column"],
                Text = text
            } };
        }
    }
}
