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