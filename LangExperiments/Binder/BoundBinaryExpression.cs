using System;

namespace LangExperiments
{
    class BoundBinaryExpression : BoundExpression
    {
        public BoundBinaryExpression(BoundExpression left,
           BoundBinaryOperatorKind operatorKind,
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

        public int Evaluate()
        {
            var left = Left.Evaluate();
            var right = Right.Evaluate();
            return SwitchOperator();

            int SwitchOperator() => OperatorKind switch
            {
                BoundBinaryOperatorKind.Addition => left + right,
                BoundBinaryOperatorKind.Subtraction => left - right,
                BoundBinaryOperatorKind.Multiplication => left * right,
                BoundBinaryOperatorKind.Division => left / right,
                _ => throw new Exception("Invalid operator")
            };
        }
    }
}
