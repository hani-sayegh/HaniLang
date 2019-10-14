namespace LangExperiments
{
    enum SyntaxKind
    {
        Plus = 1,
        Minus = 2,
        WhiteSpace = 4,
        Number = 8,
        EndOfString = 16,
        Unrecognized = 32
    }
    class SyntaxToken : ISyntaxNode
    {
        public static SyntaxToken Unrecognized { get; } = new SyntaxToken(SyntaxKind.Unrecognized, -1, null);

        public bool EndOfString => SyntaxKind == SyntaxKind.EndOfString;
        public SyntaxToken(SyntaxKind syntaxKind, int position, string text)
        {
            SyntaxKind = syntaxKind;
            Position = position;
            Text = text;
            var mask = (SyntaxKind.Plus | SyntaxKind.Minus);
            Operator = (mask & SyntaxKind) != 0;
        }

        [ToStringAttribute]
        public SyntaxKind SyntaxKind { get; }
        public int Position { get; }
        public string Text { get; }

        public bool Number => SyntaxKind == SyntaxKind.Number;
        public readonly bool Operator;
    }
}