using LangExperiments.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangExperiments
{
    enum BoundNodeKind
    {
        UnaryExpression,
        LiteralExpression
    }

    interface BoundExpression
    {
        //we only know the type at runtime
        Type Type { get; }
        object Evaluate();
    }

    enum BoundUnaryOperatorKind
    {
        Identity,
        Negation,
        Not
    }

    enum BoundBinaryOperatorKind
    {
        Addition,
        Subtraction,
        Multiplication,
        Division,
        LogicalAnd,
        LogicalEqual,
        LogicalOr,
        NotEqual
    }

    class Binder
    {
        readonly List<string> _diagnostics = new List<string>();
        public IEnumerable<string> Diagnostics => _diagnostics;
        public BoundExpression BindExpression(ISyntaxNode syntax)
        {
            switch (syntax.Kind)
            {
                case SyntaxKind.LiteralExpression:
                    return BindLiteralExpression((LiteralNode)syntax);
                case SyntaxKind.BinaryNode:
                    return BindBinaryExpression((BinaryNode)syntax);
                case SyntaxKind.UnaryExpression:
                    return BindUnaryExpression((UnaryNode)syntax);
                default:
                    throw new Exception($"Invalid {syntax.Kind}");
            }
        }

        private BoundExpression BindUnaryExpression(UnaryNode syntax)
        {
            var boundOperand = BindExpression(syntax.Expression);
            var boundOperator = BinadUnaryOperatorKind(syntax.OperatorToken, boundOperand);

            if (boundOperator == null)
            {
                _diagnostics.Add($"Could not bind unary operator {syntax.OperatorToken.Kind} to type of {boundOperand.Type}");
                return boundOperand;
            }

            return new BoundUnaryExpression(boundOperator.Value, boundOperand);
        }

        private BoundUnaryOperatorKind? BinadUnaryOperatorKind(SyntaxNode operatorToken, BoundExpression boundExpression)
        {
            var type = boundExpression.Type;
            if (type == typeof(int))
            {
                switch (operatorToken.Kind)
                {
                    case SyntaxKind.Plus: return BoundUnaryOperatorKind.Identity;
                    case SyntaxKind.MinusToken: return BoundUnaryOperatorKind.Negation;
                    default: 
                        _diagnostics.Add($"Operator token {operatorToken.Kind} not applicable on type {type}");
                        return null;
                }
            }
            else if (type == typeof(bool))
            {
                switch (operatorToken.Kind)
                {
                    case SyntaxKind.Not: return BoundUnaryOperatorKind.Not;
                    default: 
                        _diagnostics.Add($"Operator token {operatorToken.Kind} not applicable on type {type}");
                        return null;
                }
            }

            return null;
        }

        private BoundExpression BindBinaryExpression(BinaryNode syntax)
        {
            var boundLeft = BindExpression(syntax.Left);
            var boundRight = BindExpression(syntax.Right);
            var boundOperator = BindBinaryOperator(syntax.BinaryOperator, boundLeft, boundRight);

            if(boundOperator == null)
            {
                _diagnostics.Add($"Could not bind operator {syntax.BinaryOperator.Kind} to type of {boundLeft.Type} && {boundRight.Type}");
                return boundLeft;
            }
            return new BoundBinaryExpression(boundLeft, boundOperator.Value, boundRight);
        }

        private (BoundBinaryOperatorKind, Type)? BindBinaryOperator(SyntaxNode syntaxNode, BoundExpression left,
            BoundExpression right)
        {
            if(left.Type != right.Type)
                return null;

            if (left.Type == typeof(bool))
            {
                var validBoolOperators = new List<SyntaxKind> { SyntaxKind.LogicalAnd, SyntaxKind.LogicalEqual, SyntaxKind.LogicalOr, SyntaxKind.NotEqual };
                if (!validBoolOperators.Contains(syntaxNode.Kind))
                    return null;
            }

            else if (left.Type == typeof(int))
            {
                var validIntOperators = new List<SyntaxKind> { SyntaxKind.Plus, SyntaxKind.MinusToken, SyntaxKind.MultiplyToken, SyntaxKind.DivideToken, SyntaxKind.LogicalEqual, SyntaxKind.NotEqual};

                if (!validIntOperators.Contains(syntaxNode.Kind))
                    return null;
            }
            else return null;

            return SwitchBindBinaryOperator(syntaxNode);
        }

        private (BoundBinaryOperatorKind, Type) SwitchBindBinaryOperator(SyntaxNode syntaxNode) =>
            syntaxNode.Kind switch
            {
                SyntaxKind.Plus => (BoundBinaryOperatorKind.Addition, typeof(int)),
                SyntaxKind.MinusToken => (BoundBinaryOperatorKind.Subtraction, typeof(int)),
                SyntaxKind.DivideToken => (BoundBinaryOperatorKind.Division, typeof(int)),
                SyntaxKind.MultiplyToken => (BoundBinaryOperatorKind.Multiplication, typeof(int)),
                SyntaxKind.LogicalAnd => (BoundBinaryOperatorKind.LogicalAnd, typeof(bool)),
                SyntaxKind.LogicalEqual => (BoundBinaryOperatorKind.LogicalEqual, typeof(bool)),
                SyntaxKind.LogicalOr => (BoundBinaryOperatorKind.LogicalOr, typeof(bool)),
                SyntaxKind.NotEqual => (BoundBinaryOperatorKind.NotEqual, typeof(bool)),
                _ => throw new Exception($"Could not recognize operator: {syntaxNode.Kind}"),
            };

        private BoundExpression BindLiteralExpression(LiteralNode syntax)
        {
            var value = syntax.Value ?? 0;
            return new BoundLiteralExpression(value);
        }
    }
}
