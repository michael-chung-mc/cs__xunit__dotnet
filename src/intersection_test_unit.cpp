#include "pch.h";

class IntersectionTest : public ::testing::Test {
};

TEST_F(IntersectionTest, CanaryTest) {
	EXPECT_EQ(1, 1);
	EXPECT_TRUE(true);
};

TEST_F(IntersectionTest, InitTest) {
	Sphere s = Sphere();
	double t = 3.5;
	Intersection i = Intersection(t, s);
	EXPECT_EQ(i.time, t);
	EXPECT_TRUE(i.object.checkEqual(s));
};