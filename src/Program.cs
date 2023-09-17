using System;
using System.Diagnostics.Metrics;
using wip__cs__xunit.src.Fibonacci;
using wip__cs__xunit.src.Multiple;
using wip__cs__xunit.src.Factor;
using wip__cs__xunit.src.Calculator;

namespace wip__cs__xunit
{
    internal class Program
    {
        static void projectEuler()
        {
            Multiple multiple = new();
            Fibonacci fibonacci = new Fibonacci();
            Factor factor = new Factor();
            Console.WriteLine("project euler problem 1 = sum of all the multiples of 3 or 5 below 1000");
            Console.WriteLine(multiple.sumMultiple(3, 5, 1000));
            Console.WriteLine("project euler problem 2 = sum of even of numbers < 4 million");
            Console.WriteLine(fibonacci.sumEven(4000000));
            Console.WriteLine("project euler problem 3 = largest prime factor of 600851475143");
            Console.WriteLine(factor.maxFactor(600851475143));
        }
        static void Main()
        {
            Calculator c = new Calculator();
            c.CliLoop();
        }
    }
}
