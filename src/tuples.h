#include "pch.h"
#pragma once
#ifndef Tuples_H
#define Tuples_H

class Tuples {
public:
	double x;
	double y;
	double z;
	double w;
	Tuples(double argx, double argy, double argz, double argw);
	virtual Tuples add(Tuples a);
	virtual Tuples subtract(Tuples a);
};

class Vector : public Tuples {
public:
	Vector(double argx, double argy, double argz);
	Vector subtract(Vector a);
};

class Point : public Tuples {
public:
	Point(double argx, double argy, double argz);
	Point subtract(Vector a);
};

#endif