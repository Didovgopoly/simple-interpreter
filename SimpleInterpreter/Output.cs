using System;

namespace SimpleInterpreter
{
    public class Output : IOutput
    {
        public void WriteLine(string value)
        {
            Console.WriteLine(value);
        }
    }
}