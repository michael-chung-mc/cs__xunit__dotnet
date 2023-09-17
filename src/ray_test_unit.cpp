#include "pch.h"

class RayTest : public ::testing::Test {
protected:
	//TupleTest() {}
	//~TupleTest() override {}
	//void SetUp() override { }
	//void TearDown() override { }
};

TEST_F(RayTest, CanaryTest) {
	EXPECT_EQ(1, 1);
	EXPECT_TRUE(true);
};

TEST_F(RayTest, RayCtor) {
	Comparinator ce = Comparinator();
	Point p = Point(1, 2, 3);
	Vector d = Vector(4, 5, 6);
	Ray r = Ray(p, d);
	EXPECT_TRUE(ce.checkTuple(r.origin,p));
	EXPECT_TRUE(ce.checkTuple(r.direction,d));
};