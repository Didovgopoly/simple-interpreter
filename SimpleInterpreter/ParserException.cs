using System;

namespace SimpleInterpreter
{
    public class ParserException : Exception
    {
        public ParserException(string message)
        {
            OutMessage = message;
        }
        
        public string OutMessage { get; }
    }
}