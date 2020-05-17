namespace SimpleInterpreter
{
    public class Parser
    {
        private readonly Lexer _lexer;
        private Token _currentToken;

        public Parser(Lexer lexer)
        {
            _lexer = lexer;
            _currentToken = _lexer.GetNextToken();
        }

        private void Advance()
        {
            _currentToken = _lexer.GetNextToken();
        }

        private TreeNode Statement()
        {
            var token = _currentToken;
            if (token.Type == TokenType.Integer)
            {
                Advance();
                return new Number(int.Parse(token.Value));
            }

            if (token.Type == TokenType.Variable)
            {
                Advance();
                return new Variable(token.Value);
            }

            if (token.Type == TokenType.LeftBracket)
            {
                Advance();
                var result = LowPriorityExpression();
                if (_currentToken.Type != TokenType.RightBracket)
                {
                    throw new ParserException($"Closing bracket is missing for bracket in position {token.Index}");
                }

                Advance();
                return result;
            }

            throw new ParserException($"Incorrect symbol in position {_currentToken.Index}");
        }

        private TreeNode HighPriorityExpression()
        {
            var result = Statement();

            while (_currentToken.Type == TokenType.Multiply || _currentToken.Type == TokenType.Divide)
            {
                var token = _currentToken;
                Advance();
                result = new BinaryOperation(result, token.Type, Statement(), token.Index);
            }

            return result;
        }

        private TreeNode LowPriorityExpression()
        {
            var result = HighPriorityExpression();

            while (_currentToken.Type == TokenType.Plus || _currentToken.Type == TokenType.Minus)
            {
                var token = _currentToken;
                Advance();
                result = new BinaryOperation(result, token.Type, HighPriorityExpression(), token.Index);
            }

            return result;
        }

        private TreeNode Assignment()
        {
            if (_currentToken.Type != TokenType.Variable)
            {
                throw new ParserException("Only variable assignment is possible");
            }

            var variableName = _currentToken;
            Advance();

            if (_currentToken.Type != TokenType.Assignment)
            {
                throw new ParserException("Expression should be an assignment");
            }

            Advance();
            var result = LowPriorityExpression();

            if (_currentToken.Type != TokenType.EndOfLine)
            {
                throw new ParserException("Garbage symbols after the expression");
            }

            return new Assignment(new Variable(variableName.Value), result);
        }

        public TreeNode Evaluate()
        {
            return Assignment();
        }
    }
}