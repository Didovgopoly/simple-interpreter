using System;
using System.Collections.Generic;

namespace SimpleInterpreter
{
    public class Runtime : IAstVisitor
    {
        private readonly IOutput _output;
        private Dictionary<string, int> _variables;

        public Runtime(IOutput output)
        {
            _variables = new Dictionary<string, int>();
            _output = output;
        }

        public void Execute(string text)
        {
            try
            {
                var lexer = new Lexer(text);
                var interpreter = new Parser(lexer);
                var ast = interpreter.Evaluate();

                Execute(ast);
            }
            catch (ParserException e)
            {
                _output.WriteLine(e.OutMessage);
            }
            catch (Exception ex)
            {
                _output.WriteLine(ex.Message);
            }
        }

        private void Execute(TreeNode node)
        {
            node.Visit(this);
        }

        int IAstVisitor.Visit(Number node)
        {
            return node.Value;
        }

        int IAstVisitor.Visit(BinaryOperation node)
        {
            var left = node.Left.Visit(this);
            var right = node.Right.Visit(this);

            switch (node.Operation)
            {
                case TokenType.Plus:
                    return left + right;
                case TokenType.Minus:
                    return left - right;
                case TokenType.Multiply:
                    return left * right;
                case TokenType.Divide:
                    if (right == 0)
                    {
                        throw new ParserException($"Division by zero in position {node.Position}");
                    }

                    return left / right;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        int IAstVisitor.Visit(Variable node)
        {
            if (_variables.TryGetValue(node.Name, out var value))
            {
                return value;
            }

            throw new ParserException($"Variable <{node.Name}> is not defined");
        }

        public int Visit(Assignment node)
        {
            var result = node.Value.Visit(this);

            _variables[node.Variable.Name] = result;

            _output.WriteLine($"{node.Variable.Name} = {result}");

            return result;
        }
    }
}