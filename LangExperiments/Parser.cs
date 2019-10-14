using System.Collections.Generic;

namespace LangExperiments
{
    class Parser
    {
        private List<SyntaxNode> _tokens = new List<SyntaxNode>();
        private SyntaxNode Current => _tokens[_position == _tokens.Count ? _tokens.Count - 1: _position];
        private int _position = 0;
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

        }

        public ISyntaxNode Parse()
        {
            var left = ParsePrimaryExpression();

            while(Current.Operator)
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