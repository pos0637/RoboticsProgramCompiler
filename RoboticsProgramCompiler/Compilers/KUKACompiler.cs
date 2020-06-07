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
    /// <summary>
    /// KUKA程序编译器
    /// </summary>
    public class KUKACompiler : Compiler
    {
        /// <summary>
        /// 符号解析器列表
        /// </summary>
        private readonly IParser[] parsers = new IParser[] {
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

        public override Assembly BuildProject(string rootFolder)
        {
            var assemblies = new List<Assembly>();
            var files = Directory.EnumerateFiles(rootFolder, "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(".src") || s.EndsWith(".dat"));
            foreach (var file in files) {
                var assembly = Compile(file);
                if (assembly == null) {
                    return null;
                }

                assemblies.Add(assembly);
            }

            var result = Link(assemblies);
            if (result == null) {
                return null;
            }

            return result;
        }

        public override void Clean()
        {
        }

        public override Assembly Compile(string filename)
        {
            try {
                var assembly = new Assembly() { filename = filename };
                var sr = new StreamReader(filename, Encoding.UTF8);
                string text;
                var line = 0;

                while ((text = sr.ReadLine()) != null) {
                    line++;
                    foreach (var parser in parsers) {
                        var result = parser.Parse(new Dictionary<string, object>() {
                            { "namespace", filename },
                            { "file", filename },
                            { "line", line },
                            { "column", 0 },
                            { "text", text }
                        });
                        if (result == null) {
                            continue;
                        }

                        Tracker.LogD($"compile > {text}");
                        lock (this) {
                            foreach (var symbol in result) {
                                if (symbol.Type != Symbol.SymbolType.Reference) {
                                    if (symbol.Type != Symbol.SymbolType.Variable) {
                                        assembly.instructions.Add(symbol);
                                    }

                                    assembly.symbols.Add(symbol.Name, symbol);
                                }
                                else {
                                    assembly.relocatedSymbols.Add(symbol.Name, symbol);
                                }
                            }
                        }
                    }
                }

                return assembly;
            }
            catch (Exception e) {
                Tracker.LogE(e);
                return null;
            }
        }

        public override Assembly Link(List<Assembly> assemblies)
        {
            return Link(new Assembly(), assemblies);
        }

        public override Assembly Link(Assembly source, List<Assembly> assemblies)
        {
            foreach (var assembly in assemblies) {
                source.instructions.AddRange(assembly.instructions);
                source.symbols.Concat(assembly.symbols);
            }

            var ip = 0;
            foreach (Instruction instruction in source.instructions) {
                instruction.address = ip++;
                var symbols = new List<Symbol>();
                foreach (var reference in instruction.referenceSymbols) {
                    var name = UUID.Generate(reference.Namespace, reference.Name);
                    if (!source.symbols.ContainsKey(name)) {
                        Tracker.LogE($"symbol not found: {name}");
                        return null;
                    }

                    symbols.Add(source.symbols[name]);
                }
            }

            return source;
        }
    }
}
