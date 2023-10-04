#include "world.h"
#include "comparinator.h"
#include "color.h"
#include "sphere.h"
#include "intersection.h"
#include "ray.h"
#include "pch.h"

class WorldTest : public ::testing::Test {
protected:
    Comparinator ce;
	//TupleTest() {}
	//~TupleTest() override {}
	void SetUp() override {
        ce = Comparinator();
    }
	//void TearDown() override { }
};

TEST_F(WorldTest, CanaryTest) {
	EXPECT_EQ(1, 1);
	EXPECT_TRUE(true);
};

TEST_F(WorldTest, WorldEmptyCtor) {
	World w = World();
    EXPECT_EQ(w.objects.size(), 0);
    EXPECT_EQ(w.lights.size(), 0);
};

TEST_F(WorldTest, WorldDefaultCtor) {
	DefaultWorld dw = DefaultWorld();
    PointSource l = PointSource(Point(-10,-10,-10), Color(1,1,1));
    Sphere s = Sphere();
    s.material = Material();
    s.material.color = Color (0.8,1.0,0.6);
    s.material.diffuse = 0.7;
    s.material.specular = 0.2;
    Sphere t = Sphere();
    t.transform = ScalingMatrix(0.5,0.5,0.5);
    EXPECT_TRUE(l.checkEqual(dw.lights[0]));
    EXPECT_TRUE(dw.objects[0].checkEqual(s));
    EXPECT_TRUE(dw.objects[1].checkEqual(t));
};

TEST_F(WorldTest, WorldRayIntersect) {
	DefaultWorld dw = DefaultWorld();
    Ray r = Ray(Point(0,0,-5), Vector(0,0,1));
    Intersections xs = dw.intersect(r);
    EXPECT_EQ(xs.intersections.size(), 4);
    EXPECT_EQ(xs.intersections[0].time, 4);
    EXPECT_EQ(xs.intersections[1].time, 4.5);
    EXPECT_EQ(xs.intersections[2].time, 5.5);
    EXPECT_EQ(xs.intersections[3].time, 6);
};

TEST_F(WorldTest, WorldIntersectionShading) {
	DefaultWorld dw = DefaultWorld();
    Ray r = Ray(Point(0,0,-5), Vector(0,0,1));
    Sphere obj = dw.objects[0];
    Sphere s = Sphere();
    s.material = Material();
    s.material.color = Color (0.8,1.0,0.6);
    s.material.diffuse = 0.7;
    s.material.specular = 0.2;
    EXPECT_TRUE(obj.checkEqual(s));
    Intersection i = Intersection(4,obj);
    EXPECT_TRUE(i.object.checkEqual(obj));
    IntersectionState is = i.getState(r);
    Color c = dw.getShade(is);
    Color expectedColor = Color(0.38066, 0.47583, 0.2855);
    EXPECT_TRUE(c.checkEqual(expectedColor));
};

TEST_F(WorldTest, WorldIntersectionInteriorShading) {
	DefaultWorld dw = DefaultWorld();
    dw.lights[0] = PointSource(Point(0,0.25,0), Color(1,1,1));
    Ray r = Ray(Point(0,0,0), Vector(0,0,1));
    Sphere obj = dw.objects[1];
    Intersection i = Intersection(0.5,obj);
    IntersectionState is = i.getState(r);
    Color c = dw.getShade(is);
    Color expectedColor = Color(0.90498, 0.90498, 0.90498);
    EXPECT_TRUE(c.checkEqual(expectedColor));
};

TEST_F(WorldTest, WorldColorMissIsBlack) {
	DefaultWorld dw = DefaultWorld();
    Ray r = Ray(Point(0,0,-5), Vector(0,1,0));
    Color c = dw.getColor(r);
    Color expectedColor = Color(0,0,0);
    EXPECT_TRUE(c.checkEqual(expectedColor));
};

TEST_F(WorldTest, WorldColorHit) {
	DefaultWorld dw = DefaultWorld();
    Ray r = Ray(Point(0,0,-5), Vector(0,0,1));
    Color c = dw.getColor(r);
    Color expectedColor = Color(0.38066, 0.47583, 0.2855);
    EXPECT_TRUE(c.checkEqual(expectedColor));
};

TEST_F(WorldTest, WorldColorHitInsideInnerSphere) {
	DefaultWorld dw = DefaultWorld();
    dw.objects[0].material.ambient = 1;
    dw.objects[1].material.ambient = 1;
    Ray r = Ray(Point(0,0,0.75), Vector(0,0,-1));
    Color c = dw.getColor(r);
    Color expectedColor = dw.objects[1].material.color;
    EXPECT_TRUE(c.checkEqual(expectedColor));
};

TEST_F(WorldTest, NoShadowIfObjectCollinearWithPointLight) {
	DefaultWorld varWorld = DefaultWorld();
    Point varPoint = Point(0,10,0);
    EXPECT_FALSE(varWorld.checkShadowed(varPoint));
};

TEST_F(WorldTest, ShadowIfObjectBetweenPointLight) {
	DefaultWorld varWorld = DefaultWorld();
    Point varPoint = Point(10,-10,10);
    EXPECT_TRUE(varWorld.checkShadowed(varPoint));
};

TEST_F(WorldTest, NoShadowIfObjectBehindLight) {
	DefaultWorld varWorld = DefaultWorld();
    Point varPoint = Point(-20,20,-20);
    EXPECT_FALSE(varWorld.checkShadowed(varPoint));
};

TEST_F(WorldTest, NoShadowIfObjectBehindPoint) {
	DefaultWorld varWorld = DefaultWorld();
    Point varPoint = Point(-2,2,-2);
    EXPECT_FALSE(varWorld.checkShadowed(varPoint));
};