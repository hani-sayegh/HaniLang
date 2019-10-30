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
                var input = Console.ReadLine();
                //var input = "1 + 1 * 7 + 1";


                var syntaxTree = SyntaxTree.Parse(input);
                var binder = new Binder();
                var boundTree = binder.BindExpression(syntaxTree.Root);



                Console.WriteLine();

                Console.WriteLine(syntaxTree.Root.Tree());
                Console.WriteLine(boundTree.Evaluate());
                Console.ForegroundColor = ConsoleColor.Red;
                foreach(var error in syntaxTree.Diagnostics.Concat(binder.Diagnostics))
                    Console.WriteLine(error);
                Console.ResetColor();

                Console.Write("> ");
            }
        }
    }
}