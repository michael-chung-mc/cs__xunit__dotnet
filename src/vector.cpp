#include "pch.h"
class Vector {
public:
	double x;
	double y;
	double z;
	double w;
	Vector(double argx, double argy, double argz)
	{
		x = argx;
		y = argy;
		z = argz;
		w = 0.0;
	}
};