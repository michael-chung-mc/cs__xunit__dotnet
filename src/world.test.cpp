#include "world.h"
#include "comparinator.h"
#include "color.h"
#include "pch.h"

class WorldTest : public ::testing::Test {
protected:
    Comparinator ce;
	//TupleTest() {}
	//~TupleTest() override {}
	void SetUp() override {
        ce = Comparinator();
    }
	//void TearDown() override { }
};

TEST_F(WorldTest, CanaryTest) {
	EXPECT_EQ(1, 1);
	EXPECT_TRUE(true);
};

TEST_F(WorldTest, WorldEmptyCtor) {
	World w = World();
    EXPECT_EQ(w.objects.size(), 0);
    EXPECT_TRUE(ce.checkTuple(w.light.intensity, Color(0,0,0)));
};