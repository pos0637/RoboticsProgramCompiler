using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RoboticsProgramCompiler.Symbols.KUKA
{
    /// <summary>
    /// 参数
    /// </summary>
    public class LDAT : Variable, IParser
    {
        private const string regex = @"^DECL LDAT ([\S]*) = { VEL ([\S^,]*) , ACC ([\S^,]*) , APO_DIST ([\S^,]*) , APO_FAC ([\S^,]*) , AXIS_VEL ([\S^,]*) , AXIS_ACC ([\S^,]*) , ORI_TYP ([\S^,]*) , CIRC_TYP ([\S^,]*) , JERK_FAC ([\S^,]*) , GEAR_JERK ([\S^,]*) , EXAX_IGN ([\S^\}]*) \}";

        public float VEL { get; set; }
        public float ACC { get; set; }
        public float APO_DIST { get; set; }
        public float APO_FAC { get; set; }
        public float AXIS_VEL { get; set; }
        public float AXIS_ACC { get; set; }
        public string ORI_TYP { get; set; }
        public string CIRC_TYP { get; set; }
        public float JERK_FAC { get; set; }
        public float GEAR_JERK { get; set; }
        public int EXAX_IGN { get; set; }

        public Symbol[] Parse(Dictionary<string, object> arguments)
        {
            var text = arguments["text"] as string;
            var mc = Regex.Match(text, regex.Replace(" ", @"[\s]*"));
            if (!mc.Success) {
                return null;
            }

            return new Symbol[] { new LDAT() {
                Namespace = arguments["namespace"] as string,
                Name = mc.Groups[1].Value,
                File = arguments["file"] as string,
                Line = (int)arguments["line"],
                Column = (int)arguments["column"],
                Text = text,
                VEL = float.Parse(mc.Groups[2].Value),
                ACC = float.Parse(mc.Groups[3].Value),
                APO_DIST = float.Parse(mc.Groups[4].Value),
                APO_FAC = float.Parse(mc.Groups[5].Value),
                AXIS_VEL = float.Parse(mc.Groups[6].Value),
                AXIS_ACC = float.Parse(mc.Groups[7].Value),
                ORI_TYP = mc.Groups[8].Value,
                CIRC_TYP = mc.Groups[9].Value,
                JERK_FAC = float.Parse(mc.Groups[10].Value),
                GEAR_JERK = float.Parse(mc.Groups[11].Value),
                EXAX_IGN = int.Parse(mc.Groups[12].Value)
            } };
        }
    }
}
