#include "pch.h"
class Point {
public:
	double x;
	double y;
	double z;
	double w;
	Point(double argx, double argy, double argz)
	{
		x = argx;
		y = argy;
		z = argz;
		w = 1.0;
	}
};