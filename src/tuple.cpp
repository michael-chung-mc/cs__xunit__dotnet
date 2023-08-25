#include "pch.h"
#include "tuple.h"

class Tuple;
class Point;
class Vector;

Tuple::Tuple(double argx, double argy, double argz, double argw)
{
	x = argx;
	y = argy;
	z = argz;
	w = argw;
};

Tuple Tuple::add(Tuple a)
{
	return Tuple(a.x + x, a.y + y, a.z + z, a.w + w);
};

Tuple Tuple::subtract(Tuple a)
{
	return Tuple(x - a.x, y - a.y, z - a.z, w - a.w);
};

Tuple Tuple::negate()
{
	return Tuple(-x, -y, -z, -w);
};

Tuple Tuple::operator-()
{
	return Tuple(-x, -y, -z, -w);
}

Tuple Tuple::operator*(float multiple)
{
	return Tuple(x*multiple, y*multiple, z*multiple, w*multiple);
}

Point::Point(double argx, double argy, double argz) : Tuple(argx, argy, argz, 1.0) {};

Point Point::subtract(Vector a)
{
	return Point(x - a.x, y - a.y, z - a.z);
}

Vector::Vector(double argx, double argy, double argz) : Tuple(argx, argy, argz, 0.0) {};

Vector Vector::subtract(Vector a)
{
	return Vector(x - a.x, y - a.y, z - a.z);
}

Vector Vector::operator-()
{
	return Vector(-x, -y, -z);
}