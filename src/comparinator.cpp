#include "comparinator.h"
#include <cmath>

bool Comparinator::checkFloat(const double &a, const double &b) const
{
	return fabs(a - b) < EPSILON;
};
