using System;
using System.Linq;
namespace LangExperiments
{
    class Program
    {
        //problems encounter readline spawning mysterious thread
        static void Main(string[] args)
        {
            Console.Write("> ");


            while (true)
            {
                //var input = Console.ReadLine();
                var input = "1 + 3 * 9";

                var parser = new Parser(input);

                var syntaxTree = parser.Parse();

                Console.WriteLine();

                Console.WriteLine(syntaxTree.Root.Tree());
                Console.WriteLine(syntaxTree.Root.Evaluate());
                Console.ForegroundColor = ConsoleColor.Red;

                Console.Write("> ");
            }
        }
    }
}