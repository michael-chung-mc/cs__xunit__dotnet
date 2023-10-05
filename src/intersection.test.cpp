#include "intersection.h"
#include "form.h"
#include "comparinator.h"
#include "ray.h"
#include "pch.h"

class IntersectionTest : public ::testing::Test {
public:
	Comparinator varComp;
	void SetUp () override {
		varComp = Comparinator();
	}
};

TEST_F(IntersectionTest, CanaryTest) {
	EXPECT_EQ(1, 1);
	EXPECT_TRUE(true);
};

TEST_F(IntersectionTest, InitTest) {
	Sphere s = Sphere();
	double t = 3.5;
	Intersection i = Intersection(t, s);
	EXPECT_EQ(i.mbrTime, t);
	EXPECT_TRUE(i.mbrObject.checkEqual(s));
};

TEST_F(IntersectionTest, AggregationTest) {
	Sphere s = Sphere();
	double t1 = 1;
	double t2 = 2;
	Intersections i = Intersections(t1, s);
	i.intersect(t2,s);
	std::vector<Intersection> xs = i.mbrIntersections;
	EXPECT_EQ(xs.size(),2);
	EXPECT_EQ(xs[0].mbrTime, t1);
	EXPECT_EQ(xs[1].mbrTime, t2);
};

TEST_F(IntersectionTest, PositiveT) {
	Sphere s = Sphere();
	Intersections i = Intersections(1,s);
	i.intersect(2, s);
	std::vector<Intersection> xs = i.mbrIntersections;
	Intersection hit = i.hit();
	EXPECT_TRUE(hit.checkEqual(xs[0]));
};

TEST_F(IntersectionTest, NegativeT) {
	Sphere s = Sphere();
	Intersections i = Intersections(-1,s);
	i.intersect(2, s);
	std::vector<Intersection> xs = i.mbrIntersections;
	Intersection hit = i.hit();
	EXPECT_TRUE(hit.checkEqual(xs[1]));
};

TEST_F(IntersectionTest, AllNegativeT) {
	Sphere s = Sphere();
	Intersections i = Intersections(-1,s);
	i.intersect(-2, s);
	std::vector<Intersection> xs = i.mbrIntersections;
	Intersection hit = i.hit();
	EXPECT_FALSE(hit.mbrExists);
};

TEST_F(IntersectionTest, HitIsNonnegativeIntersection) {
	Sphere s = Sphere();
	Intersections i = Intersections(5,s);
	i.intersect(7, s);
	i.intersect(-3, s);
	i.intersect(2, s);
	std::vector<Intersection> xs = i.mbrIntersections;
	Intersection hit = i.hit();
	EXPECT_TRUE(hit.checkEqual(xs[1]));
};

TEST_F(IntersectionTest, PrecomputeIntersectionState) {
	Ray r = Ray(Point(0,0,-5), Vector(0,0,1));
	Sphere s = Sphere();
	Intersection i = Intersection(4, s);
	IntersectionState is = i.getState(r);
	EXPECT_TRUE(varComp.checkFloat(is.mbrTime, i.mbrTime));
	EXPECT_TRUE(is.mbrObject.checkEqual(i.mbrObject));
	EXPECT_TRUE(varComp.checkTuple(is.mbrPoint, Point(0,0,-1)));
	EXPECT_TRUE(varComp.checkTuple(is.mbrEye, Vector(0,0,-1)));
	EXPECT_TRUE(varComp.checkTuple(is.mbrNormal, Vector(0,0,-1)));
};

TEST_F(IntersectionTest, PrecomputeIntersectionStateInteriorHitFalse) {
	Ray r = Ray(Point(0,0,-5), Vector(0,0,1));
	Sphere s = Sphere();
	Intersection i = Intersection(4, s);
	IntersectionState is = i.getState(r);
	EXPECT_FALSE(is.mbrInside);
}

TEST_F(IntersectionTest, PrecomputeIntersectionStateInteriorHitTrue) {
	Ray r = Ray(Point(0,0,0), Vector(0,0,1));
	Sphere s = Sphere();
	Intersection i = Intersection(1, s);
	IntersectionState is = i.getState(r);
	EXPECT_TRUE(varComp.checkTuple(is.mbrPoint, Point(0,0,1)));
	EXPECT_TRUE(varComp.checkTuple(is.mbrEye, Vector(0,0,-1)));
	EXPECT_TRUE(is.mbrInside);
	EXPECT_TRUE(varComp.checkTuple(is.mbrNormal, Vector(0,0,-1)));
}

TEST_F(IntersectionTest, HitShouldOffsetPoint) {
	Ray varRay = Ray(Point(0,0,-5), Vector(0,0,1));
	Sphere varSphere = Sphere();
	varSphere.setTransform(new TranslationMatrix(0,0,1));
	Intersection varIntersection = Intersection(5, varSphere);
	IntersectionState varIs = varIntersection.getState(varRay);
	EXPECT_TRUE(varIs.mbrOverPoint.mbrZ < -getEpsilon()/2);
	EXPECT_TRUE(varIs.mbrPoint.mbrZ > varIs.mbrOverPoint.mbrZ);
}