using RoboticsProgramCompiler.Miscs;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RoboticsProgramCompiler.Symbols.KUKA
{
    /// <summary>
    /// 参数
    /// </summary>
    public class PDAT : Variable, IParser
    {
        private const string regex1 = @"^DECL PDAT ([\S]*) = { VEL ([\S^,]*) , ACC ([\S^,]*) , APO_DIST ([\S^,]*) , APO_MODE ([\S^,]*) , GEAR_JERK ([\S^,]*) , EXAX_IGN ([\S^\}]*) \}";
        private const string regex2 = @"^DECL PDAT ([\S]*) = { VEL ([\S^,]*) , ACC ([\S^,]*) , APO_DIST ([\S^,]*) , GEAR_JERK ([\S^,]*) , EXAX_IGN ([\S^\}]*) \}";

        public float VEL { get; set; }
        public float ACC { get; set; }
        public float APO_DIST { get; set; }
        public string APO_MODE { get; set; }
        public float GEAR_JERK { get; set; }
        public int EXAX_IGN { get; set; }

        public Symbol[] Parse(Dictionary<string, object> arguments)
        {
            var symbols = Parse1(arguments);
            if (symbols == null) {
                symbols = Parse2(arguments);
            }

            return symbols;
        }

        public Symbol[] Parse1(Dictionary<string, object> arguments)
        {
            var text = arguments["text"] as string;
            var mc = Regex.Match(text, regex1.Replace(" ", @"[\s]*"));
            if (!mc.Success) {
                return null;
            }

            return new Symbol[] { new PDAT() {
                Namespace = arguments["namespace"] as string,
                Name = UUID.Generate(arguments["namespace"] as string, mc.Groups[1].Value),
                File = arguments["file"] as string,
                Line = (int)arguments["line"],
                Column = (int)arguments["column"],
                Text = text,
                VEL = float.Parse(mc.Groups[2].Value),
                ACC = float.Parse(mc.Groups[3].Value),
                APO_DIST = float.Parse(mc.Groups[4].Value),
                APO_MODE = mc.Groups[5].Value,
                GEAR_JERK = float.Parse(mc.Groups[6].Value),
                EXAX_IGN = int.Parse(mc.Groups[7].Value)
            } };
        }

        public Symbol[] Parse2(Dictionary<string, object> arguments)
        {
            var text = arguments["text"] as string;
            var mc = Regex.Match(text, regex2.Replace(" ", @"[\s]*"));
            if (!mc.Success) {
                return null;
            }

            return new Symbol[] { new PDAT() {
                Namespace = arguments["namespace"] as string,
                Name = UUID.Generate(arguments["namespace"] as string, mc.Groups[1].Value),
                File = arguments["file"] as string,
                Line = (int)arguments["line"],
                Column = (int)arguments["column"],
                Text = text,
                VEL = float.Parse(mc.Groups[2].Value),
                ACC = float.Parse(mc.Groups[3].Value),
                APO_DIST = float.Parse(mc.Groups[4].Value),
                GEAR_JERK = float.Parse(mc.Groups[5].Value),
                EXAX_IGN = int.Parse(mc.Groups[6].Value)
            } };
        }
    }
}
