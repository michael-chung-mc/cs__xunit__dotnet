#include "pch.h"

class CanvasTest : public ::testing::Test {
protected:
	//TupleTest() {}
	//~TupleTest() override {}
	//void SetUp() override { }
	//void TearDown() override { }
};

TEST_F(CanvasTest, Canary)
{
	EXPECT_EQ(1, 1);
	EXPECT_TRUE(true);
}

TEST_F(CanvasTest, CanvasInit)
{
	//Scenario: Creating a canvas
	//Given c ← canvas(10, 20)
	//Then c.width = 10
	//And c.height = 20
	//And every pixel of c is color(0, 0, 0)
	Canvas c = Canvas(10,20);
	EXPECT_EQ(c.w, 10);
	EXPECT_EQ(c.h, 20);
}