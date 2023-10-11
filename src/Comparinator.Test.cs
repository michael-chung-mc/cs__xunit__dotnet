using Xunit;
using LibTuple;
using LibComparinator;

namespace LibComparinator.Test;

public class ComparinatorTest
{
	Comparinator _fieldComp = new Comparinator();
    [Fact]
    public void Canary()
    {
        Assert.Equal(1, 1);
    }
	[Fact]
	public void TestFloatingPointEqual() {
		double z = 0;
		double zz = 0.0000000;
		double o = 0.0001;
		double t = 0.0002;
		Assert.True(_fieldComp.CheckFloat(z,zz));
		Assert.True(_fieldComp.CheckFloat(o,o));
		Assert.False(_fieldComp.CheckFloat(t,o));
	}
	[Fact]
	public void TestTuplesEqual() {
		Assert.True(_fieldComp.CheckTuple(new SpaceTuple(4.3, -4.2, 3.1, 1.0), new SpaceTuple(4.3, -4.2, 3.1, 1.0)));
		Assert.False(_fieldComp.CheckTuple(new SpaceTuple(4.3, -4.2, 3.1, 1.0), new SpaceTuple(4.3, -4.2, 3.0, 1.0)));
	}
}