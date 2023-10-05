#include "world.h"
#include "comparinator.h"
#include "color.h"
#include "form.h"
#include "intersection.h"
#include "ray.h"
#include "pch.h"

class WorldTest : public ::testing::Test {
protected:
	DefaultWorld varDefaultWorld;
    Comparinator varComp;
	//TupleTest() {}
	//~TupleTest() override {}
	void SetUp() override {
        varDefaultWorld = DefaultWorld();
        varComp = Comparinator();
    }
	//void TearDown() override { }
};

TEST_F(WorldTest, CanaryTest) {
	EXPECT_EQ(1, 1);
	EXPECT_TRUE(true);
};

TEST_F(WorldTest, WorldEmptyCtor) {
	World w = World();
    EXPECT_EQ(w.mbrObjects.size(), 0);
    EXPECT_EQ(w.mbrLights.size(), 0);
};

TEST_F(WorldTest, WorldDefaultCtor) {
    PointSource l = PointSource(Point(-10,10,-10), Color(1,1,1));
    Sphere s = Sphere();
    s.mbrMaterial = Material();
    s.mbrMaterial.mbrColor = Color (0.8,1.0,0.6);
    s.mbrMaterial.mbrDiffuse = 0.7;
    s.mbrMaterial.mbrSpecular = 0.2;
    Sphere t = Sphere();
    t.mbrTransform = ScalingMatrix(0.5,0.5,0.5) * IdentityMatrix(4,4);
    EXPECT_TRUE(l.checkEqual(varDefaultWorld.mbrLights[0]));
    EXPECT_TRUE(varDefaultWorld.mbrObjects[0].checkEqual(s));
    EXPECT_TRUE(varDefaultWorld.mbrObjects[1].checkEqual(t));
};

TEST_F(WorldTest, WorldRayIntersect) {
    Ray r = Ray(Point(0,0,-5), Vector(0,0,1));
    Intersections xs = varDefaultWorld.getIntersect(r);
    EXPECT_EQ(xs.mbrIntersections.size(), 4);
    EXPECT_EQ(xs.mbrIntersections[0].mbrTime, 4);
    EXPECT_EQ(xs.mbrIntersections[1].mbrTime, 4.5);
    EXPECT_EQ(xs.mbrIntersections[2].mbrTime, 5.5);
    EXPECT_EQ(xs.mbrIntersections[3].mbrTime, 6);
};

TEST_F(WorldTest, WorldIntersectionShading) {
    Ray r = Ray(Point(0,0,-5), Vector(0,0,1));
    Form obj = varDefaultWorld.mbrObjects[0];
    Sphere s = Sphere();
    s.mbrMaterial = Material();
    s.mbrMaterial.mbrColor = Color (0.8,1.0,0.6);
    s.mbrMaterial.mbrDiffuse = 0.7;
    s.mbrMaterial.mbrSpecular = 0.2;
    EXPECT_TRUE(obj.checkEqual(s));
    Intersection i = Intersection(4,obj);
    EXPECT_TRUE(i.mbrObject.checkEqual(obj));
    IntersectionState is = i.getState(r);
    Color c = varDefaultWorld.getShade(is);
    Color expectedColor = Color(0.38066, 0.47583, 0.2855);
    EXPECT_TRUE(c.checkEqual(expectedColor));
};

TEST_F(WorldTest, WorldIntersectionInteriorShading) {
    varDefaultWorld.mbrLights[0] = PointSource(Point(0,0.25,0), Color(1,1,1));
    Ray r = Ray(Point(0,0,0), Vector(0,0,1));
    Form obj = varDefaultWorld.mbrObjects[1];
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
    varDefaultWorld.mbrObjects[0].mbrMaterial.mbrAmbient = 1;
    varDefaultWorld.mbrObjects[1].mbrMaterial.mbrAmbient = 1;
    Ray r = Ray(Point(0,0,0.75), Vector(0,0,-1));
    Color c = varDefaultWorld.getColor(r);
    Color expectedColor = varDefaultWorld.mbrObjects[1].mbrMaterial.mbrColor;
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