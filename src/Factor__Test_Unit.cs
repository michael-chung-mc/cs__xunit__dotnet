using Xunit;
using wip__cs__xunit.src.Factor;
using System.Numerics;

namespace wip__cs__xunit.test
{
    public class FactorTest
    {
        [Fact]
        public void Canary()
        {
            Assert.Equal(1, 1);
        }
        [Fact]
        public void MaxFactor_3__is_3()
        {
            Factor f = new Factor();
            BigInteger factor = f.maxFactor(3);
            int max = 3;
            Assert.Equal(factor, max);
        }
    }
}
