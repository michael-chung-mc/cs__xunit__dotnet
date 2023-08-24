#include "pch.h"
#include "comparinator.cpp"
TEST(CanaryTuple, TestTesting) {
	EXPECT_EQ(1, 1);
	EXPECT_TRUE(true);
};

class TupleTest : public ::testing::Test {
protected:
	//TupleTest() {}
	//~TupleTest() override {}
	//void SetUp() override { }
	//void TearDown() override { }
};
TEST_F(TupleTest, TupleWOneIsPoint)
{
	Tuple a = Tuple(4.3, -4.2, 3.1, 1.0);
	EXPECT_EQ(a.x, 4.3);
	EXPECT_EQ(a.y, -4.2);
	EXPECT_EQ(a.z, 3.1);
	EXPECT_EQ(a.w, 1.0);
	Point ap = Point(4.3, -4.2, 3.1);
	EXPECT_EQ(a.x, ap.x);
	EXPECT_EQ(a.y, ap.y);
	EXPECT_EQ(a.z, ap.z);
	EXPECT_EQ(a.w, ap.w);
	Vector av = Vector(4.3, -4.2, 3.1);
	EXPECT_EQ(a.x, av.x);
	EXPECT_EQ(a.y, av.y);
	EXPECT_EQ(a.z, av.z);
	EXPECT_NE(a.w, av.w);
};

TEST_F(TupleTest, TupleWZeroIsVector)
{
	Tuple a = Tuple(4.3, -4.2, 3.1, 0.0);
	EXPECT_EQ(a.x, 4.3);
	EXPECT_EQ(a.y, -4.2);
	EXPECT_EQ(a.z, 3.1);
	EXPECT_EQ(a.w, 0.0);
	Point ap = Point(4.3, -4.2, 3.1);
	EXPECT_EQ(a.x, ap.x);
	EXPECT_EQ(a.y, ap.y);
	EXPECT_EQ(a.z, ap.z);
	EXPECT_NE(a.w, ap.w);
	Vector av = Vector(4.3, -4.2, 3.1);
	EXPECT_EQ(a.x, av.x);
	EXPECT_EQ(a.y, av.y);
	EXPECT_EQ(a.z, av.z);
	EXPECT_EQ(a.w, av.w);
};

TEST_F(TupleTest, TuplePlusTupleEqualsTuple)
{
	//Scenario: Adding two Tuple
	//Given a1 ← tuple(3, -2, 5, 1)
	//And a2 ← tuple(-2, 3, 1, 0)
	//Then a1 + a2 = tuple(1, 1, 6, 1)
	Tuple a = Tuple(3, -2, 5, 1);
	Tuple b = Tuple(-2, 3, 1, 0);
	Tuple c = Tuple(1, 1, 6, 1);
	Comparinator ce = Comparinator();
	Tuple result = a.add(b);
	EXPECT_TRUE(ce.equalTuple(c, result));
};

TEST_F(TupleTest, TupleMinusTupleEqualsTuple)
{
	//Scenario: Subtracting two Tuple
	//Given p1 ← tuple(3, 2, 1, 1)
	//And p2 ← tuple(5, 6, 7, 1)
	//Then p1 - p2 = tuple(-2, -4, -6, 0)
	Tuple a = Tuple(3, 2, 1, 1);
	Tuple b = Tuple(5, 6, 7, 1);
	Tuple c = Tuple(-2, -4, -6, 0);
	Comparinator ce = Comparinator();
	Tuple result = a.subtract(b);
	EXPECT_TRUE(ce.equalTuple(c, result));
};

TEST_F(TupleTest, PointMinusVectorEqualsPoint)
{
	//Scenario: Subtracting two points
	//Given p1 ← tuple(3, 2, 1, 1)
	//And p2 ← tuple(5, 6, 7, 1)
	//Then p1 - p2 = tuple(-2, -4, -6, 0)
	Point a = Point(3, 2, 1);
	Vector b = Vector(5, 6, 7);
	Point c = Point(-2, -4, -6);
	Comparinator ce = Comparinator();
	Point result = a.subtract(b);
	EXPECT_TRUE(ce.equalTuple(c, result));
};

TEST_F(TupleTest, VectorMinusVectorEqualsVector)
{
	//Scenario: Subtracting two vectors
	//Given v1 ← vector(3, 2, 1)
	//And v2 ← vector(5, 6, 7)
	//Then v1 - v2 = vector(-2, -4, -6)
	Vector a = Vector(3, 2, 1);
	Vector b = Vector(5, 6, 7);
	Vector c = Vector(-2, -4, -6);
	Comparinator ce = Comparinator();
	Vector result = a.subtract(b);
	EXPECT_TRUE(ce.equalTuple(c, result));
};

TEST_F(TupleTest, ZeroMinusVectorEqualsNegatedVector)
{
	//Scenario: Subtracting a vector from the zero vector
	//Given zero ← vector(0, 0, 0)
	//And v ← vector(1, -2, 3)
	//Then zero - v = vector(-1, 2, -3)
	Vector a = Vector(0, 0, 0);
	Vector b = Vector(1, -2, 3);
	Vector c = Vector(-1, 2, -3);
	Comparinator ce = Comparinator();
	Vector result = a.subtract(b);
	EXPECT_TRUE(ce.equalTuple(c, result));
};

TEST_F(TupleTest, NegateTupleMethod)
{
	//Scenario: Negating a tuple
	//Given a ← tuple(1, -2, 3, -4)
	//Then - a = tuple(-1, 2, -3, 4)
	Tuple a = Tuple(1, -2, 3, -4);
	Tuple an = a.negate();
	Tuple c = Tuple(-1, 2, -3, 4);
	Comparinator ce = Comparinator();
	EXPECT_TRUE(ce.equalTuple(an, c));
}

TEST_F(TupleTest, NegateZeroTuple)
{
	Tuple a = Tuple(0,0,0,0);
	Tuple an = -a;
	Tuple c = Tuple(0,0,0,0);
	Comparinator ce = Comparinator();
	EXPECT_TRUE(ce.equalTuple(an, c));
}

TEST_F(TupleTest, NegateTupleOperator)
{
	//Scenario: Negating a tuple
	//Given a ← tuple(1, -2, 3, -4)
	//Then - a = tuple(-1, 2, -3, 4)
	Tuple a = Tuple(1, -2, 3, -4);
	Tuple an = -a;
	Tuple c = Tuple(-1, 2, -3, 4);
	Comparinator ce = Comparinator();
	EXPECT_TRUE(ce.equalTuple(an, c));
}