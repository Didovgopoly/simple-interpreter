using System.Collections.Generic;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SimpleInterpreter;

namespace InterpreterTests
{
    public class RuntimeTests
    {
        private Mock<IOutput> _outputMock;
        private List<string> _output;

        [SetUp]
        public void Setup()
        {
            _output = new List<string>();
            
            _outputMock = new Mock<IOutput>();
            _outputMock.Setup(x => x.WriteLine(It.IsAny<string>()))
                .Callback<string>(x => _output.Add(x));
        }

        private Runtime CreateRuntime()
        {
            return new Runtime(_outputMock.Object);
        }

        [Test]
        public void UsesVariables()
        {
            var runtime = CreateRuntime();
            
            runtime.Execute("a = 5 + 3");
            runtime.Execute("b = 2 * 2");
            runtime.Execute("c = a + b");

            _output.Should().BeEquivalentTo("a = 8", "b = 4", "c = 12");
        }

        [TestCase("c = 5", "c = 5", Description = "Simple expression")]
        [TestCase("c = 5 + 3", "c = 8", Description = "Sum")]
        [TestCase("c = 117 - 100", "c = 17", Description = "Minus")]
        [TestCase("c = 14 * 3", "c = 42", Description = "Multiplication")]
        [TestCase("c = 16 / 2", "c = 8", Description = "Division")]
        [TestCase("c = 9 / 2", "c = 4", Description = "Integer Division")]
        [TestCase("c = (5 + 6)", "c = 11", Description = "Brackets")]
        [TestCase("c = (5 + 6) * 2", "c = 22", Description = "Complex Brackets")]
        [TestCase("   c   =    16    /   2   ", "c = 8", Description = "Long spaces")]
        [TestCase("c = 16/0", "Division by zero in position 6", Description = "Division by zero")]
        [TestCase("cab = 5 * 4", "cab = 20", Description = "Long variable name")]
        [TestCase("c = a + 3", "Variable <a> is not defined", Description = "Variable not defined")]
        [TestCase("_c = 1 + 3", "Invalid symbol <_>", Description = "Invalid symbol not defined")]
        [TestCase("c = (1 + 3", "Closing bracket is missing for bracket in position 4", Description = "Missing open bracket")]
        [TestCase("c = (1 + 3))", "Garbage symbols after the expression", Description = "Extra symbols")]
        [TestCase("5 = (1 + 3))", "Only variable assignment is possible", Description = "non variable assignment")]
        [TestCase("c + (1 + 3))", "Expression should be an assignment", Description = "not an assignment")]
        [TestCase("c = 1a + 2", "Invalid character for number 1a", Description = "Letter after number")]
        [TestCase("c4 = 1 + 2", "Invalid variable name c4", Description = "Variable with letter")]
        public void ExecutesLine(string line, string outcome)
        {
            var runtime = CreateRuntime();
            runtime.Execute(line);

            _output.Should().ContainSingle().Which.Should().Be(outcome);
        }

        [TestCase("a = +1", "Incorrect symbol in position 4")]
        [TestCase("a = (+1)", "Incorrect symbol in position 5")]
        [TestCase("a = (6+*1)", "Incorrect symbol in position 7")]
        [TestCase("a = (1+)", "Incorrect symbol in position 7")]
        public void IncorrectSymbols(string line, string outcome)
        {
            var runtime = CreateRuntime();
            runtime.Execute(line);
            
            _output.Should().ContainSingle().Which.Should().Be(outcome);
        }
    }
}