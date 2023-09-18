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

TEST_F(IntersectionTest, AggregationTest) {
	Sphere s = Sphere();
	double t1 = 1;
	double t2 = 2;
	Intersection i = Intersection(t1, s);
	i.intersect(t2,s);
	std::vector<Intersection> xs = i.intersections;
	EXPECT_EQ(xs.size(),2);
	EXPECT_TRUE(xs[0].time,t1);
	EXPECT_TRUE(xs[1].time, t2);
};