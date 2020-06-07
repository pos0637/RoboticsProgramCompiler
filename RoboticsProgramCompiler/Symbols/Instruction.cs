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
        public int address;

        /// <summary>
        /// 引用符号表
        /// </summary>
        public List<Symbol> referenceSymbols;

        public Instruction()
        {
            Type = SymbolType.Instruction;
        }

        public abstract object Execute(Executor executor);
    }
}
