using System.Collections.Generic;

namespace LangExperiments
{
    class UnaryNode : ISyntaxNode
    {
        public UnaryNode(SyntaxNode operatorToken, ISyntaxNode expression)
        {
            OperatorToken = operatorToken;
            Expression = expression;
        }
        public SyntaxKind Kind => SyntaxKind.UnaryExpression;

        public SyntaxNode OperatorToken { get; }
        public ISyntaxNode Expression { get; }

        public IEnumerable<ISyntaxNode> Children()
        {
            yield return OperatorToken;
            yield return Expression;
        }

        public int Evaluate()
        {
            if (OperatorToken.Kind == SyntaxKind.Plus)
                return +Expression.Evaluate();
            else if (OperatorToken.Kind == SyntaxKind.MinusToken)
                return -Expression.Evaluate();

            throw new System.Exception("Unrecognized operator: " + OperatorToken.Kind);
        }
    }
}