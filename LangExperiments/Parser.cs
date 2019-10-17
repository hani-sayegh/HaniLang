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
    }
    class Parser
    {
        private List<SyntaxNode> _tokens = new List<SyntaxNode>();
        private SyntaxNode Current => _tokens[_position == _tokens.Count ? _tokens.Count - 1 : _position];
        private int _position = 0;
        private List<string> _diagnostics = new List<string>();
        public Parser(string text)
        {
            var lexer = new Lexer(text);
            var token = lexer.NextToken();

            while (!token.EndOfString)
            {
                if (token.Kind != SyntaxKind.WhiteSpace)
                {
                    _tokens.Add(token);
                }
                token = lexer.NextToken();
            }
            _tokens.Add(token);
        }

        public SyntaxTree Parse()
        {
            var parsedExpression = ParseTerm();
            Match(SyntaxKind.EndOfString);
            return new SyntaxTree(parsedExpression, _diagnostics);
        }

        ISyntaxNode ParseTerm()
        {
            var left = ParseFactor();

            while (Current.Term)
            {
                var @operator = NextToken();
                var right = ParseFactor();
                left = new BinaryNode(left, @operator, right);
            }
            return left;
        }
        ISyntaxNode ParseFactor()
        {
            var left = ParsePrimaryExpression();

            while (Current.Factor)
            {
                var @operator = NextToken();
                var right = ParsePrimaryExpression();
                left = new BinaryNode(left, @operator, right);
            }
            return left;
        }

        public SyntaxNode NextToken()
        {
            var tmp = Current;
            ++_position;
            return tmp;
        }

        public ISyntaxNode Match(SyntaxKind kind)
        {
            if (Current.Kind == kind)
                return NextToken();
            _diagnostics.Add($"'{Current.Kind}' does not match expected kind '{kind}'");
            return SyntaxNode.Unrecognized;
        }

        public ISyntaxNode ParsePrimaryExpression()
        {
            if (Current.Kind == SyntaxKind.Number)
            {
                return NextToken();
            }
            throw new System.Exception("werw");
        }
    }
}