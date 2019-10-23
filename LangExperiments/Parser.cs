using System.Collections.Generic;

namespace LangExperiments
{
    class Parser
    {
        private List<SyntaxNode> _tokens = new List<SyntaxNode>();
        private SyntaxNode Current => _tokens[_position == _tokens.Count ? _tokens.Count - 1 : _position];
        private int _position = 0;
        private List<string> _diagnostics = new List<string>();
        public Parser(string text)
        {
            var lexer = new Lexer(text);
            var token = lexer.Lex();

            while (!token.EndOfString)
            {
                if (token.Kind != SyntaxKind.WhiteSpace)
                {
                    _tokens.Add(token);
                }
                token = lexer.Lex();
            }
            _tokens.Add(token);
            _diagnostics.AddRange(lexer.Diagnostics);
        }

        public SyntaxTree Parse()
        {
            var parsedExpression = ParseExpression();
            Match(SyntaxKind.EndOfString);
            return new SyntaxTree(parsedExpression, _diagnostics);
        }

        ISyntaxNode ParseExpression(int parentPrecedence = 0)
        {
            ISyntaxNode left = null;
            var unaryOperatorPrecedence = GetUnaryOpoeratorPrecedence();
            if (unaryOperatorPrecedence != -1)
            {
                left = new UnaryNode(NextToken(), ParseExpression(unaryOperatorPrecedence));
            }
            else
                left = ParsePrimaryExpression();

            while (true)
            {
                var precdence = GetOperatorPrecedence();
                if(precdence == -1 || parentPrecedence > precdence)
                {
                    break;
                }

                var @operator = NextToken();
                var right = ParseExpression(precdence);

                left = new BinaryNode(left, @operator, right);
            }
            return left;
        }

        int GetOperatorPrecedence()
        {
            if (Current.Factor)
                return 2;
            else if (Current.Term)
                return 1;
            return -1;
        }
        int GetUnaryOpoeratorPrecedence()
        {
            if (Current.Term)
                return 3;
            return -1;
        }

        public SyntaxNode NextToken()
        {
            var tmp = Current;
            ++_position;
            return tmp;
        }

        public SyntaxNode Match(SyntaxKind kind)
        {
            if (Current.Kind == kind)
                return NextToken();

            _diagnostics.Add($"ERROR: '{Current.Kind}' does not match expected kind '{kind}'");
            //fabricate token to continue analyzing code and avoid dealing with null
            return new SyntaxNode(kind, Current.Position, null);
        }

        //Primary expressions: number, parantheses
        public ISyntaxNode ParsePrimaryExpression()
        {
            if (Current.Kind == SyntaxKind.OpenP)
            {
                var openP = NextToken();
                var expression = ParseExpression();
                var closeP = Match(SyntaxKind.CloseP);
                return new ParanNode(openP, expression, closeP);
            }

            var numberToken = Match(SyntaxKind.LiteralExpression);
            return numberToken;
        }
    }
}