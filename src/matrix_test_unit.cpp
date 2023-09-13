#include "pch.h"

class MatrixTest : public ::testing::Test {
protected:
	//TupleTest() {}
	//~TupleTest() override {}
	//void SetUp() override { }
	//void TearDown() override { }
};

TEST_F(MatrixTest, CanaryTest) {
	EXPECT_EQ(1, 1);
	EXPECT_TRUE(true);
};

TEST_F(MatrixTest, Matrix4x4)
{
	Comparinator ce = Comparinator();
	Matrix mx = Matrix(4,4);
	mx.setRC(0, 0, 1);
	mx.setRC(0, 1, 2);
	mx.setRC(0, 2, 3);
	mx.setRC(0, 3, 4);
	mx.setRC(1, 0, 5.5);
	mx.setRC(1, 1, 6.5);
	mx.setRC(1, 2, 7.5);
	mx.setRC(1, 3, 8.5);
	mx.setRC(2, 0, 9);
	mx.setRC(2, 1, 10);
	mx.setRC(2, 2, 11);
	mx.setRC(2, 3, 12);
	mx.setRC(3, 0, 13.5);
	mx.setRC(3, 1, 14.5);
	mx.setRC(3, 2, 15.5);
	mx.setRC(3, 3, 16.5);
	EXPECT_TRUE(ce.equalFloat(mx.getRC(0, 0), 1));
	EXPECT_TRUE(ce.equalFloat(mx.getRC(0, 1), 2));
	EXPECT_TRUE(ce.equalFloat(mx.getRC(0, 2), 3));
	EXPECT_TRUE(ce.equalFloat(mx.getRC(0, 3), 4));
	EXPECT_TRUE(ce.equalFloat(mx.getRC(1, 0), 5.5));
	EXPECT_TRUE(ce.equalFloat(mx.getRC(1, 1), 6.5));
	EXPECT_TRUE(ce.equalFloat(mx.getRC(1, 2), 7.5));
	EXPECT_TRUE(ce.equalFloat(mx.getRC(1, 3), 8.5));
	EXPECT_TRUE(ce.equalFloat(mx.getRC(2, 0), 9));
	EXPECT_TRUE(ce.equalFloat(mx.getRC(2, 1), 10));
	EXPECT_TRUE(ce.equalFloat(mx.getRC(2, 2), 11));
	EXPECT_TRUE(ce.equalFloat(mx.getRC(2, 3), 12));
	EXPECT_TRUE(ce.equalFloat(mx.getRC(3, 0), 13.5));
	EXPECT_TRUE(ce.equalFloat(mx.getRC(3, 1), 14.5));
	EXPECT_TRUE(ce.equalFloat(mx.getRC(3, 2), 15.5));
	EXPECT_TRUE(ce.equalFloat(mx.getRC(3, 3), 16.5));
};

TEST_F(MatrixTest, Matrix2x2)
{
	Comparinator ce = Comparinator();
	Matrix mx = Matrix(2, 2);
	mx.setRC(0, 0, -3);
	mx.setRC(0, 1, 5);
	mx.setRC(1, 0, 1);
	mx.setRC(1, 1, -2);
	EXPECT_TRUE(ce.equalFloat(mx.getRC(0, 0), -3));
	EXPECT_TRUE(ce.equalFloat(mx.getRC(0, 1), 5));
	EXPECT_TRUE(ce.equalFloat(mx.getRC(1, 0), 1));
	EXPECT_TRUE(ce.equalFloat(mx.getRC(1, 1), -2));
};

TEST_F(MatrixTest, Matrix3x3)
{
	Comparinator ce = Comparinator();
	Matrix mx = Matrix(3, 3);
	mx.setRC(0, 0, -3);
	mx.setRC(0, 1, 5);
	mx.setRC(0, 2, 0);
	mx.setRC(1, 0, 1);
	mx.setRC(1, 1, -2);
	mx.setRC(1, 2, -7);
	mx.setRC(2, 0, 0);
	mx.setRC(2, 1, 1);
	mx.setRC(2, 2, 1);
	EXPECT_TRUE(ce.equalFloat(mx.getRC(0, 0), -3));
	EXPECT_TRUE(ce.equalFloat(mx.getRC(0, 1), 5));
	EXPECT_TRUE(ce.equalFloat(mx.getRC(0, 2), 0));
	EXPECT_TRUE(ce.equalFloat(mx.getRC(1, 0), 1));
	EXPECT_TRUE(ce.equalFloat(mx.getRC(1, 1), -2));
	EXPECT_TRUE(ce.equalFloat(mx.getRC(1, 2), -7));
	EXPECT_TRUE(ce.equalFloat(mx.getRC(2, 0), 0));
	EXPECT_TRUE(ce.equalFloat(mx.getRC(2, 1), 1));
	EXPECT_TRUE(ce.equalFloat(mx.getRC(2, 2), 1));
};

TEST_F(MatrixTest, MatrixComparison4x4to4x4True)
{
	Matrix mx1 = Matrix(4, 4);
	mx1.setRC(0, 0, 1);
	mx1.setRC(0, 1, 2);
	mx1.setRC(0, 2, 3);
	mx1.setRC(0, 3, 4);
	mx1.setRC(1, 0, 5);
	mx1.setRC(1, 1, 6);
	mx1.setRC(1, 2, 7);
	mx1.setRC(1, 3, 8);
	mx1.setRC(2, 0, 9);
	mx1.setRC(2, 1, 8);
	mx1.setRC(2, 2, 7);
	mx1.setRC(2, 3, 6);
	mx1.setRC(3, 0, 5);
	mx1.setRC(3, 1, 4);
	mx1.setRC(3, 2, 3);
	mx1.setRC(3, 3, 2);
	Matrix mx2 = Matrix(4, 4);
	mx2.setRC(0, 0, 1);
	mx2.setRC(0, 1, 2);
	mx2.setRC(0, 2, 3);
	mx2.setRC(0, 3, 4);
	mx2.setRC(1, 0, 5);
	mx2.setRC(1, 1, 6);
	mx2.setRC(1, 2, 7);
	mx2.setRC(1, 3, 8);
	mx2.setRC(2, 0, 9);
	mx2.setRC(2, 1, 8);
	mx2.setRC(2, 2, 7);
	mx2.setRC(2, 3, 6);
	mx2.setRC(3, 0, 5);
	mx2.setRC(3, 1, 4);
	mx2.setRC(3, 2, 3);
	mx2.setRC(3, 3, 2);
	EXPECT_TRUE((mx1 == mx2));
};

TEST_F(MatrixTest, MatrixComparison4x4to4x4False)
{
	Matrix mx1 = Matrix(4, 4);
	mx1.setRC(0, 0, 1);
	mx1.setRC(0, 1, 2);
	mx1.setRC(0, 2, 3);
	mx1.setRC(0, 3, 4);
	mx1.setRC(1, 0, 5);
	mx1.setRC(1, 1, 6);
	mx1.setRC(1, 2, 7);
	mx1.setRC(1, 3, 8);
	mx1.setRC(2, 0, 9);
	mx1.setRC(2, 1, 8);
	mx1.setRC(2, 2, 7);
	mx1.setRC(2, 3, 6);
	mx1.setRC(3, 0, 5);
	mx1.setRC(3, 1, 4);
	mx1.setRC(3, 2, 3);
	mx1.setRC(3, 3, 2);
	Matrix mx2 = Matrix(4, 4);
	mx2.setRC(0, 0, 1);
	mx2.setRC(0, 1, 2);
	mx2.setRC(0, 2, 3);
	mx2.setRC(0, 3, 4);
	mx2.setRC(1, 0, 5);
	mx2.setRC(1, 1, 6);
	mx2.setRC(1, 2, 7);
	mx2.setRC(1, 3, 8);
	mx2.setRC(2, 0, 9);
	mx2.setRC(2, 1, 8);
	mx2.setRC(2, 2, 7);
	mx2.setRC(2, 3, 6);
	mx2.setRC(3, 0, 5);
	mx2.setRC(3, 1, 4);
	mx2.setRC(3, 2, 3);
	mx2.setRC(3, 3, 1);
	EXPECT_FALSE((mx1 == mx2));
};

TEST_F(MatrixTest, MatrixMultiplication4x4to4x4)
{
	Matrix mx1 = Matrix(4, 4);
	mx1.setRC(0, 0, 1);
	mx1.setRC(0, 1, 2);
	mx1.setRC(0, 2, 3);
	mx1.setRC(0, 3, 4);
	mx1.setRC(1, 0, 5);
	mx1.setRC(1, 1, 6);
	mx1.setRC(1, 2, 7);
	mx1.setRC(1, 3, 8);
	mx1.setRC(2, 0, 9);
	mx1.setRC(2, 1, 8);
	mx1.setRC(2, 2, 7);
	mx1.setRC(2, 3, 6);
	mx1.setRC(3, 0, 5);
	mx1.setRC(3, 1, 4);
	mx1.setRC(3, 2, 3);
	mx1.setRC(3, 3, 2);
	Matrix mx2 = Matrix(4, 4);
	mx2.setRC(0, 0, -2);
	mx2.setRC(0, 1, 1);
	mx2.setRC(0, 2, 2);
	mx2.setRC(0, 3, 3);
	mx2.setRC(1, 0, 3);
	mx2.setRC(1, 1, 2);
	mx2.setRC(1, 2, 1);
	mx2.setRC(1, 3, -1);
	mx2.setRC(2, 0, 4);
	mx2.setRC(2, 1, 3);
	mx2.setRC(2, 2, 6);
	mx2.setRC(2, 3, 5);
	mx2.setRC(3, 0, 1);
	mx2.setRC(3, 1, 2);
	mx2.setRC(3, 2, 7);
	mx2.setRC(3, 3, 8);
	Matrix* res = mx1 * mx2;
	Matrix mx3 = Matrix(4, 4);
	mx3.setRC(0, 0, 20);
	mx3.setRC(0, 1, 22);
	mx3.setRC(0, 2, 50);
	mx3.setRC(0, 3, 48);
	mx3.setRC(1, 0, 44);
	mx3.setRC(1, 1, 54);
	mx3.setRC(1, 2, 114);
	mx3.setRC(1, 3, 108);
	mx3.setRC(2, 0, 40);
	mx3.setRC(2, 1, 58);
	mx3.setRC(2, 2, 110);
	mx3.setRC(2, 3, 102);
	mx3.setRC(3, 0, 16);
	mx3.setRC(3, 1, 26);
	mx3.setRC(3, 2, 46);
	mx3.setRC(3, 3, 42);
	EXPECT_TRUE(mx3.checkEqual(*res));
};


TEST_F(MatrixTest, MatrixMultiplicationTuple)
{
	Comparinator ce = Comparinator();
	Matrix mx1 = Matrix(4, 4);
	mx1.setRC(0, 0, 1);
	mx1.setRC(0, 1, 2);
	mx1.setRC(0, 2, 3);
	mx1.setRC(0, 3, 4);
	mx1.setRC(1, 0, 2);
	mx1.setRC(1, 1, 4);
	mx1.setRC(1, 2, 4);
	mx1.setRC(1, 3, 2);
	mx1.setRC(2, 0, 8);
	mx1.setRC(2, 1, 6);
	mx1.setRC(2, 2, 4);
	mx1.setRC(2, 3, 1);
	mx1.setRC(3, 0, 0);
	mx1.setRC(3, 1, 0);
	mx1.setRC(3, 2, 0);
	mx1.setRC(3, 3, 1);
	Tuple tup1 = Tuple(1, 2, 3, 1);
	Tuple res = mx1 * tup1;
	Tuple tup2 = Tuple(18, 24, 33, 1);
	EXPECT_TRUE(ce.equalTuple(res,tup2));
};

TEST_F(MatrixTest, IdentityMatrixMultiplication)
{
	Matrix mx1 = Matrix(4, 4);
	mx1.setRC(0, 0, 1);
	mx1.setRC(0, 1, 2);
	mx1.setRC(0, 2, 3);
	mx1.setRC(0, 3, 4);
	mx1.setRC(1, 0, 2);
	mx1.setRC(1, 1, 4);
	mx1.setRC(1, 2, 4);
	mx1.setRC(1, 3, 2);
	mx1.setRC(2, 0, 8);
	mx1.setRC(2, 1, 6);
	mx1.setRC(2, 2, 4);
	mx1.setRC(2, 3, 1);
	mx1.setRC(3, 0, 0);
	mx1.setRC(3, 1, 0);
	mx1.setRC(3, 2, 0);
	mx1.setRC(3, 3, 1);
	IdentityMatrix im = IdentityMatrix(4, 4);
	Matrix* res = mx1 * im;
	EXPECT_TRUE(mx1.checkEqual(*res));
};


TEST_F(MatrixTest, TransposeMatrix)
{
	Matrix mx1 = Matrix(4, 4);
	mx1.setRC(0, 0, 0);
	mx1.setRC(0, 1, 9);
	mx1.setRC(0, 2, 3);
	mx1.setRC(0, 3, 0);
	mx1.setRC(1, 0, 9);
	mx1.setRC(1, 1, 8);
	mx1.setRC(1, 2, 0);
	mx1.setRC(1, 3, 8);
	mx1.setRC(2, 0, 1);
	mx1.setRC(2, 1, 8);
	mx1.setRC(2, 2, 5);
	mx1.setRC(2, 3, 3);
	mx1.setRC(3, 0, 0);
	mx1.setRC(3, 1, 0);
	mx1.setRC(3, 2, 5);
	mx1.setRC(3, 3, 8);
	Matrix* res = mx1.transpose();
	Matrix mx2 = Matrix(4, 4);
	mx2.setRC(0, 0, 0);
	mx2.setRC(0, 1, 9);
	mx2.setRC(0, 2, 1);
	mx2.setRC(0, 3, 0);
	mx2.setRC(1, 0, 9);
	mx2.setRC(1, 1, 8);
	mx2.setRC(1, 2, 8);
	mx2.setRC(1, 3, 0);
	mx2.setRC(2, 0, 3);
	mx2.setRC(2, 1, 0);
	mx2.setRC(2, 2, 5);
	mx2.setRC(2, 3, 5);
	mx2.setRC(3, 0, 0);
	mx2.setRC(3, 1, 8);
	mx2.setRC(3, 2, 3);
	mx2.setRC(3, 3, 8);
	EXPECT_TRUE(mx2.checkEqual(*res));
};

TEST_F(MatrixTest, TransposeIdentityMatrix)
{
	IdentityMatrix im = IdentityMatrix(4, 4);
	Matrix* res = im.transpose();
	EXPECT_TRUE(im.checkEqual(*res));
};

TEST_F(MatrixTest, MatrixDeterminant)
{
	Comparinator ce = Comparinator();
	Matrix mx1 = Matrix(2, 2);
	mx1.setRC(0, 0, 1);
	mx1.setRC(0, 1, 5);
	mx1.setRC(1, 0, -3);
	mx1.setRC(1, 1, 2);
	double res = mx1.determinant();
	int expected = 17;
	EXPECT_TRUE(ce.equalFloat(res, expected));
};

TEST_F(MatrixTest, MatrixSub3x3)
{
	Matrix mx1 = Matrix(3, 3);
	mx1.setRC(0, 0, 1);
	mx1.setRC(0, 1, 5);
	mx1.setRC(0, 2, 0);
	mx1.setRC(1, 0, -3);
	mx1.setRC(1, 1, 2);
	mx1.setRC(1, 2, 7);
	mx1.setRC(2, 0, 0);
	mx1.setRC(2, 1, 6);
	mx1.setRC(2, 2, -3);
	Matrix* res = mx1.submatrix(0,2);
	Matrix mx2 = Matrix(2, 2);
	mx2.setRC(0, 0, -3);
	mx2.setRC(0, 1, 2);
	mx2.setRC(1, 0, 0);
	mx2.setRC(1, 1, 6);
	EXPECT_TRUE(mx2.checkEqual(*res));
};

TEST_F(MatrixTest, MatrixSub4x4)
{

	Matrix mx1 = Matrix(4, 4);
	mx1.setRC(0, 0, -6);
	mx1.setRC(0, 1, 1);
	mx1.setRC(0, 2, 1);
	mx1.setRC(0, 3, 6);
	mx1.setRC(1, 0, -8);
	mx1.setRC(1, 1, 5);
	mx1.setRC(1, 2, 8);
	mx1.setRC(1, 3, 6);
	mx1.setRC(2, 0, -1);
	mx1.setRC(2, 1, 0);
	mx1.setRC(2, 2, 8);
	mx1.setRC(2, 3, 2);
	mx1.setRC(3, 0, -7);
	mx1.setRC(3, 1, 1);
	mx1.setRC(3, 2, -1);
	mx1.setRC(3, 3, 1);
	Matrix* res = mx1.submatrix(2, 1);
	Matrix mx2 = Matrix(3, 3);
	mx2.setRC(0, 0, -6);
	mx2.setRC(0, 1, 1);
	mx2.setRC(0, 2, 6);
	mx2.setRC(1, 0, -8);
	mx2.setRC(1, 1, 8);
	mx2.setRC(1, 2, 6);
	mx2.setRC(2, 0, -7);
	mx2.setRC(2, 1, -1);
	mx2.setRC(2, 2, 1);
	EXPECT_TRUE(mx2.checkEqual(*res));
};

TEST_F(MatrixTest, MatrixMinor)
{

	Matrix mx1 = Matrix(3, 3);
	mx1.setRC(0, 0, 3);
	mx1.setRC(0, 1, 5);
	mx1.setRC(0, 2, 0);
	mx1.setRC(1, 0, 2);
	mx1.setRC(1, 1, -1);
	mx1.setRC(1, 2, -7);
	mx1.setRC(2, 0, 6);
	mx1.setRC(2, 1, -1);
	mx1.setRC(2, 2, 5);
	double res = mx1.minor(1, 0);
	int expected = 25;
	EXPECT_EQ(res, expected);
};

TEST_F(MatrixTest, MatrixCofactor)
{
	Matrix mx1 = Matrix(3, 3);
	mx1.setRC(0, 0, 3);
	mx1.setRC(0, 1, 5);
	mx1.setRC(0, 2, 0);
	mx1.setRC(1, 0, 2);
	mx1.setRC(1, 1, -1);
	mx1.setRC(1, 2, -7);
	mx1.setRC(2, 0, 6);
	mx1.setRC(2, 1, -1);
	mx1.setRC(2, 2, 5);
	double m1 = mx1.cofactor(0, 0);
	int even = -12;
	EXPECT_EQ(m1, even);
	double m2 = mx1.cofactor(1, 0);
	int odd = -25;
	EXPECT_EQ(m2, odd);
};

TEST_F(MatrixTest, MatrixDeteriminant3x3)
{
	Matrix mx1 = Matrix(3, 3);
	mx1.setRC(0, 0, 1);
	mx1.setRC(0, 1, 2);
	mx1.setRC(0, 2, 6);
	mx1.setRC(1, 0, -5);
	mx1.setRC(1, 1, 8);
	mx1.setRC(1, 2, -4);
	mx1.setRC(2, 0, 2);
	mx1.setRC(2, 1, 6);
	mx1.setRC(2, 2, 4);
	double m1d = mx1.determinant();
	int expected = -196;
	EXPECT_EQ(m1d, expected);
};

TEST_F(MatrixTest, MatrixDeteriminant4x4)
{
	Matrix mx1 = Matrix(4, 4);
	mx1.setRC(0, 0, -2);
	mx1.setRC(0, 1, -8);
	mx1.setRC(0, 2, 3);
	mx1.setRC(0, 3, 5);
	mx1.setRC(1, 0, -3);
	mx1.setRC(1, 1, 1);
	mx1.setRC(1, 2, 7);
	mx1.setRC(1, 3, 3);
	mx1.setRC(2, 0, 1);
	mx1.setRC(2, 1, 2);
	mx1.setRC(2, 2, -9);
	mx1.setRC(2, 3, 6);
	mx1.setRC(3, 0, -6);
	mx1.setRC(3, 1, 7);
	mx1.setRC(3, 2, 7);
	mx1.setRC(3, 3, -9);
	double m1d = mx1.determinant();
	int expected = -4071;
	EXPECT_EQ(m1d, expected);
};

TEST_F(MatrixTest, MatrixInvertibility)
{
	Matrix mx1 = Matrix(4, 4);
	mx1.setRC(0, 0, 6);
	mx1.setRC(0, 1, 4);
	mx1.setRC(0, 2, 4);
	mx1.setRC(0, 3, 4);
	mx1.setRC(1, 0, 5);
	mx1.setRC(1, 1, 5);
	mx1.setRC(1, 2, 7);
	mx1.setRC(1, 3, 6);
	mx1.setRC(2, 0, 4);
	mx1.setRC(2, 1, -9);
	mx1.setRC(2, 2, 3);
	mx1.setRC(2, 3, -7);
	mx1.setRC(3, 0, 9);
	mx1.setRC(3, 1, 1);
	mx1.setRC(3, 2, 7);
	mx1.setRC(3, 3, -6);
	EXPECT_TRUE(mx1.checkInvertible());
	Matrix mx2 = Matrix(4, 4);
	mx2.setRC(0, 0, -4);
	mx2.setRC(0, 1, 2);
	mx2.setRC(0, 2, -2);
	mx2.setRC(0, 3, -3);
	mx2.setRC(1, 0, 9);
	mx2.setRC(1, 1, 6);
	mx2.setRC(1, 2, 2);
	mx2.setRC(1, 3, 6);
	mx2.setRC(2, 0, 0);
	mx2.setRC(2, 1, -5);
	mx2.setRC(2, 2, 1);
	mx2.setRC(2, 3, -5);
	mx2.setRC(3, 0, 0);
	mx2.setRC(3, 1, 0);
	mx2.setRC(3, 2, 0);
	mx2.setRC(3, 3, 0);
	EXPECT_FALSE(mx2.checkInvertible());
};

TEST_F(MatrixTest, MatrixInverse4x4)
{
	Matrix mx1 = Matrix(4, 4);
	mx1.setRC(0, 0, -5);
	mx1.setRC(0, 1, 2);
	mx1.setRC(0, 2, 6);
	mx1.setRC(0, 3, -8);
	mx1.setRC(1, 0, 1);
	mx1.setRC(1, 1, -5);
	mx1.setRC(1, 2, 1);
	mx1.setRC(1, 3, 8);
	mx1.setRC(2, 0, 7);
	mx1.setRC(2, 1, 7);
	mx1.setRC(2, 2, -6);
	mx1.setRC(2, 3, -7);
	mx1.setRC(3, 0, 1);
	mx1.setRC(3, 1, -3);
	mx1.setRC(3, 2, 7);
	mx1.setRC(3, 3, 4);
	Matrix* im = mx1.invert();
	Matrix mx2 = Matrix(4, 4);
	mx2.setRC(0, 0, 0.21805);
	mx2.setRC(0, 1, 0.45113);
	mx2.setRC(0, 2, 0.24060);
	mx2.setRC(0, 3, -0.04511);
	mx2.setRC(1, 0, -0.80827);
	mx2.setRC(1, 1, -1.45677);
	mx2.setRC(1, 2, -0.44361);
	mx2.setRC(1, 3, 0.52068);
	mx2.setRC(2, 0, -0.07895);
	mx2.setRC(2, 1, -0.22368);
	mx2.setRC(2, 2, -0.05263);
	mx2.setRC(2, 3, 0.19737);
	mx2.setRC(3, 0, -0.52256);
	mx2.setRC(3, 1, -0.81391);
	mx2.setRC(3, 2, -0.30075);
	mx2.setRC(3, 3, 0.30639);
	EXPECT_TRUE(mx2.checkEqual(*im));
};
