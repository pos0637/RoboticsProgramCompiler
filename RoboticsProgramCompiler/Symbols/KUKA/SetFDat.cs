﻿using RoboticsProgramCompiler.Executors;
using RoboticsProgramCompiler.Miscs;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RoboticsProgramCompiler.Symbols.KUKA
{
    /// <summary>
    /// 设置参数
    /// </summary>
    public class SetFDAT : Instruction, IParser
    {
        private const string regex = @"^FDAT_ACT = ([\S]*)";

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

            return new Symbol[] { new SetFDAT() {
                Namespace = arguments["namespace"] as string,
                Name = UUID.Generate(arguments["namespace"] as string),
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
