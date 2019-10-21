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
                var input = "1 + 3 + 1 + 8";


                var syntaxTree = SyntaxTree.Parse(input);

                Console.WriteLine();

                Console.WriteLine(syntaxTree.Root.Tree());
                Console.WriteLine(syntaxTree.Root.Evaluate());
                Console.ForegroundColor = ConsoleColor.Red;
                foreach(var error in syntaxTree.Diagnostics)
                    Console.WriteLine(error);

                Console.Write("> ");
            }
        }
    }
}