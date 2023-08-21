#include "pch.h"
#include "point.cpp"
#include "vector.cpp"
#include "comparinator.cpp"
TEST(CanaryTuples, TestTesting) {
	EXPECT_EQ(1, 1);
	EXPECT_TRUE(true);
}

TEST(TuplesTest, WOneIsPoint)
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

TEST(TuplesTestVector, WZeroIsVector)
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

TEST(TuplesTestAdd, TuplesAdd)
{
	//Scenario: Adding two tuples
	//Given a1 ← tuple(3, -2, 5, 1)
	//And a2 ← tuple(-2, 3, 1, 0)
	Tuples a = Tuples(3, -2, 5, 1);
	Tuples b = Tuples(-2, 3, 1, 0);
	//Then a1 + a2 = tuple(1, 1, 6, 1)
	Comparinator c = Comparinator();
	EXPECT_TRUE(c.equalTuples(Tuples(1, 1, 6, 1), a.add(b)));
}