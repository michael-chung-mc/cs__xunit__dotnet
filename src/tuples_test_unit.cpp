#include "pch.h"
#include "comparinator.cpp"
TEST(CanaryTuples, TestTesting) {
	EXPECT_EQ(1, 1);
	EXPECT_TRUE(true);
}

class TuplesTest : public ::testing::Test {
protected:
	//TuplesTest() {}
	//~TuplesTest() override {}
	//void SetUp() override { }
	//void TearDown() override { }
};
TEST_F(TuplesTest, WOneIsPoint)
{
	Tuples a = Tuples(4.3, -4.2, 3.1, 1.0);
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
}

TEST_F(TuplesTest, WZeroIsVector)
{
	Tuples a = Tuples(4.3, -4.2, 3.1, 0.0);
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
}

TEST_F(TuplesTest, TuplesAdd)
{
	//Scenario: Adding two tuples
	//Given a1 ← tuple(3, -2, 5, 1)
	//And a2 ← tuple(-2, 3, 1, 0)
	//Then a1 + a2 = tuple(1, 1, 6, 1)
	Tuples a = Tuples(3, -2, 5, 1);
	Tuples b = Tuples(-2, 3, 1, 0);
	Tuples c = Tuples(1, 1, 6, 1);
	Comparinator ce = Comparinator();
	Tuples result = a.add(b);
	EXPECT_TRUE(ce.equalTuples(c, result));
}

TEST_F(TuplesTest, TupleMinusTupleEqualsTuple)
{
	//Scenario: Subtracting two tuples
	//Given p1 ← tuple(3, 2, 1, 1)
	//And p2 ← tuple(5, 6, 7, 1)
	//Then p1 - p2 = tuple(-2, -4, -6, 0)
	Tuples a = Tuples(3, 2, 1,1);
	Tuples b = Tuples(5, 6, 7,1);
	Tuples c = Tuples(-2, -4, -6,0);
	Comparinator ce = Comparinator();
	Tuples result = a.subtract(b);
	EXPECT_TRUE(ce.equalTuples(c, result));
}

TEST_F(TuplesTest, PointMinusPointEqualsVector)
{
	//Scenario: Subtracting two points
	//Given p1 ← tuple(3, 2, 1, 1)
	//And p2 ← tuple(5, 6, 7, 1)
	//Then p1 - p2 = tuple(-2, -4, -6, 0)
	Tuples a = Tuples(3, 2, 1, 1);
	Tuples b = Tuples(5, 6, 7, 1);
	Tuples c = Tuples(-2, -4, -6, 0);
	Comparinator ce = Comparinator();
	Tuples result = a.subtract(b);
	EXPECT_TRUE(ce.equalTuples(c, result));
}
