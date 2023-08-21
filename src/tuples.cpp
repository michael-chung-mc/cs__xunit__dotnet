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