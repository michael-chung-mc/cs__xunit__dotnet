#include "world.h"
#include "comparinator.h"
#include "color.h"
#include "sphere.h"
#include "intersection.h"
#include "ray.h"
#include "pch.h"

class WorldTest : public ::testing::Test {
protected:
	DefaultWorld varDefaultWorld;
    Comparinator ce;
	//TupleTest() {}
	//~TupleTest() override {}
	void SetUp() override {
        varDefaultWorld = DefaultWorld();
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
    PointSource l = PointSource(Point(-10,10,-10), Color(1,1,1));
    Sphere s = Sphere();
    s.material = Material();
    s.material.color = Color (0.8,1.0,0.6);
    s.material.diffuse = 0.7;
    s.material.specular = 0.2;
    Sphere t = Sphere();
    t.transform = ScalingMatrix(0.5,0.5,0.5);
    EXPECT_TRUE(l.checkEqual(varDefaultWorld.lights[0]));
    EXPECT_TRUE(varDefaultWorld.objects[0].checkEqual(s));
    EXPECT_TRUE(varDefaultWorld.objects[1].checkEqual(t));
};

TEST_F(WorldTest, WorldRayIntersect) {
    Ray r = Ray(Point(0,0,-5), Vector(0,0,1));
    Intersections xs = varDefaultWorld.getIntersect(r);
    EXPECT_EQ(xs.intersections.size(), 4);
    EXPECT_EQ(xs.intersections[0].time, 4);
    EXPECT_EQ(xs.intersections[1].time, 4.5);
    EXPECT_EQ(xs.intersections[2].time, 5.5);
    EXPECT_EQ(xs.intersections[3].time, 6);
};

TEST_F(WorldTest, WorldIntersectionShading) {
    Ray r = Ray(Point(0,0,-5), Vector(0,0,1));
    Sphere obj = varDefaultWorld.objects[0];
    Sphere s = Sphere();
    s.material = Material();
    s.material.color = Color (0.8,1.0,0.6);
    s.material.diffuse = 0.7;
    s.material.specular = 0.2;
    EXPECT_TRUE(obj.checkEqual(s));
    Intersection i = Intersection(4,obj);
    EXPECT_TRUE(i.object.checkEqual(obj));
    IntersectionState is = i.getState(r);
    Color c = varDefaultWorld.getShade(is);
    Color expectedColor = Color(0.38066, 0.47583, 0.2855);
    EXPECT_TRUE(c.checkEqual(expectedColor));
};

TEST_F(WorldTest, WorldIntersectionInteriorShading) {
    varDefaultWorld.lights[0] = PointSource(Point(0,0.25,0), Color(1,1,1));
    Ray r = Ray(Point(0,0,0), Vector(0,0,1));
    Sphere obj = varDefaultWorld.objects[1];
    Intersection i = Intersection(0.5,obj);
    IntersectionState is = i.getState(r);
    Color c = varDefaultWorld.getShade(is);
    Color expectedColor = Color(0.90498, 0.90498, 0.90498);
    EXPECT_TRUE(c.checkEqual(expectedColor));
};

TEST_F(WorldTest, WorldColorMissIsBlack) {
    Ray r = Ray(Point(0,0,-5), Vector(0,1,0));
    Color c = varDefaultWorld.getColor(r);
    Color expectedColor = Color(0,0,0);
    EXPECT_TRUE(c.checkEqual(expectedColor));
};

TEST_F(WorldTest, WorldColorHit) {
    Ray r = Ray(Point(0,0,-5), Vector(0,0,1));
    Color c = varDefaultWorld.getColor(r);
    Color expectedColor = Color(0.38066, 0.47583, 0.2855);
    EXPECT_TRUE(c.checkEqual(expectedColor));
};

TEST_F(WorldTest, WorldColorHitInsideInnerSphere) {
    varDefaultWorld.objects[0].material.ambient = 1;
    varDefaultWorld.objects[1].material.ambient = 1;
    Ray r = Ray(Point(0,0,0.75), Vector(0,0,-1));
    Color c = varDefaultWorld.getColor(r);
    Color expectedColor = varDefaultWorld.objects[1].material.color;
    EXPECT_TRUE(c.checkEqual(expectedColor));
};

TEST_F(WorldTest, NoShadowIfObjectCollinearWithPointLight) {
    Point varPoint = Point(0,10,0);
    EXPECT_FALSE(varDefaultWorld.checkShadowed(varPoint));
};

TEST_F(WorldTest, ShadowIfObjectBetweenPointLight) {
    Point varPoint = Point(10,-10,10);
    EXPECT_TRUE(varDefaultWorld.checkShadowed(varPoint));
};

TEST_F(WorldTest, NoShadowIfObjectBehindLight) {
    Point varPoint = Point(-20,20,-20);
    EXPECT_FALSE(varDefaultWorld.checkShadowed(varPoint));
};

TEST_F(WorldTest, NoShadowIfObjectBehindPoint) {
    Point varPoint = Point(-2,2,-2);
    EXPECT_FALSE(varDefaultWorld.checkShadowed(varPoint));
};

TEST_F(WorldTest, ShadeWithShadowIntersections) {
	World varWorld = World();
    PointSource varLight = PointSource(Point(0,0,-10), Color(1,1,1));
    varWorld.setLight(varLight);
    Sphere varS1 = Sphere();
    varWorld.setObject(varS1);
    Sphere varS2 = Sphere();
    varS2.setTransform(TranslationMatrix(0,0,10));
    varWorld.setObject(varS2);
    Ray varRay = Ray(Point(0,0,5), Vector(0,0,1));
    Intersection varIx = Intersection(4,varS2);
    IntersectionState varIs = varIx.getState(varRay);
    Color varClr = varWorld.getShade(varIs);
    Color varExpectedClr = Color(0.1,0.1,0.1);
    EXPECT_TRUE(varExpectedClr.checkEqual(varClr));
};