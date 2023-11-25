using Xunit;
using LibColor;
using LibComparinator;
namespace LibColor.Test;

public class ColorTest {
	Comparinator _fieldComp = new Comparinator();
    [Fact]
    public void ColorTestCanary_WithDefault_ExpectDefault()
    {
        Assert.Equal(1, 1);
    }
    [Fact]
	public void ColorConstructor_WithGiven_ExpectGiven() {
		Color c = new Color(-0.5, 0.4, 1.7);
		Assert.Equal(-.5, c._fieldRed);
		Assert.Equal(.4, c._fieldGreen);
		Assert.Equal(1.7, c._fieldBlue);
	}
    [Fact]
	public void ColorAddition_WithGiven_ExpectSummed() {
		Color c1 = new Color(0.9, 0.6, 0.75);
		Color c2 = new Color(0.7, 0.1, 0.25);
		Color c12 = c1 + c2;
		Color c3 = new Color(1.6, 0.7, 1.0);
		Assert.True(c3.CheckEqual(c12));
	}
    [Fact]
	public void ColorSubtraction_WithGiven_ExpectResult() {
		Color c1 = new Color(0.9, 0.6, 0.75);
		Color c2 = new Color(0.7, 0.1, 0.25);
		Color c12 = c1 - c2;
		Color c3 = new Color(0.2, 0.5, 0.5);
		Assert.True(c3.CheckEqual(c12));
	}
    [Fact]
	public void ColorScalarMultiplication_WithGiven_ExpectResult() {
		Color c1 = new Color(0.2, 0.3, 0.4);
		Color c2 = new Color(0.4, 0.6, 0.8);
		Color c3 = c1 * 2;
		Assert.True(c3.CheckEqual(c2));
	}
    [Fact]
	public void ColorMultiplicationHadamardProduct_WithGiven_ExpectResult() {
		Color c1 = new Color(1, 0.2, 0.4);
		Color c2 = new Color(0.9, 1, 0.1);
		Color c12 = c1 * c2;
		Color c3 = new Color(0.9, 0.2, 0.04);
		Assert.True(c3.CheckEqual(c12));
	}
}