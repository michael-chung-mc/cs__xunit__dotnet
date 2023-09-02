using System;
using System.Numerics;

namespace wip__cs__xunit.src.Fibonacci
{
    internal class Fibonacci
    {
        public List<int> iterative(int n)
        {
            return iterative(n, Int32.MaxValue);
        }
        public List<int> iterative(int n, int limit)
        {
            List<int> fib = new List<int>();
            fib.Add(1);
            if (n < 1)  { return fib; };
            fib.Add(1);
            if (n == 1) { return fib; };
            for (int i = 2; i < n; i++)
            {
                int nth = fib[i - 1] + fib[i - 2];
                if (nth > limit) { return fib; }
                fib.Add(nth);
            }
            return fib;
        }
        public BigInteger sumEven(int n)
        {
            return sumEven(n, Int32.MaxValue);
        }
        public BigInteger sumEven(int n, int limit)
        {
            BigInteger sum = 0;
            List<int> f = iterative(n, limit);
            for (int i = 0; i < f.Count(); i++)
            {
                BigInteger num = f[i];
                if (num % 2 == 0) { sum += num; }
            }
            return sum;
        }
    }
}