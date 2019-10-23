using System.Collections.Generic;
using System.Linq;

namespace LangExperiments
{
    enum SyntaxKind
    {
        MultiplyToken,
        MinusToken,
        WhiteSpace,
        EndOfString,
        Unrecognized,
        Plus,
        DivideToken,
        CloseP,
        OpenP,

        //expressions
        ParanExpression,
        UnaryExpression,
        LiteralExpression,
        BinaryNode,
    }

    class NumberNode : ISyntaxNode
    {
        public NumberNode()
        {

        }

        public SyntaxKind Kind => throw new System.NotImplementedException();

        public IEnumerable<ISyntaxNode> Children()
        {
            throw new System.NotImplementedException();
        }

        public int Evaluate()
        {
            throw new System.NotImplementedException();
        }
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
            Factor = Kind == SyntaxKind.MultiplyToken || Kind == SyntaxKind.DivideToken;
            Term = Kind == SyntaxKind.Plus || Kind == SyntaxKind.MinusToken;
        }

        public SyntaxKind Kind { get; }
        public int Position { get; }
        public string Text { get; }

        public bool Number => Kind == SyntaxKind.LiteralExpression;
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