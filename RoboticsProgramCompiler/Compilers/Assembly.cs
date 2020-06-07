using RoboticsProgramCompiler.Symbols;
using System.Collections.Generic;

namespace RoboticsProgramCompiler.Compilers
{
    /// <summary>
    /// 程序集
    /// </summary>
    public class Assembly
    {
        /// <summary>
        /// 文件名
        /// </summary>
        public string filename;

        /// <summary>
        /// 指令集合
        /// </summary>
        public List<Symbol> instructions = new List<Symbol>();

        /// <summary>
        /// 符号表
        /// </summary>
        public Dictionary<string, Symbol> symbols = new Dictionary<string, Symbol>();

        /// <summary>
        /// 重定位符号表
        /// </summary>
        public Dictionary<string, Symbol> relocatedSymbols = new Dictionary<string, Symbol>();
    }
}
