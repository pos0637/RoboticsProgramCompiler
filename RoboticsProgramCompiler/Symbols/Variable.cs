namespace RoboticsProgramCompiler.Symbols
{
    /// <summary>
    /// 变量
    /// </summary>
    public class Variable : Symbol
    {
        /// <summary>
        /// 值
        /// </summary>
        public object Value { get; set; }

        public Variable()
        {
            Type = SymbolType.Variable;
        }

        public override string ToString()
        {
            return $"{GetType().Name}({Newtonsoft.Json.JsonConvert.SerializeObject(this)})";
        }
    }
}
