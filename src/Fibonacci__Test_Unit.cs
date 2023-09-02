using Xunit;
using wip__cs__xunit.src.Fibonacci;
using System.Numerics;

namespace wip__cs__xunit.test
{
    public class FibonacciTest
    {
        [Fact]
        public void Canary()
        {
            Assert.Equal(1, 1);
        }
        [Fact]
        public void Fibonacci_3__is_1_1_2()
        {
            Fibonacci f = new Fibonacci();
            List<int> fib = f.iterative(3);
            List<int> res = new List<int>() { 1, 1, 2 };
            Assert.Equal(fib, res);
        }
        [Fact]
        public void FibonacciEvenSum_3__is_2()
        {
            Fibonacci f = new Fibonacci();
            BigInteger sum = f.sumEven(3);
            BigInteger res = 2;
            Assert.Equal(sum, res);
        }
    }
}
