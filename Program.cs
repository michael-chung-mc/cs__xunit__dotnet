using System;
using System.Diagnostics.Metrics;
using wip__cs__xunit.src.Fibonacci;
using wip__cs__xunit.src.Multiple;

namespace wip__cs__xunit
{
    internal class Program
    {
        static void projectEuler()
        {
            Console.WriteLine("project euler problem 1");
            Multiple m = new();
            Console.WriteLine(m.sumMultiple(3, 5, 1000));
            Console.WriteLine("project euler problem 2");
            Fibonacci f = new Fibonacci();
            Console.WriteLine(f.sumEven(4));
            Console.WriteLine(f.sumEven(40));
            Console.WriteLine(f.sumEven(4000000));
        }
        static void Main()
        {
            projectEuler();
        }
    }
}
