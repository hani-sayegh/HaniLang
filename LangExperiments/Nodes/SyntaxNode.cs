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
        falseKeyword,
        TrueKeyword,
        undefinedKeyword,
        NumberToken,
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
        public static SyntaxNode Unrecognized { get; } = new SyntaxNode(SyntaxKind.Unrecognized, -1, "", null);

        public bool EndOfString => Kind == SyntaxKind.EndOfString;
        public SyntaxNode(SyntaxKind syntaxKind, int position, string text, object value = null, bool fabricated = false)
        {
            Kind = syntaxKind;
            Position = position;
            Text = text;
            Fabricated = fabricated;
            Value = value;
            Factor = Kind == SyntaxKind.MultiplyToken || Kind == SyntaxKind.DivideToken;
            Term = Kind == SyntaxKind.Plus || Kind == SyntaxKind.MinusToken;
        }

        public SyntaxKind Kind { get; }
        public int Position { get; }
        public string Text { get; }
        public bool Fabricated { get; }
        public object Value { get; }

        public readonly bool Term;
        public readonly bool Factor;


        public override string ToString()
        {
            return (Fabricated ? "Fabricated: " : "") + Kind + " " + Text;
        }
        public IEnumerable<ISyntaxNode> Children()
        {
            return Enumerable.Empty<ISyntaxNode>();
        }

        public int Evaluate()
        {
            return int.Parse(Text);
        }
    }
}