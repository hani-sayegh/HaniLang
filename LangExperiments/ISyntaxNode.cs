using System.Collections.Generic;

namespace LangExperiments
{
    interface ISyntaxNode
    {
        IEnumerable<ISyntaxNode> Children();
        SyntaxKind Kind {get; }
    }
}