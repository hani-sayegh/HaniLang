using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangExperiments.Nodes
{
    class IdentifierNode : ISyntaxNode
    {
        public IdentifierNode(SyntaxNode identifier, ISyntaxNode expression)
        {
            Identifier = identifier;
            Expression = expression;
        }

        public SyntaxKind Kind => SyntaxKind.Identifier;

        public SyntaxNode Identifier { get; }
        public ISyntaxNode Expression { get; }

        public IEnumerable<ISyntaxNode> Children()
        {
            yield return Expression;
        }

        public override string ToString()
        {
            return nameof(IdentifierNode) + " " + Identifier.Text;
        }

        public int Evaluate()
        {
            return Expression.Evaluate();
        }
    }
}
