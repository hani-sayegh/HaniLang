﻿using System;

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

        public int Evaluate()
        {
            var result = Operand.Evaluate();
            if (OperatorKind == BoundUnaryOperatorKind.Negation)
                result = -result;

            return result;
        }
    }
}