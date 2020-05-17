using System;

namespace SimpleInterpreter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the interpreter!");
            Console.WriteLine("You can interpret anything");
            Console.WriteLine("If it is an assignment <a = 5 + 4>");
            var runtime = new Runtime(new Output());

            while (true)
            {
                runtime.Execute(Console.ReadLine());
            }
        }
    }
}