#include "pch.h"
#include "tuple.h"

class Tuple;
class Point;
class Vector;

Tuple::Tuple()
{
	x = 0;
	y = 0;
	z = 0;
	w = 0;
}
Tuple::Tuple(double argx, double argy, double argz, double argw)
{
	x = argx;
	y = argy;
	z = argz;
	w = argw;
};
Tuple Tuple::operator-()
{
	return Tuple(-x, -y, -z, -w);
}

Tuple Tuple::operator*(float multiple)
{
	return Tuple(x * multiple, y * multiple, z * multiple, w * multiple);
}
Tuple Tuple::operator/(float multiple)
{
	return Tuple(x / multiple, y / multiple, z / multiple, w / multiple);
}
Tuple Tuple::operator+(Tuple a)
{
	return Tuple(a.x + x, a.y + y, a.z + z, a.w + w);;
}
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
float Tuple::magnitude()
{
	return sqrt(pow(x, 2) + pow(y, 2) + pow(z, 2) + pow(w, 2));
}
double Tuple::dot(Tuple a)
{
	return (x * a.x + y * a.y + z * a.z);
};


Point::Point() : Tuple(0, 0, 0, 1.0) {};
Point::Point(double argx, double argy, double argz) : Tuple(argx, argy, argz, 1.0) {};
Point Point::operator+(Tuple a)
{
	return Point(a.x + x, a.y + y, a.z + z);;
}
Point Point::subtract(Tuple a)
{
	return Point(x - a.x, y - a.y, z - a.z);
}

Vector::Vector() : Tuple(0, 0, 0, 0.0) {};
Vector::Vector(double argx, double argy, double argz) : Tuple(argx, argy, argz, 0.0) {};
Vector Vector::operator+(Tuple a)
{
	return Vector(a.x + x, a.y + y, a.z + z);;
}
Vector Vector::operator-()
{
	return Vector(-x, -y, -z);
}
Vector Vector::operator*(float multiple)
{
	return Vector(x * multiple, y * multiple, z * multiple);
}
Vector Vector::subtract(Tuple a)
{
	return Vector(x - a.x, y - a.y, z - a.z);
}
Vector Vector::normalize()
{
	return Vector(x / magnitude(), y / magnitude(), z / magnitude());
};
Vector Vector::cross(Vector a)
{
	return Vector(y * a.z - z * a.y, z * a.x - x * a.z, x * a.y - y * a.x);
};
