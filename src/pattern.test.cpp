#include "pattern.h"
#include "comparinator.h"
#include "form.h"
#include "pch.h"

class PatternTest : public ::testing::Test {
protected:
    Comparinator varComp;
    PatternStripe *varPatternStripe;
	//TupleTest() {}
	//~TupleTest() override {}
	void SetUp() override {
        varComp = Comparinator();
        varPatternStripe = new PatternStripe();
    }
	void TearDown() override { 
        delete varPatternStripe;
    }
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
    EXPECT_TRUE(varP.mbrTransform->checkEqual(IdentityMatrix(4,4)));
}
TEST_F(PatternTest, PatternTransformationAssignment)
{
    Pattern varP = Pattern();
    TranslationMatrix *varTM = new TranslationMatrix(1,2,3);
    varP.setTransform(varTM);
    EXPECT_TRUE(varP.mbrTransform->checkEqual(*varTM));
    delete varTM;
}
TEST_F(PatternTest, PatternObjectTransformation)
{
    Sphere varObj = Sphere();
    varObj.setTransform(new ScalingMatrix(2,2,2));
    varObj.mbrMaterial.setPattern(new Pattern());
    Color varColor = varObj.getColorLocal(Point(2,3,4));
    EXPECT_TRUE(varColor.checkEqual(Color(1,1.5,2)));
}
TEST_F(PatternTest, PatternPatternTransformation)
{
    Sphere varObj = Sphere();
    varObj.mbrMaterial.setPattern(new Pattern());
    varObj.mbrMaterial.mbrPattern->setTransform(new ScalingMatrix(2,2,2));
    Color varColor = varObj.getColorLocal(Point(2,3,4));
    EXPECT_TRUE(varColor.checkEqual(Color(1,1.5,2)));
}
TEST_F(PatternTest, PatternObjectPatternTransformation)
{
    Sphere varObj = Sphere();
    varObj.setTransform(new ScalingMatrix(2,2,2));
    varObj.mbrMaterial.setPattern(new Pattern());
    varObj.mbrMaterial.mbrPattern->setTransform(new TranslationMatrix(0.5,1,1.5));
    Color varColor = varObj.getColorLocal(Point(2.5,3,3.5));
    EXPECT_TRUE(varColor.checkEqual(Color(0.75,0.5,0.25)));
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
    varObj.setTransform(new ScalingMatrix(2,2,2));
    varObj.mbrMaterial.setPattern(varPatternStripe);
    Color varColor = varObj.getColorLocal(Point(1.5,0,0));
    EXPECT_TRUE(varColor.checkEqual(Color(1,1,1)));
}
TEST_F(PatternTest, StripePatternPatternTransformation)
{
    Sphere varObj = Sphere();
    varObj.mbrMaterial.setPattern(varPatternStripe);
    varObj.mbrMaterial.mbrPattern->setTransform(new ScalingMatrix(2,2,2));
    Color varColor = varObj.getColorLocal(Point(1.5,0,0));
    EXPECT_TRUE(varColor.checkEqual(Color(1,1,1)));
}
TEST_F(PatternTest, StripePatternObjectPatternTransformation)
{
    Sphere varObj = Sphere();
    varObj.setTransform(new ScalingMatrix(2,2,2));
    varObj.mbrMaterial.setPattern(varPatternStripe);
    varObj.mbrMaterial.mbrPattern->setTransform(new TranslationMatrix(0.5,0,0));
    Color varColor = varObj.getColorLocal(Point(2.5,0,0));
    EXPECT_TRUE(varColor.checkEqual(Color(1,1,1)));
}