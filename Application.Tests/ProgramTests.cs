using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using NUnit.Framework;

namespace Application.Tests
{
    [TestFixture]
    public class ProgramTests
    {
        private readonly Mock<ICalculator> _calculator = new Mock<ICalculator>();

        [TestFixtureSetUp]
        public void Setup()
        {
            Program.SetContainer(new TestContainer(_calculator.Object));
        }

        [Test]
        public void Should_Call_Calc()
        {
            _calculator.Setup(c => c.Sum(It.IsAny<int>(), It.IsAny<int>()));
            Program.Main(new string[0]);
            _calculator.VerifyAll();
        }
    }

    public class TestContainer : IFactoryContainer
    {
        private readonly ICalculator _calculator;

        public TestContainer(ICalculator calculator)
        {
            _calculator = calculator;
        }

        public T Resolve<T>() where T : class
        {
            if (typeof(T) == typeof(ICalculator)) return _calculator as T;
            return null;
        }
    }

    
}
