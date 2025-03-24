using Recipe.Service;

namespace Recipe
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            ProgramService program = new ProgramService();
            program.start();
        }
    }
}
