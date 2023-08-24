#include "pch.h"
#include "comparinator.cpp"

TEST(CanaryComparinator, TestTesting) {
	EXPECT_EQ(1, 1);
	EXPECT_TRUE(true);
}

TEST(TestFloatingPointEqual, TestEqualFloat) {
	Comparinator m = Comparinator();
	EXPECT_TRUE(m.equalFloat(0, .00000000));
	EXPECT_TRUE(m.equalFloat(0.00001,.00001));
	EXPECT_FALSE(m.equalFloat(0.00002, .00001));
}

TEST(TestTuplesEqual, TestEqualTuples) {
	Comparinator m = Comparinator();
	EXPECT_TRUE(m.equalTuple(Tuple(4.3, -4.2, 3.1, 1.0), Tuple(4.3, -4.2, 3.1, 1.0)));
	EXPECT_FALSE(m.equalTuple(Tuple(4.3, -4.2, 3.1, 1.0), Tuple(4.3, -4.2, 3.0, 1.0)));
}