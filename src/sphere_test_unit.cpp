#include "pch.h"

class SphereTest : public ::testing::Test {
protected:
	//TupleTest() {}
	//~TupleTest() override {}
	//void SetUp() override { }
	//void TearDown() override { }
};

TEST_F(SphereTest, CanaryTest) {
	EXPECT_EQ(1, 1);
	EXPECT_TRUE(true);
};

TEST_F(SphereTest, EqualityTest) {
	Sphere s = Sphere();
	EXPECT_TRUE(s.checkEqual(s));
};
TEST_F(SphereTest, RayIntersectTwo) {
	Sphere s = Sphere();
	Ray r = Ray(Point(0, 0, -5), Vector(0, 0, 1));
	std::vector<double> xs = s.intersect(r);
	EXPECT_EQ(xs.size(), 2);
	EXPECT_EQ(xs[0], 4);
	EXPECT_EQ(xs[1], 6);
};

TEST_F(SphereTest, RayIntersectTangent) {
	Sphere s = Sphere();
	Ray r = Ray(Point(0, 1, -5), Vector(0, 0, 1));
	std::vector<double> xs = s.intersect(r);
	EXPECT_EQ(xs.size(), 2);
	EXPECT_EQ(xs[0], 5);
	EXPECT_EQ(xs[1], 5);
};

TEST_F(SphereTest, RayIntersectMiss) {
	Sphere s = Sphere();
	Ray r = Ray(Point(0, 2, -5), Vector(0, 0, 1));
	std::vector<double> xs = s.intersect(r);
	EXPECT_EQ(xs.size(), 0);
};

TEST_F(SphereTest, RayIntersectWithinSphere) {
	Sphere s = Sphere();
	Ray r = Ray(Point(0, 0, 0), Vector(0, 0, 1));
	std::vector<double> xs = s.intersect(r);
	EXPECT_EQ(xs.size(), 2);
	EXPECT_EQ(xs[0], -1);
	EXPECT_EQ(xs[1], 1);
};

TEST_F(SphereTest, RayIntersectBehindSphere) {
	Sphere s = Sphere();
	Ray r = Ray(Point(0, 0, 5), Vector(0, 0, 1));
	std::vector<double> xs = s.intersect(r);
	EXPECT_EQ(xs.size(), 2);
	EXPECT_EQ(xs[0], -6);
	EXPECT_EQ(xs[1], -4);
};