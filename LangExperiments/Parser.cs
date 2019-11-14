using LangExperiments.Nodes;
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
                left = new UnaryNode(ConsumeToken(), ParseExpression(unaryOperatorPrecedence));
            }
            else
                left = ConsumePrimaryExpression();

            while (true)
            {
                var precdence = GetBinaryOperatorPrecedence();
                if(precdence == -1 || parentPrecedence > precdence)
                {
                    break;
                }

                var @operator = ConsumeToken();
                var right = ParseExpression(precdence);

                left = new BinaryNode(left, @operator, right);
            }
            return left;
        }

        int GetBinaryOperatorPrecedence()
        {
            if (Current.Factor)
                return 4;
            else if (Current.Term)
                return 3;
            else if (Current.Kind == SyntaxKind.LogicalEqual || Current.Kind == SyntaxKind.NotEqual)
                return 2;
            else if (Current.Kind == SyntaxKind.LogicalAnd || Current.Kind == SyntaxKind.LogicalOr)
                return 1;
            return -1;
        }
        int GetUnaryOpoeratorPrecedence()
        {
            if (Current.Term)
                return 3;
            if (Current.Kind == SyntaxKind.Not)
                return 3;
            return -1;
        }

        public SyntaxNode ConsumeToken()
        {
            var tmp = Current;
            ++_position;
            return tmp;
        }

        public SyntaxNode Match(SyntaxKind kind)
        {
            if (Current.Kind == kind)
                return ConsumeToken();

            _diagnostics.Add($"ERROR: '{Current.Kind}' does not match expected kind '{kind}'");
            //fabricate token to continue analyzing code and avoid dealing with null
            return new SyntaxNode(kind, Current.Position,null, null, true);
        }

        //Primary expressions: number, parantheses, bool
        public ISyntaxNode ConsumePrimaryExpression()
        {
            if (Current.Kind == SyntaxKind.OpenP)
            {
                var openP = ConsumeToken();
                var expression = ParseExpression();
                var closeP = Match(SyntaxKind.CloseP);
                return new ParanNode(openP, expression, closeP);
            }
            else if(Current.Kind == SyntaxKind.falseKeyword
                    || Current.Kind == SyntaxKind.TrueKeyword)
            {
                return new LiteralNode(ConsumeToken());
            }

            var numberToken = Match(SyntaxKind.NumberToken);
            return new LiteralNode(numberToken);
        }
    }
}