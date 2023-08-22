#include "pch.h"
#include "tuples.h"

Tuples::Tuples(double argx, double argy, double argz, double argw)
{
	x = argx;
	y = argy;
	z = argz;
	w = argw;
}

Tuples Tuples::add(Tuples a)
{
	return Tuples(a.x + x, a.y + y, a.z + z, a.w + w);
}

Tuples Tuples::subtract(Tuples a)
{
	return Tuples(x - a.x, y - a.y, z - a.z, w - a.w);
}


Point::Point(double argx, double argy, double argz) : Tuples(argx, argy, argz, 1.0)
{
	//x = argx;
	//y = argy;
	//z = argz;
	//w = 1.0;
}

Vector::Vector(double argx, double argy, double argz) : Tuples(argx, argy, argz, 0.0)
{
	//x = argx;
	//y = argy;
	//z = argz;
	//w = 0.0;
}