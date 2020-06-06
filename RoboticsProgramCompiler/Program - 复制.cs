using RoboticsProgramCompiler.Symbols.KUKA;
using System.Collections.Generic;

namespace RoboticsProgramCompiler
{
    class Program
    {
        static void Main(string[] args)
        {
            var symbols = new E6Pos().Parse(new Dictionary<string, object>() {
                {"file", ""},
                {"line", 0},
                {"column", 0},
                {"text", "DECL E6POS XP1={X -132.307266,Y -427.616180,Z 1162.82056,A 95.0179,B -1.98647153,C 1.45468986,S 6,T 50,E1 0.0,E2 0.0,E3 0.0,E4 0.0,E5 0.0,E6 0.0}" }
            });

            symbols = new FDAT().Parse(new Dictionary<string, object>(){
                {"file", ""},
                {"line", 0},
                {"column", 0},
                {"text", "DECL FDAT FP1={TOOL_NO 1,BASE_NO 0,IPO_FRAME #BASE,POINT2[] \" \",TQ_STATE FALSE}" }
            });

            symbols = new PDAT().Parse(new Dictionary<string, object>(){
                {"file", ""},
                {"line", 0},
                {"column", 0},
                {"text", "DECL PDAT PPDAT3={VEL 100.000,ACC 100.000,APO_DIST 100.000,APO_MODE #CDIS,GEAR_JERK 50.0000,EXAX_IGN 0}" }
            });

            symbols = new LDAT().Parse(new Dictionary<string, object>(){
                {"file", ""},
                {"line", 0},
                {"column", 0},
                {"text", "DECL LDAT LCPDAT14={VEL 2.00000,ACC 100.000,APO_DIST 100.000,APO_FAC 50.0000,AXIS_VEL 100.000,AXIS_ACC 100.000,ORI_TYP #VAR,CIRC_TYP #BASE,JERK_FAC 50.0000,GEAR_JERK 50.0000,EXAX_IGN 0}" }
            });

            symbols = new SetFDAT().Parse(new Dictionary<string, object>(){
                {"file", ""},
                {"line", 0},
                {"column", 0},
                {"text", "FDAT_ACT=FP38" }
            });

            symbols = new SetPDAT().Parse(new Dictionary<string, object>(){
                {"file", ""},
                {"line", 0},
                {"column", 0},
                {"text", "PDAT_ACT=PPDAT36" }
            });

            symbols = new SetLDAT().Parse(new Dictionary<string, object>(){
                {"file", ""},
                {"line", 0},
                {"column", 0},
                {"text", "LDAT_ACT=LCPDAT14" }
            });

            symbols = new SetPTPParams().Parse(new Dictionary<string, object>(){
                {"file", ""},
                {"line", 0},
                {"column", 0},
                {"text", "BAS(#PTP_PARAMS,30)" }
            });

            symbols = new SetCPParams().Parse(new Dictionary<string, object>(){
                {"file", ""},
                {"line", 0},
                {"column", 0},
                {"text", "BAS(#CP_PARAMS,0.2)" }
            });

            symbols = new PTP().Parse(new Dictionary<string, object>(){
                {"file", ""},
                {"line", 0},
                {"column", 0},
                {"text", "PTP XP40 C_DIS" }
            });

            symbols = new LIN().Parse(new Dictionary<string, object>(){
                {"file", ""},
                {"line", 0},
                {"column", 0},
                {"text", "LIN XP40 C_DIS" }
            });
        }
    }
}
