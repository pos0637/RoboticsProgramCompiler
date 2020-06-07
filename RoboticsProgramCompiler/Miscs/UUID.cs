namespace RoboticsProgramCompiler.Miscs
{
    /// <summary>
    /// UUID生成器
    /// </summary>
    public sealed class UUID
    {
        /// <summary>
        /// 生成UUID
        /// </summary>
        /// <param name="file">文件名称</param>
        /// <returns>UUID</returns>
        public static string Generate(string file)
        {
            return file + System.Guid.NewGuid().ToString("N");
        }

        /// <summary>
        /// 生成UUID
        /// </summary>
        /// <param name="file">文件名称</param>
        /// <param name="symbol">符号</param>
        /// <returns>UUID</returns>
        public static string Generate(string file, string symbol)
        {
            return $"{file}/{symbol}";
        }
    }
}
