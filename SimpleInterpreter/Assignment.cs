namespace SimpleInterpreter
{
    public class Assignment : TreeNode
    {
        public Variable Variable { get; }
        public TreeNode Value { get; }

        public Assignment(Variable variable, TreeNode value)
        {
            Variable = variable;
            Value = value;
        }

        public override int Visit(IAstVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}