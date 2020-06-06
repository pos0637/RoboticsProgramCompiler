using System.Collections.Generic;

namespace RoboticsProgramCompiler.Symbols
{
    /// <summary>
    /// 解析器
    /// </summary>
    interface IParser
    {
        /// <summary>
        /// 解析
        /// <param name="arguments">参数列表</param>
        /// <returns>符号列表</returns>
        Symbol[] Parse(Dictionary<string, object> arguments);
    }
}
