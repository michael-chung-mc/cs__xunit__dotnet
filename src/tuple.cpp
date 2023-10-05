#include "tuple.h"
#include <cmath>

class Tuple;
class Point;
class Vector;

Tuple::Tuple()
{
	argX = 0;
	argY = 0;
	argZ = 0;
	argW = 0;
}
Tuple::Tuple(double argx, double argy, double argz, double argw)
{
	argX = argx;
	argY = argy;
	argZ = argz;
	argW = argw;
};
Tuple Tuple::operator-()
{
	return Tuple(-argX, -argY, -argZ, -argW);
}

Tuple Tuple::operator*(float multiple)
{
	return Tuple(argX * multiple, argY * multiple, argZ * multiple, argW * multiple);
}
Tuple Tuple::operator/(float multiple)
{
	return Tuple(argX / multiple, argY / multiple, argZ / multiple, argW / multiple);
}
Tuple Tuple::operator+(Tuple a)
{
	return Tuple(a.argX + argX, a.argY + argY, a.argZ + argZ, a.argW + argW);;
}
Tuple Tuple::add(Tuple a)
{
	return Tuple(a.argX + argX, a.argY + argY, a.argZ + argZ, a.argW + argW);
};
Tuple Tuple::subtract(Tuple a)
{
	return Tuple(argX - a.argX, argY - a.argY, argZ - a.argZ, argW - a.argW);
};
Tuple Tuple::negate()
{
	return Tuple(-argX, -argY, -argZ, -argW);
};
float Tuple::magnitude()
{
	return sqrt(pow(argX, 2) + pow(argY, 2) + pow(argZ, 2) + pow(argW, 2));
}
double Tuple::dot(Tuple a)
{
	return (argX * a.argX + argY * a.argY + argZ * a.argZ);
};
Tuple Tuple::reflect(Tuple normal)
{
	return subtract(normal) * 2 * dot(normal);
}

Point::Point() : Tuple(0, 0, 0, 1.0) {};
Point::Point(double argx, double argy, double argz) : Tuple(argx, argy, argz, 1.0) {};
Point Point::operator+(Tuple a)
{
	return Point(a.argX + argX, a.argY + argY, a.argZ + argZ);;
}
Vector Point::operator-(Point a)
{
	return Vector(argX-a.argX, argY-a.argY, argZ-a.argZ);;
}
Point Point::subtract(Tuple a)
{
	return Point(argX - a.argX, argY - a.argY, argZ - a.argZ);
}

Vector::Vector() : Tuple(0, 0, 0, 0.0) {};
Vector::Vector(double argx, double argy, double argz) : Tuple(argx, argy, argz, 0.0) {};
Vector Vector::operator+(Tuple a)
{
	return Vector(a.argX + argX, a.argY + argY, a.argZ + argZ);;
}
Vector Vector::operator-()
{
	return Vector(-argX, -argY, -argZ);
}
Vector Vector::operator*(float multiple)
{
	return Vector(argX * multiple, argY * multiple, argZ * multiple);
}
Vector Vector::subtract(Tuple a)
{
	return Vector(argX - a.argX, argY - a.argY, argZ - a.argZ);
}
Vector Vector::normalize()
{
	return Vector(argX / magnitude(), argY / magnitude(), argZ / magnitude());
};
double Vector::dot(Vector a)
{
	return (argX * a.argX + argY * a.argY + argZ * a.argZ);
};
Vector Vector::cross(Vector a)
{
	return Vector(argY * a.argZ - argZ * a.argY, argZ * a.argX - argX * a.argZ, argX * a.argY - argY * a.argX);
};
Vector Vector::reflect(Vector normal)
{
	double varDot = dot(normal);
	varDot = varDot * 2;
	Vector ref = normal * varDot;
	ref = subtract(ref);
	return  ref;
}