#include "intersection.h"
#include "sphere.h"
#include "pch.h"

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
	Intersections i = Intersections(t1, s);
	i.intersect(t2,s);
	std::vector<Intersection> xs = i.intersections;
	EXPECT_EQ(xs.size(),2);
	EXPECT_EQ(xs[0].time, t1);
	EXPECT_EQ(xs[1].time, t2);
};

TEST_F(IntersectionTest, PositiveT) {
	Sphere s = Sphere();
	Intersections i = Intersections(1,s);
	i.intersect(2, s);
	std::vector<Intersection> xs = i.intersections;
	Intersection hit = i.hit();
	EXPECT_TRUE(hit.checkEqual(xs[0]));
};

TEST_F(IntersectionTest, NegativeT) {
	Sphere s = Sphere();
	Intersections i = Intersections(-1,s);
	i.intersect(2, s);
	std::vector<Intersection> xs = i.intersections;
	Intersection hit = i.hit();
	EXPECT_TRUE(hit.checkEqual(xs[1]));
};

TEST_F(IntersectionTest, AllNegativeT) {
	Sphere s = Sphere();
	Intersections i = Intersections(-1,s);
	i.intersect(-2, s);
	std::vector<Intersection> xs = i.intersections;
	Intersection hit = i.hit();
	EXPECT_TRUE(hit.checkEqual(Intersection()));
};

TEST_F(IntersectionTest, UnsortedT) {
	Sphere s = Sphere();
	Intersections i = Intersections(5,s);
	i.intersect(7, s);
	i.intersect(-3, s);
	i.intersect(2, s);
	std::vector<Intersection> xs = i.intersections;
	Intersection hit = i.hit();
	EXPECT_TRUE(hit.checkEqual(xs[3]));
};