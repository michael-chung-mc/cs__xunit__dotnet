#include "pch.h"

class Comparinator {
private:
	float EPSILON = 0.00001;
public:
	bool equalFloat(float a, float b)
	{
		return abs(a - b) < EPSILON;
	}
	bool equalTuples(Tuples a, Tuples b)
	{
		return (equalFloat(a.x, b.x) && equalFloat(a.y, b.y) && equalFloat(a.z, b.z) && equalFloat(a.w, b.w));
	}
};