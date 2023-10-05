#include "pattern.h"
#include "comparinator.h"
#include "form.h"
#include "pch.h"

class PatternTest : public ::testing::Test {
protected:
    Comparinator varComp;
	//TupleTest() {}
	//~TupleTest() override {}
	void SetUp() override {
        varComp = Comparinator();
    }
	//void TearDown() override { }
};

TEST_F(PatternTest, CanaryTest)
{
    EXPECT_TRUE(true);
}

TEST_F(PatternTest, PatternCtor)
{
    Pattern varP = Pattern();
    EXPECT_TRUE(varP.mbrBlack.checkEqual(Color(0,0,0)));
    EXPECT_TRUE(varP.mbrWhite.checkEqual(Color(1,1,1)));
}

TEST_F(PatternTest, StripePatternCtor)
{
    PatternStripe varP = PatternStripe();
    EXPECT_TRUE(varP.mbrColors[0].checkEqual(varP.mbrWhite));
    EXPECT_TRUE(varP.mbrColors[1].checkEqual(varP.mbrBlack));
}
TEST_F(PatternTest, StripePatternConstantInX)
{
    PatternStripe varP = PatternStripe();
    EXPECT_TRUE(varP.getColor(Point(0,0,0)).checkEqual(varP.mbrWhite));
    EXPECT_TRUE(varP.getColor(Point(0,1,0)).checkEqual(varP.mbrWhite));
    EXPECT_TRUE(varP.getColor(Point(0,2,0)).checkEqual(varP.mbrWhite));
}
TEST_F(PatternTest, StripePatternConstantInZ)
{
    PatternStripe varP = PatternStripe();
    EXPECT_TRUE(varP.getColor(Point(0,0,0)).checkEqual(varP.mbrWhite));
    EXPECT_TRUE(varP.getColor(Point(0,0,1)).checkEqual(varP.mbrWhite));
    EXPECT_TRUE(varP.getColor(Point(0,0,2)).checkEqual(varP.mbrWhite));
}
TEST_F(PatternTest, StripePatternAlternateInX)
{
    PatternStripe varP = PatternStripe();
    EXPECT_TRUE(varP.getColor(Point(0,0,0)).checkEqual(varP.mbrWhite));
    EXPECT_TRUE(varP.getColor(Point(0.9,0,0)).checkEqual(varP.mbrWhite));
    EXPECT_TRUE(varP.getColor(Point(1,0,0)).checkEqual(varP.mbrBlack));
    EXPECT_TRUE(varP.getColor(Point(-0.1,0,0)).checkEqual(varP.mbrBlack));
    EXPECT_TRUE(varP.getColor(Point(-1,0,0)).checkEqual(varP.mbrBlack));
    EXPECT_TRUE(varP.getColor(Point(-1.1,0,0)).checkEqual(varP.mbrWhite));
}
TEST_F(PatternTest, StripePatternObjectTransformation)
{
    Sphere varObj = Sphere();
    varObj.setTransform(ScalingMatrix(2,2,2));
    varObj.mbrMaterial.mbrPattern = PatternStripe();
    Color varColor = varObj.getColor(Point(1.5,0,0));
    EXPECT_TRUE(varColor.checkEqual(Color(1,1,1)));
}
TEST_F(PatternTest, StripePatternPatternTransformation)
{
    Sphere varObj = Sphere();
    varObj.mbrMaterial.mbrPattern = PatternStripe();
    varObj.mbrMaterial.mbrPattern.setTransform(ScalingMatrix(2,2,2));
    Color varColor = varObj.getColor(Point(1.5,0,0));
    EXPECT_TRUE(varColor.checkEqual(Color(1,1,1)));
}
TEST_F(PatternTest, StripePatternObjectPatternTransformation)
{
    Sphere varObj = Sphere();
    varObj.setTransform(ScalingMatrix(2,2,2));
    varObj.mbrMaterial.mbrPattern = PatternStripe();
    varObj.mbrMaterial.mbrPattern.setTransform(TranslationMatrix(0.5,0,0));
    Color varColor = varObj.getColor(Point(2.5,0,0));
    EXPECT_TRUE(varColor.checkEqual(Color(1,1,1)));
}