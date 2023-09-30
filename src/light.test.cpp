#include "pch.h"

class LightTest : public ::testing::Test {
protected:
	//TupleTest() {}
	//~TupleTest() override {}
	//void SetUp() override { }
	//void TearDown() override { }
};

TEST_F(LightTest, CanaryTest)
{
    EXPECT_EQ(1,1);
}

TEST_F(LightTest, PointSourceCtor)
{
    Comparinator ce = Comparinator();
    Color c = Color(1,1,1);
    Point p = Point(0,0,0);
    PointSource ps = PointSource(p,c);
    EXPECT_TRUE(ce.checkTuple(c,ps.intensity));
    EXPECT_TRUE(ce.checkTuple(p,ps.position));
}