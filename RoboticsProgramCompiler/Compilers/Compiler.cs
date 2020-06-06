using RoboticsProgramCompiler.Symbols;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace RoboticsProgramCompiler.Compilers
{
    /// <summary>
    /// 编译器接口
    /// </summary>
    public abstract class Compiler
    {
        /// <summary>
        /// 指令集合
        /// </summary>
        protected List<Symbol> instructions = new List<Symbol>();

        /// <summary>
        /// 符号表
        /// </summary>
        protected List<Symbol> symbols = new List<Symbol>();

        /// <summary>
        /// 编译链接工程
        /// </summary>
        /// <param name="rootFolder"></param>
        /// <returns>是否成功</returns>
        public abstract bool BuildProject(string rootFolder);

        /// <summary>
        /// 编译文件
        /// </summary>
        /// <param name="filename"></param>
        /// <returns>是否成功</returns>
        public abstract bool Compile(string filename);

        /// <summary>
        /// 链接
        /// </summary>
        /// <returns>是否成功</returns>
        public abstract bool Link();

        /// <summary>
        /// 链接
        /// </summary>
        /// <param name="instructions">指令集合</param>
        /// <param name="symbols">符号表</param>
        /// <returns>是否成功</returns>
        public abstract bool Link(List<Symbol> instructions, List<Symbol> symbols);

        /// <summary>
        /// 清理
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public virtual void Clean()
        {
            instructions.Clear();
            symbols.Clear();
        }

        /// <summary>
        /// 获取指令集合
        /// </summary>
        /// <returns>指令集合</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public virtual List<Symbol> GetInstructions()
        {
            return instructions;
        }

        /// <summary>
        /// 获取符号表
        /// </summary>
        /// <returns>符号表</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public virtual List<Symbol> GetSymbols()
        {
            return symbols;
        }
    }
}
