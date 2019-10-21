using System.Collections.Generic;

namespace LangExperiments
{
    class ParanNode : ISyntaxNode
    {
        public override string ToString()
        {
            return nameof(ParanNode);
        }
        public SyntaxKind Kind => SyntaxKind.ParanExpression;

        public SyntaxNode OpenP { get; }
        public ISyntaxNode Expression { get; }
        public ISyntaxNode SyntaxNode { get; }
        public SyntaxNode CloseP { get; }

        public ParanNode(SyntaxNode openP, ISyntaxNode expression, SyntaxNode closeP)
        {
            OpenP = openP;
            Expression = expression;
            CloseP = closeP;
        }
        public IEnumerable<ISyntaxNode> Children()
        {
            yield return OpenP;
            yield return Expression;
            yield return CloseP;
        }

        public int Evaluate()
        {
            return Expression.Evaluate();
        }
    }
}