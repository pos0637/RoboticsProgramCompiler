using System.Collections.Generic;

namespace RoboticsProgramCompiler.Symbols
{
    /// <summary>
    /// 指令
    /// </summary>
    public abstract class Instruction : Symbol
    {
        /// <summary>
        /// 关联符号
        /// </summary>
        public List<string> symbols;

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="arguments">参数列表</param>
        /// <returns>返回值</returns>
        public abstract object[] Execute(params object[] arguments);

        public Instruction()
        {
            Type = SymbolType.Instruction;
        }
    }
}
