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
                var input = "1 + 3 + 4 + 9 * 10";

                var parser = new Parser(input);

                var expression = parser.Parse();

                Console.WriteLine();

                Console.WriteLine(expression.Tree());

                Console.Write("> ");
            }

            void Print(ISyntaxNode node,
                string indent = "",
                string childS = "")
            {
                Console.WriteLine(childS +  node);
                var treeString = "│  ";
                var childString = "├──";
                var finalChild = "└──";

                var last = node.Children().LastOrDefault();
                foreach (var child in node.Children())
                {
                    Print(child, treeString + indent, child == last ? indent + finalChild : indent + childString);
                }
            }
        }
    }
}