#include "pch.h"

class Comparinator {
private:
	float EPSILON = 0.00001;
public:
	bool equalFloat(float a, float b)
	{
		return abs(a - b) < EPSILON;
	}
	// tuples & point & vector
	template <typename Agent>
	bool equalTuples(Agent a, Agent b)
	{
		return (equalFloat(a.x, b.x) && equalFloat(a.y, b.y) && equalFloat(a.z, b.z) && equalFloat(a.w, b.w));
	}
};