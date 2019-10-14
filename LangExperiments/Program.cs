using System;

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
                var input = "1 + 3 + 4";

                var parser = new Parser(input);

                var expression = parser.Parse();

                Console.WriteLine();

                Print(expression);

                Console.Write("> ");
            }

            void Print(ISyntaxNode node, string indent = "")
            {
                Console.WriteLine(indent + node.Kind);
                foreach(var child in node.Children())
                {
                    Print(child, indent + "   ");
                }
            }
        }
    }
}