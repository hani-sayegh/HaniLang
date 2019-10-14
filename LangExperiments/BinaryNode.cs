namespace LangExperiments
{
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
}