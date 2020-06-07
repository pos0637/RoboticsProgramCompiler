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
            new LIN(),
            new DEF(),
            new END(),
            new CALL()
        };

        private PDAT PDEFAULT = new PDAT() { Name = "PDEFAULT" };
        private FDAT FHOME = new FDAT() { Name = "FHOME" };
        private E6Pos XHOME = new E6Pos() { Name = "XHOME" };

        public override Assembly BuildProject(string rootFolder)
        {
            rootFolder = rootFolder.Replace("\\", "/");
            var assemblies = new List<Assembly>();
            var files = Directory.EnumerateFiles(rootFolder, "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(".src") || s.EndsWith(".dat"));
            foreach (var file in files) {
                var assembly = Compile(rootFolder, file.Replace("\\", "/"));
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

        public override Assembly Compile(string rootFolder, string filename)
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
                            { "namespace", filename.Substring(rootFolder.Length + 1).ToLower() },
                            { "file", filename.Substring(rootFolder.Length + 1) },
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
            return Link(GetDefaultAssembly(), assemblies);
        }

        public override Assembly Link(Assembly source, List<Assembly> assemblies)
        {
            foreach (var assembly in assemblies) {
                source.instructions.AddRange(assembly.instructions);
                foreach (var symbol in assembly.symbols) {
                    if (source.symbols.ContainsKey(symbol.Key)) {
                        Tracker.LogE($"symbol duplicated: {symbol.Value}");
                        return null;
                    }
                    source.symbols.Add(symbol.Key, symbol.Value);
                }
            }

            var ip = 0;
            foreach (Instruction instruction in source.instructions) {
                instruction.Address = ip++;
                var symbols = new List<Symbol>();
                foreach (var reference in instruction.referenceSymbols) {
                    if (!(reference is Reference)) {
                        symbols.Add(reference);
                        continue;
                    }

                    var symbol = FindSymbol(source, reference as Reference);
                    if (symbol == null) {
                        Tracker.LogE($"symbol not found: {reference}");
                        return null;
                    }

                    symbols.Add(symbol);
                }

                instruction.referenceSymbols = symbols;
            }

            return source;
        }

        public override Symbol FindSymbol(Assembly assembly, Reference reference)
        {
            // 查找同名符号
            if (assembly.symbols.ContainsKey(reference.Name)) {
                return assembly.symbols[reference.Name];
            }

            // 同程序文件中查找符号
            var name = UUID.Generate(reference.Namespace, reference.Name);
            if (assembly.symbols.ContainsKey(name)) {
                return assembly.symbols[name];
            }

            // 同程序文件的数据文件中查找符号
            name = name.Replace(".src", ".dat");
            if (assembly.symbols.ContainsKey(name)) {
                return assembly.symbols[name];
            }

            // 查找全局符号
            foreach (var symbol in assembly.symbols) {
                var simpleName = string.IsNullOrEmpty(symbol.Value.Namespace) ? symbol.Value.Name : symbol.Value.Name.Substring(symbol.Value.Namespace.Length + 1);
                if (simpleName.Equals(reference.Name)) {
                    return symbol.Value;
                }
            }

            return null;
        }

        /// <summary>
        /// 获取默认程序集
        /// </summary>
        /// <returns>默认程序集</returns>
        private Assembly GetDefaultAssembly()
        {
            var assembly = new Assembly();
            assembly.symbols.Add(PDEFAULT.Name, PDEFAULT);
            assembly.symbols.Add(FHOME.Name, FHOME);
            assembly.symbols.Add(XHOME.Name, XHOME);

            return assembly;
        }
    }
}
