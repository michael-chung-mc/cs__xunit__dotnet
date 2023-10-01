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