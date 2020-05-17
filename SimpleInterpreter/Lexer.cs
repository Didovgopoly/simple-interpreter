using System.Collections.Generic;

namespace SimpleInterpreter
{
    public class Lexer
    {
        private readonly string _text;
        private int _position;
        private char? _currentChar;
        private readonly Dictionary<char, TokenType> _tokenMap;
        public Lexer(string text)
        {
            _text = text.TrimEnd();
            _position = 0;
            _currentChar = _text[_position];
            
            _tokenMap = new Dictionary<char, TokenType>
            {
                {'+', TokenType.Plus},
                {'-', TokenType.Minus},
                {'/', TokenType.Divide},
                {'*', TokenType.Multiply},
                {'(', TokenType.LeftBracket},
                {')', TokenType.RightBracket},
                {'=', TokenType.Assignment},
            };
        }
        
        public Token GetNextToken()
        {
            while (_currentChar.HasValue)
            {
                SkipWhitespace();
                
                int position = _position;

                if (char.IsDigit(_currentChar.Value))
                {
                    return new Token(TokenType.Integer, Integer().ToString(), position);
                }

                if (char.IsLetter(_currentChar.Value))
                {
                    return new Token(TokenType.Variable, Variable(), position);
                }

                if (_tokenMap.TryGetValue(_currentChar.Value, out var type))
                {
                    var token = new Token(type, _currentChar.Value.ToString(), _position);
                    Advance();
                    return token;
                }
                throw new ParserException($"Invalid symbol <{_currentChar.Value}>");
            }
            
            return new Token(TokenType.EndOfLine, string.Empty, _position);
        }

        private void Advance()
        {
            _position++;
            _currentChar = _position < _text.Length ? _text[_position] : (char?)null;
        }

        private void SkipWhitespace()
        {
            while (_currentChar == ' ')
            {
                Advance();
            }
        }

        private int Integer()
        {
            string value = "";
            while (_currentChar.HasValue && char.IsDigit(_currentChar.Value))
            {
                value += _currentChar;
                Advance();
            }

            if (_currentChar.HasValue && char.IsLetter(_currentChar.Value))
            {
                throw new ParserException($"Invalid character for number {value}{_currentChar.Value}");
            }

            if (int.TryParse(value, out var integer))
            {
                return integer;
            }
            
            throw new ParserException($"Number is too big {value}");
        }    
        
        private string Variable()
        {
            string value = "";
            while (_currentChar.HasValue && char.IsLetter(_currentChar.Value))
            {
                value += _currentChar;
                Advance();
            }
            
            if (_currentChar.HasValue && char.IsDigit(_currentChar.Value))
            {
                throw new ParserException($"Invalid variable name {value}{_currentChar.Value}");
            }

            return value;
        }

    }
}