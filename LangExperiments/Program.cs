using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LangExperiments
{

        public class ToStringAttribute : Attribute
        {

        }
        static class Extensions
        {
public static string ToProperties(this object o)
            {
                var sb = new StringBuilder();
            var type = o.GetType();
                foreach (var prop in o.GetType().GetProperties())
                {
                    foreach (var att in prop.GetCustomAttributes(false))
                    {
                        if (att is ToStringAttribute)
                        {
                            sb.Append($"{prop.Name}: {prop.GetValue(o)}");
                            break;
                        }
                    }
                }
                
                return sb.ToString();
            }
        }
    class Program
    {
        //problems encounter readline spawning mysterious thread
        static void Main(string[] args)
        {
            Console.Write("> ");
            while (true)
            {
                var input = Console.ReadLine();

                var lexer = new Lexer(input);

                while (true)
                {
                    var nextToekn = lexer.NextToken();
                    if (nextToekn.EndOfString)
                        break;
                    Console.WriteLine(nextToekn.ToProperties());
                }

                Console.Write("> ");
            }

        }
        class Parser
        {
            private List<SyntaxToken> _tokens;
            public Parser(string text)
            {
                var lexer = new Lexer(text);
                while()
                {

                }
            }
        }

        enum SyntaxKind
        {
            Plus,
            WhiteSpace,
            Number,
            EndOfString,
            Unrecognized
        }

        class SyntaxToken : ISyntaxNode
        {
            public static SyntaxToken Unrecognized { get; } = new SyntaxToken(SyntaxKind.Unrecognized, -1, null);

            public bool EndOfString => SyntaxKind == SyntaxKind.EndOfString;
            public SyntaxToken(SyntaxKind syntaxKind, int position, string text)
            {
                SyntaxKind = syntaxKind;
                Position = position;
                Text = text;
            }

            [ToStringAttribute]
            public SyntaxKind SyntaxKind { get; }
            public int Position { get; }
            public string Text { get; }

        }

        interface ISyntaxNode
        {
        }

        class BinaryNode : ISyntaxNode
        {
            public BinaryNode(ISyntaxNode left, SyntaxToken binaryOperator, ISyntaxNode right)
            {
                Left = left;
                BinaryOperator = binaryOperator;
                Right = right;
            }

            public ISyntaxNode Left { get; }
            public SyntaxToken BinaryOperator { get; }
            public ISyntaxNode Right { get; }
        }

        class Lexer
        {
            string _text;
            int _position = 0;
            public char Current => _position == _text.Length ? '\0' : _text[_position];

            public Lexer(string text)
            {
                _text = text;
            }

            public SyntaxToken NextToken()
            {
                if (Current == '\0')
                    return new SyntaxToken(SyntaxKind.EndOfString, _text.Length, "\0");

                var start = _position;

                if (char.IsWhiteSpace(Current))
                {
                    while (char.IsWhiteSpace(Current))
                    {
                        ++_position;
                    }
                    return
                        new SyntaxToken(SyntaxKind.WhiteSpace, start, _text.Substring(start, _position - start));
                }

                if (char.IsDigit(Current))
                {
                    while (char.IsDigit(Current))
                    {
                        ++_position;
                    }
                    return
                        new SyntaxToken(SyntaxKind.Number, start, _text.Substring(start, _position - start));
                }

                if (Current == '+')
                {
                    ++_position;
                    return
                        new SyntaxToken(SyntaxKind.Plus, start, _text.Substring(start, _position - start));
                }

                return SyntaxToken.Unrecognized;
            }
        }
    }
}