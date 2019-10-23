using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LangExperiments
{
    class BinaryNode : ISyntaxNode
    {
        public BinaryNode(ISyntaxNode left, SyntaxNode binaryOperator, ISyntaxNode right)
        {
            Left = left;
            BinaryOperator = binaryOperator;
            Right = right;
        }

        public ISyntaxNode Left { get; }
        public SyntaxNode BinaryOperator { get; }
        public ISyntaxNode Right { get; }

        public SyntaxKind Kind => SyntaxKind.BinaryNode;

        public IEnumerable<ISyntaxNode> Children()
        {
            yield return Left;
            yield return BinaryOperator;
            yield return Right;
        }

        public int Evaluate() => BinaryOperator.Kind switch
        {
            SyntaxKind.Plus => Left.Evaluate() + Right.Evaluate(),
            SyntaxKind.MinusToken => Left.Evaluate() - Right.Evaluate(),
            SyntaxKind.MultiplyToken => Left.Evaluate() * Right.Evaluate(),
            SyntaxKind.DivideToken => Left.Evaluate() / Right.Evaluate(),
            _ => throw new System.Exception("Could not evalute")
        };
             
        public override string ToString()
        {
            return nameof(BinaryNode);
        }
    }
}