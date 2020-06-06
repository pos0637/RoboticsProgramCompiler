using RoboticsProgramCompiler.Miscs;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RoboticsProgramCompiler.Symbols.KUKA
{
    /// <summary>
    /// 参数
    /// </summary>
    public class FDAT : Variable, IParser
    {
        private const string regex = @"DECL FDAT ([\S]*) = { TOOL_NO ([\S^,]*) , BASE_NO ([\S^,]*) , IPO_FRAME ([\S^,]*) , POINT2\[\] \"" \"", TQ_STATE ([\S^\}]*)";

        public int TOOL_NO { get; set; }
        public int BASE_NO { get; set; }
        public string IPO_FRAME { get; set; }

        public Symbol[] Parse(Dictionary<string, object> arguments)
        {
            var text = arguments["text"] as string;
            var mc = Regex.Match(text, regex.Replace(" ", @"[\s]*"));
            if (!mc.Success) {
                return null;
            }

            return new Symbol[] { new FDAT() {
                Namespace = arguments["namespace"] as string,
                Name = UUID.Generate(arguments["file"] as string, mc.Groups[1].Value),
                File = arguments["file"] as string,
                Line = (int)arguments["line"],
                Column = (int)arguments["column"],
                Text = text,
                TOOL_NO = int.Parse(mc.Groups[2].Value),
                BASE_NO = int.Parse(mc.Groups[3].Value),
                IPO_FRAME = mc.Groups[4].Value
            } };
        }
    }
}
