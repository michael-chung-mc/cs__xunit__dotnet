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
	Tuples add(Tuples a);
	Tuples subtract(Tuples a);
};

class Point : public Tuples {
public:
	Point(double argx, double argy, double argz);
};

class Vector : public Tuples {
public:
	Vector(double argx, double argy, double argz);
};

#endif