#include "pch.h"

class ComparinatorTest : public ::testing::Test {
protected:
    Comparinator ce;
	//TupleTest() {}
	//~TupleTest() override {}
	void SetUp() override {
        ce = Comparinator();
    }
	//void TearDown() override { }
};

TEST_F(ComparinatorTest, Canary) {
	EXPECT_EQ(1, 1);
	EXPECT_TRUE(true);
}

TEST_F(ComparinatorTest, TestFloatingPointEqual) {
	EXPECT_TRUE(ce.checkFloat(0, .00000000));
	EXPECT_TRUE(ce.checkFloat(0.00001,.00001));
	EXPECT_FALSE(ce.checkFloat(0.00002, .00001));
}

TEST_F(ComparinatorTest, TestTuplesEqual) {
	EXPECT_TRUE(ce.checkTuple(Tuple(4.3, -4.2, 3.1, 1.0), Tuple(4.3, -4.2, 3.1, 1.0)));
	EXPECT_FALSE(ce.checkTuple(Tuple(4.3, -4.2, 3.1, 1.0), Tuple(4.3, -4.2, 3.0, 1.0)));
}