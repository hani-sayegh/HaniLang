using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangExperiments.Nodes
{
    class LiteralNode : ISyntaxNode
    {
        public LiteralNode(SyntaxNode syntaxNode)
        {
            SyntaxNode = syntaxNode;
        }

        public object Value => SyntaxNode.Value;
        public SyntaxKind Kind => SyntaxKind.LiteralExpression;

        public SyntaxNode SyntaxNode { get; }

        public IEnumerable<ISyntaxNode> Children()
        {
            return Enumerable.Empty<ISyntaxNode>();
        }

        public override string ToString()
        {
            return SyntaxNode.ToString();
        }

        public int Evaluate()
        {
            return (int)SyntaxNode.Value;
        }
    }
}
