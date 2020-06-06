using RoboticsProgramCompiler.Miscs;
using RoboticsProgramCompiler.Symbols;
using RoboticsProgramCompiler.Symbols.KUKA;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RoboticsProgramCompiler.Compilers
{
    public class KUKACompiler : Compiler
    {
        /// <summary>
        /// 符号解析器列表
        /// </summary>
        private IParser[] parsers = new IParser[] {
            new E6Pos(),
            new FDAT(),
            new LDAT(),
            new PDAT(),
            new SetFDAT(),
            new SetLDAT(),
            new SetPDAT(),
            new SetPTPParams(),
            new SetCPParams(),
            new PTP(),
            new LIN()
        };

        public override bool BuildProject(string rootFolder)
        {
            var files = Directory.EnumerateFiles(rootFolder, "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(".src") || s.EndsWith(".dat"));
            foreach (var file in files) {
                if (!Compile(file)) {
                    return false;
                }
            }

            if (!Link()) {
                return false;
            }

            return true;
        }

        public override bool Compile(string filename)
        {
            try {
                var sr = new StreamReader(filename, Encoding.UTF8);
                string text;
                var line = 0;
                while ((text = sr.ReadLine()) != null) {
                    line++;
                    foreach (var parser in parsers) {
                        var result = parser.Parse(new Dictionary<string, object>() {
                            { "file", filename },
                            { "line", line },
                            { "column", 0 },
                            { "text", text }
                        });
                        if (result == null) {
                            continue;
                        }

                        lock (this) {
                            symbols.AddRange(result);
                            instructions.AddRange(
                                from s in symbols
                                where s.Type != Symbol.SymbolType.Variable
                                select s);
                        }
                    }
                }

                return true;
            }
            catch (Exception e) {
                Tracker.LogE(e);
                return false;
            }
        }

        public override bool Link()
        {
            throw new NotImplementedException();
        }

        public override bool Link(List<Symbol> instructions, List<Symbol> symbols)
        {
            throw new NotImplementedException();
        }
    }
}
