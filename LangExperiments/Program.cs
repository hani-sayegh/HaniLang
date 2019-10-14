using System;

namespace LangExperiments
{

    public class ToStringAttribute : Attribute
        {

        }

    partial class Program
    {
        //problems encounter readline spawning mysterious thread
        static void Main(string[] args)
        {
            Console.Write("> ");


            while (true)
            {
                var input = Console.ReadLine();

                var parser = new Parser(input);

                var tree = parser.Parse();

                Console.Write("> ");
            }

        }
    }
}