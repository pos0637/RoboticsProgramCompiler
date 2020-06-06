using System;

namespace RoboticsProgramCompiler.Miscs
{
    /// <summary>
    /// 日志
    /// </summary>
    public sealed class Tracker
    {
        /// <summary>
        /// 调试日志
        /// </summary>
        /// <param name="content">内容</param>
        public static void LogD(string content)
        {
            Console.WriteLine(content);
        }

        /// <summary>
        /// 信息日志
        /// </summary>
        /// <param name="content">内容</param>
        public static void LogI(string content)
        {
            Console.WriteLine(content);
        }

        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="e">异常</param>
        public static void LogE(Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }
}
