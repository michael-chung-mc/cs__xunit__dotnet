#include "material.h"
#include "comparinator.h"
#include "tuple.h"
#include "color.h"
#include "light.h"
#include "pattern.h"
#include "pch.h"

class MaterialTest : public ::testing::Test {
protected:
    Comparinator ce;
	//TupleTest() {}
	//~TupleTest() override {}
	void SetUp() override {
        ce = Comparinator();
    }
	//void TearDown() override { }
};

TEST_F(MaterialTest, CanaryTest)
{
    EXPECT_EQ(1,1);
}

TEST_F(MaterialTest, MaterialCtor)
{
    Material m = Material();
    EXPECT_TRUE(ce.checkTuple(m.mbrColor, Color(1,1,1)));
    EXPECT_TRUE(ce.checkFloat(m.mbrAmbient, 0.1));
    EXPECT_TRUE(ce.checkFloat(m.mbrDiffuse, 0.9));
    EXPECT_TRUE(ce.checkFloat(m.mbrSpecular, 0.9));
    EXPECT_TRUE(ce.checkFloat(m.mbrShininess, 200.0));
}

TEST_F(MaterialTest, LightingStraightOn)
{
    Material m = Material();
    Point p = Point(0,0,0);
    Vector pov = Vector(0,0,-1);
    Vector normal = Vector(0,0,-1);
    PointSource light = PointSource(Point(0,0,-10),Color(1,1,1));
    Color res = m.getLighting(light, p, pov, normal, false);
    Color expectedLight = Color(1.9,1.9,1.9);
    EXPECT_TRUE(ce.checkTuple(res,expectedLight));
}

TEST_F(MaterialTest, Lighting45PovShift)
{
    Material m = Material();
    Point p = Point(0,0,0);
    Vector pov = Vector(0,sqrt(2)/2,sqrt(2)/2);
    Vector normal = Vector(0,0,-1);
    PointSource light = PointSource(Point(0,0,-10),Color(1,1,1));
    Color res = m.getLighting(light, p, pov, normal, false);
    Color expectedLight = Color(1.0,1.0,1.0);
    EXPECT_TRUE(ce.checkTuple(res,expectedLight));
}

TEST_F(MaterialTest, Lighting45LightShift)
{
    Material m = Material();
    Point p = Point(0,0,0);
    Vector pov = Vector(0,0,-1);
    Vector normal = Vector(0,0,-1);
    PointSource light = PointSource(Point(0,10,-10),Color(1,1,1));
    Color res = m.getLighting(light, p, pov, normal, false);
    Color expectedLight = Color(0.7364,0.7364,0.7364);
    EXPECT_TRUE(ce.checkTuple(res,expectedLight));
}

TEST_F(MaterialTest, Lighting45EyeLightShift)
{
    Material m = Material();
    Point p = Point(0,0,0);
    Vector pov = Vector(0,-sqrt(2)/2,-sqrt(2)/2);
    Vector normal = Vector(0,0,-1);
    PointSource light = PointSource(Point(0,10,-10),Color(1,1,1));
    Color res = m.getLighting(light, p, pov, normal, false);
    Color expectedLight = Color(1.6364,1.6364,1.6364);
    EXPECT_TRUE(ce.checkTuple(res,expectedLight));
}

TEST_F(MaterialTest, LightingBehindSurface)
{
    Material m = Material();
    Point p = Point(0,0,0);
    Vector pov = Vector(0,0,-1);
    Vector normal = Vector(0,0,-1);
    PointSource light = PointSource(Point(0,0,10),Color(1,1,1));
    Color res = m.getLighting(light, p, pov, normal, false);
    Color expectedLight = Color(0.1,0.1,0.1);
    EXPECT_TRUE(ce.checkTuple(res,expectedLight));
}

TEST_F(MaterialTest, LightingShadow)
{
    Material m = Material();
    Point p = Point(0,0,0);
    Vector pov = Vector(0,0,-1);
    Vector normal = Vector(0,0,-1);
    PointSource light = PointSource(Point(0,0,-10),Color(1,1,1));
    Color res = m.getLighting(light, p, pov, normal, true);
    Color expectedLight = Color(0.1,0.1,0.1);
    EXPECT_TRUE(ce.checkTuple(res,expectedLight));
}

TEST_F(MaterialTest, LightingPatternCtor)
{
    Material varMat = Material();
    varMat.mbrPattern = PatternStripe(Color(1,1,1), Color(0,0,0));
    varMat.mbrAmbient = 1;
    varMat.mbrDiffuse = 0;
    varMat.mbrSpecular = 0;
    Vector varPov = Vector(0,0,-1);
    Vector varNormal = Vector(0,0,-1);
    PointSource varLight = PointSource(Point(0,0,-10),Color(1,1,1));
    Color varStripeA = varMat.getLighting(varLight, Point(0.9,0,0), varPov, varNormal, true);
    Color varStripeB = varMat.getLighting(varLight, Point(1.1,0,0), varPov, varNormal, true);
    EXPECT_TRUE(ce.checkTuple(varStripeA,Color(1,1,1)));
    EXPECT_TRUE(ce.checkTuple(varStripeA,Color(0,0,0)));
}