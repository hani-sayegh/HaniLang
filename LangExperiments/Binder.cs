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
    }

    enum BoundUnaryOperatorKind
    {
Identity,
Negation
    }

    class BoundUnaryExpression : BoundExpression
    {
        public BoundUnaryExpression(BoundUnaryExpression operatorKind,
            BoundExpression operand)
        {
            OperatorKind = operatorKind;
            Operand = operand;
        }
        public Type Type => Operand.Type;
        public BoundNodeKind Kind => BoundNodeKind.UnaryExpression;

        public BoundUnaryExpression OperatorKind { get; }
        public BoundExpression Operand { get; }
    }

    class BoundLiteralExpression : BoundExpression
    {
        public BoundLiteralExpression(object value)
        {
            Value = value;
        }

        public object Value { get; }

        public Type Type => Value.GetType();

        public BoundNodeKind Kind => BoundNodeKind.LiteralExpression;
    }

    enum BoundBinaryOperatorKind
    {
Addition,
Subtraction,
Multiplication,
Division
    }

    class BoundBinaryExpression : BoundExpression
    {
        public BoundBinaryExpression(BoundExpression left,
           BoundBinaryOperatorKind operatorKind ,
           BoundExpression right)
        {
            Left = left;
            OperatorKind = operatorKind;
            Right = right;
        }

        public BoundExpression Left { get; }
        public BoundBinaryOperatorKind OperatorKind { get; }
        public BoundExpression Right { get; }

        public Type Type => Left.Type;

        public BoundNodeKind Kind => BoundNodeKind.UnaryExpression;
    }

class Binder
    {
        public BoundExpression Bind(ISyntaxNode expressionSyntax)
        {
            switch(expressionSyntax.Kind)
            {
                case SyntaxKind.LiteralExpression: 
                    break;
                case SyntaxKind.BinaryNode:
                    break;
                case SyntaxKind.UnaryExpression:
                    break;
                default:
                    throw new Exception("WAAA");
            }
        }
    }
}
