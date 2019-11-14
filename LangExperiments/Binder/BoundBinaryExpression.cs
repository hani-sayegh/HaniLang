using System;

namespace LangExperiments
{
    class BoundBinaryExpression : BoundExpression
    {
        public BoundBinaryExpression(BoundExpression left,
           (BoundBinaryOperatorKind, Type) operatorKind,
           BoundExpression right)
        {
            Left = left;
            OperatorKind = operatorKind.Item1;
            Right = right;
            Type = operatorKind.Item2;
        }

        public BoundExpression Left { get; }
        public BoundBinaryOperatorKind OperatorKind { get; }
        public BoundExpression Right { get; }

        public Type Type { get;}

        public object Evaluate()
        {
            dynamic left =  Left.Evaluate();
            dynamic right = Right.Evaluate();
            return SwitchOperator();

            object SwitchOperator() => OperatorKind switch
            {
                BoundBinaryOperatorKind.Addition => left + right,
                BoundBinaryOperatorKind.Subtraction => left - right,
                BoundBinaryOperatorKind.Multiplication => left * right,
                BoundBinaryOperatorKind.Division => left / right,
                BoundBinaryOperatorKind.LogicalAnd => left && right,
                BoundBinaryOperatorKind.LogicalEqual => left == right,
                BoundBinaryOperatorKind.LogicalOr => left || right,
                BoundBinaryOperatorKind.NotEqual => left != right,
                _ => throw new Exception("Invalid operator")
            };
        }
    }
}
