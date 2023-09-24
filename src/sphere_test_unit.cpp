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
	std::vector<Intersection> xs = s.intersect(r);
	EXPECT_EQ(xs.size(), 2);
	EXPECT_EQ(xs[0].time, 4);
	EXPECT_EQ(xs[1].time, 6);
};

TEST_F(SphereTest, RayIntersectTangent) {
	Sphere s = Sphere();
	Ray r = Ray(Point(0, 1, -5), Vector(0, 0, 1));
	std::vector<Intersection> xs = s.intersect(r);
	EXPECT_EQ(xs.size(), 2);
	EXPECT_EQ(xs[0].time, 5);
	EXPECT_EQ(xs[1].time, 5);
};

TEST_F(SphereTest, RayIntersectMiss) {
	Sphere s = Sphere();
	Ray r = Ray(Point(0, 2, -5), Vector(0, 0, 1));
	std::vector<Intersection> xs = s.intersect(r);
	EXPECT_EQ(xs.size(), 0);
};

TEST_F(SphereTest, RayIntersectWithinSphere) {
	Sphere s = Sphere();
	Ray r = Ray(Point(0, 0, 0), Vector(0, 0, 1));
	std::vector<Intersection> xs = s.intersect(r);
	EXPECT_EQ(xs.size(), 2);
	EXPECT_EQ(xs[0].time, -1);
	EXPECT_EQ(xs[1].time, 1);
};

TEST_F(SphereTest, RayIntersectBehindSphere) {
	Sphere s = Sphere();
	Ray r = Ray(Point(0, 0, 5), Vector(0, 0, 1));
	std::vector<Intersection> xs = s.intersect(r);
	EXPECT_EQ(xs.size(), 2);
	EXPECT_EQ(xs[0].time, -6);
	EXPECT_EQ(xs[1].time, -4);
};

TEST_F(SphereTest, RayIntersectSetsObject) {
	Sphere s = Sphere();
	Ray r = Ray(Point(0, 0, -5), Vector(0, 0, 1));
	std::vector<Intersection> xs = s.intersect(r);
	EXPECT_EQ(xs.size(), 2);
	EXPECT_TRUE(s.checkEqual(xs[0].object));
	EXPECT_TRUE(s.checkEqual(xs[1].object));
};

TEST_F(SphereTest, SphereDefaultTransformIsIdentity) {
	Sphere s = Sphere();
	IdentityMatrix m = IdentityMatrix(4, 4);
	EXPECT_TRUE(m.checkEqual(s.transform));
};

TEST_F(SphereTest, SphereModifyTransform) {
	Sphere s = Sphere();
	TranslationMatrix m = TranslationMatrix(2, 3, 4);
	s.setTransform(m);
	EXPECT_TRUE(m.checkEqual(s.transform));
};

TEST_F(SphereTest, SphereIdentityDoesNotModifyIntersections) {
	Sphere s = Sphere();
	Ray r = Ray(Point(0, 0, -5), Vector(0, 0, 1));
	ScalingMatrix m = ScalingMatrix(1, 1, 1);
	s.setTransform(m);
	EXPECT_TRUE(m.checkEqual(s.transform));
	std::vector<Intersection> xs = s.intersect(r);
	EXPECT_EQ(xs.size(), 2);
	EXPECT_EQ(xs[0].time, 4);
	EXPECT_EQ(xs[1].time, 6);
};

TEST_F(SphereTest, SphereScaledModifiesIntersections) {
	Sphere s = Sphere();
	Ray r = Ray(Point(0, 0, -5), Vector(0, 0, 1));
	ScalingMatrix m = ScalingMatrix(2, 2, 2);
	s.setTransform(m);
	EXPECT_TRUE(m.checkEqual(s.transform));
	std::vector<Intersection> xs = s.intersect(r);
	EXPECT_EQ(xs.size(), 2);
	EXPECT_EQ(xs[0].time, 3);
	EXPECT_EQ(xs[1].time, 7);
};

TEST_F(SphereTest, SphereScaledTo5Intersections) {
	Comparinator ce = Comparinator();
	Sphere s = Sphere();
	Ray r = Ray(Point(0, 0, -5), Vector(0, 0, 1));
	ScalingMatrix m = ScalingMatrix(5, 5, 5);
	s.setTransform(m);
	EXPECT_TRUE(m.checkEqual(s.transform));
	std::vector<Intersection> xs = s.intersect(r);
	EXPECT_EQ(xs.size(), 2);
	EXPECT_TRUE(ce.checkFloat(xs[0].time, 0));
	EXPECT_TRUE(ce.checkFloat(xs[1].time, 10));
};

TEST_F(SphereTest, SphereTranslatedToMiss) {
	Sphere s = Sphere();
	Ray r = Ray(Point(0, 0, -5), Vector(0, 0, 1));
	TranslationMatrix m = TranslationMatrix(5, 0, 0);
	s.setTransform(m);
	std::vector<Intersection> xs = s.intersect(r);
	EXPECT_EQ(xs.size(), 0);
};

TEST_F(SphereTest, SphereTranslatedAway) {
	Sphere s = Sphere();
	Ray r = Ray(Point(0, 0, -5), Vector(0, 0, 1));
	TranslationMatrix m = TranslationMatrix(0, 0, 1);
	s.setTransform(m);
	std::vector<Intersection> xs = s.intersect(r);
	EXPECT_EQ(xs.size(), 2);
	EXPECT_EQ(xs[0].time, 5);
	EXPECT_EQ(xs[1].time, 7);
};