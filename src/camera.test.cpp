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
    EXPECT_EQ(varCamera.membSizeHorizontal, varH);
    EXPECT_EQ(varCamera.membSizeVertical, varV);
    EXPECT_TRUE(ce.checkFloat(varCamera.membFieldOfView, varFOV));
    EXPECT_TRUE(varIdentity.checkEqual(varCamera.membTransform));
}