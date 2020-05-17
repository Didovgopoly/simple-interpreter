namespace SimpleInterpreter
{
    public class BinaryOperation : TreeNode
    {
        public TreeNode Left { get; }
        public TokenType Operation { get; }
        
        public TreeNode Right { get; }
        public int Position { get; }

        public BinaryOperation(TreeNode left, TokenType operation, TreeNode right, int position)
        {
            Left = left;
            Operation = operation;
            Right = right;
            Position = position;
        }

        public override int Visit(IAstVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}