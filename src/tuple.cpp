#include "tuple.h"
#include <cmath>

class Tuple;
class Point;
class Vector;

Tuple::Tuple()
{
	mbrX = 0;
	mbrY = 0;
	mbrZ = 0;
	mbrW = 0;
}
Tuple::Tuple(double argx, double argy, double argz, double argw)
{
	mbrX = argx;
	mbrY = argy;
	mbrZ = argz;
	mbrW = argw;
};
Tuple Tuple::operator-()
{
	return Tuple(-mbrX, -mbrY, -mbrZ, -mbrW);
}

Tuple Tuple::operator*(float multiple)
{
	return Tuple(mbrX * multiple, mbrY * multiple, mbrZ * multiple, mbrW * multiple);
}
Tuple Tuple::operator/(float multiple)
{
	return Tuple(mbrX / multiple, mbrY / multiple, mbrZ / multiple, mbrW / multiple);
}
Tuple Tuple::operator+(Tuple a)
{
	return Tuple(a.mbrX + mbrX, a.mbrY + mbrY, a.mbrZ + mbrZ, a.mbrW + mbrW);;
}
Tuple Tuple::add(Tuple a)
{
	return Tuple(a.mbrX + mbrX, a.mbrY + mbrY, a.mbrZ + mbrZ, a.mbrW + mbrW);
};
Tuple Tuple::subtract(Tuple a)
{
	return Tuple(mbrX - a.mbrX, mbrY - a.mbrY, mbrZ - a.mbrZ, mbrW - a.mbrW);
};
Tuple Tuple::negate()
{
	return Tuple(-mbrX, -mbrY, -mbrZ, -mbrW);
};
float Tuple::magnitude()
{
	return sqrt(pow(mbrX, 2) + pow(mbrY, 2) + pow(mbrZ, 2) + pow(mbrW, 2));
}
double Tuple::dot(Tuple a)
{
	return (mbrX * a.mbrX + mbrY * a.mbrY + mbrZ * a.mbrZ);
};
Tuple Tuple::reflect(Tuple normal)
{
	return subtract(normal) * 2 * dot(normal);
}

Point::Point() : Tuple(0, 0, 0, 1.0) {};
Point::Point(double argx, double argy, double argz) : Tuple(argx, argy, argz, 1.0) {};
Point Point::operator+(Tuple a)
{
	return Point(a.mbrX + mbrX, a.mbrY + mbrY, a.mbrZ + mbrZ);;
}
Vector Point::operator-(Point a)
{
	return Vector(mbrX-a.mbrX, mbrY-a.mbrY, mbrZ-a.mbrZ);;
}
Point Point::subtract(Tuple a)
{
	return Point(mbrX - a.mbrX, mbrY - a.mbrY, mbrZ - a.mbrZ);
}

Vector::Vector() : Tuple(0, 0, 0, 0.0) {};
Vector::Vector(double argx, double argy, double argz) : Tuple(argx, argy, argz, 0.0) {};
Vector Vector::operator+(Tuple a)
{
	return Vector(a.mbrX + mbrX, a.mbrY + mbrY, a.mbrZ + mbrZ);;
}
Vector Vector::operator-()
{
	return Vector(-mbrX, -mbrY, -mbrZ);
}
Vector Vector::operator*(float multiple)
{
	return Vector(mbrX * multiple, mbrY * multiple, mbrZ * multiple);
}
Vector Vector::subtract(Tuple a)
{
	return Vector(mbrX - a.mbrX, mbrY - a.mbrY, mbrZ - a.mbrZ);
}
Vector Vector::normalize()
{
	return Vector(mbrX / magnitude(), mbrY / magnitude(), mbrZ / magnitude());
};
double Vector::dot(Vector a)
{
	return (mbrX * a.mbrX + mbrY * a.mbrY + mbrZ * a.mbrZ);
};
Vector Vector::cross(Vector a)
{
	return Vector(mbrY * a.mbrZ - mbrZ * a.mbrY, mbrZ * a.mbrX - mbrX * a.mbrZ, mbrX * a.mbrY - mbrY * a.mbrX);
};
Vector Vector::reflect(Vector normal)
{
	double varDot = dot(normal);
	varDot = varDot * 2;
	Vector ref = normal * varDot;
	ref = subtract(ref);
	return  ref;
}