using RoboticsProgramCompiler.Compilers;
using System.Collections.Generic;

namespace RoboticsProgramCompiler.Executors
{
    /// <summary>
    /// 执行器
    /// </summary>
    public abstract class Executor
    {
        /// <summary>
        /// 当前程序地址
        /// </summary>
        protected int ip;

        /// <summary>
        /// 调用栈
        /// </summary>
        protected List<Stack> stacks = new List<Stack>();

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="assembly">程序集</param>
        /// <param name="symbolName">符号名称</param>
        /// <returns>执行结果</returns>
        public abstract object Execute(Assembly assembly, string symbolName);

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="assembly">程序集</param>
        /// <param name="entry">入口程序地址</param>
        /// <returns>执行结果</returns>
        public abstract object Execute(Assembly assembly, int entry);

        /// <summary>
        /// 函数调用
        /// </summary>
        /// <param name="ip">函数地址</param>
        public abstract void Call(int ip);

        /// <summary>
        /// 函数返回
        /// </summary>
        public abstract void Return();
    }
}
