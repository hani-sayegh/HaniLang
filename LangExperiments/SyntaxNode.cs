using System.Collections.Generic;
using System.Linq;

namespace LangExperiments
{
    enum SyntaxKind
    {
        Plus = 1,
        Minus = 2,
        WhiteSpace = 4,
        Number = 8,
        EndOfString = 16,
        Unrecognized = 32,
        BinaryNode = 64
    }
    class SyntaxNode : ISyntaxNode
    {
        public static SyntaxNode Unrecognized { get; } = new SyntaxNode(SyntaxKind.Unrecognized, -1, null);

        public bool EndOfString => Kind == SyntaxKind.EndOfString;
        public SyntaxNode(SyntaxKind syntaxKind, int position, string text)
        {
            Kind = syntaxKind;
            Position = position;
            Text = text;
            var mask = (SyntaxKind.Plus | SyntaxKind.Minus);
            Operator = (mask & Kind) != 0;
        }

        [ToStringAttribute]
        public SyntaxKind Kind { get; }
        public int Position { get; }
        public string Text { get; }

        public bool Number => Kind == SyntaxKind.Number;
        public readonly bool Operator;

        public IEnumerable<ISyntaxNode> Children()
        {
            return Enumerable.Empty<SyntaxNode>();
        }
    }
}