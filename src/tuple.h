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
	Tuple(double argx, double argy, double argz, double argw);
	virtual Tuple add(Tuple a);
	virtual Tuple subtract(Tuple a);
	virtual Tuple negate();
	virtual Tuple operator-();
};

class Vector : public Tuple {
public:
	Vector(double argx, double argy, double argz);
	Vector subtract(Vector a);
};

class Point : public Tuple {
public:
	Point(double argx, double argy, double argz);
	Point subtract(Vector a);
};

#endif