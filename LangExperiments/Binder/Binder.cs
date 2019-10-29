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

    interface BoundNode
    {
        BoundNodeKind Kind { get; }
    }

    interface BoundExpression : BoundNode
    {
        Type Type { get; }
        int Evaluate();
    }

    enum BoundUnaryOperatorKind
    {
        Identity,
        Negation
    }

    enum BoundBinaryOperatorKind
    {
        Addition,
        Subtraction,
        Multiplication,
        Division
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
                    return BindLiteralExpression((SyntaxNode)syntax);
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
            if (boundExpression.Type != typeof(int))
                return null;

            switch (operatorToken.Kind)
            {
                case SyntaxKind.Plus: return BoundUnaryOperatorKind.Identity;
                case SyntaxKind.MinusToken: return BoundUnaryOperatorKind.Negation;
                default: throw new Exception($"Operator token {operatorToken.Kind} not recignized");
            }
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

        private BoundBinaryOperatorKind? BindBinaryOperator(SyntaxNode syntaxNode, BoundExpression left,
            BoundExpression right)
        {
            var intType = typeof(int);
            if (left.Type != intType || right.Type != intType)
                return null;
            return SwitchBindBinaryOperator(syntaxNode);
        }

        private BoundBinaryOperatorKind SwitchBindBinaryOperator(SyntaxNode syntaxNode) =>
            syntaxNode.Kind switch
            {
                SyntaxKind.Plus => BoundBinaryOperatorKind.Addition,
                SyntaxKind.MinusToken => BoundBinaryOperatorKind.Subtraction,
                SyntaxKind.DivideToken => BoundBinaryOperatorKind.Division,
                SyntaxKind.MultiplyToken => BoundBinaryOperatorKind.Multiplication,
                _ => throw new Exception($"Could not recognize operator: {syntaxNode.Kind}"),
            };

        private BoundExpression BindLiteralExpression(SyntaxNode syntax)
        {
            var value = syntax.Value as int? ?? 0;
            return new BoundLiteralExpression(value);
        }
    }
}
