using RoboticsProgramCompiler.Compilers;
using RoboticsProgramCompiler.Symbols;
using System.Linq;

namespace RoboticsProgramCompiler.Executors
{
    /// <summary>
    /// KUKA程序集执行器
    /// </summary>
    public class KUKAExecutor : Executor
    {
        /// <summary>
        /// 退出标志
        /// </summary>
        private bool exitFlag;

        public override void Call(int ip)
        {
            stacks.Add(new Stack() { returnIp = this.ip });
            this.ip = ip;
        }

        public override object Execute(Assembly assembly, string symbolName)
        {
            if (!assembly.symbols.ContainsKey(symbolName)) {
                return false;
            }

            var symbol = assembly.symbols[symbolName];
            if (!symbol.GetType().IsSubclassOf(typeof(Instruction))) {
                return false;
            }

            return Execute(assembly, (symbol as Instruction).Address);
        }

        public override object Execute(Assembly assembly, int entry)
        {
            exitFlag = false;
            ip = entry;

            while ((!exitFlag) && (ip < assembly.instructions.Count)) {
                (assembly.instructions[ip] as Instruction).Execute(this);
                ip++;
            }

            return true;
        }

        public override void Return()
        {
            if (stacks.Count == 0) {
                exitFlag = true;
                return;
            }

            var stack = stacks.Last();
            ip = stack.returnIp;
            stacks.Remove(stack);
        }
    }
}
