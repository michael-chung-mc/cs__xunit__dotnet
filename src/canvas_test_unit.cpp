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
	EXPECT_TRUE(c.isClean());
}

TEST_F(CanvasTest, CanvasSetColor)
{
	//Scenario: Writing pixels to a canvas
	//Given c ← canvas(10, 20)
	//And red ← color(1, 0, 0)
	//When write_pixel(c, 2, 3, red)
	//Then pixel_at(c, 2, 3) = red
	Comparinator ce = Comparinator();
	Canvas c = Canvas(10, 20);
	Color red = Color(1, 0, 0);
	c.setPixel(2, 3, red);
	Color test = c.getPixel(2, 3);
	EXPECT_TRUE(ce.equalTuple(test,red));
}