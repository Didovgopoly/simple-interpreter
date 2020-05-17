namespace SimpleInterpreter
{
    public interface IAstVisitor
    {
        public int Visit(Number node);
        public int Visit(BinaryOperation node);
        public int Visit(Variable node);
        public int Visit(Assignment node);
    }
    
    public abstract class TreeNode
    {
        public abstract int Visit(IAstVisitor visitor);
    }
}