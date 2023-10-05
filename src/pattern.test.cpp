#include "pattern.h"
#include "comparinator.h"
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
    EXPECT_TRUE(varP.argBlack.checkEqual(Color(0,0,0)));
    EXPECT_TRUE(varP.argWhite.checkEqual(Color(1,1,1)));
}