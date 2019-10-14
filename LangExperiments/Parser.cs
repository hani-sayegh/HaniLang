using System.Collections.Generic;

namespace LangExperiments
{
    class Parser
    {
        private List<SyntaxToken> _tokens = new List<SyntaxToken>();
        private SyntaxToken Current => _tokens[_position == _tokens.Count ? _tokens.Count - 1: _position];
        private int _position = 0;
        public Parser(string text)
        {
            var lexer = new Lexer(text);
            var token = lexer.NextToken();

            while (!token.EndOfString)
            {
                if (token.SyntaxKind != SyntaxKind.WhiteSpace)
                {
                    _tokens.Add(token);
                }
                    token = lexer.NextToken();
            }

        }

        public ISyntaxNode Parse()
        {
            var left = ParsePrimaryExpression();

            while(Current.Operator)
            {
                var opera = NextToken();
                var right = ParsePrimaryExpression();
                left = new BinaryNode(left, opera, right);
            }

            return left;
        }

        public SyntaxToken NextToken()
        {
            var tmp = Current;
            ++_position;
            return tmp;
        }

        public ISyntaxNode ParsePrimaryExpression()
        {
            if (Current.SyntaxKind == SyntaxKind.Number)
            {
                return NextToken();
            }
            throw new System.Exception("werw");
        }
    }
}