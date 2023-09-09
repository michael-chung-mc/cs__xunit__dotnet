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
	ce.equalFloat(mx.getRC(0, 0), 1);
	ce.equalFloat(mx.getRC(0, 1), 2);
	ce.equalFloat(mx.getRC(0, 2), 3);
	ce.equalFloat(mx.getRC(0, 3), 4);
	ce.equalFloat(mx.getRC(1, 0), 5.5);
	ce.equalFloat(mx.getRC(1, 1), 6.5);
	ce.equalFloat(mx.getRC(1, 2), 7.5);
	ce.equalFloat(mx.getRC(1, 3), 8.5);
	ce.equalFloat(mx.getRC(2, 0), 9);
	ce.equalFloat(mx.getRC(2, 1), 10);
	ce.equalFloat(mx.getRC(2, 2), 11);
	ce.equalFloat(mx.getRC(2, 3), 12);
	ce.equalFloat(mx.getRC(3, 0), 13.5);
	ce.equalFloat(mx.getRC(3, 1), 14.5);
	ce.equalFloat(mx.getRC(3, 2), 15.5);
	ce.equalFloat(mx.getRC(3, 3), 16.5);
};

TEST_F(MatrixTest, Matrix2x2)
{
	Comparinator ce = Comparinator();
	Matrix mx = Matrix(2, 2);
	mx.setRC(0, 0, -3);
	mx.setRC(0, 1, 5);
	mx.setRC(1, 0, 1);
	mx.setRC(1, 1, -2);
	ce.equalFloat(mx.getRC(0, 0), -3);
	ce.equalFloat(mx.getRC(0, 1), 5);
	ce.equalFloat(mx.getRC(1, 0), 1);
	ce.equalFloat(mx.getRC(1, 1), -2);
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
	ce.equalFloat(mx.getRC(0, 0), -3);
	ce.equalFloat(mx.getRC(0, 1), 5);
	ce.equalFloat(mx.getRC(0, 2), 0);
	ce.equalFloat(mx.getRC(1, 0), 1);
	ce.equalFloat(mx.getRC(1, 1), -2);
	ce.equalFloat(mx.getRC(1, 2), 7);
	ce.equalFloat(mx.getRC(2, 0), 0);
	ce.equalFloat(mx.getRC(2, 1), 1);
	ce.equalFloat(mx.getRC(2, 2), 1);
};