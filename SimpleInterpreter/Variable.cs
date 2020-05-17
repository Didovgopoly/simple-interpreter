namespace SimpleInterpreter
{
    public class Variable : TreeNode
    {
        public string Name { get; }

        public Variable(string name)
        {
            Name = name;
        }
        
        public override int Visit(IAstVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}