namespace RoboticsProgramCompiler.Symbols
{
    /// <summary>
    /// 符号
    /// </summary>
    public abstract class Symbol
    {
        /// <summary>
        /// 符号类型
        /// </summary>
        public enum SymbolType
        {
            /// <summary>
            /// 标签
            /// </summary>
            Label,

            /// <summary>
            /// 调用
            /// </summary>
            Call,

            /// <summary>
            /// 返回
            /// </summary>
            Return,

            /// <summary>
            /// 指令
            /// </summary>
            Instruction,

            /// <summary>
            /// 变量
            /// </summary>
            Variable
        }

        /// <summary>
        /// 作用域
        /// </summary>
        public enum ScopeType
        {
            /// <summary>
            /// 局部
            /// </summary>
            Local,

            /// <summary>
            /// 模块
            /// </summary>
            Module,

            /// <summary>
            /// 全局
            /// </summary>
            Global
        }

        /// <summary>
        /// 命名空间
        /// </summary>
        public string Namespace { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public SymbolType Type { get; set; }

        /// <summary>
        /// 作用域
        /// </summary>
        public ScopeType Scope { get; set; }

        /// <summary>
        /// 文件
        /// </summary>
        public string File { get; set; }

        /// <summary>
        /// 行号
        /// </summary>
        public int Line { get; set; }

        /// <summary>
        /// 列号
        /// </summary>
        public int Column { get; set; }

        /// <summary>
        /// 文本
        /// </summary>
        public string Text { get; set; }
    }
}
