using RoboticsProgramCompiler.Compilers;

namespace RoboticsProgramCompiler
{
    class Program
    {
        static void Main(string[] args)
        {
            var compiler = new KUKACompiler();
            compiler.BuildProject("C:/Users/etrit/Desktop/Type");
        }
    }
}
