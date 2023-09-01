using Xunit;
using wip__cs__xunit.src.Multiple;

namespace wip__cs__xunit.test
{
    public class MultipleTest
    {
        [Fact]
        public void Canary()
        {
            Assert.Equal(1, 1);
        }
        [Fact]
        public void SumMultiple_0_0_0_Is0()
        {
            Multiple m = new();
            int res = m.sumMultiple(0, 0, 0);
            Assert.Equal(0, res);
        }
        [Fact]
        public void SumMultiple_3_5_10_Is23()
        {
            Multiple m = new();
            int res = m.sumMultiple(3, 5, 10);
            Assert.Equal(23, res);
        }
        [Fact]
        public void SumMultiple_N1_N1_0_IsN1()
        {
            Multiple m = new();
            int res = m.sumMultiple(-1, -1, 0);
            Assert.Equal(-1, res);
        }
    }
}
