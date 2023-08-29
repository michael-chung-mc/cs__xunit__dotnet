#include "pch.h"
#pragma once
#ifndef Tuple_H
#define Tuple_H

class Tuple {
public:
	double x;
	double y;
	double z;
	double w;
	Tuple();
	Tuple(double argx, double argy, double argz, double argw);
	Tuple operator-();
	Tuple operator*(float multiple);
	Tuple operator/(float multiple);
	Tuple operator+(Tuple a);
	virtual Tuple add(Tuple a);
	Tuple subtract(Tuple a);
	virtual Tuple negate();
	virtual float magnitude();
	double dot(Tuple a);
};

class Vector : public Tuple {
public:
	Vector();
	Vector(double argx, double argy, double argz);
	Vector operator-();
	Vector operator+(Tuple a);
	Vector operator*(float multiple);
	Vector subtract(Tuple a);
	Vector normalize();
	Vector cross(Vector a);
};

class Point : public Tuple {
public:
	Point();
	Point(double argx, double argy, double argz);
	Point operator+(Tuple a);
	Point subtract(Tuple a);
};

#endif