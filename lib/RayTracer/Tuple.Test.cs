using Xunit;
using LibTuple;
using LibComparinator;

namespace LibTuple.Test;
public class TupleTest
{
	Comparinator _fieldComp = new Comparinator();
    [Fact]
    public void TupleTestCanary_WithDefault_ExpectDefault() {
        Assert.Equal(1, 1);
    }
    [Fact]
    public void Tuple_WithWOne_ExpectPoint ()
    {
    	SpaceTuple a = new SpaceTuple(4.3, -4.2, 3.1, 1.0);
    	Assert.Equal(4.3, a._fieldX);
    	Assert.Equal(-4.2, a._fieldY);
    	Assert.Equal(3.1, a._fieldZ);
    	Assert.Equal(1.0, a._fieldW);
    	Point ap = new Point(4.3, -4.2, 3.1);
    	Assert.Equal(a._fieldX, ap._fieldX);
    	Assert.Equal(a._fieldY, ap._fieldY);
    	Assert.Equal(a._fieldZ, ap._fieldZ);
    	Assert.Equal(a._fieldW, ap._fieldW);
    	Vector av = new Vector(4.3, -4.2, 3.1);
    	Assert.Equal(a._fieldX, av._fieldX);
    	Assert.Equal(a._fieldY, av._fieldY);
    	Assert.Equal(a._fieldZ, av._fieldZ);
    	Assert.NotEqual(a._fieldW, av._fieldW);
    }
    [Fact]
	public void Tuple_WithWZero_ExpectVector ()
	{
		SpaceTuple varTuple = new SpaceTuple(4.3, -4.2, 3.1, 0.0);
		Assert.Equal(4.3,varTuple._fieldX);
		Assert.True(_fieldComp.CheckFloat(-4.2, varTuple._fieldY));
		Assert.Equal(3.1,varTuple._fieldZ);
		Assert.Equal(0.0,varTuple._fieldW);
		Point ap = new Point(4.3, -4.2, 3.1);
		Assert.Equal(varTuple._fieldX, ap._fieldX);
		Assert.Equal(varTuple._fieldY, ap._fieldY);
		Assert.Equal(varTuple._fieldZ, ap._fieldZ);
		Assert.NotEqual(varTuple._fieldW, ap._fieldW);
		Vector av = new Vector(4.3, -4.2, 3.1);
		Assert.Equal(varTuple._fieldX, av._fieldX);
		Assert.Equal(varTuple._fieldY, av._fieldY);
		Assert.Equal(varTuple._fieldZ, av._fieldZ);
		Assert.Equal(varTuple._fieldW, av._fieldW);
	}
    [Fact]
	public void TuplePlusTuple_ExpectEqualsTuple ()
	{
		SpaceTuple a = new SpaceTuple(3, -2, 5, 1);
		SpaceTuple b = new SpaceTuple(-2, 3, 1, 0);
		SpaceTuple c = new SpaceTuple(1, 1, 6, 1);
		SpaceTuple result = a + b;
		Assert.True(_fieldComp.CheckTuple(c, result));
		a.SetAdd(b);
		Assert.True(_fieldComp.CheckTuple(a,c));
	}
    [Fact]
	public void TupleMinusTuple_ExpectEqualsTuple ()
	{
		SpaceTuple a = new SpaceTuple(3, 2, 1, 1);
		SpaceTuple b = new SpaceTuple(5, 6, 7, 1);
		SpaceTuple c = new SpaceTuple(-2, -4, -6, 0);
		SpaceTuple result = a - b;
		Assert.True(_fieldComp.CheckTuple(c, result));
	}
    [Fact]
	public void PointMinusVector_ExpectEqualsPoint ()
	{
		Point a = new Point(3, 2, 1);
		Vector b = new Vector(5, 6, 7);
		Point c = new Point(-2, -4, -6);
		SpaceTuple result = a - b;
		Assert.True(_fieldComp.CheckTuple(c, result));
	}
    [Fact]
	public void VectorMinusVector_ExpectEqualsVector ()
	{
		Vector a = new Vector(3, 2, 1);
		Vector b = new Vector(5, 6, 7);
		Vector c = new Vector(-2, -4, -6);
		SpaceTuple result = a - b;
		Assert.True(_fieldComp.CheckTuple(c, result));
	}
    [Fact]
	public void ZeroMinusVector_ExpectEqualsNegatedVector ()
	{
		Vector a = new Vector(0, 0, 0);
		Vector b = new Vector(1, -2, 3);
		Vector c = new Vector(-1, 2, -3);
		SpaceTuple result = a - b;
		Assert.True(_fieldComp.CheckTuple(c, result));
	}
    [Fact]
	public void NegateTupleMethod_WithGiven_ExpectFlippedSigns ()
	{
		SpaceTuple a = new SpaceTuple(1, -2, 3, -4);
		SpaceTuple an = -a;
		SpaceTuple c = new SpaceTuple(-1, 2, -3, 4);
		Assert.True(_fieldComp.CheckTuple(an, c));
	}
    [Fact]
	public void NegateZeroTuple_ExpectZero ()
	{
		SpaceTuple a = new SpaceTuple(0,0,0,0);
		SpaceTuple an = -a;
		SpaceTuple c = new SpaceTuple(0,0,0,0);
		Assert.True(_fieldComp.CheckTuple(an, c));
	}
    [Fact]
	public void NegateTupleOperator_WithGiven_ExpectFlippedSigns ()
	{
		//Scenario: Negating a tuple
		//Given a ← tuple(1, -2, 3, -4)
		//Then - a = new Tuple(-1, 2, -3, 4)
		SpaceTuple a = new SpaceTuple(1, -2, 3, -4);
		SpaceTuple an = -a;
		SpaceTuple c = new SpaceTuple(-1, 2, -3, 4);
		Assert.True(_fieldComp.CheckTuple(an, c));
	}
    [Fact]
	public void NegateVectorOperator_WithGiven_ExpectFlippedSigns ()
	{
		Vector a = new Vector(1, -2, 3);
		SpaceTuple an = -a;
		Vector c = new Vector(-1, 2, -3);
		Assert.True(_fieldComp.CheckTuple(an, c));
	}
    [Fact]
	public void TupleMultiplication_WithGiven_ExpectMultipliedValues ()
	{
		// Scenario: Multiplying a tuple by a scalar
		// Given a ← tuple(1, -2, 3, -4)
		// Then a * 3.5 = new Tuple(3.5, -7, 10.5, -14)
		SpaceTuple a = new SpaceTuple (1, -2, 3, -4);
		SpaceTuple b = a * 3.5;
		SpaceTuple c = new SpaceTuple(3.5, -7, 10.5, -14);
		Assert.True(_fieldComp.CheckTuple(b, c));
		Assert.True(_fieldComp.CheckTuple(b, c));
	}
    [Fact]
	public void TupleMultiplication_WithFractional_ExpectDividedValues ()
	{
		// Scenario: Multiplying a tuple by a fraction
		// Given a ← tuple(1, -2, 3, -4)
		// Then a * 0.5 = new Tuple(0.5, -1, 1.5, -2)
		SpaceTuple a = new SpaceTuple(1, -2, 3, -4);
		SpaceTuple b = a * 0.5;
		SpaceTuple c = new SpaceTuple(0.5, -1, 1.5, -2);
		Assert.True(_fieldComp.CheckTuple(b, c));
	}
    [Fact]
	public void DivideTuple_ExpectDividedValues ()
	{
		// Scenario: Dividing a tuple by a scalar
		// Given a ← tuple(1, -2, 3, -4)
		// Then a / 2 = new Tuple(0.5, -1, 1.5, -2)
		SpaceTuple a = new SpaceTuple(1, -2, 3, -4);
		SpaceTuple b = a / 2;
		SpaceTuple c = new SpaceTuple(0.5, -1, 1.5, -2);
		Assert.True(_fieldComp.CheckTuple(b, c));
	}
    [Fact]
	public void TupleGetMagnitudeUnit ()
	{
		//Scenario: Computing the GetMagnitude of vector(1, 0, 0)
		//Given v ← vector(1, 0, 0)
		//Then GetMagnitude(v) = 1
		//Scenario : Computing the GetMagnitude of vector(0, 1, 0)
		//Given v ← vector(0, 1, 0)
		//Then GetMagnitude(v) = 1
		//Scenario : Computing the GetMagnitude of vector(0, 0, 1)
		//Given v ← vector(0, 0, 1)
		//Then GetMagnitude(v) = 1
		Vector a = new Vector(1, 0, 0);
		Assert.Equal(1, a.GetMagnitude());
		Vector b = new Vector(0, 1, 0);
		Assert.Equal(1,b.GetMagnitude());
		Vector c = new Vector(0, 0, 1);
		Assert.Equal(1,c.GetMagnitude());
	}
    [Fact]
	public void TupleGetMagnitude ()
	{
		Vector a = new Vector(1, 2, 3);
		double mag = Math.Sqrt(14);
		Assert.True(_fieldComp.CheckFloat(a.GetMagnitude(), mag));
		Vector b = new Vector(-1, -2, -3);
		Assert.True(_fieldComp.CheckFloat(b.GetMagnitude(), mag));
	}
    [Fact]
	public void TupleNormalized ()
	{
		Vector unit = new Vector(1, 0, 0);
		Vector a = new Vector(4, 0, 0);
		SpaceTuple norm = a.GetNormal();
		Assert.True(_fieldComp.CheckTuple(unit, norm));
		unit = new Vector(1 / Math.Sqrt(14), 2 / Math.Sqrt(14), 3 / Math.Sqrt(14));
		a = new Vector(1, 2, 3);
		norm = a.GetNormal();
		Assert.True(_fieldComp.CheckTuple(unit, norm));
		double GetMagnitude = norm.GetMagnitude();
		Assert.Equal(1, GetMagnitude);
	}
    [Fact]
	public void TupleGetDotProductProduct ()
	{
		Vector a = new Vector(1, 2, 3);
		Vector b = new Vector(2, 3, 4);
		double GetDotProduct = a.GetDotProduct(b);
		Assert.Equal(20, GetDotProduct);
	}
    [Fact]
	public void TupleGetCrossProductProduct ()
	{
		Vector a = new Vector(1, 2, 3);
		Vector b = new Vector(2, 3, 4);
		SpaceTuple ab = a.GetCrossProduct(b);
		Vector c = new Vector(-1, 2, -1);
		Assert.True(_fieldComp.CheckTuple(ab, c));
		SpaceTuple ba = b.GetCrossProduct(a);
		c = new Vector(1, -2, 1);
		Assert.True(_fieldComp.CheckTuple(ba, c));
	}
    [Fact]
	public void TupleGetReflect ()
	{
		Vector v = new Vector(1, -1, 0);
		Vector n = new Vector(0, 1, 0);
		SpaceTuple r = v.GetReflect(n);
		Vector expectedR = new Vector(1,1,0);
		Assert.True(_fieldComp.CheckTuple(r, expectedR));
	}
    [Fact]
	public void TupleGetReflectAngled ()
	{
		Vector v = new Vector(0, -1, 0);
		Vector n = new Vector(Math.Sqrt(2)/2, Math.Sqrt(2)/2, 0);
		SpaceTuple r = v.GetReflect(n);
		Vector expectedR = new Vector(1,0,0);
		Assert.True(_fieldComp.CheckTuple(r, expectedR));
	}
}