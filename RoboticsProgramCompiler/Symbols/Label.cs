namespace RoboticsProgramCompiler.Symbols
{
    /// <summary>
    /// 标签
    /// </summary>
    public abstract class Label : Instruction
    {
        public Label()
        {
            Type = SymbolType.Label;
        }
    }
}
