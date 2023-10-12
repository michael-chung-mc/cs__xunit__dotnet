using Xunit;
using LibMatrix;
using LibComparinator;
using LibTuple;
using LibProjectMeta;
namespace LibMatrix.Test;

public class MatrixTest
{
	Comparinator _fieldComp = new Comparinator();
	ProjectMeta varPM = new ProjectMeta();
	[Fact]
	public void CanaryTest() {
		Assert.Equal(1, 1);
		Assert.True(true);
	}
	[Fact]
	public void Matrix4x4ctor ()
	{
		double a = 1;
		double b = 2;
		double c = 3;
		double d = 4;
		double e = 5.5;
		double f = 6.5;
		double g = 7.5;
		double h = 8.5;
		double i = 9;
		double j = 10;
		double k = 11;
		double l = 12;
		double m = 13.5;
		double n = 14.5;
		double o = 15.5;
		double p = 16.5;
		Matrix mx = new Matrix(4,4);
		mx.SetRC(0, 0, 1);
		mx.SetRC(0, 1, 2);
		mx.SetRC(0, 2, 3);
		mx.SetRC(0, 3, 4);
		mx.SetRC(1, 0, 5.5);
		mx.SetRC(1, 1, 6.5);
		mx.SetRC(1, 2, 7.5);
		mx.SetRC(1, 3, 8.5);
		mx.SetRC(2, 0, 9);
		mx.SetRC(2, 1, 10);
		mx.SetRC(2, 2, 11);
		mx.SetRC(2, 3, 12);
		mx.SetRC(3, 0, 13.5);
		mx.SetRC(3, 1, 14.5);
		mx.SetRC(3, 2, 15.5);
		mx.SetRC(3, 3, 16.5);
		Assert.True(_fieldComp.CheckFloat(mx.GetRC(0, 0), a));
		Assert.True(_fieldComp.CheckFloat(mx.GetRC(0, 1), b));
		Assert.True(_fieldComp.CheckFloat(mx.GetRC(0, 2), c));
		Assert.True(_fieldComp.CheckFloat(mx.GetRC(0, 3), d));
		Assert.True(_fieldComp.CheckFloat(mx.GetRC(1, 0), e));
		Assert.True(_fieldComp.CheckFloat(mx.GetRC(1, 1), f));
		Assert.True(_fieldComp.CheckFloat(mx.GetRC(1, 2), g));
		Assert.True(_fieldComp.CheckFloat(mx.GetRC(1, 3), h));
		Assert.True(_fieldComp.CheckFloat(mx.GetRC(2, 0), i));
		Assert.True(_fieldComp.CheckFloat(mx.GetRC(2, 1), j));
		Assert.True(_fieldComp.CheckFloat(mx.GetRC(2, 2), k));
		Assert.True(_fieldComp.CheckFloat(mx.GetRC(2, 3), l));
		Assert.True(_fieldComp.CheckFloat(mx.GetRC(3, 0), m));
		Assert.True(_fieldComp.CheckFloat(mx.GetRC(3, 1), n));
		Assert.True(_fieldComp.CheckFloat(mx.GetRC(3, 2), o));
		Assert.True(_fieldComp.CheckFloat(mx.GetRC(3, 3), p));
		List<double> arr = new List<double>{ 1,2,3,4,5.5,6.5,7.5,8.5,9,10,11,12,13.5,14.5,15.5,16.5 };
		Matrix mx2 = new Matrix(4, 4, arr);
		Assert.True(mx.CheckEqual(mx2));
	}
	[Fact]
	public void Matrix2x2ctor()
	{
		double a = -3;
		double b = 5;
		double c = 1;
		double d = -2;
		Matrix mx = new Matrix(2, 2);
		mx.SetRC(0, 0, a);
		mx.SetRC(0, 1, b);
		mx.SetRC(1, 0, c);
		mx.SetRC(1, 1, d);
		Assert.True(_fieldComp.CheckFloat(mx.GetRC(0, 0), a));
		Assert.True(_fieldComp.CheckFloat(mx.GetRC(0, 1), b));
		Assert.True(_fieldComp.CheckFloat(mx.GetRC(1, 0), c));
		Assert.True(_fieldComp.CheckFloat(mx.GetRC(1, 1), d));
		List<double> arr = new List<double>{ -3,5,1,-2 };
		Matrix mx2 = new Matrix(2, 2, arr);
		Assert.True(mx.CheckEqual(mx2));
	}
	[Fact]
	public void Matrix3x3ctor()
	{
		double a = -3;
		double b = 5;
		double c = 1;
		double d = -2;
		double e = 0;
		double f = -7;
		Matrix mx = new Matrix(3, 3);
		mx.SetRC(0, 0, -3);
		mx.SetRC(0, 1, 5);
		mx.SetRC(0, 2, 0);
		mx.SetRC(1, 0, 1);
		mx.SetRC(1, 1, -2);
		mx.SetRC(1, 2, -7);
		mx.SetRC(2, 0, 0);
		mx.SetRC(2, 1, 1);
		mx.SetRC(2, 2, 1);
		Assert.True(_fieldComp.CheckFloat(mx.GetRC(0, 0), a));
		Assert.True(_fieldComp.CheckFloat(mx.GetRC(0, 1), b));
		Assert.True(_fieldComp.CheckFloat(mx.GetRC(0, 2), e));
		Assert.True(_fieldComp.CheckFloat(mx.GetRC(1, 0), c));
		Assert.True(_fieldComp.CheckFloat(mx.GetRC(1, 1), d));
		Assert.True(_fieldComp.CheckFloat(mx.GetRC(1, 2), f));
		Assert.True(_fieldComp.CheckFloat(mx.GetRC(2, 0), e));
		Assert.True(_fieldComp.CheckFloat(mx.GetRC(2, 1), c));
		Assert.True(_fieldComp.CheckFloat(mx.GetRC(2, 2), c));
		List<double> arr = new List<double>{ -3,5,0,1,-2,-7,0,1,1 };
		Matrix mx2 = new Matrix(3, 3, arr);
		Assert.True(mx.CheckEqual(mx2));
	}
	[Fact]
	public void MatrixComparison4x4to4x4True()
	{
		Matrix mx1 = new Matrix(4, 4);
		mx1.SetRC(0, 0, 1);
		mx1.SetRC(0, 1, 2);
		mx1.SetRC(0, 2, 3);
		mx1.SetRC(0, 3, 4);
		mx1.SetRC(1, 0, 5);
		mx1.SetRC(1, 1, 6);
		mx1.SetRC(1, 2, 7);
		mx1.SetRC(1, 3, 8);
		mx1.SetRC(2, 0, 9);
		mx1.SetRC(2, 1, 8);
		mx1.SetRC(2, 2, 7);
		mx1.SetRC(2, 3, 6);
		mx1.SetRC(3, 0, 5);
		mx1.SetRC(3, 1, 4);
		mx1.SetRC(3, 2, 3);
		mx1.SetRC(3, 3, 2);
		Matrix mx2 = new Matrix(4, 4);
		mx2.SetRC(0, 0, 1);
		mx2.SetRC(0, 1, 2);
		mx2.SetRC(0, 2, 3);
		mx2.SetRC(0, 3, 4);
		mx2.SetRC(1, 0, 5);
		mx2.SetRC(1, 1, 6);
		mx2.SetRC(1, 2, 7);
		mx2.SetRC(1, 3, 8);
		mx2.SetRC(2, 0, 9);
		mx2.SetRC(2, 1, 8);
		mx2.SetRC(2, 2, 7);
		mx2.SetRC(2, 3, 6);
		mx2.SetRC(3, 0, 5);
		mx2.SetRC(3, 1, 4);
		mx2.SetRC(3, 2, 3);
		mx2.SetRC(3, 3, 2);
		Assert.True((mx1 == mx2));
	}
	[Fact]
	public void MatrixComparison4x4to4x4False()
	{
		Matrix mx1 = new Matrix(4, 4);
		mx1.SetRC(0, 0, 1);
		mx1.SetRC(0, 1, 2);
		mx1.SetRC(0, 2, 3);
		mx1.SetRC(0, 3, 4);
		mx1.SetRC(1, 0, 5);
		mx1.SetRC(1, 1, 6);
		mx1.SetRC(1, 2, 7);
		mx1.SetRC(1, 3, 8);
		mx1.SetRC(2, 0, 9);
		mx1.SetRC(2, 1, 8);
		mx1.SetRC(2, 2, 7);
		mx1.SetRC(2, 3, 6);
		mx1.SetRC(3, 0, 5);
		mx1.SetRC(3, 1, 4);
		mx1.SetRC(3, 2, 3);
		mx1.SetRC(3, 3, 2);
		Matrix mx2 = new Matrix(4, 4);
		mx2.SetRC(0, 0, 1);
		mx2.SetRC(0, 1, 2);
		mx2.SetRC(0, 2, 3);
		mx2.SetRC(0, 3, 4);
		mx2.SetRC(1, 0, 5);
		mx2.SetRC(1, 1, 6);
		mx2.SetRC(1, 2, 7);
		mx2.SetRC(1, 3, 8);
		mx2.SetRC(2, 0, 9);
		mx2.SetRC(2, 1, 8);
		mx2.SetRC(2, 2, 7);
		mx2.SetRC(2, 3, 6);
		mx2.SetRC(3, 0, 5);
		mx2.SetRC(3, 1, 4);
		mx2.SetRC(3, 2, 3);
		mx2.SetRC(3, 3, 1);
		Assert.False((mx1 == mx2));
	}
		[Fact]
	public void MatrixMultiplication4x4to4x4()
	{
		Matrix mx1 = new Matrix(4, 4);
		mx1.SetRC(0, 0, 1);
		mx1.SetRC(0, 1, 2);
		mx1.SetRC(0, 2, 3);
		mx1.SetRC(0, 3, 4);
		mx1.SetRC(1, 0, 5);
		mx1.SetRC(1, 1, 6);
		mx1.SetRC(1, 2, 7);
		mx1.SetRC(1, 3, 8);
		mx1.SetRC(2, 0, 9);
		mx1.SetRC(2, 1, 8);
		mx1.SetRC(2, 2, 7);
		mx1.SetRC(2, 3, 6);
		mx1.SetRC(3, 0, 5);
		mx1.SetRC(3, 1, 4);
		mx1.SetRC(3, 2, 3);
		mx1.SetRC(3, 3, 2);
		Matrix mx2 = new Matrix(4, 4);
		mx2.SetRC(0, 0, -2);
		mx2.SetRC(0, 1, 1);
		mx2.SetRC(0, 2, 2);
		mx2.SetRC(0, 3, 3);
		mx2.SetRC(1, 0, 3);
		mx2.SetRC(1, 1, 2);
		mx2.SetRC(1, 2, 1);
		mx2.SetRC(1, 3, -1);
		mx2.SetRC(2, 0, 4);
		mx2.SetRC(2, 1, 3);
		mx2.SetRC(2, 2, 6);
		mx2.SetRC(2, 3, 5);
		mx2.SetRC(3, 0, 1);
		mx2.SetRC(3, 1, 2);
		mx2.SetRC(3, 2, 7);
		mx2.SetRC(3, 3, 8);
		// Matrix* res = mx1 * mx2;
		Matrix res = mx1 * mx2;
		Matrix mx3 = new Matrix(4, 4);
		mx3.SetRC(0, 0, 20);
		mx3.SetRC(0, 1, 22);
		mx3.SetRC(0, 2, 50);
		mx3.SetRC(0, 3, 48);
		mx3.SetRC(1, 0, 44);
		mx3.SetRC(1, 1, 54);
		mx3.SetRC(1, 2, 114);
		mx3.SetRC(1, 3, 108);
		mx3.SetRC(2, 0, 40);
		mx3.SetRC(2, 1, 58);
		mx3.SetRC(2, 2, 110);
		mx3.SetRC(2, 3, 102);
		mx3.SetRC(3, 0, 16);
		mx3.SetRC(3, 1, 26);
		mx3.SetRC(3, 2, 46);
		mx3.SetRC(3, 3, 42);
		Assert.True(mx3.CheckEqual(res));
		// Assert.True(mx3.CheckEqual(*res));
	}
		[Fact]
	public void MatrixMultiplicationTuple()
	{
		Matrix mx1 = new Matrix(4, 4);
		mx1.SetRC(0, 0, 1);
		mx1.SetRC(0, 1, 2);
		mx1.SetRC(0, 2, 3);
		mx1.SetRC(0, 3, 4);
		mx1.SetRC(1, 0, 2);
		mx1.SetRC(1, 1, 4);
		mx1.SetRC(1, 2, 4);
		mx1.SetRC(1, 3, 2);
		mx1.SetRC(2, 0, 8);
		mx1.SetRC(2, 1, 6);
		mx1.SetRC(2, 2, 4);
		mx1.SetRC(2, 3, 1);
		mx1.SetRC(3, 0, 0);
		mx1.SetRC(3, 1, 0);
		mx1.SetRC(3, 2, 0);
		mx1.SetRC(3, 3, 1);
		SpaceTuple tup1 = new SpaceTuple(1, 2, 3, 1);
		SpaceTuple res = mx1 * tup1;
		SpaceTuple tup2 = new SpaceTuple(18, 24, 33, 1);
		Assert.True(_fieldComp.CheckTuple(res,tup2));
	}
	[Fact]
	public void IdentityMatrixMultiplication()
	{
		Matrix mx1 = new Matrix(4, 4);
		mx1.SetRC(0, 0, 1);
		mx1.SetRC(0, 1, 2);
		mx1.SetRC(0, 2, 3);
		mx1.SetRC(0, 3, 4);
		mx1.SetRC(1, 0, 2);
		mx1.SetRC(1, 1, 4);
		mx1.SetRC(1, 2, 4);
		mx1.SetRC(1, 3, 2);
		mx1.SetRC(2, 0, 8);
		mx1.SetRC(2, 1, 6);
		mx1.SetRC(2, 2, 4);
		mx1.SetRC(2, 3, 1);
		mx1.SetRC(3, 0, 0);
		mx1.SetRC(3, 1, 0);
		mx1.SetRC(3, 2, 0);
		mx1.SetRC(3, 3, 1);
		IdentityMatrix im = new IdentityMatrix(4, 4);
		// Matrix* res = mx1 * im;
		// Assert.True(mx1.CheckEqual(*res));
		Matrix res = mx1 * im;
		Assert.True(mx1.CheckEqual(res));
	}
	[Fact]
	public void TransposeMatrix()
	{
		Matrix mx1 = new Matrix(4, 4);
		mx1.SetRC(0, 0, 0);
		mx1.SetRC(0, 1, 9);
		mx1.SetRC(0, 2, 3);
		mx1.SetRC(0, 3, 0);
		mx1.SetRC(1, 0, 9);
		mx1.SetRC(1, 1, 8);
		mx1.SetRC(1, 2, 0);
		mx1.SetRC(1, 3, 8);
		mx1.SetRC(2, 0, 1);
		mx1.SetRC(2, 1, 8);
		mx1.SetRC(2, 2, 5);
		mx1.SetRC(2, 3, 3);
		mx1.SetRC(3, 0, 0);
		mx1.SetRC(3, 1, 0);
		mx1.SetRC(3, 2, 5);
		mx1.SetRC(3, 3, 8);
		// Matrix* res = mx1.transpose();
		Matrix res = mx1.GetTranspose();
		Matrix mx2 = new Matrix(4, 4);
		mx2.SetRC(0, 0, 0);
		mx2.SetRC(0, 1, 9);
		mx2.SetRC(0, 2, 1);
		mx2.SetRC(0, 3, 0);
		mx2.SetRC(1, 0, 9);
		mx2.SetRC(1, 1, 8);
		mx2.SetRC(1, 2, 8);
		mx2.SetRC(1, 3, 0);
		mx2.SetRC(2, 0, 3);
		mx2.SetRC(2, 1, 0);
		mx2.SetRC(2, 2, 5);
		mx2.SetRC(2, 3, 5);
		mx2.SetRC(3, 0, 0);
		mx2.SetRC(3, 1, 8);
		mx2.SetRC(3, 2, 3);
		mx2.SetRC(3, 3, 8);
		// Assert.True(mx2.CheckEqual(*res));
		Assert.True(mx2.CheckEqual(res));
	}
	[Fact]
	public void TransposeIdentityMatrix ()
	{
		IdentityMatrix im = new IdentityMatrix(4, 4);
		// Matrix* res = im.transpose();
		// Assert.True(im.CheckEqual(*res));
		Matrix res = im.GetTranspose();
		Assert.True(im.CheckEqual(res));
	}
	[Fact]
	public void MatrixDeterminant()
	{
		Matrix mx1 = new Matrix(2, 2);
		mx1.SetRC(0, 0, 1);
		mx1.SetRC(0, 1, 5);
		mx1.SetRC(1, 0, -3);
		mx1.SetRC(1, 1, 2);
		double res = mx1.GetDeterminant();
		int expected = 17;
		Assert.True(_fieldComp.CheckFloat(res, expected));
	}
	[Fact]
	public void MatrixSub3x3()
	{
		Matrix mx1 = new Matrix(3, 3);
		mx1.SetRC(0, 0, 1);
		mx1.SetRC(0, 1, 5);
		mx1.SetRC(0, 2, 0);
		mx1.SetRC(1, 0, -3);
		mx1.SetRC(1, 1, 2);
		mx1.SetRC(1, 2, 7);
		mx1.SetRC(2, 0, 0);
		mx1.SetRC(2, 1, 6);
		mx1.SetRC(2, 2, -3);
		// Matrix* res = mx1.submatrix(0,2);
		Matrix res = mx1.GetSubMatrix(0,2);
		Matrix mx2 = new Matrix(2, 2);
		mx2.SetRC(0, 0, -3);
		mx2.SetRC(0, 1, 2);
		mx2.SetRC(1, 0, 0);
		mx2.SetRC(1, 1, 6);
		// Assert.True(mx2.CheckEqual(*res));
		Assert.True(mx2.CheckEqual(res));
	}

	public void MatrixSub4x4()
	{
		Matrix mx1 = new Matrix(4, 4);
		mx1.SetRC(0, 0, -6);
		mx1.SetRC(0, 1, 1);
		mx1.SetRC(0, 2, 1);
		mx1.SetRC(0, 3, 6);
		mx1.SetRC(1, 0, -8);
		mx1.SetRC(1, 1, 5);
		mx1.SetRC(1, 2, 8);
		mx1.SetRC(1, 3, 6);
		mx1.SetRC(2, 0, -1);
		mx1.SetRC(2, 1, 0);
		mx1.SetRC(2, 2, 8);
		mx1.SetRC(2, 3, 2);
		mx1.SetRC(3, 0, -7);
		mx1.SetRC(3, 1, 1);
		mx1.SetRC(3, 2, -1);
		mx1.SetRC(3, 3, 1);
		// Matrix* res = mx1.submatrix(2, 1);
		Matrix res = mx1.GetSubMatrix(2, 1);
		Matrix mx2 = new Matrix(3, 3);
		mx2.SetRC(0, 0, -6);
		mx2.SetRC(0, 1, 1);
		mx2.SetRC(0, 2, 6);
		mx2.SetRC(1, 0, -8);
		mx2.SetRC(1, 1, 8);
		mx2.SetRC(1, 2, 6);
		mx2.SetRC(2, 0, -7);
		mx2.SetRC(2, 1, -1);
		mx2.SetRC(2, 2, 1);
		// Assert.True(mx2.CheckEqual(*res));
		Assert.True(mx2.CheckEqual(res));
	}

	public void MatrixMinor()
	{

		Matrix mx1 = new Matrix(3, 3);
		mx1.SetRC(0, 0, 3);
		mx1.SetRC(0, 1, 5);
		mx1.SetRC(0, 2, 0);
		mx1.SetRC(1, 0, 2);
		mx1.SetRC(1, 1, -1);
		mx1.SetRC(1, 2, -7);
		mx1.SetRC(2, 0, 6);
		mx1.SetRC(2, 1, -1);
		mx1.SetRC(2, 2, 5);
		double res = mx1.GetMinor(1, 0);
		int expected = 25;
		Assert.Equal(res, expected);
	}

	public void MatrixCofactor()
	{
		Matrix mx1 = new Matrix(3, 3);
		mx1.SetRC(0, 0, 3);
		mx1.SetRC(0, 1, 5);
		mx1.SetRC(0, 2, 0);
		mx1.SetRC(1, 0, 2);
		mx1.SetRC(1, 1, -1);
		mx1.SetRC(1, 2, -7);
		mx1.SetRC(2, 0, 6);
		mx1.SetRC(2, 1, -1);
		mx1.SetRC(2, 2, 5);
		double m1 = mx1.GetCofactor(0, 0);
		int even = -12;
		Assert.Equal(m1, even);
		double m2 = mx1.GetCofactor(1, 0);
		int odd = -25;
		Assert.Equal(m2, odd);
	}

	public void MatrixDeteriminant3x3()
	{
		Matrix mx1 = new Matrix(3, 3);
		mx1.SetRC(0, 0, 1);
		mx1.SetRC(0, 1, 2);
		mx1.SetRC(0, 2, 6);
		mx1.SetRC(1, 0, -5);
		mx1.SetRC(1, 1, 8);
		mx1.SetRC(1, 2, -4);
		mx1.SetRC(2, 0, 2);
		mx1.SetRC(2, 1, 6);
		mx1.SetRC(2, 2, 4);
		double m1d = mx1.GetDeterminant();
		int expected = -196;
		Assert.Equal(m1d, expected);
	}
	[Fact]
	public void MatrixDeteriminant4x4()
	{
		Matrix mx1 = new Matrix(4, 4);
		mx1.SetRC(0, 0, -2);
		mx1.SetRC(0, 1, -8);
		mx1.SetRC(0, 2, 3);
		mx1.SetRC(0, 3, 5);
		mx1.SetRC(1, 0, -3);
		mx1.SetRC(1, 1, 1);
		mx1.SetRC(1, 2, 7);
		mx1.SetRC(1, 3, 3);
		mx1.SetRC(2, 0, 1);
		mx1.SetRC(2, 1, 2);
		mx1.SetRC(2, 2, -9);
		mx1.SetRC(2, 3, 6);
		mx1.SetRC(3, 0, -6);
		mx1.SetRC(3, 1, 7);
		mx1.SetRC(3, 2, 7);
		mx1.SetRC(3, 3, -9);
		double m1d = mx1.GetDeterminant();
		int expected = -4071;
		Assert.Equal(m1d, expected);
	}

	public void MatrixInvertibility()
	{
		Matrix mx1 = new Matrix(4, 4);
		mx1.SetRC(0, 0, 6);
		mx1.SetRC(0, 1, 4);
		mx1.SetRC(0, 2, 4);
		mx1.SetRC(0, 3, 4);
		mx1.SetRC(1, 0, 5);
		mx1.SetRC(1, 1, 5);
		mx1.SetRC(1, 2, 7);
		mx1.SetRC(1, 3, 6);
		mx1.SetRC(2, 0, 4);
		mx1.SetRC(2, 1, -9);
		mx1.SetRC(2, 2, 3);
		mx1.SetRC(2, 3, -7);
		mx1.SetRC(3, 0, 9);
		mx1.SetRC(3, 1, 1);
		mx1.SetRC(3, 2, 7);
		mx1.SetRC(3, 3, -6);
		Assert.True(mx1.CheckInvertible());
		Matrix mx2 = new Matrix(4, 4);
		mx2.SetRC(0, 0, -4);
		mx2.SetRC(0, 1, 2);
		mx2.SetRC(0, 2, -2);
		mx2.SetRC(0, 3, -3);
		mx2.SetRC(1, 0, 9);
		mx2.SetRC(1, 1, 6);
		mx2.SetRC(1, 2, 2);
		mx2.SetRC(1, 3, 6);
		mx2.SetRC(2, 0, 0);
		mx2.SetRC(2, 1, -5);
		mx2.SetRC(2, 2, 1);
		mx2.SetRC(2, 3, -5);
		mx2.SetRC(3, 0, 0);
		mx2.SetRC(3, 1, 0);
		mx2.SetRC(3, 2, 0);
		mx2.SetRC(3, 3, 0);
		Assert.False(mx2.CheckInvertible());
	}

	public void MatrixInverse4x4()
	{
		Matrix mx1 = new Matrix(4, 4);
		mx1.SetRC(0, 0, -5);
		mx1.SetRC(0, 1, 2);
		mx1.SetRC(0, 2, 6);
		mx1.SetRC(0, 3, -8);
		mx1.SetRC(1, 0, 1);
		mx1.SetRC(1, 1, -5);
		mx1.SetRC(1, 2, 1);
		mx1.SetRC(1, 3, 8);
		mx1.SetRC(2, 0, 7);
		mx1.SetRC(2, 1, 7);
		mx1.SetRC(2, 2, -6);
		mx1.SetRC(2, 3, -7);
		mx1.SetRC(3, 0, 1);
		mx1.SetRC(3, 1, -3);
		mx1.SetRC(3, 2, 7);
		mx1.SetRC(3, 3, 4);
		// Matrix* imx1 = mx1.invert();
		Matrix imx1 = mx1.GetInverse();
		Matrix mx2 = new Matrix(4, 4);
		mx2.SetRC(0, 0, 0.21805);
		mx2.SetRC(0, 1, 0.45113);
		mx2.SetRC(0, 2, 0.24060);
		mx2.SetRC(0, 3, -0.04511);
		mx2.SetRC(1, 0, -0.80827);
		mx2.SetRC(1, 1, -1.45677);
		mx2.SetRC(1, 2, -0.44361);
		mx2.SetRC(1, 3, 0.52068);
		mx2.SetRC(2, 0, -0.07895);
		mx2.SetRC(2, 1, -0.22368);
		mx2.SetRC(2, 2, -0.05263);
		mx2.SetRC(2, 3, 0.19737);
		mx2.SetRC(3, 0, -0.52256);
		mx2.SetRC(3, 1, -0.81391);
		mx2.SetRC(3, 2, -0.30075);
		mx2.SetRC(3, 3, 0.30639);
		// Assert.True(mx2.CheckEqual(*imx1));
		Assert.True(mx2.CheckEqual(imx1));
		List<double> mx3arr = new List<double>{ 8,-5,9,2,7,5,6,1,-6,0,9,6,-3,0,-9,-4 };
		Matrix mx3 = new Matrix(4, 4, mx3arr);
		List<double> mx3iarr = new List<double>{ -0.15385 , -0.15385 , -0.28205 , -0.53846 , -0.07692 , 0.12308 , 0.02564 , 0.03077 , 0.35897 , 0.35897 , 0.43590 , 0.92308 , -0.69231 , -0.69231 , -0.76923 , -1.92308 };
		Matrix mx3i = new Matrix(4, 4, mx3iarr);
		// Matrix* imx3 = mx3.invert();
		Matrix imx3 = mx3.GetInverse();
		// Assert.True(mx3i.CheckEqual(*imx3));
		Assert.True(mx3i.CheckEqual(imx3));
		List<double> mx4arr = new List<double>{ 9,3,0,9,-5,-2,-6,-3,-4,9,6,4,-7,6,6,2 };
		Matrix mx4 = new Matrix(4, 4, mx4arr);
		List<double> mx4iarr = new List<double>{ -0.04074 , -0.07778 , 0.14444 , -0.22222 , -0.07778 , 0.03333 , 0.36667 , -0.33333 , -0.02901 , -0.14630 , -0.10926 , 0.12963 , 0.17778 , 0.06667 , -0.26667 , 0.33333 };
		Matrix mx4i = new Matrix(4, 4, mx4iarr);
		// Matrix* imx4 = mx4.invert();
		// Assert.True(mx4i.CheckEqual(*imx4));
		Matrix imx4 = mx4.GetInverse();
		Assert.True(mx4i.CheckEqual(imx4));
	}

	public void MatrixProductByInverse4x4()
	{
		List<double> mx1arr = new List<double>{ 3,-9,7,3,3,-8,2,-9,-4,4,4,1,-6,5,-1,1 };
		Matrix mx1 = new Matrix(4, 4, mx1arr);
		List<double> mx2arr = new List<double>{ 8,2,2,2,3,-1,7,0,7,0,5,4,6,-2,-5 };
		Matrix mx2 = new Matrix(4, 4, mx2arr);
		// Matrix* mx21 = mx1 * mx2;
		// Matrix* imx2 = mx2.invert();
		// Matrix* mx21imx2 = (*mx21) * (*imx2);
		// Assert.True(mx1.CheckEqual(*mx21imx2));
		Matrix mx21 = mx1 * mx2;
		Matrix imx2 = mx2.GetInverse();
		Matrix mx21imx2 = mx21 * imx2;
		Assert.True(mx1.CheckEqual(mx21imx2));
	}

	public void TransformationTranslationMatrixMove()
	{
		TranslationMatrix tm = new TranslationMatrix(5, -3,2);
		Point p = new Point(-3, 4, 5);
		Point res = tm * p;
		Point expected = new Point(2, 1, 7);
		Assert.True(_fieldComp.CheckTuple(res, expected));
	}

	public void TransformationInverseTranslationMatrixReverseMove()
	{
		TranslationMatrix tm = new TranslationMatrix(5, -3, 2);
		// Matrix* itm = tm.invert();
		Matrix itm = tm.GetInverse();
		Point p = new Point(-3, 4, 5);
		// Point res = (*itm) * p;
		Point res = itm * p;
		Point expected = new Point(-8, 7, 3);
		Assert.True(_fieldComp.CheckTuple(res, expected));
	}

	public void TransformationTranslationMatrixVectorNoEffect()
	{
		TranslationMatrix tm = new TranslationMatrix(5, -3, 2);
		Vector v = new Vector(-3, 4, 5);
		Vector res = tm * v;
		Vector expected = new Vector(-3, 4, 5);
		Assert.True(_fieldComp.CheckTuple(res, expected));
	}

	public void TransformationScalingMatrixPoint()
	{
		ScalingMatrix sm = new ScalingMatrix(2, 3, 4);
		Point p = new Point(-4, 6, 8);
		Point res = sm * p;
		Point expected = new Point(-8, 18, 32);
		Assert.True(_fieldComp.CheckTuple(res, expected));
	}

	public void TransformationScalingMatrixVector()
	{
		ScalingMatrix sm = new ScalingMatrix(2, 3, 4);
		Vector p = new Vector(-4, 6, 8);
		Vector res = sm * p;
		Vector expected = new Vector(-8, 18, 32);
		Assert.True(_fieldComp.CheckTuple(res, expected));
	}

	public void TransformationScalingMatrixInverseVector()
	{
		ScalingMatrix sm = new ScalingMatrix(2, 3, 4);
		// Matrix* im = sm.invert();
		Matrix im = sm.GetInverse();
		Vector p = new Vector(-4, 6, 8);
		// Vector res = (*im) * p;
		Vector res = im * p;
		Vector expected = new Vector(-2, 2, 2);
		Assert.True(_fieldComp.CheckTuple(res, expected));
	}

	public void TransformationScalingMatrixReflection()
	{
		ScalingMatrix rm = new ScalingMatrix(-1, 1, 1);
		Point p = new Point(2, 3, 4);
		Point res = rm * p;
		Point expected = new Point(-2, 3, 4);
		Assert.True(_fieldComp.CheckTuple(res, expected));
	}

	public void TransformationRotationMatrixX()
	{
		Point p = new Point(0, 1, 0);
		XRotationMatrix xrm90 = new XRotationMatrix(varPM.getPI()/4);
		Point res90 = xrm90 * p;
		Point expected90 = new Point(0,Math.Sqrt(2)/2, Math.Sqrt(2)/2);
		Assert.True(_fieldComp.CheckTuple(res90, expected90));
		XRotationMatrix xrm180 = new XRotationMatrix(varPM.getPI() / 2);
		Point res180 = xrm180 * p;
		Point expected180 = new Point(0, 0, 1);
		Assert.True(_fieldComp.CheckTuple(res180, expected180));
	}

	public void TransformationRotationMatrixXInverse()
	{
		Point p = new Point(0, 1, 0);
		XRotationMatrix xrm90 = new XRotationMatrix(varPM.getPI() / 4);
		// Matrix* ixrm90 = xrm90.invert();
		Matrix ixrm90 = xrm90.GetInverse();
		// Point res = (*ixrm90) * p;
		Point res = ixrm90 * p;
		Point expected = new Point(0, Math.Sqrt(2) / 2, -Math.Sqrt(2) / 2);
		Assert.True(_fieldComp.CheckTuple(res, expected));
	}

	public void TransformationRotationMatrixY()
	{
		Point p = new Point(0, 0, 1);
		YRotationMatrix yrm90 = new YRotationMatrix(varPM.getPI() / 4);
		Point res90 = yrm90 * p;
		Point expected90 = new Point(Math.Sqrt(2) / 2, 0, Math.Sqrt(2) / 2);
		Assert.True(_fieldComp.CheckTuple(res90, expected90));
		YRotationMatrix yrm180 = new YRotationMatrix(varPM.getPI() / 2);
		Point res180 = yrm180 * p;
		Point expected180 = new Point(1, 0, 0);
		Assert.True(_fieldComp.CheckTuple(res180, expected180));
	}

	public void TransformationRotationMatrixZ()
	{
		Point p = new Point(0, 1, 0);
		ZRotationMatrix zrm90 = new ZRotationMatrix(varPM.getPI() / 4);
		Point res90 = zrm90 * p;
		Point expected90 = new Point(-Math.Sqrt(2) / 2, Math.Sqrt(2) / 2, 0);
		Assert.True(_fieldComp.CheckTuple(res90, expected90));
		ZRotationMatrix zrm180 = new ZRotationMatrix(varPM.getPI() / 2);
		Point res180 = zrm180 * p;
		Point expected180 = new Point(-1, 0, 0);
		Assert.True(_fieldComp.CheckTuple(res180, expected180));
	}

	public void TransformationShearingXToY()
	{
		ShearingMatrix sm = new ShearingMatrix(1, 0, 0, 0, 0, 0);
		Point p = new Point(2, 3, 4);
		Point res = sm * p;
		Point expected = new Point(5, 3, 4);
		Assert.True(_fieldComp.CheckTuple(res, expected));
	}

	public void TransformationShearingXToZ()
	{
		ShearingMatrix sm = new ShearingMatrix(0, 1, 0, 0, 0, 0);
		Point p = new Point(2, 3, 4);
		Point res = sm * p;
		Point expected = new Point(6, 3, 4);
		Assert.True(_fieldComp.CheckTuple(res, expected));
	}

	public void TransformationShearingYToX()
	{
		ShearingMatrix sm = new ShearingMatrix(0, 0, 1, 0, 0, 0);
		Point p = new Point(2, 3, 4);
		Point res = sm * p;
		Point expected = new Point(2, 5, 4);
		Assert.True(_fieldComp.CheckTuple(res, expected));
	}

	public void TransformationShearingYToZ()
	{
		ShearingMatrix sm = new ShearingMatrix(0, 0, 0, 1, 0, 0);
		Point p = new Point(2, 3, 4);
		Point res = sm * p;
		Point expected = new Point(2, 7, 4);
		Assert.True(_fieldComp.CheckTuple(res, expected));
	}

	public void TransformationShearingZToX()
	{
		ShearingMatrix sm = new ShearingMatrix(0, 0, 0, 0, 1, 0);
		Point p = new Point(2, 3, 4);
		Point res = sm * p;
		Point expected = new Point(2, 3, 6);
		Assert.True(_fieldComp.CheckTuple(res, expected));
	}

	public void TransformationShearingZToY()
	{
		ShearingMatrix sm = new ShearingMatrix(0, 0, 0, 0, 0, 1);
		Point p = new Point(2, 3, 4);
		Point res = sm * p;
		Point expected = new Point(2, 3, 7);
		Assert.True(_fieldComp.CheckTuple(res, expected));
	}

	public void ChainingTransformationInSequence()
	{
		Point p = new Point(1, 0, 1);
		XRotationMatrix xrm = new XRotationMatrix(varPM.getPI() / 2);
		ScalingMatrix sm = new ScalingMatrix(5, 5, 5);
		TranslationMatrix tm = new TranslationMatrix(10, 5, 7);
		Point rotated = xrm * p;
		Point rotatedExpected = new Point(1, -1, 0);
		Assert.True(_fieldComp.CheckTuple(rotated, rotatedExpected));
		Point scaled = sm * rotated;
		Point scaledExpected = new Point(5,-5,0);
		Assert.True(_fieldComp.CheckTuple(scaled, scaledExpected));
		Point translated = tm * scaled;
		Point translatedExpected = new Point(15, 0, 7);
		Assert.True(_fieldComp.CheckTuple(translated, translatedExpected));
	}

	public void ChainingTransformationAppliedInReverseOrder()
	{
		Point p = new Point(1, 0, 1);
		XRotationMatrix xrm = new XRotationMatrix(varPM.getPI() / 2);
		ScalingMatrix sm = new ScalingMatrix(5, 5, 5);
		TranslationMatrix tm = new TranslationMatrix(10, 5, 7);
		// Matrix* tsrm = *(tm * sm) * xrm;
		Matrix tsrm = tm * sm * xrm;
		// Point transformed = (*tsrm) * p;
		Point transformed = tsrm * p;
		Point expected = new Point(15, 0, 7);
		Assert.True(_fieldComp.CheckTuple(transformed, expected));
		// Matrix tsrmc = *(*(*(*(Matrix(4, 4).identity())).translate(10, 5, 7)).scale(5, 5, 5)).rotateX(getPI() / 2);
		Matrix tsrmc = new Matrix(4, 4).GetIdentity().GetTranslate(10, 5, 7).GetScale(5, 5, 5).GetRotateX(varPM.getPI() / 2);
		Assert.True(tsrm.CheckEqual(tsrmc));
		Point transformedc = tsrmc * p;
		Assert.True(_fieldComp.CheckTuple(transformedc, expected));
	}

	public void ViewTransformDefault()
	{
		Point from = new Point(0,0,0);
		Point to = new Point(0,0,-1);
		Vector up = new Vector(0,1,0);
		ViewMatrix view = new ViewMatrix(from, to, up);
		IdentityMatrix im = new IdentityMatrix(4,4);
		Assert.True(view.CheckEqual(im));
	}

	public void ViewTransformDefaultReverse()
	{
		Point from = new Point(0,0,0);
		Point to = new Point(0,0,1);
		Vector up = new Vector(0,1,0);
		ViewMatrix view = new ViewMatrix(from, to, up);
		ScalingMatrix sm = new ScalingMatrix(-1,1,-1);
		Assert.True(view.CheckEqual(sm));
	}

	public void ViewTransformMovesWorldNotEyeP()
	{
		Point from = new Point(0,0,8);
		Point to = new Point(0,0,0);
		Vector up = new Vector(0,1,0);
		ViewMatrix view = new ViewMatrix(from, to, up);
		TranslationMatrix sm = new TranslationMatrix(0,0,-8);
		Assert.True(view.CheckEqual(sm));
	}

	public void ViewTransformAngledView ()
	{
		Point from = new Point(1,3,2);
		Point to = new Point(4,-2,8);
		Vector up = new Vector(1,1,0);
		ViewMatrix view = new ViewMatrix(from, to, up);
		List<double> arr = new List<double>{-0.50709, 0.50709, 0.67612, -2.36643, 0.76772, 0.60609, 0.12122, -2.82843, -0.35857, 0.59761, -0.71714, 0, 0, 0, 0, 1 };
		Matrix sm = new Matrix(4, 4, arr);
		Assert.True(view.CheckEqual(sm));
	}
}