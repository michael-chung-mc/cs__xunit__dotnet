using System;
using wip__cs__xunit.src.Multiple;

namespace wip__cs__xunit
{
    internal class Program
    {
        static void Main()
        {
            Multiple m = new();
            Console.WriteLine(m.sumMultiple(3, 5, 1000));
        }
    }
}
