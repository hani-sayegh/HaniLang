﻿using System.Collections.Generic;

namespace LangExperiments
{
    class Lexer
    {
        string _text;
        int _position = 0;
        public char Current => _position == _text.Length ? '\0' : _text[_position];
        private readonly List<string> _diagnostics = new List<string>();
        public List<string> Diagnostics => _diagnostics;

        public Lexer(string text)
        {
            _text = text;
        }

        SyntaxKind KeyWordKind(string keyword) => keyword switch
        {
            "false" => SyntaxKind.falseKeyword,
            "true" => SyntaxKind.TrueKeyword,
            _ => SyntaxKind.undefinedKeyword
        };

        public SyntaxNode Lex()
        {
            if (Current == '\0')
                return new SyntaxNode(SyntaxKind.EndOfString, _text.Length, "\0");

            var start = _position;

            if (char.IsWhiteSpace(Current))
            {
                while (char.IsWhiteSpace(Current))
                {
                    ++_position;
                }
                return
                    new SyntaxNode(SyntaxKind.WhiteSpace, start, _text.Substring(start, _position - start));
            }

            if(char.IsLetter(Current))
            {
                while (char.IsLetter(Current))
                {
                    ++_position;
                }
                var keyword = _text.Substring(start, _position - start);
                var keywordVal = KeyWordKind(keyword) == SyntaxKind.TrueKeyword;

                return
                    new SyntaxNode(KeyWordKind(keyword), start, keyword, keywordVal);
            }

            if (char.IsDigit(Current))
            {
                while (char.IsDigit(Current))
                {
                    ++_position;
                }
                int integer;
                var text = _text.Substring(start, _position - start);
                var parsed = int.TryParse(text, out integer);
                if (!parsed)
                    _diagnostics.Add($"Could not represet {text} with 32 bit int");

                return
                    new SyntaxNode(SyntaxKind.NumberToken, start, _text.Substring(start, _position - start), parsed ? (int ?)integer: null);
            }

            if (Current == '+')
            {
                ++_position;
                return
                    new SyntaxNode(SyntaxKind.Plus, start, _text.Substring(start, _position - start));
            }

            if (Current == '*')
            {
                ++_position;
                return
                    new SyntaxNode(SyntaxKind.MultiplyToken, start, _text.Substring(start, _position - start));
            }

            if (Current == '-')
            {
                ++_position;
                return
                    new SyntaxNode(SyntaxKind.MinusToken, start, _text.Substring(start, _position - start));
            }

            if (Current == '/')
            {
                ++_position;
                return
                    new SyntaxNode(SyntaxKind.DivideToken, start, _text.Substring(start, _position - start));
            }

            if (Current == '(')
            {
                ++_position;
                return
                    new SyntaxNode(SyntaxKind.OpenP, start, _text.Substring(start, _position - start));
            }

            if (Current == ')')
            {
                ++_position;
                return
                    new SyntaxNode(SyntaxKind.CloseP, start, _text.Substring(start, _position - start));
            }

            _diagnostics.Add($"Error: could not recognize following char: {Current}");
            ++_position;
            return SyntaxNode.Unrecognized;
        }
    }
}