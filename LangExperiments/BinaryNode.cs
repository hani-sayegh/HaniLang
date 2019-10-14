using System.Collections.Generic;

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

        public override string ToString()
        {
            return "frew";
        }
    }
}