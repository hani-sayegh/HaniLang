using System;

namespace LangExperiments
{
    class BoundLiteralExpression : BoundExpression
    {
        public BoundLiteralExpression(object value)
        {
            Value = value;
        }

        public object Value { get; }

        public Type Type => Value.GetType();

        public BoundNodeKind Kind => BoundNodeKind.LiteralExpression;

        public int Evaluate()
        {
            return (int)Value;
        }
    }
}
