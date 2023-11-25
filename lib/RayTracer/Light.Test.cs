using Xunit;
using LibLight;
using LibTuple;
using LibColor;
using LibComparinator;

namespace LibLight.Test;

public class LightTest
{
	Comparinator _fieldComp = new Comparinator();
    [Fact]
    public void LightTestCanary_WithDefault_ExpectDefault() {
        Assert.Equal(1, 1);
    }
    [Fact]
    public void PointSourceCtor_WithDefault_ExpectDefault() {
        Color c = new Color(1,1,1);
        Point p = new Point(0,0,0);
        PointSource ps = new PointSource(p,c);
        Assert.True(_fieldComp.CheckTuple(c,ps.mbrIntensity));
        Assert.True(_fieldComp.CheckTuple(p,ps.mbrPosition));
    }
}
