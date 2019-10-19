using System.Collections.Generic;

namespace LangExperiments
{
    class SyntaxTree
    {
        public ISyntaxNode Root { get; }
        public IReadOnlyList<string> Diagnostics { get; }
        public SyntaxTree(ISyntaxNode root, IReadOnlyList<string> diagnostics)
        {
            Root = root;
            Diagnostics = diagnostics;
        }
        public static SyntaxTree Parse(string text)
        {
            var parser = new Parser(text);
            return parser.Parse();
        }
    }
}