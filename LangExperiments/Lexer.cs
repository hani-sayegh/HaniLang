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

        public SyntaxNode NextToken()
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

            if (char.IsDigit(Current))
            {
                while (char.IsDigit(Current))
                {
                    ++_position;
                }
                return
                    new SyntaxNode(SyntaxKind.Number, start, _text.Substring(start, _position - start));
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
                    new SyntaxNode(SyntaxKind.Multiply, start, _text.Substring(start, _position - start));
            }

            if (Current == '-')
            {
                ++_position;
                return
                    new SyntaxNode(SyntaxKind.Minus, start, _text.Substring(start, _position - start));
            }

            if (Current == '/')
            {
                ++_position;
                return
                    new SyntaxNode(SyntaxKind.Divide, start, _text.Substring(start, _position - start));
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