using System;
using System.Diagnostics.Metrics;

using LibProjectMeta;
using LibCalculator;
using wip__cs__xunit.src.CalculatorCLI;

namespace wip__cs__xunit
{
    internal class Program
    {
        static void projectEuler()
        {
            Calculator cr = new();
            Console.WriteLine("project euler problem 1 = sum of all the multiples of 3 or 5 below 1000");
            Console.WriteLine(cr.SumMultiple(3, 5, 1000));
            Console.WriteLine("project euler problem 2 = sum of even of numbers < 4 million");
            Console.WriteLine(cr.FibonacciSumEven(4000000));
            Console.WriteLine("project euler problem 3 = largest prime factor of 600851475143");
            Console.WriteLine(cr.MaxFactor(600851475143));
        }
        static void Main()
        {
            CalculatorCLI c = new CalculatorCLI();
            c.loop();
        }
    }
}