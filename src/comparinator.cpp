#include "pch.h"

bool Comparinator::checkFloat(float a, float b)
{
	return abs(a - b) < EPSILON;
};