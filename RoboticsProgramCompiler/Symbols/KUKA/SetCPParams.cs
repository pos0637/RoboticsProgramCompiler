using RoboticsProgramCompiler.Executors;
using RoboticsProgramCompiler.Miscs;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RoboticsProgramCompiler.Symbols.KUKA
{
    /// <summary>
    /// 设置参数
    /// </summary>
    public class SetCPParams : Instruction, IParser
    {
        private const string regex = @"^BAS \( #CP_PARAMS , ([\S^\)^\s]*) \)";

        public override object Execute(Executor executor)
        {
            Tracker.LogD($"SetCPParams {referenceSymbols}");
            return null;
        }

        public Symbol[] Parse(Dictionary<string, object> arguments)
        {
            var text = arguments["text"] as string;
            var mc = Regex.Match(text, regex.Replace(" ", @"[\s]*"));
            if (!mc.Success) {
                return null;
            }

            var variable = new Variable() {
                Namespace = arguments["namespace"] as string,
                Name = UUID.Generate(arguments["file"] as string),
                File = arguments["file"] as string,
                Line = (int)arguments["line"],
                Column = (int)arguments["column"],
                Text = mc.Groups[1].Value,
                Value = mc.Groups[1].Value
            };

            return new Symbol[] { new SetCPParams() {
                Namespace = arguments["namespace"] as string,
                Name = UUID.Generate(arguments["file"] as string),
                File = arguments["file"] as string,
                Line = (int)arguments["line"],
                Column = (int)arguments["column"],
                Text = text,
                referenceSymbols = new List<Symbol>() {
                    new Reference() {
                        Namespace = variable.Namespace,
                        Name = variable.Name
                    }
                }
            }, variable };
        }
    }
}
