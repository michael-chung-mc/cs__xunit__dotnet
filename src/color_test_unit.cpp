#include "pch.h"

class ColorTest : public ::testing::Test {
protected:
	//TupleTest() {}
	//~TupleTest() override {}
	//void SetUp() override { }
	//void TearDown() override { }
};

TEST_F(ColorTest, CanaryTest) {
	EXPECT_EQ(1, 1);
	EXPECT_TRUE(true);
};

TEST_F(ColorTest, TestInit) {
	//Scenario: Colors are(red, green, blue) tuples
	//Given c ← color(-0.5, 0.4, 1.7)
	//Then c.red = -0.5
	//And c.green = 0.4
	//And c.blue = 1.7
	Color c = Color(-0.5, 0.4, 1.7);
	EXPECT_EQ(c.r, -.5);
	EXPECT_EQ(c.g, .4);
	EXPECT_EQ(c.b, 1.7);
};