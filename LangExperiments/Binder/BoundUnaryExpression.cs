using System;

namespace LangExperiments
{
    class BoundUnaryExpression : BoundExpression
    {
        public BoundUnaryExpression(BoundUnaryOperatorKind operatorKind,
            BoundExpression operand)
        {
            OperatorKind = operatorKind;
            Operand = operand;
        }
        public Type Type => Operand.Type;
        public BoundNodeKind Kind => BoundNodeKind.UnaryExpression;

        public BoundUnaryOperatorKind OperatorKind { get; }
        public BoundExpression Operand { get; }

        public object Evaluate()
        {
            var result = Operand.Evaluate();
            var type = Operand.Type;
            if (type == typeof(bool))
                if (OperatorKind == BoundUnaryOperatorKind.Not)
                    result = !(bool)result;
            if(type == typeof(int))
            if (OperatorKind == BoundUnaryOperatorKind.Negation)
                result = -(int)result;

            return result;
        }
    }
}
