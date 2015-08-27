using System;

namespace Application
{
    public class Program
    {
        static Program()
        {
            SetContainer(null);
        }

        public static void Main(string[] args)
        {
            ICalculator calc = ObjectFactory.Current.Resolve<ICalculator>();
            calc.Sum(10, 100);
        }

        public static void SetContainer(IFactoryContainer container)
        {
            ObjectFactory.SerCurrent(container ?? new ConsoleContainer());
        }
    }

    public class ConsoleContainer : IFactoryContainer
    {
        public T Resolve<T>() where T : class
        {
            if (typeof(T) == typeof(ICalculator)) return new Calculator() as T;
            throw new Exception();
        }
    }

    public interface IFactoryContainer
    {
        T Resolve<T>() where T : class;
    }

    public class ObjectFactory
    {
        private static IFactoryContainer _current;

        public static IFactoryContainer Current { get { return _current; } }

        public static void SerCurrent(IFactoryContainer factory)
        {
            _current = factory;
        }
    }

    public class Calculator : ICalculator
    {
        public void Sum(int a, int b)
        {
            var sum = a + b;
            Console.WriteLine(sum);
            Console.ReadLine();
        }
    }

    public interface ICalculator
    {
        void Sum(int a, int b);
    }
}
