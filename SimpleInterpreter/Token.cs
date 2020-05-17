namespace SimpleInterpreter
{
    public class Token
    {
        public TokenType Type { get; }
        public string Value { get; }
        public int Index { get; }

        public Token(TokenType type, string value, int index)
        {
            Type = type;
            Value = value;
            Index = index;
        }
    }
}