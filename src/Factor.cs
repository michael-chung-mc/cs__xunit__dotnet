using System;
using System.Numerics;

namespace wip__cs__xunit.src.Factor
{
    internal class Factor
    {
        public BigInteger maxFactor(BigInteger n)
        {
            if (n < 4) { return n; }
            BigInteger maxFactor = 0;
            BigInteger factor = 2;
            while (n > 1)
            {
                if (n % factor == 0)
                {
                    maxFactor = factor;
                    n /= factor;
                }
                else
                {
                    factor += 1;
                }
            }
            return maxFactor;
        }
    }
}