#include "camera.h"
#include "comparinator.h"
#include "pch.h"

class CameraTest : public ::testing::Test {
protected:
    Comparinator ce;
	//TupleTest() {}
	//~TupleTest() override {}
	void SetUp() override {
        ce = Comparinator();
    }
	//void TearDown() override { }
};

TEST_F(CameraTest, Canary)
{
	EXPECT_EQ(1, 1);
	EXPECT_TRUE(true);
}

TEST_F(CameraTest, CameraDefault)
{
	int varH = 160;
    int varV = 120;
    double varFOV = getPI()/2;
    IdentityMatrix varIdentity = IdentityMatrix(4,4);
    Camera varCamera = Camera(varH, varV, varFOV);
    EXPECT_EQ(varCamera.mbrCanvasHorizontal, varH);
    EXPECT_EQ(varCamera.mbrCanvasVertical, varV);
    EXPECT_TRUE(ce.checkFloat(varCamera.mbrFieldOfView, varFOV));
    EXPECT_TRUE(varIdentity.checkEqual(varCamera.mbrTransform));
}

TEST_F(CameraTest, CameraCanvasPixelSizeHorizontalGTVertical)
{
    Camera varCamera = Camera(200, 125, getPI()/2);
    EXPECT_TRUE(ce.checkFloat(varCamera.mbrPixelSquare, 0.01));
}

TEST_F(CameraTest, CameraCanvasPixelSizeVerticalGTHorizontal)
{
    Camera varCamera = Camera(125, 200, getPI()/2);
    EXPECT_TRUE(ce.checkFloat(varCamera.mbrPixelSquare, 0.01));
}