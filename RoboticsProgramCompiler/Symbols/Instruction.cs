using RoboticsProgramCompiler.Executors;
using System.Collections.Generic;

namespace RoboticsProgramCompiler.Symbols
{
    /// <summary>
    /// 指令
    /// </summary>
    public abstract class Instruction : Symbol, IExecutable
    {
        /// <summary>
        /// 地址
        /// </summary>
        public int Address { get; set; }

        /// <summary>
        /// 引用符号表
        /// </summary>
        public List<Symbol> referenceSymbols = new List<Symbol>();

        public Instruction()
        {
            Type = SymbolType.Instruction;
        }

        public abstract object Execute(Executor executor);
    }
}
