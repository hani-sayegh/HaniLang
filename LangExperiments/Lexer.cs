namespace LangExperiments
{

        class Lexer
        {
            string _text;
            int _position = 0;
            public char Current => _position == _text.Length ? '\0' : _text[_position];

            public Lexer(string text)
            {
                _text = text;
            }

            public SyntaxToken NextToken()
            {
                if (Current == '\0')
                    return new SyntaxToken(SyntaxKind.EndOfString, _text.Length, "\0");

                var start = _position;

                if (char.IsWhiteSpace(Current))
                {
                    while (char.IsWhiteSpace(Current))
                    {
                        ++_position;
                    }
                    return
                        new SyntaxToken(SyntaxKind.WhiteSpace, start, _text.Substring(start, _position - start));
                }

                if (char.IsDigit(Current))
                {
                    while (char.IsDigit(Current))
                    {
                        ++_position;
                    }
                    return
                        new SyntaxToken(SyntaxKind.Number, start, _text.Substring(start, _position - start));
                }

                if (Current == '+')
                {
                    ++_position;
                    return
                        new SyntaxToken(SyntaxKind.Plus, start, _text.Substring(start, _position - start));
                }

                return SyntaxToken.Unrecognized;
            }
        }
    }