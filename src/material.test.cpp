#include "pch.h"

class MaterialTest : public ::testing::Test {
protected:
	//TupleTest() {}
	//~TupleTest() override {}
	//void SetUp() override { }
	//void TearDown() override { }
};

TEST_F(MaterialTest, CanaryTest)
{
    EXPECT_EQ(1,1);
}

TEST_F(MaterialTest, MaterialCtor)
{
    Comparinator ce = Comparinator();
    Material m = Material();
    EXPECT_TRUE(ce.checkTuple(m.color, Color(1,1,1)));
    EXPECT_TRUE(ce.checkFloat(m.ambient, 0.1));
    EXPECT_TRUE(ce.checkFloat(m.diffuse, 0.9));
    EXPECT_TRUE(ce.checkFloat(m.specular, 0.9));
    EXPECT_TRUE(ce.checkFloat(m.shininess, 200.0));
}

TEST_F(MaterialTest, LightingStraightOn)
{
    Comparinator ce = Comparinator();
    Material m = Material();
    Point p = Point(0,0,0);
    Vector pov = Vector(0,0,-1);
    Vector normal = Vector(0,0,-1);
    PointSource light = PointSource(Point(0,0,-10),Color(1,1,1));
    Color res = m.getLighting(light, p, pov, normal);
    Color expectedLight = Color(1.9,1.9,1.9);
    EXPECT_TRUE(ce.checkTuple(res,expectedLight));
}

TEST_F(MaterialTest, Lighting45PovShift)
{
    Comparinator ce = Comparinator();
    Material m = Material();
    Point p = Point(0,0,0);
    Vector pov = Vector(0,sqrt(2)/2,sqrt(2)/2);
    Vector normal = Vector(0,0,-1);
    PointSource light = PointSource(Point(0,0,-10),Color(1,1,1));
    Color res = m.getLighting(light, p, pov, normal);
    Color expectedLight = Color(1.0,1.0,1.0);
    EXPECT_TRUE(ce.checkTuple(res,expectedLight));
}

TEST_F(MaterialTest, Lighting45LightShift)
{
    Comparinator ce = Comparinator();
    Material m = Material();
    Point p = Point(0,0,0);
    Vector pov = Vector(0,0,-1);
    Vector normal = Vector(0,0,-1);
    PointSource light = PointSource(Point(0,10,-10),Color(1,1,1));
    Color res = m.getLighting(light, p, pov, normal);
    Color expectedLight = Color(0.7364,0.7364,0.7364);
    EXPECT_TRUE(ce.checkTuple(res,expectedLight));
}

TEST_F(MaterialTest, Lighting45EyeLightShift)
{
    Comparinator ce = Comparinator();
    Material m = Material();
    Point p = Point(0,0,0);
    Vector pov = Vector(0,-sqrt(2)/2,-sqrt(2)/2);
    Vector normal = Vector(0,0,-1);
    PointSource light = PointSource(Point(0,10,-10),Color(1,1,1));
    Color res = m.getLighting(light, p, pov, normal);
    Color expectedLight = Color(1.6364,1.6364,1.6364);
    EXPECT_TRUE(ce.checkTuple(res,expectedLight));
}

TEST_F(MaterialTest, LightingBehindSurface)
{
    Comparinator ce = Comparinator();
    Material m = Material();
    Point p = Point(0,0,0);
    Vector pov = Vector(0,0,-1);
    Vector normal = Vector(0,0,-1);
    PointSource light = PointSource(Point(0,0,10),Color(1,1,1));
    Color res = m.getLighting(light, p, pov, normal);
    Color expectedLight = Color(0.1,0.1,0.1);
    EXPECT_TRUE(ce.checkTuple(res,expectedLight));
}