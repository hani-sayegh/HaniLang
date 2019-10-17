using System.Collections.Generic;
using System.Text;

namespace LangExperiments
{
    interface ISyntaxNode
    {
        IEnumerable<ISyntaxNode> Children();
        SyntaxKind Kind {get; }

        int Evaluate();
    }
}