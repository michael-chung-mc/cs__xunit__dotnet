#include "pch.h"

TEST(CanaryComparinator, TestTesting) {
	EXPECT_EQ(1, 1);
	EXPECT_TRUE(true);
}

TEST(TestFloatingPointEqual, TestEqualFloat) {
	Comparinator m = Comparinator();
	EXPECT_TRUE(m.checkFloat(0, .00000000));
	EXPECT_TRUE(m.checkFloat(0.00001,.00001));
	EXPECT_FALSE(m.checkFloat(0.00002, .00001));
}

TEST(TestTuplesEqual, TestEqualTuples) {
	Comparinator m = Comparinator();
	EXPECT_TRUE(m.checkTuple(Tuple(4.3, -4.2, 3.1, 1.0), Tuple(4.3, -4.2, 3.1, 1.0)));
	EXPECT_FALSE(m.checkTuple(Tuple(4.3, -4.2, 3.1, 1.0), Tuple(4.3, -4.2, 3.0, 1.0)));
}