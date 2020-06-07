using RoboticsProgramCompiler.Executors;

namespace RoboticsProgramCompiler.Symbols
{
    /// <summary>
    /// 执行器
    /// </summary>
    interface IExecutable
    {
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="executor">执行器</param>
        /// <returns>结果</returns>
        object Execute(Executor executor);
    }
}
