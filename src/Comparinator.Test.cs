using Xunit;
using LibTuple;
using LibComparinator;

namespace LibComparinator.Test;

public class ComparinatorTest
{
	Comparinator _fieldComp = new Comparinator();
    [Fact]
    public void ComparinatorTestCanary_WithDefault_ExpectDefault()
    {
        Assert.Equal(1, 1);
    }
	[Fact]
	public void ComparinatorTestFloatingPoint_WithEqual_ExpectTrue() {
		double z = 0;
		double zz = 0.0000000;
		Assert.True(_fieldComp.CheckFloat(z,zz));
	}
	[Fact]
	public void ComparinatorTestFloatingPoint_WithNotEqual_ExpectFalse() {
		double o = 0.0001;
		double t = 0.0002;
		Assert.True(_fieldComp.CheckFloat(o,o));
		Assert.False(_fieldComp.CheckFloat(t,o));
	}
	[Fact]
	public void ComparinatorTestTuples_WithEqual_ExpectTrue() {
		Assert.True(_fieldComp.CheckTuple(new SpaceTuple(4.3, -4.2, 3.1, 1.0), new SpaceTuple(4.3, -4.2, 3.1, 1.0)));
	}
	[Fact]
	public void ComparinatorTestTuples_WithNotEqual_ExpectFalse() {
		Assert.False(_fieldComp.CheckTuple(new SpaceTuple(4.3, -4.2, 3.1, 1.0), new SpaceTuple(4.3, -4.2, 3.0, 1.0)));
	}
}