using RoboticsProgramCompiler.Miscs;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RoboticsProgramCompiler.Symbols.KUKA
{
    /// <summary>
    /// 位姿
    /// </summary>
    public class E6Pos : Variable, IParser
    {
        private const string regex = @"^DECL E6POS ([\S]*) = { X ([\S^,]*) , Y ([\S^,]*) , Z ([\S^,]*) , A ([\S^,]*) , B ([\S^,]*) , C ([\S^,]*) , S ([\S^,]*) , T ([\S^,]*) , E1 ([\S^,]*) , E2 ([\S^,]*) , E3 ([\S^,]*) , E4 ([\S^,]*) , E5 ([\S^,]*) , E6 ([\S^\}]*) \}";

        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float A { get; set; }
        public float B { get; set; }
        public float C { get; set; }
        public int S { get; set; }
        public int T { get; set; }
        public float E1 { get; set; }
        public float E2 { get; set; }
        public float E3 { get; set; }
        public float E4 { get; set; }
        public float E5 { get; set; }
        public float E6 { get; set; }

        public Symbol[] Parse(Dictionary<string, object> arguments)
        {
            var text = arguments["text"] as string;
            var mc = Regex.Match(text, regex.Replace(" ", @"[\s]*"));
            if (!mc.Success) {
                return null;
            }

            return new Symbol[] { new E6Pos() {
                    Namespace = arguments["namespace"] as string,
                    Name = UUID.Generate(arguments["namespace"] as string, mc.Groups[1].Value),
                    File = arguments["file"] as string,
                    Line = (int)arguments["line"],
                    Column = (int)arguments["column"],
                    Text = text,
                    X = float.Parse(mc.Groups[2].Value),
                    Y = float.Parse(mc.Groups[3].Value),
                    Z = float.Parse(mc.Groups[4].Value),
                    A = float.Parse(mc.Groups[5].Value),
                    B = float.Parse(mc.Groups[6].Value),
                    C = float.Parse(mc.Groups[7].Value),
                    S = int.Parse(mc.Groups[8].Value),
                    T = int.Parse(mc.Groups[9].Value),
                    E1 = float.Parse(mc.Groups[10].Value),
                    E2 = float.Parse(mc.Groups[11].Value),
                    E3 = float.Parse(mc.Groups[12].Value),
                    E4 = float.Parse(mc.Groups[13].Value),
                    E5 = float.Parse(mc.Groups[14].Value),
                    E6 = float.Parse(mc.Groups[15].Value)
             } };
        }
    }
}
