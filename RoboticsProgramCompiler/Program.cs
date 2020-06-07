using RoboticsProgramCompiler.Compilers;
using RoboticsProgramCompiler.Executors;

namespace RoboticsProgramCompiler
{
    class Program
    {
        static void Main(string[] args)
        {
            var compiler = new KUKACompiler();
            var assembly = compiler.BuildProject("C:/Users/etrit/Desktop/Type");

            var executor = new KUKAExecutor();
            executor.Execute(assembly, compiler.FindSymbol(assembly, new Symbols.Reference() { Name = "Fstep1" })?.Name);
        }
    }
}
