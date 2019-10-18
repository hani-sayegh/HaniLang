using System.Collections.Generic;
using System.Linq;

namespace LangExperiments
{
    enum SyntaxKind
    {
        Multiply = 1,
        Minus = 2,
        WhiteSpace = 4,
        Number = 8,
        EndOfString = 16,
        Unrecognized = 32,
        BinaryNode = 64,
        Plus = 128,
        Divide = 256
    }
    class SyntaxNode : ISyntaxNode
    {
        public static SyntaxNode Unrecognized { get; } = new SyntaxNode(SyntaxKind.Unrecognized, -1, "");

        public bool EndOfString => Kind == SyntaxKind.EndOfString;
        public SyntaxNode(SyntaxKind syntaxKind, int position, string text)
        {
            Kind = syntaxKind;
            Position = position;
            Text = text;
            Factor = ((SyntaxKind.Multiply | SyntaxKind.Divide) & Kind) != 0;
            Term = ((SyntaxKind.Minus | SyntaxKind.Plus) & Kind) != 0;
        }

        public SyntaxKind Kind { get; }
        public int Position { get; }
        public string Text { get; }

        public bool Number => Kind == SyntaxKind.Number;
        public readonly bool Term;
        public readonly bool Factor;


        public override string ToString()
        {
            return Kind + " " + Text;
        }
        public IEnumerable<ISyntaxNode> Children()
        {
            return Enumerable.Empty<SyntaxNode>();
        }

        public int Evaluate()
        {
            return int.Parse(Text);
        }
    }
}