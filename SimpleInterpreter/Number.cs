namespace SimpleInterpreter
{
    public class Number : TreeNode
    {
        public int Value { get; }

        public Number(int value)
        {
            Value = value;
        }
        
        public override int Visit(IAstVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}