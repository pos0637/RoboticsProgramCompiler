using System.Collections.Generic;

namespace RoboticsProgramCompiler.Compilers
{
    /// <summary>
    /// 编译器接口
    /// </summary>
    public abstract class Compiler
    {
        /// <summary>
        /// 编译工程
        /// </summary>
        /// <param name="rootFolder">根目录</param>
        /// <returns>程序集</returns>
        public abstract Assembly BuildProject(string rootFolder);

        /// <summary>
        /// 编译文件
        /// </summary>
        /// <param name="filename"></param>
        /// <returns>程序集</returns>
        public abstract Assembly Compile(string filename);

        /// <summary>
        /// 链接
        /// </summary>
        /// <param name="assemblies">程序集列表</param>
        /// <returns>程序集</returns>
        public abstract Assembly Link(List<Assembly> assemblies);

        /// <summary>
        /// 链接
        /// </summary>
        /// <param name="source">程序集</param>
        /// <param name="assemblies">程序集列表</param>
        /// <returns>程序集</returns>
        public abstract Assembly Link(Assembly source, List<Assembly> assemblies);

        /// <summary>
        /// 清理
        /// </summary>
        public abstract void Clean();
    }
}
