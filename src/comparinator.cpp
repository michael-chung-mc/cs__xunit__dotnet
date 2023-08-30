#include "pch.h"

bool Comparinator::equalFloat(float a, float b)
{
	return abs(a - b) < EPSILON;
};